using Microsoft.AspNetCore.Mvc.Rendering;
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

        public Guid CountryId { get; set; }
        public Guid StateId { get; set; }
        public Guid CityId { get; set; }

        public IEnumerable<SelectListItem>? Countries { get; set; }
        public IEnumerable<SelectListItem>? States { get; set; }
        public IEnumerable<SelectListItem>? Cities { get; set; }
    }
}
