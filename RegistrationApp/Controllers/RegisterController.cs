using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            var countries = _registrationService.GetCountries();
            ViewData["Countries"] = countries;
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
                    return RedirectToAction("Login", "Account");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            string? userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return RedirectToAction("Login", "Account");
            }
            if (id != Guid.Empty)
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
                        MobileNo = registrationById.MobileNo,
                        CountryId = registrationById.CountryId,
                        StateId = registrationById.StateId,
                        CityId = registrationById.CityId,
                        Countries = _registrationService.GetCountries().Select(c => new SelectListItem
                        {
                            Value = c.Id.ToString(),
                            Text = c.CountryName
                        }),
                        States = _registrationService.GetStatesByCountry(registrationById.CountryId).Select(s => new SelectListItem
                        {
                            Value = s.Id.ToString(),
                            Text = s.StateName
                        }),
                        Cities = _registrationService.GetCitiesByState(registrationById.StateId).Select(ci => new SelectListItem
                        {
                            Value = ci.Id.ToString(),
                            Text = ci.CityName
                        })
                    };
                    return View(updateRegistrationVM);
                }
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public IActionResult Edit(UpdateRegistrationVM registrationRequest)
        {
            string? userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return RedirectToAction("Login", "Account");
            }
            if (registrationRequest.Id != Guid.Empty)
            {
                bool res = _registrationService.UpdateRegistration(registrationRequest);
                if(res)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult Details(Guid id)
        {
            string? userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return RedirectToAction("Login", "Account");
            }
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
                        MobileNo = registrationById.MobileNo,
                        CountryId = registrationById.CountryId,
                        StateId = registrationById.StateId,
                        CityId = registrationById.CityId,
                        Countries = _registrationService.GetCountries().Select(c => new SelectListItem
                        {
                            Value = c.Id.ToString(),
                            Text = c.CountryName
                        }),
                        States = _registrationService.GetStatesByCountry(registrationById.CountryId).Select(s => new SelectListItem
                        {
                            Value = s.Id.ToString(),
                            Text = s.StateName
                        }),
                        Cities = _registrationService.GetCitiesByState(registrationById.StateId).Select(ci => new SelectListItem
                        {
                            Value = ci.Id.ToString(),
                            Text = ci.CityName
                        })
                    };
                    return View(registrationVM);
                }
            }
            return RedirectToAction("Login", "Account");
        }
        public IActionResult Delete(Guid id)
        {
            string? userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return RedirectToAction("Login", "Account");
            }
            if (id != Guid.Empty)
            {
                bool res = _registrationService.DeleteRegistration(id);
                if (res)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            return RedirectToAction("Login", "Account");
        }
        public JsonResult GetStatesByCountry(Guid countryId)
        {
            var states=_registrationService.GetStatesByCountry(countryId);
            return Json(states);
        }
        public JsonResult GetCitiesByState(Guid stateId)
        {
            var cities = _registrationService.GetCitiesByState(stateId);
            return Json(cities);
        }
    }
}
