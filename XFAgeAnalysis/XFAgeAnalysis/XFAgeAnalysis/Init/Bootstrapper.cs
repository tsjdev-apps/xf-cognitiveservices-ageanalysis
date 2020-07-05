using CommonServiceLocator;
using Unity;
using Unity.Lifetime;
using Unity.ServiceLocation;
using XFAgeAnalysis.Interfaces;
using XFAgeAnalysis.Services;
using XFAgeAnalysis.ViewModels;

namespace XFAgeAnalysis.Init
{
    public static class Bootstrapper
    {
        public static void RegisterDependencies()
        {
            var container = new UnityContainer();

            // services
            container.RegisterType<IDataService, DataService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPhotoService, PhotoService>(new ContainerControlledLifetimeManager());

            // viewmodels
            container.RegisterType<AgeAnalyzeViewModel>(new ContainerControlledLifetimeManager());

            var locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}
