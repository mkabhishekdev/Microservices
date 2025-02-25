using System.Collections.Generic;

namespace AbhiShop.Webmvc.Models
{
    public class Inventory
    {
        public int PageIndex {get; set;}
        public int PageSize { get; set;}
        public int Count{get;set;}
        public List<InventoryItem> Data{get;set;}

    }
}
