namespace AdList.Web.ViewModels.User
{
    using System;
    using System.Linq;
    using AdList.Data.Models;
    using AdList.Web.Infrastructure.Mapping;
    using AdList.Web.ViewModels.Ads;

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

        public virtual IQueryable<AdDetailViewModel> Ads { get; set; }
    }
}
