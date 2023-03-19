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
        public async Task<IQueryable<MatchedRidesResponseDTO>> GetMatchedRides(MatchedRidesRequestDTO matchedRidesRequestDTO)
        {
            Stops stops = new Stops(_dbContext);
            return await stops.GetMatchedRides(matchedRidesRequestDTO);
        }


    }
}
