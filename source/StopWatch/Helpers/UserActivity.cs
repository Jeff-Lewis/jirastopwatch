using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StopWatch.Helpers {


    /// <summary>
    /// Helps to find the idle time, (in milliseconds) spent since the last user input
    /// http://stackoverflow.com/questions/1037595/c-sharp-detect-time-of-last-user-interaction-with-the-os
    /// </summary>
    internal class UserActivity {        

        public static uint GetIdleTime() {
            LASTINPUTINFO lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)Marshal.SizeOf(lastInPut);
            NativeMethods.GetLastInputInfo(ref lastInPut);

            return ((uint)Environment.TickCount - lastInPut.dwTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static long GetLastInputTime() {
            LASTINPUTINFO lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)Marshal.SizeOf(lastInPut);
            if (!NativeMethods.GetLastInputInfo(ref lastInPut)) {
                throw new Exception(NativeMethods.GetLastError().ToString());
            }
            return lastInPut.dwTime;
        }

        public static InactivityTimeout[] InactivityTimeouts
        {
            get
            {
                return new[] {
                    new InactivityTimeout() { Name="Never", Seconds=0 },
                    new InactivityTimeout() { Name="1 Minute", Seconds=60 },
                    new InactivityTimeout() { Name="2 Minutes", Seconds=120 },
                    new InactivityTimeout() { Name="3 Minutes", Seconds=180 },
                    new InactivityTimeout() { Name="5 Minutes", Seconds=300 },
                    new InactivityTimeout() { Name="10 Minutes", Seconds=600 },
                    new InactivityTimeout() { Name="15 Minutes", Seconds=900 },
                    new InactivityTimeout() { Name="20 Minutes", Seconds=1200 },
                    new InactivityTimeout() { Name="30 Minutes", Seconds=1800 },
                    new InactivityTimeout() { Name="45 Minutes", Seconds=2700 },
                    new InactivityTimeout() { Name="1 Hour", Seconds=3600 },
                    new InactivityTimeout() { Name="2 Hours", Seconds=7200 },
                    new InactivityTimeout() { Name="4 Hours", Seconds=15400 }
                };
            }
        }


    }

    internal class InactivityTimeout {
        public string Name { get; set; }
        public int Seconds { get; set; }
    }


}
