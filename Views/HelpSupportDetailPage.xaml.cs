using ShopApp.DataAccess;
using ShopApp.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ShopApp.Views;

public partial class HelpSupportDetailPage : ContentPage
{
	public HelpSupportDetailPage(HelpSupportDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}

