using System;
using PerfMonBeep.Settings;

namespace PerfMonBeep.MonitorStrategies
{
    class InstantMonitorStrategy : IMonitorStrategy
    {
        private readonly MeasureMode _measureMode;

        public InstantMonitorStrategy(MeasureMode measureMode)
        {
            _measureMode = measureMode;
        }

        public void Check(PerformanceMonitor pm, int cpuThreshold, int memoryThreshold)
        {
            if (_measureMode == MeasureMode.HighCpu || _measureMode == MeasureMode.Both)
            {
                var cpuUsage = pm.GetCurrentCpuUsage();
                Console.WriteLine("CPU usage: {0}%", cpuUsage);

                if (cpuUsage >= cpuThreshold)
                {
                    Beeper.HighCpuBeep();
                }
            }

            if (_measureMode == MeasureMode.HighMemory || _measureMode == MeasureMode.Both)
            {
                var memoryUsage = pm.GetCurrentMemoryUsage();
                Console.WriteLine("Memory usage: {0}%", memoryUsage);

                if (memoryUsage >= memoryThreshold)
                {
                    Beeper.HighMemoryBeep();
                }
            }
        }
    }
}