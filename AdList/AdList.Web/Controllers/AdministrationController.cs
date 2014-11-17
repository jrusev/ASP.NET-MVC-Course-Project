namespace AdList.Web.Controllers
{
    using AdList.Data.Models;
    using AdList.Web.InputModels.Ads;
    using System.Linq;
    using System.Web.Mvc;
    using AdList.Web.ViewModels.Ads;
    using AutoMapper.QueryableExtensions;
    using AdList.Web.Infrastructure;
    using AdList.Data.UnitOfWork;

    public class AdminController : AdsPagingControllerBase
    {
        private readonly ISanitizer sanitizer;

        public AdminController(IDataProvider provider, ISanitizer sanitizer)
            :base(provider)
        {
            this.sanitizer = sanitizer;
        }

        [Authorize(Roles = "Administrator")]
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var allAds = this.Data.Ads.All().Project().To<AdDetailViewModel>();
            var model = this.GetAds(allAds, sortOrder, currentFilter, searchString, page, pageSize: 6);
            return View(model);
        }

        // GET: Admin/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            var ad = this.Data.Ads.All().FirstOrDefault(x => x.Id == id);
            if (ad == null)
            {
                return this.HttpNotFound("No ad with such id!");
            }

            var model = new AdInputModel()
            {
                Title = ad.Title,
                Description = ad.Description,
                Price = ad.Price,
                ImageUrl = ad.ImageUrl,
                CategoryId = ad.CategoryId
            };
            model.CategoryOptions = this.Data.Categories.All().OrderBy(x => x.Name);

            return this.View(model);
        }

        // POST: Admin/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index");
        }

        // POST: Admin/Delete/5
        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}