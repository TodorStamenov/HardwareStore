namespace HardwareStore.Services.Areas.Moderator
{
    using Models.Sales;
    using System.Collections.Generic;

    public interface ISaleService
    {
        int Total(string search);

        bool Add(
            Dictionary<int, int> items,
            string firstName,
            string lastName,
            string address,
            string phone,
            int userId);

        SaleDetailsServiceModel Details(int id);

        IEnumerable<ListSalesServiceModel> All(int page, int pageSize, string search);
    }
}