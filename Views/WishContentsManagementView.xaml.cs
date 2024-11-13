using Amazon.MAUI.Models;
using Amazon.MAUI.ViewModels;
using Amazon.Library.Services;

namespace Amazon.MAUI.Views;

[QueryProperty(nameof(CartID), "cartId")]
public partial class WishContentsManagementView : ContentPage
{
    public int CartID { get; set; }

    public WishContentsManagementView()
    {
        InitializeComponent();
        BindingContext = new ShopManagementViewModel();
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {

        var viewModel = BindingContext as ShopManagementViewModel;
        if (viewModel != null)
        {
            viewModel.TestCartID = CartID;
            viewModel.TestItems = viewModel.GetItems(CartID)?.Select(it => new ItemViewModel(it)).ToList() ?? new List<ItemViewModel>(); ;
        }
        (BindingContext as ShopManagementViewModel).RefreshInventory();
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
        (BindingContext as ShopManagementViewModel).RefreshInventory();
    }

    private void WishListClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//WishListPage");
    }
    private void ShopAddClick(object sender, EventArgs e)
    {
        ShoppingCart temp = new ShoppingCart();
        temp.CartID = CartID;
        Shell.Current.GoToAsync($"//ShopAddPage?cartId={CartID}");
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
                (BindingContext as ShopManagementViewModel)?.DeleteItem(CartID, itemToDelete);
            }
        }
        viewModel.TestItems = viewModel.GetItems(CartID)?.Select(it => new ItemViewModel(it)).ToList() ?? new List<ItemViewModel>(); ;
        (BindingContext as ShopManagementViewModel).RefreshInventory();

    }

}