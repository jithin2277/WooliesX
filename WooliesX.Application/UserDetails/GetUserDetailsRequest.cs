using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WooliesX.Application.Core;

namespace WooliesX.Application.UserDetails
{
    public class GetUserDetailsRequest : IRequest<UserDto>
    {
        public string Name { get; set; }
    }

    public class GetUserDetailsRequestHandler : IRequestHandler<GetUserDetailsRequest, UserDto>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetUserDetailsRequestHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserDetailsRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<UserDto>(await _userService.GetUserDetails(request.Name));
        }
    }
}
