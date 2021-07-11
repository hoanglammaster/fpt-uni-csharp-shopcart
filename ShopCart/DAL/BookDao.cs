using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShopCart.DAL
{
    public class BookDao
    {
        public static DataTable GetDataTableAllBooks()
        {
            string query = "SELECT b.BookId,BookName,Prices,Quantity,ImageURL FROM Books b JOIN Images i ON b.BookId = i.BookId";
            SqlConnection connection = DAO.GetConnection();
            connection.Open();
            SqlCommand command = new SqlCommand(query,connection);

            return DAO.GetTableFromSql(command);
        }
        public static DataTable GetDataTableBookWithId(int bookId)
        {
            string query = "SELECT b.BookId,BookName,Prices,Quantity,ImageURL FROM Books b JOIN Images i ON b.BookId = i.BookId WHERE b.BookId = @BookId";
            SqlConnection connection = DAO.GetConnection();
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            SqlParameter parameter = new SqlParameter("BookId",bookId);
            command.Parameters.Add(parameter);
            return DAO.GetTableFromSql(command);
        }
    }
}