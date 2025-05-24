using Microsoft.AspNetCore.Mvc;
using RegistrationApp.Models;
using RegistrationApp.ServiceRepository;

namespace RegistrationApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IRegistrationService _registrationService;
        public DashboardController(IRegistrationService _registrationService)
        {
             this._registrationService= _registrationService;
        }
        public IActionResult Index()
        {
            return View();
            //string? userIdString = HttpContext.Session.GetString("UserId");
            //if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            //if (userId != Guid.Empty)
            //{
            //    var userById=_registrationService.GetRegistrationById(userId);
            //    if (userById != null)
            //    {
            //        var registrations = _registrationService.GetRegistrations() ?? Enumerable.Empty<Registration>();
            //        return View(registrations);
            //    }
            //}
            //return RedirectToAction("Login","Account");
        }
    }
}
