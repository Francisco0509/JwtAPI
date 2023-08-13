using JwtAPI.Models;

namespace JwtAPI.Constants
{
    public class EmployeeConstants
    {
        public static List<EmployeeModel> lEmployees = new List<EmployeeModel>()
        {
            new EmployeeModel() { FistName = "Sonia", LastName = "Carranza", Email = "sonybb@gmail.com" },
            new EmployeeModel() { FistName = "Patricia", LastName = "Olivares", Email = "patto80@gmail.com" },
        };
    }
}
