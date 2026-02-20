using Common.Application.Base;
using Common.Application.DTOs;
using Common.Domain.Language.Feedback.ValueObjects;
using Common.Domain.Language.Global.ValueObjects;
using Feedback.Api.Data;
using Feedback.Api.Models;

namespace Feedback.Api.Commands
{
    public class AddCommentCommandHandler : CommandHandlerBase<AddCommentCommand, ApplicationResponse<CommentId>>
    {
        private readonly FeedbackContext _feedbackContext;

        public AddCommentCommandHandler(FeedbackContext feedbackContext)
        {
            _feedbackContext = feedbackContext;
        }

        public override async Task<ApplicationResponse<CommentId>> HandleAsync(AddCommentCommand command)
        {
            var resp = new ApplicationResponse<CommentId>();

            //TODO: pass in author id correctly
            var comment = new Comment(new UserId(Guid.Empty), command.ProductId,
                command.AuthorName, command.BodyText, command.Email);

            await _feedbackContext.AddAsync(comment);
            await _feedbackContext.SaveChangesAsync();
            return resp.Succeeded(comment.Id);
        }
    }
}
