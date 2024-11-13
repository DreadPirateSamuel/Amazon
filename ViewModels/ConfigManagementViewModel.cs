using Amazon.Library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.MAUI.ViewModels
{
    public class ConfigManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        private static ConfigManagementViewModel? instance;
        private static object instanceLock = new object();
        public static ConfigManagementViewModel Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ConfigManagementViewModel();
                    }
                }

                return instance;
            }
        }
        private ConfigManagementViewModel()
        {

        }

        public decimal TaxRate { get; set; }

        public decimal GetTaxRate()
        {
            return TaxRate;
        }
        public void RefreshTax()
        {
            NotifyPropertyChanged("TaxRate");
        }
    }
}
