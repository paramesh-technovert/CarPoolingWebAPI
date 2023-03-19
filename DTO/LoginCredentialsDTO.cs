namespace CarPoolingWebAPI.DTO
{
    public class LoginCredentialsDTO
    {
        public Guid UserId { get; set; }
        public string EmailId { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
