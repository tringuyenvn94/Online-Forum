
using System.ComponentModel.DataAnnotations;


namespace ForumDisscussion.Models
{
    public class EditViwModel
    {

        [Required]
        public string Reply { get; set; }
    }
}