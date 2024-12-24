using Dapper;
using DapperRelations.Application.Context.Contract;
using DapperRelations.Application.Context.Database;
using DapperRelations.Application.Services.Contracts;
using DapperRelations.Domain.Models;
using System.Data;


namespace DapperRelations.Application.Services.Repositories
{
    public class CustomerTypeRepository : ICustomerTypeRepository
    {

        private readonly IDatabaseContext databaseContext;

        public CustomerTypeRepository(IDatabaseContext _databaseContext)
        {
            databaseContext = _databaseContext ?? new DatabaseContext();
        }



        public async Task<CustomerType?> Get(int id)
        {
            var sql = @"SELECT 
                        ct.Id, ct.CustomerTypeName, ct.CustomerTypeDescription, ct.OrderNo, 
                        ct.CreatedDate, ct.CreatedId, 
                        ct.ModifiedDate, ct.ModifiedId, ct.DeletedDate, ct.DeletedId, ct.DeletedFlg                                            
                        FROM CustomerType ct                       
                        WHERE ct.Id = @Id AND ct.DeletedFlg = 0;";            


            using (IDbConnection connection = databaseContext.CreateConnection())
            {
                var parameters = new { Id = id };

                var customerType = await connection.QueryAsync<CustomerType>(
                  sql,                  
                  param: parameters                  
                  );

                return customerType.FirstOrDefault();

                
            }
        }

        public async Task<List<CustomerType>> GetAll()
        {
            var sql = @"SELECT 
                        ct.Id, ct.CustomerTypeName, ct.CustomerTypeDescription, ct.OrderNo, 
                        ct.CreatedDate, ct.CreatedId, 
                        ct.ModifiedDate, ct.ModifiedId, ct.DeletedDate, ct.DeletedId, ct.DeletedFlg                                           
                        FROM CustomerType ct WHERE ct.DeletedFlg = 0";


            using (IDbConnection connection = databaseContext.CreateConnection())
            {                
                var customerType = await connection.QueryAsync<CustomerType>(sql);

                return customerType.ToList();
            }

        }


       


    }
}
