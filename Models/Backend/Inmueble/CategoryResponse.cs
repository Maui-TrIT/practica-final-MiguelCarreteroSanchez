using Newtonsoft.Json;

namespace ShopApp.Models.Backend.Inmueble;

public class CategoryResponse
{

    [JsonProperty("id")]   //no es necesario porque es igual el nombre de la propiedad a lo que me devuelve el backend
    public int Id { get; set; }


    [JsonProperty("nombre")]
    public string NombreCategory { get; set; }


    [JsonProperty("imageUrl")] //no es necesario porque es igual el nombre de la propiaedad a lo que me devuelve el backend
    public string ImagenUrl { get; set; }
}
