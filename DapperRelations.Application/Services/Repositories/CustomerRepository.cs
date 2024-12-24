using Dapper;
using DapperRelations.Application.Context.Contract;
using DapperRelations.Application.Context.Database;
using DapperRelations.Application.Services.Contracts;
using DapperRelations.Domain.Models;
using DapperRelations.Infrastructure.Models;
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
                        ct.Id,
                        ct.CustomerTypeName,
                        ct.CustomerTypeDescription,
                        ct.OrderNo,
                        ct.CreatedDate,
                        ct.ModifiedDate,
                        u1.Id,
                        u1.UserName,                       
                        u2.Id,
                        u2.UserName,
                        u3.Id,
                        u3.UserName
                        FROM CustomerType ct
                        JOIN User u1 ON u1.Id = ct.CreatedById
                        LEFT JOIN User u2 ON u2.Id = ct.ModifiedById 
                        LEFT JOIN User u3 ON u3.Id = ct.DeletedById 
                        WHERE ct.Id = @Id;";


            using (IDbConnection connection = databaseContext.CreateConnection())
            {
                var parameters = new { Id = id };

                var customerType = await connection.QueryAsync<Customer, User, User, User, Customer>(
                  sql,
                  (customerType, createdBy, modifiedBy, deletedBy) =>
                  {
                      customerType.CreatedBy = createdBy;
                      customerType.ModifiedBy = modifiedBy;
                      customerType.DeletedBy = deletedBy;

                      return customerType;
                  },
                  param: parameters,
                  splitOn: "Id,Id,Id"
                  );

                return customerType.FirstOrDefault();

                //splitOn => Every item signs  begining new objet => UserId(u1 begin),UserId(u2 begin),UserId(u3 begin)
            }
        }

        public async Task<List<Customer>> GetAll()
        {
            var sql = @"SELECT 
                        ct.Id,
                        ct.CustomerTypeName,
                        ct.CustomerTypeDescription,
                        ct.OrderNo,
                        ct.CreatedDate,
                        ct.ModifiedDate,
                        u1.Id,
                        u1.UserName,                       
                        u2.Id,
                        u2.UserName,
                        u3.Id,
                        u3.UserName
                        FROM CustomerType ct
                        JOIN User u1 ON u1.Id = ct.CreatedById
                        LEFT JOIN User u2 ON u2.Id = ct.ModifiedById 
                        LEFT JOIN User u3 ON u3.Id = ct.DeletedById 
                        WHERE ct.Id = @Id;";


            using (IDbConnection connection = databaseContext.CreateConnection())
            {                

                var customerType = await connection.QueryAsync<Customer, User, User, User, Customer>(
                  sql,
                  (customerType, createdBy, modifiedBy, deletedBy) =>
                  {
                      customerType.CreatedBy = createdBy;
                      customerType.ModifiedBy = modifiedBy;
                      customerType.DeletedBy = deletedBy;

                      return customerType;
                  },                  
                  splitOn: "Id,Id,Id"
                  );

                return customerType.ToList();

                //splitOn => Every item signs  begining new objet => UserId(u1 begin),UserId(u2 begin),UserId(u3 begin)
            }
        }


        public Task<Customer> Create(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> Update(Customer entity)
        {
            throw new NotImplementedException();
        }

        public int GetAutoOrder()
        {
            throw new NotImplementedException();
        }

        
    }
}
