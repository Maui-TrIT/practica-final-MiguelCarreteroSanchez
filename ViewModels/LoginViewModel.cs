
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopApp.Services;
using ShopApp.Views;

namespace ShopApp.ViewModels;

public partial class LoginViewModel : ViewModelGlobal
{
    private readonly IConnectivity _connectivity;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string password;

    private readonly SecurityService _securityService;

    private readonly INavegacionService _navegacionService;

    public LoginViewModel(IConnectivity connectivity, SecurityService securityService, INavegacionService navegacionService)
    {
        _connectivity = connectivity;
        _securityService = securityService;
        _connectivity.ConnectivityChanged += _connectivity_ConnectivityChanged;
        _navegacionService = navegacionService;
    }

    [RelayCommand(CanExecute = nameof(StatusConnection))]
    private async Task LoginMethod()
    {
         var resultado = await _securityService.Login(Email, Password);
        if (resultado)
        {
            Application.Current.MainPage = new AppShell();
        }
        else {
            await Shell.Current.DisplayAlert("Mensaje", "Ingreso credenciales erroneas", "Aceptar");
        }

    }

    private void _connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
        LoginMethodCommand.NotifyCanExecuteChanged();
    }

    

    private bool StatusConnection()
    {
        return _connectivity.NetworkAccess == NetworkAccess.Internet ? true : false;
    }


    [RelayCommand]
    async Task TapNewUser()
    {
        var uri = $"{nameof(NewUserPage)}";
        await _navegacionService.GoToAsync(uri);
    }
}
