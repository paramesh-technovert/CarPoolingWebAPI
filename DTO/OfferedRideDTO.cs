﻿using CarPoolingWebAPI.Models;

namespace CarPoolingWebAPI.DTO
{
    public class OfferedRideDTO
    {
        public Guid RideOwnerId { get; set; }

        public String StartingStop { get; set; }

        public String EndingStop { get; set; }

        public DateTime Date { get; set; }

        public int Fair { get; set; }

        public int TotalSeats { get; set; }
        public List<StopsDTO> stops { get; set; }
    }
}
