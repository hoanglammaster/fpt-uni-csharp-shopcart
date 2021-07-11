using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;


namespace ShopCart.DAL
{
    public class DAO
    { 
        public static SqlConnection GetConnection()
        {
            SqlConnection connection =  new SqlConnection(ConfigurationManager.ConnectionStrings["ShopBook"].ConnectionString);
            return connection;
        }
        public static DataTable GetTableFromSql(SqlCommand command)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataTable table = new DataTable
            {
                Locale = CultureInfo.InvariantCulture
            };
            dataAdapter.Fill(table);
            return table;
        }
        public static void InsertDataToSql(SqlCommand command)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.InsertCommand = command;
            adapter.InsertCommand.ExecuteNonQuery();
        }
        public static void UpdateDataToSql(SqlCommand command)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.UpdateCommand = command;
            adapter.UpdateCommand.ExecuteNonQuery();
        }

        public static int InsertAndGetId(SqlCommand command)
        {
            command.ExecuteNonQuery();
            return (int)command.Parameters["@RETURN_VALUE"].Value;
        }
        public static void RunBatchSql(List<String> dbOperations)
        {
            SqlConnection conn = DAO.GetConnection();
            SqlTransaction transaction = conn.BeginTransaction();

            foreach (string commandString in dbOperations)
            {
                SqlCommand cmd = new SqlCommand(commandString, conn, transaction);
                cmd.ExecuteNonQuery();
            }

            transaction.Commit();
        }
    }
}
