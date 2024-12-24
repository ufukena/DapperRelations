using System;


namespace DapperRelations.Infrastructure.Models
{

    public abstract class BaseAuditableEntity : BaseEntity
    {
        
        public required DateTime? CreatedDate { get; set; }
        public required User CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public User? ModifiedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
        public User? DeletedBy { get; set; }

        public bool? DeletedFlg { get; set; }
        

    }
}
