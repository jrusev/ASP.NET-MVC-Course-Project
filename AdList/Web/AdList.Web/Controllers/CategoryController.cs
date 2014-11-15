using AdList.Data.Common.Repository;
using AdList.Data.Models;
using AdList.Web.ViewModels.Home;
using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using AdList.Web.ViewModels.Category;

namespace AdList.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IDeletableEntityRepository<Ad> ads;
        private readonly IDeletableEntityRepository<Category> categories;

        public CategoryController(IDeletableEntityRepository<Ad> ads, IDeletableEntityRepository<Category> categories)
        {
            this.ads = ads;
            this.categories = categories;
        }

        // GET: Category/{name}
        public ActionResult Index(string name)
        {
            var model = new CategoryViewModel();
            model.Ads = this.ads.All().Where(a => a.Category.Name == name).Project().To<HomeAdViewModel>();
            model.CategoryName = name;
            model.Categories = this.categories.All().OrderBy(x => x.Name);
            return this.View(model);

        }
    }
}
