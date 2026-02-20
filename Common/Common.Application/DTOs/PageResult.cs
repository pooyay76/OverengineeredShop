namespace Common.Application.DTOs
{
    public class PageResult<T>
    {

        public ICollection<T> DataCollection { get; set; }
        public int TotalItemsCount { get; set; }

        private int _pageSize = 25;
        public int PageSize  { get { return _pageSize; } }

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

        public PageResult(ICollection<T> dataCollection, int totalItemsCount, int currentPage, int pageSize = 25)
        {
            DataCollection = dataCollection;
            TotalItemsCount = totalItemsCount;
            _pageSize = pageSize;

            if(pageSize != 0 && pageSize <= 50 )
                _pageSize = pageSize;

            CurrentPage = currentPage;
        }
    }
}
