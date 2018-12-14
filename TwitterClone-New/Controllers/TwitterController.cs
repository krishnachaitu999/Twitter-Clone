using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TwitterClone.BAL;
using TwitterClone.DAL;

namespace TwitterClone_New.Controllers
{
    public class TwitterController : Controller
    {
        private TwitterCloneDataContext db = new TwitterCloneDataContext();
        TwitterBAL twitterBAL = new TwitterBAL();
        // GET: 
        [Authorize]
        [MyExceptionHandler]
        public ActionResult Home()
        {
            Response.AddHeader("Refresh", "50");
            return View(twitterBAL.GetTweetInfo(System.Web.HttpContext.Current.User.Identity.Name));
        }
        [HttpPost]
       
        public ActionResult Search(string Prefix)
        {
            List<User> userList= twitterBAL.Search(Prefix);
            return Json(userList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [MyExceptionHandler]
        public ActionResult Follow(string Folllower_ID)
        {
            bool alreadyFollowing=twitterBAL.IsAlreadyFollowing(Folllower_ID, System.Web.HttpContext.Current.User.Identity.Name);
            if(alreadyFollowing)
            {
                TempData["Message"] = "You are already following the User:" + Folllower_ID;
                return RedirectToAction("Home");
            }
           twitterBAL.AddFollower(Folllower_ID, System.Web.HttpContext.Current.User.Identity.Name);
           return RedirectToAction("Home");
        }



        // GET: Twitter/Details/5
        [MyExceptionHandler]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSON pERSON = db.PERSON.Find(id);
            if (pERSON == null)
            {
                return HttpNotFound();
            }
            return View(pERSON);
        }

        // GET: Twitter/Create
        [Authorize]
        [MyExceptionHandler]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Twitter/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [MyExceptionHandler]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TWEET tweet)
        {
            if (ModelState.IsValid)
            {
                tweet.User_ID = System.Web.HttpContext.Current.User.Identity.Name;
                tweet.Created = DateTime.Now;
                twitterBAL.AddTweet(tweet);
            }

            return RedirectToAction("Home");
        }

        // GET: Twitter/Edit/5
        [MyExceptionHandler]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(twitterBAL.GetTweetDetails(Convert.ToInt32(id)));
        }

        // POST: Twitter/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyExceptionHandler]
        public ActionResult Edit(TWEET Tweet)
        {
            if (ModelState.IsValid)
            {
                twitterBAL.UpdateTweet(Tweet);
            }
            return RedirectToAction("Home");

        }

        // GET: Twitter/Delete/5
        [MyExceptionHandler]
        public ActionResult Delete(string id)
        {
            return View(twitterBAL.GetTweetDetails(Convert.ToInt32(id)));
        }

        // POST: Twitter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [MyExceptionHandler]
        public ActionResult DeleteConfirmed(string id)
        {
            twitterBAL.DeleteTweet(Convert.ToInt32(id));
            return RedirectToAction("Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
    public class MyExceptionHandler : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }
            Exception e = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error"
            };
        }
    }
}
