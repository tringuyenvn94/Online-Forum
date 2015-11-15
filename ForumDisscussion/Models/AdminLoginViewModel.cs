
using System.ComponentModel.DataAnnotations;


namespace ForumDisscussion.Models
{
    public class AdminLoginViewModel
    {
       
        [Required(ErrorMessage = "* Required!")]
        public string Email { get; set; }
         [Required(ErrorMessage = "* Required!")]
        public string Password { get; set; }
    }
    [MetadataType(typeof(AdminLoginViewModel))]
    public partial class Admin
    {
         
    }
}