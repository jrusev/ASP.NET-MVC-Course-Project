﻿namespace AdList.Web.Controllers
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

        // /ads/details/7
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

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            var model = new AdInputModel();
            model.CategoryOptions = this.Data.Categories.All().OrderBy(x => x.Name);
            return this.View(model);
        }

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
    }
}