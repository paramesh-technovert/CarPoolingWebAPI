namespace CarPoolingWebAPI.DTO
{
    public class MatchedRidesResponseDTO
    {
        public int RideId { get; set; }
        public int AvailableSeats { get; set; }
        public int BoardingStopId { get; set; }
        public int DestinationStopId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
