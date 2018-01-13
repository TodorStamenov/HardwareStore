namespace HardwareStore.Services.Models.Reviews
{
    using Comments;
    using System;
    using System.Collections.Generic;

    public class ListReviewsServiceModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int Mark { get; set; }

        public DateTime DateAdded { get; set; }

        public string Author { get; set; }

        public string AuthorImage { get; set; }

        public bool IsOwner { get; set; }

        public IEnumerable<ListCommentsServiceModel> Comments { get; set; }
    }
}