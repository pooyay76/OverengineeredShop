using Common.Application.Base;
using Common.Domain.Language.Feedback.ValueObjects;

namespace Feedback.Api.Commands
{
    public class RemoveCommentCommand : CommandBase
    {
        public CommentId CommentId { get; init; }

        public RemoveCommentCommand(CommentId commentId)
        {
            CommentId = commentId;
        }
    }
}
