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

namespace BP_UniqueCodeGen
{
    public class WorkerRole : RoleEntryPoint
    {
        private Repository repo = new Repository();

        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("WorkerRole1 entry point called", "Information");

            while (true)
            {
                //sleep for 2 hours BUGBUG
                //TO DEBUG THIS SERVICE, Comment this line.
                //Thread.Sleep(1000 * 60 * 60 * 2);
                //TO DEBUG THIS SERVICE, Uncomment this line.
                Thread.Sleep(1000 * 60 * 2);
                Trace.WriteLine("Working", "Information");

                //get a list of all restaurants
                IList<Restaurant> restaurants = this.repo.GetRestaurants();

                //for each restaurant check to see if new codes have to be generated
                foreach (Restaurant r in restaurants)
                {
                    //this is the number of codes that need to generated
                    int numberOfCodes;

                    bool isCreateNewCodes = repo.IsGenerateUniqueCodes(r.ID, out numberOfCodes);

                    if (isCreateNewCodes)
                    {
                        UniqueKeyGenerator ucGen = new UniqueKeyGenerator();
                        string[] uniqueCodes = ucGen.GenerateUniqueKeys(numberOfCodes);

                        repo.UniqueCode_BulkInsert(uniqueCodes, r.ID);
                    }
                }
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
