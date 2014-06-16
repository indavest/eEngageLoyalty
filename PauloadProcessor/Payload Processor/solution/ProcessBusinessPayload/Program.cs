using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using log4net.Config;
using log4net;
using System.Reflection;
using System.Diagnostics;

namespace ProcessBusinessPayload
{
    class Program
    {
        public static string payloadPath = "";
        public static string payloadArchivePath = "";
        public static string[] configFileContents;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static bool isSendMailRequired = true;
        //public static string payloadPath = "F:/apps/eMunching/eMunching-Payload-Processor/Payload/";
        //public static string payloadArchivePath = "F:/apps/eMunching/eMunching-Payload-Processor/Payload/Archive/";
        //public static string payloadPath = "C:/websites/Payloads/Dropbox/Toit/Payload/";
        //public static string payloadPath = "C:/websites/Payloads/Payload_Processor/";
        //public static Repository _repository = new Repository();

        public static StringBuilder logString = new StringBuilder();
        static void Main(string[] args)
        {
            
            DateTime startTime = DateTime.Now;
            Console.WriteLine("Start @ " + startTime.ToString());
            Console.WriteLine(Directory.GetCurrentDirectory());
            configFileContents = new FileManager().readFileLIneByLine(Directory.GetCurrentDirectory()+"\\config.txt");
            payloadPath = configFileContents[1];
            payloadArchivePath = configFileContents[2];

            XmlConfigurator.Configure();


            //uploadRestaurantPayload();
            updateBusinessPayload();
            //updateBillAmount();
            Console.WriteLine("End @ " + DateTime.Now.ToString());
            TimeSpan duration = DateTime.Parse(DateTime.Now.ToString()).Subtract(DateTime.Parse(startTime.ToString()));
            Console.WriteLine(duration.ToString());
            Console.WriteLine(duration.Ticks);
            //Console.Read();
        }

        public static bool updateBillAmount()
        {
            #region 
                //read folder for files
                //load all processed file data into summary datatable
                //loop through all datatabel reords for amtching bill numbers from back end 
                    //if not available, remove record and dump it to log
                    //if available, ignore and mentioned in log
                //Bulk copy full data to backend
            #endregion
            string timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
            payloadPath = "F:/apps/eMunching/eMunching-Payload-Processor/Payload/LiveRun2/data";

            DataTable BillAmountTable = new DataTable();
            BillAmountTable.Columns.Add("Id", typeof(int));
            BillAmountTable.Columns.Add("RestaurantId", typeof(int));
            BillAmountTable.Columns.Add("BillNumber", typeof(string));
            BillAmountTable.Columns.Add("NetAmount", typeof(float));
            BillAmountTable.Columns.Add("TaxAmount", typeof(float));
            BillAmountTable.Columns.Add("DateCreated", typeof(DateTime));
            BillAmountTable.Columns.Add("DateModified", typeof(DateTime));

            //Holds all processed data 
            DataTable BillAmountProcessed = HelperManager.CloneTable(BillAmountTable);

            DataBaseManager dataBaseManager = new DataBaseManager();
            int totalRecordsProcessed = 0;

            string currentFileName = "";

            try
            {
                //detect and read payload file
                string completeFilePath = payloadPath;
                logString.AppendLine("Payload file source: " + completeFilePath);
                var directory = new DirectoryInfo(completeFilePath);
                string[] files = Directory.GetFiles(payloadPath, "*.csv", SearchOption.AllDirectories);
                
                foreach (string filename in files){
                        try{
                            currentFileName = Path.GetFileName(filename);
                            Console.WriteLine("Reading File: " + currentFileName);
                            //iterate fileDataTable and fill local data tables
                            int pRestaurantId = 67;
                            DateTime today = DateTime.Now;

                            //read payload file into a filedatatable
                            logString.AppendLine("--------------------------------------------");
                            logString.AppendLine("Restaurant Location ID: " + pRestaurantId);
                            logString.AppendLine("Processing File: " + currentFileName);
                            FileManager fileManager = new FileManager();
                            DataTable fileDataTable = fileManager.readFromFile("/"+currentFileName);
                            logString.AppendLine("Records Count:" + fileDataTable.Rows.Count);

                            if (fileDataTable.Rows.Count > 0)
                            {
                                foreach (DataRow row in fileDataTable.Rows)
                                {
                                    BillAmountTable.Rows.Add(0, pRestaurantId, row["BillNumber"].ToString() + row["FloorCode"].ToString(), row["TotalPrice"], row["Tax"], today, today);
                                }
                              
                                //Prepare data for BillAmount data table
                                /*****************************************************************************************************/
                                //get unique bill numbers
                                DataTable distinctTable = BillAmountTable.DefaultView.ToTable(true, "BillNumber");
                                
                                //Loop through and calculate sum of total and tax and add to new table
                                int count = 0;
                                foreach (DataRow dr in distinctTable.Rows)
                                {
                                    count++;
                                    String NetAmount = BillAmountTable.Compute("sum(NetAmount)", "BillNumber ='" + dr["BillNumber"].ToString() + "'").ToString();
                                    String SumTax = BillAmountTable.Compute("sum(TaxAmount)", "BillNumber ='" + dr["BillNumber"].ToString() + "'").ToString();
                                    BillAmountProcessed.Rows.Add(0, pRestaurantId, dr["BillNumber"], NetAmount, SumTax, today, today);
                                }
                                Console.WriteLine("Currently processed Records: " + BillAmountProcessed.Rows.Count + " ("+distinctTable.Rows.Count+")");
                                //clean core table as we have processed data in a different datatable
                                totalRecordsProcessed += BillAmountTable.Rows.Count;
                                BillAmountTable.Clear();
                                //HelperManager.printDataTableToConsoleWithUserBreak(dtSummerized);
                                /*****************************************************************************************************/
                            }
                            else
                            {
                                logString.AppendLine("Got Zero Records file.");
                            }
                        }
                        catch (Exception e)
                        {
                            logString.AppendLine("Got Exception while reading " + currentFileName + ": 175");
                            logString.AppendLine("Stack Trace Message:");
                            logString.AppendLine(e.Message);
                            logString.AppendLine(e.Source);
                            logString.AppendLine(e.StackTrace);
                        }
                    }

                //Load Bill Numbers from Backend to a collection
                String[] AuthenticBillNumbers = File.ReadAllLines("F:/apps/eMunching/eMunching-Payload-Processor/Payload/LiveRun2/Authentic_BillNumbers.csv");

                DataTable CorrectBillNumebrsTable = BillAmountTable.Clone();
                DataTable WrongBillNumebrsTable = BillAmountTable.Clone();

                int Counter= 0;
                //Loop through processed data and check against authentic bill numbers
                foreach (DataRow dr in BillAmountProcessed.Rows)
                {
                    Console.WriteLine("looking for Matching Authentic Bill NUmber :" + (Counter++));
                    int testIndex = Array.IndexOf(AuthenticBillNumbers, dr["BillNumber"]);
                    if (Array.IndexOf(AuthenticBillNumbers, dr["BillNumber"]) >= 0)
                    {
                        //BillNumberExits
                        CorrectBillNumebrsTable.ImportRow(dr);
                    }
                    else
                    {
                        WrongBillNumebrsTable.ImportRow(dr);
                    }
                }

                Console.WriteLine("Total: " + totalRecordsProcessed + "Processed: "+BillAmountProcessed.Rows.Count +"; Authenticated: " + CorrectBillNumebrsTable.Rows.Count + "; Wrong BillNumbers: " + WrongBillNumebrsTable.Rows.Count);

                
                HelperManager.WriteToCsvFile(CorrectBillNumebrsTable, "F:/apps/eMunching/eMunching-Payload-Processor/Payload/Success-" + string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now) + ".csv");
                HelperManager.WriteToCsvFile(WrongBillNumebrsTable, "F:/apps/eMunching/eMunching-Payload-Processor/Payload/Error-" + string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now) + ".csv");

                //update Bill Amounts data using bulk copy
                DataSet dataSet = new DataSet();
                dataSet.Tables.Add(CorrectBillNumebrsTable.Copy());
                string[] tableNames = new string[] { "BillAmount" };
                int[] OrdersInserted = dataBaseManager.processBulkCopy(dataSet, tableNames);
                dataSet.Clear();

                return true;
            }
            catch (Exception e)
            {
                logString.AppendLine("---------------------------------------");
                logString.AppendLine("Got Exception while reading " + currentFileName + ": 196");
                logString.AppendLine("Stack Trace Message:");
                logString.AppendLine(e.Message);
                logString.AppendLine(e.Source);
                logString.AppendLine(e.StackTrace);
                Console.WriteLine(logString);
                log.Error(logString);
                MailHelper.sendEmailToEengage("tech@emunching.com", "", "Reading Payload Exception for" + currentFileName, logString.ToString(), null, "eMunching Loyalty");
                return false;
            }
        }
        
        public static bool updateBusinessPayload()
        {
            #region stpes to process
            /**
             * Step 1 : prepare Data Holders
             * Step 2 : detect and read csv file
             *  Step 2.1 : if reading 999 records done update to database and clear local data holders
             *  Step 2.2 : else update counter and countinue
             * Step 3 : Update last read file in Payload table
             */
            #endregion
            //define local data table for item table
            DataTable itemCodesDataTable = new DataTable();
            itemCodesDataTable.Columns.Add("ItemCode1", typeof(int));
            itemCodesDataTable.Columns.Add("RestaurantId", typeof(int));
            itemCodesDataTable.Columns.Add("ItemName", typeof(string));
            itemCodesDataTable.Columns.Add("LoyaltyEnabled", typeof(bool));
            itemCodesDataTable.Columns.Add("LoyaltyPoints", typeof(int));
            itemCodesDataTable.Columns.Add("LoyaltyMultiplier", typeof(int));
            itemCodesDataTable.Columns.Add("BonusPoints", typeof(int));
            itemCodesDataTable.Columns.Add("DateCreated", typeof(DateTime));
            itemCodesDataTable.Columns.Add("DateModified", typeof(DateTime));

            DataTable orderDetailsTable = new DataTable();
            orderDetailsTable.Columns.Add("Id", typeof(int));
            orderDetailsTable.Columns.Add("BillNumber", typeof(string));
            orderDetailsTable.Columns.Add("ItemCode", typeof(int));
            orderDetailsTable.Columns.Add("RestaurantId", typeof(int));
            orderDetailsTable.Columns.Add("Quantity", typeof(int));

            DataTable settlementInfoTable = new DataTable();
            settlementInfoTable.Columns.Add("BillNumber", typeof(string));
            settlementInfoTable.Columns.Add("RestaurantId", typeof(int));
            settlementInfoTable.Columns.Add("UniqueCode", typeof(string));
            settlementInfoTable.Columns.Add("EmailAddres", typeof(string));
            settlementInfoTable.Columns.Add("IsServiced", typeof(bool));
            settlementInfoTable.Columns.Add("DateCreated", typeof(DateTime));
            settlementInfoTable.Columns.Add("DateModified", typeof(DateTime));
            settlementInfoTable.Columns.Add("SettlementDate", typeof(DateTime));

            DataTable payLoadTable = new DataTable();
            payLoadTable.Columns.Add("Id", typeof(int));
            payLoadTable.Columns.Add("LastReadFile", typeof(string));
            payLoadTable.Columns.Add("ReastaurantID", typeof(int));
            payLoadTable.Columns.Add("DateCreated", typeof(DateTime));

            DataTable BillAmountTable = new DataTable();
            BillAmountTable.Columns.Add("Id", typeof(int));
            BillAmountTable.Columns.Add("RestaurantId", typeof(int));
            BillAmountTable.Columns.Add("BillNumber", typeof(string));
            BillAmountTable.Columns.Add("NetAmount", typeof(float));
            BillAmountTable.Columns.Add("TaxAmount", typeof(float));
            BillAmountTable.Columns.Add("DateCreated", typeof(DateTime));
            BillAmountTable.Columns.Add("DateModified", typeof(DateTime));

            //Holds all processed data 
            DataTable BillAmountProcessed = HelperManager.CloneTable(BillAmountTable);

            DataBaseManager dataBaseManager = new DataBaseManager();

            string currentFileName = "";

            try
            {
                //detect and read payload file
                string completeFilePath = payloadPath;
                logString.AppendLine("Payload file source: " + completeFilePath);
                var directory = new DirectoryInfo(completeFilePath);
                //fileName = (from f in directory.GetFiles() orderby f.LastWriteTime descending select f).First().ToString();
                //string[] fileNameArray = fileName.Split('_');

                //get all file names in last modified order into array
                FileInfo[] filesArray = (from f in directory.GetFiles() orderby f.LastWriteTime descending select f).ToArray();
                ArrayList filesToBeRead = new ArrayList();
                String LastFilesRead = dataBaseManager.readPayLoadTable();
                logString.AppendLine("Last File Read :" + LastFilesRead);
                logString.AppendLine("Readable Files:");
                foreach (FileInfo finfo in filesArray)
                {
                    //check if current file already read into table for double confirm data consistancey
                    if (!dataBaseManager.checkFileReadOnPayloadTable(finfo.Name))
                    {
                        filesToBeRead.Add(finfo.Name);
                        logString.AppendLine(finfo.Name);
                    }
                    else
                    {
                        break;
                    }
                }

                if (filesToBeRead.Count > 0)
                {

                    filesToBeRead.Reverse();

                    foreach (string filename in filesToBeRead)
                    {
                        try
                        {
                            currentFileName = filename;
                            //iterate fileDataTable and fill local data tables
                            int pRestaurantId = Convert.ToInt32(filename.Split('_')[3].Split('.')[0]);//0-payload;1-date;2-;3-restaurantid                        
                            DateTime today = DateTime.Now;
                            bool isServiced = false;

                            //read payload file into a filedatatable
                            logString.AppendLine("--------------------------------------------");
                            logString.AppendLine("Restaurant Location ID: " + pRestaurantId);
                            logString.AppendLine("Processing File: " + filename);
                            FileManager fileManager = new FileManager();
                            DataTable fileDataTable = fileManager.readFromFile(filename);
                            logString.AppendLine("Records Count:" + fileDataTable.Rows.Count);

                            if (fileDataTable.Rows.Count > 0)
                            {
                                foreach (DataRow row in fileDataTable.Rows)
                                {
                                    itemCodesDataTable.Rows.Add(row["ItemCode"], pRestaurantId, row["ItemName"], 1, 1, 1, 0, today, today);
                                    orderDetailsTable.Rows.Add(0, row["BillNumber"].ToString() + row["FloorCode"].ToString(), row["ItemCode"], pRestaurantId, row["Quantity"]);
                                    settlementInfoTable.Rows.Add(row["BillNumber"].ToString() + row["FloorCode"].ToString(), pRestaurantId, row["UniqueCode"], null, isServiced, today, today, row["SettlementDate"]);
                                    BillAmountTable.Rows.Add(0, pRestaurantId, row["BillNumber"].ToString() + row["FloorCode"].ToString(), row["TotalPrice"], row["Tax"], today, today);
                                }
                                //remove dulicates in settle ment info based on bill no
                                DataBaseManager.RemoveDuplicateRows(settlementInfoTable, "BillNumber");
                                //initiating database update
                                //update ItemCodes
                                int itemCodesInsered = dataBaseManager.updateDataBaseWithSP("process_updateItemCodes", itemCodesDataTable, "ItemCode1");
                                //update settlemet records
                                int settlementRecordsInserted = dataBaseManager.updateDataBaseWithSP("process_updateSettlementInfoes", settlementInfoTable, "BillNumber");
                                
                                //Prepare data for BillAmount data table
                                /*****************************************************************************************************/
                                //get unique bill numbers
                                DataTable distinctTable = BillAmountTable.DefaultView.ToTable( true, "BillNumber");

                                //Loop through and calculate sum of total and tax and add to new table
                                int count = 0;
                                foreach (DataRow dr in distinctTable.Rows)
                                {
                                    count++;
                                    String NetAmount = BillAmountTable.Compute("sum(NetAmount)", "BillNumber ='" + dr["BillNumber"].ToString()+"'").ToString();
                                    String SumTax = BillAmountTable.Compute("sum(TaxAmount)", "BillNumber ='" + dr["BillNumber"].ToString() + "'").ToString();

                                    BillAmountProcessed.Rows.Add(0,pRestaurantId, dr["BillNumber"], NetAmount, SumTax,today,today);
                                }

                                //update order details and Bill Amounts data using bulk copy
                                DataSet dataSet = new DataSet();
                                dataSet.Tables.Add(BillAmountProcessed.Copy());
                                dataSet.Tables.Add(orderDetailsTable.Copy());
                                string[] tableNames = new string[] { "BillAmount" ,"OrderDetails" };
                                int[] OrdersInserted = dataBaseManager.processBulkCopy(dataSet, tableNames);


                                //HelperManager.printDataTableToConsoleWithUserBreak(dtSummerized);
                                /*****************************************************************************************************/
                                itemCodesDataTable.Clear();
                                orderDetailsTable.Clear();
                                settlementInfoTable.Clear();
                                BillAmountTable.Clear();
                                BillAmountProcessed.Clear();
                                dataSet.Clear();
                                //logString.AppendLine("New ItemCodes Added: "+itemCodesInsered);
                                //logString.AppendLine("New Settlement Records Created: "+settlementRecordsInserted);
                                //logString.AppendLine("New Order Records loaded (approx.): "+OrdersInserted[0]);
                            }
                            else
                            {
                                logString.AppendLine("Got Zero Records file.");
                            }
                            //Update last read file in Payload table
                            //bool test = _repository.UpdateLastReadPayload(pRestaurantId, fileName);
                            string insertQuery = "INSERT INTO [Payload]([LastReadFile],[RestaurantID],[DateCreated]) VALUES('" + filename + "'," + pRestaurantId + ",getDate())";
                            int effectedRecords = dataBaseManager.processSQLInsertQuery(insertQuery);
                            bool fileMoved = fileManager.movePayloadFile(payloadPath+filename, payloadArchivePath+filename);
                        }
                        catch (Exception e)
                        {
                            logString.AppendLine("Got Exception while reading "+currentFileName + ": 175");
                            logString.AppendLine("Stack Trace Message:");
                            logString.AppendLine(e.Message);
                            logString.AppendLine(e.Source);
                            logString.AppendLine(e.StackTrace);
                        }
                        finally
                        {
                        }
                    }
                }
                else {
                    logString.AppendLine("No new files to read today.");
                }
                logString.AppendLine("---------------------------------------");
                logString.AppendLine("Files Reading Completed");
                Console.WriteLine(logString);
                log.Info(logString);
                MailHelper.sendEmailToEengage("tech@emunching.com", "", "Payload process Successful", logString.ToString(), null, "eMunching Loyalty");
                return true;
            }
            catch (Exception e)
            {
                logString.AppendLine("---------------------------------------");
                logString.AppendLine("Got Exception while reading " + currentFileName + ": 196");
                logString.AppendLine("Stack Trace Message:");
                logString.AppendLine(e.Message);
                logString.AppendLine(e.Source);
                logString.AppendLine(e.StackTrace);
                Console.WriteLine(logString);
                log.Error(logString);
                MailHelper.sendEmailToEengage("tech@emunching.com", "", "Reading Payload Exception for" + currentFileName, logString.ToString(), null, "eMunching Loyalty");
                return false;
            }
        }

    }
}