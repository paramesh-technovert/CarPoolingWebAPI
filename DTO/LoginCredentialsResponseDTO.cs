﻿namespace CarPoolingWebAPI.DTO
{
    public class LoginCredentialsResponseDTO
    {
        public Guid UserId { get; set; }
        public string EmailId { get; set; } = null!;
        public string JWT { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
