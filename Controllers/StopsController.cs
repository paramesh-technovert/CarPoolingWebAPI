using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolingWebAPI.Controllers
{
    [ApiController]
    [Route("api/MatchedRides")]
    public class StopsController : Controller
    {
        private readonly CarPoolingDbContext _dbContext;
        public StopsController(CarPoolingDbContext dbContext) : base()
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public Task<List<MatchedRidesResponseDTO>> GetMatchedRides([FromBody] MatchedRidesRequestDTO matchedRidesRequestDTO)
        {
            StopsService service = new StopsService(_dbContext);
            return service.GetMatchedRides(matchedRidesRequestDTO);
        }
    }
}
