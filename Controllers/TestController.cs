using APIUsingToken.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIUsingToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController
    {
        [HttpGet("CHeck")]
        public string  GetAllStudent()
        {
            
            return "Correct";
        }
    }
}
