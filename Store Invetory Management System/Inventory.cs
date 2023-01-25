using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Invetory_Management_System
{
    internal class Inventory
    {

        public int Id {get; set;}

        public int ProductId { get; set; }

        public int StoreId { get; set; }

        public int Quantity { get; set; }

        public string? Aisle { get; set; }

    }
}
