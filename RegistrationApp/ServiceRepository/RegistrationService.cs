using RegistrationApp.DataContext;
using RegistrationApp.Models;

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
                    registration.MobileNo = updateRegistrationRequest.MobileNo;

                    applicationDbContext.Update(registration);
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
    }
}
