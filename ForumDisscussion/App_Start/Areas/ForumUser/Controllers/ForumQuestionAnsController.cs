using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using System.Web.Mvc;

using ForumDisscussion.Models;


namespace ForumDisscussion.Areas.ForumUser.Controllers
{
    public class ForumQuestionAnsController : Controller
    {
        VirtualDbContext b = new VirtualDbContext();
        TempDbContext db = new TempDbContext();
        Questions question = new Questions();
        ForumDataBaseConnectionContext _db = new ForumDataBaseConnectionContext();
        public JsonResult DisplayCat(string searchByCat)
        {
            ViewBag.list = _db.Category.Select(m => m.CategoryName);


            return Json(ViewBag.list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UnAnsweredQuestions()
        {

            return View(GetUnAnsweredQuestions(""));
        }


        [HttpPost]
        public ActionResult UnAnsweredQuestions(string searchTerm, int id = 0)
        {
            if (searchTerm == string.Empty)
            {

                ViewBag.reqErrorMsg = "* Please enter a category name for Searching";

            }
            return View(GetUnAnsweredQuestions(searchTerm));
        }

        public ActionResult PostAnswer(int id = 0)
        {

            if (id == 0)
            {
                int tempId = _db.Temp.Single(m => m.Id.Equals(1)).TempId.Value;

                ViewData["GetQuestionAndImageByID"] = (from q in _db.Questions
                                                       join u in _db.ForumUser on q.UserId equals u.UserId
                                                       where q.QuestionId.Equals(tempId)
                                                       select new AnsPostedUser() { Image = u.Image, Question = q.Question, Username = u.UserName, PostedDate = q.PostedDate });

            }
            else
            {
                UpdateTemId(id);
                ViewData["GetQuestionAndImageByID"] = (from q in _db.Questions
                                                       join u in _db.ForumUser on q.UserId equals u.UserId
                                                       where q.QuestionId.Equals(id)
                                                       select new AnsPostedUser() { Image = u.Image, Question = q.Question, Username = u.UserName, PostedDate = q.PostedDate });



                TempData["t"] = ViewData["GetQuestionByID"];
            }
            return View("PostAnswer");

        }
        [HttpPost]
        public ActionResult PostAnswer(int? id, string msg)
        {


            if (Request.IsAuthenticated)
            {
                if (id == null)
                {
                    int tempId = _db.Temp.Single(m => m.Id.Equals(1)).TempId.Value;

                    if (msg != "")
                    {
                        if (msg.Length >= 10)
                        {
                            Answers ans = new Answers();
                            int authuserId = _db.ForumUser.Single(m => m.UserName.Equals(User.Identity.Name)).UserId;
                            ans.UserId = authuserId;
                            ans.AnsweredTimeDate = DateTime.Now;
                            ans.QuestionId = tempId;
                            ans.Answer = msg;


                            _db.Answers.Add(ans);
                            _db.SaveChanges();
                            UpdateAns(tempId);

                            ViewData["PostedAnsAndUserInformation"] = (from a in _db.Answers
                                                                       join u in _db.ForumUser on a.UserId equals u.UserId
                                                                       where a.QuestionId.Equals(tempId)
                                                                       select new AnsPostedUser() { Username = u.UserName, Image = u.Image, PostedDate = a.AnsweredTimeDate, Answer = a.Answer }).ToList();





                            ViewData["GetQuestionAndImageByID"] = (from q in _db.Questions
                                                                   join u in _db.ForumUser on q.UserId equals u.UserId
                                                                   where q.QuestionId.Equals(tempId)
                                                                   select new AnsPostedUser() { Image = u.Image, Question = q.Question });


                        }
                        else
                        {
                            ViewData["GetQuestionAndImageByID"] = (from q in _db.Questions
                                                                   join u in _db.ForumUser on q.UserId equals u.UserId
                                                                   where q.QuestionId.Equals(tempId)
                                                                   select new AnsPostedUser() { Image = u.Image, Question = q.Question });

                            ViewData["ReqMsg"] = 3;
                            ViewData["Mess"] = "* Answers should not contain less than 10 chracters";
                            return View();
                        }
                    }
                    else
                    {

                        ViewData["GetQuestionAndImageByID"] = (from q in _db.Questions
                                                               join u in _db.ForumUser on q.UserId equals u.UserId
                                                               where q.QuestionId.Equals(tempId)
                                                               select new AnsPostedUser() { Image = u.Image, Question = q.Question });


                        ViewData["ReqMsg"] = 3;
                        ViewData["Mess"] = "*  Empty Field not allowed!.Please Write Your Answer";
                        return View();
                    }
                }
                else
                {
                    if (msg != "")
                    {
                        if (msg.Length >= 10)
                        {


                            Answers ans = new Answers();
                            int authuserId = _db.ForumUser.Single(m => m.UserName.Equals(User.Identity.Name)).UserId;
                            ans.UserId = authuserId;
                            ans.AnsweredTimeDate = DateTime.Now;
                            ans.QuestionId = id.Value;
                            ans.Answer = msg;

                            ViewData["Date"] = ans.AnsweredTimeDate;
                            _db.Answers.Add(ans);
                            _db.SaveChanges();
                            UpdateAns(id.Value);
                            ViewData["PostedAnsAndUserInformation"] = (from a in _db.Answers
                                                                       join u in _db.ForumUser on a.UserId equals u.UserId
                                                                       where a.QuestionId.Equals(id.Value)
                                                                       select new AnsPostedUser() { Username = u.UserName, Image = u.Image, PostedDate = a.AnsweredTimeDate, Answer = a.Answer }).ToList();

                            ViewData["GetQuestionAndImageByID"] = (from q in _db.Questions
                                                                   join u in _db.ForumUser on q.UserId equals u.UserId
                                                                   where q.QuestionId.Equals(id.Value)
                                                                   select new AnsPostedUser() { Image = u.Image, Question = q.Question });

                            //return View();
                        }
                        else
                        {
                            ViewData["GetQuestionAndImageByID"] = (from q in _db.Questions
                                                                   join u in _db.ForumUser on q.UserId equals u.UserId
                                                                   where q.QuestionId.Equals(id.Value)
                                                                   select new AnsPostedUser() { Image = u.Image, Question = q.Question });

                            ViewData["ReqMsg"] = 3;
                            ViewData["Mess"] = "* Answers should not contain less than 10 chracters";

                        }
                    }
                    else
                    {
                        ViewData["GetQuestionAndImageByID"] = (from q in _db.Questions
                                                               join u in _db.ForumUser on q.UserId equals u.UserId
                                                               where q.QuestionId.Equals(id.Value)
                                                               select new AnsPostedUser() { Image = u.Image, Question = q.Question });

                        ViewData["ReqMsg"] = 3;
                        ViewData["Mess"] = "*  Empty Field not allowed!.Please Write Your Answer";


                    }
                }

            }
            else
            {

                ViewData["GetQuestionAndImageByID"] = (from q in _db.Questions
                                                       join u in _db.ForumUser on q.UserId equals u.UserId
                                                       where q.QuestionId.Equals(id.Value)
                                                       select new AnsPostedUser() { Image = u.Image, Question = q.Question });

                TempData["FQACPostAnsUnAnsweredQuestion"] = 1;
                ViewData["c"] = 3;
                return View();
            }
            ViewData["GetQuestionAndImageByID"] = (from q in _db.Questions
                                                   join u in _db.ForumUser on q.UserId equals u.UserId
                                                   where q.QuestionId.Equals(id.Value)
                                                   select new AnsPostedUser() { Image = u.Image, Question = q.Question }).ToList();

            return View();
        }



        private void UpdateAns(int id)
        {
            Questions question = new Questions();
            int totAns = _db.Questions.SingleOrDefault(x => x.QuestionId.Equals(id)).answers;
            question = _db.Questions.Single(m => m.QuestionId.Equals(id));
            question.answers = totAns + 1;
            _db.Entry(question).State = EntityState.Modified;
            _db.SaveChanges();


        }
        List<UnAnsweredQuesListModel> GetUnAnsweredQuestions(string cname)
        {
            var unansQuesList = (from q in _db.Questions
                                 join a in _db.Answers on q.QuestionId equals a.QuestionId into aa
                                 from ans in aa.DefaultIfEmpty()
                                 join u in _db.ForumUser on q.UserId equals u.UserId into uu
                                 from usr in uu.DefaultIfEmpty()
                                 join c in _db.Category on q.CategoryId equals c.CategoryId
                                 where ans.Answer == null
                                 where c.CategoryName.StartsWith(cname) || cname == null
                                 select
                                 new UnAnsweredQuesListModel()
                                 {
                                     Question = q.Question,
                                     PostedDate = q.PostedDate,
                                     Username = usr.UserName,
                                     Image = usr.Image,
                                     Category = c.CategoryName
                                     ,
                                     Views = q.views
                                     ,
                                     Id = q.QuestionId
                                 }).ToList();
            return unansQuesList;
        }

        private void DisplayQuestionAndPostedUserInfo(int id)
        {
            ViewBag.question = _db.Questions.FirstOrDefault(m => m.QuestionId.Equals(id)).Question;
            int quesUserId = _db.Questions.SingleOrDefault(x => x.QuestionId.Equals(id)).UserId;
            ViewBag.quesuserImg = _db.ForumUser.Single(x => x.UserId.Equals(quesUserId)).Image;
            ViewBag.userName = _db.ForumUser.Single(x => x.UserId.Equals(quesUserId)).UserName;
            ViewBag.PostDate = _db.Questions.Single(x => x.QuestionId.Equals(id)).PostedDate;


        }


        public ActionResult GetAnsweredQuestion()
        {

            IEnumerable<int> getIds = b.UserPostedQuestions.Select(m => m.Id);
            try
            {
                foreach (int id in getIds)
                {
                    UserPostedQuestion del = b.UserPostedQuestions.Where(m => m.Id.Equals(id)).Single();
                    b.UserPostedQuestions.Remove(del);

                }
                b.SaveChanges();


                TemporaryTable tmp = b.TemporaryTables.SingleOrDefault(m => m.Id.Equals(1));


                UserPostedQuestion ui = new UserPostedQuestion();


                var ansQuesList = (from q in _db.Questions

                                   join u in _db.ForumUser on q.UserId equals u.UserId
                                   join a in _db.Answers on q.QuestionId equals a.QuestionId
                                   join c in _db.Category on q.CategoryId equals c.CategoryId
                                   orderby q.PostedDate

                                   select
                                   new AnsweredQuestionModel()
                                   {
                                       Question = q.Question,
                                       PostedDate = q.PostedDate,
                                       Username = u.UserName,
                                       Category = c.CategoryName,
                                       Views = q.views,
                                       Replies = q.answers,
                                       Id = q.QuestionId,
                                       Image = u.Image
                                   });

                foreach (var h in ansQuesList)
                {
                    string g = h.Question;
                    var v = b.UserPostedQuestions.FirstOrDefault(
                                    m => m.Question.Equals(g));

                    if (v == null)
                    {
                        ui.Question = h.Question;




                        ui.Answer = h.Question;

                        ui.ReplyUser = h.Username;
                        ui.PostedDate = h.PostedDate;
                        ui.Image = h.Image;
                        ui.QuesPostDate = h.PostedDate;
                        ui.QuestionId = h.Id;
                        ui.Category = h.Category;
                        ui.TotalAnswer = h.Views;
                        ui.TotalReplies = h.Replies;
                        b.UserPostedQuestions.Add(ui);

                        b.SaveChanges();
                    }

                }
                ViewData["AnsweredQuesList"] = (from a in b.UserPostedQuestions

                                                select new QuestionList()
                                                {
                                                    Question = a.Question,
                                                    PostedBy = a.ReplyUser,
                                                    Image = a.Image,
                                                    Id = a.QuestionId.Value,
                                                    Category = a.Category,
                                                    Views = a.TotalAnswer.Value,
                                                    Answers = a.TotalReplies.Value,
                                                    Posteddate = a.QuesPostDate.Value

                                                }).OrderByDescending(x => x.Posteddate).ToList();



            }
            catch (Exception ex)
            {


            }
            ViewData["FQAC"] = 2;


            return View();
        }
        [HttpPost]
        public ActionResult GetAnsweredQuestion(string cname, int id = 0)
        {

            ViewData["FQAC"] = 2;
            if (cname == string.Empty)
            {

                ViewBag.reqErrorMsg = "* Please enter a category name for Searching";

            }



            ViewData["AnsweredQuesList"] = (from a in b.UserPostedQuestions
                                            where a.Category.StartsWith(cname)
                                            select new QuestionList()
                                            {
                                                Question = a.Question,
                                                PostedBy = a.ReplyUser,
                                                Image = a.Image,
                                                Id = a.QuestionId.Value,
                                                Category = a.Category,
                                                Views = a.TotalAnswer.Value,
                                                Answers = a.TotalReplies.Value,
                                                Posteddate = a.QuesPostDate.Value

                                            }).OrderByDescending(x => x.Posteddate).ToList();


            return View();
        }



        public ActionResult PostAnswerForAnsweredQuestion(int id = 0)
        {

            if (id == 0)
            {
                if (TempData["AreaFQAAnsTempId"] != null)
                {
                    int temAnsId = Convert.ToInt32(TempData["AreaFQAAnsTempId"]);
                    UpdateTemId(temAnsId);
                }

                int tempId = db.TemQuesIdModels.Single(m => m.Id.Equals(1)).TempQuesId;

                TempData["AreaFQAAnsId"] = Convert.ToInt32(tempId);
                DisplayQuestionAndAnswers(tempId);
                DisplayQuestionAndPostedUserInfo(tempId);
                UpdateTotViews(tempId);

            }

            else
            {
                TempData["AreaFQAAnsId"] = id;
                TempData["QusIdFetchToLoginCotroller"] = id;
                UpdateTempQuesId(id);
                DisplayQuestionAndAnswers(id);
                DisplayQuestionAndPostedUserInfo(id);
                UpdateTotViews(id);

            }


            ViewBag.checkCrontroller = 1;
            return View("_PostAnswer");
        }

        private void UpdateTemId(int id)
        {
            Temp te = _db.Temp.Single(m => m.Id.Equals(1));
            te.TempId = id;
            _db.Entry(te).State = EntityState.Modified;
            _db.SaveChanges();
        }

        private void DisplayQuestionAndAnswers(int id)
        {


            ViewBag.operation = 1;

            ViewData["GetAns"] = (from a in _db.Answers
                                  join u in _db.ForumUser on a.UserId equals u.UserId
                                  where a.QuestionId.Equals(id)
                                  select new AnsPostedUser() { Answer = a.Answer, AnsId = a.AnswerId, Username = u.UserName, PostedDate = a.AnsweredTimeDate, Image = u.Image, QuesId = a.QuestionId, Comments = a.Comments }).ToList();


            ViewData["totalAns"] = _db.Answers.Where(x => x.QuestionId.Equals(id)).Count();
        }

        [HttpPost]
        public ActionResult PostAnswerForAnsweredQuestion(int? id, string msg)
        {

            int tempId = db.TemQuesIdModels.SingleOrDefault(m => m.Id.Equals(1)).TempQuesId;

            if (Request.IsAuthenticated)
            {
                if (id == null)
                {

                    if (msg != "")
                    {
                        if (msg.Length >= 10)
                        {

                            string ms = "";
                            WordDbContext wdb = new WordDbContext();
                            var sensitiveWods = wdb.Words.Select(m => m.SensitiveWord);
                            List<SensitiveWordsViewModel> strList = new List<SensitiveWordsViewModel>();
                            ViewBag.fc = 2;
                            string convt = msg.ToLower();
                            foreach (string str in sensitiveWods)
                            {
                                if (convt.Contains(str))
                                {
                                    SensitiveWordsViewModel sensitive = new SensitiveWordsViewModel();
                                    sensitive.SensitiveWords = str;
                                    strList.Add(sensitive);
                                    ViewBag.senWordsMsg = 11;
                                    ms = str;

                                }





                            }
                            IEnumerable<SensitiveWordsViewModel> s = strList;
                            ViewData["s"] = s;

                            if (ms == "")
                            {
                                Answers ans = new Answers();
                                int authuserId = _db.ForumUser.Single(m => m.UserName.Equals(User.Identity.Name)).UserId;
                                ans.UserId = authuserId;
                                ans.AnsweredTimeDate = DateTime.Now;
                                ans.QuestionId = tempId;
                                ans.Answer = msg;
                                ans.Rating = 0;
                                _db.Answers.Add(ans);
                                _db.SaveChanges();

                                ViewData["c"] = 4;
                                ViewBag.operation = 2;
                                ViewBag.checkCrontroller = 2;
                                UpdateAns(tempId);
                                DisplayQuesAnsComments(tempId);

                            }


                        }
                        else
                        {

                            TempData["AnsId"] = tempId;
                            DisplayQuesAnsComments(tempId);
                            ViewBag.operation = 1;
                            ViewBag.checkCrontroller = 1;
                            ViewData["ReqMsg"] = 3;
                            ViewData["Mess"] = "* Answers should not contain less than 10 chracters";

                        }

                    }
                    else
                    {
                        TempData["AnsId"] = tempId;
                        DisplayQuesAnsComments(tempId);
                        ViewBag.operation = 1;
                        ViewBag.checkCrontroller = 1;
                        ViewData["ReqMsg"] = 3;
                        ViewData["Mess"] = "*  Empty Field not allowed!.Please Write Your Answer";

                        return View("_PostAnswer");
                    }

                }
                else
                {

                    if (msg != "")
                    {
                        if (msg.Length >= 10)
                        {

                            string ms = "";
                            WordDbContext wdb = new WordDbContext();
                            var sensitiveWods = wdb.Words.Select(m => m.SensitiveWord);
                            List<SensitiveWordsViewModel> strList = new List<SensitiveWordsViewModel>();

                            string convt = msg.ToLower();
                            foreach (string str in sensitiveWods)
                            {
                                if (convt.Contains(str))
                                {
                                    SensitiveWordsViewModel sensitive = new SensitiveWordsViewModel();
                                    sensitive.SensitiveWords = str;
                                    strList.Add(sensitive);
                                    ViewBag.senWordsMsg = 11;
                                    ms = str;

                                }





                            }
                            IEnumerable<SensitiveWordsViewModel> s = strList;
                            ViewData["s"] = s;
                            if (ms == "")
                            {

                                Answers ans = new Answers();
                                int authuserId = _db.ForumUser.Single(m => m.UserName.Equals(User.Identity.Name)).UserId;
                                ans.UserId = authuserId;
                                ans.AnsweredTimeDate = DateTime.Now;
                                ans.QuestionId = id.Value;
                                ans.Answer = msg;
                                ans.Rating = 0;
                                _db.Answers.Add(ans);
                                _db.SaveChanges();
                                ViewData["c"] = 4;
                                ViewBag.operation = 1;
                                ViewBag.checkCrontroller = 1;
                                UpdateAns(id.Value);
                                DisplayQuesAnsComments(id.Value);
                            }




                        }
                        else
                        {
                            TempData["AnsId"] = id.Value;
                            DisplayQuesAnsComments(id.Value);
                            ViewBag.operation = 1;
                            ViewBag.checkCrontroller = 1;
                            ViewData["ReqMsg"] = 3;
                            ViewData["Mess"] = "* Answers should not contain less than 10 chracters";

                        }
                    }
                    else
                    {
                        TempData["AnsId"] = id.Value;
                        DisplayQuesAnsComments(id.Value);
                        ViewBag.operation = 1;
                        ViewBag.checkCrontroller = 1;
                        ViewData["ReqMsg"] = 3;
                        ViewData["Mess"] = "*  Empty Field not allowed!.Please Write Your Answer";
                    }


                    UpdateTempQuesId(id.Value);
                    TempData["QiComm"] = id;
                    TempData["AnsId"] = id;

                    DisplayQuesAnsComments(id.Value);
                    ViewBag.operation = 1;
                    ViewBag.checkCrontroller = 1;
                    return View("_PostAnswer");
                }


            }
            else
            {
                if (id == null)
                {

                    ViewData["c"] = 5;
                    UpdateTempQuesId(tempId);
                    DisplayQuesAnsComments(tempId);
                    ViewBag.operation = 1;
                    ViewBag.checkCrontroller = 1;
                }
                ViewData["c"] = 5;
                UpdateTempQuesId(id.Value);
                DisplayQuesAnsComments(id.Value);
                ViewBag.operation = 1;
                ViewBag.checkCrontroller = 1;
                TempData["AreaFQAnsPostController"] = 1;
                return View("_PostAnswer");
            }




            return View("_PostAnswer");

        }

        private void DisplayQuesAnsComments(int id)
        {
            ViewBag.question = _db.Questions.FirstOrDefault(m => m.QuestionId.Equals(id)).Question;

            DisplayQuesPostUserInfo(id);
            ViewData["GetAns"] = (from uu in _db.ForumUser
                                  from an in _db.Answers
                                  where uu.UserId == an.UserId && an.QuestionId.Equals(id)
                                  select new AnsPostedUser() { AnsId = an.AnswerId, Answer = an.Answer, Username = uu.UserName, PostedDate = an.AnsweredTimeDate, Image = uu.Image, Comments = an.Comments }).ToList();



            UpdateTotViews(id);
            ViewData["totalAns"] = _db.Answers.Where(x => x.QuestionId.Equals(id)).Count();
        }

        private void DisplayQuesPostUserInfo(int id)
        {
            int quesUserId = _db.Questions.SingleOrDefault(x => x.QuestionId.Equals(id)).UserId;
            ViewBag.quesuserImg = _db.ForumUser.Single(x => x.UserId.Equals(quesUserId)).Image;
            ViewBag.userName = _db.ForumUser.Single(x => x.UserId.Equals(quesUserId)).UserName;
            ViewBag.PostDate = _db.Questions.Single(x => x.QuestionId.Equals(id)).PostedDate;
        }


        private void UpdateTotViews(int id)
        {
            int totAns = _db.Questions.SingleOrDefault(x => x.QuestionId.Equals(id)).views;
            question = _db.Questions.Single(m => m.QuestionId.Equals(id));
            question.views = totAns + 1;
            _db.Entry(question).State = EntityState.Modified;
            _db.SaveChanges();


        }

        public ActionResult PostComment(int id = 0, int qUesId = 0)
        {

            int ansId = Convert.ToInt32(TempData["AreaFQAAnsId"]);
            TempData["AreaFQAAnsTempId"] = Convert.ToInt32(ansId);
            ViewData["op"] = 2;
            TempData["AnsId"] = id;
            TempData["quId"] = qUesId;
            ViewBag.ansId = id;
            ViewBag.quId = qUesId;
            if (id != 0)
            {
                UpdateTemId(id);
                DisplayAnswerAndPostedUserInfo(id);
                DisplayComments(id);
            }

            else
            {

                int temId = _db.Temp.Single(x => x.Id.Equals(1)).TempId.Value;
                DisplayAnswerAndPostedUserInfo(temId);
                DisplayComments(temId);

            }
            return View("Comments");
        }
        [HttpPost]
        public ActionResult PostComment(RatingViewModel mo, RatingViewModel model, string msg, int? id)
        {
            ViewData["op"] = 2;

            Comment comment = new Comment();
            TempData["AreaFQAC"] = id;

          
            if (Request.IsAuthenticated)
            {
                if (id != null)
                {
                    int userId = _db.ForumUser.SingleOrDefault(m => m.UserName.Equals(User.Identity.Name)).UserId;




                    if (msg != "")
                    {
                        comment.CommentMsg = msg;
                        comment.UserId = userId;
                        comment.AnswerId = id.Value;
                        comment.PostedDateTime = DateTime.Now;
                        _db.Comment.Add(comment);
                        _db.SaveChanges();
                        Response.Write("<div id='op'>");
                        Response.Write("</div>");
                        UpdateTotalComments(id.Value);
                        DisplayAnswerAndPostedUserInfo(id.Value);
                        DisplayComments(id.Value);

                        //return View("Comments");
                    }

                    else
                    {
                        DisplayAnswerAndPostedUserInfo(id.Value);
                        DisplayComments(id.Value);
                        ViewData["op"] = 2;
                        ViewData["ReqMsg"] = 2;
                        ViewBag.errormsg = "* You must Write Something! to Post  comment";
                        return View("Comments");
                    }

                }
                else
                {

                    int temId = _db.Temp.Single(x => x.Id.Equals(1)).Id;
                    if (msg != "")
                    {

                        int useId = _db.ForumUser.SingleOrDefault(m => m.UserName.Equals(User.Identity.Name)).UserId;
                        comment.CommentMsg = msg;
                        comment.UserId = useId;
                        comment.AnswerId = temId;
                        comment.PostedDateTime = DateTime.Now;
                        _db.Comment.Add(comment);
                        _db.SaveChanges();
                        UpdateTotalComments(temId);
                        DisplayAnswerAndPostedUserInfo(temId);
                        DisplayComments(temId);
                        //return View("Comments");
                    }
                    else
                    {
                        DisplayAnswerAndPostedUserInfo(temId);
                        DisplayComments(temId);
                        ViewData["op"] = 2;
                        ViewData["ReqMsg"] = 2;
                        ViewBag.errormsg = "* You must Write Something! to Post  comment";
                        return View("Comments");
                    }
                }



            }
            else
            {
                DisplayAnswerAndPostedUserInfo(id.Value);
                DisplayComments(id.Value);
                ViewData["op"] = 2;
                ViewData["ReqMsg"] = 1;
                ViewData["c"] = 6;

                return View("Comments");


            }

            return View("Comments");

        }

        private void UpdateTempQuesId(int id)
        {


            TemQuesIdModel temp = db.TemQuesIdModels.Single(m => m.Id.Equals(1));
            temp.TempQuesId = id;
            db.Entry(temp).State = EntityState.Modified;
            db.SaveChanges();
        }
        private void UpdateTotalComments(int id)
        {
            int totCom = _db.Comment.Where(m => m.AnswerId.Equals(id)).Count();
            Answers answer = _db.Answers.Single(x => x.AnswerId.Equals(id));
            answer.Comments = totCom;
            _db.Entry(answer).State = EntityState.Modified;
            _db.SaveChanges();
        }



        public ActionResult GetComments(int id = 0)
        {

            ViewBag.operation = 2;
            DisplayAnswerAndPostedUserInfo(id);
            DisplayComments(id);
            return View("Comments");
        }

        private void DisplayComments(int id)
        {

            ViewData["Comments"] = (from c in _db.Comment
                                    join u in _db.ForumUser on c.UserId equals u.UserId
                                    where c.AnswerId.Equals(id)
                                    select new UserComment() { CommentMsg = c.CommentMsg, Username = u.UserName, Image = u.Image, PostedDate = c.PostedDateTime }).ToList();

            int sum = _db.Rating.Where(m => m.AnswerId.Equals(id)).Sum(m => m.RateScore);
            int totRater = _db.Rating.Where(m => m.AnswerId.Equals(id)).Count();
            double avgRating = (double)sum / totRater;

            ViewBag.avgRat = sum / totRater;


        }

        private void DisplayAnswerAndPostedUserInfo(int id)
        {

            ViewData["Query"] = (from a in _db.Answers
                                 join u in _db.ForumUser on a.UserId equals u.UserId
                                 where a.AnswerId.Equals(id)
                                 select new AnsPostedUser() { Image = u.Image, Username = u.UserName, PostedDate = a.AnsweredTimeDate, Answer = a.Answer });
            ViewBag.totalComments = _db.Comment.Where(m => m.AnswerId.Equals(id)).Count();

        }
    }
}
