using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;
using CarPoolingWebAPI.Repository;
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
        private bool IsValidMail(string email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }
        private bool IsValidPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            if (password.Length > 7 && password.Length < 26)
            {
                return true;
            }
            return false;
        }
        public async Task<LoginCredentialsDTO?> GetUserDetails(string email, string password)
        {
            if (email == null)
            {
                throw new ArgumentNullException("email");
            }
            LoginCredentials loginCredentials = new LoginCredentials(_dbContext);
            LoginDetail credential = await loginCredentials.GetUserDetails(email);
            if (credential != null)
            {
                if (credential.Password == password)
                {
                    LoginCredentialsDTO obj = new LoginCredentialsDTO
                    {
                        UserId = credential.Id,
                        EmailId = credential.Email,
                        Password = credential.Password
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
                if (IsValidMail(loginCredential.EmailId))
                {
                    if (IsValidPassword(loginCredential.Password))
                    {
                        LoginCredentials loginCredentials = new(_dbContext);
                        LoginDetail credential = new LoginDetail
                        {

                            Email = loginCredential.EmailId,
                            Password = loginCredential.Password
                        };
                        LoginDetail details = await loginCredentials.AddUser(credential);
                        if (details != null)
                        {
                            UserDetail userDetail = new UserDetail { Id = details.Id };
                            UserDetails userDetails = new UserDetails(_dbContext);
                            userDetails.AddUserDetails(userDetail);
                        }
                        return details;
                    }
                    else
                    {
                        throw new Exception("Invalid Password!Password length must be a minimum of 8 and a maximum of 25");
                    }
                }
                else
                {
                    throw new Exception("Invalid Mail Id");
                }
            }
            else
            {
                throw new Exception("Email Already Exists");
            }
        }
    }
}
