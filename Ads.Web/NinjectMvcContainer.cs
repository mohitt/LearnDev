using Ads.Model.EFRepositories;
using Ads.Model.Entities;
using Ads.Model.Repositories;
using Ads.Services;
using System.Reflection;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Mvc;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Ads.Web.NinjectMvcContainer), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Ads.Web.NinjectMvcContainer), "Stop")]

namespace Ads.Web
{
    public static class NinjectMvcContainer
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestModule));
            DynamicModuleUtility.RegisterModule(typeof(HttpApplicationInitializationModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop() {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel() {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            ServiceBindings(kernel);
            RepositoryBindings(kernel);
        }

        private static void ServiceBindings(IKernel kernel)
        {
            kernel.Bind<IFormsAuthenticationService>().To<FormsAuthenticationService>().InRequestScope();
            kernel.Bind<IMembershipService>().To<AccountMembershipService>();
            kernel.Bind<IOpenIdService>().To<OpenIdService>();
            kernel.Bind<IAdProcessorService>().To<AdProcessorService>();
            
        }

        private static void RepositoryBindings(IKernel kernel)
        {
            kernel.Bind<IUserRepository>().To<UserEFRepository>();
            kernel.Bind<ICategoryRepository>().To<CategoryEFRepository>();
            kernel.Bind<IAdRepository>().To<AdEFRepository>();
        }
    }
}
