using Amazon.MAUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace Amazon.API.Database
{
    public class Filebase
    {
        private string _root;
        private static Filebase _instance;


        public static Filebase Current
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }

        public int LastItemId
        {
            get
            {
                if (Items?.Any() ?? false)
                {
                    return Items?.Select(c => c.Id)?.Max() ?? 0;
                }
                return 0;
            }
        }

        private Filebase()
        {
            _root = @"C:\temp\Items";
        }

        public Item AddOrUpdate(Item item)
        {
            //set up a new Id if one doesn't already exist
            if(item.Id <= 0)
            {
                item.Id = (LastItemId + 1);
            }

            //go to the right place]
            string path = $"{_root}\\{item.Id}.json";

            //if the item has been previously persisted
            if(File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(item));

            //return the item, which now has an id
            return item;
        }


        public List<Item> Items 
        {
            get
            {
                var root = new DirectoryInfo(_root);
                var items = new List<Item>();
                foreach (var appFile in root.GetFiles())
                {
                    var item = JsonConvert.DeserializeObject<Item>(File.ReadAllText(appFile.FullName));
                    if(item != null)
                    {
                        items.Add(item);
                    }
                }
                return items;
            }
        }

        public bool Delete(int id)
        {
            //TODO: refer to AddOrUpdate for an idea of how you can implement this.
            string path = $"{_root}\\{id}.json";

            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }

            return false;
        }
    }
    
}
