using DapperRelations.Infrastructure.Models;


namespace DapperRelations.Domain.Models
{
    public class Sales : BaseAuditableEntity
    {
        
        public required DateTime SalesDate { get; set; }

        public required Customer Customer { get; set; }

        public required Employee Employee { get; set; }
        
        public required int OrderNo { get; set; }


        public List<SalesDetail> SalesDetails { get; set; } = new();


    }
}
