using AdList.Data.Models;
using AdList.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdList.Web.ViewModels.Ads
{
    public class AdDetailViewModel : IMapFrom<Ad>
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }
}