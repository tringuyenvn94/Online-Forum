using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumDisscussion.Models
{
    public class UnAnsweredQuesListModel
    {
        public int Id { get; set; }
        //   [DataType(DataType.Url)]
        public int Replies { get; set; }
        public string Question { get; set; }
        public int Views { get; set; }
       public string Answer { get; set; }
        public string Username { get; set; }
        public byte[] Image { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime PostedDate { get; set; }
        public string Category { get; set; }
    }

    public class AnsweredQuestionModel
    {
        public int Id { get; set; }
        //   [DataType(DataType.Url)]
        public int Replies { get; set; }
        public string Question { get; set; }
        public int Views { get; set; }      
        public string Username { get; set; }        
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime PostedDate { get; set; }
        public string Category { get; set; }
        public byte[] Image { get; set; }
    }
}