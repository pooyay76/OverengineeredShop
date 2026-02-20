using Common.Domain.Base;
using Common.Domain.Language.Catalog.ValueObjects;

namespace Common.Domain.Language.Catalog.Events
{
    public record ProductAssignedToCategoryEvent : EventBase
    {
        public ProductId ProductId { get; init; }
        public ProductCategoryId ProducCategorytId { get; init; }

        public ProductAssignedToCategoryEvent(ProductId productId, ProductCategoryId producCategorytId)
        {
            ProductId = productId;
            ProducCategorytId = producCategorytId;
        }
    }
}
