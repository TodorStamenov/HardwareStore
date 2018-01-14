namespace HardwareStore.Web.Models.Reviews
{
    using Data.Models;
    using Services.Models.Reviews;

    public class ReviewFormViewModel
    {
        public ReviewFormServiceModel Review { get; set; }

        public Mark Mark { get; set; }
    }
}