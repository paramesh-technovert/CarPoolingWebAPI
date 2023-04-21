using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;

namespace CarPoolingWebAPI.Repository
{
    public interface IStops
    {
        public Task AddStops(Stop stop);
        public Task<List<MatchedRidesResponseDTO>> GetMatchedRides(MatchedRidesRequestDTO matchedRidesRequestDTO);
        public void BookingRide(BookRideRequestDTO bookRideRequestDTO);
    }
}
