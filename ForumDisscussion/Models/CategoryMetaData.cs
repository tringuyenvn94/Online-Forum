using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ForumDisscussion.Models
{
    public class CategoryMetaData
    {
        [Key]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "* Required!")]
        public string CategoryName { get; set; }
    }
    [MetadataType(typeof(CategoryMetaData))]
    public partial class Category
    {

    }
    public class CategoryViewModel : Category
    {
      
    }
}