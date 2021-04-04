using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Error = Application.Common.Errors.Error;

namespace Infrastructure.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _dbContext;

        public ClientService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<bool>> CheckIfClientAvailableAsync(string requestClientId)
        {
            var existCheck = await CheckIfExistsAsync(requestClientId);

            if (existCheck.IsFaulted)
                return existCheck;

            var clientInDb = await _dbContext.Clients.FindAsync(requestClientId);

            if (clientInDb.NumOfHires >= Client.MaxNumOfHires)
                return new Result<bool>(new BadRequestException(Error.ClientFullHires));

            return new Result<bool>(true);
        }

        public async Task<Result<bool>> CheckIfExistsAsync(string requestClientId)
        {
            var outcome = await _dbContext.Clients.AnyAsync(c => c.Id == requestClientId);

            if (!outcome)
                return new Result<bool>(new BadRequestException(Error.ClientNotExists));

            return new Result<bool>(true);
        }

        public async Task AddClientAsync(Client client)
        {
            _dbContext.Add(client);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteClientAsync(Client client)
        {
            _dbContext.Remove(client);

            await _dbContext.SaveChangesAsync();
        }
    }
}