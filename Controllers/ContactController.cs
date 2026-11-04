using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using PortfolioApi.Interfaces;
using PortfolioApi.Models;

namespace PortfolioApi.Controllers;

[ApiController]
[Route("api/contact")]
public class ContactController : ControllerBase
{
    private readonly IContactService _service;

    public ContactController(IContactService service) => _service = service;

    // POST /api/contact
    // [ApiController] validates ContactRequest first. If a rule fails, this method is never
    // called and the visitor gets 400 with the list of problems.
    [HttpPost]
    [EnableRateLimiting("contact")]   // at most 5 messages per minute from one IP address
    public async Task<ActionResult<ContactResponse>> Submit(ContactRequest request, CancellationToken cancellationToken)
    {
        var id = await _service.SubmitAsync(request, cancellationToken);
        return StatusCode(StatusCodes.Status201Created,
            new ContactResponse(id, "Thank you! Your message has been received."));
    }
}
