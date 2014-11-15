using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AdList.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using AdList.Data.Common.Repository;
using AdList.Data;
using System.Data.Entity;
using AdList.Web.ViewModels.User;

namespace AdList.Web.Controllers
{
    public class UserController : Controller
    {
        public UserController(DbContext context)
            : base()
        {
            this.UserManager = new UserManager<User>(new UserStore<User>(context));
        }

        /// <summary>
        /// User manager - attached to application DB context
        /// </summary>
        protected UserManager<User> UserManager { get; set; }

        //public User currentUser { 
        //    get
        //    {
        //        return this.UserManager.FindById(User.Identity.GetUserId());
        //    }
        //}

        // GET: User
        public ActionResult Index()
        {
            var currentUser = UserManager.FindById(User.Identity.GetUserId());
            if (currentUser == null)
            {
                return this.HttpNotFound("User not found!");
            }

            var model = new UserProfileViewModel()
            {
                UserName = currentUser.UserName,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                City = currentUser.City,
                ImageUrl = currentUser.ImageUrl,
                PhoneNumber = currentUser.PhoneNumber,
                CreatedOn = currentUser.CreatedOn,
                Ads = currentUser.Ads
            };

            
            return View(model);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
