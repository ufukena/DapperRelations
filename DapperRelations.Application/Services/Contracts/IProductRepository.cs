using DapperRelations.Domain.Models;
using DapperRelations.Infrastructure.Contracts;


namespace DapperRelations.Application.Services.Contracts
{
    public interface IProductRepository : IGetRepository<Product>
    {
    }
}
