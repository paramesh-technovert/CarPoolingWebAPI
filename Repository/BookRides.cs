using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;

namespace CarPoolingWebAPI.Repository
{
    public class BookRides : IBookRides
    {
        private readonly CarPoolingDbContext _dbContext;
        public BookRides(CarPoolingDbContext dbContext) : base()
        {
            _dbContext = dbContext;
        }
        public BookRideRequestDTO BookRide(BookRideRequestDTO bookRideRequestDTO,int Fair)
        {
            BookedRide bookedRide=new BookedRide()
            {
                CustomerId=bookRideRequestDTO.Id,
                RideId=bookRideRequestDTO.RideId,
                BoardingStop=bookRideRequestDTO.BoardingStopId,
                DestinationStop=bookRideRequestDTO.DestinationStopId,
                Date=bookRideRequestDTO.DateTime,
                SeatsBooked=bookRideRequestDTO.BookedSeats,
                Fair=Fair
            };
            _dbContext.BookedRides.Add(bookedRide);
            _dbContext.SaveChanges();
            return bookRideRequestDTO;

        }
        public IQueryable<BookingDetailsDTO> GetBookedRides(Guid userId)
        {
            return from c in from a in _dbContext.BookedRides join b in _dbContext.Cities on a.CustomerId equals userId where a.BoardingStop == b.CityId select new{a.RideId, a.CustomerId, a.BookingId, a.Date, a.Fair, a.SeatsBooked, a.DestinationStop,b.CityName} join d in _dbContext.Cities on c.DestinationStop equals d.CityId select (new BookingDetailsDTO { RideId=c.RideId,CustomerId=c.CustomerId,BookingId=c.BookingId,Fair=c.Fair,SeatsBooked=c.SeatsBooked,Date=c.Date,TotalFair=c.Fair*c.SeatsBooked,BoardingStop=c.CityName,DestinationStop=d.CityName});
        }
        /*public IQueryable<BookedRide> GetBookedRides(Guid userId)
        {
            return _dbContext.BookedRides.Where(x=>x.CustomerId==userId).AsQueryable();
        }*/
    }
}
