namespace HardwareStore.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Models.Comments;
    using System;
    using System.Linq;

    public class CommentService : ICommentService
    {
        private readonly HardwareStoreDbContext db;

        public CommentService(HardwareStoreDbContext db)
        {
            this.db = db;
        }

        public bool CanEdit(int id, int userId)
        {
            return this.db
                .Comments
                .Any(c => c.Id == id && c.AuthorId == userId);
        }

        public bool Create(int reviewId, int authorId, string content)
        {
            if (!this.db.Reviews.Any(r => r.Id == reviewId))
            {
                return false;
            }

            Comment comment = new Comment
            {
                ReviewId = reviewId,
                Content = content,
                DateAdded = DateTime.UtcNow,
                AuthorId = authorId
            };

            this.db.Comments.Add(comment);
            this.db.SaveChanges();

            return true;
        }

        public bool Edit(int id, string content)
        {
            Comment comment = this.db.Comments.Find(id);

            if (comment == null)
            {
                return false;
            }

            comment.Content = content;

            this.db.SaveChanges();

            return true;
        }

        public CommentFormServiceModel GetForm(int id)
        {
            return this.db
                .Comments
                .Where(c => c.Id == id)
                .ProjectTo<CommentFormServiceModel>()
                .FirstOrDefault();
        }
    }
}