using Amazon.MAUI.ViewModels;

namespace Amazon.MAUI.Views;

public partial class ShopManagementView : ContentPage
{
   public ShopManagementView()
   {
       InitializeComponent();
	   BindingContext = new ShopManagementViewModel();
   }

    private void ShopAddClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ShopAddPage");
    }

    private void ShopDeleteClick(object sender, EventArgs e)
    {

        var button = sender as Button;
        var viewModel = BindingContext as ShopManagementViewModel;

        if (button != null)
        {
            var itemToDelete = button.CommandParameter as ItemViewModel;

            if (itemToDelete != null)
            {
                // Passing the item to the ShopManagementViewModel's DeleteItem
                (BindingContext as ShopManagementViewModel)?.DeleteItem(viewModel.CartID, itemToDelete);
            }
        }

    }

    private void WishListClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//WishListPage");
    }
    private void MainMenuClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ShopManagementViewModel).RefreshInventory();
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
        (BindingContext as ShopManagementViewModel).RefreshInventory();
    }
}