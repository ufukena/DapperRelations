using System.Data;


namespace DapperRelations.Application.Context.Contract
{
    public interface IDatabaseContext
    {
        IDbConnection CreateConnection();
    }
}
