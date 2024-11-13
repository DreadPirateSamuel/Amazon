using Amazon.Library.DTO;
using Amazon.Library.Services;
using Amazon.MAUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.MAUI.ViewModels
{
    public class CheckoutManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public CheckoutManagementViewModel() { }
        private ShoppingCart selectedCart { get; set; }
        public List<ItemViewModel> Items { get; set; }
        public int CartID { get; set; }

        public ShoppingCart GetCart(int id)
        {
            return ShoppingCartServiceProxy.Current.GetCartById(id);
        }

        public List<ItemDTO> GetItems(int id)
        {
            selectedCart = GetCart(id);
            ShoppingCart temp = GetCart(id);
            NotifyPropertyChanged("selectedCart");
            return temp.items;
        }

        public decimal TaxRate { 
            get
            {
                var config = ConfigManagementViewModel.Current;
                return config.GetTaxRate();
            }
        }

        public decimal SubTotalPrice
        {
            get
            {
                if (selectedCart == null)
                    return 0m;

                decimal total = 0m;
                foreach (var item in selectedCart.items)
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
                        total += item.Quantity * (item.Price - (item.Price * item.Markdown / 10 / 10));
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

        public decimal TotalTax
        {
            get
            {
                return (TaxRate / 100) * SubTotalPrice;
            }
        }

        public decimal TotalPrice
        {
            get 
            {
                return SubTotalPrice + TotalTax;
            
            }
        }
        public void Refresh()
        {
            NotifyPropertyChanged("CartID");
            NotifyPropertyChanged("Items");
            NotifyPropertyChanged("SubTotalPrice");
            NotifyPropertyChanged("TotalTax");
            NotifyPropertyChanged("TotalPrice");
            NotifyPropertyChanged("TaxRate");
        }

    }
}
