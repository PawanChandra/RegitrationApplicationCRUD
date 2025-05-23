using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RegistrationApp.Models;
using RegistrationApp.ServiceRepository;
using System.Security.Claims;

namespace RegistrationApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRegistrationService _registrationService;
        public AccountController(IRegistrationService _registrationService)
        {
            this._registrationService = _registrationService;      
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var userData = _registrationService.GetRegistrations();
                var user=userData.Where(u=> u.Email==loginVM.Email && u.Password==loginVM.Password).FirstOrDefault();
                if(user!=null)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim(ClaimTypes.Name,user.Name),
                        new Claim(ClaimTypes.Role,user.Email)
                    };

                    //adding claims into identity
                    var identity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

                    //adding indetity into principle
                    var principle = new ClaimsPrincipal(identity);

                    //addiing identity and principle into signAsyncMethod
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principle,new AuthenticationProperties
                    {
                        IsPersistent=false
                    });

                    HttpContext.Session.SetString("UserId",user.Id.ToString());
                    HttpContext.Session.SetString("Name",user.Name);
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
