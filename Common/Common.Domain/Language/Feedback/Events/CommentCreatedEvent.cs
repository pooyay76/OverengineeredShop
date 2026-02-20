using Common.Domain.Base;
using Common.Domain.Language.Catalog.ValueObjects;
using Common.Domain.Language.Feedback.ValueObjects;
using Common.Domain.Language.Global.ValueObjects;

namespace Common.Domain.Language.Feedback.Events
{
    public record CommentCreatedEvent:EventBase
    {
        public CommentId CommentId { get; init; }
        public UserId AuthorId { get; init; }
        public string BodyText { get; init; }
        public string Email { get; init; }
        public ProductId ProductId { get; init; }
        public string AuthorName { get; init; }

        public CommentCreatedEvent(CommentId commentId, UserId authorId, string authorName, string bodyText, string email, ProductId productId)
        {
            CommentId = commentId;
            AuthorId = authorId;
            BodyText = bodyText;
            Email = email;
            ProductId = productId;
            AuthorName = authorName;
        }
    }
}
