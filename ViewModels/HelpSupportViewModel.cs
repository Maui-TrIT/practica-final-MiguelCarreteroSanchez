using CommunityToolkit.Mvvm.ComponentModel;
using ShopApp.DataAccess;
using ShopApp.Services;
using ShopApp.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace ShopApp.ViewModels;

public partial class HelpSupportViewModel : ViewModelGlobal
{
    [ObservableProperty]
    public int visitasPendientes;

    [ObservableProperty]
    private ObservableCollection<Client> clients;

    [ObservableProperty]
    private Client clienteSeleccionado;


    private readonly INavigationService _navigationService;
    public HelpSupportViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        var database = new ShopDbContext();
        Clients = new ObservableCollection<Client>(database.Clients);
        PropertyChanged += HelpSupportData_PropertyChanged;
    }

    private async void HelpSupportData_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ClienteSeleccionado))
        {
            var uri = $"{nameof(HelpSupportDetailPage)}?id={ClienteSeleccionado.Id}";
            await _navigationService.GoToAsync(uri);
        }
    }



    //public int _visitasPendientes;
    //public int VisitasPendientes
    //{
    //    get { return _visitasPendientes; }
    //    set
    //    {
    //        if (_visitasPendientes != value)
    //        {
    //            _visitasPendientes = value;
    //            OnPropertyChanged();
    //            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VisitasPendientes"));
    //        }
    //    }
    //}


    //private ObservableCollection<Client> _clients;
    //public ObservableCollection<Client> Clients
    //{
    //    get { return _clients; }
    //    set
    //    {
    //        if (_clients != value)
    //        {
    //            _clients = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}



    //private Client _clienteSeleccionado;

    //public Client ClienteSeleccionado
    //{

    //    get { return _clienteSeleccionado; }
    //    set
    //    {
    //        if (_clienteSeleccionado != value)
    //        {
    //            _clienteSeleccionado = value;
    //            OnPropertyChanged();
    //        }
    //    }


    //}
}
