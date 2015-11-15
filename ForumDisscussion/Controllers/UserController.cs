using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumDisscussion.Models;

namespace ForumDisscussion.Controllers
{
    public class UserController : Controller
    {

        private ForumDataBaseConnectionContext _db = new ForumDataBaseConnectionContext();

        public ActionResult GetAllUsers()
        {
            ViewBag.co = _db.PostInformation.Select(m => m.PostId).Count();
            ViewData["users"] = (from u in _db.ForumUser
                                 join p in _db.PostInformation on u.UserId equals p.UserId
                                 select new UsersDetailsViewModel()
                                     {
                                         UserImage = u.Image,
                                         Username = u.UserName,
                                         Country = u.Country,
                                         TotalQuestion = p.TotalQuesPost.Value,
                                         TotalReply = p.TotalAnsPost.Value,
                                         TotalComment = p.TotalCommentPost.Value,
                                         TotalValidPost = p.TotalValidPost.Value


                                     });



            return View();
        }
        [HttpPost]
        public ActionResult GetAllUsers(UsersDetailsViewModel model)
        {









            return View();
        }
    }
}
