using HotelMS.Domain.Entities;

namespace HotelMS.Interfaces.Repositories;

public interface IPaymentRepository
{


    Task<Payment> GetByIdAsync(int id);
    Task<Payment> GetByBookingIdAsync(int bookingId);
    Task AddAsync(Payment payment);
    void Update(Payment payment);


    Task SaveChangesAsync();

}
