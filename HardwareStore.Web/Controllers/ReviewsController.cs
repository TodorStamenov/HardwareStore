namespace HardwareStore.Web.Controllers
{
    using Common;
    using Infrastructure;
    using Infrastructure.Extensions;
    using Microsoft.AspNet.Identity;
    using Models.Reviews;
    using Services;
    using Services.Models.Reviews;
    using System.Web.Mvc;

    [Authorize]
    public class ReviewsController : BaseController
    {
        private const string Review = "Review";

        private readonly IReviewService reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        public ActionResult Create(int? itemId, int? page)
        {
            if (itemId == null)
            {
                return BadRequest();
            }

            if (page == null || page < 1)
            {
                page = 1;
            }

            ReviewFormViewModel model = new ReviewFormViewModel()
            {
                Review = new ReviewFormServiceModel
                {
                    ItemId = itemId,
                    Page = page
                }
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReviewFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Review.ItemId == null)
            {
                return BadRequest();
            }

            if (model.Review.Page == null
                || model.Review.Page < 1)
            {
                model.Review.Page = 1;
            }

            int authorId = User.Identity.GetUserId<int>();

            bool success = this.reviewService.Create(
                model.Review.Content,
                model.Mark,
                model.Review.ItemId.Value,
                authorId);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage(string.Format(
                WebConstants.SuccessfullEntityOperation,
                Review,
                WebConstants.Added));

            return RedirectToAction(
                nameof(ItemsController.Details),
                Items,
                new
                {
                    id = model.Review.ItemId,
                    page = model.Review.Page
                });
        }

        public ActionResult Edit(int? id, int? itemId, int? page)
        {
            if (id == null || itemId == null)
            {
                return BadRequest();
            }

            if (page == null || page < 1)
            {
                page = 1;
            }

            int userId = User.Identity.GetUserId<int>();

            if (!this.reviewService.CanEdit(id.Value, userId)
               && !User.IsInRole(CommonConstants.ModeratorRole))
            {
                return new HttpUnauthorizedResult();
            }

            ReviewFormServiceModel model = this.reviewService.GetForm(id.Value);

            model.Page = page;
            model.ItemId = itemId;

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, ReviewFormServiceModel model)
        {
            if (id == null || model.ItemId == null)
            {
                return BadRequest();
            }

            if (model.Page == null || model.Page < 1)
            {
                model.Page = 1;
            }

            int userId = User.Identity.GetUserId<int>();

            if (!this.reviewService.CanEdit(id.Value, userId)
               && !User.IsInRole(CommonConstants.ModeratorRole))
            {
                return new HttpUnauthorizedResult();
            }

            bool success = this.reviewService.Edit(
                id.Value,
                model.Content);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage(string.Format(
                WebConstants.SuccessfullEntityOperation,
                Review,
                WebConstants.Edited));

            return RedirectToAction(
                DetailsAction,
                Items,
                new
                {
                    id = model.ItemId,
                    page = model.Page
                });
        }
    }
}