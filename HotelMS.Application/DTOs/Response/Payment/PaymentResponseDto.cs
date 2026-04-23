namespace HotelMS.Application.DTOs.Response.Payment
{
    public class PaymentResponseDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime? PaidAt { get; set; }

        public int BookingId { get; set; }
        public int PaymentMethodId { get; set; }
    }
}