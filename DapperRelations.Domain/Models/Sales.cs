using DapperRelations.Infrastructure.Models;


namespace DapperRelations.Domain.Models
{
    public class Sales : BaseAuditableEntity
    {
        
        public required DateTime SalesDate { get; set; }

        public required Customer Customer { get; set; }

        public required Employee Employe { get; set; }
        
        public required int OrderNo { get; set; }


    }
}
