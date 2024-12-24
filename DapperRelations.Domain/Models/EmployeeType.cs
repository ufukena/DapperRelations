using DapperRelations.Infrastructure.Models;


namespace DapperRelations.Domain.Models
{
    public class EmployeeType : BaseAuditableEntity
    {

        public required string EmployeeTypeName { get; set; }

        public required int OrderNo { get; set; }
      
    }

}
