using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;
using CarPoolingWebAPI.Repository;

namespace CarPoolingWebAPI.Services
{
    public class UserDetailsService
    {
        private readonly CarPoolingDbContext _dbContext;
        public UserDetailsService(CarPoolingDbContext dbContext) : base()
        {
            _dbContext = dbContext;
        }
        public async Task<UserDetailsRequestDTO> AddUser(UserDetailsRequestDTO userDetailsRequestDTO)
        {
            string folderPath = "F:/CarPoolingWebAPI/Images";
            string filePath;
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                string imagePath = @"F:/Default.png";
                byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                string defaultImage = "F:/CarPoolingWebAPI/Images/Default";
                using (FileStream stream = new FileStream(defaultImage, FileMode.Create))
                {
                    stream.Write(imageBytes, 0, imageBytes.Length);
                }
            }
            UserDetail userDetail = new UserDetail()
            {
                Id = userDetailsRequestDTO.Id,
                FirstName = userDetailsRequestDTO.FirstName,
                LastName = userDetailsRequestDTO.LastName,
                PhoneNumber = userDetailsRequestDTO.PhoneNumber,

            };
            if (userDetailsRequestDTO.ImageUrl != "")
            {

                filePath = Path.Combine(folderPath, userDetailsRequestDTO.Id.ToString());
                byte[] imageBytes = Convert.FromBase64String(userDetailsRequestDTO.ImageUrl);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    stream.Write(imageBytes, 0, imageBytes.Length);
                }

            }
            else
            {
                filePath = Path.Combine(folderPath, "Default");
            }
            userDetail.ImageUrl = filePath;
            UserDetails userDetails = new UserDetails(_dbContext);
            var result=await userDetails.AddUserDetails(userDetail);
            return new UserDetailsRequestDTO { Id = result.Id, FirstName = result.FirstName, LastName = result.LastName, PhoneNumber = result.PhoneNumber, ImageUrl = GetImage(result.ImageUrl) };
        }
        public async Task<UserDetailsRequestDTO> GetUserDetails(Guid id)
        {
            UserDetails userDetails = new UserDetails(_dbContext);
            UserDetail userDetail= await userDetails.GetUserDetails(id);
            if (userDetail != null)
            {
                if (userDetail.ImageUrl != null)
                {
                    byte[] imagePath = System.IO.File.ReadAllBytes(userDetail.ImageUrl);
                    string base64String = "data:image/png;base64," + Convert.ToBase64String(imagePath);
                    userDetail.ImageUrl = base64String;
                }
                return new UserDetailsRequestDTO { Id = userDetail.Id, FirstName = userDetail.FirstName, LastName = userDetail.LastName, ImageUrl = userDetail.ImageUrl, PhoneNumber = userDetail.PhoneNumber };
            }
            return null;
        }
        public string GetImage(string path)
        {
                byte[] imagePath = System.IO.File.ReadAllBytes(path);
                string base64String = "data:image/png;base64," + Convert.ToBase64String(imagePath);
                return base64String;
        }
    }
}
