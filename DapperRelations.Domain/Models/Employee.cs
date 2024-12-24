using DapperRelations.Infrastructure.Models;


namespace DapperRelations.Domain.Models
{
    public class Employee : BaseAuditableEntity
    {
        public required string EmployeeName { get; set; }

        public required string EmployeeAddress { get; set; }

        public required EmployeeType EmployeeType { get; set; }

        public required int OrderNo { get; set; }        

    }
}
