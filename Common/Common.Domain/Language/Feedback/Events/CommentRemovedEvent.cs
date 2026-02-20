using Common.Domain.Base;
using Common.Domain.Language.Feedback.ValueObjects;

namespace Common.Domain.Language.Feedback.Events
{
    public record CommentRemovedEvent :EventBase
    {
        public CommentId Id { get; init; }

        public CommentRemovedEvent(CommentId id)
        {
            Id = id;
        }
    }
}
