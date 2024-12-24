using Dapper;
using DapperRelations.Application.Context.Contract;
using DapperRelations.Application.Context.Database;
using DapperRelations.Application.Services.Contracts;
using DapperRelations.Domain.Models;
using DapperRelations.Infrastructure.Models;
using System.Data;
using System.Data.Common;


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
                        Id,
                        CustomerTypeName,
                        CustomerTypeDescription,
                        OrderNo,
                        CreatedDate,
                        ModifiedDate,
                        CreatedUserId,
                        CreatedUserName,                       
                        ModifiedUserId,
                        ModifiedUserName,
                        DeletedUserId,
                        DeletedUserName
                        FROM VW_CustomerType 
                        WHERE Id = @Id;";            


            using (IDbConnection connection = databaseContext.CreateConnection())
            {
                var parameters = new { Id = id };

                var customerType = await connection.QueryAsync<CustomerType, User, User, User, CustomerType>(
                  sql,
                  (customerType, createdBy, modifiedBy, deletedBy) =>
                  {
                      customerType.CreatedBy = createdBy;
                      customerType.ModifiedBy = modifiedBy;
                      customerType.DeletedBy = deletedBy;
                      
                      return customerType;
                  },
                  param: parameters,
                  splitOn: "CreatedUserId,ModifiedUserId,DeletedUserId"
                  );

                return customerType.FirstOrDefault();

                //splitOn => Every item signs  begining new objet => UserId(u1 begin),UserId(u2 begin),UserId(u3 begin)
            }
        }

        public async Task<List<CustomerType>> GetAll()
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
                        ";


            using (IDbConnection connection = databaseContext.CreateConnection())
            {
                

                var customerType = await connection.QueryAsync<CustomerType, User, User, User, CustomerType>(
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
                
            }
        }


        public Task<CustomerType> Create(CustomerType entity)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerType> Update(CustomerType entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(CustomerType entity)
        {
            throw new NotImplementedException();
        }

        public int GetAutoOrder()
        {
            throw new NotImplementedException();
        }



    }
}
