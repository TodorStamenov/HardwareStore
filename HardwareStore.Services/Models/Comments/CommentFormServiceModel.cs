namespace HardwareStore.Services.Models.Comments
{
    using Common.Mapping;
    using Data;
    using Data.Models;
    using Infrastructure.Helpers;
    using System.ComponentModel.DataAnnotations;

    public class CommentFormServiceModel : BaseRedirectServiceModel, IMapFrom<Comment>
    {
        [Required]
        [MinLength(DataConstants.CommentConstants.MinContentLength)]
        public string Content { get; set; }
    }
}