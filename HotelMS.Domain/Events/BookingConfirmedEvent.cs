namespace HotelMS.Domain.Events;

public record BookingConfirmedEvent
{
    public int UserId { get; init; }
    public int BookingId { get; init; }
}