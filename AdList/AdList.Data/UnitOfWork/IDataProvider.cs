namespace AdList.Data.UnitOfWork
{
    using AdList.Data.Models;

    public interface IDataProvider
    {
        IRepository<User> Users { get; }

        IRepository<Ad> Ads { get; }

        IRepository<Category> Categories { get; }

        int SaveChanges();
    }
}
