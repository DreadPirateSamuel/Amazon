using Amazon.MAUI.ViewModels;
using Amazon.Library.DTO;
using Newtonsoft.Json;
using Amazon.MAUI.Models;
namespace Amazon.MAUI.Views;

public partial class InventoryManagementView : ContentPage
{
	public InventoryManagementView()
	{
		InitializeComponent();
        BindingContext = new InventoryManagementViewModel();
    }

    private void InventoryAddClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//InventoryAddPage");
    }

    private void InventoryUpdateClick(object sender, EventArgs e)
    {
        var button = sender as Button;

        // Cast the button's BindingContext to your item type
        var temp = button.BindingContext as ItemViewModel;

        // Ensure the item is not null
        if (temp != null)
        {
            // Use the item's ID to navigate
            Shell.Current.GoToAsync($"//InventoryUpdatePage?itemId={temp.Item.Id}");
        }
    }

    private void InventoryDeleteClick(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel).RefreshInventory();
    }

    private async void ImportCsvClick(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Select a CSV file",
                FileTypes = CustomFilePickerFileType.Csv
            });

            if (result != null)
            {
                using (var stream = await result.OpenReadAsync())
                {   
                    await (BindingContext as InventoryManagementViewModel).ImportItemsFromCsv(stream);
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred while importing the CSV file: {ex.Message}", "OK");
        }

        //(BindingContext as InventoryManagementViewModel).RefreshInventory();
    }

    private void MainMenuClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as InventoryManagementViewModel).RefreshInventory();
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
        (BindingContext as InventoryManagementViewModel).RefreshInventory();
    }
}