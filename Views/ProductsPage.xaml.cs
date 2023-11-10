using ShopApp.DataAccess;
using ShopApp.ViewModels;

namespace ShopApp.Views;

public partial class ProductsPage : ContentPage
{
	public ProductsPage(ProductsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;


        //var dbContext = new ShopDbContext();

        //foreach (var product in dbContext.Products)
        //{
        //	var boton = new Button { Text = product.Nombre };
        //	boton.Clicked += async (s, a) =>
        //	{
        //		var uri = $"{nameof(ProductDetailPage)}?id={product.Id}";
        //		await Shell.Current.GoToAsync(uri);
        //	};
        //	container.Children.Add(boton);
        //}


    }
}