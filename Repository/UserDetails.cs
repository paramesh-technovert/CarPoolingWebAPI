using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.Models;

namespace CarPoolingWebAPI.Repository
{
    public class UserDetails
    {
        private readonly CarPoolingDbContext _dbcontext;
        public UserDetails(CarPoolingDbContext dbContext) { 
            _dbcontext = dbContext;
        }
        public async Task AddUserDetails(UserDetail userDetails)
        {
            await _dbcontext.UserDetails.AddAsync(userDetails);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
