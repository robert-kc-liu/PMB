using Autofac;
using Autofac.Configuration;
using Autofac.Core;
using Topshelf;

namespace PerfMonBeep
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupIoc();
            SetupWindowsService();
        }

        private static IContainer Container { get; set; }

        private static void SetupIoc()
        {
            var builder = new ContainerBuilder();

            //builder.RegisterType<PerformanceMonitor>().AsSelf();
            //builder.RegisterType<PerformanceMonitorBeeper>().AsSelf();
            builder.RegisterModule(new ConfigurationSettingsReader("autofac"));

            Container = builder.Build();
        }

        private static void SetupWindowsService()
        {
            HostFactory.Run(x =>
            {
                x.Service<PerformanceMonitorBeeper>(s =>
                {
                    s.ConstructUsing(() => Container.Resolve<PerformanceMonitorBeeper>());
                    s.WhenStarted(pmb => pmb.Start());
                    s.WhenStopped(pmb => pmb.Stop());
                });

                x.RunAsLocalSystem();

                x.SetServiceName("PerformanceMonitorBeeper");
            });
        }
    }
}