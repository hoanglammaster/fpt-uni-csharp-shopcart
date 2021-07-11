using ShopCart.DAL;
using ShopCart.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ShopCart.BLL
{
    public class BookBll
    {
        public static List<Book> GetListAllBooks ()
        {
            DataTable dataTable = BookDao.GetDataTableAllBooks();
            List<Book> listBook = new List<Book>();
            foreach(DataRow row in dataTable.Rows)
            {
                listBook.Add(new Book(Convert.ToInt32(row["BookId"].ToString()),
                    row["BookName"].ToString(),
                    Decimal.Parse(row["Prices"].ToString()),
                    Convert.ToInt32(row["Quantity"].ToString()),
                    "img/"+row["ImageURL"].ToString()
                    ));
            }
            return listBook;
        }
        public static Book GetBookWithId(int bookId)
        {
            DataTable dataTable = BookDao.GetDataTableBookWithId(bookId);
            DataRow row = dataTable.Rows[0];
            return new Book(Convert.ToInt32(row["BookId"].ToString()),
                    row["BookName"].ToString(),
                    Decimal.Parse(row["Prices"].ToString()),
                    Convert.ToInt32(row["Quantity"].ToString()),
                    "img/" + row["ImageURL"].ToString()
                    );
            
        }
    }
}