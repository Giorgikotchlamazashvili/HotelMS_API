namespace HotelMS.Domain.Helpers
{
    public static class PriceCalculator
    {
        public static decimal Calculate(DateTime start, DateTime end, decimal pricePerNight)
        {
            if (end <= start) return 0;

            int days = (end - start).Days;


            if (days == 0) days = 1;

            return days * pricePerNight;
        }
    }
}