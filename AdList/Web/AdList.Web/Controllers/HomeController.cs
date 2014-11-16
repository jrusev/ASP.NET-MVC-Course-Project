namespace AdList.Web.Controllers
{
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using AdList.Data.Common.Repository;
    using AdList.Data.Models;
    using AdList.Web.ViewModels.Ads;
    using PagedList;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IDeletableEntityRepository<Ad> ads;

        public HomeController(IDeletableEntityRepository<Ad> ads)
        {
            this.ads = ads;
        }

        public ActionResult Index()
        {
            int pageSize = 12;
            int pageNumber = 1;
            var model = this.ads.All().Project().To<AdDetailViewModel>();
            model = model.OrderBy(ad => ad.CreatedOn);

            return this.View(model.ToPagedList(pageNumber, pageSize));
        }
    }
}