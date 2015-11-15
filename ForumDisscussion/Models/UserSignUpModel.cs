using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumDisscussion.Models
{
    public class UserSignUpModel
    {
        [Required(ErrorMessage = "FirstName is Required!")]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "Length must be between 4 and 16")]
        [DisplayName("FirstName :")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]

        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is Required!")]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "Length must be between 4 and 16")]
        [DisplayName("LastName :")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]

        public string LastName { get; set; }
        [Remote("CheckIsUsernameExist", "UserSignUp", ErrorMessage = "Username Already Exist")]
        [Required(ErrorMessage = "UserName is Required!")]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "Length must be between 4 and 12")]
        [DisplayName("UserName :")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]

        public string UserName { get; set; }
        [Required(ErrorMessage = "Country is Required!")]
        [DisplayName("Country :")]
        public string Country { get; set; }
        [Remote("CheckIsEmailExist", "UserSignUp", ErrorMessage = "Email Already Exist")]
        [Required(ErrorMessage = "Email is Required!")]
        [DisplayName("Email :")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter a valid Email")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required!")]
        [DisplayName("Password :")]
        [RegularExpression(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Your Password must have a uper case,a lower case letter,a number or Special chrater")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
     
        [Required(ErrorMessage = "ConfirmPassword is Required!")]
        [DisplayName("Password :")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password",ErrorMessage = "Password don't match")] 
        public string COnfirmPassword { get; set; }
        public byte[] Image { get; set; }
        public HttpPostedFileBase File { get; set; }      
   
    }
    
    public class UserLogin
    {
        [Required(ErrorMessage = "UserName is Required!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is Required!")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}