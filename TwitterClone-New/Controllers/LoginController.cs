using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TwitterClone.BAL;
using TwitterClone.DAL;

namespace TwitterClone_New.Controllers
{
    public class LoginController : Controller
    {
        UserBAL userBal = new UserBAL();
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Create()
        {
            
            return View();
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_ID,Password,FullName,Email")] PERSON pERSON)
        {
            if (ModelState.IsValid)
            {
                if(userBal.IsUserExist(pERSON.User_ID))
                {
                    TempData["Message"] = pERSON.User_ID +": User ID already Exist";
                    return RedirectToAction("Login");
                }
                pERSON.Joined = DateTime.Now;
                pERSON.Active = true;
                UserBAL.CreateUser(pERSON);
               
                return RedirectToAction("Login");
            }

            return View(pERSON);
        }
        [HttpPost]
        public ActionResult Login(PERSON LoginInfo)
        {
            if (ModelState.IsValid)
            {
                if(userBal.ValidateUser(LoginInfo))
                {
                    FormsAuthentication.SetAuthCookie(LoginInfo.User_ID, false);
                    return RedirectToAction("Home", "Twitter");
                }
                else
                {
                    TempData["Message"] = "Invalid Login Details";
                    return RedirectToAction("Login");
                }
                   
            }
            else
            {
                return RedirectToAction("Login");
            }
            //FormsAuthentication.SetAuthCookie(LoginInfo.UserName, false);
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSON pERSON = userBal.GetUserDetails(id);
            if (pERSON == null)
            {
                return HttpNotFound();
            }
            return View(pERSON);
        }



    }

}