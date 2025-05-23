using System.ComponentModel.DataAnnotations;

namespace RegistrationApp.Models.CoreModels
{
    public class CityManager
    {
        [Key]
        public Guid Id { get; set; }
        public string CityName { get; set; }

        public Guid StateId { get; set; }
        public virtual StateManager State { get; set; }
    }
}
