using Common.Domain.Base;

namespace Common.Domain.Language.Global.ValueObjects
{
    public record UserId : StronglyTypedId
    {
        public UserId(Guid value) : base(value)
        {
        }
    }
}
