namespace HardwareStore.Web.Areas.Moderator.Controllers
{
    using Data.Models;
    using Infrastructure;
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Services.Areas.Moderator;
    using Services.Infrastructure.Helpers;
    using System.Web.Mvc;

    public class CommentsController : BaseModeratorController
    {
        private const string Comment = "Comment";
        private const string Comments = "Comments";

        private readonly IModeratorCommentService commentService;

        public CommentsController(IModeratorCommentService commentService)
        {
            this.commentService = commentService;
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

            BaseRedirectServiceModel model = new BaseRedirectServiceModel
            {
                Page = page,
                ItemId = itemId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Delete))]
        [Log(LogType.Delete, Comments)]
        public ActionResult DeletePost(int? id, BaseRedirectServiceModel model)
        {
            if (id == null || model.ItemId == null)
            {
                return BadRequest();
            }

            if (model.Page == null || model.Page < 1)
            {
                model.Page = 1;
            }

            bool success = this.commentService.Delete(id.Value);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage(string.Format(
                WebConstants.SuccessfullEntityOperation,
                Comment,
                WebConstants.Deleted));

            return RedirectToAction(
                DetailsAction,
                Items,
                new { id = model.ItemId, model.Page, area = string.Empty });
        }
    }
}