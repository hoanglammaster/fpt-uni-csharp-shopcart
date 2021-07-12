using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ShopCart.DAL
{
    public class BookDao
    {
        public static DataTable GetDataTableAllBooks()
        {
            string query = "SELECT b.BookId,BookName,Prices,Quantity,ImageURL FROM Books b JOIN Images i ON b.BookId = i.BookId";
            using (SqlConnection connection = DAO.GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
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
            }
        }
        public static DataTable GetDataTableBookWithId(int bookId)
        {
            string query = "SELECT b.BookId,BookName,Prices,Quantity,ImageURL FROM Books b JOIN Images i ON b.BookId = i.BookId WHERE b.BookId = @BookId";
            using (SqlConnection connection = DAO.GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlParameter parameter = new SqlParameter("BookId", bookId);
                    command.Parameters.Add(parameter);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                    DataTable table = new DataTable
                    {
                        Locale = CultureInfo.InvariantCulture
                    };
                    dataAdapter.Fill(table);
                    return table;
                }
            }

        }
    }
}