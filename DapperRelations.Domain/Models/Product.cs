using DapperRelations.Infrastructure.Models;


namespace DapperRelations.Domain.Models
{
    public class Product : BaseAuditableEntity
    {
        
        public required string ProductName { get; set; }
        public required ProductType ProductType { get; set; }
        public decimal UnitPrice { get; set; }
        public string Color { get; set; } = string.Empty;
        public required int OrderNo { get; set; }

    }
}
