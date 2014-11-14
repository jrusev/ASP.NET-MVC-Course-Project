namespace AdList.Web.ViewModels.Home
{
    using AdList.Data.Models;
    using AdList.Web.Infrastructure.Mapping;

    public class HomeAdViewModel : IMapFrom<Ad>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }
    }
}