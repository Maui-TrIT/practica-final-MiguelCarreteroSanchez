using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopApp.Services;
using ShopApp.Views;

namespace ShopApp.ViewModels;

public partial class NewUserViewModel : ViewModelGlobal
{
    [ObservableProperty]
    private string nombre;

    [ObservableProperty]
    private string apellido;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string userName;

    [ObservableProperty]
    private string telefono;

    [ObservableProperty]
    private string password;

    private readonly UserService _userService;

    private readonly INavegacionService _navegacionService;

    public NewUserViewModel(IConnectivity connectivity, UserService userService, INavegacionService navegacionService)
    {
        _userService = userService;
        _navegacionService = navegacionService;
    }


    [RelayCommand]
    async Task TapGotoLogin()
    {
        var uri = $"{nameof(LoginPage)}";
        await _navegacionService.GoToAsync(uri);
    }

    [RelayCommand]
    private async Task RegisterUser()
    {
        if (string.IsNullOrWhiteSpace(Nombre)
            || string.IsNullOrWhiteSpace(Email)
            || string.IsNullOrWhiteSpace(UserName)
            || string.IsNullOrWhiteSpace(Password))
        {
            await Shell.Current.DisplayAlert("Mensaje", "Los campos marcados con * son obligatorios.", "Aceptar");
        }
        else if (!Email.Contains("@"))
        {
            //añadir otros controles como el dominio
            await Shell.Current.DisplayAlert("Mensaje", "Email no válido.", "Aceptar");
        }
        else
        {
            var resultado = await _userService.Register(Nombre, Apellido, Email, UserName, Telefono, Password);
            if (resultado)
            {
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await Shell.Current.DisplayAlert("Mensaje", "No ha sido posible registrar este usuario", "Aceptar");
            }
        }

    }

}
