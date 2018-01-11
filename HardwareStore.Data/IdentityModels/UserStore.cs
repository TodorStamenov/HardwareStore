namespace HardwareStore.Data.IdentityModels
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class UserStore : UserStore<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public UserStore(HardwareStoreDbContext context)
            : base(context)
        {
        }
    }
}