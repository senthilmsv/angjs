using Microsoft.Practices.Unity;

using HiAsgRAS.BLL;
using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.DAL.Interfaces;
using HiAsgRAS.DAL.Repositories;
using System.Web.Mvc;
using Microsoft.Practices.Unity.Mvc;

namespace HiAsgRAS
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();   
            //container.RegisterType<ICategoriesServices, CategoriesServices>();
            //container.RegisterType<IController, CategoriesController>("Categories");

            container.RegisterType<IRepositoryContext, BaseRepositoryContext>();

            #region RepositoryRegister
            container.RegisterType<IUserDetailRepository, UserDetailRepository>();
            container.RegisterType<IHiradServerRepository, HiradServerRepository>();
            container.RegisterType<IHiradWebRepository, HiradWebRepository>();
            container.RegisterType<IHiradAppRepository, HiradAppRepository>();
            container.RegisterType<ILayerInfoRepository, LayerInfoRepository>();
            container.RegisterType<IBAOInfoRepository, BAOInfoRepository>();
            container.RegisterType<IUserDetailRepository, UserDetailRepository>();
            container.RegisterType<IRenewalRepository, RenewalRepository>();
            container.RegisterType<IHiradServerLogRepository, HiradServerLogRepository>();
            container.RegisterType<IHiradWebLogRepository, HiradWebLogRepository>();
            container.RegisterType<IStatusTypeRepository, StatusTypeRepository>();
            container.RegisterType<IHiradDbMonitorRepository, HiradDbMonitorRepository>();
            container.RegisterType<IApplicationEntityRepository, ApplicationEntityRepository>();
            container.RegisterType<IHiradDbMonitorLogRepository, HiradDbMonitorLogRepository>();
            container.RegisterType<IServerDecommissionRepository, ServerDecommissionRepository>();
            container.RegisterType<IApplicationInformationRepository, ApplicationInformationRepository>();
            container.RegisterType<ISharedPathRepository, SharedPathRepository>();
            #endregion RepositoryRegister

            #region BLLRegister
            container.RegisterType<IUserDetailBLL, UserDetailBLL>();
            container.RegisterType<IHiradServerBLL, HiradServerBLL>();
            container.RegisterType<IHiradServerLogBLL, HiradServerLogBLL>();
            container.RegisterType<IHiradWebBLL, HiradWebBLL>();
            container.RegisterType<IHiradAppBLL, HiradAppBLL>();
            container.RegisterType<ILayerInfoBLL, LayerInfoBLL>();
            container.RegisterType<IBAOInfoBLL, BAOInfoBLL>();
            container.RegisterType<IUserDetailBLL, UserDetailBLL>();
            container.RegisterType<IRenewalBLL, RenewalBLL>();
            container.RegisterType<IStatusTypeBLL, StatusTypeBLL>();
            container.RegisterType<IHiradDbMonitorBLL, HiradDbMonitorBLL>();
            container.RegisterType<IApplicationEntityBLL, ApplicationEntityBLL>();
            container.RegisterType<IServerDecommissionBLL, ServerDecommissionBLL>();
            container.RegisterType<IApplicationInfomationBLL, ApplicationInfomationBLL>();
            container.RegisterType<ISharedPathBLL, SharedPathBLL>();
            #endregion BLLRegister

           

            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}