namespace HardwareStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Review
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.ReviewConstants.MinContentLength)]
        public string Content { get; set; }

        public Mark Mark { get; set; }

        public DateTime ReviewDate { get; set; }

        public int AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int ItemId { get; set; }

        public virtual Item Item { get; set; }

        public virtual List<Comment> Comments { get; set; } = new List<Comment>();
    }
}