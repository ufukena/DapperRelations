
namespace DapperRelations.Infrastructure.Contracts
{

    public interface IGetAllRepository<TEntityDto>
    {

        Task<List<TEntityDto>> GetAll();

        //Add alternatives

    }
    

}
