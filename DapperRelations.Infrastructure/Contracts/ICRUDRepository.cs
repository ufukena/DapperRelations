

namespace DapperRelations.Infrastructure.Contracts
{


    public interface ICRUDRepository<TEntity>
    {
        
        Task<TEntity> Get(int id);

        Task<TEntity> Create(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task Delete(TEntity entity);


    }

    

}
