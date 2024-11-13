using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.MAUI.Models;

namespace Amazon.Library.DTO
{
    public class ItemDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string DisplayId => $"[{Id}]";
        public bool IsBOGO { get; set; }
        public int Markdown { get; set; }
        public decimal PriceOfItems { get; set; }

        public ItemDTO() { }
        public ItemDTO(Item i)
        {
            Name = i.Name;
            Description = i.Description;
            Price = i.Price;
            Id = i.Id;
            Quantity = i.Quantity;
            IsBOGO = i.IsBOGO;
            Markdown = i.Markdown;
            PriceOfItems = i.PriceOfItems;
        }

        public ItemDTO(ItemDTO i)
        {
            Name = i.Name;
            Description = i.Description;
            Price = i.Price;
            Id = i.Id;
            Quantity = i.Quantity;
            IsBOGO = i.IsBOGO;
            Markdown = i.Markdown;
            PriceOfItems = i.PriceOfItems;
        }
    }
}
