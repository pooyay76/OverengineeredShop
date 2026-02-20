using Common.Domain.Base;

namespace Common.Domain.Language.Feedback.ValueObjects
{
    public record CommentId : StronglyTypedId
    {
        public CommentId():base()
        {
        }

        public CommentId(Guid value) : base(value)
        {
        }
    }
}
