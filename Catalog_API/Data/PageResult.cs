namespace Catalog_API.Data
{
    public class PageResult<T>
    {
        public ICollection<T> DataCollection { get; set; }
        public int TotalItemsCount { get; set; }

        private int _pageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value > 50 ? value = 50 : value;
            }
        }

        public int CurrentPage { get; set; }
        public int TotalPagesCount
        {
            get
            {
                bool isDivisible = TotalItemsCount % PageSize == 0;
                int divisionResult = TotalItemsCount / PageSize;
                return isDivisible ? divisionResult : divisionResult + 1;
            }
        }

    }
}
