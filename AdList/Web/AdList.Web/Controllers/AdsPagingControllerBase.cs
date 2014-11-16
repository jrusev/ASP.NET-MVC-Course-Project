namespace AdList.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using AdList.Web.ViewModels.Ads;
    using PagedList;

    public abstract class AdsPagingControllerBase : Controller
    {
        protected IPagedList<AdDetailViewModel> GetAds(
            IQueryable<AdDetailViewModel> allAds,
            string sortOrder,
            string currentFilter,
            string searchString,
            int? page,
            int pageSize)
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

            int pageNumber = (page ?? 1);
            return allAds.ToPagedList(pageNumber, pageSize);
        }
    }
}