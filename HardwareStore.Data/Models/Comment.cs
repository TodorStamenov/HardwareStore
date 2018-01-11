namespace HardwareStore.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.CommentConstants.MinContentLength)]
        public string Content { get; set; }

        public DateTime DateAdded { get; set; }

        public int ReviewId { get; set; }

        public virtual Review Review { get; set; }

        public int AuthorId { get; set; }

        public virtual User Author { get; set; }
    }
}