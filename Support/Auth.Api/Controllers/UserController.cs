using Auth.Api.Commands;
using Auth.Api.Infrastructure;
using Auth.Api.Infrastructure.Data;
using Auth.Api.Models;
using Auth.Api.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly AuthContext authContext;
    private readonly JwtOptions jwtOptions;
    private readonly SigningCredentials signingCredentials;


    public UserController(AuthContext authContext, IOptions<JwtOptions> options, SigningCredentials signingCredentials)
    {
        this.authContext = authContext;
        jwtOptions = options.Value;
        this.signingCredentials = signingCredentials;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody]LoginCommandDto command)
    {

        //we search the database for the given phone number
        var user = await authContext.Users.FirstOrDefaultAsync(x => x.PhoneNumber == command.PhoneNumber);
        //if not found return exception
        if (user == null)
        {
            return NotFound();
        }
        //else
        //check the password with the stored password in DB for equality
        else
        {

            //if not equal, return exception
            if (CryptographyUtility.VerifyPassword(command.Password, user.Password) == false)
            {
                return Unauthorized();
            }

            //else
            //generate a JWT token string and return it
            else
            {
                var token = JwtTokenIssuer.IssueJwtToken(user.Id, signingCredentials, jwtOptions.Issuer,jwtOptions.Audience ,jwtOptions.TokenTimeout);
                var refreshToken = CryptographyUtility.GenerateRefreshToken();
                await authContext.UserRefreshTokens.AddAsync(new UserRefreshToken(user.Id, refreshToken, jwtOptions.TokenTimeout));
                await authContext.SaveChangesAsync();
                return Ok(new Dictionary<string, string>
                {
                    {"token",token },
                    {"refreshToken",refreshToken },
                    {"timeout",jwtOptions.TokenTimeout.ToString()}
                });
            }
        }

    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody]RegisterCommandDto command)
    {
        //we search the database for the given phonenumber
        var user = await authContext.Users.FirstOrDefaultAsync(x => x.PhoneNumber == command.PhoneNumber);
        //if any of them was found return "username/email already exists" exception 
        if (user != null)
        {
            throw new Exception("An account with the same phone number already exists please use forgot password to recover your account");
        }

        //else
        //hash the password (here we can also check if the password is strong enough before hashing)
        var hashedPw = CryptographyUtility.HashPassword(command.Password);
        //create a new record in DB for the new user (remember to make IsEmailConfirmed column false)
        User newUser = new(command.FName, command.LName, command.PhoneNumber, hashedPw);


        await authContext.AddAsync(newUser);
        //generate a JWT token string and return it
        var token = JwtTokenIssuer.IssueJwtToken(newUser.Id, signingCredentials,jwtOptions.Issuer,jwtOptions.Audience ,
            jwtOptions.TokenTimeout);

        var refreshToken = new UserRefreshToken(newUser.Id, CryptographyUtility.GenerateRefreshToken(), jwtOptions.TokenTimeout);
        await authContext.UserRefreshTokens.AddAsync(refreshToken);
        await authContext.SaveChangesAsync();



        return Ok(new Dictionary<string, string>
        {
            {"token",token },
            {"refreshToken",refreshToken.RefreshToken },
            {"timeout",jwtOptions.TokenTimeout.ToString()}
        });
    }


    [HttpGet("get")]
    public async Task<IActionResult> GetAsync()
    {
       var userIdString = Request.Headers["X-User-Id"].FirstOrDefault();
       Guid userId;
       if (string.IsNullOrWhiteSpace( userIdString) || Guid.TryParse(userIdString,out userId) == false)
           return BadRequest();
        var user = await authContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        return Ok(user);
    }

        [HttpPost("refresh")]
    public async Task<IActionResult> RenewTokenAsync(Guid userId, string refreshToken)
    {
        var user = await authContext.UserRefreshTokens.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken && x.UserId == userId && x.IsValid);
        if (user == null)
        {
            throw new Exception();
        }
        else
        {
            var token = JwtTokenIssuer.IssueJwtToken(userId, signingCredentials, jwtOptions.Issuer, jwtOptions.Audience,
                jwtOptions.TokenTimeout);
            var newRefreshToken = CryptographyUtility.GenerateRefreshToken();
            await authContext.UserRefreshTokens.AddAsync(new UserRefreshToken(userId, newRefreshToken, jwtOptions.TokenTimeout));
            await authContext.SaveChangesAsync();
            return Ok(new Dictionary<string, string>
        {
            {"token",token },
            {"refreshToken",refreshToken },
            {"timeout",jwtOptions.TokenTimeout.ToString()}
        });
        }

    }
}
