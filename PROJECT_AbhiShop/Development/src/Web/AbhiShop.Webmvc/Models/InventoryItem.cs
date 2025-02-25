namespace AbhiShop.Webmvc.Models
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
        public int inventoryCategoryId { get; set;}
        public string inventoryCategory { get; set;}
    }
}
