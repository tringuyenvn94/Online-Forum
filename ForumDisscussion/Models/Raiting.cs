using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ForumDisscussion.Models
{
    [Table("Rating")]
    public class Raiting
    {
        [Key]
        public int RatingId { get; set; }

        public int RateScore { get; set; }

        public int RaterId { get; set; }

        public int AnswerId { get; set; }

        public DateTime RatedDateTime { get; set; }
    }
}