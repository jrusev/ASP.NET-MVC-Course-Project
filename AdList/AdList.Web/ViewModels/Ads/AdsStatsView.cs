using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdList.Web.ViewModels.Ads
{
    public class AdsStatsView
    {
        [DataType(DataType.Date)]
        public DateTime? CreatedOn { get; set; }

        public int AdCount { get; set; }
    }
}