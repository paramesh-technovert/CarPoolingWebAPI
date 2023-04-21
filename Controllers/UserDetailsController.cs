using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;
using CarPoolingWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserDetailsController : Controller
    {
        private readonly CarPoolingDbContext _dbContext;
        public UserDetailsController(CarPoolingDbContext dbContext) : base()
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<UserDetailsRequestDTO> GetUserDetails([FromHeader]Guid id)
        {
            UserDetailsService userDetailsService = new UserDetailsService(_dbContext);
            return await userDetailsService.GetUserDetails(id);
        }
        [HttpPut]
        public async Task<UserDetailsRequestDTO> AddUserDetails([FromBody]UserDetailsRequestDTO userDetailsRequestDTO)
        {
            
            UserDetailsService userDetailsService = new UserDetailsService(_dbContext);
            return await userDetailsService.AddUser(userDetailsRequestDTO);
        }
    }
}
