using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;
using CarPoolingWebAPI.Repository;
using System.Text;
using System.Text.RegularExpressions;

namespace CarPoolingWebAPI.Services
{
    public class LoginService
    {
        private readonly CarPoolingDbContext _dbContext;
        public LoginService(CarPoolingDbContext dbContext) : base()
        {
            _dbContext = dbContext;
        }
        private string EncryptData(string password)
        {
            byte[] encode = new byte[password.Length];
            encode=Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(encode);
        }
        public async Task<LoginCredentialsResponseDTO?> GetUserDetails(LoginCredentialsDTO loginCredentialsDTO)
        {
            LoginCredentials loginCredentials = new LoginCredentials(_dbContext);
            LoginDetail credential = await loginCredentials.GetUserDetails(loginCredentialsDTO.EmailId);
            if (credential != null)
            {
                if (credential.Password == EncryptData(loginCredentialsDTO.Password))
                {
                    
                    LoginCredentialsResponseDTO obj = new LoginCredentialsResponseDTO
                    {
                        UserId = credential.Id,
                        EmailId = credential.Email,
                        Password = loginCredentialsDTO.Password
                    };
                    return obj;
                }
                else
                {
                    throw new Exception("Wrong Password");
                }
            }
            throw new Exception("Unregistered Email");
        }
        public async Task<LoginDetail?> AddUser(LoginCredentialsDTO loginCredential)
        {
            if (loginCredential == null)
            {
                throw new ArgumentNullException(nameof(loginCredential));
            }
            if (_dbContext.LoginDetails.FirstOrDefault(e => e.Email == loginCredential.EmailId) == null)
            {
                loginCredential.Password=EncryptData(loginCredential.Password);
                LoginCredentials loginCredentials = new(_dbContext);
                LoginDetail credential = new LoginDetail
                {

                    Email = loginCredential.EmailId.ToLower(),
                    Password = loginCredential.Password
                };
                LoginDetail details = await loginCredentials.AddUser(credential);
                if (details != null)
                {
                    UserDetailsService userDetailsService = new UserDetailsService(_dbContext);
                    UserDetailsRequestDTO detailsRequestDTO=new UserDetailsRequestDTO { Id=details.Id,FirstName="New",LastName="User",ImageUrl=""};
                    await userDetailsService.AddUser(detailsRequestDTO);
                }
                return await loginCredentials.GetUserDetails(details.Email);
            }
            else
            {
                throw new Exception("Email Already Exists");
            }
        }
    }
}
