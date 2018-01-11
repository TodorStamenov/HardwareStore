namespace HardwareStore.Services.Implementations
{
    using Data;
    using Data.Models;

    public class UserService : IUserService
    {
        private readonly HardwareStoreDbContext db;

        public UserService(HardwareStoreDbContext db)
        {
            this.db = db;
        }

        public bool AddProfileImage(int userId, byte[] imageContent)
        {
            User user = this.db.Users.Find(userId);

            if (user == null)
            {
                return false;
            }

            user.ProfileImage = imageContent;

            this.db.SaveChanges();

            return true;
        }
    }
}