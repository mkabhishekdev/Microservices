using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Domain
{
    public class InventoryCategory
    {
        public int Id { get; set; } //1
        public string invCategory{ get; set; }  //electronics
    }
}

/*
categoryId-invCategory
1-electronics
2-books
3-sports
4-food
 */