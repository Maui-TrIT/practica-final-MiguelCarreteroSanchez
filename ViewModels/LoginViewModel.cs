using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopApp.Services;

namespace ShopApp.ViewModels;

public partial class LoginViewModel:ViewModelGlobal
{
    private readonly IConnectivity _connectivity;

    private readonly SecurityService _securityService;

    [ObservableProperty]
    private string email;
    
    [ObservableProperty]
    private string password;



    public LoginViewModel(IConnectivity connectivity, SecurityService securityService)
    { 
        _connectivity = connectivity;
        _securityService = securityService;

        _connectivity.ConnectivityChanged += _connectivity_ConnectivityChanged;

    }

    private void _connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
       LoginMethodCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand (CanExecute =nameof(StatusConnection))]
    private async Task LoginMethod()
    {
        var resultado = await _securityService.Login(Email, Password);

        if (resultado)
        {
            //login ok
            Application.Current.MainPage = new AppShell();
        }
        else 
        {
            //login no OK
            await Shell.Current.DisplayAlert("Mensaje", "credenciales incorrectas", "Aceptar");
        }

    }

    private bool StatusConnection()
    {
        return _connectivity.NetworkAccess == NetworkAccess.Internet ? true : false;
    }



}
