using DapperRelations.Application.Context.Contract;
using DapperRelations.Application.Context.Database;
using DapperRelations.Application.Services.Contracts;
using DapperRelations.Domain.Models;
using System.Data;


namespace DapperRelations.Application.Services.Repositories
{
    public class SalesRepository : ISalesRepository
    {

        private readonly IDatabaseContext databaseContext;

        public SalesRepository(IDatabaseContext _databaseContext)
        {
            databaseContext = _databaseContext ?? new DatabaseContext();
        }


        public async Task<Sales?> Get(int id)
        {
            var sql = @"SELECT 
                        p.*,
                        pt.*
                        FROM Product p
                        JOIN ProductType pt ON pt.Id = p.ProductTypeId
                        WHERE p.Id = @Id AND p.DeletedFlg = 0;";


            using (IDbConnection connection = databaseContext.CreateConnection())
            {
                var parameters = new { Id = id };

                var product = await connection.QueryAsync<Product, ProductType, Product>(
                  sql,
                  (product, productType) =>
                  {
                      product.ProductType = productType;

                      return product;
                  },
                  param: parameters,
                  splitOn: "Id"
                  );

                return product.FirstOrDefault();
            }
        }

        public async Task<List<Product>> GetAll()
        {
            var sql = @"SELECT 
                        p.*,
                        pt.*
                        FROM Product p
                        JOIN ProductType pt ON pt.Id = p.ProductTypeId
                        WHERE p.DeletedFlg = 0;";


            using (IDbConnection connection = databaseContext.CreateConnection())
            {

                var product = await connection.QueryAsync<Product, ProductType, Product>(
                  sql,
                  (product, productType) =>
                  {
                      product.ProductType = productType;

                      return product;
                  },
                  splitOn: "Id"
                  );

                return product.ToList();

            }
        }




    }
}
