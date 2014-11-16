namespace AdList.Web.ViewModels.Category
{
    using System.Linq;
    using AdList.Data.Models;
    using AdList.Web.ViewModels.Ads;

    public class CategoryViewModel
    {
        public string CategoryName { get; set; }

        public IQueryable<AdDetailViewModel> Ads { get; set; }

        public IQueryable<Category> Categories { get; set; }        
    }
}