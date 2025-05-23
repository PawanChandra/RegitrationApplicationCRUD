using RegistrationApp.Models;
using RegistrationApp.Models.CoreModels;

namespace RegistrationApp.ServiceRepository
{
    public interface IRegistrationService
    {
        bool Register(Registration registrationRequest);
        IEnumerable<Registration> GetRegistrations();
        Registration GetRegistrationById(Guid id);
        bool UpdateRegistration(UpdateRegistrationVM registrationRequest);
        bool DeleteRegistration(Guid id);

        List<CountryManager> GetCountries();
        List<StateManager> GetStatesByCountry(Guid countryId);
        List<CityManager> GetCitiesByState(Guid stateId);
    }
}
