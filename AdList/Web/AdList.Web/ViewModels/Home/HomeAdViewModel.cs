//namespace AdList.Web.ViewModels.Home
//{
//    using AdList.Data.Models;
//    using AdList.Web.Infrastructure.Mapping;
//    using System.Collections.Generic;
//    using System.ComponentModel.DataAnnotations;

//    public class HomeAdViewModel : IMapFrom<Ad>
//    {
//        public int Id { get; set; }

//        public string Title { get; set; }

//        public string Description { get; set; }

//        public string ImageUrl { get; set; }

//        [DisplayFormat(DataFormatString = "{0:C}")]
//        public decimal Price { get; set; }

//        public IEnumerable<Category> CategoryOptions { get; set; }
//    }
//}