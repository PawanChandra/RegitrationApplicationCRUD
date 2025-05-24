using System.ComponentModel.DataAnnotations;

namespace RegistrationApp.Models
{
    public class CreateProductManagerVM
    {
        public IFormFile? ImageFile { get; set; }
        public string ImagePath { get; set; } = string.Empty;

        [Required(ErrorMessage = "Product name is required")]
        public string ProductName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public Guid UserId { get; set; }
    }
}
