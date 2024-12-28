using RegistrationApp.Models;

namespace RegistrationApp.ServiceRepository
{
    public interface IRegistrationService
    {
        bool Register(Registration registrationRequest);
        IEnumerable<Registration> GetRegistrations();
        Registration GetRegistrationById(Guid id);
        bool UpdateRegistration(UpdateRegistrationVM registrationRequest);
        bool DeleteRegistration(Guid id);
    }
}
