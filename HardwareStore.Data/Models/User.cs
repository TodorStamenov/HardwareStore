namespace HardwareStore.Data.Models
{
    using IdentityModels;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        [MaxLength(DataConstants.UserConstants.MaxProfileImageSize)]
        public byte[] ProfileImage { get; set; }

        public virtual List<Sale> Sales { get; set; } = new List<Sale>();

        public virtual List<Review> Reviews { get; set; } = new List<Review>();

        public virtual List<Comment> Comments { get; set; } = new List<Comment>();

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}