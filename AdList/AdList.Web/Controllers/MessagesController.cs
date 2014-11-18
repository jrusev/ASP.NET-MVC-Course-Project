namespace AdList.Web.Controllers
{
    using System.Web.Mvc;
    using System.Linq;

    using AdList.Data.Models;
    using AdList.Data.UnitOfWork;
    using AdList.Web.Infrastructure;

    using Microsoft.AspNet.Identity;
    using AdList.Web.ViewModels.Messages;

    public class MessagesController : BaseController
    {
        private readonly ISanitizer sanitizer;

        public MessagesController(IDataProvider provider, ISanitizer sanitizer)
            : base(provider)
        {
            this.sanitizer = sanitizer;
        }

        // GET: /Messages/Inbox
        public ActionResult Inbox()
        {
            var inbox = new InboxViewModel() 
            {
                User = this.CurrentUser,
                // TODO: return only this user's messages
                Messages = this.Data.Messages.All().Where(m => m.ToId == this.CurrentUser.Id).ToList(),
                MessagesByMe = this.Data.Messages.All().Where(m => m.FromId == this.CurrentUser.Id).ToList(),
            };

            return View(inbox);
        }

        // GET: Messages/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Messages/Send/{userId}
        public ActionResult Send(string userId)
        {
            var receiver = this.Data.Users.Find(userId);
            if (receiver == null)
            {
                return this.HttpNotFound();
            }

            ViewBag.ToUserName = receiver.UserName;
            var message = new Message() { ToId = userId };
            return View(message);
        }

        // POST: Messages/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Send(Message message)
        {
            if (ModelState.IsValid)
            {
                message.FromId = this.User.Identity.GetUserId();
                message.Content = sanitizer.Sanitize(message.Content);

                this.Data.Messages.Add(message);
                this.Data.Ads.SaveChanges();
                return this.RedirectToAction("Inbox");
            }

            return this.View(message);
        }

        // GET: Messages/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Messages/Edit/5
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

        // GET: Messages/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Messages/Delete/5
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
