using Amazon.MAUI.ViewModels;

namespace Amazon.MAUI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        private void InventoryClick(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//InventoryManagementPage");
        }

        private void ShopClick(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ShopManagementPage");
        }

        private void ConfigClick(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ConfigManagementPage");
        }
    }

}