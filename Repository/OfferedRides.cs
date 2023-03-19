using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarPoolingWebAPI.Repository
{
    public class OfferedRides : IOfferedRides
    {
        private readonly CarPoolingDbContext _dbContext;
        public OfferedRides(CarPoolingDbContext dbContext) : base()
        {
            _dbContext = dbContext;
        }
        /*public async Task<List<OfferRide>> GetOfferedRides(Guid id)
        {
            return  _dbContext.OfferRides.Where(e=>e.RideProviderId==id).ToList();
        }*/
        public async Task<List<OfferedRidesResponseDTO>> GetOfferedRides(Guid id)
        {
            return (from c in from a in _dbContext.OfferRides join b in _dbContext.Cities on a.RideProviderId equals id where a.SourceId == b.CityId select new { a.RideId,a.RideProviderId, a.Date, a.TotalSeats, a.Fair,a.Distance, a.DestinationId, b.CityName } join d in _dbContext.Cities on c.DestinationId equals d.CityId select (new OfferedRidesResponseDTO{ RideId=c.RideId,RideOwnerId=c.RideProviderId,Date=c.Date,Distance=c.Distance,Fair=c.Fair,TotalSeats=c.TotalSeats,StartingStop=c.CityName,EndingStop=d.CityName})).ToList();
        }
        public async Task<OfferRide?> GetRideDetails(int RideId)
        {
            return await _dbContext.OfferRides.FindAsync(RideId);
        }

        public async Task<OfferRide> OfferRide(OfferRide offeredRide)
        {
            try
            {
                await _dbContext.OfferRides.AddAsync(offeredRide);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex);
            }
            return offeredRide;
        }
    }
}
