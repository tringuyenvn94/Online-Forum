using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumDisscussion.Models;
using PagedList;


namespace ForumDisscussion.Controllers
{

    public class UserQuestionsController : Controller
    {
        readonly ForumDataBaseConnectionContext _db = new ForumDataBaseConnectionContext();


        public ActionResult PostQuestion()
        {

            try
            {
                if (!Request.IsAuthenticated)
                {
                    ViewBag.authErrorState = 402;

                    TempData["UQC"] = 3;
                }
                if (Request.IsAuthenticated)
                {

                    ViewBag.authErrorState = null;
                }
                ViewBag.cs = _db.Category.Select(m => m.CategoryName);

                var u = new ForumUser();

                var categories = _db.Category.Select(c => new
                {
                    CategoryID = c.CategoryId,

                    Category = c.CategoryName
                }).ToList();

                ViewBag.Categories = new SelectList(categories, "CategoryID", "Category");


                ViewData["QuestionLists"] = (_db.Questions.Join(_db.ForumUser, q => q.UserId, us => us.UserId,
                    (q, us) => new {q, us})
                    .Join(_db.Category, @t => @t.q.CategoryId, c => c.CategoryId, (@t, c) => new {@t, c})
                    .OrderByDescending(@t => @t.@t.q.PostedDate)
                    .Select(@t => new QuestionList()
                    {
                        Question = @t.@t.q.Question,
                        Image = @t.@t.us.Image,
                        PostedBy = @t.@t.us.UserName,
                        Posteddate = @t.@t.q.PostedDate,
                        Category = @t.c.CategoryName,
                        Answers = @t.@t.q.answers,
                        Views = @t.@t.q.views,
                        Id = @t.@t.q.QuestionId
                    }));
            }
            catch (Exception e)
            {

            }



            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PostQuestion(QuestionPostModel model)
        {
            if (Request.IsAuthenticated)
            {

                if (ModelState.IsValid)
                {
                    try
                    {

                        var checkQuestionIsExist = _db.Questions.Any(m => m.Question.Equals(model.QuestionMessage));
                        if (!checkQuestionIsExist)
                        {
                            var u = _db.ForumUser.FirstOrDefault(m => m.UserName.Equals(User.Identity.Name));
                            if (u != null)
                            {
                                var userId = u.UserId;
                                var userQuestions = new Questions
                                {
                                    PostedDate = DateTime.Now,
                                    Question = model.QuestionMessage,
                                    CategoryId = Convert.ToInt32(model.Category),
                                    UserId = userId,
                                    answers = 0,
                                    views = 0
                                };


                                _db.Questions.Add(userQuestions);
                                _db.SaveChanges();
                                var v = _db.PostInformation.SingleOrDefault(m => m.UserId.Equals(userId));
                                if (v != null)
                                {
                                    var posId = v.PostId;
                                    var totalQuesPost = _db.PostInformation.Find(posId).TotalQuesPost;
                                    if (totalQuesPost != null)
                                    {
                                        var prevPosts = totalQuesPost.Value;
                                        var pi = _db.PostInformation.Find(posId);


                                        pi.TotalQuesPost = prevPosts + 1;

                                        _db.Entry(pi).State = EntityState.Modified;
                                    }
                                }
                            }
                            _db.SaveChanges();

                            ViewData["unLUQC"] = 4;
                            ViewData["QuestionLists"] = (from q in _db.Questions

                                                         join us in _db.ForumUser on q.UserId equals us.UserId
                                                         join c in _db.Category on q.CategoryId equals c.CategoryId
                                                         orderby q.QuestionId descending



                                                         select
                                                             new QuestionList()
                                                             {
                                                                 Question = q.Question,
                                                                 Image = us.Image,
                                                                 PostedBy = us.UserName,
                                                                 Posteddate = q.PostedDate,
                                                                 Category = c.CategoryName,
                                                                 Answers = q.answers,
                                                                 Views = q.views,
                                                                 Id = q.QuestionId
                                                             });
                            var categories = _db.Category.Select(c => new
                            {
                                CategoryID = c.CategoryId,
                                CategoryName = c.CategoryName
                            }).OrderByDescending(m => m.CategoryID).ToList();
                            ViewBag.Categories = new MultiSelectList(categories, "CategoryID", "");



                            Response.Write("<div id='op'></div>");
                        }
                        else
                        {
                            ModelState.AddModelError("","It seems that this question already exist.");
                        }
                  
                    }
                    catch (Exception e)
                    {

                        
                    }


                }

                ViewData["QuestionLists"] = (_db.Questions.Join(_db.ForumUser, q => q.UserId, us => us.UserId,
                    (q, us) => new {q, us})
                    .Join(_db.Category, @t => @t.q.CategoryId, c => c.CategoryId, (@t, c) => new QuestionList()
                    {
                        Question = @t.q.Question,
                        Image = @t.us.Image,
                        PostedBy = @t.us.UserName,
                        Posteddate = @t.q.PostedDate,
                        Category = c.CategoryName,
                        Answers = @t.q.answers,
                        Views = @t.q.views,
                        Id = @t.q.QuestionId
                    })).OrderByDescending(m => m.Id);



                var cates = _db.Category.Select(c => new
                {
                    CategoryID = c.CategoryId,
                    CategoryName = c.CategoryName


                }).ToList();

                ViewBag.Categories = new SelectList(cates, "CategoryID", "CategoryName", Convert.ToInt32(model.Category).ToString());





            }
            else
            {
                ViewBag.authErrorState = 402;
                TempData["UQC"] = 3;
            }



            return View(model);

        }


    }
}
