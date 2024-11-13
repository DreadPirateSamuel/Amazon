using Amazon.Library.DTO;
using Amazon.Library.Services;
using Amazon.MAUI.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Amazon.MAUI.ViewModels
{
    public class ShopManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        public ShopManagementViewModel()
        {
            Carts = new List<ShoppingCart>(ShoppingCartServiceProxy.Current.Carts);

        }

        public int TestCartID { get; set; }
        public int? CartID
        {
            get
            {
                return ShoppingCartServiceProxy.Current.Carts.FirstOrDefault().CartID;
            }
        }
        public ShoppingCart SelectedCart { get; set; }
        public List<ShoppingCart> Carts { get; set; }

        private ShoppingCart _firstCart;
        public ShoppingCart FirstCart
        {
            get => _firstCart;
            set
            {
                if (_firstCart != value)
                {
                    _firstCart = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal TotalPrice {
            get
            {
                if (FirstCart == null)
                    return 0m;

                decimal total = 0m;
                foreach (var item in FirstCart.items)
                {
                    if (item.IsBOGO)
                    {
                        // For BOGO items, calculate the total price considering the free items
                        int numberOfItemsToPayFor = item.Quantity / 2 + (item.Quantity % 2);
                        total += numberOfItemsToPayFor * item.Price;
                    }
                    else if (item.Markdown > 0)
                    {
                        // For items with markdowns, subtract the markdown amount from the total price
                        total += item.Quantity * (item.Price - (item.Price * item.Markdown/10/10));
                    }
                    else
                    {
                        // For regular items, calculate the total price normally
                        total += item.Quantity * item.Price;
                    }
                }
                return total;
            }
        }

        public ItemViewModel SelectedItem { get; set; }
        public List<ItemViewModel> TestItems { get; set; }
        public List<ItemViewModel> Items
        {
            get
            {
                return ShoppingCartServiceProxy.Current?.Items?.Select(it => new ItemViewModel(it)).ToList() ?? new List<ItemViewModel>(); ;
            }
        }

        //TWO BELOW ARE COPY/PASTED FROM CHECKOUT MANAGEMENT. CHECK IF REALLY NEEDED!!!
        public ShoppingCart GetCart(int id)
        {
            return ShoppingCartServiceProxy.Current.GetCartById(id);
        }

        public List<ItemDTO> GetItems(int id)
        {
            SelectedCart = GetCart(id);
            ShoppingCart temp = GetCart(id);
            NotifyPropertyChanged("SelectedCart");
            return temp.items;
        }

        public void RefreshInventory()
        {
            FirstCart = ShoppingCartServiceProxy.Current?.Carts?.FirstOrDefault();
            Carts = new List<ShoppingCart>(ShoppingCartServiceProxy.Current?.Carts);
            NotifyPropertyChanged("Items");
            NotifyPropertyChanged("TotalPrice");
            NotifyPropertyChanged("Carts");
            NotifyPropertyChanged("CartID");
            NotifyPropertyChanged("TestCartID");
            NotifyPropertyChanged("TestItems");
        }


        public void DeleteItem(int? CartId, ItemViewModel? i)
        {
            int id = CartId ?? 0;
            //ShoppingCartServiceProxy.Current.DeleteItem(ShoppingCartServiceProxy.Current?.Carts?.FirstOrDefault(), i.Item.Id, 1, ItemServiceProxy.Current);
            ShoppingCartServiceProxy.Current.DeleteItem(ShoppingCartServiceProxy.Current?.GetCartById(id), i.Item.Id, 1, ItemServiceProxy.Current);
            RefreshInventory();
        }

        public void AddItem(int CartId, ItemViewModel? i)
        {
            if (i == null)
            {
                return;
            }
            //            ShoppingCartServiceProxy.Current.AddItem(FirstCart, i.Item, 1, ItemServiceProxy.Current);
            ShoppingCartServiceProxy.Current.AddItem(ShoppingCartServiceProxy.Current?.GetCartById(CartId), i.Item, 1, ItemServiceProxy.Current);
            RefreshInventory();
        }

        public void AddCart()
        {
            ShoppingCart temp = new ShoppingCart();
            ShoppingCartServiceProxy.Current.AddOrUpdate(temp);
            RefreshInventory();
        }
    }

}
