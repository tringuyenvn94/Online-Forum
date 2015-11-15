using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumDisscussion.Models
{
    public class UserInfoViewModel
    {
        public byte[] Image { get; set; }
        public string Category { get; set; }
        public string Question { get; set; }
        public string Answe { get; set; }
    }
}