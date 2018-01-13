namespace HardwareStore.Services
{
    using Models.Reviews;
    using System.Collections.Generic;

    public interface IReviewService
    {
        int TotalByItem(int itemId);

        IEnumerable<ListReviewsServiceModel> ByItem(int itemId, int page, int pageSize, int userId);
    }
}