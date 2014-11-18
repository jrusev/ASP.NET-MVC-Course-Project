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
    using System.Net;

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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ad =
                this.Data.Ads
                .All()
                .Project().To<AdInputModel>()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (ad == null)
            {
                return this.HttpNotFound();
            }

            if (ad.AuthorId != this.CurrentUser.Id && !this.User.IsInRole(AdList.Data.Models.User.AdminRole))
            {
                return this.RedirectToAction("Index");
            }

            ViewBag.CategoryOptions = this.Data.Categories.All().OrderBy(x => x.Name);

            return this.View(ad);
        }

        // POST: Admin/Edit/5
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }

            var adFromDb = this.Data.Ads.Find(input.Id);

            if (adFromDb.AuthorId != this.CurrentUser.Id && !User.IsInRole(AdList.Data.Models.User.AdminRole))
            {
                return this.RedirectToAction("Index");
            }
                    
            adFromDb.Title = input.Title;
            adFromDb.Price = input.Price;
            adFromDb.Description = input.Description;
            adFromDb.CategoryId = input.CategoryId;
            adFromDb.ImageUrl = input.ImageUrl;

            this.Data.Ads.Update(adFromDb);
            this.Data.SaveChanges();

            return this.RedirectToAction("Index");
        }

        // GET: Admin/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index");
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ad ad = this.Data.Ads.Find(id);

            if (ad.AuthorId != this.CurrentUser.Id && !User.IsInRole(AdList.Data.Models.User.AdminRole))
            {
                return this.RedirectToAction("Index");
            }

            this.Data.Ads.Delete(ad);
            this.Data.SaveChanges();

            return this.RedirectToAction("Index");
        }
    }
}