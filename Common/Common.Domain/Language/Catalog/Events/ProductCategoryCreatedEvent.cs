using Common.Domain.Base;
using Common.Domain.Language.Catalog.ValueObjects;

namespace Common.Domain.Language.Catalog.Events
{
    public record ProductCategoryCreatedEvent: EventBase
    {
        public ProductCategoryId Id{ get; init; }
        public string Title { get; init; }

        public ProductCategoryCreatedEvent(ProductCategoryId id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
