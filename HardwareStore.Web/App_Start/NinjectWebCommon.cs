﻿[assembly: WebActivator.PreApplicationStartMethod(typeof(HardwareStore.Web.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethod(typeof(HardwareStore.Web.NinjectWebCommon), "Stop")]

namespace HardwareStore.Web
{
    using Data;
    using Data.IdentityModels;
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Services;
    using Services.Implementations;
    using System;
    using System.Data.Entity;
    using System.Reflection;
    using System.Web;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
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
            kernel.Load(Assembly.GetExecutingAssembly());

            kernel.Bind<DbContext>().ToConstructor(_ => new HardwareStoreDbContext());

            kernel.Bind<HardwareStoreDbContext>().ToSelf().InRequestScope();
            kernel.Bind<IUserStore<User, int>>().To<UserStore<User, Role, int, UserLogin, UserRole, UserClaim>>();
            kernel.Bind<ApplicationUserManager>().ToSelf();
            kernel.Bind<ApplicationSignInManager>().ToSelf();
            kernel.Bind<IAuthenticationManager>().ToMethod(x => HttpContext.Current.GetOwinContext().Authentication);

            kernel.AddDomainServices();
            kernel.Bind<IShoppingCartManager>().To<CartManager>().InSingletonScope();
        }
    }
}