using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.IO;
using eMunching_Loyalty_DataManager;
using System.Collections;
using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace KeyGenApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repo = new Repository();

            while (true)
            {
                ShowMenu();

                int n = ReadUserChoice();

                switch (n)
                {
                    case 1:
                        int restId = GetRestaurantId();
                        string[] uniqueCodes = repo.GetUnusedUniqueCodes(restId, 7000);
                        File.WriteAllLines(restId.ToString() + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + ".txt", uniqueCodes);
                        repo.MarkUniqueCodeAsAssigned(uniqueCodes);
                        break;
                    case 2:
                        InsertUniqueCodesFromFile();
                        break;
                    case 3:
                        Environment.Exit(1);
                        break;
                    default:
                        continue;
                }
            }
            //read the database and dump N unused Unique Codes into a text file.
        }

        /// <summary>
        /// Given an input xls file of UniqueCodes, we enter that into the database table Generated_UniqueCode
        /// </summary>
        private static void InsertUniqueCodesFromFile()
        {
            string inputFilePath = Configuration.Default.InputFilePath;
            string completeFilepath = inputFilePath + Configuration.Default.FileName;

            //var directory = new DirectoryInfo(completeFilePath);
            //var fileName = (from f in directory.GetFiles() orderby f.LastWriteTime descending select f).First();
            //string fileExtension = Path.GetExtension(filepath);

            DataTable uniqueCodeTable = new DataTable();
            uniqueCodeTable.Columns.Add("UniqueCode", typeof(string));
            uniqueCodeTable.Columns.Add("DateCreated", typeof(DateTime));
            uniqueCodeTable.Columns.Add("RestaurantId", typeof(int));
            uniqueCodeTable.Columns.Add("IsValidated", typeof(bool));
            uniqueCodeTable.Columns.Add("DateValidated", typeof(DateTime));
            uniqueCodeTable.Columns.Add("IsAssigned", typeof(bool));

            HSSFWorkbook hssfwb;
            using (FileStream file = new FileStream(@completeFilepath, FileMode.Open, FileAccess.Read))
            {
                hssfwb = new HSSFWorkbook(file);
            }

            try
            {
                ISheet isheet = hssfwb.GetSheetAt(0);
                for (int row = 0; row <= isheet.LastRowNum; row++)
                {
                    if (isheet.GetRow(row) != null) //null is when the row only contains empty cells 
                    {
                        //dynamic value = isheet.GetRow(row).GetCell(1).StringCellValue;
                        string uniqueCode = isheet.GetRow(row).GetCell(0).StringCellValue;
                        string today = DateTime.Now.ToString();
                        int restaurantId = 67;
                        bool isValidated = false;
                        bool isAssigned = true;
                        uniqueCodeTable.Rows.Add(uniqueCode, today, restaurantId, isValidated, null, isAssigned);
                    }
                }
                DataSet dataSet = new DataSet();
                dataSet.Tables.Add(uniqueCodeTable);
                SqlConnection connection = new SqlConnection("Data Source=ijgz12f2wq.database.windows.net;Initial Catalog=eMunching_Loyalty;Persist Security Info=True;User ID=anandvv;Password=t8pD6FXb;MultipleActiveResultSets=True;Application Name=EntityFramework");
                System.Data.SqlClient.SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection);
                sqlBulkCopy.DestinationTableName = "dbo.Generated_UniqueCode";
                connection.Open();
                sqlBulkCopy.WriteToServer(dataSet.Tables[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Error Occurred!");
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Gets the Restaurant Id from the Console
        /// </summary>
        /// <returns></returns>
        private static int GetRestaurantId()
        {
            Console.Clear();
            Console.WriteLine("Enter the Restaurant Id");
            int restId = int.Parse(Console.ReadLine());
            Console.Clear();
            return restId;
        }

        /// <summary>
        /// Gets the user's input for the menu presented to them
        /// </summary>
        /// <returns>integer value selected by the user</returns>
        private static int ReadUserChoice()
        {
            string userInput = Console.ReadLine();
            try
            {
                int userChoice = int.Parse(userInput);
                return userChoice;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR OCCURRED WHILE READING USER OPTION");
                Console.WriteLine(ex.Message);

                return -1;
            }
        }

        /// <summary>
        /// Shows a list of options to the user
        /// </summary>
        private static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Please choose on the following options:");
            Console.WriteLine("\n");
            Console.WriteLine("1. Dump Unique Keys to File");
            Console.WriteLine("2. Insert Unique Keys from File to Database");
            Console.WriteLine("3. Exit");
        }
    }
}
