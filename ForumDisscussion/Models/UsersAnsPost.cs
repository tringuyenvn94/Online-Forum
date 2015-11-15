using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumDisscussion.Models
{
    public class UsersAnsPost
    {
        [Required(ErrorMessage = "* Required!")]
        public string Answer { get; set; }
    }
}