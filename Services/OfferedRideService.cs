using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;
using CarPoolingWebAPI.Repository;

namespace CarPoolingWebAPI.Services
{
    public class OfferedRideService
    {
        private readonly CarPoolingDbContext _dbContext;
        public OfferedRideService(CarPoolingDbContext dbContext) : base()
        {
            _dbContext = dbContext;
        }
        /*public Task<List<OfferRide>> GetOfferedRides(Guid id)
        {
            OfferedRides offeredRides = new(_dbContext);
            return offeredRides.GetOfferedRides(id);
        }*/
        public Task<List<OfferedRidesResponseDTO>> GetOfferedRides(Guid id)
        {
            OfferedRides offeredRides = new(_dbContext);
            return offeredRides.GetOfferedRides(id);
        }
        public async Task<OfferRide> OfferRide(OfferedRideDTO offeredRide)
        {
            Cities cities = new Cities(_dbContext);
            string city = offeredRide.StartingStop.ToLower().Trim();
            int sourceId = await cities.GetCityID(city);
            if (sourceId == 0)
            {
                await cities.AddCity(city);
                sourceId = await cities.GetCityID(city);
            }
            city = offeredRide.EndingStop.ToLower().Trim();
            int destinationId = await cities.GetCityID(city);
            if (destinationId == 0)
            {
                await cities.AddCity(city);
                destinationId = await cities.GetCityID(city);
            }
            if (sourceId == destinationId)
            {
                throw new Exception("Destination Can't Be same as source");
            }
            OfferRide ride = new OfferRide()
            {
                RideProviderId = offeredRide.RideOwnerId,
                Distance = offeredRide.Distance,
                Fair = offeredRide.Fair,
                SourceId = sourceId,
                DestinationId = destinationId,
                Date = Convert.ToDateTime(offeredRide.Date + " " + offeredRide.Time),
                TotalSeats = offeredRide.TotalSeats

            };
            offeredRide.stops.Insert(0, new StopsDTO { StopName = offeredRide.StartingStop.ToLower(), PickupTime = offeredRide.Time, PickupDate = offeredRide.Date });
            offeredRide.stops.Add(new StopsDTO { StopName = offeredRide.EndingStop.ToLower() });
            Stops stops = new Stops(_dbContext);
            OfferedRides offeredRides = new OfferedRides(_dbContext);
            OfferRide rideDetails = await offeredRides.OfferRide(ride);
            for (int i = 0; i < offeredRide.stops.Count(); i++)
            {
                Stop stop = new Stop();
                if (i != 0 && i != offeredRide.stops.Count() - 1)
                {
                    if (Convert.ToDateTime(offeredRide.stops[i].PickupDate + " " + offeredRide.stops[i].PickupTime) < Convert.ToDateTime(offeredRide.stops[i - 1].PickupDate + " " + offeredRide.stops[i - 1].PickupTime))
                    {
                        throw new Exception("Invalid Stop Details");
                    }
                }
                stop.RideId = rideDetails.RideId;
                stop.StopId = await cities.GetCityID(offeredRide.stops[i].StopName.ToLower().Trim());
                if (stop.StopId == 0)
                {
                    await cities.AddCity(offeredRide.stops[i].StopName.ToLower().Trim());
                    stop.StopId = await cities.GetCityID(offeredRide.stops[i].StopName.ToLower().Trim());
                }
                stop.AvailableSeats = offeredRide.TotalSeats;
                if (i == offeredRide.stops.Count() - 1)
                {
                    stop.Date = Convert.ToDateTime(offeredRide.stops[i - 1].PickupDate + " " + offeredRide.stops[i - 1].PickupTime).AddHours(1);
                }
                else
                {
                    stop.Date = Convert.ToDateTime(offeredRide.stops[i].PickupDate + " " + offeredRide.stops[i].PickupTime);
                }
                await stops.AddStops(stop);

            }
            await _dbContext.SaveChangesAsync();
            return rideDetails;
        }
    }
}
