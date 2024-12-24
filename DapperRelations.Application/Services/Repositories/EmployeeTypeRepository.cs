using Dapper;
using DapperRelations.Application.Context.Contract;
using DapperRelations.Application.Context.Database;
using DapperRelations.Application.Services.Contracts;
using DapperRelations.Domain.Models;
using System.Data;


namespace DapperRelations.Application.Services.Repositories
{
    public class EmployeeTypeRepository : IEmployeeTypeRepository
    {

        private readonly IDatabaseContext databaseContext;

        public EmployeeTypeRepository(IDatabaseContext _databaseContext)
        {
            databaseContext = _databaseContext ?? new DatabaseContext();
        }



        public async Task<EmployeeType?> Get(int id)
        {
            var sql = @"SELECT 
                        et.Id, et.EmployeeTypeName, et.OrderNo, 
                        et.CreatedDate, et.CreatedId, 
                        et.ModifiedDate, et.ModifiedId, et.DeletedDate, et.DeletedId, et.DeletedFlg                                            
                        FROM EmployeeType et                       
                        WHERE et.Id = @Id AND et.DeletedFlg = 0;";


            using (IDbConnection connection = databaseContext.CreateConnection())
            {
                var parameters = new { Id = id };

                var employeeType = await connection.QueryAsync<EmployeeType>(
                  sql,
                  param: parameters
                  );

                return employeeType.FirstOrDefault();


            }
        }

        public async Task<List<EmployeeType>> GetAll()
        {
            var sql = @"SELECT 
                        et.Id, et.EmployeeTypeName, et.OrderNo, 
                        et.CreatedDate, et.CreatedId, 
                        et.ModifiedDate, et.ModifiedId, et.DeletedDate, et.DeletedId, et.DeletedFlg                                            
                        FROM EmployeeType et 
                        WHERE et.DeletedFlg = 0";

            using (IDbConnection connection = databaseContext.CreateConnection())
            {
                var employeeType = await connection.QueryAsync<EmployeeType>(sql);

                return employeeType.ToList();
            }

        }


    }
}
