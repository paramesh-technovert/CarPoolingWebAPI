using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Repository;

namespace CarPoolingWebAPI.Services
{
    public class StopsService
    {
        private readonly CarPoolingDbContext _dbContext;
        public StopsService(CarPoolingDbContext dbContext) : base()
        {
            _dbContext = dbContext;
        }
        public async Task<List<MatchedRidesResponseDTO>> GetMatchedRides(MatchedRidesRequestDTO matchedRidesRequestDTO)
        {
            Stops stops = new Stops(_dbContext);
            UserDetailsService userDetailsService = new UserDetailsService(_dbContext);
            var result= (await stops.GetMatchedRides(matchedRidesRequestDTO)).Where(e=>e.AvailableSeats>=matchedRidesRequestDTO.SeatsRequired).ToList();
            for(int i=0; i<result.Count; i++)
            {
                result[i].Image = userDetailsService.GetImage(result[i].Image);
            }
            return result;
        }


    }
}
