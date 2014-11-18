namespace AdList.Web.Controllers
{
    using AdList.Data.Models;
    using AdList.Web.InputModels.Ads;
    using System.Linq;
    using System.Web.Mvc;
    using AdList.Web.ViewModels.Ads;
    using AutoMapper.QueryableExtensions;
    using AdList.Web.Infrastructure;
    using Microsoft.AspNet.Identity;
    using AdList.Web.ViewModels.Home;
    using AdList.Data.UnitOfWork;
    using System.Net;

    public class AdsController : AdsPagingControllerBase
    {
        private readonly ISanitizer sanitizer;

        public AdsController(IDataProvider provider, ISanitizer sanitizer)
            :base(provider)
        {
            this.sanitizer = sanitizer;
        }

        public ActionResult All(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var model = new HomeViewModel();

            var allAds = this.Data.Ads.All().Project().To<AdDetailViewModel>();
            model.Ads = this.GetAds(allAds, sortOrder, currentFilter, searchString, page, pageSize:6);
            model.Categories = this.Data.Categories.All().OrderBy(x => x.Name);
            return this.View(model);
        }

        public ActionResult Stats()
        {
            var stats = from ad in this.Data.Ads.All()
                        group ad by ad.CreatedOn into dateGroup
                        select new AdsStatsView()
                        {
                            CreatedOn = dateGroup.Key,
                            AdCount = dateGroup.Count()
                        };
            return View(stats.ToList());
        }

        // GET: /Ads/Details/7
        public ActionResult Details(int id, int page = 1)
        {
            var adViewModel = this.Data.Ads.All().Where(x => x.Id == id)
                .Project().To<AdDetailViewModel>().FirstOrDefault();

            if (adViewModel == null)
            {
                return this.HttpNotFound("No ad with such id!");
            }

            return View(adViewModel);
        }

        // GET: /Ads/Create
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            var model = new AdInputModel();
            model.CategoryOptions = this.Data.Categories.All().OrderBy(x => x.Name);
            return this.View(model);
        }

        // POST: /Ads/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdInputModel input)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();

                var ad = new Ad
                    {
                        Title = input.Title,
                        Description = sanitizer.Sanitize(input.Description),
                        AuthorId = userId,
                        CategoryId = input.CategoryId,
                        Price = input.Price,
                        ImageUrl = input.ImageUrl
                    };

                this.Data.Ads.Add(ad);
                this.Data.Ads.SaveChanges();
                return this.RedirectToAction("Details", new { id = ad.Id, url = "new" });
            }

            return this.View(input);
        }

        // GET: /Ads/Edit/5
        [Authorize]
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

        // POST: /Ads/Edit/5
        [Authorize]
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

            return this.RedirectToAction("MyAds","User");
        }

        // POST: /Ads/Delete/5
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