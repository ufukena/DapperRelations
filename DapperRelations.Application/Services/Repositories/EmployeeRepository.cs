using Dapper;
using DapperRelations.Application.Context.Contract;
using DapperRelations.Application.Context.Database;
using DapperRelations.Application.Services.Contracts;
using DapperRelations.Domain.Models;
using System.Data;


namespace DapperRelations.Application.Services.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly IDatabaseContext databaseContext;

        public EmployeeRepository(IDatabaseContext _databaseContext)
        {
            databaseContext = _databaseContext ?? new DatabaseContext();
        }



        public async Task<Employee?> Get(int id)
        {
            var sql = @"SELECT 
                        em.*,
                        et.*
                        FROM Employee em
                        JOIN EmployeeType et ON et.Id = em.EmployeeTypeId
                        WHERE em.Id = @Id AND em.DeletedFlg = 0;";


            using (IDbConnection connection = databaseContext.CreateConnection())
            {
                var parameters = new { Id = id };

                var employee = await connection.QueryAsync<Employee, EmployeeType, Employee>(
                  sql,
                  (employee, employeeType) =>
                  {
                      employee.EmployeeType = employeeType;

                      return employee;
                  },
                  param: parameters,
                  splitOn: "Id"
                  );

                return employee.FirstOrDefault();
            }
        }

        public async Task<List<Employee>> GetAll()
        {
            var sql = @"SELECT 
                        em.*,
                        et.*
                        FROM Employee em
                        JOIN EmployeeType et ON et.Id = em.EmployeeTypeId
                        WHERE em.DeletedFlg = 0;";


            using (IDbConnection connection = databaseContext.CreateConnection())
            {

                var employee = await connection.QueryAsync<Employee, EmployeeType, Employee>(
                  sql,
                  (employee, employeeType) =>
                  {
                      employee.EmployeeType = employeeType;

                      return employee;
                  },                  
                  splitOn: "Id"
                  );

                return employee.ToList();

            }
        }




    }
}
