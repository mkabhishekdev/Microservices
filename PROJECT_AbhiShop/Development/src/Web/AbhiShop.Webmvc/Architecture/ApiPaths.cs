namespace AbhiShop.Webmvc.Architecture
{
    // join together request paths for each microservice function
    public class ApiPaths
    {
        public static class Inventory
        {
            public static string GetAllInventoryItems(string baseUri, int page, int take, int? category)
            {
                var filterQs = "";

                if (category.HasValue)
                {
                    var categoryQs = (category.HasValue) ? category.Value.ToString() : "null";
                    filterQs = $"/category/{categoryQs}";
                }

                return $"{baseUri}items{filterQs}?pageIndex={page}&pageSize={take}";
            }

            public static string GetInventoryItem(string baseUri, int id)
            {
                return $"{baseUri}/items/{id}";
            }

            public static string GetAllCategories(string baseUri)
            {
                return $"{baseUri}inventoryCategories";
            }

        }
    }
}