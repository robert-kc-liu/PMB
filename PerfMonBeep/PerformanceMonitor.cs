using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualBasic.Devices;

namespace PerfMonBeep
{
    class PerformanceMonitor
    {
        private readonly Lazy<PerformanceCounter> _cpuCounter = new Lazy<PerformanceCounter>
            (
                () => new PerformanceCounter("Processor", "% Processor Time", "_Total")
            );

        private readonly Lazy<ComputerInfo> _computerInfo = new Lazy<ComputerInfo>();

        public float GetCurrentCpuUsage()
        {
            _cpuCounter.Value.NextValue();
            Thread.Sleep(1000);

            return _cpuCounter.Value.NextValue();
        }

        public float GetCurrentMemoryUsage()
        {
            return (float) (_computerInfo.Value.TotalPhysicalMemory - _computerInfo.Value.AvailablePhysicalMemory)
                           / _computerInfo.Value.TotalPhysicalMemory * 100;
        }
    }
}