using AdList.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdList.Web.ViewModels.Home
{
    public class HomeViewModel
    {
        public IQueryable<HomeAdViewModel> Ads { get; set; }

        public IQueryable<Category> Categories { get; set; }
    }
}