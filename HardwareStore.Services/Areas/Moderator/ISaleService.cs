namespace HardwareStore.Services.Areas.Moderator
{
    using Models.Sales;
    using System.Collections.Generic;

    public interface ISaleService
    {
        int Total(string search);

        SaleDetailsServiceModel Details(int id);

        IEnumerable<ListSalesServiceModel> All(int page, int pageSize, string search);
    }
}