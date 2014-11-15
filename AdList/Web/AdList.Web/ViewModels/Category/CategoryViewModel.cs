namespace AdList.Web.ViewModels.Category
{
    using AdList.Web.ViewModels.Home;
    using System.Linq;
    using AdList.Data.Models;

    public class CategoryViewModel
    {
        public string CategoryName { get; set; }

        public IQueryable<HomeAdViewModel> Ads { get; set; }

        public IQueryable<Category> Categories { get; set; }        
    }
}