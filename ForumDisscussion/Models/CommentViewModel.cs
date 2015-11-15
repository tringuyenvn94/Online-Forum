using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumDisscussion.Models
{
    public class CommentViewModel
    {
       
        [Required(ErrorMessage = "* Please Select a category")]
        [DisplayName("Category")]
        public string Category { get; set; }

        [Required(ErrorMessage = "* Required !")]
        [DisplayName("CommentMsg")]
        [MinLength(10, ErrorMessage = "Comment should not contain less than 10 chracters.")]

        public string CommentMsg { get; set; }
    }

    public class QuestionPostModel
    {
        
        [Required(ErrorMessage = "* Please Select a category")]
        [DisplayName("Category")]

        public string Category { get; set; }


        [Required(ErrorMessage = "* Required !")]
        [DisplayName("CommentMsg")]
        [MinLength(10, ErrorMessage = "Quesion should not contain less than 10 chracters.")]

        public string QuestionMessage { get; set; }
    }
}