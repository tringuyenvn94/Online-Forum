using System;

using System.Data;
using System.Linq;
using ForumDisscussion.Filters;
using System.Web.Mvc;
using System.Web.Security;
using ForumDisscussion.Models;

namespace ForumDisscussion.Controllers
{
    public class AdminController : Controller
    {
        readonly AdminDbContext _d = new AdminDbContext();
        private readonly ForumDataBaseConnectionContext _db = new ForumDataBaseConnectionContext();
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated) return RedirectToAction("Index", "Home");
            var authEmail = _d.Admin.SingleOrDefault(m => m.Email.Equals(User.Identity.Name));
            if (authEmail == null) return RedirectToAction("Index", "Home");
            ViewData["userDetails"] = (

                from p in _db.PostInformation
                join u in _db.ForumUser on p.UserId equals u.UserId into uu
                from usr in uu.DefaultIfEmpty()



                select new UsersDetailsViewModel()
                {
                    Id = usr.UserId,
                    UserImage = usr.Image,
                    Username = usr.UserName,
                    Firstname = usr.FirstName,
                    Lastname = usr.LastName,
                    Country = usr.Country,
                    Email = usr.Email,
                    TotalQuestion = p.TotalQuesPost.Value,
                    TotalReply = p.TotalAnsPost.Value,
                    TotalComment = p.TotalCommentPost.Value,
                    TotalValidPost = p.TotalValidPost.Value,
                    LstActive = usr.LastLogin.Value

                }).OrderBy(m => m.Username).ToList();






            return View();
        }

  

        public ActionResult Delete(int id)
        {

            try
            {
                var poin = _db.PostInformation.SingleOrDefault(m => m.UserId == id);
                var q = _db.Questions.FirstOrDefault(m => m.UserId == id);
                var a = _db.Answers.FirstOrDefault(m => m.UserId == id);
                var c = _db.Comment.FirstOrDefault(m => m.UserId == id);

                if (q != null)
                {
                    var qid = _db.Questions.Where(m => m.UserId.Equals(id)).Select(m => m.QuestionId);
                    foreach (var i in qid)
                    {
                        q = _db.Questions.Find(i);
                        _db.Entry(q).State = EntityState.Deleted;

                    }
                    _db.SaveChanges();
                }
                if (a != null)
                {
                    var aid = _db.Answers.Where(m => m.UserId.Equals(id)).Select(m => m.AnswerId);
                    foreach (var i in aid)
                    {
                        a = _db.Answers.Find(i);
                        _db.Entry(a).State = EntityState.Deleted;

                    }
                    _db.SaveChanges();
                }
                if (c != null)
                {
                    var cid = _db.Comment.Where(m => m.UserId.Equals(id)).Select(m => m.CommentId);
                    foreach (var i in cid)
                    {
                        c = _db.Comment.Find(i);
                        _db.Entry(c).State = EntityState.Deleted;

                    }
                    _db.SaveChanges();
                }
                if (poin != null)
                {



                    _db.Entry(poin).State = EntityState.Deleted;

                    _db.SaveChanges();




                }
                var fu = _db.ForumUser.Find(id);
                _db.Entry(fu).State = EntityState.Deleted;
                _db.SaveChanges();
            }
            catch (Exception e)
            {

                Response.Write(e);
            }


            return RedirectToAction("Index", "Admin");
        }

        public ActionResult LogOn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogOn(Admin a)
        {
            if (!ModelState.IsValid) return View();
            var v = _d.Admin.SingleOrDefault(m => m.Email.Equals(a.Email) && m.Password.Equals(a.Password));

            if (v == null)
            {
                ModelState.AddModelError("", "Invalid Attempt to Login");
            }
            else
            {
                UpdateLoginDateTime(a.Email);
                FormsAuthentication.SetAuthCookie(a.Email, false);
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        private void UpdateLoginDateTime(string email)
        {
            try
            {
                var u = _d.Admin.SingleOrDefault(m => m.Email.Equals(email));
                if (u != null)
                {
                    var id = u.Id;
                    var a = _d.Admin.Find(id);
                    _d.Entry(a).State = EntityState.Modified;
                }
                _d.SaveChanges();
            }
            catch (Exception e)
            {
                Response.Write(e);

            }

        }
        public ActionResult AddNewCategory(int? id)
        {
            ViewData["Operation"] = id;
            ViewData["GetCategories"] = from c in _db.Category
                                        select new CategoryViewModel() { CategoryId = c.CategoryId, CategoryName = c.CategoryName };
            var category = _db.Category.Find(id);
            return View(category);

        }
        [HttpPost]
        [OnAction(ButtonName = "Create")]
        public ActionResult AddNewCategory(Category category)
        {

            if (ModelState.IsValid)
            {
                if (!_db.Category.Any(m=>m.CategoryName.Equals(category.CategoryName)))
                {
                    _db.Category.Add(category);
                    _db.SaveChanges();
                    ViewBag.successMsg = "New category has been added successfully";
                    ViewBag.emptyId = 1;

                }
                else
                {
                  ModelState.AddModelError("","Category name already exist!");  
                }
            
            }
            ViewData["GetCategories"] = from c in _db.Category
                                        select new CategoryViewModel() { CategoryId = c.CategoryId, CategoryName = c.CategoryName };
            ViewBag.successMseeageShowId = 1;
            return View();

        }

        public ActionResult Update(int id=0)
        {
            var category = _db.Category.Find(id);
            return View(category);

        }
        [HttpPost]
  
        public ActionResult Update(Category category)
        {
            if (!ModelState.IsValid) return View();
            _db.Entry(category).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("AddNewCategory");
        }
        public ActionResult DeleteCategory(int id) {
          
                try
                {
                    var category = _db.Category.Find(id);
                    _db.Entry(category).State = EntityState.Deleted;
                    _db.SaveChanges();
                }

                catch (Exception e) { Response.Write(e); }
          
         
           return RedirectToAction("AddNewCategory");
        }
    
    }
}
