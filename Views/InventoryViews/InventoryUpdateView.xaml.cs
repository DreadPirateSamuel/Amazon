using Amazon.Library.DTO;
using Amazon.Library.Services;
using Amazon.MAUI.ViewModels;

namespace Amazon.MAUI.Views.InventoryViews;

[QueryProperty(nameof(ItemId), "itemId")]
public partial class InventoryUpdateView : ContentPage
{
    public int ItemId { get; set; }
    public InventoryUpdateView()
    {
        InitializeComponent();
    }
    private void InventoryClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//InventoryManagementPage");
    }

    private void UpdateClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//InventoryManagementPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ItemViewModel(ItemId);
    }
}