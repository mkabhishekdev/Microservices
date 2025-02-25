using AbhiShop.Webmvc.Models;
using AbhiShop.Webmvc.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AbhiShop.Webmvc.ViewModels
{
    /* when buiding razor view for the front end page, this model class will be used*/
    public class InventoryIndexViewModel
    {
        public IEnumerable<InventoryItem> InventoryItems {get; set;} // think of drop down box
        public IEnumerable<SelectListItem> Categories{get;set;} //think of drop down box
        public int? CategoriesFilterApplied{get; set;}
        public PaginationInfo PaginationInfo{get; set;} //carries info about the paging parameters
    }
}
