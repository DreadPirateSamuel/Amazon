using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using Amazon.Library.DTO;
using Amazon.Library.Services;
using Amazon.MAUI.Models;

using CsvHelper;



namespace Amazon.MAUI.ViewModels
{
    public class InventoryManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public List<ItemViewModel> Items
        {
            get
            {
                return ItemServiceProxy.Current?.Items?.Select(it => new ItemViewModel(it)).ToList() ?? new List<ItemViewModel>();
            }
        }

        public ItemViewModel SelectedItem { get; set; }
        public InventoryManagementViewModel() 
        { 
        
        
        }

        public async Task ImportItemsFromCsv(Stream stream)
        {
            List<ItemDTO> records;

            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<ItemDTO>().ToList();
            }

            // Perform the asynchronous operation after reading all records
            var items = await ItemServiceProxy.Current.AddCSV(records);

            // Notify property changed and refresh inventory
            NotifyPropertyChanged(nameof(Items));
            RefreshInventory();
        }

        public void RefreshInventory()
        {
            ItemServiceProxy.Current.EnforceChanges(); //Makes sure items are synced with server.
            NotifyPropertyChanged("Items");
        }
    }
}
