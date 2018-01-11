namespace HardwareStore.Common
{
    using System.IO;
    using System.Reflection;
    using System.Web;

    public class CommonConstants
    {
        public const string AdminRole = "Administrator";
        public const string ModeratorRole = "Moderator";

        public static readonly Assembly webAssembly = Assembly.Load("HardwareStore.Web");

        public static readonly string lorem = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\Content\Files\Lorem.txt"));
        public static readonly byte[] defaultUserImage = File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~\Content\Img\default-user-image.png"));
        public static readonly byte[] amdImage = File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~\Content\Img\amd-image.jpg"));
        public static readonly byte[] intelImage = File.ReadAllBytes(HttpContext.Current.Server.MapPath(@"~\Content\Img\intel-image.jpg"));
    }
}