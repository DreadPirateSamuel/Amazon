using Amazon.Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.MAUI.Models
{
    public class ShoppingCart
    {
        public int CartID { get; set; }
        public List<ItemDTO> items { get; set; }
        public string DisplayId => $"[{CartID}]";

        public ShoppingCart()
        {
            CartID = 0;
            items = new List<ItemDTO>();
        }
    }
}
