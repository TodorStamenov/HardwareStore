namespace HardwareStore.Services.Areas.Moderator.Implementations
{
    using Data;
    using Data.Models;

    public class ModeratorCommentService : IModeratorCommentService
    {
        private readonly HardwareStoreDbContext db;

        public ModeratorCommentService(HardwareStoreDbContext db)
        {
            this.db = db;
        }

        public bool Delete(int id)
        {
            Comment comment = this.db.Comments.Find(id);

            if (comment == null)
            {
                return false;
            }

            this.db.Comments.Remove(comment);
            this.db.SaveChanges();

            return true;
        }
    }
}