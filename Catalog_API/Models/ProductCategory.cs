using Catalog_API.Exceptions;

namespace Catalog_API.Models
{
    public class ProductCategory
    {


        public long Id { get; private set; }
        public string Title { get; private set; }

        //Navigation property
        public List<Product> Products { get; set; }

        public ProductCategory(string title)
        {
            SetTitle(title);
        }
        public void Edit(string title)
        {
            SetTitle(title);
        }
        public void SetTitle(string title = "")
        {
            if (title.Length <= 1)
            {
                throw new ProductCategoryTitleTooShortException();
            }
            Title = title;
        }
    }
}
