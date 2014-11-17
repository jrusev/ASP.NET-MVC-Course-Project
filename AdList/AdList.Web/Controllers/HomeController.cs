namespace AdList.Web.Controllers
{
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using AdList.Data.Common.Repository;
    using AdList.Data.Models;
    using AdList.Web.ViewModels.Ads;
    using PagedList;
    using System.Linq;

    public class HomeController : AdsPagingControllerBase
    {
        protected readonly IDeletableEntityRepository<Ad> ads;

        public HomeController(IDeletableEntityRepository<Ad> ads)
        {
            this.ads = ads;
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var allAds = this.ads.All().Where(a => a.Featured == true).Project().To<AdDetailViewModel>();
            var model = this.GetAds(allAds, sortOrder, currentFilter, searchString, page, pageSize: 6);
            return this.View(model);
        }
    }
}