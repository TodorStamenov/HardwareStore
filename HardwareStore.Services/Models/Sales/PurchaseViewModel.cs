namespace HardwareStore.Services.Models.Sales
{
    using Data;
    using System.ComponentModel.DataAnnotations;

    public class PurchaseViewModel
    {
        [Required]
        [StringLength(
            DataConstants.SaleConstants.MaxFirstNameLength,
            MinimumLength = DataConstants.SaleConstants.MinFirstNameLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(
            DataConstants.SaleConstants.MaxLastNameLength,
            MinimumLength = DataConstants.SaleConstants.MinLastNameLength)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(DataConstants.SaleConstants.PhoneRegex)]
        public string Phone { get; set; }

        [Required]
        [StringLength(
            DataConstants.SaleConstants.MaxAddressLength,
            MinimumLength = DataConstants.SaleConstants.MinAddressLength)]
        public string Address { get; set; }
    }
}