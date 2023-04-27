﻿using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;

namespace CarPoolingWebAPI.Repository
{
    public interface IUserDetails
    {
        public UserDetail GetUserDetail(Guid id);
        public UserDetailsRequestDTO SetUserDetail(UserDetailsRequestDTO userDetailsDTO);
    }
}
