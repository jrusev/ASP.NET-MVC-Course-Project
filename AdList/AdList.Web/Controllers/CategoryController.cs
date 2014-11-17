namespace AdList.Web.Controllers
{
    using AdList.Data.Common.Repository;
    using AdList.Data.Models;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using AdList.Web.ViewModels.Category;
    using AdList.Web.ViewModels.Ads;

    public class CategoryController : AdsPagingControllerBase
    {
        protected readonly IDeletableEntityRepository<Ad> ads;
        private readonly IDeletableEntityRepository<Category> categories;

        public CategoryController(IDeletableEntityRepository<Ad> ads, IDeletableEntityRepository<Category> categories)
        {
            this.ads = ads;
            this.categories = categories;
        }

        // GET: Category/{name}
        public ActionResult Index(string name, string sortOrder, string currentFilter, string searchString, int? page)
        {
            var model = new CategoryViewModel();

            var allAds = this.ads.All().Where(a => a.Category.Name == name).Project().To<AdDetailViewModel>();
            model.Ads = this.GetAds(allAds, sortOrder, currentFilter, searchString, page, pageSize: 6);

            //model.Ads = this.ads.All().Where(a => a.Category.Name == name).Project().To<AdDetailViewModel>();
            model.CategoryName = name;
            model.Categories = this.categories.All().OrderBy(x => x.Name);
            return this.View(model);

        }
    }
}
