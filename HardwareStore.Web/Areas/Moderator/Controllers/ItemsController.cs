namespace HardwareStore.Web.Areas.Moderator.Controllers
{
    using Data;
    using Infrastructure;
    using Infrastructure.Extensions;
    using Models.Items;
    using Services.Areas.Moderator;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class ItemsController : BaseModeratorController
    {
        private const string Item = "Item";
        private const string Details = "Details";

        private readonly IModeratorItemService itemService;
        private readonly ISubCategoryService subCategoryService;

        public ItemsController(
            IModeratorItemService itemService,
            ISubCategoryService subCategoryService)
        {
            this.itemService = itemService;
            this.subCategoryService = subCategoryService;
        }

        public ActionResult Create()
        {
            ItemFormViewModel model = new ItemFormViewModel
            {
                SubCategories = this.GetSubCategories()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.SubCategories = this.GetSubCategories();
                return View(model);
            }

            if (this.itemService.NameExists(model.Item.Name))
            {
                model.SubCategories = this.GetSubCategories();
                TempData.AddErrorMessage(string.Format(WebConstants.EntryExists, Item));
                return View(model);
            }

            if (model.Item.Image == null
                || !model.Item.Image.ContentType.Contains("image")
                || model.Item.Image.ContentLength > DataConstants.ItemConstants.MaxItemImageSize)
            {
                TempData.AddErrorMessage("Uploaded image file must be less then 1 MB");
                return View(model);
            }

            int id = this.itemService.Create(
                model.Item.Name,
                model.Item.Description,
                model.Item.Price,
                model.Item.Discount,
                model.Item.Quantity,
                model.Item.WarrantyLength,
                model.Item.Image.ToByteArray(),
                model.SubCategoryId);

            if (id <= 0)
            {
                return BadRequest();
            }

            return RedirectToAction(
                Details,
                Items,
                new { id, area = string.Empty });
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            ItemFormViewModel model = new ItemFormViewModel
            {
                Item = this.itemService.GetForm(id.Value),
                SubCategories = this.GetSubCategories()
            };

            if (model.Item == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, ItemFormViewModel model)
        {
            if (id == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                model.SubCategories = this.GetSubCategories();
                return View(model);
            }

            string oldName = this.itemService.GetName(id.Value);

            if (oldName == null)
            {
                return BadRequest();
            }

            string newName = model.Item.Name;

            if (this.itemService.NameExists(newName)
                && oldName != newName)
            {
                model.SubCategories = this.GetSubCategories();
                TempData.AddErrorMessage(string.Format(WebConstants.EntryExists, Item));
                return View(model);
            }

            bool success = this.itemService.Edit(
                id.Value,
                newName,
                model.Item.Description,
                model.Item.Price,
                model.Item.Discount,
                model.Item.Quantity,
                model.Item.WarrantyLength,
                model.Item.Image?.ToByteArray(),
                model.SubCategoryId);

            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction(
                Details,
                Items,
                new { id, area = string.Empty });
        }

        private IEnumerable<SelectListItem> GetSubCategories()
        {
            return this.subCategoryService
                .GetMenu()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });
        }
    }
}