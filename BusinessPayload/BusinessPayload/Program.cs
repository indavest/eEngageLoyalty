using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace BusinessPayload
{
    class Program
    {
        public static string payloadPath = "E:/Dropbox/Toit/Payload/";
        public static string restaurantId = "67";
        static void Main(string[] args)
        {
            DateTime thisDate = DateTime.Now;

            string fileName = "payload_" + thisDate.ToString("ddMMyyyy_HHmmss") + "_" + restaurantId + ".csv";
            WriteToTextFile(";", fileName);
            //sendEmailToEengage("harsha@indavest.com", "", "From Console", "Test Email", "test@indavest.com", "eEngage");
        }

        public static void WriteToTextFile(string separator, string filename)
        {
            //DB Connection object and connection string
            string yesterdayDate = DateTime.Today.AddDays(-1).ToString("yyyyMMdd");
            string today = DateTime.Today.ToString("yyyyMMdd");
            Hashtable connectionObject = getDBNamesAndConnectionStrings();
            string connectionString = connectionObject["connectionStringApp"].ToString();
            string connectionStringLoyalty = connectionObject["connectionStringLoyalty"].ToString();
            string query = "SELECT O.BILNUB, O.RESCOD, /*Each section of Toit is demarkated as GRD, FFL, SFL. Those are the only ones we should care about*/";
                   query += "O.KOTDAT,O.ITMCOD,O.ITMNAM,SUM(O.QUANTY) QUANTITY,O.RATAMT PRICE ,SUM(O.VALAMT) SUBTOTAL,SUM(O.TAXAMT) TAX";
                   query += " FROM [PRISM].[dbo].[POSORD] O WHERE O.BILNUB IN (SELECT DISTINCT(BILNUB) FROM [PRISM].[dbo].[POSSET] S WHERE S.BILDAT = " + yesterdayDate + " OR S.BILDAT = "+ today +" /*This is to get everything dated yesterday*/ AND S.SETMOD IN (1,2)   /*this is important because anything other than 1 and 2 are for internal use only*/)";
                   query += " GROUP BY O.ITMCOD, O.ITMNAM, O.BILNUB, O.RESCOD, O.KOTDAT, O.RATAMT ORDER BY O.BILNUB, O.RESCOD";
            //string query = "SELECT  id, PrimaryCountry FROM Restaurant";
            string sep = separator;

            StreamWriter sw = new StreamWriter(payloadPath + filename);
            try
            {
                using (SqlConnection Conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand Cmd = new SqlCommand(query, Conn))
                    {
                        Conn.Open();
                        using (SqlDataReader dr = Cmd.ExecuteReader())
                        {
                            int fields = dr.FieldCount - 1;
                            if (!dr.HasRows)
                            {
                                sendEmailToEengage("tech@emunching.com", "", "Uploading Payload Failure From Toit", query + connectionString, null, "Toit Loyalty");
                                return;
                            }
                            DataTable dataTable = new DataTable();
                            dataTable.Load(dr);//Loads all the Payload from PRISM POSSET2013 to datatable
                            dr.Close();
                            Conn.Close();

                            SqlConnection Connection = new SqlConnection(connectionStringLoyalty);
                            SqlCommand Command = null;
                            Connection.Open();
                            DataTable billNum = dataTable.DefaultView.ToTable(true, "BILNUB", "RESCOD");
                            Hashtable billNum_UniqueCode_Map = new Hashtable(); //New hastable for making BilNumber and UniqueCode a Keyvalue pair

                            foreach (DataRow row in billNum.Rows)
                            {
                                query = "SELECT TOP 1 LoyaltyNumber FROM PRMLOYALTY WHERE BillNumber=" + row["BILNUB"] + " AND RESCOD = '" + row["RESCOD"] + "' ORDER BY BillTime DESC";
                                Command = new SqlCommand(query, Connection);
                                string uniqueCode = (string)Command.ExecuteScalar();
                                if (!string.IsNullOrEmpty(uniqueCode))
                                {
                                    billNum_UniqueCode_Map.Add(row["BILNUB"].ToString() + row["RESCOD"].ToString(), uniqueCode);
                                }
                            }
                            //Adding Uniquecode from keyvalue pair to datatable for printing
                            dataTable.Columns.Add("LoyaltyNumber", Type.GetType("System.String"));
                            foreach (DataRow row in dataTable.Rows)
                            {
                                row["LoyaltyNumber"] = billNum_UniqueCode_Map[row["BILNUB"].ToString() + row["RESCOD"].ToString()];
                                fields = dataTable.Columns.Count - 1;
                                StringBuilder sb = new StringBuilder();
                                for (int i = 0; i <= fields; i++)
                                {
                                    if (i != fields)
                                    {
                                        sep = separator;
                                    }
                                    else
                                    {
                                        sep = "";
                                    }
                                    sb.Append(row[i].ToString() + sep);

                                }
                                sw.WriteLine(sb.ToString());
                            }
                            sw.Close();
                            //UploadFile(payloadPath + filename, "ftp://www.emunching.com/" + filename, "Toit", "indavest", filename);
                            sendEmailToEengage("tech@emunching.com", "", "Uploading Payload Success From Toit", "Upload of " + filename + " from toit is successfull", null, "Toit Loyalty");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sendEmailToEengage("tech@emunching.com", "", "Generating CSV Exception From Toit", ex.Message, null, "Toit Loyalty");
            }

        }

        /// <summary>
        /// Methods to upload file to FTP Server
        /// </summary>
        /// <param name="_FilePath">local source file name</param>
        /// <param name="_UploadPath">Upload FTP path including Host name</param>
        /// <param name="_FTPUser">FTP login username</param>
        /// <param name="_FTPPass">FTP login password</param>
        public static void UploadFile(string _FilePath, string _UploadPath, string _FTPUser, string _FTPPass, string _FileName)
        {
            System.IO.FileInfo _FileInfo = new System.IO.FileInfo(_FilePath);

            try
            {
                // Create FtpWebRequest object from the Uri provided
                System.Net.FtpWebRequest _FtpWebRequest = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(new Uri(_UploadPath));

                // Provide the WebPermission Credintials
                _FtpWebRequest.Credentials = new System.Net.NetworkCredential(_FTPUser, _FTPPass);

                // By default KeepAlive is true, where the control connection is not closed
                // after a command is executed.
                _FtpWebRequest.KeepAlive = false;

                // set timeout for 20 seconds
                _FtpWebRequest.Timeout = 20000;

                // Specify the command to be executed.
                _FtpWebRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile;

                // Specify the data transfer type.
                _FtpWebRequest.UseBinary = true;

                // Notify the server about the size of the uploaded file
                _FtpWebRequest.ContentLength = _FileInfo.Length;

                //_FtpWebRequest.UsePassive = false;
                // The buffer size is set to 2kb
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];

                // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
                System.IO.FileStream _FileStream = _FileInfo.OpenRead();
                // Stream to which the file to be upload is written
                System.IO.Stream _Stream = _FtpWebRequest.GetRequestStream();

                // Read from the file stream 2kb at a time
                int contentLen = _FileStream.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    _Stream.Write(buff, 0, contentLen);
                    contentLen = _FileStream.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                _Stream.Close();
                _Stream.Dispose();
                _FileStream.Close();
                _FileStream.Dispose();
                sendEmailToEengage("tech@emunching.com", "", "Uploading Payload Success From Toit", "Upload of " + _FileName + " from toit is successfull" , null, "Toit Loyalty");
            }
            catch (Exception ex)
            {
                sendEmailToEengage("tech@emunching.com", "", "Uploading Payload Exception From Toit", ex.Message, null, "Toit Loyalty");
                //MessageBox.Show(ex.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void sendEmailToEengage(String To, String Cc, String Subject, String Body, String From, String FromName)
        {
            string SMTP_HOST = "smtp.gmail.com";
            string SMTP_USER = "tech@emunching.com";
            string SMTP_PASS = "..emunching01";
            try
            {
                MailMessage msg = new MailMessage();
                msg.Subject = Subject;
                msg.Body = Body;
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

                NetworkCredential authinfo = new NetworkCredential(SMTP_USER, SMTP_PASS);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = authinfo;
                smtp.EnableSsl = true;
                smtp.Send(msg);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Reads all the three connection strings from web.config and returns a Hashtable of DB names.
        /// </summary>
        public static Hashtable getDBNamesAndConnectionStrings()
        {
            string connectionStringApp;
            string connectionStringLoyalty;
            //connectionStringApp = "Data Source=192.168.1.100;database=PRISM;uid=PRISM;pwd=PRISM;";
            connectionStringApp = "Data Source=192.168.1.100;database=PRISM;uid=PRISM;pwd=PRISM;";
            connectionStringLoyalty = "Data Source=192.168.1.100;database=LOYALTY;uid=LOYALTY;pwd=LOYALTY;";

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionStringApp);

            Hashtable catalog = new Hashtable();
            catalog.Add("app", builder.InitialCatalog);
            catalog.Add("connectionStringApp", connectionStringApp);
            catalog.Add("connectionStringLoyalty", connectionStringLoyalty);

            return catalog;
        }
    }
}
