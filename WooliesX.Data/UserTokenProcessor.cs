using System;
using System.Collections.Generic;
using System.Text;
using WooliesX.Data.Entities;

namespace WooliesX.Data
{
    public interface IUserTokenProcessor
    {
        UserEntity GetUserToken();
    }

    public class UserTokenProcessor : IUserTokenProcessor
    {
        public UserEntity GetUserToken()
        {
            return new UserEntity 
            { 
                Name = "Jithin Jayasankar", 
                Token = Guid.NewGuid().ToString() 
            };
        }
    }
}
