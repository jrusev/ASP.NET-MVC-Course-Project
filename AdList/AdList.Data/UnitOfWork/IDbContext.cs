namespace AdList.Data.UnitOfWork
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using AdList.Data.Models;

    public interface IDbContext
    {
        IDbSet<Ad> Ads { get; set; }

        IDbSet<Category> Categories { get; set; }

        int SaveChanges();

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
