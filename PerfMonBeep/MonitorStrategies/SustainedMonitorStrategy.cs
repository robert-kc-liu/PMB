using System;
using PerfMonBeep.Settings;

namespace PerfMonBeep.MonitorStrategies
{
    class SustainedMonitorStrategy : IMonitorStrategy
    {
        private readonly MeasureMode _measureMode;

        private int _highCpuDuration;
        private int _highMemoryDuration;

        private const int HighDurationCount = 60;

        public SustainedMonitorStrategy(MeasureMode measureMode)
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
                    _highCpuDuration++;
                }
                else
                {
                    _highCpuDuration = 0;
                }
            }

            if (_measureMode == MeasureMode.HighCpu || _measureMode == MeasureMode.Both)
            {
                var memoryUsage = pm.GetCurrentMemoryUsage();
                Console.WriteLine("Memory usage: {0}%", memoryUsage);

                if (memoryUsage >= memoryThreshold)
                {
                    _highMemoryDuration++;
                }
                else
                {
                    _highMemoryDuration = 0;
                }
            }

            BeepOnHighUsage();
        }

        private void BeepOnHighUsage()
        {
            if (_highCpuDuration >= HighDurationCount)
            {
                Beeper.HighCpuBeep();
            }

            if (_highMemoryDuration >= HighDurationCount)
            {
                Beeper.HighMemoryBeep();
            }
        }
    }
}