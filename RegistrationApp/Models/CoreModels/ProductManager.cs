using System.ComponentModel.DataAnnotations;

namespace RegistrationApp.Models.CoreModels
{
    public class ProductManager
    {
        [Key]
        public Guid ProductId { get; set; }
        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public Guid UserId { get; set; }
    }
}
