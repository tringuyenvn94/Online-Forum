using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumDisscussion.Models
{
    public class UserComment:AnsPostedUser

    {
        public string CommentMsg { get; set; }
        public int CommentId { get; set; }
    }

    public class CommentMetadata
    {
        [Required(ErrorMessage = "* Required")]
        public string CommentMsg { get; set; }
    }
    [MetadataType(typeof(CommentMetadata))]
    public partial class Comment
    {
         
    }
}