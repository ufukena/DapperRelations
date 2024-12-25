using Dapper;
using DapperRelations.Application.Context.Contract;
using DapperRelations.Application.Context.Database;
using DapperRelations.Application.Services.Contracts;
using DapperRelations.Domain.Models;
using System;
using System.Collections.Specialized;
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
                        s.*,
                        c.*, ct.*,
                        em.*, et.*,
                        sd.*
                        FROM Sales s
                        JOIN Customer c ON c.Id = s.CustomerId
                        JOIN CustomerType ct ON ct.Id = c.CustomerTypeId
                        JOIN Employee em ON em.Id = s.EmployeeId
                        JOIN EmployeeType et ON et.Id = em.EmployeeTypeId
                        JOIN SalesDetail sd ON sd.SalesId = s.Id
                        WHERE s.Id = @Id AND s.DeletedFlg = 0;";


            using (IDbConnection connection = databaseContext.CreateConnection())
            {
                var parameters = new { Id = id };

                var salesDictionary = new Dictionary<long, Sales>();

                var sales = await connection.QueryAsync<Sales, Customer, CustomerType, Employee, EmployeeType, SalesDetail, Sales>(
                  sql,
                  (sales, customer, customerType, employee, employeeType, salesDetail) =>
                  {
                      if (salesDictionary.TryGetValue(sales.Id, out var existingOrder))
                      {
                          sales = existingOrder;
                      }
                      else
                      {
                          salesDictionary.Add(sales.Id, sales);
                      }

                      sales.Customer = customer;
                      sales.Customer.CustomerType = customerType;
                      sales.Employee = employee;
                      sales.Employee.EmployeeType = employeeType;

                      //Product in Sales not bind..if you want you can add here..

                      sales.SalesDetails.Add(salesDetail);
                      
                      return sales;

                  },
                  param: parameters,
                  splitOn: "Id"
                  );
                  

                return sales.FirstOrDefault();
            }
        }

        public async Task<List<Sales>> GetAll()
        {
            var sql = @"SELECT 
                        s.*,
                        c.*, ct.*,
                        em.*, et.*,
                        sd.*
                        FROM Sales s
                        JOIN Customer c ON c.Id = s.CustomerId
                        JOIN CustomerType ct ON ct.Id = c.CustomerTypeId
                        JOIN Employee em ON em.Id = s.EmployeeId
                        JOIN EmployeeType et ON et.Id = em.EmployeeTypeId
                        JOIN SalesDetail sd ON s.Id = sd.SalesId
                        WHERE s.DeletedFlg = 0;";


            using (IDbConnection connection = databaseContext.CreateConnection())
            {
                
                var salesDictionary = new Dictionary<int, Sales>();

                var sales = await connection.QueryAsync<Sales, Customer, CustomerType, Employee, EmployeeType, SalesDetail, Sales>(
                  sql,
                  (sales, customer, customerType, employee, employeeType, salesDetail) =>
                  {

                      Sales salesEntity;
                      //trip
                      if (!salesDictionary.TryGetValue(sales.Id, out salesEntity))
                      {
                          salesDictionary.Add(sales.Id, salesEntity = sales);
                      }

                      sales.Customer = customer;
                      sales.Customer.CustomerType = customerType;
                      sales.Employee = employee;
                      sales.Employee.EmployeeType = employeeType;

                      //Product in Sales not bind..if you want you can add here..

                      if (!salesEntity.SalesDetails.Any(x => x.Id == salesDetail.Id))
                      {
                          salesEntity.SalesDetails.Add(salesDetail);
                      }

                      return salesEntity;

                  },
                  splitOn: "Id"                  
                  );


                return salesDictionary.Values.ToList();
            }
        }




    }
}
