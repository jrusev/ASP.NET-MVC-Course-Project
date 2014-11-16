namespace AdList.Web.Controllers
{
    using AdList.Data.Common.Repository;
    using AdList.Data.Models;
    using AdList.Web.InputModels.Ads;
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using AdList.Web.ViewModels.Ads;
    using AutoMapper.QueryableExtensions;
    using AdList.Web.Infrastructure;
    using Microsoft.AspNet.Identity;
    using AdList.Web.ViewModels.Home;
    using PagedList;

    public class AdsController : AdsPagingControllerBase
    {
        private readonly IDeletableEntityRepository<Category> categories;

        private readonly ISanitizer sanitizer;

        public AdsController(
            IDeletableEntityRepository<Ad> ads,
            IDeletableEntityRepository<Category> categories,
            ISanitizer sanitizer)
            :base(ads)
        {
            this.categories = categories;
            this.sanitizer = sanitizer;
        }

        public ActionResult All(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var model = new HomeViewModel();
            model.Ads = this.GetAds(sortOrder, currentFilter, searchString, page);
            model.Categories = this.categories.All().OrderBy(x => x.Name);
            return this.View(model);
        }

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var allAds = this.ads.All().Project().To<AdDetailViewModel>();

            if (!String.IsNullOrEmpty(searchString))
            {
                allAds = allAds.Where(ad => ad.Title.Contains(searchString)
                                       || ad.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    allAds = allAds.OrderByDescending(ad => ad.Title);
                    break;
                case "Date":
                    allAds = allAds.OrderBy(ad => ad.CreatedOn);
                    break;
                case "date_desc":
                    allAds = allAds.OrderByDescending(ad => ad.CreatedOn);
                    break;
                default:  // by date 
                    allAds = allAds.OrderBy(ad => ad.CreatedOn);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(allAds.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Stats()
        {
            var stats = from ad in this.ads.All()
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
            var adViewModel = this.ads.All().Where(x => x.Id == id)
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