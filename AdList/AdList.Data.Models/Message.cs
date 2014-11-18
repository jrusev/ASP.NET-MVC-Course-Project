namespace AdList.Data.Models
{
    using AdList.Data.Models.Base;
    using System.ComponentModel.DataAnnotations;

    public class Message : AuditInfo
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public string FromId { get; set; }

        public virtual User From { get; set; }


        public string ToId { get; set; }

        public virtual User To { get; set; }

        [Required]
        public string Content { get; set; }

        public bool IsRead { get; set; }
    }
}
