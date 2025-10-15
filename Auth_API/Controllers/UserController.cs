using Auth_API.Data;
using Auth_API.Models;
using Auth_API.Options;
using Auth_API.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Auth_API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly AuthContext authContext;
    private readonly JwtOptions jwtOptions;


    public UserController(AuthContext authContext, IOptions<JwtOptions> options)
    {
        this.authContext = authContext;
        jwtOptions = options.Value;
    }

    [HttpPost("login")]
    public IActionResult Login(string phoneNumber, string password)
    {

        //we search the database for the given phone number
        var user = authContext.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
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
            if (CryptographyUtility.VerifyPassword(password, user.Password) == false)
            {
                return Unauthorized();
            }

            //else
            //generate a JWT token string and return it
            else
            {
                var token = JwtTokenIssuer.IssueJwtToken(user.Id, jwtOptions.TokenKey, jwtOptions.TokenTimeout);
                var refreshToken = CryptographyUtility.GenerateRefreshToken();
                authContext.UserRefreshTokens.Add(new UserRefreshToken(user.Id, refreshToken, jwtOptions.TokenTimeout));
                authContext.SaveChanges();
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
    public IActionResult Register(string phoneNumber, string fName, string lName, string password)
    {
        //we search the database for the given phonenumber
        var user = authContext.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
        //if any of them was found return "username/email already exists" exception 
        if (user != null)
        {
            throw new Exception();
        }

        //else
        //hash the password (here we can also check if the password is strong enough before hashing)
        var hashedPw = CryptographyUtility.HashPassword(password);
        //create a new record in DB for the new user (remember to make IsEmailConfirmed column false)
        User newUser = new(fName, lName, phoneNumber, hashedPw);


        authContext.Add(newUser);
        //generate a JWT token string and return it
        var token = JwtTokenIssuer.IssueJwtToken(newUser.Id, jwtOptions.TokenKey, jwtOptions.TokenTimeout);
        var refreshToken = CryptographyUtility.GenerateRefreshToken();
        authContext.UserRefreshTokens.Add(new UserRefreshToken(user.Id, refreshToken, jwtOptions.TokenTimeout));
        authContext.SaveChanges();

        return Ok(new Dictionary<string, string>
        {
            {"token",token },
            {"refreshToken",refreshToken },
            {"timeout",jwtOptions.TokenTimeout.ToString()}
        });
    }


    [HttpPost("refresh")]
    public IActionResult RenewToken(Guid userId, string refreshToken)
    {
        var user = authContext.UserRefreshTokens.FirstOrDefault(x => x.RefreshToken == refreshToken && x.UserId == userId && x.IsValid);
        if (user == null)
        {
            throw new Exception();
        }
        else
        {
            var token = JwtTokenIssuer.IssueJwtToken(userId, jwtOptions.TokenKey, jwtOptions.TokenTimeout);
            var newRefreshToken = CryptographyUtility.GenerateRefreshToken();
            authContext.UserRefreshTokens.Add(new UserRefreshToken(userId, newRefreshToken, jwtOptions.TokenTimeout));
            authContext.SaveChanges();
            return Ok(new Dictionary<string, string>
        {
            {"token",token },
            {"refreshToken",refreshToken },
            {"timeout",jwtOptions.TokenTimeout.ToString()}
        });
        }

    }
}
