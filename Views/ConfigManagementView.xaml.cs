using Amazon.MAUI.ViewModels;
namespace Amazon.MAUI.Views;

public partial class ConfigManagementView : ContentPage
{
	public ConfigManagementView()
	{
		InitializeComponent();
		BindingContext = ConfigManagementViewModel.Current;
	}

    private void MenuClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void TaxClick(object sender, EventArgs e)
    {
        (BindingContext as ConfigManagementViewModel).RefreshTax();
    }


}