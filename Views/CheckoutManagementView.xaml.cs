using Amazon.MAUI.ViewModels;
using Amazon.Library.Services;
using Amazon.MAUI.Models;

namespace Amazon.MAUI.Views;

[QueryProperty(nameof(CartID), "cartId")]
public partial class CheckoutManagementView : ContentPage
{
    public int CartID { get; set; }

    public CheckoutManagementView()
	{
		InitializeComponent();
        BindingContext = new CheckoutManagementViewModel();
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        //Works with id: BindingContext = CheckoutViewModel().GetCart(CartID);
        //BindingContext = new CheckoutManagementViewModel();
        //Cart = (BindingContext as CheckoutManagementViewModel).GetCart(CartID);
        //Items = Cart.items?.Select(it => new ItemViewModel(it)).ToList() ?? new List<ItemViewModel>(); ;

        var viewModel = BindingContext as CheckoutManagementViewModel;
        if (viewModel != null)
        {
            viewModel.CartID = CartID;
            viewModel.Items = viewModel.GetItems(CartID)?.Select(it => new ItemViewModel(it)).ToList() ?? new List<ItemViewModel>(); ;
        }
        viewModel.Refresh();
    }

    private void WishListClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//WishListPage");
    }

    private void CheckOutClick(object sender, EventArgs e)
    {
        ShoppingCartServiceProxy.Current.Delete(CartID);
        Shell.Current.GoToAsync("//MainPage");
    }
}