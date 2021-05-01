using WooliesX.Application.Common.Mappings;
using WooliesX.Domain.Entities;

namespace WooliesX.Application.UserDetails
{
    public class UserDto : IMapFrom<UserEntity>
    {
        public string Name { get; set; }
        public string Token { get; set; }
    }
}
