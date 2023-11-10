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

    protected override void OnAppearing()  //este método se ejecuta cuando se carga la página
    {
        _viewModel.GetInmueblesCommand.Execute(this);
    }
}