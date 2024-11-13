using Amazon.MAUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Amazon.Library.Utilities;
using Amazon.Library.DTO;

namespace Amazon.Library.Services
{
    public class ItemServiceProxy
    {
        private ItemServiceProxy()
        {
            /*
            items = new List<Item>() {

                new Item{Name = "Item 1", Description = "spicy", Id = 1, Price = 5.5M, Quantity = 3, IsBOGO = false, Markdown = 0},
                new Item{Name = "Item 2", Description = "mild", Id = 2, Price = 10M, Quantity = 15, IsBOGO = true, Markdown = 0},
                new Item{Name = "Item 3", Description = "medium", Id = 3, Price = 6M, Quantity = 4, IsBOGO = false, Markdown = 10},

            };*/

            //TODO: Make a web call.
            var response = new WebRequestHandler().Get("/Inventory").Result;
            items = JsonConvert.DeserializeObject<List<ItemDTO>>(response);
        }

        private static ItemServiceProxy? instance;
        private static object instanceLock = new object();
        public static ItemServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ItemServiceProxy();
                    }
                }

                return instance;
            }
        }

        private List<ItemDTO>? items;
        public ReadOnlyCollection<ItemDTO>? Items
        {
            get
            {
                var response = new WebRequestHandler().Get("/Inventory").Result;
                items = JsonConvert.DeserializeObject<List<ItemDTO>>(response);
                return items?.AsReadOnly();
            }
        }

        public void EnforceChanges()
        {
            var response = new WebRequestHandler().Get("/Inventory").Result;
            if(response != null)
            {
                items = JsonConvert.DeserializeObject<List<ItemDTO>>(response);
            }
            
        }
        //------------ Functionality -------------
        public ItemDTO? Specific(int id)
        {
            foreach (var item in Items)
            {
                if (item.Id.Equals(id))
                {
                    return item;
                }
            }

            return null;
        }
        public int LastId
        {
            get
            {
                if (items?.Any() ?? false)
                {
                    return items?.Select(c => c.Id)?.Max() ?? 0;
                }
                return 0;
            }
        }
        public async Task<ItemDTO?> AddOrUpdate(ItemDTO item)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
            };


            var result = await new WebRequestHandler().Post("/Inventory", item);
            
            return JsonConvert.DeserializeObject<ItemDTO>(result, settings);

        }
        public async Task<ItemDTO?> Delete(int id)
        {
            var response = await new WebRequestHandler().Delete($"/{id}");
            var itemToDelete = JsonConvert.DeserializeObject<ItemDTO>(response);
            return itemToDelete;
        }
        
        //changed to list
        public async Task<List<ItemDTO?>> AddCSV(List<ItemDTO> its)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
            };

            List<Task<string>> tasks = new List<Task<string>>();
            WebRequestHandler webRequestHandler = new WebRequestHandler();

            // Add the first item to the list of tasks
            tasks.Add(webRequestHandler.Put("/Inventory", its[0]));

            // Add the remaining items to the list of tasks, skipping items with Id == 1
            foreach (var i in its.Skip(1))
            {
                if (i.Id != 1)
                {
                    tasks.Add(webRequestHandler.Put("/Inventory", i));
                }
            }

            // Wait for all tasks to complete
            string[] results = await Task.WhenAll(tasks);

            List<ItemDTO> itemDTOs = new List<ItemDTO>();

            foreach (var result in results)
            {
                // Split the result if it contains concatenated JSON objects
                var jsonObjects = result.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var jsonObject in jsonObjects)
                {
                    itemDTOs.Add(JsonConvert.DeserializeObject<ItemDTO>(jsonObject, settings));
                }
            }

            return itemDTOs;

        }

        public void ListInventory()
        {
            Console.WriteLine("Here is a list of items in our inventory: ");
            foreach (var item in this.Items)
            {
                Console.WriteLine("ID number " + item.Id + ":");
                Console.WriteLine("Name: " + item.Name + ", " + " Description: " + item.Description);
                Console.WriteLine("The price is $" + item.Price + " and there are " + item.Quantity + " in stock.");
                Console.WriteLine("- - - - - - - -");
            }
        }


    }
}
