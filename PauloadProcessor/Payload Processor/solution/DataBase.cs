using System;

namespace ProcessBusinessPayload
{
    public class DataBaseManager
    {

        public int updateDataBaseWithSP(String sp, DataTable dataTable, String uniqueRow)
        {
            int returnData = 0;
            using (SqlConnection connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=eMunching_Loyalty;Persist Security Info=True;User ID=MenuTrix;Password=..menutrix01;MultipleActiveResultSets=True"))
            {
                try
                {
                    //remove duplicate records from dataTable
                    String dataTableString = ConvertDataTableToSQLString(dataTable);
                    dataTable = this.RemoveDuplicateRows(dataTable, uniqueRow);
                    dataTableString = ConvertDataTableToSQLString(dataTable);
                    //write to database using stored procedure
                    using (SqlCommand command = new SqlCommand(sp))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.Parameters.AddWithValue("@TVP", dataTable);
                        connection.Open();
                        returnData = command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
            return returnData;
        }

        public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
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
    }
}