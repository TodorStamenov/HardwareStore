namespace HardwareStore.Services.Models.Comments
{
    using System;

    public class ListCommentsServiceModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime DateAdded { get; set; }

        public string Author { get; set; }

        public string AuthorImage { get; set; }

        public bool IsOwner { get; set; }
    }
}