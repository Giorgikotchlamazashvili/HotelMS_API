using HotelMS.Domain.Common;

namespace HotelMS.Domain.Entities;

public class PaymentMethod : BaseEntity
{
    public string Name { get; set; }
    public List<Payment> Payments { get; set; }
}
