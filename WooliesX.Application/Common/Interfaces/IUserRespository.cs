using System.Threading.Tasks;
using WooliesX.Domain.Entities;

namespace WooliesX.Application.Common.Interfaces
{
    public interface IUserRespository
    {
        Task<UserEntity> GetUserDetails(string name);
    }
}
