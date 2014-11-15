namespace AdList.Web.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AdList.Data.Models;
    using AdList.Web.Infrastructure.Mapping;
    using AdList.Web.ViewModels.Home;

    public class UserProfileViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string ImageUrl { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual IQueryable<HomeAdViewModel> Ads { get; set; }
    }
}
