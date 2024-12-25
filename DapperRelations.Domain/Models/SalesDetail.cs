using DapperRelations.Infrastructure.Models;


namespace DapperRelations.Domain.Models
{
    public class SalesDetail : BaseAuditableEntity
    {
        public required Sales Sales { get; set; }
        public required Product Product { get; set; }
        public required int UnitCount { get; set; }
        public required decimal UnitPrice { get; set; }                
        public required int OrderNo { get; set; }

    }

}

