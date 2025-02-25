using Microsoft.Extensions.Logging;
using AbhiShop.Webmvc.Architecture;
using AbhiShop.Webmvc.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace AbhiShop.Webmvc.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IOptionsSnapshot<AppSettings> _settings;
        private readonly IHttpClient _apiClient;
        private readonly ILogger<InventoryService> _logger;
        private readonly string _remoteServiceBaseUrl;
        public InventoryService(IOptionsSnapshot<AppSettings> settings, IHttpClient httpClient, ILogger<InventoryService> logger)
        {
            _settings = settings;
            _apiClient = httpClient;
            _logger = logger;
            _remoteServiceBaseUrl = $"{_settings.Value.InventoryUrl}/api/inventory/";
        }

        public async Task<Inventory> GetInventoryItems(int page, int take, int? category)
        {
             var allinventoryItemsUri = ApiPaths.Inventory.GetAllInventoryItems(_remoteServiceBaseUrl, page, take, category);
             var dataString = await _apiClient.GetStringAsync(allinventoryItemsUri);
             var response = JsonConvert.DeserializeObject<Inventory>(dataString);
             return response;
        } 

        public async Task<IEnumerable<SelectListItem>> GetCategories()
        {
            var getCategoriesUri = ApiPaths.Inventory.GetAllCategories(_remoteServiceBaseUrl);
            var dataString = await _apiClient.GetStringAsync(getCategoriesUri);

            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value=null, Text="All", Selected = true}
            };
            var categories = JArray.Parse(dataString);

            foreach(var category in categories.Children<JObject>())
            {
                items.Add(new SelectListItem()
                {
                    Value = category.Value<string>("id"),
                    Text = category.Value<string>("category")
                });
            }
            return items;
        }
        


    }
}