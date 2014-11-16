﻿namespace AdList.Web.Controllers
{
    using AdList.Data.Common.Repository;
    using AdList.Data.Models;
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using AdList.Web.ViewModels.Ads;
    using AutoMapper.QueryableExtensions;
    using PagedList;

    public abstract class AdsPagingControllerBase : Controller
    {
        protected readonly IDeletableEntityRepository<Ad> ads;

        public AdsPagingControllerBase(IDeletableEntityRepository<Ad> ads)
        {
            this.ads = ads;
        }

        protected IPagedList<AdDetailViewModel> GetAds(string sortOrder, string currentFilter, string searchString, int? page)
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
            return allAds.ToPagedList(pageNumber, pageSize);
        }
    }
}