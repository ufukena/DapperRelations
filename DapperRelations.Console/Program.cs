using DapperRelations.Application.Services.Contracts;
using DapperRelations.Application.Services.Repositories;


ICustomerTypeRepository ct = new CustomerTypeRepository(null);
var customerType = await ct.Get(1);
//var customerType = await ct.GetAll();

Console.WriteLine("End");
