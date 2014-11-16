namespace AdList.Web.ViewModels.Home
{
    using AdList.Data.Models;
    using AdList.Web.ViewModels.Ads;
    using PagedList;
    using System.Linq;

    public class HomeViewModel
    {
        public IPagedList<AdDetailViewModel> Ads { get; set; }

        public IQueryable<Category> Categories { get; set; }
    }
}