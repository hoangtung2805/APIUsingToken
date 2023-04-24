using APIUsingToken.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace APIUsingToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        [HttpPost("login")]
        public async Task<ActionResult<object>> Login([FromBody] LoginModel model)
        {
            string token;

            var _context = new StudentContext();
            var _username = _context.Students.Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();
            if (model.Username == null || model.Password == null)
            {
                return BadRequest(new { error = "Please enter username and password" });
            }
            if (_username == null)
            {
                return BadRequest(new { error = "Invalid username or password" });
            }
            if (_username.Password != model.Password)
            {
                return BadRequest(new { error = "Invalid username or password" });
            }

            token = CreateToken(_username);

            return new { token };
        }

        private string CreateToken(Student student)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, student.Username)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var cred  = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var _token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);
            var jwt  = new JwtSecurityTokenHandler().WriteToken (_token);
            return jwt;
        }
    }
}
