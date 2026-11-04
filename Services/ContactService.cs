using PortfolioApi.Interfaces;
using PortfolioApi.Models;

namespace PortfolioApi.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _repository;
    private readonly ILogger<ContactService> _logger;

    public ContactService(IContactRepository repository, ILogger<ContactService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<int> SubmitAsync(ContactRequest request, CancellationToken cancellationToken)
    {
        // Clean up the text (the [Required]/[StringLength] rules were already checked by the controller).
        var message = new ContactMessage
        {
            Name = request.Name.Trim(),
            Email = request.Email.Trim().ToLowerInvariant(),
            Subject = request.Subject.Trim(),
            Message = request.Message.Trim()
        };

        var id = await _repository.AddAsync(message, cancellationToken);
        _logger.LogInformation("Contact message {Id} saved.", id);
        return id;
    }
}
