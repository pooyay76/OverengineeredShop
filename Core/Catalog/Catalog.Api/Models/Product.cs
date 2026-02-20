using Catalog.Api.Exceptions;
using Common.Domain.Base;
using Common.Domain.Language.Catalog.Events;
using Common.Domain.Language.Catalog.ValueObjects;

namespace Catalog.Api.Models
{
    public class Product : AggregateRootBase<ProductId>
    {
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;


        public string Name { get; private set; }
        public string Description { get; private set; }

        public bool IsInStock { get; private set; } = false;
        public DateTime PublishedAt { get; private set; }
        public bool IsPublished { get; private set; } = false;
        public string PictureMediaAddress { get; private set; }
        public ProductCategoryId CategoryId { get; private set; }
        public int ProductItemsCount { get;private set; }


        //Navigation properties
        public ProductCategory Category { get; set; }
        public List<ProductItem> ProductItems{ get; set; }

        private Product()
        {
        }



        public Product(string name, string desc, string pictureMediaAddress) : base()
        {
            ValidateName(name);
            ValidateDescription(desc);
            AddEvent(new ProductCreatedEvent(Id, name, desc, pictureMediaAddress));
        }
        public void Apply(ProductCreatedEvent productCreatedEvent)
        {

            Id = productCreatedEvent.ProductId;
            Name = productCreatedEvent.Name;
            Description = productCreatedEvent.Description;
            ProductItemsCount = 0;
            PictureMediaAddress = productCreatedEvent.PictureMediaAddress;
        }




        public void Edit(string name, string desc, string picMediaAddress)
        {
            if(string.IsNullOrWhiteSpace( picMediaAddress ) == false && 
                PictureMediaAddress.ToLower() != picMediaAddress.ToLower())
            {
                ValidatePictureMediaAddress(picMediaAddress);
                AddEvent(new ProductNameChangedEvent(Id, name));

            }
            if(string.IsNullOrWhiteSpace(name) != false && name.ToLower() != Name.ToLower())
            {
                ValidateName(name);
                AddEvent(new ProductNameChangedEvent(Id,name));
            }
            if (string.IsNullOrWhiteSpace(name) != false && Description.ToLower() != desc.ToLower())
            {
                ValidateDescription(desc);
                AddEvent(new ProductDescriptionChangedEvent(Id, desc));
            }
        }

        public void Apply(ProductDescriptionChangedEvent @event)
        {
            Description = @event.Description;
        }
        public void Apply(ProductNameChangedEvent @event)
        {
            Name = @event.Name;
        }
        public void Apply(ProductPictureChangedEvent @event)
        {
            PictureMediaAddress = @event.NewPictureMediaAddress;
        }
        





        public void Publish()
        {
            
            if (ProductItems.Count == 0)
            {
                throw new CannotPublishProductWithoutItemsException();
            }
            if(IsPublished == false)
            {
                AddEvent(new ProductPublishedEvent(Id));
            }

        }
        public void Apply(ProductPublishedEvent @event)
        {
            PublishedAt = DateTime.UtcNow;
            IsPublished = true;
        }





        public void AssignToProductCategory(ProductCategoryId catId)
        {
            AddEvent(new ProductAssignedToCategoryEvent(Id, catId));

        }
        public void Apply(ProductAssignedToCategoryEvent @event)
        {
            CategoryId = @event.ProducCategorytId;
        }






        //Called in event handler
        public void Apply(ProductItemCreatedEvent @event)
        {
            ProductItemsCount++;
        }

        //Called in event handler
        public void Apply(ProductItemRemovedEvent @event)
        {
            if (ProductItemsCount > 0)
                ProductItemsCount--;
        }






        private void ValidatePictureMediaAddress(string pictureMediaAddress)
        {
            //Will write this later
        }

        private void ValidateName(string name)
        {
            if (name.Length < 2)
            {
                AddError(ProductNameTooShortException.DefaultMessage);
            }
        }

        private void ValidateDescription(string desc)
        {
            if (desc.Length < 10 )
            {
                AddError(ProductDescriptionTooShortException.DefaultMessage);
            }

        }

    }
}
