using DapperRelations.Infrastructure.Models;


namespace DapperRelations.Domain.Models
{
    public class Customer : BaseAuditableEntity
    {
        public required string CustomerName { get; set; }        

        public string CustomerAddress { get; set; }

        public required CustomerType CustomerType { get; set; }

        public required int OrderNo { get; set; }
        
    }

}
