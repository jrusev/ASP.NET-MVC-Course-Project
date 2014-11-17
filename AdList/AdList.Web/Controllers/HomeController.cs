namespace AdList.Web.Controllers
{
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using AdList.Data.Models;
    using AdList.Web.ViewModels.Ads;
    using PagedList;
    using System.Linq;
    using AdList.Data.UnitOfWork;

    public class HomeController : AdsPagingControllerBase
    {
        public HomeController(IDataProvider provider)
            :base(provider)
        {
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var allAds = this.Data.Ads.All().Where(a => a.Featured == true).Project().To<AdDetailViewModel>();
            var model = this.GetAds(allAds, sortOrder, currentFilter, searchString, page, pageSize: 6);
            return this.View(model);
        }
    }
}