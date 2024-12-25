
namespace DapperRelations.Infrastructure.Contracts
{

    public interface IGetRepository<TEntity>
    {
        Task<TEntity> Get(int id);
        Task<List<TEntity>> GetAll();

        //Add alternatives

    }
    

}
