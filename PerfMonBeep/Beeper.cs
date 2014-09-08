using System;

namespace PerfMonBeep
{
    static class Beeper 
    {
        public static void HighCpuBeep()
        {
            Console.Beep(800, 200);
        }

        public static void HighMemoryBeep()
        {
            Console.Beep(800, 1200);
        }
    }
}