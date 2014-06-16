using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.Collections.Generic;
using System.IO;

namespace ProcessBusinessPayload
{
    public class MailHelper
    {
        public static void sendEmailToEengage(String To, String Cc, String Subject, String Body, String From, String FromName)
        {
            if (Program.isSendMailRequired)
            {
                string SMTP_HOST = "smtp.gmail.com";
                string SMTP_USER = "tech@emunching.com";
                string SMTP_PASS = "..emunching01";
                try
                {
                    //process body text for HTMl Formatting
                    string str = Body;
                    //All blank spaces would be replaced for html subsitute of blank space(&nbsp;) 
                    str = str.Replace(" ", "&nbsp;");

                    //Carriage return & newline replaced to <br/>
                    str = str.Replace("\r\n", "<br/>");

                    string bodyString = "<html>";
                    bodyString += "<head>";
                    bodyString += "<title></title>";
                    bodyString += "</head>";
                    bodyString += "<body>";
                    bodyString += "<table border=0 width=95% cellpadding=0 cellspacing=0>";
                    bodyString += "<tr>";
                    bodyString += "<td>" + str + "</td>";
                    bodyString += "</tr>";
                    bodyString += "</table>";
                    bodyString += "</body>";
                    bodyString += "</html>";


                    MailMessage msg = new MailMessage();
                    msg.Subject = Subject;
                    msg.Body = bodyString;
                    msg.IsBodyHtml = true;
                    msg.From = new MailAddress("svc@emunching.com", FromName);
                    String[] toArray = To.Split(new string[] { ";" }, StringSplitOptions.None);
                    String[] ccArray = Cc.Split(new string[] { ";" }, StringSplitOptions.None);
                    for (int toLength = 0; toLength < toArray.Length; toLength++)
                    {
                        msg.To.Add(new MailAddress(toArray[toLength]));
                    }

                    for (int ccLength = 0; ccLength < ccArray.Length; ccLength++)
                    {
                        if (!String.IsNullOrEmpty(ccArray[ccLength]))
                        {
                            msg.CC.Add(new MailAddress(ccArray[ccLength]));
                        }
                    }

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = SMTP_HOST;
                    smtp.Port = 587;


                    NetworkCredential authinfo = new NetworkCredential(SMTP_USER, SMTP_PASS);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = authinfo;
                    smtp.EnableSsl = true;
                    smtp.Send(msg);
                }
                catch (Exception ex)
                {
                    Program.isSendMailRequired = false;
                    throw (ex);
                }
            }
        }
    }

    public class DateHelper
    {
        public static DateTime parseDate(String dateString)
        {
            //string[] format = { "yyyyMMdd" };
            //DateTime dateObj;
            //if (DateTime.TryParseExact(dateString,format,System.Globalization.CultureInfo.InvariantCulture,System.Globalization.DateTimeStyles.None,out dateObj))
            //{

            //}
            DateTime date = DateTime.ParseExact(dateString, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            return date;
        }
    }
    
    public class HelperManager
    {
        public static string ConvertDataTableToSQLString(DataTable dataTable)
        {
            var output = new StringBuilder();

            var columnsWidths = new int[dataTable.Columns.Count];

            // Get column widths
            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    var length = row[i].ToString().Length;
                    if (columnsWidths[i] < length)
                        columnsWidths[i] = length;
                }
            }

            // Get Column Titles
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                var length = dataTable.Columns[i].ColumnName.Length;
                if (columnsWidths[i] < length)
                    columnsWidths[i] = length;
            }

            // Write Column titles
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                var text = dataTable.Columns[i].ColumnName;
                output.Append("|" + PadCenter(text, columnsWidths[i] + 2));
            }
            output.Append("|\n" + new string('=', output.Length) + "\n");

            // Write Rows
            foreach (DataRow row in dataTable.Rows)
            {
                output.Append("(");
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    var text = row[i].ToString();
                    text = text.Equals("True") ? "1" : text;
                    text = text.Equals("False") ? "0" : text;
                    text = (!IsNumeric(text)) ? '"' + text + '"' : text;
                    output.Append("," + PadCenter(text, columnsWidths[i] + 2));
                }
                output.Append("),\n");
            }
            return output.ToString();
        }

        public static void printDataTableToConsoleWithUserBreak(DataTable dt)
        {
            foreach (DataRow row in dt.Rows) // Loop over the rows.
            {
                Console.WriteLine("--- Row ---"); // Print separator.
                foreach (var item in row.ItemArray) // Loop over the items.
                {
                    Console.Write("Item: "); // Print label.
                    Console.WriteLine(item); // Invokes ToString abstract method.
                }
            }
            Console.Read(); // Pause.
        }

        public static DataTable CloneTable(DataTable originalTable)
        {
            return originalTable.Clone();
        }

        public static void WriteToCsvFile(DataTable dataTable, string filePath) {
            StringBuilder fileContent = new StringBuilder();

            foreach (var col in dataTable.Columns) {
                fileContent.Append(col.ToString() + ",");
            }

            fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);



            foreach (DataRow dr in dataTable.Rows) {

                foreach (var column in dr.ItemArray) {
                    fileContent.Append("\"" + column.ToString() + "\",");
                }

                fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);
            }

            System.IO.File.WriteAllText(filePath, fileContent.ToString());

        }
    

        /// <summary>
        /// Determines if the value contained in a string variable
        /// is a numeric value
        /// </summary>
        /// <param name="text">text value containing number</param>
        /// <returns>true if text is a number</returns>
        public static bool IsNumeric(string text)
        {
            //Test if string is valid
            return string.IsNullOrEmpty(text) ? false :
                //run regular expression to check if string is number
                Regex.IsMatch(text, @"^\s*\-?\d+(\.\d+)?\s*$");
        }

        public static string ConvertDataTableToString(DataTable dataTable)
        {
            var output = new StringBuilder();

            var columnsWidths = new int[dataTable.Columns.Count];

            // Get column widths
            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    var length = row[i].ToString().Length;
                    if (columnsWidths[i] < length)
                        columnsWidths[i] = length;
                }
            }

            // Get Column Titles
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                var length = dataTable.Columns[i].ColumnName.Length;
                if (columnsWidths[i] < length)
                    columnsWidths[i] = length;
            }

            // Write Column titles
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                var text = dataTable.Columns[i].ColumnName;
                output.Append("|" + PadCenter(text, columnsWidths[i] + 2));
            }
            output.AppendLine("|");
            output.AppendLine(new string('=', output.Length));

            // Write Rows
            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    var text = row[i].ToString();
                    output.Append("|" + PadCenter(text, columnsWidths[i] + 2));
                }
                output.AppendLine("|");
            }
            return output.ToString();
        }

        private static string PadCenter(string text, int maxLength)
        {
            int diff = maxLength - text.Length;
            return new string(' ', diff / 2) + text + new string(' ', (int)(diff / 2.0 + 0.5));

        }
    }
}