using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using ForumDisscussion.Models;

using WebGrease.Css.Extensions;

namespace ForumDisscussion.Controllers
{
    [Authorize]
    public class UserAccountController : Controller
    {
        readonly ForumDataBaseConnectionContext _forumDb = new ForumDataBaseConnectionContext();
        //UserLoginDbContext _db=new UserLoginDbContext();
        public ActionResult UpdateAccount()
        {

            var users = new ForumUserProfile();
            var u = _forumDb.ForumUser.FirstOrDefault(x => x.UserName.Equals(User.Identity.Name));
            if (u != null)
                users.Fname = u.FirstName;

            var v = _forumDb.ForumUser.FirstOrDefault(x => x.UserName.Equals(User.Identity.Name));
            if (v != null)
                users.Lname = v.LastName;
            var w = _forumDb.ForumUser.FirstOrDefault(x => x.UserName.Equals(User.Identity.Name));
            if (w != null)
                users.EmailId = w.Email;

            return View(users);
        }



        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateAccount(ForumUserProfile usetProfiles)
        {
            var user = new ForumUser();


            if (ModelState.IsValid)
            {
                int userId = _forumDb.ForumUser.Single(m => m.UserName.Equals(User.Identity.Name)).UserId;


                var userEmail = _forumDb.ForumUser.SingleOrDefault(email => email.Email == usetProfiles.EmailId);

                var u = _forumDb.ForumUser.SingleOrDefault(email => email.UserName.Equals(User.Identity.Name));
                if (u == null) return View();
                var authUserEmail = u.Email;




                if (usetProfiles.OldPassword == null)
                {
                    if (userEmail != null)
                    {
                        var v = _forumDb.ForumUser.SingleOrDefault(email => email.Email == usetProfiles.EmailId);
                        if (v == null) return View();
                        var userEmailId = v.Email;

                        if (userEmailId == authUserEmail)
                        {

                            user = _forumDb.ForumUser.Single(m => m.UserId == userId);
                            user.FirstName = usetProfiles.Fname;
                            user.LastName = usetProfiles.Lname;
                            user.Email = usetProfiles.EmailId;
                            _forumDb.Entry(user).State = EntityState.Modified;
                            _forumDb.SaveChanges();
                            Response.Write("<div id='acUpDialog'>");
                            Response.Write("<img src='/Png/exclamation_circle.png'>");
                            Response.Write("Account Updated Successfully");
                            Response.Write("</div>");
                            ViewBag.p = 1;
                        }

                        else
                        {
                            Response.Write("<div id='logDialog'>");
                            Response.Write("This Email already in use.It seems this is not your Email.Please enter your own email.");
                            Response.Write("</div>");
                            ViewBag.p = 2;
                        }
                    }

                    else
                    {

                        user = _forumDb.ForumUser.Single(m => m.UserId == userId);
                        user.FirstName = usetProfiles.Fname;
                        user.LastName = usetProfiles.Lname;
                        user.Email = usetProfiles.EmailId;
                        _forumDb.Entry(user).State = EntityState.Modified;
                        _forumDb.SaveChanges();
                        Response.Write("Account Updated Successfully");
                        ViewBag.p = 1;
                    }


                }
                else
                {
                    var userOldPass = _forumDb.ForumUser.SingleOrDefault(pass => pass.Password == usetProfiles.OldPassword);
                    if (userOldPass != null)
                    {
                        if (usetProfiles.NewPassword != null)
                        {

                            user = _forumDb.ForumUser.Single(m => m.UserId == userId);
                            user.FirstName = usetProfiles.Fname;
                            user.LastName = usetProfiles.Lname;
                            user.Email = usetProfiles.EmailId;
                            user.Password = usetProfiles.NewPassword;
                            _forumDb.Entry(user).State = EntityState.Modified;
                            _forumDb.SaveChanges();
                            Response.Write("Account Updated Successfully"); ;
                            ViewBag.p = 1;
                        }
                        else
                        {
                            Response.Write("<div id='logDialog'>");
                            Response.Write("You must enter a new password to change password.");
                            Response.Write("</div>");
                            ViewBag.p = 3;
                        }
                    }
                    else
                    {
                        Response.Write("<div id='logDialog'>");
                        Response.Write("<img src='/Png/cancel(1).png' id='p'/><div id='t'>Sorry,Your old<b> Password</b> you typed  is not correct</div>");
                        Response.Write("</div>");
                        ViewBag.p = 5;
                    }
                }
            }












            return View();


        }




    }

}

