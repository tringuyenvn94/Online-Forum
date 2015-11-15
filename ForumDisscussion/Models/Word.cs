
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;


namespace ForumDisscussion.Models
{
    [Table("Word")]
    public class Word
    {
        [Key]
        public int Id { get; set; }
        public string SensitiveWord { get; set; }
    }

    public class WordDbContext:DbContext
    {
        public DbSet<Word> Words { get; set; }
        
    }
}