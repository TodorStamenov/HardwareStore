namespace HardwareStore.Services.Areas.Moderator.Implementations
{
    using Data;
    using Data.Models;

    public class ModeratorReviewService : IModeratorReviewService
    {
        private readonly HardwareStoreDbContext db;

        public ModeratorReviewService(HardwareStoreDbContext db)
        {
            this.db = db;
        }

        public bool Delete(int id)
        {
            Review review = this.db.Reviews.Find(id);

            if (review == null)
            {
                return false;
            }

            this.db.Reviews.Remove(review);
            this.db.SaveChanges();

            return true;
        }
    }
}