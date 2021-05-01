using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WooliesX.Application.Common.Interfaces;

namespace WooliesX.Application.UserDetails
{
    public class GetUserDetailsRequest : IRequest<UserDto>
    {
        public string Name { get; set; }
    }

    public class GetUserDetailsRequestHandler : IRequestHandler<GetUserDetailsRequest, UserDto>
    {
        private readonly IUserRespository _userRespository;
        private readonly IMapper _mapper;

        public GetUserDetailsRequestHandler(IUserRespository userRespository, IMapper mapper)
        {
            _userRespository = userRespository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserDetailsRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<UserDto>(await _userRespository.GetUserDetails(request.Name));
        }
    }
}
