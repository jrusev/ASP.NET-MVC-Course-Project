namespace AdList.Web.Controllers
{
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using AdList.Data.Common.Repository;
    using AdList.Data.Models;
    using AdList.Web.ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IDeletableEntityRepository<Ad> posts;

        public HomeController(IDeletableEntityRepository<Ad> posts)
        {
            this.posts = posts;
        }

        public ActionResult Index()
        {
            var model = this.posts.All().Project().To<HomeAdViewModel>();

            return this.View(model);
        }
    }
}