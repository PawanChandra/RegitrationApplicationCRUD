using System.ComponentModel.DataAnnotations;

namespace RegistrationApp.Models.CoreModels
{
    public class CountryManager
    {
        [Key]
        public Guid Id { get; set; }
        public string? CountryName { get; set; }

        public virtual ICollection<StateManager> States { get; set; }
    }
}
