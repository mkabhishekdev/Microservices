using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagementAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementAPI.Data
{
    public class InventorySeed
    {
        public static async Task SeedAsync(InventoryContext context)
        {
            context.Database.Migrate();
            if(!context.InventoryCategories.Any())
            {
             context.InventoryCategories.AddRange(GetPreconfiguredInventoryCategory());
             await context.SaveChangesAsync();
            }
            if(!context.InventoryItems.Any())
            {
             context.InventoryItems.AddRange(GetPreconfiguredInventoryItem());
             await context.SaveChangesAsync();
            }
        }
        static IEnumerable<InventoryCategory> GetPreconfiguredInventoryCategory()
        {
            return new List<InventoryCategory>()
            {
                new InventoryCategory(){invCategory = "Electronics"},
                new InventoryCategory(){invCategory = "Books"},
                new InventoryCategory(){invCategory = "Food"},
                new InventoryCategory(){invCategory = "Sports"},
            };
        }

        static IEnumerable<InventoryItem> GetPreconfiguredInventoryItem()
        {
            return new List<InventoryItem>()
            {
              new InventoryItem(){inventoryCategoryId=1,productName="headphones",price=80,category="electronics",imageName="Beats",imageURL="http://externalinventorybaseurltobereplaced/api/Image/1",description="headphones by Beats"},
              new InventoryItem(){inventoryCategoryId=1,productName="iphone",price=450,category="electronics",imageName="Apple",imageURL="http://externalinventorybaseurltobereplaced/api/Image/2",description="mobile by Apple"},
              new InventoryItem(){inventoryCategoryId=1,productName="keyboard",price=50,category="electronics",imageName="HP",imageURL="http://externalinventorybaseurltobereplaced/api/Image/3",description="keyboard by HP"},
              new InventoryItem(){inventoryCategoryId=1,productName="monitor",price=250,category="electronics",imageName="LG",imageURL="http://externalinventorybaseurltobereplaced/api/Image/4",description="monitor by LG"},
              new InventoryItem(){inventoryCategoryId=1,productName="mouse",price=20,category="electronics",imageName="Samsung",imageURL="http://externalinventorybaseurltobereplaced/api/Image/5",description="mouse by Samsung"},
              new InventoryItem(){inventoryCategoryId=1,productName="speakers",price=200,category="electronics",imageName="Bose",imageURL="http://externalinventorybaseurltobereplaced/api/Image/6",description="speakers by Bose"},
              new InventoryItem(){inventoryCategoryId=2,productName="elonmusk",price=70,category="books",imageName="Elon",imageURL="http://externalinventorybaseurltobereplaced/api/Image/7",description="book on elon musk"},
              new InventoryItem(){inventoryCategoryId=2,productName="malgudidays",price=20,category="books",imageName="IndianBook",imageURL="http://externalinventorybaseurltobereplaced/api/Image/8",description="story book by Indian writer"},
              new InventoryItem(){inventoryCategoryId=2,productName="microservices",price=250,category="books",imageName="TechBook",imageURL="http://externalinventorybaseurltobereplaced/api/Image/9",description="technology book"},
              new InventoryItem(){inventoryCategoryId=2,productName="RIT",price=10,category="books",imageName="SchoolMagazine",imageURL="http://externalinventorybaseurltobereplaced/api/Image/10",description="university magazine"},
              new InventoryItem(){inventoryCategoryId=2,productName="stevejobs",price=90,category="books",imageName="Book",imageURL="http://externalinventorybaseurltobereplaced/api/Image/11",description="book on SteveJobs"},
              new InventoryItem(){inventoryCategoryId=3,productName="hersheys",price=5,category="food",imageName="Chocolate",imageURL="http://externalinventorybaseurltobereplaced/api/Image/12",description="chocolate by hersheys"},
              new InventoryItem(){inventoryCategoryId=3,productName="oats",price=10,category="food",imageName="Food",imageURL="http://externalinventorybaseurltobereplaced/api/Image/13",description="breakfast cereals"},
              new InventoryItem(){inventoryCategoryId=3,productName="proteinbars",price=15,category="food",imageName="Bars",imageURL="http://externalinventorybaseurltobereplaced/api/Image/14",description="protein bars"},
              new InventoryItem(){inventoryCategoryId=3,productName="water",price=20,category="food",imageName="Water",imageURL="http://externalinventorybaseurltobereplaced/api/Image/15",description="water"},
              new InventoryItem(){inventoryCategoryId=3,productName="whey",price=25,category="food",imageName="Protein powder",imageURL="http://externalinventorybaseurltobereplaced/api/Image/16",description="whey protein"},
              new InventoryItem(){inventoryCategoryId=4,productName="boxinggloves",price=70,category="sports",imageName="Boxing gloves",imageURL="http://externalinventorybaseurltobereplaced/api/Image/17",description="boxing gloves"},
              new InventoryItem(){inventoryCategoryId=4,productName="cricketbat",price=45,category="sports",imageName="Cricket bat",imageURL="http://externalinventorybaseurltobereplaced/api/Image/18",description="cricket bat"},
              new InventoryItem(){inventoryCategoryId=4,productName="dumbell",price=15,category="sports",imageName="Dumbell",imageURL="http://externalinventorybaseurltobereplaced/api/Image/19",description="dumbell"},
              new InventoryItem(){inventoryCategoryId=4,productName="football",price=20,category="sports",imageName="Football",imageURL="http://externalinventorybaseurltobereplaced/api/Image/20",description="football"},
              new InventoryItem(){inventoryCategoryId=4,productName="icehockey",price=25,category="sports",imageName="Ice hockey",imageURL="http://externalinventorybaseurltobereplaced/api/Image/21",description="ice hockey"},
            };
        }
        
    }
}