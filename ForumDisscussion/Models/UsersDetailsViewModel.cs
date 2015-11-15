using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumDisscussion.Models
{
    public class UsersDetailsViewModel
    {
        public int Id { get; set; }
        public byte[] UserImage { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public int TotalQuestion { get; set; }
        public int TotalReply { get; set; }
        public int TotalComment { get; set; }
        public int TotalValidPost { get; set; }
        public DateTime? LstActive { get; set; }
    }

    
}