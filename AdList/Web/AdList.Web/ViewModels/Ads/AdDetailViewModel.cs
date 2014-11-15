namespace AdList.Web.ViewModels.Ads
{
    using System;
    using AdList.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using AdList.Web.ViewModels.User;
    using AdList.Web.Infrastructure.Mapping;

    public class AdDetailViewModel : IMapFrom<Ad>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM dd, yyy}")]
        public DateTime CreatedOn { get; set; }

        public virtual User Author { get; set; }

        public virtual Category Category { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public bool Featured { get; set; }
    }
}