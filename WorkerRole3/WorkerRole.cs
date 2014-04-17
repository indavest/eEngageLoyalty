using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;
using eMunching_Loyalty_DataManager;

namespace BP_SettlementDataProcessor
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            Repository repo = new Repository();

            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("BP_SettlementDataProcessor entry point called", "Information");

            while (true)
            {
                //sleep for 2 hours BUGBUG
                //TO DEBUG THIS SERVICE, Comment this line.
                //Thread.Sleep(1000 * 60 * 60 * 2);
                //TO DEBUG THIS SERVICE, Uncomment this line.
                Thread.Sleep(1000 * 60 * 1);
                Trace.WriteLine("BP_SettlementDataProcessor is Working", "Information");

                repo.AddOwnershipToSettlementInfo();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
