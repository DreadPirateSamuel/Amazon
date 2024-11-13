using Amazon.MAUI.Models;
using Amazon.MAUI.ViewModels;
namespace Amazon.MAUI.Views;

public partial class WishListManagementView : ContentPage
{
	public WishListManagementView()
	{
		InitializeComponent();
        BindingContext = new ShopManagementViewModel();
    }

    private void ShopClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ShopManagementPage");
    }

    private void AddCartClick(object sender, EventArgs e)
    {
        (BindingContext as ShopManagementViewModel).AddCart();
    }

    private void CheckoutClick(object sender, EventArgs e)
    {
        var button = sender as Button;
        var checkOutCart = button.CommandParameter;
        ShoppingCart temp = checkOutCart as ShoppingCart;
        Shell.Current.GoToAsync($"//CheckoutPage?cartId={temp.CartID}");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ShopManagementViewModel).RefreshInventory();
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
        (BindingContext as ShopManagementViewModel).RefreshInventory();
    }

    private void ViewItemsClick(object sender, EventArgs e)
    {
        var button = sender as Button;
        var checkOutCart = button.CommandParameter;
        ShoppingCart temp = checkOutCart as ShoppingCart;
        Shell.Current.GoToAsync($"//WishContentsPage?cartId={temp.CartID}");
    }
}