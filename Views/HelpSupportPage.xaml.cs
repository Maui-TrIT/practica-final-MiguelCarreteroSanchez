
using ShopApp.DataAccess;
using ShopApp.ViewModels;


namespace ShopApp.Views;

public partial class HelpSupportPage : ContentPage
{
	public HelpSupportPage(HelpSupportViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	//private void Button_Clicked(object sender, EventArgs e)
	//{
	//	var dataObject = Resources["datadeHelpSupport"] as HelpSupportViewModel;
	//	dataObject.VisitasPendientes = 30;
	//}
}


