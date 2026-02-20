using Common.Domain.Base;
using Common.Domain.Language.Catalog.ValueObjects;

namespace Common.Domain.Language.Catalog.Events
{
    public record ProductCategoryTitleChangedEvent : EventBase
    {
        public ProductCategoryId Id { get; init; }
        public string NewTitle { get; init; }

        public ProductCategoryTitleChangedEvent(ProductCategoryId id, string newTitle)
        {
            Id = id;
            NewTitle = newTitle;
        }
    }
}
