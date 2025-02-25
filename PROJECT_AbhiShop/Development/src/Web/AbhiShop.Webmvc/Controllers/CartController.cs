using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Diagnostics;

namespace AbhiShopMvc.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        [HttpPost]
        public IActionResult AddToCart()
        {
            string name ="Secured Method";
            return View(name);

        }
    }
}