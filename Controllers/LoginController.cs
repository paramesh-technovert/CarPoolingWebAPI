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
        public async Task<ActionResult<LoginDetail?>> AddUser([FromBody] LoginCredentialsDTO loginCredential)
        {
            LoginService loginService;
            try
            {
                loginService = new LoginService(_dbContext);
                return Ok(await loginService.AddUser(loginCredential));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("SignIn")]
        public async Task<ActionResult<LoginCredentialsResponseDTO?>> GetUserDetails([FromBody] LoginCredentialsDTO loginCredentialsDTO)
        {
            LoginService loginService = new LoginService(_dbContext);
            try
            {
                return await loginService.GetUserDetails(loginCredentialsDTO);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
