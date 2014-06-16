using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;

namespace ProcessBusinessPayload
{
    public class DataBaseManager
    {
        private string DSN = "";
        
        //DSN For Local DB
        //private string DSN = "Data Source=.\\SQLEXPRESS;Initial Catalog=eMunching_Loyalty;Persist Security Info=True;User ID=MenuTrix;Password=..menutrix01;MultipleActiveResultSets=True";
        //DSN for Live DBs
        //private string DSN = "Data Source=ijgz12f2wq.database.windows.net;Initial Catalog=eMunching_Loyalty;Persist Security Info=True;User ID=anandvv;Password=t8pD6FXb;MultipleActiveResultSets=True";
        SqlConnection con = null;
        SqlCommand command = null;

        public DataBaseManager() {
            //DSN = new FileManager().readFileLIneByLine("config.txt")[2];
            DSN = ProcessBusinessPayload.Program.configFileContents[3].Replace("\\\\","\\");
        }

        public int updateDataBaseWithSP(String sp, DataTable dataTable, String uniqueRow)
        {
            int returnData = 0;
            try
            {
                using (con = new SqlConnection(DSN))
                {
                    //remove duplicate records from dataTable
                    String dataTableString = HelperManager.ConvertDataTableToSQLString(dataTable);
                    dataTable = RemoveDuplicateRows(dataTable, uniqueRow);
                    dataTableString = HelperManager.ConvertDataTableToSQLString(dataTable);
                    //write to database using stored procedure
                    using (command = new SqlCommand(sp))
                    {
                        SqlParameter outParam = new SqlParameter("@RowCount", SqlDbType.Int, 100) { Direction = ParameterDirection.ReturnValue };
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = con;
                        command.Parameters.AddWithValue("@TVP", dataTable);
                        command.Parameters.Add(outParam);
                        con.Open();
                        returnData = command.ExecuteNonQuery();
                        returnData = Convert.ToInt32(command.Parameters["@RowCount"].Value.ToString());
                        con.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return returnData;
        }

        public DataTable processSelectQuery(String query)
        {
            SqlDataReader dataReader = null;
            DataTable dataTable = new DataTable();
            try
            {
                using (con = new SqlConnection(DSN))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(query);
                    command.CommandType = CommandType.Text;
                    command.Connection = con;
                    dataReader = command.ExecuteReader();
                    dataTable.Load(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return dataTable;
        }

        public String readPayLoadTable()
        {
            String query = "select TOP 1 * from dbo.Payload Order By DateCreated Desc";
            String LastReadFileName = "";
            try
            {
                DataTable data = processSelectQuery(query);
                foreach (DataRow dataRow in data.Rows)
                {
                    LastReadFileName = dataRow[1].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return LastReadFileName;
        }

        public bool checkFileReadOnPayloadTable(String filename) {
            String query = "select * from Payload where lastreadfile='" + filename + "'";
            try
            {
                DataTable data = processSelectQuery(query);
                if (data.Rows.Count > 0) return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
                return false; 
            }
        }

        public int processSQLInsertQuery(string query)
        {
            int effctedRecords = 0;
            try
            {
                using (con = new SqlConnection(DSN))
                {
                    con.Open();

                    command = new SqlCommand(query, con);
                    command.CommandType = CommandType.Text;
                    effctedRecords = command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
            return effctedRecords;
        }

        public void processInsert(DataTable dataTable)
        {
            try
            {
                using (con = new SqlConnection(DSN))
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        String billno = row["BillNumber"].ToString();
                        String item = row["ItemCode"].ToString();
                        item = row["RestaurantID"].ToString();
                        item = row["Quantity"].ToString();
                        command = new SqlCommand("INSERT INTO [OrderDetails]([BillNumber],[ItemCode],[RestaurantId],[Quantity])VALUES(@billNumber,@itemCode,@restId,@qty)", con);
                        command.Parameters.Add("billNumber", SqlDbType.VarChar).Value = row["BillNumber"];
                        command.Parameters.Add("itemCode", SqlDbType.Int).Value = row["ItemCode"];
                        command.Parameters.Add("restId", SqlDbType.Int).Value = row["RestaurantID"];
                        command.Parameters.Add("qty", SqlDbType.Int).Value = row["Quantity"];
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {

            }
        }

        public int[] processBulkCopy(DataSet dataSet, string[] tableNames)
        {
            System.Data.SqlClient.SqlBulkCopy sqlBulkCopy = null;
            con = null;
            int[] returnData = new int[tableNames.Length];
            DataTable dataTable = new DataTable();
            IDataReader reader = dataTable.CreateDataReader();
            using (con = new SqlConnection(DSN))
            {
                sqlBulkCopy = new SqlBulkCopy(con);
                try
                {
                    int index = 0;
                    con.Open();
                    foreach (DataTable table in dataSet.Tables)
                    {
                        dataTable = table;
                        sqlBulkCopy.DestinationTableName = tableNames[index];
                        sqlBulkCopy.WriteToServer(table);
                        returnData[index++] = table.Rows.Count;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
            return returnData;
        }

        public static DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colName]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[colName], string.Empty);
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);

            //Datatable which contains unique records will be return as output.
            return dTable;
        }

        /// <summary>
        /// Build an error message with the failed records and their related exceptions.
        /// </summary>
        /// <param name="connectionString">Connection string to the destination database</param>
        /// <param name="tableName">Table name into which the data will be bulk copied.</param>
        /// <param name="dataReader">DataReader to bulk copy</param>
        /// <returns>Error message with failed constraints and invalid data rows.</returns>
        public static string GetBulkCopyFailedData(string connectionString, string tableName, IDataReader dataReader)
        {
            StringBuilder errorMessage = new StringBuilder("Bulk copy failures:" + Environment.NewLine);
            SqlConnection connection = null;
            SqlTransaction transaction = null;
            SqlBulkCopy bulkCopy = null;
            DataTable tmpDataTable = new DataTable();

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                transaction = connection.BeginTransaction();
                bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.CheckConstraints, transaction);
                bulkCopy.DestinationTableName = tableName;

                // create a datatable with the layout of the data.
                DataTable dataSchema = dataReader.GetSchemaTable();
                foreach (DataRow row in dataSchema.Rows)
                {
                    tmpDataTable.Columns.Add(new DataColumn(
                       row["ColumnName"].ToString(),
                       (Type)row["DataType"]));
                }

                // create an object array to hold the data being transferred into tmpDataTable 
                //in the loop below.
                object[] values = new object[dataReader.FieldCount];

                // loop through the source data
                while (dataReader.Read())
                {
                    // clear the temp DataTable from which the single-record bulk copy will be done
                    tmpDataTable.Rows.Clear();

                    // get the data for the current source row
                    dataReader.GetValues(values);

                    // load the values into the temp DataTable
                    tmpDataTable.LoadDataRow(values, true);

                    // perform the bulk copy of the one row
                    try
                    {
                        bulkCopy.WriteToServer(tmpDataTable);
                    }
                    catch (Exception ex)
                    {
                        // an exception was raised with the bulk copy of the current row. 
                        // The row that caused the current exception is the only one in the temp 
                        // DataTable, so document it and add it to the error message.
                        DataRow faultyDataRow = tmpDataTable.Rows[0];
                        errorMessage.AppendFormat("Error: {0}{1}", ex.Message, Environment.NewLine);
                        errorMessage.AppendFormat("Row data: {0}", Environment.NewLine);
                        foreach (DataColumn column in tmpDataTable.Columns)
                        {
                            errorMessage.AppendFormat(
                               "\tColumn {0} - [{1}]{2}",
                               column.ColumnName,
                               faultyDataRow[column.ColumnName].ToString(),
                               Environment.NewLine);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                   "Unable to document SqlBulkCopy errors. See inner exceptions for details.",
                   ex);
            }
            finally
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
            return errorMessage.ToString();
        }
    }

}