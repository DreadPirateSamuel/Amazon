using Amazon.Library.DTO;
using Amazon.Library.Utilities;
using Amazon.MAUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Amazon.Library.Services
{
    public class ShoppingCartServiceProxy
    {
        private ShoppingCartServiceProxy()
        {
            carts = new List<ShoppingCart>()
            {
                new ShoppingCart{CartID = 1,


                items = new List<ItemDTO>
                    {
                        new ItemDTO { Name = "Item 1", Description = "spicy", Id = 1, Price = 5.5M, Quantity = 3, IsBOGO = false, Markdown = 0 },
                        new ItemDTO { Name = "Item 2", Description = "medium", Id = 2, Price = 10M, Quantity = 15, IsBOGO = true, Markdown = 0 },
                        new ItemDTO { Name = "Item 3", Description = "mild", Id = 3, Price = 6M, Quantity = 4, IsBOGO = false, Markdown = 10 }
                    }



                }
            };
        }

        private static ShoppingCartServiceProxy? instance;
        private static object instanceLock = new object();
        public static ShoppingCartServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShoppingCartServiceProxy();
                    }
                }

                return instance;
            }
        }

        private List<ShoppingCart>? carts;
        public ReadOnlyCollection<ShoppingCart> Carts
        {
            get
            {
                return carts?.AsReadOnly();
            }
        }
        public ShoppingCart GetCartById(int cartId)
        {
            foreach(var i in carts)
            {
                if (i.CartID == cartId)
                {
                    return i;
                }
            }
            //find better way to do this.
            return carts[0];
        }

        public ReadOnlyCollection<ShoppingCart> GetFirstCart()
        {
            return new ReadOnlyCollection<ShoppingCart>(new List<ShoppingCart> { carts[0] });
        }

        private List<ItemDTO>? items;
        public ReadOnlyCollection<ItemDTO>? Items
        {
            get
            {
                int i = 0;
                if(LastId == 1)
                {
                    return carts[0].items?.AsReadOnly();
                }
                else if (i < LastId)
                {
                    while(i < LastId)
                    {
                        if (carts[i] == null)
                        {
                            i++;
                        }
                        else
                        {
                            return carts[i].items?.AsReadOnly();
                        }
                    }
                }
                ShoppingCart temp = new ShoppingCart();
                AddOrUpdate(temp);
                return temp.items?.AsReadOnly();
            }
        }

        //----------- Functionality -------------
        public ShoppingCart? Specific(int cartID)
        {
            foreach (var cart in carts)
            {
                if (cart.CartID == cartID)
                {
                    return cart;
                }
            }
            return null;
        }
        public int LastId
        {
            get
            {
                if (carts?.Any() ?? false)
                {
                    return carts?.Select(c => c.CartID)?.Max() ?? 0;
                }
                return 0;
            }
        }

        public ShoppingCart? AddOrUpdate(ShoppingCart c)
        {
            if (c == null)
            {
                return null;
            }

            bool isAdd = false;
            if (c.CartID == 0)
            {
                c.CartID = LastId + 1;
                isAdd = true;
            }

            if (isAdd == true)
            {
                carts?.Add(c);
            }

            return c;
        }

        public void Delete(int cartID)
        {
            if (carts == null)
            {
                return;
            }

            var cartToDelete = Specific(cartID);

            if (cartToDelete != null)
            {
                carts.Remove(cartToDelete);
            }
        }
        //-------- Dealing with items functionality --------------
        public async Task<ItemDTO?> AddItem(ShoppingCart c, ItemDTO i, int amount, ItemServiceProxy service)
        {
            int updatedQuantity = i.Quantity - amount;

            //If the item with a same ID is already in a cart.
            foreach (var item in c.items)
            {
                if (item.Id == i.Id)
                {
                    i.Quantity = updatedQuantity;  //Taking out of inventory.
                    JsonSerializerSettings settings = new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All,
                    };
                    var result = await new WebRequestHandler().Post("/Inventory", i);
                    item.Quantity = item.Quantity + amount;
                    return JsonConvert.DeserializeObject<ItemDTO>(result, settings);
                }
            }

            var tempItem = new ItemDTO { Name = i.Name, Description = i.Description, Price = i.Price, Quantity = amount, IsBOGO = i.IsBOGO, Markdown = i.Markdown, PriceOfItems = i.PriceOfItems };
            tempItem.Id = i.Id;

            if (0 < updatedQuantity)
            {
                i.Quantity = updatedQuantity;  //Taking out of inventory.
                JsonSerializerSettings settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All,
                };
                var result = await new WebRequestHandler().Post("/Inventory", i);
                if(updatedQuantity == 0)
                {
                    result = await new WebRequestHandler().Delete($"/{i.Id}");
                }
                
                


                c.items.Add(tempItem);
                return JsonConvert.DeserializeObject<ItemDTO>(result, settings);
            }
            else if (0 == updatedQuantity)
            {
                JsonSerializerSettings settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All,
                };
                var result = await new WebRequestHandler().Delete($"/{i.Id}");
                c.items.Add(tempItem);
                return JsonConvert.DeserializeObject<ItemDTO>(result, settings);
            }
            else
            {
                Console.WriteLine("That is more than what's in stock. Nothing added to cart.");
            }

            return tempItem;
        }

        public void DeleteItem(ShoppingCart c, int id, int amount, ItemServiceProxy service)
        {
            amount = 1;
            var tempItem = new ItemDTO();
            int updatedQuantity = 0;
            var itemInventory = service.Specific(id);

            foreach (var item in c.items)
            {
                if (item.Id == id)
                {
                    tempItem = new ItemDTO
                    {
                        Name = item.Name,
                        Description = item.Description,
                        Price = item.Price,
                        Quantity = itemInventory.Quantity,
                        Id = item.Id,
                        IsBOGO = item.IsBOGO,
                        Markdown = item.Markdown,
                        PriceOfItems = item.PriceOfItems,
                    };
                    updatedQuantity = tempItem.Quantity + amount;

                    if (amount == item.Quantity)
                    {
                        c.items.Remove(item);
                        AddBack();
                        break;
                    }
                    else if (amount < item.Quantity)
                    {
                        item.Quantity = item.Quantity - amount;
                        tempItem.Quantity = updatedQuantity;
                        service.AddOrUpdate(tempItem);
                    }
                    else
                    {
                        Console.WriteLine("That is more than what's in your cart. Nothing removed.");
                    }
                }
            }

            async void AddBack()
            {
                if (itemInventory != null)
                {
                    updatedQuantity = itemInventory.Quantity + amount;
                    itemInventory.Quantity = updatedQuantity;
                    JsonSerializerSettings settings = new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All,
                    };
                    var result = await new WebRequestHandler().Post("/Inventory", itemInventory);
                }
                else
                {
                    ItemDTO addBack = new ItemDTO
                    {
                        Name = tempItem.Name,
                        Description = tempItem.Description,
                        Price = tempItem.Price,
                        Quantity = amount
                    };

                    addBack.Id = id;
                    service.AddOrUpdate(addBack);
                }
            }
        }

        public void ListContents(ShoppingCart c)
        {
            if (!c.items.Any())
            {
                Console.WriteLine("No items found in your cart.");
            }
            else
            {
                Console.WriteLine("List of items in your cart:");
                Console.WriteLine("-----------------");
                foreach (var item in c.items)
                {
                    Console.WriteLine("ID: " + item.Id);
                    Console.WriteLine("Name: " + item.Name);
                    Console.WriteLine("Description: " + item.Description);
                    Console.WriteLine("Price: " + item.Price);
                    Console.WriteLine("Quantity: " + item.Quantity);
                    Console.WriteLine("- - - - - -");
                }
            }
        }

        public void Receipt(ShoppingCart c)
        {
            decimal total = 0.0m;
            Console.WriteLine("Itemized Reciept:");
            Console.WriteLine("--------------------");
            int s = 0;
            foreach (var item in c.items)
            {
                total = total + (item.Quantity * item.Price);
                Console.WriteLine($"[{item.Id}]: {item.Name}, (${item.Price}) | Qnty:" + item.Quantity);
                s++;
            }
            Console.WriteLine("--------------------");
            Console.WriteLine("Subtotal: $" + total);
            Console.WriteLine("Tax (7%): $" + (total * .07m));
            Console.WriteLine("Total: $" + (total + (total * .07m)));
        }

    }
}
