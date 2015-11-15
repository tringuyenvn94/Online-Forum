using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumDisscussion.Models;

namespace ForumDisscussion.Controllers
{
    public class UsersController : Controller
    {
        ForumDbContext _db=new ForumDbContext();

        public ActionResult ForumUser()
        {
         
            return View();
        }

    }
}
