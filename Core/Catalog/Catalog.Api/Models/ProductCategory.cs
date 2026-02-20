
using Catalog.Api.Exceptions;
using Common.Domain.Base;
using Common.Domain.Language.Catalog.Events;
using Common.Domain.Language.Catalog.ValueObjects;

namespace Catalog.Api.Models
{
    public class ProductCategory :AggregateRootBase<ProductCategoryId>
    {

        public string Title { get; private set; }

        //Navigation property
        public List<Product> Products { get; set; }

        public ProductCategory(string title)
        {
            if (title.Length <= 1)
            {
                throw new ProductCategoryTitleTooShortException();
            }
            AddEvent(new ProductCategoryCreatedEvent(Id, title));
        }
        public void Apply(ProductCategoryCreatedEvent @event)
        {
            Id = @event.Id;
            Title = @event.Title;
        }


        public void Edit(string title)
        {
            if (string.IsNullOrWhiteSpace(title) != false)
            {
                ValidateTitle(title);
                AddEvent(new ProductCategoryTitleChangedEvent(Id, title));
            }
        }
        public void Apply(ProductCategoryTitleChangedEvent @event)
        {
            Title = @event.NewTitle;
        }



        private void ValidateTitle(string title)
        {
            if (title.Length <= 1)
            {
                AddError(ProductCategoryTitleTooShortException.DefaultMessage);
            }
        }


    }
}
