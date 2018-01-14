namespace HardwareStore.Web.Controllers
{
    using System.Net;
    using System.Web.Mvc;

    public class BaseController : Controller
    {
        protected string Items = "Items";
        protected string DetailsAction = "Details";

        protected HttpStatusCodeResult BadRequest()
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}