using Common.Domain.Base;
using Common.Domain.Language.Catalog.ValueObjects;

namespace Common.Domain.Language.Catalog.Events
{
    public record ProductPictureChangedEvent : EventBase
    {
        public ProductId ProductId { get; init; }

        public string NewPictureMediaAddress { get; init; }

        public ProductPictureChangedEvent(ProductId productId, string pictureMediaAddress)
        {
            ProductId = productId;
            NewPictureMediaAddress = pictureMediaAddress;
        }
    }


}
