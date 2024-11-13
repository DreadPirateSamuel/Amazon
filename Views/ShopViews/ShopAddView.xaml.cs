using Amazon.MAUI.ViewModels;

namespace Amazon.MAUI.Views.ShopViews;

[QueryProperty(nameof(TempCartID), "cartId")]
public partial class ShopAddView : ContentPage
{
    public int TempCartID { get; set; }
    public ShopAddView()
	{
		InitializeComponent();
        var inventoryViewModel = new InventoryManagementViewModel();
        var shopViewModel = new ShopManagementViewModel();

        // Set the BindingContext to an object containing both view models
        BindingContext = new { InventoryViewModel = inventoryViewModel, ShopViewModel = shopViewModel };
    }



    private void ShopClick(object sender, EventArgs e)
    {
        var context = BindingContext as dynamic;
        var button = sender as Button;
        if (button != null)
        {
            var itemToAdd = button.CommandParameter as ItemViewModel;

            if (itemToAdd != null)
            {
                
                context.ShopViewModel.AddItem(TempCartID, itemToAdd);
            }
        }

        context.InventoryViewModel.RefreshInventory();
    }

    private void ShoppingPageClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ShopManagementPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        var context = BindingContext as dynamic;
        context.ShopViewModel.RefreshInventory();
        context.InventoryViewModel.RefreshInventory();
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
        var context = BindingContext as dynamic;
        context.ShopViewModel.RefreshInventory();
        context.InventoryViewModel.RefreshInventory();
    }

    private void WishListClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//WishListPage");
    }
}
