using Microsoft.AspNetCore.Mvc.Rendering;
using AbhiShop.Webmvc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AbhiShop.Webmvc.Services
{
    public interface IInventoryService
    {
         Task<Inventory> GetInventoryItems(int page, int take, int? category);
         Task<IEnumerable<SelectListItem>> GetCategories();       
    }
}
