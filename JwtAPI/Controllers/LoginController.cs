using JwtAPI.Constants;
using JwtAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace JwtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult Login(LoginUser login)
        {
            var user = Authenticate(login);
            
            if(user != null) 
            {
                var token = GenerateToken(user);
                return Ok(token);
            }

            
            return NotFound("Usuario no encontrado");
        }

        [HttpGet]
        public IActionResult Get() 
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hola {currentUser.FirstName} te logueaste con el rol {currentUser.Role}");
        }

        private UserModel Authenticate(LoginUser login) 
        {
            var currentUser = UserContstants.lUsers.FirstOrDefault(user => user.UserName.ToLower() == login.UserName.ToLower()
                            && user.Password == login.Password);

            if(currentUser != null) 
            {
                return currentUser;
            }
            return null;
        }

        private string GenerateToken(UserModel user)
        {
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            //Generar claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            //Generar token
            var token = new JwtSecurityToken(
                    _configuration["Jwt:issuer"],
                    _configuration["Jwt:audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(1),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if(identity != null)
            {
                var userClaims = identity.Claims;

                return new UserModel
                {
                    UserName = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value,
                    EmailAddress = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value,
                    FirstName = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName).Value,
                    LastName = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Surname).Value,
                    Role = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value,
                };
            }

            return null;
        }
    }
}
