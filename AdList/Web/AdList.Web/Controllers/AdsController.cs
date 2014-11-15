using AdList.Data.Common.Repository;
using AdList.Data.Models;
using AdList.Web.InputModels.Ads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdList.Web.ViewModels.Ads;
using AutoMapper.QueryableExtensions;
using AdList.Web.Infrastructure;
using Microsoft.AspNet.Identity;
using AdList.Web.ViewModels.Home;

namespace AdList.Web.Controllers
{
    public class AdsController : Controller
    {
        private readonly IDeletableEntityRepository<Ad> ads;
        private readonly IDeletableEntityRepository<Category> categories;

        private readonly ISanitizer sanitizer;

        public AdsController(
            IDeletableEntityRepository<Ad> ads,
            IDeletableEntityRepository<Category> categories,
            ISanitizer sanitizer)
        {
            this.ads = ads;
            this.categories = categories;
            this.sanitizer = sanitizer;
        }

        public ActionResult All()
        {
            var model = this.ads.All().Project().To<HomeAdViewModel>();
            return this.View(model);
        }

        // /ads/details/7
        public ActionResult Details(int id, int page = 1)
        {
            var adViewModel = this.ads.All().Where(x => x.Id == id)
                .Project().To<AdDetailViewModel>().FirstOrDefault();

            if (adViewModel == null)
            {
                return this.HttpNotFound("No ad with such id!");
            }

            return View(adViewModel);
        }

        // /ads/tagged/phones
        public ActionResult GetByTag(string tag)
        {
            return Content(tag);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            var model = new AdInputModel();
            model.CategoryOptions = this.categories.All().OrderBy(x => x.Name);
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

                this.ads.Add(ad);
                this.ads.SaveChanges();
                return this.RedirectToAction("Details", new { id = ad.Id, url = "new" });
            }

            return this.View(input);
        }
    }
}