namespace PerfMonBeep
{
    interface IMonitorStrategy
    {
        void Check(PerformanceMonitor pm, int cpuThreshold, int memoryThreshold);
    }
}