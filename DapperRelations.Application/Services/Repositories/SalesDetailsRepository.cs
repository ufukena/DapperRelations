using DapperRelations.Application.Services.Contracts;
using DapperRelations.Domain.Models;

namespace DapperRelations.Application.Services.Repositories
{
    public class SalesDetailsRepository : ISalesDetailsRepository
    {
        public Task<SalesDetails> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<SalesDetails>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
