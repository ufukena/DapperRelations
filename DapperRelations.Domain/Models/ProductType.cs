using DapperRelations.Infrastructure.Models;


namespace DapperRelations.Domain.Models
{
    public class ProductType : BaseAuditableEntity
    {
        public required string ProductTypeName { get; set; }        
        public required int OrderNo { get; set; }

    }

}
