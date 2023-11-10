using ShopApp.ViewModels;

namespace ShopApp.Views;

public partial class BookmarkPage : ContentPage
{
	private BookmarkViewModel _viewModel;
	public BookmarkPage(BookmarkViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
	}

    protected override void OnAppearing()  //este m�todo se ejecuta cuando se carga la p�gina
    {
        _viewModel.GetInmueblesCommand.Execute(this);
    }
}