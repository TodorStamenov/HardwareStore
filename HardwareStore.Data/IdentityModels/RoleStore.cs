namespace HardwareStore.Data.IdentityModels
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class RoleStore : RoleStore<Role, int, UserRole>
    {
        public RoleStore(HardwareStoreDbContext context)
            : base(context)
        {
        }
    }
}