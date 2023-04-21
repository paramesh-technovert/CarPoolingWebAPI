﻿using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;

namespace CarPoolingWebAPI.Repository
{
    public interface IBookRides
    {
        public BookRideRequestDTO BookRide(BookRideRequestDTO bookRideRequestDTO, int Fair);
        public IQueryable<BookedRidesDTO> GetBookedRides(Guid userId);
        //public IQueryable<BookedRide> GetBookedRides(Guid userId);
    }
}
