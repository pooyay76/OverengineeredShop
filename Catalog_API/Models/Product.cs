using Catalog.Api.Exceptions;

namespace Catalog.Api.Models
{
    public class Product
    {
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;


        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public bool IsInStock { get; private set; } = false;
        public DateTime PublishedAt { get; private set; }
        public bool IsPublished { get; private set; } = false;
        public string PictureMediaAddress { get; private set; }
        public long? CategoryId { get; private set; }



        //Navigation property
        public ProductCategory Category { get; set; }

        public List<ProductItem> Items{ get; set; }
        private Product()
        {

        }

        public Product(string name, string desc, string pictureMediaAddress)
        {
            SetName(name);
            SetDescription(desc);
            PictureMediaAddress = pictureMediaAddress;
        }

        public void Edit(string name, string desc)
        {
            SetName(name);
            SetDescription(desc);
        }
        public void Publish()
        {
            if (Items.Count == 0)
            {
                throw new CannotPublishProductWithoutItemsException();
            }
            PublishedAt = DateTime.UtcNow;
            IsPublished = true;
        }

        public void AssignToProductCategory(long id)
        {
            CategoryId = id;
        }


        private void SetName(string name = "")
        {
            if (name.Length < 2)
            {
                throw new ProductNameTooShortException();
            }
            Name = name;
        }

        private void SetDescription(string desc = "")
        {
            if (desc.Length < 10)
            {
                throw new ProductDescriptionTooShortException();
            }
            Description = desc;
        }

    }
}
