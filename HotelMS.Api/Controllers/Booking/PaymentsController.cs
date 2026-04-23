using HotelMS.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelMS.Api.Controllers.Booking
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("paymant-process/{paymentId}")]
        public async Task<IActionResult> ProcessPayment(int paymentId)
        {
            var result = await _paymentService.ProcessPaymentAsync(paymentId);
            if (!result.success)
            {
                return BadRequest(result.error);
            }

            return Ok("გადახდა წარმატებით დასრულდა და ჯავშანი დადასტურდა.");
        }

        [HttpGet("booking/{bookingId}")]
        public async Task<IActionResult> GetPaymentByBookingId(int bookingId)
        {
            var payment = await _paymentService.GetPaymentByBookingIdAsync(bookingId);
            if (payment == null)
            {
                return NotFound("მოცემული ჯავშნისთვის გადახდა არ მოიძებნა.");
            }
            return Ok(payment);
        }

        [HttpGet("get-payment-detals")]
        public async Task<IActionResult> GetPaymentDetails(int paymentId)
        {
            var payment = await _paymentService.GetPaymentDetailsAsync(paymentId);
            if (payment == null)
            {
                return NotFound("მოცემული გადახდის დეტალები არ მოიძებნა.");
            }
            return Ok(payment);

        }
    }
}
