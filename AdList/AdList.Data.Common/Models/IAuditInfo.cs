namespace AdList.Data.Common.Models
{
    using System;

    public interface IAuditInfo
    {
        DateTime CreatedOn { get; set; }

        bool PreserveCreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}
