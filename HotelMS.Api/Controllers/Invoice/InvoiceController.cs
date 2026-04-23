using HotelMS.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelMS.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet("get-invoice")]
        public async Task<IActionResult> GetInvoiceUser()
        {
            var invoice = await _invoiceService.GetInvoice();

            if (string.IsNullOrEmpty(invoice.InvoicePdfUrl))
                return Accepted(new { message = "Invoice is still being generated, try again shortly." });

            return Ok(invoice);
        }
    }
}