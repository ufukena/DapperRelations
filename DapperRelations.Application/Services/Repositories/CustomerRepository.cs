using Dapper;
using DapperRelations.Application.Context.Contract;
using DapperRelations.Application.Context.Database;
using DapperRelations.Application.Services.Contracts;
using DapperRelations.Domain.Models;
using System.Data;


namespace DapperRelations.Application.Services.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly IDatabaseContext databaseContext;

        public CustomerRepository(IDatabaseContext _databaseContext)
        {
            databaseContext = _databaseContext ?? new DatabaseContext();
        }


        public async Task<Customer?> Get(int id)
        {
            var sql = @"SELECT 
                        c.*,
                        ct.*
                        FROM Customer c
                        JOIN CustomerType ct ON ct.Id = c.CustomerTypeId
                        WHERE c.Id = @Id AND c.DeletedFlg = 0;";


            using (IDbConnection connection = databaseContext.CreateConnection())
            {
                var parameters = new { Id = id };

                var customer = await connection.QueryAsync<Customer, CustomerType, Customer>(
                  sql,
                  (customer,customerType) =>
                  {
                      customer.CustomerType = customerType;                      

                      return customer;
                  },
                  param: parameters,
                  splitOn: "Id"
                  );

                return customer.FirstOrDefault();                
            }
        }

        public async Task<List<Customer>> GetAll()
        {
            var sql = @"SELECT 
                        c.*,
                        ct.*
                        FROM Customer c
                        JOIN CustomerType ct ON ct.Id = c.CustomerTypeId
                        WHERE c.DeletedFlg = 0;";


            using (IDbConnection connection = databaseContext.CreateConnection())
            {
                
                var customer = await connection.QueryAsync<Customer, CustomerType, Customer>(
                  sql,
                  (customer, customerType) =>
                  {
                      customer.CustomerType = customerType;

                      return customer;
                  },                  
                  splitOn: "Id"
                  );

                return customer.ToList();
                
            }
        }


       

        
    }
}
