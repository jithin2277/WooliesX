using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WooliesX.Application.Common.Interfaces;
using WooliesX.Domain.Entities;

namespace WooliesX.Infrastructure.Respositories
{
    public class UserRespository : IUserRespository
    {
        private readonly string _token;
        private readonly ILogger<UserRespository> _logger;

        public UserRespository(IOptions<AppSettings> appSettingsOptions, ILogger<UserRespository> logger)
        {
            var appSettings = appSettingsOptions.Value;
            if (string.IsNullOrEmpty(appSettings.Token))
            {
                throw new ArgumentNullException("Token is null or empty");
            }

            _token = appSettings.Token;
            _logger = logger;
        }

        public async Task<UserEntity> GetUserDetails(string name)
        {
            _logger.LogInformation($"GetUserDetails: Getting User details for: {name}");

            return await Task.FromResult(new UserEntity
            {
                Name = name,
                Token = _token
            });
        }
    }
}
