using JwtAPI.Models;

namespace JwtAPI.Constants
{
    public class UserContstants
    {
        public static List<UserModel> lUsers = new List<UserModel>()
        {
            new UserModel { UserName = "fruiz", Password = "mossca", EmailAddress = "fruiz@gmail.com", FirstName = "Francisco", LastName = "Ruiz", Role = "Administrador" },
            new UserModel { UserName = "tomcruz", Password = "515t3m45", EmailAddress = "tomcruz@gmail.com", FirstName = "Tomás", LastName = "Cruz", Role = "Soporte" },
        };
    }
}
