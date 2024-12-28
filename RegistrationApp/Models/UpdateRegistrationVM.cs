using System.ComponentModel.DataAnnotations;

namespace RegistrationApp.Models
{
    public class UpdateRegistrationVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Email should be in valid.")]
        public string Email { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
    }
}
