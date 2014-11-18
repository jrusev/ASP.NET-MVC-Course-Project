using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AdList.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using AdList.Data;
using System.Data.Entity;
using AdList.Web.ViewModels.User;
using AutoMapper.QueryableExtensions;
using AdList.Web.ViewModels.Ads;
using AdList.Data.UnitOfWork;

namespace AdList.Web.Controllers
{
    public class UserController : AdsPagingControllerBase
    {
        public UserController(DbContext context, IDataProvider provider)
            :base(provider)
        {
            this.UserManager = new UserManager<User>(new UserStore<User>(context));
        }

        /// <summary>
        /// User manager - attached to application DB context
        /// </summary>
        protected UserManager<User> UserManager { get; set; }

        // GET: User/Ads
        [Authorize]
        public ActionResult MyAds(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var currentUser = UserManager.FindById(User.Identity.GetUserId());
            if (currentUser == null)
            {
                return this.HttpNotFound("User not found!");
            }

            var myAds = currentUser.Ads.AsQueryable().Project().To<AdDetailViewModel>();
            var model = new UserProfileViewModel()
            {
                UserName = currentUser.UserName,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                City = currentUser.City,
                ImageUrl = currentUser.ImageUrl,
                PhoneNumber = currentUser.PhoneNumber,
                CreatedOn = currentUser.CreatedOn,
                Ads = this.GetAds(myAds, sortOrder, currentFilter, searchString, page, pageSize: 12)
            };

            
            return View(model);
        }

        // GET: User/GetById/{id}
        public ActionResult GetById(string id, string sortOrder, string currentFilter, string searchString, int? page)
        {
            var user = UserManager.FindById(id);
            if (user == null)
            {
                return this.HttpNotFound("User not found!");
            }

            var userAds = user.Ads.AsQueryable().Project().To<AdDetailViewModel>();
            var model = new UserProfileViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                City = user.City,
                ImageUrl = user.ImageUrl,
                PhoneNumber = user.PhoneNumber,
                CreatedOn = user.CreatedOn,
                Ads = this.GetAds(userAds, sortOrder, currentFilter, searchString, page, pageSize: 12)
            };

            ViewBag.UserId = id;
            return View(model);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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
