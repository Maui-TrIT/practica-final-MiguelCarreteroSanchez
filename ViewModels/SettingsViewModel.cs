using CommunityToolkit.Mvvm.Input;
using ShopApp.Services;
using ShopApp.Views;

namespace ShopApp.ViewModels;

public partial class SettingsViewModel:ViewModelGlobal
{
    private readonly INavigationService _navegacionService;

    [RelayCommand]
    async Task SalirSesion()
    {
        //borramos el token
        Preferences.Set("accesstoken", string.Empty);

        //redireccionamos a home
        var uri = $"//{nameof(AboutPage)}";
        await _navegacionService.GoToAsync(uri);    
    }

    public SettingsViewModel(INavigationService navegacionService)
    {
        _navegacionService = navegacionService;
    }

}
