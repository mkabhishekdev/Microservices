using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AbhiShop.Webmvc.Models;
using AbhiShop.Webmvc.Services;
using AbhiShop.Webmvc.ViewModels;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace AbhiShopMvc.Controllers
{
    public class InventoryController : Controller
    {
        private IInventoryService _inventorySvc;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventorySvc = inventoryService;
        }

        public async Task<IActionResult> Index(int? categoriesFilterApplied, int? page)
        {
            int itemsPage=5;
            var inventory = await _inventorySvc.GetInventoryItems(page ?? 0,itemsPage, categoriesFilterApplied); //if no page indicated, 0th page will appear
            var vm = new InventoryIndexViewModel()
            {
                InventoryItems = inventory.Data,
                Categories = await _inventorySvc.GetCategories(),
                CategoriesFilterApplied = categoriesFilterApplied ?? 0,
                PaginationInfo = new PaginationInfo
                {
                    ActualPage = page ?? 0,
                    ItemsPerPage = itemsPage,
                    TotalItems = inventory.Count,
                    TotalPages = (int)Math.Ceiling(((decimal)inventory.Count/itemsPage))
                }
            };
            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1)?"is-disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";
            return View(vm);
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["message"] = "AbhiShop is a application demonstrating Microservices architecture";
            
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "https://www.linkedin.com/in/abhikemp/";
            
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
