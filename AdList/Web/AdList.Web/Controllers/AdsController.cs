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

namespace AdList.Web.Controllers
{
    public class AdsController : Controller
    {
        private readonly IDeletableEntityRepository<Ad> posts;

        private readonly ISanitizer sanitizer;

        public AdsController(IDeletableEntityRepository<Ad> posts,
            ISanitizer sanitizer)
        {
            this.posts = posts;
            this.sanitizer = sanitizer;
        }

        // /ads/7
        public ActionResult Details(int id, string url, int page = 1)
        {
            var postViewModel = this.posts.All().Where(x => x.Id == id)
                .Project().To<AdDetailViewModel>().FirstOrDefault();

            if (postViewModel == null)
            {
                return this.HttpNotFound("No ad with such id!");
            }

            return View(postViewModel);
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

                var post = new Ad
                    {
                        Title = input.Title,
                        Description = sanitizer.Sanitize(input.Description),
                        AuthorId = userId,
                        CategoryId = input.CategoryId
                    };

                this.posts.Add(post);
                this.posts.SaveChanges();
                return this.RedirectToAction("Details", new { id = post.Id, url = "new" });
            }

            return this.View(input);
        }
    }
}