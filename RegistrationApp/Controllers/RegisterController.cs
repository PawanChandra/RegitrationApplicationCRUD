using Microsoft.AspNetCore.Mvc;
using RegistrationApp.Models;
using RegistrationApp.ServiceRepository;
using System.Diagnostics;

namespace RegistrationApp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IRegistrationService _registrationService;

        public RegisterController(IRegistrationService registrationService)
        {
            _registrationService=registrationService;
        }

        public IActionResult Index()
        {
            var registrations = _registrationService.GetRegistrations() ?? Enumerable.Empty<Registration>(); ;
            return View(registrations);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Registration registrationRequest)
        {
            registrationRequest.Id = Guid.NewGuid();
            if (ModelState.IsValid)
            {
                bool res= _registrationService.Register(registrationRequest);
                if (res)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            if(id != Guid.Empty)
            {
                Registration registrationById= _registrationService.GetRegistrationById(id);
                if(registrationById != null)
                {
                    UpdateRegistrationVM updateRegistrationVM = new UpdateRegistrationVM
                    {
                        Id = registrationById.Id,
                        Name = registrationById.Name,
                        Email = registrationById.Email,
                        Gender = registrationById.Gender,
                        MobileNo = registrationById.MobileNo
                    };
                    return View(updateRegistrationVM);
                }
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Edit(UpdateRegistrationVM registrationRequest)
        {
            if(registrationRequest.Id != Guid.Empty)
            {
                bool res = _registrationService.UpdateRegistration(registrationRequest);
                if(res)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Details(Guid id)
        {
            if (id != Guid.Empty)
            {
                Registration registrationById = _registrationService.GetRegistrationById(id);
                if (registrationById != null)
                {
                    UpdateRegistrationVM registrationVM = new UpdateRegistrationVM
                    {
                        Id = registrationById.Id,
                        Name = registrationById.Name,
                        Email = registrationById.Email,
                        Gender = registrationById.Gender,
                        MobileNo = registrationById.MobileNo
                    };
                    return View(registrationVM);
                }
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                bool res = _registrationService.DeleteRegistration(id);
                if (res)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
