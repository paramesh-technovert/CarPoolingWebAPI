using AutoMapper;
using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserDetailsController : Controller
    {
        private readonly CarPoolingDbContext _dbContext;
        private readonly IMapper _mapper;
        public UserDetailsController(CarPoolingDbContext dbContext,IMapper mapper) : base()
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize]
        public async Task<UserDetailsRequestDTO> GetUserDetails([FromHeader]Guid id)
        {
            UserDetailsService userDetailsService = new UserDetailsService(_dbContext,_mapper);
            UserDetailsRequestDTO res=await userDetailsService.GetUserDetails(id);
            return res;
        }
        [HttpPut]
        public async Task<UserDetailsRequestDTO> AddUserDetails([FromBody]UserDetailsRequestDTO userDetailsRequestDTO)
        {
            
            UserDetailsService userDetailsService = new UserDetailsService(_dbContext, _mapper);
            UserDetailsRequestDT userDetailsRequestDT = new UserDetailsRequestDT();
            userDetailsRequestDT = _mapper.Map<UserDetailsRequestDT>(userDetailsRequestDTO);
            UserDetailsRequestDTO user= await userDetailsService.AddUser(userDetailsRequestDTO);
            return user;
        }
    }
}
