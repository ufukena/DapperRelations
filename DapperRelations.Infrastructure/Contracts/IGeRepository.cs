
namespace DapperRelations.Infrastructure.Contracts
{

    public interface IGeRepository<TEntity>
    {
        Task<TEntity> Get(int id);
        Task<List<TEntity>> GetAll();

        //Add alternatives

    }
    

}
