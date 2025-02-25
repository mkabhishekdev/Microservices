using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Domain
{
    public class InventoryItem
    {
        public int id { get; set; }          //1
        public string productName { get; set; }  //iphone
        public decimal price { get; set; }  //$500
        public string category { get; set; }  //electronics

        public string imageName { get; set; } //img
        public string imageURL { get; set; } //imgurl
        public string description { get; set; } //iphone description

        public int inventoryCategoryId { get; set; } //map to categoryId
        public InventoryCategory InventoryCategory { get; set; } //connect to inventorycategory class
    }
}