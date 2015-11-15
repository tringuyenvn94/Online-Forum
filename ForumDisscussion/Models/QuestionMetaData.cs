using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumDisscussion.Models
{
    public class QuestionMetaData
    {
        [Required(ErrorMessage = "* Required!")]
        public string Question { get; set; }
        [Required]
        public int CategoryId { get; private set; }
    }
    [MetadataType(typeof(QuestionMetaData))]
    public partial class Questions
    {
         
    }

    public class Search
    {
        [Required]
        public string SerachTerm { get; set; }
    }
}