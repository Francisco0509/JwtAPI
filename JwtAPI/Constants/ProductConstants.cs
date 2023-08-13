using JwtAPI.Models;

namespace JwtAPI.Constants
{
    public class ProductConstants
    {
        public static List<ProductModel> lProducts = new List<ProductModel>()
        {
            new ProductModel() { Codigo = "MACAIR", Descripcion = "MacBook Air 11"},
            new ProductModel() { Codigo = "AZUSGM", Descripcion = "Azuz Gamer Ryzen7"},
        };
    }
}
