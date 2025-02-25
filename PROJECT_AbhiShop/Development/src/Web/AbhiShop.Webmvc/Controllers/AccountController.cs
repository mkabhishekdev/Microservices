using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Diagnostics;

namespace AbhiShopMvc.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        [Authorize]
        public async Task<IActionResult> SignIn(string returnUrl)
        {
            var user = User as ClaimsPrincipal;

            var token = await HttpContext.GetTokenAsync("access_token");
            var idToken = await HttpContext.GetTokenAsync("id_token");
            foreach (var claim in user.Claims)
            {
                Debug.WriteLine($"Claim Type: {claim.Type} - Claim Value : {claim.Value}");
            }

            if (token != null)
            {
                ViewData["access_token"] = token;

            }
            if (idToken != null)
            {

                ViewData["id_token"] = idToken;
            }
         
            return RedirectToAction(nameof(InventoryController.About), "Inventory");
        }


        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

            var homeUrl = Url.Action(nameof(InventoryController.Index), "Inventory");
            return new SignOutResult(OpenIdConnectDefaults.AuthenticationScheme,
                new AuthenticationProperties { RedirectUri = homeUrl });
        }

    }
}

 