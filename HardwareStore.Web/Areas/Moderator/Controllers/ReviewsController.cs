namespace HardwareStore.Web.Areas.Moderator.Controllers
{
    using Data.Models;
    using Infrastructure;
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Services.Areas.Moderator;
    using Services.Infrastructure.Helpers;
    using System.Web.Mvc;

    public class ReviewsController : BaseModeratorController
    {
        private const string Review = "Review";
        private const string Reviews = "Reviews";

        private readonly IModeratorReviewService reviewService;

        public ReviewsController(IModeratorReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        public ActionResult Delete(int? id, int? itemId, int? page)
        {
            if (id == null || itemId == null)
            {
                return BadRequest();
            }

            if (page == null || page < 1)
            {
                page = 1;
            }

            BaseRedirectServiceModel model = new BaseRedirectServiceModel()
            {
                Page = page,
                ItemId = itemId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Delete))]
        [Log(LogType.Delete, Reviews)]
        public ActionResult DeletePost(int? id, int? itemId, int? page)
        {
            if (id == null || itemId == null)
            {
                return BadRequest();
            }

            if (page == null || page < 1)
            {
                page = 1;
            }

            bool success = this.reviewService.Delete(id.Value);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage(string.Format(
                WebConstants.SuccessfullEntityOperation,
                Review,
                WebConstants.Deleted));

            return RedirectToAction(
                DetailsAction,
                Items,
                new { id = itemId, page, area = string.Empty });
        }
    }
}