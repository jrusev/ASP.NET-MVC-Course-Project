namespace AdList.Web.Controllers
{
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using AdList.Data.Models;
    using AdList.Data.UnitOfWork;

    public abstract class BaseController : Controller
    {
        private User currentUser;

        public BaseController(IDataProvider provider)
        {
            this.Data = provider;
        }

        public IDataProvider Data { get; set; }

        public User CurrentUser
        {
            get
            {
                if (this.currentUser == null)
                {
                    var currentUserId = this.User.Identity.GetUserId();
                    this.currentUser = this.Data.Users.Find(currentUserId);
                }

                return this.currentUser;
            }
        }
    }
}