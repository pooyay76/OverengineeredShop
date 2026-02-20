using Common.Domain.Base;

namespace Common.Domain.Language.Catalog.ValueObjects
{
    public record ProductCategoryId : StronglyTypedId
    {
        public ProductCategoryId() : base()
        {
        }

        public ProductCategoryId(Guid value) : base(value)
        {
        }
    }
}
