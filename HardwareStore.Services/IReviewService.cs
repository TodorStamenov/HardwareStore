namespace HardwareStore.Services
{
    using Data.Models;
    using Models.Reviews;
    using System.Collections.Generic;

    public interface IReviewService
    {
        int TotalByItem(int itemId);

        bool CanEdit(int id, int userId);

        bool Create(string content, Mark mark, int itemId, int authorId);

        bool Edit(int id, string content);

        ReviewFormServiceModel GetForm(int id);

        IEnumerable<ListReviewsServiceModel> ByItem(int itemId, int page, int pageSize, int userId);
    }
}