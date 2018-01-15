namespace HardwareStore.Services.Areas.Moderator.Models.Sales
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;

    public class ListSalesServiceModel : IMapFrom<Sale>, ICustomMapping
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime Date { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<Sale, ListSalesServiceModel>()
                .ForMember(s => s.Username, cfg => cfg.MapFrom(opt => opt.User.UserName));
        }
    }
}