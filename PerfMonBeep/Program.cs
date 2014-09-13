using Autofac;
using Autofac.Configuration;
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

        public static IContainer Container { get; set; }

        private static void SetupIoc()
        {
            var builder = new ContainerBuilder();

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