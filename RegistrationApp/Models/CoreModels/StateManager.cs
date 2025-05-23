using System.ComponentModel.DataAnnotations;

namespace RegistrationApp.Models.CoreModels
{
    public class StateManager
    {
        [Key]
        public Guid Id { get; set; }
        public string? StateName { get; set; }

        public Guid CountryId { get; set; }
        public virtual CountryManager Country { get; set; }

        public virtual ICollection<CityManager> Cities { get; set; } 
    }
}
