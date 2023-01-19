using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace MS_Toys
{
    public class Log
    {
        private static bool Initialized = false;

        public static void Initialize()
        {
            if(Initialized)
            {
                return;
            }

            //FileStream file = new FileStream("MS-Toys.log",FileMode.Append, FileAccess.Write);
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.AutoFlush = true;

            Initialized = true;
        }
    }
}