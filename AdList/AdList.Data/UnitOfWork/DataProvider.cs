namespace AdList.Data.UnitOfWork
{   
    using System;
    using System.Collections.Generic;

    using AdList.Data.Models;
    using AdList.Data.Models.Base;

    public class DataProvider : IDataProvider
    {
        private IDbContext databaseContext;
        private IDictionary<Type, object> createdRepositories;

        public DataProvider(IDbContext context)
        {
            this.databaseContext = context;
            this.createdRepositories = new Dictionary<Type, object>();
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IRepository<Ad> Ads
        {
            get { return this.GetRepository<Ad>(); }
        }

        public IRepository<Category> Categories
        {
            get { return this.GetRepository<Category>(); }
        }

        public int SaveChanges()
        {
            return this.databaseContext.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class, IAuditInfo
        {
            var typeOfRepository = typeof(T);

            if (!this.createdRepositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(Repository<T>), this.databaseContext);
                this.createdRepositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.createdRepositories[typeOfRepository];
        }
    }
}
