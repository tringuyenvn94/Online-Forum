using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumDisscussion.Models;
using WebGrease.Css.Ast.Selectors;

namespace ForumDisscussion.Controllers
{
    public class ForumQuestionListController : Controller
    {
        private ForumDataBaseConnectionContext _db = new ForumDataBaseConnectionContext();
        Questions _question = new Questions();
        public List<QuestionList> GetData()
        {
            var data = (from u in _db.ForumUser
                        join q in _db.Questions on u.UserId equals q.UserId

                        join c in _db.Category on q.CategoryId equals c.CategoryId
                        join a in _db.Answers on q.QuestionId equals a.QuestionId into d
                        from b in d.DefaultIfEmpty()


                        select new QuestionList()
                        {
                            Question = q.Question,
                            //Question = q.Question1.Remove(70),
                            Category = c.CategoryName,
                            PostedBy = u.UserName,
                            Posteddate = q.PostedDate,
                            Answers = q.answers,
                            Views = q.views,
                            Id = q.QuestionId

                        }).ToList();

            return data;
        }

        public ActionResult Index()
        {



            return View(GetData());
        }
        [Authorize]
        public ActionResult PostAnswer(int id = 0)
        {

            DisplayAnsList(id);
            UpdateTotViews(id);

            return View();
        }




        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PostAnswer(UsersAnsPost post, int id, string answer)
        {
            DisplayAnsList(id);

            if (ModelState.IsValid)
            {
                var ans = new Answers();
                var authuserId = _db.ForumUser.Single(m => m.UserName.Equals(User.Identity.Name)).UserId;
                ans.UserId = authuserId;
                ans.AnsweredTimeDate = DateTime.Now;
                ans.QuestionId = id;
                ans.Answer = answer;
                _db.Answers.Add(ans);
                _db.SaveChanges();
                DisplayAnsList(id);

            }

            return View();
        }

        private void DisplayAnsList(int id)
        {

            ViewBag.quId = id;
            var u = _db.Questions.FirstOrDefault(m => m.QuestionId.Equals(id));
            if (u != null)
                ViewBag.m = u.Question;
            ViewBag.Answers = _db.Answers.Where(X => X.QuestionId.Equals(id)).Select(m => m.Answer).ToList();
            ViewBag.UserId = _db.Answers.Where(m => m.QuestionId.Equals(id)).Select(m => m.UserId).ToList();

        }

        private void UpdateTotViews(int id)
        {
            var v = _db.Questions.SingleOrDefault(x => x.QuestionId.Equals(id));
            if (v != null)
            {
                int totAns = v.views;
                _question = _db.Questions.Single(m => m.QuestionId.Equals(id));
                _question.views = totAns + 1;
            }
            _db.Entry(_question).State = EntityState.Modified;
            _db.SaveChanges();


        }


    }

}
