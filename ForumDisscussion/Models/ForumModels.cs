using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;


namespace ForumDisscussion.Models
{
    public class ForumModels
    {
    }
    [Table("ForumUser")]
    public  class ForumUser
    {[Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public byte[] Image { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
    }
     [Table("Questions")]
    public  class Questions
    {
        [Key]
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public System.DateTime PostedDate { get; set; }
        public int answers { get; set; }
        public int views { get; set; }
    }
     [Table("Answers")]
    public  class Answers
    {
        [Key]
        public int AnswerId { get; set; }
        public string Answer { get; set; }
        public int UserId { get; set; }
        public System.DateTime AnsweredTimeDate { get; set; }
        public int QuestionId { get; set; }
        public int? Comments { get; set; }
        public double? Rating { get; set; }
    }
     [Table("Comment")]
    public  class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string CommentMsg { get; set; }
        public System.DateTime PostedDateTime { get; set; }
        public int UserId { get; set; }
        public int AnswerId { get; set; }
    }
     [Table("PostInformation")]
    public  class PostInformation
    {
        [Key]
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int? TotalQuesPost { get; set; }
        public int? TotalAnsPost { get; set; }
        public int? TotalCommentPost { get; set; }
        public int? TotalValidPost { get; set; }
    }
     [Table("Rating")]
    public  class Rating
    {
        [Key]
        public int RatingId { get; set; }
        public int RateScore { get; set; }
        public int RaterId { get; set; }
        public int AnswerId { get; set; }
        public System.DateTime RatedDateTime { get; set; }
    }
     [Table("Temp")]
    public  class Temp
    {
        [Key]
        public int Id { get; set; }
        public int? TempId { get; set; }
    }
     [Table("Category")]
    public  class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
     [Table("Admin")]
    public  class Admin
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime? LastActive { get; set; }
    }
     [Table("TemporaryTable")]
    public  class TemporaryTable
    {
        [Key]
        public int Id { get; set; }
        public int? TempId { get; set; }
    }
     [Table("TemQuesIdModel")]
    public  class TemQuesIdModel
    {
        [Key]
        public int Id { get; set; }
        public int TempQuesId { get; set; }
    }
     [Table("Word")]
    public class Word
    {
        [Key]
        public int Id { get; set; }
        public string SensitiveWord { get; set; }
    }
     public class ForumDataBaseContextClass : DbContext
    {
        

        public DbSet<Answers> Answers { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<ForumUser> ForumUser { get; set; }
        public DbSet<PostInformation> PostInformation { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Temp> Temp { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<TemporaryTable> TemporaryTables { get; set; }
        public DbSet<TemQuesIdModel> TemQuesModels { get; set; }
        public DbSet<Word> Words { get; set; }
    }
}