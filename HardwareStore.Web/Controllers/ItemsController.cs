namespace HardwareStore.Web.Controllers
{
    using Microsoft.AspNet.Identity;
    using Models.Items;
    using Services;
    using Services.Areas.Moderator;
    using System.Web.Mvc;

    public class ItemsController : BaseController
    {
        private const int ItemsPerPage = 5;
        private const int ReviewsPerPage = 5;

        private readonly ICategoryService categoryService;
        private readonly ISubCategoryService subCategoryService;
        private readonly IItemService itemService;
        private readonly IReviewService reviewService;

        public ItemsController(
            ICategoryService categoryService,
            ISubCategoryService subCategoryService,
            IItemService itemService,
            IReviewService reviewService)
        {
            this.categoryService = categoryService;
            this.subCategoryService = subCategoryService;
            this.itemService = itemService;
            this.reviewService = reviewService;
        }

        public ActionResult Details(int? id, int? page)
        {
            if (id == null)
            {
                return BadRequest();
            }

            if (page == null || page < 1)
            {
                page = 1;
            }

            int reviewsCount = this.reviewService.TotalByItem(id.Value);
            int userId = User.Identity.GetUserId<int>();

            ItemDetailsViewModel model = new ItemDetailsViewModel
            {
                CurrentPage = page.Value,
                EntriesPerPage = ReviewsPerPage,
                TotalEntries = reviewsCount,
                Item = this.itemService.Details(id.Value),
                Reviews = this.reviewService.ByItem(id.Value, page.Value, ReviewsPerPage, userId)
            };

            if (model.Item == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        public ActionResult Index(int? page, string search, int? categoryId, int? subCategoryId)
        {
            if (page == null || page < 1)
            {
                page = 1;
            }

            ListItemsViewModel model = new ListItemsViewModel
            {
                CurrentPage = page.Value,
                EntriesPerPage = ItemsPerPage
            };

            if (categoryId != null
                 && this.categoryService.Exists(categoryId.Value)
                 && this.categoryService.HasItems(categoryId.Value))
            {
                model.CategoryId = categoryId;
                model.TotalEntries = this.itemService.TotalByCategory(categoryId.Value);
                model.Items = this.itemService.ByCategory(page.Value, ItemsPerPage, categoryId.Value);
            }
            else if (subCategoryId != null
                && this.subCategoryService.Exists(subCategoryId.Value)
                && this.subCategoryService.HasItems(subCategoryId.Value))
            {
                model.SubCategoryId = subCategoryId;
                model.TotalEntries = this.itemService.TotalBySubCategory(subCategoryId.Value);
                model.Items = this.itemService.BySubCategory(page.Value, ItemsPerPage, subCategoryId.Value);
            }
            else
            {
                model.TotalEntries = this.itemService.Total(search);
                model.Search = search;
                model.Items = this.itemService.All(page.Value, ItemsPerPage, search);
            }

            return View(model);
        }
    }
}