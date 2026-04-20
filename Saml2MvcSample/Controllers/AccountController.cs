using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Saml2MvcSample.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return BadRequest("Invalid return URL.");
            }
            var props = new AuthenticationProperties
            {
                RedirectUri = returnUrl ?? Url.Action("Claims", "Account")
            };
            return Challenge(props);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Claims()
        {
            return View(User.Claims);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Logout()
        {
            return SignOut(
                new AuthenticationProperties
                {
                    RedirectUri = "/"
                },
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
