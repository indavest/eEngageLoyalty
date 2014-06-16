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
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ProcessBusinessPayload
{
    /// <summary>
    /// Class to read data from a CSV file
    /// </summary>
    public class CsvFileReader : StreamReader
    {
        public CsvFileReader(Stream stream)
            : base(stream)
        {
        }

        public CsvFileReader(string filename)
            : base(filename)
        {
        }

        /// <summary>
        /// Reads a row of data from a CSV file
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool ReadRow(CsvRow row)
        {
            row.LineText = ReadLine();
            if (String.IsNullOrEmpty(row.LineText))
                return false;

            int pos = 0;
            int rows = 0;

            while (pos < row.LineText.Length)
            {
                string value;

                // Special handling for quoted field
                if (row.LineText[pos] == '"')
                {
                    // Skip initial quote
                    pos++;

                    // Parse quoted value
                    int start = pos;
                    while (pos < row.LineText.Length)
                    {
                        // Test for quote character
                        if (row.LineText[pos] == '"')
                        {
                            // Found one
                            pos++;

                            // If two quotes together, keep one
                            // Otherwise, indicates end of value
                            if (pos >= row.LineText.Length || row.LineText[pos] != '"')
                            {
                                pos--;
                                break;
                            }
                        }
                        pos++;
                    }
                    value = row.LineText.Substring(start, pos - start);
                    value = value.Replace("\"\"", "\"");
                }
                else
                {
                    // Parse unquoted value
                    int start = pos;
                    /***************************************************************/
                    //Original line editied by prasad on 30-10-2013 
                    //To fix issue with commma in item name 
                        //which is interpreted as value seperator but 
                        //for current case ';' is the value seperator
                    //replace this line with next active code line if nessacery but it breaks if item name have a comma
                    //while (pos < row.LineText.Length && row.LineText[pos] != ',')
                    /***************************************************************/
                    while (pos < row.LineText.Length)
                        pos++;
                    value = row.LineText.Substring(start, pos - start);
                }

                // Add field to list
                if (rows < row.Count)
                    row[rows] = value;
                else
                    row.Add(value);
                rows++;

                // Eat up to and including next comma
                while (pos < row.LineText.Length && row.LineText[pos] != ',')
                    pos++;
                if (pos < row.LineText.Length)
                    pos++;
            }
            // Delete any unused items
            while (row.Count > rows)
                row.RemoveAt(rows);

            // Return true if any columns read
            return (row.Count > 0);
        }
    }

    public class CsvRow : List<string>
    {
        public string LineText { get; set; }
    }

    public class FileManager
    {
        public DataTable readFromFile(String filePath)
        {
            DataTable fileDataTable = new DataTable(), fileDataMissingTable = new DataTable();
            try
            {
                string fileExtension = Path.GetExtension(filePath);
                fileDataTable.Columns.Add("BillNumber", typeof(int));
                fileDataTable.Columns.Add("FloorCode", typeof(string));
                fileDataTable.Columns.Add("SettlementDate", typeof(DateTime));
                fileDataTable.Columns.Add("ItemCode", typeof(int));
                fileDataTable.Columns.Add("ItemName", typeof(string));
                fileDataTable.Columns.Add("Quantity", typeof(int));
                fileDataTable.Columns.Add("ItemPrice", typeof(double));
                fileDataTable.Columns.Add("TotalPrice", typeof(double));
                fileDataTable.Columns.Add("Tax", typeof(double));
                fileDataTable.Columns.Add("UniqueCode", typeof(string));

                fileDataMissingTable.Columns.Add("BillNumber", typeof(int));
                fileDataMissingTable.Columns.Add("FloorCode", typeof(string));
                fileDataMissingTable.Columns.Add("SettlementDate", typeof(DateTime));
                fileDataMissingTable.Columns.Add("ItemCode", typeof(int));
                fileDataMissingTable.Columns.Add("ItemName", typeof(string));
                fileDataMissingTable.Columns.Add("Quantity", typeof(int));
                fileDataMissingTable.Columns.Add("ItemPrice", typeof(double));
                fileDataMissingTable.Columns.Add("TotalPrice", typeof(double));
                fileDataMissingTable.Columns.Add("Tax", typeof(double));
                fileDataMissingTable.Columns.Add("UniqueCode", typeof(string));
                switch (fileExtension)
                {
                    case ".xlsx":
                        XSSFWorkbook xssfwb;
                        using (FileStream file = new FileStream(@filePath, FileMode.Open, FileAccess.Read))
                        {
                            //hssfwb = new HSSFWorkbook(file);
                            xssfwb = new XSSFWorkbook(file);
                        }

                        //ISheet sheet = hssfwb.GetSheetAt(0);
                        XSSFSheet xssfsheet = (XSSFSheet)xssfwb.GetSheetAt(0);
                        for (int row = 0; row <= xssfsheet.LastRowNum; row++)
                        {
                            if (xssfsheet.GetRow(row) != null) //null is when the row only contains empty cells 
                            {
                                string pBillNumber = xssfsheet.GetRow(row).GetCell(0).NumericCellValue.ToString();
                                string pFloorCode = xssfsheet.GetRow(row).GetCell(1).StringCellValue.ToString();
                                DateTime pBillDate = DateHelper.parseDate(xssfsheet.GetRow(row).GetCell(2).StringCellValue);
                                int pItemCode = Convert.ToInt32(xssfsheet.GetRow(row).GetCell(3).NumericCellValue.ToString());
                                String pItemName = xssfsheet.GetRow(row).GetCell(4).StringCellValue;
                                int pItemQty = Convert.ToInt32(xssfsheet.GetRow(row).GetCell(5).NumericCellValue);
                                double pItemPrice = xssfsheet.GetRow(row).GetCell(6).NumericCellValue;
                                double pItemTotalPrice = xssfsheet.GetRow(row).GetCell(7).NumericCellValue;
                                double pTax = xssfsheet.GetRow(row).GetCell(8).NumericCellValue;
                                string pUniqueCode = xssfsheet.GetRow(row).GetCell(9).StringCellValue;
                                DateTime today = DateTime.Now;

                                if (pUniqueCode != "" && pItemQty != 0 && pItemCode != 0 && (pBillNumber + pFloorCode) != null)
                                    fileDataTable.Rows.Add("1382", pFloorCode, pBillDate, 376, pItemName, 2, pItemPrice, pItemTotalPrice, pTax, pUniqueCode);
                                else
                                    fileDataMissingTable.Rows.Add(pBillNumber, pFloorCode, pBillDate, pItemCode, pItemName, pItemQty, pItemPrice, pItemTotalPrice, pTax, pUniqueCode);
                            }
                        }
                        break;
                    case ".xls":
                        HSSFWorkbook hssfwb;
                        using (FileStream file = new FileStream(@filePath, FileMode.Open, FileAccess.Read))
                        {
                            hssfwb = new HSSFWorkbook(file);
                        }

                        ISheet isheet = hssfwb.GetSheetAt(0);
                        for (int row = 0; row <= isheet.LastRowNum; row++)
                        {
                            if (isheet.GetRow(row) != null) //null is when the row only contains empty cells 
                            {
                                string pBillNumber = isheet.GetRow(row).GetCell(0).NumericCellValue.ToString();
                                string pFloorCode = isheet.GetRow(row).GetCell(1).StringCellValue.ToString();
                                DateTime pBillDate = DateHelper.parseDate(isheet.GetRow(row).GetCell(2).StringCellValue);
                                int pItemCode = Convert.ToInt32(isheet.GetRow(row).GetCell(3).NumericCellValue.ToString());
                                String pItemName = isheet.GetRow(row).GetCell(4).StringCellValue;
                                int pItemQty = Convert.ToInt32(isheet.GetRow(row).GetCell(5).NumericCellValue);
                                double pItemPrice = isheet.GetRow(row).GetCell(6).NumericCellValue;
                                double pItemTotalPrice = isheet.GetRow(row).GetCell(7).NumericCellValue;
                                double pTax = isheet.GetRow(row).GetCell(8).NumericCellValue;
                                string pUniqueCode = isheet.GetRow(row).GetCell(9).StringCellValue;
                                DateTime today = DateTime.Now;

                                if (pUniqueCode != "" && pItemQty != 0 && pItemCode != 0 && (pBillNumber + pFloorCode) != null)
                                    fileDataTable.Rows.Add(pBillNumber, pFloorCode, pBillDate, pItemCode, pItemName, pItemQty, pItemPrice, pItemTotalPrice, pTax, pUniqueCode);
                                else
                                    fileDataMissingTable.Rows.Add(pBillNumber, pFloorCode, pBillDate, pItemCode, pItemName, pItemQty, pItemPrice, pItemTotalPrice, pTax, pUniqueCode);
                            }
                        }
                        break;
                    case ".csv":
                        using (CsvFileReader reader = new CsvFileReader(Program.payloadPath + filePath))
                        {
                            CsvRow row = new CsvRow();
                            while (reader.ReadRow(row))
                            {
                                foreach (string s in row)
                                {
                                    string[] rowArray = s.Split(';');
                                    string pBillNumber = rowArray[0].ToString();
                                    string pFloorCode = rowArray[1].ToString();
                                    DateTime pBillDate = DateHelper.parseDate(rowArray[2].ToString());
                                    int pItemCode = Convert.ToInt32(rowArray[3]);
                                    String pItemName = rowArray[4].ToString();
                                    int pItemQty = Convert.ToInt32(Convert.ToDouble(rowArray[5]));
                                    double pItemPrice = Convert.ToDouble(rowArray[6]);
                                    double pItemTotalPrice = Convert.ToDouble(rowArray[7]);
                                    double pTax = Convert.ToDouble(rowArray[8]);
                                    string pUniqueCode = rowArray[9].ToString();
                                    DateTime today = DateTime.Now;
                                    if (pUniqueCode != "" && pItemQty != 0 && pItemCode != 0 && (pBillNumber + pFloorCode) != null)
                                        fileDataTable.Rows.Add(pBillNumber, pFloorCode, pBillDate, pItemCode, pItemName, pItemQty, pItemPrice, pItemTotalPrice, pTax, pUniqueCode);
                                    else
                                        fileDataMissingTable.Rows.Add(pBillNumber, pFloorCode, pBillDate, pItemCode, pItemName, pItemQty, pItemPrice, pItemTotalPrice, pTax, pUniqueCode);

                                }
                            }
                        }
                        break;
                }
                if (fileDataMissingTable.Rows.Count > 0)
                {
                    Program.logString.AppendLine("Got Order Records with inconsistant data on file: " + filePath);
                    Program.logString.AppendLine(HelperManager.ConvertDataTableToString(fileDataMissingTable));
                    //MailHelper.sendEmailToEengage("tech@emunching.com", "", "Got Order Records with inconsistant data on file: " + filePath, HelperManager.ConvertDataTableToString(fileDataMissingTable), null, "eMunching Loyalty");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return fileDataTable;
        }

        public string[] readFileLIneByLine(String filePath)
        {
            System.IO.StreamReader filereader = new System.IO.StreamReader(filePath);
            string line;
            string strHolder = "";
            while ((line = filereader.ReadLine()) != null)
            {
                strHolder += Environment.NewLine + line;
            }
            return strHolder.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        }

        public bool movePayloadFile(String source, String destination)
        {
            System.IO.File.Move(source, destination);
            bool fileMoved = (!File.Exists(source) && File.Exists(destination));
            return fileMoved;
        }

    }
}