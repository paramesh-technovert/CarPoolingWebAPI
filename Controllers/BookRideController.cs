using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;
using CarPoolingWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BookRideController : Controller
    {
        private readonly CarPoolingDbContext _dbContext;
        public BookRideController(CarPoolingDbContext dbContext) : base()
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        public async Task<BookRideResponseDTO> BookRide([FromBody] BookRideRequestDTO bookRideRequestDTO)
        {
            if (bookRideRequestDTO.AvailableSeats < bookRideRequestDTO.BookedSeats)
            {
                throw new Exception("Execeded maximum available seats");
            }
            BookRideService bookRideService = new BookRideService(_dbContext);
            return await bookRideService.BookRide(bookRideRequestDTO);
        }
        [HttpGet]
        public List<BookingDetailsDTO> GetBookedRides([FromHeader] Guid userId)
        {
            BookRideService bookRideService = new BookRideService(_dbContext);
            return bookRideService.GetBookedRides(userId);
        }
    }
}
