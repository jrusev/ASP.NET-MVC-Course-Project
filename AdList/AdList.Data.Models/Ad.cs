namespace AdList.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AdList.Data.Models.Base;

    public class Ad : AuditInfo
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }
 
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set;  }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public bool Featured { get; set; }
    }
}
