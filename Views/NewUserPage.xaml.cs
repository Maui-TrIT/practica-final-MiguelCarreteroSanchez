using ShopApp.ViewModels;

namespace ShopApp.Views;

public partial class NewUserPage : ContentPage
{
	public NewUserPage(NewUserViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}