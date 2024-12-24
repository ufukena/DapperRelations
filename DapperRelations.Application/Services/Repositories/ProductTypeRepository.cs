using Dapper;
using DapperRelations.Application.Context.Contract;
using DapperRelations.Application.Context.Database;
using DapperRelations.Application.Services.Contracts;
using DapperRelations.Domain.Models;
using System.Data;


namespace DapperRelations.Application.Services.Repositories
{
    public class ProductTypeRepository : IProductTypeRepository
    {

        private readonly IDatabaseContext databaseContext;

        public ProductTypeRepository(IDatabaseContext _databaseContext)
        {
            databaseContext = _databaseContext ?? new DatabaseContext();
        }



        public async Task<ProductType?> Get(int id)
        {
            var sql = @"SELECT 
                        pt.Id, pt.ProductTypeName, pt.OrderNo, 
                        pt.CreatedDate, pt.CreatedId, 
                        pt.ModifiedDate, pt.ModifiedId, pt.DeletedDate, pt.DeletedId, pt.DeletedFlg                                            
                        FROM ProductType pt                       
                        WHERE pt.Id = @Id AND pt.DeletedFlg = 0;";


            using (IDbConnection connection = databaseContext.CreateConnection())
            {
                var parameters = new { Id = id };

                var employeeType = await connection.QueryAsync<ProductType>(
                  sql,
                  param: parameters
                  );

                return employeeType.FirstOrDefault();


            }
        }

        public async Task<List<ProductType>> GetAll()
        {
            var sql = @"SELECT 
                        pt.Id, pt.ProductTypeName, pt.OrderNo, 
                        pt.CreatedDate, pt.CreatedId, 
                        pt.ModifiedDate, pt.ModifiedId, pt.DeletedDate, pt.DeletedId, pt.DeletedFlg                                            
                        FROM ProductType pt                       
                        WHERE pt.DeletedFlg = 0;";

            using (IDbConnection connection = databaseContext.CreateConnection())
            {
                var employeeType = await connection.QueryAsync<ProductType>(sql);

                return employeeType.ToList();
            }

        }




    }
}
