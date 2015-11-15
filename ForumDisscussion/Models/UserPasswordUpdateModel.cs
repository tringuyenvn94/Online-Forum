using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumDisscussion.Models
{
    public class UserPasswordUpdateModel
    {
        [Required(ErrorMessage = "* Required!")]
        [DisplayName("Password :")]
        [RegularExpression(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Your Password must have a uper case,a lower case letter,a number or Special chrater")]
        [DataType(DataType.Password)]

        public string OldPassword { get; set; }
        [Required(ErrorMessage = "* Required!")]
        [DisplayName("New Password :")]
        [RegularExpression(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Your Password must have a uper case,a lower case letter,a number or Special chrater")]
        [DataType(DataType.Password)]

        public string NewPassword { get; set; }

        [Required(ErrorMessage = "* Required!")]
        [DisplayName("Confirm New Password :")]
        [RegularExpression(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Your Password must have a uper case,a lower case letter,a number or Special chrater")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}