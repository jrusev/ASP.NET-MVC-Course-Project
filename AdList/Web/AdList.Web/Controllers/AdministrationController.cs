namespace AdList.Web.Controllers
{
    using AdList.Data.Common.Repository;
    using AdList.Data.Models;
    using AdList.Web.InputModels.Ads;
    using System.Linq;
    using System.Web.Mvc;
    using AdList.Web.ViewModels.Ads;
    using AutoMapper.QueryableExtensions;
    using AdList.Web.Infrastructure;

    public class AdminController : AdsPagingControllerBase
    {
        protected readonly IDeletableEntityRepository<Ad> ads;
        private readonly IDeletableEntityRepository<Category> categories;
        private readonly ISanitizer sanitizer;

        public AdminController(
            IDeletableEntityRepository<Ad> ads,
            IDeletableEntityRepository<Category> categories,
            ISanitizer sanitizer)
        {
            this.ads = ads;
            this.categories = categories;
            this.sanitizer = sanitizer;
        }

        [Authorize(Roles = "Administrator")]
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var allAds = this.ads.All().Project().To<AdDetailViewModel>();
            var model = this.GetAds(allAds, sortOrder, currentFilter, searchString, page, pageSize: 3);
            return View(model);
        }

        // GET: Admin/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            var ad = this.ads.All().FirstOrDefault(x => x.Id == id);
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
            model.CategoryOptions = this.categories.All().OrderBy(x => x.Name);

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