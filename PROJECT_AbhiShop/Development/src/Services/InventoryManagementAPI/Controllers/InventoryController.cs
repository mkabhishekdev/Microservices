using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Domain;
using InventoryManagementAPI.ViewModels;


namespace InventoryManagementAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Inventory")]
    public class InventoryController : Controller
    {
        private readonly InventoryContext _inventoryContext;
        private readonly IOptionsSnapshot<InventorySettings> _settings;

        public InventoryController(InventoryContext inventoryContext, IOptionsSnapshot<InventorySettings> settings)
        {
            _inventoryContext = inventoryContext;
            _settings = settings;
         //   string url = settings.Value.ExternalInventoryBaseURL;
             ((DbContext)inventoryContext).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> InventoryCategories()
        {
           var items = await _inventoryContext.InventoryCategories.ToListAsync();
           return Ok(items);
        }

        [HttpGet]
        [Route("items/{id:int}")]
        public async Task<IActionResult> GetInventoryItemById(int id)
        {
           if(id <= 0)
           {
               return BadRequest();
           }
           var item = await _inventoryContext.InventoryItems.SingleOrDefaultAsync(x=>x.id==id);
           if(item!=null)
           {
               item.imageURL = item.imageURL.Replace("http://externalinventorybaseurltobereplaced",_settings.Value.ExternalInventoryBaseURL);
               return Ok(item);
           }
           return NotFound();
        }

        // GET api/Inventory/items?pageSize=5&pageIndex=0
        [HttpGet]
        [Route("[action]")] //this way is known as using "replacement token"
        public async Task<IActionResult> Items([FromQuery] int pageSize = 5, [FromQuery] int pageIndex = 0)
        {
              var totalItems = await _inventoryContext.InventoryItems
                                .LongCountAsync();
              
              var itemsOnPage = await _inventoryContext.InventoryItems
                                .OrderBy(x=>x.productName)
                                .Skip(pageSize*pageIndex)
                                .Take(pageSize)        
                                .ToListAsync();
              itemsOnPage = ChangeURLPlaceHolder(itemsOnPage);
              var invModel = new PaginatedItemsViewModel<InventoryItem>(pageIndex,pageSize,totalItems,itemsOnPage);  
              return Ok(invModel);          
        }

        //GET api/Inventory/items/withname/electronics?pageSize=1&pageIndex=0
        [HttpGet]
        [Route("[action]/withname/{name:minlength(1)}")]
        public async Task<IActionResult> Items(string name, [FromQuery] int pageSize = 5, [FromQuery] int pageIndex = 0)
        {
              var totalItems = await _inventoryContext.InventoryItems
                                .Where(x=>x.productName.StartsWith(name))
                                .LongCountAsync();
              var itemsOnPage = await _inventoryContext.InventoryItems
                                .Where(x=>x.productName.StartsWith(name))
                                .OrderBy(x=>x.productName)
                                .Skip(pageSize*pageIndex)
                                .Take(pageSize)        
                                .ToListAsync();
              itemsOnPage = ChangeURLPlaceHolder(itemsOnPage);
              var invModel = new PaginatedItemsViewModel<InventoryItem>(pageIndex,pageSize,totalItems,itemsOnPage);  
              return Ok(invModel);          
        }
        
        //GET api/Inventory/Items/category/1?pageSize=1&pageIndex=0
        [HttpGet]
        [Route("[action]/category/{invCategoryId}")] 
        public async Task<IActionResult> Items(int? invCategoryId, [FromQuery] int pageSize = 5, [FromQuery] int pageIndex = 0)
        {
              var root = (IQueryable<InventoryItem>)_inventoryContext.InventoryItems; // using iqueryable: the query is not yet ready to be applied to db, will not send this to db
              if(invCategoryId.HasValue)
              {
                  root = root.Where(c=>c.inventoryCategoryId == invCategoryId);
              }
              var totalItems = await root                               
                                .LongCountAsync();
              var itemsOnPage = await root                              
                                .OrderBy(x=>x.productName)
                                .Skip(pageSize*pageIndex)
                                .Take(pageSize)        
                                .ToListAsync();
              itemsOnPage = ChangeURLPlaceHolder(itemsOnPage);
              var invModel = new PaginatedItemsViewModel<InventoryItem>(pageIndex,pageSize,totalItems,itemsOnPage);  
              return Ok(invModel);          
        }

       [HttpPost]
       [Route("items")]
       public async Task<IActionResult> CreateInventoryProduct([FromBody] InventoryItem product)
       {
           var item = new InventoryItem
           {
                inventoryCategoryId = product.inventoryCategoryId,
                productName = product.productName,
                price = product.price,
                imageName = product.imageName,
                description = product.description
           };
           _inventoryContext.InventoryItems.Add(item);
           await _inventoryContext.SaveChangesAsync();
           return CreatedAtAction(nameof(GetInventoryItemById), new{id = item.id});
       }

        [HttpPut]
        [Route("items")]
        public async Task<IActionResult> UpdateProduct([FromBody] InventoryItem productToUpdate)
        {
            var inventoryItem = await _inventoryContext.InventoryItems
                                .SingleOrDefaultAsync(i=>i.id == productToUpdate.id);
            if(inventoryItem == null)
            {
                return NotFound(new {Message = $"Product with id {productToUpdate.id} not found"});
            }
            inventoryItem = productToUpdate;
            _inventoryContext.InventoryItems.Update(inventoryItem);
            await _inventoryContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetInventoryItemById),new{ id = productToUpdate.id});
        }
        
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteInventoryProduct(int id)
        {
            var product = await _inventoryContext.InventoryItems.SingleOrDefaultAsync(x=>x.id == id);
            if(product == null)
            {
                return NotFound();
            }
            _inventoryContext.InventoryItems.Remove(product);
            await _inventoryContext.SaveChangesAsync();
            return NoContent();
        }

        private List<InventoryItem> ChangeURLPlaceHolder(List<InventoryItem> items) //helper class to convert images on a collection of items
        {
            items.ForEach(x=>
             x.imageURL = x.imageURL.Replace("http://externalinventorybaseurltobereplaced",_settings.Value.ExternalInventoryBaseURL));
             return items;
        }
    }
}
