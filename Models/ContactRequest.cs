using System.ComponentModel.DataAnnotations;

namespace PortfolioApi.Models;

// What the React contact form sends to POST /api/contact.
// The [ApiController] attribute checks these rules automatically and
// answers 400 Bad Request (with a list of problems) when something is wrong.
public class ContactRequest
{
    [Required(ErrorMessage = "Please enter your name.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your email address.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [StringLength(150, ErrorMessage = "Email must be 150 characters or fewer.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter a subject.")]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "Subject must be between 3 and 150 characters.")]
    public string Subject { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please write a message.")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "Message must be between 10 and 2000 characters.")]
    public string Message { get; set; } = string.Empty;
}
