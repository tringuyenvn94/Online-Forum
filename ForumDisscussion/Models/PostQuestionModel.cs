using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumDisscussion.Models
{
    public class PostQuestionModel
    {
        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Category")]
        public string Category { get; set; }
        [Required(ErrorMessage = "* Requred")]
        [Display(Name = "Question")]
        public string Question { get; set; }
    }
}