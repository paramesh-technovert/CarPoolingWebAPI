using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;
using CarPoolingWebAPI.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarPoolingWebAPI.Services
{
    public class BookRideService
    {
        private readonly CarPoolingDbContext _dbContext;
        public BookRideService(CarPoolingDbContext dbContext) : base()
        {
            _dbContext = dbContext;
        }
        public async Task<BookRideResponseDTO> BookRide(BookRideRequestDTO bookingRideRequestDTO)
        {
            OfferedRides offeredRide = new OfferedRides(_dbContext);
            Cities cities = new Cities(_dbContext);
            Stops stops = new Stops(_dbContext);
            stops.BookingRide(bookingRideRequestDTO);
            bookingRideRequestDTO.BoardingStopId = stops.GetStopId(bookingRideRequestDTO.BoardingStopId);
            bookingRideRequestDTO.DestinationStopId = stops.GetStopId(bookingRideRequestDTO.DestinationStopId);
            OfferRide? offerRide = await offeredRide.GetRideDetails(bookingRideRequestDTO.RideId);
            BookRideResponseDTO bookingDetailsDTO = new()
            {
                RideId = bookingRideRequestDTO.RideId,
                SeatsBooked = bookingRideRequestDTO.BookedSeats,
                Id = bookingRideRequestDTO.Id,
                ProviderId = offerRide!.RideProviderId,
                Fair = offerRide!.Fair,
                TotalAmount = offerRide!.Fair * bookingRideRequestDTO.BookedSeats,
                Date = bookingRideRequestDTO.DateTime,
                BoardingPoint = cities.GetCityName(bookingRideRequestDTO.BoardingStopId),
                DroppingPoint = cities.GetCityName(bookingRideRequestDTO.DestinationStopId)
            };
            BookRides bookRides = new BookRides(_dbContext);
            try
            {
                bookRides.BookRide(bookingRideRequestDTO, offerRide.Fair);
                _dbContext.SaveChanges();
            }catch (Exception ex)
            {
                throw new Exception("Couldn't Book a ride Please Try Again");
            }
            return bookingDetailsDTO;
        }
        public List<BookedRidesDTO> GetBookedRides(Guid userId)
        {
            UserDetailsService userDetailsService = new UserDetailsService(_dbContext);
            BookRides bookRides = new BookRides(_dbContext);
            var result=bookRides.GetBookedRides(userId).ToList();
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].Image != null)
                    result[i].Image = userDetailsService.GetImage(result[i].Image);
            }
            return result.ToList();
        }
        /*public List<BookingDetailsDTO> GetBookedRides(Guid userId)
        {
            Cities cities = new Cities(_dbContext);
            BookRides bookRides = new BookRides(_dbContext);
            IQueryable<BookedRide> rides= bookRides.GetBookedRides(userId);
            List<BookingDetailsDTO> bookingDetails=new List<BookingDetailsDTO>();
            foreach(BookedRide ride in rides)
            {
                BookingDetailsDTO bookingDetailsDTO = new BookingDetailsDTO()
                {
                    RideId = ride.RideId,
                    BookingId = ride.BookingId,
                    CustomerId = ride.CustomerId,
                    SeatsBooked = ride.SeatsBooked,
                    Fair = ride.Fair,
                    TotalFair = ride.Fair * ride.SeatsBooked,
                    Date = ride.Date,
                    BoardingStop = cities.GetCityName(ride.BoardingStop),
                    DestinationStop = cities.GetCityName(ride.DestinationStop)
                };
                bookingDetails.Add(bookingDetailsDTO);
            }
            return bookingDetails;
        }*/
    }
}
