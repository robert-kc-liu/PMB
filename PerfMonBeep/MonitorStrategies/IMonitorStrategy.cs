namespace PerfMonBeep.MonitorStrategies
{
    interface IMonitorStrategy
    {
        void Check(PerformanceMonitor pm, int cpuThreshold, int memoryThreshold);
    }
}