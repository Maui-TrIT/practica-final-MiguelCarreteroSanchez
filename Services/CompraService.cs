using ShopApp.DataAccess;
using System;
using ShopApp.Models.Config;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace ShopApp.Services;

public class CompraService
{

    private HttpClient client;
    private Settings settings;

    public CompraService(HttpClient client, IConfiguration configuration)
    {
        this.client = client;
        settings = configuration.GetRequiredSection(nameof(Settings)).Get<Settings>();
    }

    public async Task<bool> EnviarData(IEnumerable<Compra> compras)
    {
        //var uri = "http://192.168.1.40/api/compra";
        var uri = $"{settings.UrlBase}/api/compra";
        var body = new
        {
            data = compras
    };

        var resultado = await client.PostAsJsonAsync(uri, body);

        return resultado.IsSuccessStatusCode;
    }

}
