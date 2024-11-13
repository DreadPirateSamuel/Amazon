using Amazon.API.Database;
using Amazon.Library.DTO;
using Amazon.MAUI.Models;

namespace Amazon.API.EC
{
    public class InventoryEC
    {
        private IEnumerable<ItemDTO> Items {  get; set; }
        public InventoryEC()
        {

        }

        public async Task<IEnumerable<ItemDTO>> Get()
        {
            //return FakeDatabase.Items.Take(100).Select(i => new ItemDTO(i));

            //FOR ASSIGNMENT 5:
            return Filebase.Current.Items.Take(100).Select(i => new ItemDTO(i));
        }

        public async Task<ItemDTO?> Delete(int id)
        {
            //var itemToDelete = FakeDatabase.Items.FirstOrDefault(i => i.Id == id);

            //FOR ASSIGNMENT 5:
            var itemToDelete = Filebase.Current.Items.FirstOrDefault(i => i.Id == id);

            if (itemToDelete == null)
            {
                return null;
            }

            //FakeDatabase.Items.Remove(itemToDelete);

            //FOR ASSIGNMENT 5:
            bool temp = Filebase.Current.Delete(id);
                
            return new ItemDTO(itemToDelete);
            

        }
        public async Task<ItemDTO> AddOrUpdate(ItemDTO item)
        {
            //ASSIGNMENT 4 STUFF
            if (item == null)
            {
                return null;
            }

            bool isAdd = false;
            if (item.Id == 0)
            {
                item.Id = Filebase.Current.LastItemId + 1;
                isAdd = true;
            }

            bool addBack = false;
            if (item.Id > 0)
            {
                foreach (var i in Filebase.Current.Items)
                {
                    if (item.Id.Equals(i.Id))
                    {
                        addBack = true;
                        //i.Quantity = i.Quantity + item.Quantity;
                        break;
                    }
                }
            }

            if (isAdd == true)
            {
                //FakeDatabase.Items.Add(new Item(item));

                //FOR ASSIGNMENT 5:
                Filebase.Current.Items.Add(new Item(item));
            }
            else
            {
                
                var itemToUpdate = Filebase.Current.Items.FirstOrDefault(i => i.Id == item.Id);
                if (itemToUpdate != null)
                {
                    int counter = 0;
                    int index = 0;
                    foreach(var i in Filebase.Current.Items)
                    {
                        if(i.Id ==  itemToUpdate.Id)
                        {
                            index = counter;
                            break;
                        }
                        counter++;
                    }
                    //FakeDatabase.Items.RemoveAt(index);

                    //FOR ASSIGNMENT 5:
                    Filebase.Current.Items.RemoveAt(index);

                    itemToUpdate = new Item(item);
                    //FakeDatabase.Items.Insert(index, itemToUpdate);

                    //FOR AASSIGNMENT 5:
                    Filebase.Current.Items.Insert(index, itemToUpdate);
                }
            }

            //return item;

            //FOR ASSIGNMENT 5:
            return new ItemDTO(Filebase.Current.AddOrUpdate(new Item(item)));
        }

        public async Task<ItemDTO> AddESC(ItemDTO item)
        {
            if (item == null)
            {
                return null;
            }
            //FakeDatabase.Items.Add(new Item(item));
            //return item;

            //FOR ASSIIGNMENT 5:
            return new ItemDTO(Filebase.Current.AddOrUpdate(new Item(item)));
        }
    }
}
