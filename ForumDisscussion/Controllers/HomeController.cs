using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ForumDisscussion.Models;
namespace ForumDisscussion.Controllers
{
    public class HomeController : Controller
    {
        readonly AdminDbContext _d = new AdminDbContext();
   
        private readonly ForumDataBaseConnectionContext _db = new ForumDataBaseConnectionContext();

        Questions _question = new Questions();
        readonly Comment _comment = new Comment();
        public ActionResult GetAllAnsweredQuestion() {
       
            return View();
        }
        public ActionResult GetAllUnAnsweredQuestion()
        {

            return View();
        }
        public ActionResult Edit(int ansId)
        {
            var answer = _db.Answers.SingleOrDefault(m => m.AnswerId.Equals(ansId));
            if (answer!=null)
            {
             
            }
            var u = _db.Answers.SingleOrDefault(m => m.AnswerId.Equals(ansId));
            if (u == null) return View();
            var mo = new EditViwModel
            {
                
                Reply = u.Answer

            };
       
            ViewBag.ansId = ansId;





            return View(mo);
        }
        [HttpPost]
        public ActionResult Edit(EditViwModel e, int? ansId)
        {
            if (!ModelState.IsValid) return View();
            if (ansId != null)
            {
                var a = _db.Answers.Find(ansId.Value);
                a.Answer = e.Reply;
                _db.Entry(a).State = EntityState.Modified;
            }
            _db.SaveChanges();

            return RedirectToAction("PostComment", "Home", new { id = ansId });
        }



        public JsonResult GetCustomer()
        {

            var data = (from q in _db.Questions
                        join c in _db.Category on q.CategoryId equals c.CategoryId
                        join u in _db.ForumUser on q.UserId equals u.UserId into uu
                        from us in uu.DefaultIfEmpty()


                        select new QuestionList()
                        {
                            Id = q.QuestionId,
                            Question = q.Question.Remove(60),
                            Category = c.CategoryName,
                            PostedBy = us.UserName,
                            Answers = q.answers,
                            Views = q.views,
                            Posteddate = q.PostedDate,



                        }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public List<QuestionList> GetQuestions()
        {


            var data = (from q in _db.Questions
                        join c in _db.Category on q.CategoryId equals c.CategoryId
                        join u in _db.ForumUser on q.UserId equals u.UserId into uu
                        from us in uu.DefaultIfEmpty()

                        
                        select new QuestionList()
                        {
                            Id = q.QuestionId,
                            Question = q.Question.Remove(60),
                            Category = c.CategoryName,
                            PostedBy = us.UserName,
                            Answers = q.answers,
                            Views = q.views,
                            Posteddate = q.PostedDate,

                            Image = us.Image

                        }).ToList();

            return data;
        }
        public JsonResult DisplayCat(string searchByCat)
        {
            ViewBag.list = _db.Category.Select(m => m.CategoryName);


            return Json(ViewBag.list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index( )
        {


            ViewData["H"] = 1;

            return View(GetQuestions().ToList());


        }


        [HttpPost]
        public ActionResult Index(string searchName, int id = 0)
        {
            ViewData["H"] = 1;

            if (searchName != "") return View( GetQuestions().Where(m => m.Category.Equals(searchName)).ToList());
            ViewData["reqErrorMsg"] = 11;

            ViewData["c"] = 1;
           return View(GetQuestions().ToList());
        }

  public ActionResult DeleteReply(int id=0)
  {
      var redirectId =0;
      try
      {
          var currentAnsPostedUserId = _db.Answers.Find(id).UserId;
          var v = _db.PostInformation.SingleOrDefault(m => m.UserId.Equals(currentAnsPostedUserId));
          if (v != null)
          {
              var postId = v.PostId;
              var totalAnsPost = _db.PostInformation.Find(postId).TotalAnsPost;
              if (totalAnsPost != null)
              {
                  var prevTotalReply = totalAnsPost.Value;
                  var p = _db.PostInformation.Find(postId);
                  p.TotalAnsPost = prevTotalReply - 1;
                  _db.Entry(p).State = EntityState.Modified;
                  _db.SaveChanges();

                  var u = _db.Answers.SingleOrDefault(x => x.AnswerId.Equals(id));
                  if (u != null)
                  {
                      var quesId = u.QuestionId;
                      var prevTotalReplies = _db.Questions.Find(quesId).answers;
                      var q = _db.Questions.Find(quesId);
                      q.answers = prevTotalReplies - 1;
                      _db.Entry(q).State = EntityState.Modified;
                  }
                  _db.SaveChanges();

                  var userId = _db.Comment.Where(m => m.AnswerId.Equals(id)).Select(m => m.UserId);
                  var userIdList=new List<int>();
                  foreach (var i in userId.Where(i => !userIdList.Contains(i)))
                  {
                      userIdList.Add(i);
                  }
                  foreach (var i in userIdList)
                  {
                      var totCommtForEach = _db.Comment.Where(m => m.AnswerId.Equals(id)&&m.UserId.Equals(i)).Select(m => m.UserId).Count();

                      var n = _db.PostInformation.SingleOrDefault(m => m.UserId.Equals(i));
                      if (n != null)
                      {
                          if (v.TotalCommentPost != null)
                          {
                              var prevTotalCommnt = v.TotalCommentPost.Value;

                              var postdId = v.PostId;

                              p = _db.PostInformation.Find(postdId);
                              p.TotalCommentPost = prevTotalCommnt - totCommtForEach;
                          }
                      }

                      _db.Entry(p).State = EntityState.Modified;
                      _db.SaveChanges();
           
                  }
              }
          }
          var c = _db.Answers.SingleOrDefault(m => m.AnswerId.Equals(id));
          if (c != null)
          {
               redirectId+= c.QuestionId;
          }
          var a = _db.Answers.Find(id);
          _db.Entry(a).State = EntityState.Deleted;
          _db.SaveChanges();
          return RedirectToAction("PostAnswer", "Home", new { id = redirectId });
      }
      catch (Exception e)
      {
          Response.Write(e.Message);
       
      }
      return RedirectToAction("PostAnswer","Home");
  }

        public ActionResult PostAnswer(int id = 0)
        {

        
            try
            {
                
             
              
                TempData["FetchQuesIDToLogC"] = id;

              
                  
                    if (Request.IsAuthenticated)
                    {
                        try {
                           
                            var findAuthenticatedUser= _d.Admin.SingleOrDefault(m => m.Email.Equals(User.Identity.Name));

                            if (findAuthenticatedUser == null)
                            {
                                var u = _db.ForumUser.SingleOrDefault(m => m.UserName.Equals(User.Identity.Name));
                                if (u != null)
                                {
                                    var authuserId = u.UserId;

                                    var v = _db.Questions.SingleOrDefault(m => m.QuestionId.Equals(id));
                                    if (v != null)
                                    {
                                        var currentQuestionPostUserId = v.UserId;
                          
                                        if (authuserId == currentQuestionPostUserId)
                                        {
                                            ViewBag.postButtonHideId = 1;

                                        }
                                    }
                                    var chechReply = _db.Answers.SingleOrDefault(m => m.UserId.Equals(authuserId) && m.QuestionId.Equals(id));
                                    if (chechReply != null)
                                    {
                                        ViewBag.postButtonHideId = 1;
                                    }
                                }
                            }
                            else {
                                ViewBag.postButtonHideId = 1;
                            }
                        }
                        catch(Exception e)
                        {
                          
                        }
                     
                    }

                 
                  


              

              
                TempData["QiComm"] = id;
               
                DisplayQuesAnsComments(id);
                ViewBag.operation = 2;
                ViewBag.checkCrontroller = 2;
            }
            catch (Exception e)
            {
              

            }

            return View("_PostAnswer");



        }

      

     


        private void DisplayQuesAnsComments(int id)
        {
            var u
                = _db.Questions.FirstOrDefault(m => m.QuestionId.Equals(id));
            if (u != null)
                ViewBag.question = u.Question;

            DisplayQuesPostUserInfo(id);

            ViewData["GetAns"] = (from uu in _db.ForumUser
                                  from an in _db.Answers

                                  where uu.UserId == an.UserId && an.QuestionId.Equals(id)
                                  select new AnsPostedUser() { AnsId = an.AnswerId, Answer = an.Answer, Username = uu.UserName, PostedDate = an.AnsweredTimeDate, Image = uu.Image, RatingScore = an.Rating.Value, Comments = an.Comments }).OrderByDescending(m => m.RatingScore).ToList();



            UpdateTotViews(id);
            ViewData["totalAns"] = _db.Answers.Count(x => x.QuestionId.Equals(id));
        }

        private void UpdateAns(int id)
        {
            var u = _db.Questions.SingleOrDefault(x => x.QuestionId.Equals(id));
            if (u != null)
            {
                var totAns = u.answers;
                _question = _db.Questions.Single(m => m.QuestionId.Equals(id));
                _question.answers = totAns + 1;
            }
            _db.Entry(_question).State = EntityState.Modified;
            _db.SaveChanges();


        }

        private void DisplayQuesPostUserInfo(int id)
        {
            var u = _db.Questions.SingleOrDefault(x => x.QuestionId.Equals(id));
            if (u != null)
            {
                var quesUserId = u.UserId;
                ViewBag.quesuserImg = _db.ForumUser.Single(x => x.UserId.Equals(quesUserId)).Image;
                ViewBag.userName = _db.ForumUser.Single(x => x.UserId.Equals(quesUserId)).UserName;
            }
            ViewBag.PostDate = _db.Questions.Single(x => x.QuestionId.Equals(id)).PostedDate;


        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PostAnswer(int? id, string msg)
        {
            if (id != null)
            {
                ViewBag.id = id.Value;
                if (Request.IsAuthenticated)
                {
                    var authuserId = _db.ForumUser.Single(m => m.UserName.Equals(User.Identity.Name)).UserId;
                   
          

                    if (msg != "")
                    {
                        if (msg.Length >= 10)
                        {
                            var ms = "";
                            var wdb = new WordDbContext();
                  
                            var sensitiveWods = wdb.Words.Select(m => m.SensitiveWord);
                            var strList = new List<SensitiveWordsViewModel>();

                            var convt = msg.ToLower();
                            foreach (var str in sensitiveWods)
                            {
                                if (!convt.Contains(str.ToLower())) continue;
                                var sensitive = new SensitiveWordsViewModel {SensitiveWords = str};
                                strList.Add(sensitive);
                                ViewBag.senWordsMsg = 11;

                                ms = str;
                            }



                            if (ms == "")
                            {

                                var checKIsReplyExist = _db.Answers.Any(m => m.UserId.Equals(authuserId) && m.QuestionId.Equals(id.Value));
                                if (!checKIsReplyExist)
                                {
                                    var ans = new Answers
                                    {
                                        UserId = authuserId,
                                        AnsweredTimeDate = DateTime.Now,
                                        QuestionId = id.Value,
                                        Answer = msg,
                                        Rating = 0
                                    };

                                    _db.Answers.Add(ans);
                                    _db.SaveChanges();

                                    var u
                                        = _db.PostInformation.SingleOrDefault(m => m.UserId.Equals(authuserId));
                                    if (u != null)
                                    {
                                        var posId = u.PostId;
                                        var v = _db.PostInformation.Find(posId).TotalAnsPost;
                                        if (v != null)
                                        {
                                            var prevPosts = v.Value;
                                            var pi = _db.PostInformation.Find(posId);


                                            pi.TotalAnsPost = prevPosts + 1;

                                            _db.Entry(pi).State = EntityState.Modified;
                                        }
                                    }
                                    _db.SaveChanges();
                                    ViewData["c"] = 4;
                                    ViewBag.operation = 2;
                                    ViewBag.checkCrontroller = 2;
                                    UpdateAns(id.Value);
                                    DisplayQuesAnsComments(id.Value);
                                }
                                else
                                {

                                    ViewData["PostNotAllowedId"] = 1;
                                }
                            }

                            TempData["QiComm"] = id;
                        

                            DisplayQuesAnsComments(id.Value);
                            ViewBag.operation = 2;
                            ViewBag.checkCrontroller = 2;
                            IEnumerable<SensitiveWordsViewModel> s = strList;
                            ViewData["s"] = s;

                        }
                        else
                        {

                            TempData["QiComm"] = id;


                            DisplayQuesAnsComments(id.Value);
                            ViewBag.operation = 2;
                            ViewBag.checkCrontroller = 2;
                            ViewData["ReqMsg"] = 3;
                            ViewData["Mess"] = "* Answers should not contain less than 10 chracters";

                        }
                    }
                    else
                    {
                        TempData["AnsId"] = id.Value;
                        DisplayQuesAnsComments(id.Value);
                        ViewBag.operation = 2;
                        ViewBag.checkCrontroller = 2;
                        ViewData["ReqMsg"] = 3;
                        ViewData["Mess"] = "*  Empty Field not allowed!.Please Write Your Answer";
                    }


             
                    return View("_PostAnswer");
                


                }
                else
                {

                
             
               
                    DisplayQuesAnsComments(id.Value);
             

                    ViewData["c"] = 5;
              
                    ViewBag.operation = 2;
                    ViewBag.checkCrontroller = 2;
                    TempData["HomeAnsPostController"] = 2;




                }


                TempData["QiComm"] = id;
                TempData["AnsId"] = id;

                DisplayQuesAnsComments(id.Value);
            }
            ViewBag.operation = 2;
            ViewBag.checkCrontroller = 2;
            return View("_PostAnswer");





        }




        public ActionResult RedirectToLogin()
        {
            return RedirectToAction("Login", "Account");
        }



        private void UpdateTotViews(int id)
        {
            var u = _db.Questions.SingleOrDefault(x => x.QuestionId.Equals(id));
            if (u != null)
            {
                var totAns = u.views;
                _question = _db.Questions.Single(m => m.QuestionId.Equals(id));
                _question.views = totAns + 1;
            }
            _db.Entry(_question).State = EntityState.Modified;
            _db.SaveChanges();


        }







        public ActionResult GetComments(int id = 0)
        {

            ViewBag.operation = 1;

            return View("Comments");
        }

        public ActionResult PostComment(int id = 0)
        {

            ViewBag.ansId = id;
            var u = _db.Answers.FirstOrDefault(m => m.AnswerId.Equals(id));
            if (u != null)
                ViewBag.quesTionid = u.QuestionId;

            ViewBag.id = id;

            try
            {



           
             
                ViewData["op"] = 1;
           
                    if (Request.IsAuthenticated)
                    {
                        var checkAuthUserId = _db.ForumUser.SingleOrDefault(m => m.UserName.Equals(User.Identity.Name));
                        if (checkAuthUserId != null)
                        {
                            var v = _db.ForumUser.SingleOrDefault(m => m.UserName.Equals(User.Identity.Name));
                            if (v != null)
                            {
                                var authUserId = v.UserId;
                                var t = _db.Answers.SingleOrDefault(m => m.AnswerId.Equals(id));
                                if (t != null)
                                {
                                    if (u != null)
                                    {
                                        var thisAnsUserId = u.UserId;
                                        if (authUserId == thisAnsUserId)
                                        {
                                            ViewData["EnableEdit"] = 1;
                                        }
                                    }
                                }
                            }
                           
                        }

                    }


                   

                  
                    DisplayAnswerAndPostedUserInfo(id);
                    DisplayComments(id);




                    var totalScore = _db.Rating.Where(m => m.AnswerId.Equals(id)).Sum(m => m.RateScore);
                    var totalRater = _db.Rating.Count(m => m.AnswerId.Equals(id));
                    var avgScore = (double)totalScore / totalRater;
                ViewBag.totalRater = totalRater;
                ViewData["raiting"] = avgScore;
                ViewBag.tempId = 1;
            }
            catch (Exception e)
            {

                
            }

            ViewBag.currentAnsId = id;
            return View("Comments");
        }

        [HttpPost]
        public ActionResult PostComment(string msg, RatingViewModel model, int? id)
        {
            ViewBag.ansId = id;
            var v = _db.Answers.FirstOrDefault(m => m.AnswerId.Equals(id.Value));
            if (v != null)
                ViewBag.quesTionid = v.QuestionId;

            ViewBag.id = id;
           

            TempData["HPostController"] = 7;


            var strList = new List<SensitiveWordsViewModel>();
            var wdb = new WordDbContext();


            if (Request.IsAuthenticated)
            {
                var u = _db.ForumUser.SingleOrDefault(m => m.UserName.Equals(User.Identity.Name));
                if (u != null)
                {
                    var userId = u.UserId;
             



       
                    if (true)
                    {
                           
                        var authUserId =
                            u.UserId;
                        var w = _db.Answers.SingleOrDefault(m => m.AnswerId.Equals(id.Value));
                        if (w != null)
                        {
                            var thisAnsUserId = w.UserId;
                            if (authUserId == thisAnsUserId)
                            {
                                ViewData["EnableEdit"] = 1;
                            }
                        }


                        if (msg != "")
                        {


                            var ms = "";
                       
                            var sensitiveWods = wdb.Words.Select(m => m.SensitiveWord);
                    

                            var convt = msg.ToLower();
                            foreach (var str in sensitiveWods)
                            {
                                if (!convt.Contains(str.ToLower())) continue;
                                var sensitive = new SensitiveWordsViewModel {SensitiveWords = str};
                                strList.Add(sensitive);
                                ViewBag.senWordsMsg = 11;

                                ms = str;
                            }

                            IEnumerable<SensitiveWordsViewModel> s = strList;
                            ViewData["s"] = s;

                            ViewData["op"] = 1;
                            if (ms == "")
                            {
                               

                                _comment.CommentMsg = msg;
                                _comment.UserId = userId;
                                if (id != null)
                                {
                                    _comment.AnswerId = id.Value;
                                    _comment.PostedDateTime = DateTime.Now;
                                    _db.Comment.Add(_comment);
                                    _db.SaveChanges();
                                    var t = _db.PostInformation.SingleOrDefault(m => m.UserId.Equals(userId));
                                    if (t != null)
                                    {
                                        var posId = t.PostId;
                                        var totalCommentPost = _db.PostInformation.Find(posId).TotalCommentPost;
                                        if (totalCommentPost != null)
                                        {
                                            var prevPosts = totalCommentPost.Value;
                                            var pi = _db.PostInformation.Find(posId);


                                            pi.TotalCommentPost = prevPosts + 1;

                                            _db.Entry(pi).State = EntityState.Modified;
                                        }
                                    }
                                    _db.SaveChanges();
                                    ViewBag.emptyBox = 1;
                                    UpdateTotalComments(id.Value);
                                    DisplayAnswerAndPostedUserInfo(id.Value);
                                    DisplayComments(id.Value);
                                }
                            }
                            if (id != null)
                            {
                                DisplayAnswerAndPostedUserInfo(id.Value);
                                DisplayComments(id.Value);
                            }
                        }
                        else
                        {
                            if (id != null)
                            {
                                DisplayAnswerAndPostedUserInfo(id.Value);
                                DisplayComments(id.Value);
                            }
                            ViewData["op"] = 1;
                            ViewData["ReqMsg"] = 1;
                            ViewBag.errormsg = "* You must Write Something! to Post  comment";

                            //return View("Comments");
                        }


                    }
                    
                }
            }
            else
            {
                if (id != null)
                {
                    DisplayAnswerAndPostedUserInfo(id.Value);
                    DisplayComments(id.Value);
                }
                ViewData["op"] = 1;
                ViewData["ReqMsg"] = 1;
                ViewData["c"] = 5;

                //return View("Comments");

            }




            try
            {


                var totalScore = _db.Rating.Where(m => m.AnswerId.Equals(id.Value)).Sum(m => m.RateScore);
                var totalRater = _db.Rating.Count(m => m.AnswerId.Equals(id.Value));
                var avgScore = (double)totalScore / totalRater;
                ViewData["raiting"] = avgScore;
            }
            catch (Exception e) { }



           

          


            return View("Comments",model);
        }

        public ActionResult DeleteComment(int id, int answerId)
        {
          
                try
                {
                    var userId = _db.Comment.Find(id).UserId;
                    var v = _db.PostInformation.SingleOrDefault(u => u.UserId.Equals(userId));
                    if (v != null)
                    {
                        if (v.TotalCommentPost != null)
                        {
                            var prevCommnt = v.TotalCommentPost.Value;
                            var postId = v.PostId;

                            var p = _db.PostInformation.Find(postId);
                            p.TotalCommentPost = prevCommnt - 1;
                            _db.Entry(p).State = EntityState.Modified;
                        }
                    }
                    _db.SaveChanges();

                    var t = _db.Comment.SingleOrDefault(m => m.CommentId.Equals(id));
                    if (t != null)
                    {
                        var ansId = t.AnswerId;
                        var comments = _db.Answers.Find(ansId).Comments;
                        if (comments != null)
                        {
                            var prevCommmnt = comments.Value;
                            var a = _db.Answers.Find(ansId);
                            a.Comments = prevCommmnt - 1;
                            _db.Entry(a).State = EntityState.Modified;
                        }
                    }
                    _db.SaveChanges();

                    var c = _db.Comment.Find(id);
                    _db.Entry(c).State = EntityState.Deleted;
                    _db.SaveChanges();
                }
                catch (Exception e)
                {
                    
                    
                }


                return RedirectToAction("PostComment", new { id = answerId });
        }

        private void UpdateTotalComments(int id)
        {
            var totCom = _db.Comment.Count(m => m.AnswerId.Equals(id));
            var answer = _db.Answers.Single(x => x.AnswerId.Equals(id));
            answer.Comments = totCom;
            _db.Entry(answer).State = EntityState.Modified;
            _db.SaveChanges();
        }

        private void DisplayComments(int id)
        {
            try
            {
                ViewData["Comments"] = (from c in _db.Comment
                                        join u in _db.ForumUser on c.UserId equals u.UserId
                                        where c.AnswerId.Equals(id)
                                        select new UserComment() {CommentId = c.CommentId,CommentMsg = c.CommentMsg, Username = u.UserName, Image = u.Image, PostedDate = c.PostedDateTime }).ToList();



            }
            catch (Exception e)
            {
                Response.Write(e.Message);

            }


        }

        public ActionResult Help()
        {

            return View();
        }

        private void DisplayAnswerAndPostedUserInfo(int id)
        {
            ViewData["Query"] = (from a in _db.Answers
                                 join u in _db.ForumUser on a.UserId equals u.UserId
                                 where a.AnswerId.Equals(id)
                                 select new AnsPostedUser() { AnsId = a.AnswerId, Image = u.Image, Username = u.UserName, PostedDate = a.AnsweredTimeDate, Answer = a.Answer });
            ViewBag.totalComments = _db.Comment.Count(m => m.AnswerId.Equals(id));


        }


        public ActionResult DeleteUserQuestion(int id)
        {

            try
            {
                var replyPostedUserIdList = new List<int>();
                var commentIdList = new List<int>();

                var commentPostedUserIdList = new List<int>();
                var commentIdListToDelComments = new List<int>();
                var asnId = _db.Answers.Where(m => m.QuestionId.Equals(id)).Select(m => m.AnswerId);

                foreach (var i in asnId)
                {

                    var g = _db.Answers.Where(x => x.AnswerId.Equals(i)).Select(x => x.UserId);

                    var c = _db.Comment.Where(x => x.AnswerId.Equals(i)).Select(x => x.CommentId);

                    foreach (var i1 in g.Where(i1 => !replyPostedUserIdList.Contains(i1)))
                    {
                        replyPostedUserIdList.Add(i1);
                    }
                    commentIdList.AddRange(c);
                }
                foreach (var i1 in commentIdList.Select(i => _db.Comment.Where(x => x.CommentId.Equals(i)).Select(x => x.UserId)).SelectMany(g => g.Where(i1 => !commentPostedUserIdList.Contains(i1))))
                {
                    commentPostedUserIdList.Add(i1);
                }
                foreach (var i in replyPostedUserIdList)
                {

                    var currentDeletedReply = _db.Answers.Where(m => m.UserId.Equals(i) && m.QuestionId.Equals(id)).Select(m => m.UserId).Count();


                    var c = _db.PostInformation.SingleOrDefault(x => x.UserId.Equals(i));
                    if (c != null)
                    {
                        if (c.TotalAnsPost != null)
                        {
                            var prevToRepy = c.TotalAnsPost.Value;
                            var k = c;
                            {
                                var postId = k.PostId;
                                var p = _db.PostInformation.Find(postId);

                                p.TotalAnsPost = prevToRepy - currentDeletedReply;
                                _db.Entry(p).State = EntityState.Modified;
                            }
                        }
                    }

                    _db.SaveChanges();

                }


                foreach (var i in commentPostedUserIdList)
                {



                    foreach (var i1 in asnId)
                    {
                        var currentDeletedCommnt = _db.Comment.Where(m => m.UserId.Equals(i) && m.AnswerId.Equals(i1)).Select(m => m.UserId).Count();
                        if (currentDeletedCommnt == 0) continue;
                        var u = _db.PostInformation.SingleOrDefault(x => x.UserId.Equals(i));
                        if (u == null) continue;
                        if (u.TotalCommentPost == null) continue;
                        var prevToComm = u.TotalCommentPost.Value;
                        var postId = u.PostId;
                        var p = _db.PostInformation.Find(postId);
                        p.TotalCommentPost = prevToComm - currentDeletedCommnt;


                        _db.Entry(p).State = EntityState.Modified;
                    }

                    _db.SaveChanges();
                }


                var t = _db.Questions.SingleOrDefault(m => m.QuestionId.Equals(id));
                if (t != null)
                {
                    var deletedQuesUserId = t.UserId;
                    var u = _db.PostInformation.SingleOrDefault(x => x.UserId.Equals(deletedQuesUserId));
                    if (u != null)
                    {
                        if (u.TotalQuesPost != null)
                        {
                            var prevToQuestion = u.TotalQuesPost.Value;

                            var postdId = u.PostId;
                            var po = _db.PostInformation.Find(postdId);
                            po.TotalQuesPost = prevToQuestion - 1;


                            _db.Entry(po).State = EntityState.Modified;
                        }
                    }
                }

                _db.SaveChanges();
                var ansIdToDelete = _db.Answers.Where(m => m.QuestionId.Equals(id)).Select(m => m.AnswerId);
                foreach (var i in ansIdToDelete)
                {

                    var commentIdToDelete = _db.Comment.Where(m => m.AnswerId.Equals(i)).Select(m => m.CommentId);

                    commentIdListToDelComments.AddRange(commentIdToDelete);
                    var a = _db.Answers.Find(i);
                    _db.Entry(a).State = EntityState.Deleted;



                }
                _db.SaveChanges();
                foreach (var j in commentIdListToDelComments)
                {
                    var c = _db.Comment.Find(j);
                    _db.Entry(c).State = EntityState.Deleted;
                    _db.SaveChanges();
                }




                var q = _db.Questions.Find(id);
                _db.Entry(q).State = EntityState.Deleted;
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Response.Write(e.Message);

            }

           
           return RedirectToAction("Index", "Home");
        }

        public JsonResult RatePost(int score,int ansId)
        {

            var message = "";
            if (Request.IsAuthenticated)
            {
                var getAnsPostedUserId = _db.Answers.Single(m => m.AnswerId.Equals(ansId)).UserId;
                var authUserId = _db.ForumUser.Single(m => m.UserName.Equals(User.Identity.Name)).UserId;
                if (getAnsPostedUserId != authUserId)
                {
                    var checkIsUserRatedThisAns = _db.Rating.Any(m => m.RaterId.Equals(authUserId));
                    if (!checkIsUserRatedThisAns)
                    {
                        var rating = new Rating
                        {
                            AnswerId = ansId,
                            RaterId = authUserId,
                            RateScore = score,
                            RatedDateTime = DateTime.Now
                        };
                        _db.Rating.Add(rating);
                        _db.SaveChanges();
                        message = "Successfully rated.";
                    }
                    else
                    {
                        message = "You already rated this answer.";
                    }
                }
                else
                {
                    message = "You cann't rate as this was posted by you.";

                }

            }
            else
            {
                message = "Please Login to rate";
            }


           
             
                return Json(new {result = message});

        }
    }
}
