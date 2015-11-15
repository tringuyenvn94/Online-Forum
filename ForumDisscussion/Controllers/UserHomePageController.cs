

using System;

using System.Data;
using System.Linq;

using System.Web.Mvc;

using ForumDisscussion.Models;

namespace ForumDisscussion.Controllers
{
    public class UserHomePageController : Controller
    {
        readonly ForumDataBaseConnectionContext _db = new ForumDataBaseConnectionContext();


        Questions _question = new Questions();
        Comment comment = new Comment();
        [Authorize]
        public ActionResult UserAnsQuestionList()
        {


            int authUserId = _db.ForumUser.Single(m => m.UserName.Equals(User.Identity.Name)).UserId;
            ViewData["ProfileInfo"] = from u in _db.ForumUser
                                      where u.UserId == authUserId
                                      select new ForumUser()
                                          {
                                              FirstName = u.FirstName,
                                              LastName = u.LastName,
                                              UserName = u.UserName,
                                              Country = u.Country,
                                              Email = u.Email
                                          };
       
                                           
            return View(_db.ForumUser.Where(m=>m.UserName.Equals(User.Identity.Name)).ToList());




        }

        public ActionResult PostComment(int id = 0)
        {
            ViewData["op"] = 9;
            DisplayAnswerAndPostedUserInfo(id);
            DisplayComments(id);
            return View("Comments");
        }
        private void DisplayAnswerAndPostedUserInfo(int id)
        {
            ViewData["Query"] = (from a in _db.Answers
                                 join u in _db.ForumUser on a.UserId equals u.UserId
                                 where a.AnswerId.Equals(id)
                                 select new AnsPostedUser() { Image = u.Image, Username = u.UserName, PostedDate = a.AnsweredTimeDate, Answer = a.Answer });
            ViewBag.totalComments = _db.Comment.Count(m => m.AnswerId.Equals(id));

        }

        private void DisplayComments(int id)
        {
            ViewData["Comments"] = (from c in _db.Comment
                                    join u in _db.ForumUser on c.UserId equals u.UserId
                                    where c.AnswerId.Equals(id)
                                    select new UserComment() { CommentMsg = c.CommentMsg, Username = u.UserName, Image = u.Image, PostedDate = c.PostedDateTime }).ToList();




        }
        [HttpPost]
        public ActionResult PostComment(int? id, string msg)
        {
            var u = _db.ForumUser.SingleOrDefault(m => m.UserName.Equals(User.Identity.Name));
            if (u == null) return View("Comments");
            var userId = u.UserId;
            if (id == null) return View("Comments");
            DisplayAnswerAndPostedUserInfo(id.Value);
            DisplayComments(id.Value);
            ViewData["op"] = 9;

            if (msg != "")
            {

                if (msg.Length >= 3)
                {
                    comment.CommentMsg = msg;
                    comment.UserId = userId;
                    comment.AnswerId = id.Value;
                    comment.PostedDateTime = DateTime.Now;
                    _db.Comment.Add(comment);
                    _db.SaveChanges();
                    DisplayComments(id.Value);
                    UpdateTotComments(id.Value);
                    DisplayAnswerAndPostedUserInfo(id.Value);
                }

                else
                {
                    ViewData["ReqMsg"] = 1;
                    ViewBag.errormsg = "* Comment should not contain less than 3 chracters.";
                }

            }

            else
            {
                ViewData["ReqMsg"] = 1;
                ViewBag.errormsg = "* You must Write Something! to Post  comment";


            }


            return View("Comments");
        }

      

        private void UpdateTotComments(int id)
        {
            var v = _db.Answers.SingleOrDefault(x => x.AnswerId.Equals(id));
            if (v != null)
            {
                if (v.Comments != null)
                {
                    var totComts = v.Comments.Value;
                    var answer = _db.Answers.Single(m => m.AnswerId.Equals(id));
                    answer.Comments = totComts + 1;
                    _db.Entry(answer).State = EntityState.Modified;
                }
            }
            _db.SaveChanges();


        }

               public ActionResult UpdateImage()
               {

                   return View();
               }
                [HttpPost]
               public ActionResult UpdateImage(ImageUpdateViewModel m)
               {
                    if (!ModelState.IsValid) return View();
                    var c = _db.ForumUser.SingleOrDefault(u => u.UserName.Equals(User.Identity.Name));
                    if (c != null)
                    {
                        var authUserId = c.UserId;
                        var usr = _db.ForumUser.Find(authUserId);
                        var data = new byte[m.File.ContentLength];
                        m.File.InputStream.Read(data, 0, m.File.ContentLength);
                        m.Image = data;

                        usr.Image = m.Image;
                        _db.Entry(usr).State=EntityState.Modified;
                    }
                    _db.SaveChanges();
                    ViewBag.imgUpSuccessId = 1;
                    return View();
               }

        public ActionResult UpdatePassword()
        {
          

            return View();

        }



        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdatePassword(UserPasswordUpdateModel model)
        {
            if (!ModelState.IsValid) return View();
            var v = _db.ForumUser.SingleOrDefault(m => m.UserName.Equals(User.Identity.Name));
            if (v == null) return View();
            var id = v.UserId;
            var checkOldPassword = _db.ForumUser.SingleOrDefault(m=>m.UserId.Equals(id)&&m.Password.Equals(model.OldPassword));
            if (checkOldPassword!=null)
            {
                var u = _db.ForumUser.Find(id);
                u.Password = model.NewPassword;
                _db.Entry(u).State=EntityState.Modified;
                _db.SaveChanges();
                ViewBag.successMs =1;
            }
            else
            {
                ModelState.AddModelError("","* Sorry,Your entered password do not match with your old password!");
            }

            return View();

            }
        }
    }

