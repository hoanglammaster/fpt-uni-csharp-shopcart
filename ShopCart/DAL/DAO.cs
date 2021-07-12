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
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ShopBook"].ConnectionString);
            return connection;
        }
    }
}
