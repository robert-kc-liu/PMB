using System;
using System.Timers;
using Autofac;
using Humanizer;
using PerfMonBeep.MonitorStrategies;
using PerfMonBeep.Settings;

namespace PerfMonBeep
{
    class PerformanceMonitorBeeper
    {
        private readonly PerformanceMonitor _pm;
        private readonly int _cpuThreshold;
        private readonly int _memoryThreshold;
        private readonly Timer _timer;
        private IMonitorStrategy _monitorStrategy;

        public PerformanceMonitorBeeper(PerformanceMonitor pm, MonitorMode monitorMode, MeasureMode measureMode, int cpuThreshold, int memoryThreshold)
        {
            _pm = pm;
            _cpuThreshold = cpuThreshold;
            _memoryThreshold = memoryThreshold;

            _timer = new Timer(1000) {AutoReset = false};
            _timer.Elapsed += (sender, eventArgs) => OnTimer();

            InitializeMonitorStrategy(monitorMode, measureMode);
        }

        private void InitializeMonitorStrategy(MonitorMode monitorMode, MeasureMode measureMode)
        {
            if (monitorMode == MonitorMode.Instant)
            {
                _monitorStrategy = Program.Container.Resolve<InstantMonitorStrategy>(new TypedParameter(typeof(MeasureMode), measureMode));
            }
            else
            {
                _monitorStrategy = Program.Container.Resolve<SustainedMonitorStrategy>(new TypedParameter(typeof(MeasureMode), measureMode));
            }

            Console.WriteLine("Set monitor mode to: {0}", monitorMode.Humanize());
            Console.WriteLine("Set measure mode to: {0}", measureMode.Humanize());
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        private void OnTimer()
        {
            _monitorStrategy.Check(_pm, _cpuThreshold, _memoryThreshold);
            _timer.Start();
        }
    }
}
