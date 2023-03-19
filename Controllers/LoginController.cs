using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;
using CarPoolingWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LoginController : Controller
    {
        private readonly CarPoolingDbContext _dbContext;
        public LoginController(CarPoolingDbContext dbContext) : base()
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        [Route("SignUp")]
        public async Task<LoginDetail?> AddUser([FromBody] LoginCredentialsDTO loginCredential)
        {
            LoginService loginService = new LoginService(_dbContext);
            return await loginService.AddUser(loginCredential);

        }
        [HttpPost]
        [Route("SignIn")]
        public async Task<LoginCredentialsDTO?> GetUserDetails([FromHeader] string email, [FromHeader] string password)
        {
            LoginService loginService = new LoginService(_dbContext);
            return await loginService.GetUserDetails(email, password);
        }
    }
}
