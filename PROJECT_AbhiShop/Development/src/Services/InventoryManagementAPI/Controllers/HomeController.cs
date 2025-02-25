using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementAPI.Controllers
{
    public class HomeController : Controller
    {
         

        // Had commented the below part, which was causing the redirect issue in the startup file
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
        
     /*  public IActionResult Index()
        {
          return View("Index");
        } */
    }
}