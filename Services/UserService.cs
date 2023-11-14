
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShopApp.Models.Backend.Login;
using ShopApp.Models.Config;
using System.Text;

namespace ShopApp.Services;

public class UserService
{

    private HttpClient client;
    private Settings settings;
    public UserService(HttpClient client, IConfiguration configuration)
    {
        this.client = client;
        settings = configuration.GetRequiredSection(nameof(Settings)).Get<Settings>();
    }

    public async Task<bool> Register(string nombre, string apellido, string email, string userName, string telefono, string password)
    {
        var url = $"{settings.UrlBase}/api/Usuario/registrar";
        var registroRequest = new RegistroRequest
        {
            Nombre = nombre, 
            Apellido = apellido,
            Email = email, 
            UserName = userName,
            Telefono = telefono,
            Password = password
        };

        var json = JsonConvert.SerializeObject(registroRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
            return false;

        else
        {
            var jsonResultado = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<UsuarioResponse>(jsonResultado);

            Preferences.Set("accesstoken", resultado.Token);
            Preferences.Set("userid", resultado.Id);
            Preferences.Set("email", resultado.Email);
            Preferences.Set("nombre", $"{resultado.Nombre}  {resultado.Apellido}");
            Preferences.Set("telefono", resultado.Telefono);
            Preferences.Set("username", resultado.UserName);
        }
        return true;
    }
}
