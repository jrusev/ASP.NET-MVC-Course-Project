namespace AdList.Web.Controllers
{
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using AdList.Data.Common.Repository;
    using AdList.Data.Models;
    using AdList.Web.ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IDeletableEntityRepository<Ad> ads;

        public HomeController(IDeletableEntityRepository<Ad> ads)
        {
            this.ads = ads;
        }

        public ActionResult Index()
        {
            var model = this.ads.All().Project().To<HomeAdViewModel>();

            return this.View(model);
        }
    }
}