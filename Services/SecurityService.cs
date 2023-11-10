using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShopApp.Models.Backend.Login;
using ShopApp.Models.Config;
using System.Text;


namespace ShopApp.Services;

public class SecurityService
{
    private HttpClient _client;
    private Settings settings;


    public SecurityService(HttpClient client, IConfiguration configuration)
    {
        this._client = client;
        settings = configuration.GetRequiredSection(nameof(Settings)).Get<Settings>();
    }

    public async Task<bool> Login(string email, string password)
    {       
        //var url = "http://192.168.1.40/api/usuario/login";
        var url = $"{settings.UrlBase}/api/usuario/login";

        var loginRequest = new LoginRequest { Email = email, Password = password };

        var json = JsonConvert.SerializeObject(loginRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(url, content);

        if (!response.IsSuccessStatusCode) return false;

        var jsonResultado = await response.Content.ReadAsStringAsync();

        var resultado = JsonConvert.DeserializeObject<UsuarioResponse>(jsonResultado);

        Preferences.Set("accesstoken", resultado.Token);
        Preferences.Set("userId", resultado.Id);
        Preferences.Set("email", resultado.Email);
        Preferences.Set("nombre", $"{resultado.Nombre}{resultado.Apellido}");
        Preferences.Set("telefono", resultado.Telefono);
        Preferences.Set("username", resultado.UserName);

        return true;

    }
}
