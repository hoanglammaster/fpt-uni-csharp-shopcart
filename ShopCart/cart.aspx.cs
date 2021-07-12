using ShopCart.BLL;
using ShopCart.DAL;
using ShopCart.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShopCart
{
    public partial class cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["listBook"] == null)
                {
                    return;
                }
                else
                {
                    Dictionary<int, int> listBookId = (Dictionary<int, int>)Session["listBook"];
                    List<Book> listBook = new List<Book>();
                    Decimal totalMoney = 0;
                    foreach (KeyValuePair<int, int> id in listBookId)
                    {
                        Book book = BookBll.GetBookWithId(id.Key);
                        book.Order = id.Value;
                        totalMoney += (book.Price * id.Value);
                        listBook.Add(book);
                    }
                    sourceBook.DataSource = listBook;
                    total.Text = totalMoney.ToString();
                    DataBind();
                }
            }
        }

        protected void minus_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            GridViewRow row = (GridViewRow)button.Parent.Parent;
            int order = Convert.ToInt32(sourceBook.Rows[row.RowIndex].Cells[6].Text);
            if (order <= 1)
            {
                return;
            }
            else
            {
                order--;

                int bookId = Convert.ToInt32(sourceBook.Rows[row.RowIndex].Cells[0].Text);

                Dictionary<int, int> listBookId = (Dictionary<int, int>)Session["listBook"];
                listBookId[bookId] = listBookId[bookId] - 1;
                Session["listBook"] = listBookId;

                Decimal totalMoney = Decimal.Parse(total.Text);
                totalMoney -= Decimal.Parse(sourceBook.Rows[row.RowIndex].Cells[2].Text);
                total.Text = totalMoney.ToString();

                sourceBook.Rows[row.RowIndex].Cells[6].Text = order.ToString();
            }
        }

        protected void remove_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            GridViewRow row = (GridViewRow)button.Parent.Parent;

            int bookId = Convert.ToInt32(sourceBook.Rows[row.RowIndex].Cells[0].Text);

            Dictionary<int, int> listBookId = (Dictionary<int, int>)Session["listBook"];
            listBookId.Remove(bookId);
            Session["listBook"] = listBookId;

            List<Book> listBook = new List<Book>();
            Decimal totalMoney = 0;
            foreach (KeyValuePair<int, int> id in listBookId)
            {
                Book book = BookBll.GetBookWithId(id.Key);
                book.Order = id.Value;
                totalMoney += (book.Price * id.Value);
                listBook.Add(book);
            }

            sourceBook.DataSource = listBook;
            total.Text = totalMoney.ToString();

            DataBind();
        }

        protected void add_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            GridViewRow row = (GridViewRow)button.Parent.Parent;

            int order = Convert.ToInt32(sourceBook.Rows[row.RowIndex].Cells[6].Text);
            int inStocks = Convert.ToInt32(sourceBook.Rows[row.RowIndex].Cells[3].Text);

            if (order >= inStocks)
            {
                return;
            }
            else
            {
                order++;

                int bookId = Convert.ToInt32(sourceBook.Rows[row.RowIndex].Cells[0].Text);

                Dictionary<int, int> listBookId = (Dictionary<int, int>)Session["listBook"];
                listBookId[bookId] = listBookId[bookId] + 1;
                Session["listBook"] = listBookId;

                Decimal totalMoney = Decimal.Parse(total.Text);
                totalMoney += Decimal.Parse(sourceBook.Rows[row.RowIndex].Cells[2].Text);
                total.Text = totalMoney.ToString();

                sourceBook.Rows[row.RowIndex].Cells[6].Text = order.ToString();
            }
        }

        protected void Buy_Click(object sender, EventArgs e)
        {
            Nullable<int> cartId = null;
            int userId = (int)Session["UserId"];

            foreach (GridViewRow rows in sourceBook.Rows)
            {
                cartId = OrderDao.InsertOrderToDB(userId, int.Parse(rows.Cells[0].Text), int.Parse(rows.Cells[6].Text), cartId, Decimal.Parse(total.Text));
            }
            Session["listBook"] = null;
            Response.Redirect("home.aspx");
        }
    }
}
