namespace MarkDonile.Blog.Admin.ViewModels
{
    public class PagingInfo
    {
        public int ItemsTotal { get; set; }
        public int ItemsPerPage { get; set; }
        public int PageNumber { get; set; }

        public int PageMaxNumber
        {
            get => (ItemsTotal / ItemsPerPage) + 1;
        }
    }
}