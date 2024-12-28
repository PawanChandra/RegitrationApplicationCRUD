using System.ComponentModel.DataAnnotations;

namespace RegistrationApp.Models
{
    public class Registration
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }= string.Empty;
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email should be in valid.")]
        public string Email { get; set; }=string.Empty;
        [Required(ErrorMessage = "Mobile is required")]
        [Display(Name="Mobile")]
        public string MobileNo { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }= string.Empty;
    }
}
