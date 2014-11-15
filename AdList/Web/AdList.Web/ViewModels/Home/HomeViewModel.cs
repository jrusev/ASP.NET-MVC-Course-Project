namespace AdList.Web.ViewModels.Home
{
    using AdList.Data.Models;
    using System.Linq;

    public class HomeViewModel
    {
        public IQueryable<HomeAdViewModel> Ads { get; set; }

        public IQueryable<Category> Categories { get; set; }
    }
}