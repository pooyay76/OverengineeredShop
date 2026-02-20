using Common.Application;
using Common.Application.DTOs;
using Common.Domain.Language.Feedback.ValueObjects;
using Common.Domain.Language.Global.ValueObjects;
using Feedback.Api.Commands;
using Feedback.Api.Data;
using Feedback.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly Mediator mediator;
        public CommentController( Mediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("/add")]
        public async Task<IActionResult> AddAsync(AddCommentCommand command)
        {
            var resp = await mediator.SendAsync<ApplicationResponse<CommentId>>(command);
            return Ok(resp);
        }

    }
}
