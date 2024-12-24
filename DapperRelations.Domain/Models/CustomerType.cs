using DapperRelations.Infrastructure.Models;


namespace DapperRelations.Domain.Models
{
    public class CustomerType : BaseAuditableEntity
    {
        
        public required string CustomerTypeName { get; set; } = string.Empty;

        public string CustomerTypeDescription { get; set; } = string.Empty;
        
        public required int OrderNo { get; set; }


    }
}
