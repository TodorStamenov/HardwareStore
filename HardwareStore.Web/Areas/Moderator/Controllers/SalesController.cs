namespace HardwareStore.Web.Areas.Moderator.Controllers
{
    using Models.Sales;
    using Services.Areas.Moderator;
    using Services.Areas.Moderator.Models.Sales;
    using System.Web.Mvc;

    public class SalesController : BaseModeratorController
    {
        private const int SalesPerPage = 20;

        private readonly ISaleService saleService;

        public SalesController(ISaleService saleService)
        {
            this.saleService = saleService;
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            SaleDetailsServiceModel model = this.saleService.Details(id.Value);

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        public ActionResult All(int? page, string search)
        {
            if (page == null || page < 1)
            {
                page = 1;
            }

            ListSalesViewModel model = new ListSalesViewModel
            {
                CurrentPage = page.Value,
                EntriesPerPage = SalesPerPage,
                TotalEntries = this.saleService.Total(search),
                Search = search,
                Sales = this.saleService.All(page.Value, SalesPerPage, search)
            };

            return View(model);
        }
    }
}