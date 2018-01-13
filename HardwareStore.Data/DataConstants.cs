namespace HardwareStore.Data
{
    public static class DataConstants
    {
        public static class CategoryConstants
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 20;
        }

        public static class SubCategotyConstants
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 20;
        }

        public static class ItemConstants
        {
            public const int MinNameLength = 2;
            public const int MaxNameLength = 100;
            public const int MinDescriptionLength = 3;
            public const int MinDiscount = 0;
            public const int MaxDiscount = 100;
            public const double MinPrice = double.Epsilon;
            public const double MaxPrice = double.MaxValue;
            public const int MinQuantity = 0;
            public const int MaxQuantity = int.MaxValue;
            public const int MinWarranty = 0;
            public const int MaxWarranty = 24;
            public const int MaxItemImageSize = 1024 * 1024;
            public const int MinViewsCount = 0;
            public const int MaxViewsCount = int.MaxValue;
        }

        public static class ReviewConstants
        {
            public const int MinContentLength = 3;
        }

        public static class CommentConstants
        {
            public const int MinContentLength = 3;
        }

        public static class SaleConstants
        {
            public const int MinFirstNameLength = 3;
            public const int MaxFirstNameLength = 50;
            public const int MinLastNameLength = 3;
            public const int MaxLastNameLength = 50;
            public const string PhoneRegex = @"^\+\d{12}$";
            public const int MinAddressLength = 3;
            public const int MaxAddressLength = 100;
            public const double MinTotalPrice = double.Epsilon;
            public const double MaxTotalPrice = double.MaxValue;
        }

        public static class ItemSaleConstants
        {
            public const int MinQuantity = 0;
            public const int MaxQuantity = int.MaxValue;
            public const double MinPrice = double.Epsilon;
            public const double MaxPrice = double.MaxValue;
        }

        public static class UserConstants
        {
            public const int MaxProfileImageSize = 100 * 1024;
        }
    }
}