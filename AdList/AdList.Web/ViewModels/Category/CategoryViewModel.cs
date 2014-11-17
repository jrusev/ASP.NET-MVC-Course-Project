namespace AdList.Web.ViewModels.Category
{
    using System.Linq;
    using AdList.Data.Models;
    using AdList.Web.ViewModels.Ads;
    using PagedList;

    public class CategoryViewModel
    {
        public string CategoryName { get; set; }

        public IPagedList<AdDetailViewModel> Ads { get; set; }

        public IQueryable<Category> Categories { get; set; }        
    }
}