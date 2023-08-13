using JwtAPI.Models;

namespace JwtAPI.Constants
{
    public class CountryConstants
    {
        public static List<CountryModel> lCountries = new List<CountryModel>()
        { 
            new CountryModel() { Nombre = "México"},
            new CountryModel() { Nombre = "USA"},
            new CountryModel() { Nombre = "Cánada"},
        };
    }
}
