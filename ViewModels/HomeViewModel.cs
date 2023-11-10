using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopApp.Models.Backend.Inmueble;
using ShopApp.Services;
using ShopApp.Views;
using System.Collections.ObjectModel;

namespace ShopApp.ViewModels;

public partial class HomeViewModel:ViewModelGlobal
{
    [ObservableProperty]
    string nombreUsuario;

    [ObservableProperty]
    CategoryResponse categoriaSeleccionada;

    [ObservableProperty]
    ObservableCollection<CategoryResponse> categories;

    [ObservableProperty]
    ObservableCollection<InmuebleResponse> favoriteInmuebles;

    public Command GetDataCommand { get; }

    private readonly InmuebleService _inmuebleService;
    private readonly INavigationService _navigationService;

    public HomeViewModel(InmuebleService inmuebleService, INavigationService navigationService)
    {
        _inmuebleService = inmuebleService;
        _navigationService = navigationService;
        NombreUsuario = Preferences.Get("nombre", string.Empty);
        GetDataCommand = new Command(async()=>await LoadDataAsync());

        GetDataCommand.Execute(this); //para ejecutar el comando 
    }


    public async Task LoadDataAsync()
    {
        if (IsBusy) //se está trabajando en otra petición
            return;
        try
        {
            IsBusy = true;
            var listCategories = await _inmuebleService.GetCategories();
            Categories = new ObservableCollection<CategoryResponse>(listCategories);

            var listFavoritos = await _inmuebleService.GetInmueblesFavoritos();
            FavoriteInmuebles = new ObservableCollection<InmuebleResponse>(listFavoritos);

        }
        catch (Exception e)
        {
            await Application.Current.MainPage.DisplayAlert("Error", e.Message, "Aceptar");
        }
        finally
        {
            IsBusy = false;
        }

    }

    [RelayCommand]
    async Task CategoryEventSelected()
    { 
        var uri = $"{nameof(InmuebleListPage)}?id={CategoriaSeleccionada.Id}";
        await _navigationService.GoToAsync(uri);
    }

    [RelayCommand]
    async Task TabBusquedaInmuebles()
    {
        var uri = $"{nameof(InmuebleBusquedaPage)}";
        await _navigationService.GoToAsync(uri);
    }
}
