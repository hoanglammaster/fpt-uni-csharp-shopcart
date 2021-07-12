using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ShopCart.DAL
{
    public class OrderDao
    {
        public static int InsertOrderToDB( int userId, int bookId, int quantity, Nullable<int> cartId,Decimal total)
        {
            string query = "EXEC usp_InsertCart @UserId, @BookId , @Quantity , @Cart, @Total";
            using (SqlConnection connection = DAO.GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    List<SqlParameter> sqlParameters = new List<SqlParameter>();
                    sqlParameters.Add(new SqlParameter("UserId", userId));
                    sqlParameters.Add(new SqlParameter("BookId", bookId));
                    sqlParameters.Add(new SqlParameter("Quantity", quantity));
                    if (cartId == null)
                    {
                        sqlParameters.Add(new SqlParameter("Cart", DBNull.Value));
                    }
                    else
                    {
                        sqlParameters.Add(new SqlParameter("Cart", cartId));
                    }
                    sqlParameters.Add(new SqlParameter("Total", total));

                    command.Parameters.AddRange(sqlParameters.ToArray());
                    
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                    DataTable table = new DataTable
                    {
                        Locale = CultureInfo.InvariantCulture
                    };
                    dataAdapter.Fill(table);
                    
                    DataRow row = table.Rows[0];
                    return (int)row["CartId"];
                }
            }
        }
    }
}