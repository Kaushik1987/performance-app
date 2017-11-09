using Core.Domain.BaseData.Countries;
using Core.Domain.BaseData.Currencies;
using Core.Domain.Institutions;
using Core.Domain.Partners;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Repositories.BaseData;
using Core.Interfaces.Repositories.Institutions;
using Core.Interfaces.Repositories.Partners;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Repositories.BaseData;
using Infrastructure.Repositories.Business.Institution;
using Infrastructure.Repositories.Business.Partner;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Web.App_Start.NinjectWebCommon), "Stop")]

namespace Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

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
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<IPartnerContactRepository<PartnerContact>>().To<PartnerContactRepository<PartnerContact>>().InRequestScope();
            kernel.Bind<ICurrencyRepository<Currency>>().To<CurrencyRepository<Currency>>().InRequestScope();
            kernel.Bind<ICountryRepository<Country>>().To<CountryRepository<Country>>().InRequestScope();
            kernel.Bind<IPartnerRepository<Partner>>().To<PartnerRepository<Partner>>().InRequestScope();
            kernel.Bind<IPartnerRepository<AssetManager>>().To<PartnerRepository<AssetManager>>().InRequestScope();
            kernel.Bind<IInstitutionRepository<Institution>>().To<InstitutionRepository<Institution>>().InRequestScope();
            kernel.Bind<IInstitutionRepository<Bank>>().To<InstitutionRepository<Bank>>().InRequestScope();
        }

        // IMapper based dependency injection
        //private static void BindMapper(this IKernel kernel)
        //{
            //var mapperConfiguration = new MapperConfiguration(cfg =>
            //{
                //cfg.AddProfile(new MappingProfile());
            //});
            //kernel.Bind<IMapper>().ToConstant(mapperConfiguration.CreateMapper());
        //}
    }
}
