
using Common.Domain.Base;
using Common.Domain.Language.Catalog.ValueObjects;

namespace Common.Domain.Language.Catalog.Events
{
    public record ProductItemFeaturesChanged : EventBase
    {
        public ProductItemId Id { get; init; }
        public Dictionary<string,string> ItemFeatures { get; init; }

    }
}
