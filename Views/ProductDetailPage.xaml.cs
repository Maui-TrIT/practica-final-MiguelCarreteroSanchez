using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess;
using ShopApp.ViewModels;

namespace ShopApp.Views;

public partial class ProductDetailPage : ContentPage
{
	public ProductDetailPage(ProductDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}


}