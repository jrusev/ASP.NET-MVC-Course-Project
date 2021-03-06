﻿using AdList.Data.Models;
using AdList.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace AdList.Web.InputModels.Ads
{
    public class AdInputModel : IMapFrom<Ad>
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<Category> CategoryOptions { get; set; }
    }
}
