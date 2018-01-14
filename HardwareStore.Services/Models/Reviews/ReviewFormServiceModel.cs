namespace HardwareStore.Services.Models.Reviews
{
    using Common.Mapping;
    using Data;
    using Data.Models;
    using Infrastructure.Helpers;
    using System.ComponentModel.DataAnnotations;

    public class ReviewFormServiceModel : BaseRedirectServiceModel, IMapFrom<Review>
    {
        [Required]
        [MinLength(DataConstants.ReviewConstants.MinContentLength)]
        public string Content { get; set; }
    }
}