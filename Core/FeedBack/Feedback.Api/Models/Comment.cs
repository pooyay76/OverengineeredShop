using Common.Domain.Base;
using Common.Domain.Language.Catalog.ValueObjects;
using Common.Domain.Language.Feedback.Events;
using Common.Domain.Language.Feedback.ValueObjects;
using Common.Domain.Language.Global.ValueObjects;

namespace Feedback.Api.Models
{
    public class Comment : AggregateRootBase<CommentId>
    {
        public UserId AuthorId { get; private set; }
        public string BodyText { get; private set; }
        public string AuthorName { get; private set; } 
        public string Email { get; private set; }
        public ProductId ProductId { get; private set; }
        public bool IsRemoved { get; private set; } = false;

        private Comment()
        {
            
        }
        public Comment(UserId authorId, ProductId productId, string authorName,string bodyText, string email)
        {
            AddEvent(new CommentCreatedEvent(Id,authorId, authorName,bodyText, email, productId));
        }
        public void Apply(CommentCreatedEvent @event)
        {
            AuthorName = @event.AuthorName;
            AuthorId = @event.AuthorId;
            BodyText = @event.BodyText;
            Email = @event.Email;
            ProductId = @event.ProductId;
            Id= @event.CommentId;
        }

        public void Remove()
        {
            AddEvent(new CommentRemovedEvent(Id));
        }
        public void Apply(CommentRemovedEvent @event)
        {
            IsRemoved = true;
        }
    }
}
