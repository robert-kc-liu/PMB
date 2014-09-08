using System;
using System.Timers;
using Humanizer;

namespace PerfMonBeep
{
    /// <summary>
    /// - Mode = Sustained high CPU (e.g. over 80% for 1 minute) or every 1 second check (instant)
    /// - Measure = High CPU, Memory or both [Humanize enum]
    /// </summary>
    class PerformanceMonitorBeeper
    {
        private readonly PerformanceMonitor _pm;
        private readonly int _cpuThreshold;
        private readonly int _memoryThreshold;
        private readonly Timer _timer;
        private IMonitorStrategy _monitorStrategy;

        public PerformanceMonitorBeeper(PerformanceMonitor pm, MonitorMode monitorMode, int cpuThreshold, int memoryThreshold)
        {
            _pm = pm;
            _cpuThreshold = cpuThreshold;
            _memoryThreshold = memoryThreshold;

            _timer = new Timer(1000) {AutoReset = false};
            _timer.Elapsed += (sender, eventArgs) => OnTimer();

            InitializeMonitorStrategy(monitorMode);
        }

        private void InitializeMonitorStrategy(MonitorMode monitorMode)
        {
            if (monitorMode == MonitorMode.Instant)
            {
                _monitorStrategy = new InstantMonitorStrategy();
            }
            else
            {
                _monitorStrategy = new SustainedMonitorStrategy();
            }

            Console.WriteLine("Set monitor mode to: {0}", monitorMode.Humanize());
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
