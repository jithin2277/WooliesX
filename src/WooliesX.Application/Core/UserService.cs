using System.Threading.Tasks;
using WooliesX.Application.Common.Interfaces;
using WooliesX.Domain.Entities;

namespace WooliesX.Application.Core
{
    public interface IUserService
    {
        Task<UserEntity> GetUserDetails(string name);
    }

    public class UserService : IUserService
    {
        private readonly IUserRespository _userRespository;

        public UserService(IUserRespository userRespository)
        {
            _userRespository = userRespository;
        }

        public async Task<UserEntity> GetUserDetails(string name)
        {
            return await _userRespository.GetUserDetails(name).ConfigureAwait(false);
        }
    }
}
