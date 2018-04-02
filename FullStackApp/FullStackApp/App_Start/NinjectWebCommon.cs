using FullStackApp.Persistance;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using System;
using System.Web;
using WebApiAndMvc5;
using System.Collections.Generic;

[assembly: PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
//[assembly: ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace WebApiAndMvc5
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
           
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Ninject.Web.WebApi.NinjectDependencyResolver(kernel);
            RegisterServices(kernel);
            return kernel;
        }
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<UnitOfWork>().ToMethod(ctx => new UnitOfWork(new FullStackApp.Models.ApplicationDbContext()));
            //kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}