using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopApp.Models.Backend.Inmueble;
using ShopApp.Services;

namespace ShopApp.ViewModels;

public partial class InmuebleDetailViewModel:ViewModelGlobal, IQueryAttributable
{

    [ObservableProperty]
    private InmuebleResponse inmueble;

    [ObservableProperty]
    private string imagenSource;

    private readonly InmuebleService _inmuebleService;
    private readonly INavigationService _navegacionService;

    public InmuebleDetailViewModel(InmuebleService inmuebleService, INavigationService navegacionService)
    {
        _inmuebleService= inmuebleService;
        _navegacionService = navegacionService;
    }

    public async Task loadDataAsync(int inmuebleId)
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            Inmueble = await _inmuebleService.GetInmuebleById(inmuebleId);
            ImagenSource = Inmueble.IsBookmarkEnabled ? "bookmark_fill_icon" : "bookmark_empty_icon";
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

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var id = int.Parse(query["id"].ToString());
        await loadDataAsync(id);

    }

    [RelayCommand]
    async Task GetBackEvent()
    {
        await _navegacionService.GoToAsync(".."); //nos lleva a la página anterior
    }

    [RelayCommand]
    async Task SaveBookmark()
    {
        var bookmark = new BookmarkRequest
        { 
        InmuebleId = Inmueble.Id,
        UsuarioId = Preferences.Get("userid", string.Empty)
        };

        await _inmuebleService.SaveBookmark(bookmark);
        await loadDataAsync(Inmueble.Id);
    }

    [RelayCommand]
    async Task CallOwner()
    {
        var confirmarLlamada = Application.Current.MainPage.DisplayAlert(
            "Marcar número",
            $"Desea llamar a {Inmueble.Telefono}",
            "Si",
            "No"
            );
        if (await confirmarLlamada)
        {
            try
            {
                PhoneDialer.Open(Inmueble.Telefono);
            }
            catch (ArgumentNullException)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "No se puede realizar la llamada",
                    "Numero erroneo",
                    "Ok"
                    );
            }
            catch (FeatureNotSupportedException)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "No se puede realizar la llamada",
                    "El dispositivo no permite llamadas telefónicas",
                    "Ok"
                    );
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "No se puede realizar la llamada",
                    "Error marcando numero",
                    "Ok"
                    );
            }

        }
    }
        [RelayCommand]
        async Task TextMessageOwner()
        {
            var message = new SmsMessage("Hola, Por favor deseo información sobre la vivienda", Inmueble.Telefono);

            var confirmarSMS = Application.Current.MainPage.DisplayAlert(
                "Envía SMS",
                $"Desea enviar un SMS a {Inmueble.Telefono}",
                "Si",
                "No"
                );
            if (await confirmarSMS)
            {
                try
                {
                    await Sms.ComposeAsync(message);
                }
                catch (ArgumentNullException)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "No se puede enviar el SMS",
                        "Numero erroneo",
                        "Ok"
                        );
                }
                catch (FeatureNotSupportedException)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "No se puede enviar el SMS",
                        "El dispositivo no permite envio de SMS",
                        "Ok"
                        );
                }
                catch (Exception e)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "No se puede enviar el SMS",
                        "Error en envio de SMS",
                        "Ok"
                        );
                }

            }

        }
}
