namespace AdList.Web.Controllers
{
    using AdList.Data.Models;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using AdList.Web.ViewModels.Category;
    using AdList.Web.ViewModels.Ads;
    using AdList.Data.UnitOfWork;

    public class CategoryController : AdsPagingControllerBase
    {
        public CategoryController(IDataProvider provider)
            :base(provider)
        {
        }

        // GET: Category/{name}
        public ActionResult Index(string name, string sortOrder, string currentFilter, string searchString, int? page)
        {
            var model = new CategoryViewModel();

            var allAds = this.Data.Ads.All().Where(a => a.Category.Name == name).Project().To<AdDetailViewModel>();
            model.Ads = this.GetAds(allAds, sortOrder, currentFilter, searchString, page, pageSize: 6);

            //model.Ads = this.Data.Ads.All().Where(a => a.Category.Name == name).Project().To<AdDetailViewModel>();
            model.CategoryName = name;
            model.Categories = this.Data.Categories.All().OrderBy(x => x.Name);
            return this.View(model);

        }
    }
}
