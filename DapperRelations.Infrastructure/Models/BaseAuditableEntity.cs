using System;


namespace DapperRelations.Infrastructure.Models
{

    public abstract class BaseAuditableEntity : BaseEntity
    {
        
        public required DateTime? CreatedDate { get; set; }
        public required int CreatedId { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedId { get; set; }

        public DateTime? DeletedDate { get; set; }
        public int? DeletedId { get; set; }

        public bool? DeletedFlg { get; set; }
        

    }
}
