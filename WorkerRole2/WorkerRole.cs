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
using KeyManager;
using eMunching_Loyalty_DataManager.eMunchingServices;
using log4net;
using log4net.Config;
using System.IO;

namespace BP_CouponCodeGen
{
    public class WorkerRole : RoleEntryPoint
    {
        private ILog _logger;
        public override void Run()
        {
            Repository repo = new Repository();

            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("BP_CouponCodeGen entry point called", "Information");
            XmlConfigurator.Configure();

            _logger = LogManager.GetLogger(GetType());

            while (true)
            {
#if DEBUG
                //sleep for 2 mins DEBUG
                Thread.Sleep(1000 * 60 * 2);
                
#else
                //sleep for 2 hours
                Thread.Sleep(1000 * 60 * 60 * 2);
#endif
                try
                {
                    Trace.WriteLine("[Background Process]: Running Settlement data processor", "Information");
                    repo.AddOwnershipToSettlementInfo();

                    Trace.WriteLine("[Background Process]: Running coupon code generator", "Information");
                    repo.GenerateCouponCodes();

                    Trace.WriteLine("[Background Process]: Running Unique Code Generator", "Information");
                    repo.UniqueCodeGenerator();
                }
                catch (Exception ex)
                {
                    Trace.TraceInformation("Something went wrong");
                    Trace.TraceError(ex.ToString());
                    _logger.Info(ex.Message + ex.ToString() + ex.InnerException);
                }
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            //logging into diagnistics
            var logFilePath = Path.Combine(Environment.CurrentDirectory, "logs");

            var logDir = new DirectoryConfiguration
            {
                Container = "wad-log4net",
                DirectoryQuotaInMB = 100,
                Path = logFilePath
            };

            var diagnostics = DiagnosticMonitor.GetDefaultInitialConfiguration();
            diagnostics.Directories.ScheduledTransferPeriod = TimeSpan.FromMinutes(1);
            diagnostics.Directories.DataSources.Add(logDir);

            CloudStorageAccount account = CloudStorageAccount.Parse(
                RoleEnvironment.GetConfigurationSettingValue(
                    "Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString"));

            DiagnosticMonitor.Start(account, diagnostics);


            return base.OnStart();
        }
    }
}
