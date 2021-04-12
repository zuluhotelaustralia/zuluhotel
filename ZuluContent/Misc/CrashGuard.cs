using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using Server.Accounting;
using Server.Network;

namespace Server.Misc
{
    public class CrashGuard
    {
        private static bool Enabled = true;
        
        public static void Initialize()
        {
            if (Enabled)
                EventSink.ServerCrashed += OnServerCrashed;
        }

        private static void OnServerCrashed(ServerCrashedEventArgs e)
        {
            #if !DEBUG
            e.Close = true;
            #endif
        }
    }
}