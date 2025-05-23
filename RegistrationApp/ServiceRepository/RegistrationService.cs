using RegistrationApp.DataContext;
using RegistrationApp.Models;
using RegistrationApp.Models.CoreModels;

namespace RegistrationApp.ServiceRepository
{
    public class RegistrationService : IRegistrationService
    {
        private readonly ApplicationDbContext applicationDbContext;
        public RegistrationService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public bool Register(Registration registrationRequest)
        {
            try
            {
                applicationDbContext.Registrations.Add(registrationRequest);
                applicationDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return false;
        }
        public Registration GetRegistrationById(Guid id)
        {
            Registration response = new();
            try
            {
                response = applicationDbContext.Registrations.Where(r => r.Id == id).FirstOrDefault()!;
                if (response != null)
                    return response;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return response!;
        }
        public IEnumerable<Registration> GetRegistrations()
        {
            try
            {
                IEnumerable<Registration> registrations = applicationDbContext.Registrations.ToList()!;
                if (registrations != null)
                    return registrations;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return Enumerable.Empty<Registration>();
        }
        public bool UpdateRegistration(UpdateRegistrationVM updateRegistrationRequest)
        {
            try
            {
                Registration registration = applicationDbContext.Registrations.Where(r => r.Id == updateRegistrationRequest.Id).FirstOrDefault()!;
                if (registration != null)
                {
                    registration.Name = updateRegistrationRequest.Name;
                    registration.Email = updateRegistrationRequest.Email;
                    registration.Gender = updateRegistrationRequest.Gender;
                    registration.MobileNo = updateRegistrationRequest.MobileNo;
                    registration.CountryId = updateRegistrationRequest.CountryId;
                    registration.StateId = updateRegistrationRequest.StateId;
                    registration.CityId = updateRegistrationRequest.CityId;


                    applicationDbContext.Registrations.Update(registration);
                    applicationDbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return false!;
        }
        public bool DeleteRegistration(Guid id)
        {
            try
            {
                var registration = applicationDbContext.Registrations.Find(id);
                if (registration != null)
                {
                    applicationDbContext.Registrations.Remove(registration);
                    applicationDbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return false;
        }

        public List<CountryManager> GetCountries()
        {
            return applicationDbContext.Countries.ToList();
        }
        public List<StateManager> GetStatesByCountry(Guid countryId)
        {
            var states = applicationDbContext.States.Where(s => s.CountryId == countryId).ToList();
            return states ?? null;
        }
        public List<CityManager> GetCitiesByState(Guid stateId)
        {
            var cities = applicationDbContext.Cities.Where(s => s.StateId == stateId).ToList();
            return cities ?? null;
        }
    }
}
