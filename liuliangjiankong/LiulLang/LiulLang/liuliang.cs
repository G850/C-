using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LiulLang
{
    static class liuliang
    {
        private static PerformanceCounterCategory performanceCounterCategory = new PerformanceCounterCategory("Network Interface");
        private static string instance = performanceCounterCategory.GetInstanceNames()[5];
        private static int interfaceLength = instance.Length;
        private static PerformanceCounter performanceCounterRecv = new PerformanceCounter("Network Interface", "Bytes Received/sec", instance);
        private static PerformanceCounter performanceCounterSend = new PerformanceCounter("Network Interface", "Bytes Sent/sec", instance);

        public static float GetNetRecv()
        {
            return performanceCounterRecv.NextValue();
        }

        public static float GetNetSend()
        {
            return performanceCounterSend.NextValue();
        }
    }
}
