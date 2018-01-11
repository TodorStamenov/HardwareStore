namespace HardwareStore.Services
{
    public interface IUserService
    {
        bool AddProfileImage(int userId, byte[] imageContent);
    }
}