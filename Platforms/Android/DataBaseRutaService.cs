
namespace ShopApp.Services;

public class DataBaseRutaService : IDataBaseRutaService
{
    public string Get(string nombreArchivo)
    {
        var rutaDirectorio = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        return Path.Combine(rutaDirectorio, nombreArchivo);
    }
}
