namespace HardwareStore.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Infrastructure.Extensions;
    using Models.Comments;
    using Models.Reviews;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReviewService : IReviewService
    {
        private readonly HardwareStoreDbContext db;

        public ReviewService(HardwareStoreDbContext db)
        {
            this.db = db;
        }

        public int TotalByItem(int itemId)
        {
            return this.db
                .Reviews
                .Where(r => r.ItemId == itemId)
                .Count();
        }

        public bool CanEdit(int id, int userId)
        {
            return this.db
                .Reviews
                .Any(r => r.Id == id && r.AuthorId == userId);
        }

        public bool Create(string content, Mark mark, int itemId, int authorId)
        {
            if (!this.db.Items.Any(i => i.Id == itemId))
            {
                return false;
            }

            Review review = new Review
            {
                Content = content,
                AuthorId = authorId,
                ItemId = itemId,
                Mark = mark,
                DateAdded = DateTime.UtcNow
            };

            this.db.Reviews.Add(review);
            this.db.SaveChanges();

            return true;
        }

        public bool Edit(int id, string content)
        {
            Review review = this.db.Reviews.Find(id);

            if (review == null)
            {
                return false;
            }

            review.Content = content;

            this.db.SaveChanges();

            return true;
        }

        public ReviewFormServiceModel GetForm(int id)
        {
            return this.db
                .Reviews
                .Where(r => r.Id == id)
                .ProjectTo<ReviewFormServiceModel>()
                .FirstOrDefault();
        }

        public IEnumerable<ListReviewsServiceModel> ByItem(int itemId, int page, int pageSize, int userId)
        {
            return this.db
                .Reviews
                .Where(r => r.ItemId == itemId)
                .OrderBy(r => r.DateAdded)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsEnumerable()
                .Select(r => new ListReviewsServiceModel
                {
                    Id = r.Id,
                    Content = r.Content,
                    Mark = (int)r.Mark,
                    DateAdded = r.DateAdded.ToLocalTime(),
                    Author = r.Author.UserName,
                    AuthorImage = r.Author.ProfileImage.ConvertImage(),
                    IsOwner = r.AuthorId == userId,
                    Comments = r.Comments
                        .OrderBy(c => c.DateAdded)
                        .Select(c => new ListCommentsServiceModel
                        {
                            Id = c.Id,
                            Content = c.Content,
                            DateAdded = c.DateAdded.ToLocalTime(),
                            Author = c.Author.UserName,
                            AuthorImage = c.Author.ProfileImage.ConvertImage(),
                            IsOwner = c.AuthorId == userId
                        })
                })
                .ToList();
        }
    }
}