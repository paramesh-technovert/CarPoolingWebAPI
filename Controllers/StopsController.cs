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
        public Task<IQueryable<MatchedRidesResponseDTO>> GetMatchedRides([FromQuery] string BoardingPoint, [FromQuery] string Destination, [FromQuery] string date, [FromQuery] string time)
        {
            MatchedRidesRequestDTO matchedRidesRequestDTO = new MatchedRidesRequestDTO
            {
                BoardingPoint = BoardingPoint,
                Destination = Destination,
                Date = Convert.ToDateTime(date + " " + time)
            };
            StopsService service = new StopsService(_dbContext);
            return service.GetMatchedRides(matchedRidesRequestDTO);
        }
    }
}
