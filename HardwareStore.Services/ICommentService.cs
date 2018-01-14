namespace HardwareStore.Services
{
    using Models.Comments;

    public interface ICommentService
    {
        bool CanEdit(int id, int userId);

        bool Create(int reviewId, int authorId, string content);

        bool Edit(int id, string content);

        CommentFormServiceModel GetForm(int id);
    }
}