using Amazon.Library.DTO;
using Amazon.Library.Services;
using Amazon.MAUI.ViewModels;

namespace Amazon.MAUI.Views.InventoryViews;

public partial class InventoryAddView : ContentPage
{
	public InventoryAddView()
	{
		InitializeComponent();
    }
    private void InventoryClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//InventoryManagementPage");
    }

    private void AddClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//InventoryManagementPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ItemViewModel();
    }
}