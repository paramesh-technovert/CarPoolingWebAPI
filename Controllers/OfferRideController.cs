using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;
using CarPoolingWebAPI.Repository;
using CarPoolingWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CarPoolingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferRideController : Controller
    {
        private readonly CarPoolingDbContext _dbContext;
        public OfferRideController(CarPoolingDbContext dbContext) : base()
        {
            _dbContext = dbContext;
        }
        /*[HttpGet]
        [Route("OfferedRides")]
        public async Task<List<OfferRide>> GetRide([FromHeader]Guid Id)
        {
            OfferedRideService offeredRideService = new(_dbContext);
            return await offeredRideService.GetOfferedRides(Id);
        }*/
        [HttpGet]
        [Route("OfferedRides")]
        public async Task<List<OfferedRidesDTO>> GetRide([FromHeader] Guid Id)
        {
            OfferedRideService offeredRideService = new(_dbContext);
            return await offeredRideService.GetOfferedRides(Id);
        }
        [HttpPost]
        public async Task<OfferRide> SetRide([FromBody] OfferedRideDTO offeredRide)
        {
            OfferedRideService offeredRides = new(_dbContext);
            OfferRide res = await offeredRides.OfferRide(offeredRide);
            return res;
        }
    }
}
