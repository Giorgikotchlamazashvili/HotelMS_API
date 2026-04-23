using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PaymentMethodController : ControllerBase
{
    private readonly IPaymentMethodService _service;

    public PaymentMethodController(IPaymentMethodService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaymentMethods()
    {
        var methods = await _service.GetPaymentMethodsAsync();
        return Ok(methods);
    }
}