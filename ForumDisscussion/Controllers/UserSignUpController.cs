using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ForumDisscussion.Models;
using WebMatrix.WebData;

namespace ForumDisscussion.Controllers
{
    public class UserSignUpController : Controller
    {
        readonly ForumDataBaseConnectionContext _db = new ForumDataBaseConnectionContext();

        public ActionResult SignUp()
        {

            if (Request.IsAjaxRequest())
            {


                return View();
            }

            return View();
        }



        [HttpPost]
        public ActionResult SignUp(UserSignUpModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var users = new ForumUser();
                    var data = new byte[model.File.ContentLength];
                    model.File.InputStream.Read(data, 0, model.File.ContentLength);
                    model.Image = data;
                    users.Image = model.Image;
                    users.FirstName = model.FirstName;
                    users.LastName = model.LastName;
                    users.UserName = model.UserName;
                    users.Country = model.Country;
                    users.Email = model.Email;
                    users.Password = model.Password;
                    _db.ForumUser.Add(users);
                    _db.SaveChanges();
                    var pi = new PostInformation();

                    var u = _db.ForumUser.SingleOrDefault(m => m.UserName.Equals(model.UserName));
                    if (u != null)
                        pi.UserId = u.UserId;
                    pi.TotalAnsPost = 0;
                    pi.TotalCommentPost = 0;
                    pi.TotalQuesPost = 0;
                    pi.TotalValidPost = 0;
                    _db.PostInformation.Add(pi);
                    _db.SaveChanges();
                    return RedirectToAction("Login", "Account");

                }
            }
            catch (Exception)
            {
                Response.Write("<div id='logDialog'>");
                Response.Write("You must upload your Image.And Image must be of jpg typed");
                Response.Write("</div>");
                return View(model);
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]

        public ActionResult Login(UserLogin users, string returnUrl)
        {
            try
            {



                if (ModelState.IsValid)
                {

                    var u = _db.ForumUser.SingleOrDefault(
                                    m => m.UserName.Equals(users.UserName) && m.Password.Equals(users.Password));


                    if (u != null)
                    {

                        FormsAuthentication.SetAuthCookie(users.UserName, users.RememberMe);
                        if (TempData["UQC"] != null)
                        {
                            return RedirectToAction("PostQuestion", "UserQuestions");
                        }
                        if (TempData["operation"] != null)
                        {

                            return RedirectToAction("PostAnswer", "Home");
                        }
                        if (TempData["HPostController"] != null)
                        {

                            return RedirectToAction("PostComment", "Home");
                        }
                        if (TempData["AreaFQAC"] != null)
                        {

                            return RedirectToAction("PostComment", "ForumQuestionAns", new { area = "ForumUser" });
                        }
                        if (TempData["AreaFQAnsPostController"] != null)
                        {
                            return RedirectToAction("PostAnswerForAnsweredQuestion", "ForumQuestionAns", new { area = "ForumUser" });
                        }
                        if (TempData["HomeAnsPostController"] != null)
                        {
                            return RedirectToAction("PostAnswer", "Home");
                        }

                        if (TempData["FQACPostAnsUnAnsweredQuestion"] != null)
                        {
                            return RedirectToAction("PostAnswer", "ForumQuestionAns", new { area = "ForumUser" });
                        }




                    }
                    else
                    {
                        Response.Write("<div id='loginFDialog'>");

                        Response.Write("<img src='/Png/remove.png' id='p'/><span id='f'>Invalid <b>Username</b> and <b>Password</b> combination.Have you <b>registered</b> ?If not <b>Signup</b> first</span>.");
                        Response.Write("</div>");
                        return View();
                    }
                    return RedirectToAction("UserAnsQuestionList", "UserHomePage");
                }

            }
            catch (Exception message)
            {

                Response.Write("<div id='logDialog'>");
                Response.Write(message.Message);

                Response.Write("</div>");
            }
            return View();
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            TempData["UQC"] = null;
            TempData["AreaFQAC"] = null;
            TempData["HPostController"] = null;
            TempData["operation"] = null;
            TempData["HomeAnsPostController"] = null;
            TempData["AreaFQAnsPostController"] = null;
            TempData["FQACPostAnsUnAnsweredQuestion"] = null;
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }


        public JsonResult CheckIsEmailExist(string email)
        {

            return Json(!_db.ForumUser.Any(x => x.Email.Equals(email)), JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckIsUsernameExist(string username)
        {

            return Json(!_db.ForumUser.Any(x => x.UserName.Equals(username)), JsonRequestBehavior.AllowGet);
        }
    }
}
