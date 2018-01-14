namespace HardwareStore.Web.Controllers
{
    using Common;
    using Infrastructure;
    using Infrastructure.Extensions;
    using Microsoft.AspNet.Identity;
    using Services;
    using Services.Models.Comments;
    using System.Web.Mvc;

    [Authorize]
    public class CommentsController : BaseController
    {
        private const string Comment = "Comment";
        private const string Comments = "Comments";

        private readonly ICommentService commentService;

        public CommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        public ActionResult Create(int? id, int? itemId, int? page)
        {
            if (id == null || itemId == null)
            {
                return BadRequest();
            }

            if (page == null || page < 1)
            {
                page = 1;
            }

            CommentFormServiceModel model = new CommentFormServiceModel()
            {
                Page = page,
                ItemId = itemId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? id, CommentFormServiceModel model)
        {
            if (id == null || model.ItemId == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Page == null || model.Page < 1)
            {
                model.Page = 1;
            }

            int authorId = User.Identity.GetUserId<int>();

            bool success = this.commentService.Create(id.Value, authorId, model.Content);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage(string.Format(
                WebConstants.SuccessfullEntityOperation,
                Comment,
                WebConstants.Added));

            return RedirectToAction(
                nameof(ItemsController.Details),
                Items,
                new { id = model.ItemId, model.Page });
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

            if (!this.commentService.CanEdit(id.Value, userId)
                && !User.IsInRole(CommonConstants.ModeratorRole))
            {
                return new HttpUnauthorizedResult();
            }

            CommentFormServiceModel model = this.commentService.GetForm(id.Value);

            if (model == null)
            {
                return BadRequest();
            }

            model.Page = page;
            model.ItemId = itemId;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, CommentFormServiceModel model)
        {
            if (id == null || model.ItemId == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Page == null
                || model.Page < 1)
            {
                model.Page = 1;
            }

            int userId = User.Identity.GetUserId<int>();

            if (!this.commentService.CanEdit(id.Value, userId)
                && !User.IsInRole(CommonConstants.ModeratorRole))
            {
                return new HttpUnauthorizedResult();
            }

            bool success = this.commentService.Edit(id.Value, model.Content);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage(string.Format(
                WebConstants.SuccessfullEntityOperation,
                Comment,
                WebConstants.Edited));

            return RedirectToAction(
                nameof(ItemsController.Details),
                Items,
                new { id = model.ItemId, model.Page });
        }
    }
}