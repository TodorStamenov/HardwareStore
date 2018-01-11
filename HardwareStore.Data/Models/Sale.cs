namespace HardwareStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Sale
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.SaleConstants.MinFirstNameLength)]
        [MaxLength(DataConstants.SaleConstants.MaxFirstNameLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(DataConstants.SaleConstants.MinLastNameLength)]
        [MaxLength(DataConstants.SaleConstants.MaxLastNameLength)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(DataConstants.SaleConstants.PhoneRegex)]
        public string Phone { get; set; }

        [Required]
        [MinLength(DataConstants.SaleConstants.MinAddressLength)]
        [MaxLength(DataConstants.SaleConstants.MaxAddressLength)]
        public string Address { get; set; }

        [Range(
            DataConstants.SaleConstants.MinTotalPrice,
            DataConstants.SaleConstants.MaxTotalPrice)]
        public decimal TotalPrice { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual List<ItemSale> Items { get; set; } = new List<ItemSale>();
    }
}