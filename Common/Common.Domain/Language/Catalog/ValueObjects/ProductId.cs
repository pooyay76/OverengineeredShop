using Common.Domain.Base;
using Common.Domain.Language.Global.ValueObjects;
using System.ComponentModel;

namespace Common.Domain.Language.Catalog.ValueObjects
{

    public record ProductId : StronglyTypedId
    {

        public ProductId(): base()
        {
        }

        public ProductId(Guid value) : base(value)
        {
        }
    }
}
