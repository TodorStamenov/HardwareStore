namespace HardwareStore.Web.Controllers
{
    using Infrastructure;
    using Infrastructure.Extensions;
    using Microsoft.AspNet.Identity;
    using Services;
    using Services.Areas.Moderator;
    using Services.Models.Sales;
    using Services.Models.Shopping;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class ShoppingCartController : BaseController
    {
        private const string Removed = "Removed";
        private const string Cleared = "Shopping cart was successfully cleared";
        private const string SuccessfullCartOperation = "Item was successfully {0}";
        private const string SuccessfullPurchase = "Your purchase data has been accepted";

        private readonly IShoppingCartManager shoppingCartManager;
        private readonly ICartService cartService;
        private readonly ISaleService saleService;

        public ShoppingCartController(
            IShoppingCartManager shoppingCartManager,
            ICartService cartService,
            ISaleService saleService)
        {
            this.shoppingCartManager = shoppingCartManager;
            this.cartService = cartService;
            this.saleService = saleService;
        }

        public ActionResult Add(int? id, int? quantity)
        {
            if (id == null)
            {
                return BadRequest();
            }

            if (quantity == null || quantity <= 0)
            {
                quantity = 1;
            }

            string shoppingCartId = Session.GetShoppingCartId();

            this.shoppingCartManager.AddToCart(shoppingCartId, id.Value, quantity.Value);

            TempData.AddSuccessMessage(string.Format(SuccessfullCartOperation, WebConstants.Added));

            return RedirectToAction(
                nameof(ItemsController.Details),
                Items,
                new { id, area = string.Empty });
        }

        public ActionResult Remove(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            string shoppingCartId = Session.GetShoppingCartId();

            this.shoppingCartManager.RemoveFromCart(shoppingCartId, id.Value);

            TempData.AddSuccessMessage(string.Format(SuccessfullCartOperation, Removed));

            return RedirectToAction(nameof(CartItems));
        }

        public ActionResult Clear()
        {
            string shoppingCartId = Session.GetShoppingCartId();

            this.shoppingCartManager.Clear(shoppingCartId);

            TempData.AddSuccessMessage(string.Format(SuccessfullCartOperation, Cleared));

            return RedirectToAction(nameof(CartItems));
        }

        [Authorize]
        public ActionResult Purchase()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Purchase(PurchaseViewModel model)
        {
            string shoppingCartId = Session.GetShoppingCartId();

            Dictionary<int, int> items = this.shoppingCartManager.GetItems(shoppingCartId);

            int userId = User.Identity.GetUserId<int>();

            bool success = this.saleService.Add(
                items,
                model.FirstName,
                model.LastName,
                model.Address,
                model.Phone,
                userId);

            if (!success)
            {
                return BadRequest();
            }

            this.shoppingCartManager.Clear(shoppingCartId);
            TempData.AddSuccessMessage(SuccessfullPurchase);

            return RedirectToAction(
                nameof(ItemsController.Index),
                Items,
                new { area = string.Empty });
        }

        public ActionResult CartItems()
        {
            string shoppingCartId = Session.GetShoppingCartId();

            Dictionary<int, int> items = this.shoppingCartManager.GetItems(shoppingCartId);

            IEnumerable<ListCartItemsServiceModel> model = this.cartService.GetItems(items);

            return View(model);
        }
    }
}