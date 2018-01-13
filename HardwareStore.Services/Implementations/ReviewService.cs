namespace HardwareStore.Services.Implementations
{
    using Data;
    using HardwareStore.Services.Infrastructure.Extensions;
    using HardwareStore.Services.Models.Comments;
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