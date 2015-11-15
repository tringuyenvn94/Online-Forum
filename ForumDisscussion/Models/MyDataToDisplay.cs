using System;


namespace ForumDisscussion.Models
{
    public class QuestionList
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Category { get; set; }
        public string PostedBy { get; set; }
        //[DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Posteddate { get; set; }
        public int Views { get; set; }
        public int Answers { get; set; }
        public byte[] Image { get; set; }
        public string AnsMessage { get; set; }
     
    }
}