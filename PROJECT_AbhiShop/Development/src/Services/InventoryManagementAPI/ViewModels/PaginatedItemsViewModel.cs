using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementAPI.ViewModels
{
    //To create a collection of items
    public class PaginatedItemsViewModel<TEntity> where TEntity:class
    {
        public int PageSize{ get; private set;}
        public int PageIndex { get; private set;}

        public long Count{get; private set;}

        public IEnumerable<TEntity> Data {get; private set;}

        public PaginatedItemsViewModel(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
        {
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.Count = count;
            this.Data = data;
        }
    }
}
