using Dapper;
using DapperRelations.Application.Context.Contract;
using DapperRelations.Application.Context.Database;
using DapperRelations.Application.Services.Contracts;
using DapperRelations.Domain.Models;
using System.Data;


namespace DapperRelations.Application.Services.Repositories
{
    public class SalesDetailRepository : ISalesDetailRepository
    {

        private readonly IDatabaseContext databaseContext;

        public SalesDetailRepository(IDatabaseContext _databaseContext)
        {
            databaseContext = _databaseContext ?? new DatabaseContext();
        }


        public async Task<SalesDetail?> Get(int id)
        {
            var sql = @"SELECT 
                        sd.*,
                        s.*, c.*, ct.*, em.*, et.*,
                        p.*, pt.*                        
                        FROM SalesDetail sd
                        JOIN Sales s ON s.Id = sd.SalesId   
                        JOIN Customer c ON c.Id = s.CustomerId   
                        JOIN CustomerType ct ON ct.Id = c.CustomerTypeId   
                        JOIN Employee em ON em.Id = s.EmployeeId   
                        JOIN EmployeeType et ON et.Id = em.EmployeeTypeId   
                        JOIN Product p ON p.Id = sd.ProductId                        
                        JOIN ProductType pt ON pt.Id = p.ProductTypeId                        
                        WHERE sd.Id = @Id AND sd.DeletedFlg = 0;";


            using (IDbConnection connection = databaseContext.CreateConnection())
            {
                var parameters = new { Id = id };

                /*Dapper normally support 7 Type..more than 7 using this...*/

                Type[] types = new Type[] 
                { typeof(SalesDetail), typeof(Sales), typeof(Customer), typeof(CustomerType), typeof(Employee), typeof(EmployeeType), typeof(Product), typeof(ProductType) };

                Func<object[], SalesDetail> map = objects =>
                {
                    var saleDetails = (SalesDetail)objects[0];
                    saleDetails.Sales = (Sales)objects[1];
                    saleDetails.Sales.Customer = (Customer)objects[2];
                    saleDetails.Sales.Customer.CustomerType = (CustomerType)objects[3];
                    saleDetails.Sales.Employee = (Employee)objects[4];
                    saleDetails.Sales.Employee.EmployeeType = (EmployeeType)objects[5];
                    saleDetails.Product = (Product)objects[6];
                    saleDetails.Product.ProductType = (ProductType)objects[7];

                    return saleDetails;
                };

                var salesDetail = await connection.QueryAsync(
                  sql, types, map,                  
                  param: parameters,
                  splitOn: "Id"
                  );

                return salesDetail.FirstOrDefault();
            }
        }

        public async Task<List<SalesDetail>> GetAll()
        {
            var sql = @"SELECT 
                        sd.*,
                        s.*, c.*, ct.*, em.*, et.*,
                        p.*, pt.*                        
                        FROM SalesDetail sd
                        JOIN Sales s ON s.Id = sd.SalesId   
                        JOIN Customer c ON c.Id = s.CustomerId   
                        JOIN CustomerType ct ON ct.Id = c.CustomerTypeId   
                        JOIN Employee em ON em.Id = s.EmployeeId   
                        JOIN EmployeeType et ON et.Id = em.EmployeeTypeId   
                        JOIN Product p ON p.Id = sd.ProductId                        
                        JOIN ProductType pt ON pt.Id = p.ProductTypeId                        
                        WHERE sd.DeletedFlg = 0;";


            using (IDbConnection connection = databaseContext.CreateConnection())
            {
                
                /*Dapper normally support 7 Type..more than 7 using this...*/

                Type[] types = new Type[]
                { typeof(SalesDetail), typeof(Sales), typeof(Customer), typeof(CustomerType), typeof(Employee), typeof(EmployeeType), typeof(Product), typeof(ProductType) };

                Func<object[], SalesDetail> map = objects =>
                {
                    var saleDetails = (SalesDetail)objects[0];
                    saleDetails.Sales = (Sales)objects[1];
                    saleDetails.Sales.Customer = (Customer)objects[2];
                    saleDetails.Sales.Customer.CustomerType = (CustomerType)objects[3];
                    saleDetails.Sales.Employee = (Employee)objects[4];
                    saleDetails.Sales.Employee.EmployeeType = (EmployeeType)objects[5];
                    saleDetails.Product = (Product)objects[6];
                    saleDetails.Product.ProductType = (ProductType)objects[7];

                    return saleDetails;
                };

                var salesDetail = await connection.QueryAsync(
                  sql, types, map,                  
                  splitOn: "Id"
                  );

                return salesDetail.ToList();
            }
        }





    }
}
