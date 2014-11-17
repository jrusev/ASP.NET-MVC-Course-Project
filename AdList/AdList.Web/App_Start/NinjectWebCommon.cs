[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(AdList.Web.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(AdList.Web.NinjectWebCommon), "Stop")]

namespace AdList.Web
{
    using System;
    using System.Data.Entity;
    using System.Web;

    using AdList.Data;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using AdList.Data.Models;
    using AdList.Web.Infrastructure;
    using AdList.Data.UnitOfWork;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<DbContext>().To<ApplicationDbContext>();

            kernel.Bind(typeof(IRepository<Ad>)).To(typeof(Repository<Ad>));
            kernel.Bind(typeof(IRepository<Category>)).To(typeof(Repository<Category>));

            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));

            kernel.Bind<ISanitizer>().To<HtmlSanitizerAdapter>();

            kernel.Bind<IDataProvider>()
                    .To<DataProvider>()
                    .WithConstructorArgument("context", c => new ApplicationDbContext());
        }
    }
}
