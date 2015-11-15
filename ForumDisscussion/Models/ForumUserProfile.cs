using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumDisscussion.Models
{
    public class ForumUserProfile
    
    {
        [StringLength(16, MinimumLength = 4, ErrorMessage = "Length must be between 4 and 16")]
        [DisplayName("FirstName :")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]
        [Required(ErrorMessage = "* Firstname Required")]
        public string Fname { get; set; }

        [StringLength(16, MinimumLength = 4, ErrorMessage = "Length must be between 4 and 16")]
        [DisplayName("LastName :")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]
        [Required(ErrorMessage = "* Lastname Required")]
        public string Lname { get; set; }

        [Required(ErrorMessage = "Email is Required!")]
        [DisplayName("Email :")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter a valid Email")]

        public string EmailId { get; set; }

        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [MinLength(6, ErrorMessage = "New Password's minimum length is 6")]
        [MaxLength(30, ErrorMessage = "New Password's maximum length is 30")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword",ErrorMessage = "Password don't match.")]
        public string ConfirmNewPassword { get; set; }
    }
  
}