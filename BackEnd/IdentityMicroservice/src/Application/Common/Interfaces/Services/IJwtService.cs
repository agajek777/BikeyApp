using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces.Services
{
    public interface IJwtService
    {
        public Task<object> GenerateTokenAsync(User user);
        
        public Task<List<Claim>> GetValidClaims(User user);
    }
}