using Amazon.MAUI.Models;
using Amazon.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Amazon.Library.DTO;

namespace Amazon.MAUI.ViewModels
{
    public class ItemViewModel
    {
        public ICommand? AddCommand { get; private set; }
        public ICommand? UpdateCommand { get; private set; }
        public ICommand? DeleteCommand { get; private set; }

        public int ID;
        public ItemDTO? Item;
        public ItemViewModel()
        {
            Item = new ItemDTO();
            SetupCommands();
        }

        public ItemViewModel(int id)
        {
            Item = ItemServiceProxy.Current?.Items?.FirstOrDefault(i => i.Id == id);
            if(Item == null)
            {
                Item = new ItemDTO();
            }
            SetupCommands();
        }
        public ItemViewModel(ItemDTO it)
        {
            Item = it;
            SetupCommands();
        }

        public ItemDTO ToItemDTO()
        {
            return new ItemDTO
            {
                Id = Item.Id,
                Name = Item.Name,
                Description = Item.Description,
                Price = Item.Price,
                Quantity = Item.Quantity,
                IsBOGO = Item.IsBOGO,
                Markdown = Item.Markdown,
                PriceOfItems = Item.PriceOfItems,

            };
        }

        public void SetupCommands()
        {
            UpdateCommand = new Command((it) => ExecuteUpdate(it as ItemViewModel));
            DeleteCommand = new Command((it) => ExecuteDelete(it as ItemViewModel));
            AddCommand = new Command((it) => ExecuteAdd(it as ItemViewModel));
        }

        private void ExecuteAdd(ItemViewModel? it)
        {
            //Parameter is no longer used after Assignment 4.
            //Item here is the desired ItemDTO.
            ItemServiceProxy.Current.AddOrUpdate(Item);
        }
        private void ExecuteDelete(ItemViewModel? it)
        {
            if (it == null)
            {
                return;
            }
            ItemServiceProxy.Current.Delete(it.Item.Id);
        }
        private void ExecuteUpdate(ItemViewModel? it)
        {
            if(it?.Item == null)
            {
                return;
            }
            ItemServiceProxy.Current.AddOrUpdate(Item);
        }

        public string? Name
        {
            get
            {
                return Item?.Name;
            }

            set
            {
                if (Item != null)
                {
                    Item.Name = value;
                }
            }
        }

        public string? Description
        {
            get
            {
                return Item?.Description;
            }

            set
            {
                if (Item != null)
                {
                    Item.Description = value;
                }
            }
        }

        public string? DisplayId
        {
            get
            {
                return Item?.DisplayId;
            }
        }

        public decimal Price
        {
            get
            {
                return Item.Price;
            }

            set
            {
                if (Item != null)
                {
                    Item.Price = value;
                }
            }
        }

        public int Quantity
        {
            get
            {
                return Item.Quantity;
            }

            set
            {
                if (Item != null)
                {
                    Item.Quantity = value;
                }
            }
        }

        public bool IsBOGO
        {
            get
            {
                if (Item.IsBOGO == true)
                {
                    return true;
                }
                return false;
            }

        }
        public string? BOGO
        {
            get
            {
                if (Item.IsBOGO == true)
                {
                    return "Y";
                }
                else
                {
                    return "N";
                }
            }

            set
            {
                if (Item != null)
                {
                    if (value == "Y")
                    {
                        Item.IsBOGO = true;
                    }
                    else
                    {
                        Item.IsBOGO = false;
                    }

                }
            }
        }

        public int Markdown
        {
            get
            {
                return Item.Markdown;
            }

            set
            {
                if (Item != null)
                {
                    Item.Markdown = value;
                }
            }
        }

        public decimal PriceOfItems
        {
            get
            {
                decimal totalPrice = Price * Quantity;

                if (Item.IsBOGO)
                {
                    // Assuming BOGO means Buy One Get One Free
                    int effectiveQuantity = Quantity / 2 + Quantity % 2;
                    totalPrice = Price * effectiveQuantity;
                }

                if (Item.Markdown > 0)
                {
                    decimal markdownAmount = (Item.Markdown / 100m) * totalPrice;
                    totalPrice -= markdownAmount;
                }

                return totalPrice;
            }
        }
    }
}
