using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopApp.DataAccess;
using ShopApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace ShopApp.ViewModels;

public partial class HelpSupportDetailViewModel : ViewModelGlobal, IQueryAttributable
{
    private readonly IConnectivity _connectivity;

    [ObservableProperty]
    private ObservableCollection<Compra> compras = new ObservableCollection<Compra>();

    [ObservableProperty]
    private int clienteId;

    [ObservableProperty]
    private ObservableCollection<Product> products;

    [ObservableProperty]
    private Product productoSeleccionado;

    [ObservableProperty]
    private int cantidad;

    private CompraService _compraService;

    private readonly ShopOutDbContext _outDbContext;
    public HelpSupportDetailViewModel(
        IConnectivity connectivity, 
        CompraService compraService,
        ShopOutDbContext outDbContext
        )
    {
        var database = new ShopDbContext();
        Products = new ObservableCollection<Product>(database.Products);
        AddCommand = new MiComando(() =>
        {
            var compra = new Compra(
                ClienteId, 
                ProductoSeleccionado.Id, 
                Cantidad, 
                ProductoSeleccionado.Nombre,
                ProductoSeleccionado.Precio,
                (Cantidad * ProductoSeleccionado.Precio));
            Compras.Add(compra);
        },
        () => true
        );
        _connectivity = connectivity;
        _compraService = compraService;
        _outDbContext = outDbContext;

        _connectivity.ConnectivityChanged += _connectivity_ConnectivityChanged;
    }

    [RelayCommand(CanExecute = nameof(StatusConnection))]

    private async Task EnviarCompra()
    {
        _outDbContext.Database.EnsureCreated();

        foreach (var item in Compras)
        {
            _outDbContext.Compras.Add(new CompraItem(
                                            item.ClientId,
                                            item.ProductId,
                                            item.Cantidad,
                                            item.ProductoPercio));
        }
        await _outDbContext.SaveChangesAsync();

        await Shell.Current.DisplayAlert("Mensaje","Datos almacenados en BBDD", "Aceptar");
    }


    //envio de datos de compra al backend
    //private async Task EnviarCompra()
    //{
    //    var resultado = await _compraService.EnviarData(Compras);
    //    if (resultado)
    //    {
    //        await Shell.Current.DisplayAlert("Mensaje", "compras enviadas correctamente", "Aceptar");
    //    }
    //}
    private void _connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
        EnviarCompraCommand.NotifyCanExecuteChanged();
    }

    private bool StatusConnection()
    {
        return _connectivity.NetworkAccess == NetworkAccess.Internet ? true : false;
    }
   

    public ICommand AddCommand { get; set; }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {        
        var clientId = int.Parse(query["id"].ToString());
        clienteId = clientId;
    }

    [RelayCommand]
    private void EliminarCompra(Compra compra)
    {
        Compras.Remove(compra);
    }



    //private ObservableCollection<Compra> _compras = new ObservableCollection<Compra>();

    //public ObservableCollection<Compra> Compras
    //{
    //    get { return _compras; }
    //    set
    //    {
    //        if (_compras != value)
    //        {
    //            _compras = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}


    //private int _clientId;
    //public int ClientId
    //{
    //    get { return _clientId; }
    //    set { _clientId = value; }

    //}


    //private ObservableCollection<Product> _products;

    //public ObservableCollection<Product> Products
    //{
    //    get { return _products; }
    //    set
    //    {
    //        if (_products != value)
    //        {
    //            _products = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}


    //private Product _productoSeleccionado;

    //public Product ProductoSeleccionado
    //{
    //    get { return _productoSeleccionado; }
    //    set
    //    {
    //        if (_productoSeleccionado != value)
    //        {
    //            _productoSeleccionado = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}


    //private int _cantidad;

    //public int Cantidad
    //{
    //    get { return _cantidad; }
    //    set
    //    {
    //        if (_cantidad != value)
    //        {
    //            _cantidad = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}

}
