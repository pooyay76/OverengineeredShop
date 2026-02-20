using Common.Domain.Base;
using Common.Domain.Language.Catalog.ValueObjects;

namespace Common.Domain.Language.Catalog.Events
{
    public record ProductItemRemovedEvent : EventBase
    {
        public ProductItemId Id { get; init; }
        public ProductId ProductId { get; init; }
    }
}
