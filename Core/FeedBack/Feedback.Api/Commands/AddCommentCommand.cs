using Common.Application.Base;
using Common.Domain.Language.Catalog.ValueObjects;

namespace Feedback.Api.Commands
{
    public class AddCommentCommand:CommandBase
    {
        public ProductId ProductId{ get; init; }
        public string BodyText { get; init; }
        public string Email { get; init; } 
        public string AuthorName { get; init; }
    }
}
