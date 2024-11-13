using Amazon.Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Amazon.MAUI.Models
{
    public class Item
    {

        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string DisplayId => $"[{Id}]";
        public bool IsBOGO { get; set; }
        public int Markdown {  get; set; }
        public decimal PriceOfItems { get; set; }

        public Item()
        {
            Name = "";
            Description = "";
            Price = 0;
            Id = 0;
            Quantity = 0;
            Markdown = 0;
            IsBOGO= false;
            PriceOfItems = 0;
        }

        public Item(string name, string desc, decimal price, int quantity, bool bogo, int mark)
        {
            Name = name;
            Description = desc;
            Price = price;
            Id = 0;
            Quantity = quantity;
            IsBOGO= bogo;
            Markdown = mark;
            PriceOfItems= 0;
        }

        public Item(ItemDTO d)
        {
            Name = d.Name;
            Description = d.Description;
            Price = d.Price;
            Id = d.Id;
            Quantity = d.Quantity;
            IsBOGO = d.IsBOGO;
            Markdown = d.Markdown;
            PriceOfItems = d.PriceOfItems;
        }
    }
}
