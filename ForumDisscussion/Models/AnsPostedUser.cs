using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace ForumDisscussion.Models
{
    public class AnsPostedUser
    {   
        
        
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Username { get; set; }
        public byte[] Image { get; set; }
        //[DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime PostedDate { get; set; }
        public int AnsId { get; set; }
        public int QuesId { get; set; }
        public int? Comments { get; set; }
        public DateTime QuesPostedDate { get; set; }
        public string Category { get; set; }
        public int NumOfReplies { get; set; }
        public double RatingScore { get; set; }
    }
    //public class UnsweredQuestionInformation:AnsPostedUser
    //{
    //    public int QuestionId { get; set; }
    //}
    public class AnsweredQuestionInformation : AnsPostedUser
    {
        public int QuestionId { get; set; }
    }
    public class UnAnsweredQuestionInformation : AnsweredQuestionInformation
    {
        public int QuestionId { get; set; }
    }
}