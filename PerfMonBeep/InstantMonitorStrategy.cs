using System;

namespace PerfMonBeep
{
    class InstantMonitorStrategy : IMonitorStrategy
    {
        public void Check(PerformanceMonitor pm, int cpuThreshold, int memoryThreshold)
        {
            var cpuUsage = pm.GetCurrentCpuUsage();
            Console.WriteLine("CPU usage: {0}%", cpuUsage);

            if (cpuUsage >= cpuThreshold)
            {
                Beeper.HighCpuBeep();
            }

            var memoryUsage = pm.GetCurrentMemoryUsage();
            Console.WriteLine("Memory usage: {0}%", memoryUsage);

            if (memoryUsage >= memoryThreshold)
            {
                Beeper.HighMemoryBeep();
            }
        }
    }
}