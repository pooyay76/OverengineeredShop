using Common.Domain.Base;
using System.ComponentModel;

namespace Common.Domain.Language.Catalog.ValueObjects
{

    public record ProductItemId : StronglyTypedId
    {
        public ProductItemId() : base()
        {
        }
        public ProductItemId(string id) : base(Guid.Parse(id))
        {

        }
        public ProductItemId(Guid id) : base(id)
        {
        }
    }

}
