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
                if(Session["listBook"] == null)
                {
                    return;
                }
                else
                {
                    Dictionary<int, int> listBookId = (Dictionary<int, int>)Session["listBook"];
                    List<Book> listBook = new List<Book>();
                    Decimal totalMoney =0;
                    foreach(KeyValuePair<int, int> id in listBookId)
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
            Nullable<int> cartid = null;
            string query = "EXEC usp_InsertCart @UserId, @BookId , @Quantity , @Cart, @Total";
            foreach (GridViewRow rows in sourceBook.Rows)
            {
                using (SqlConnection connection = DAO.GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        List<SqlParameter> sqlParameters = new List<SqlParameter>();
                        sqlParameters.Add(new SqlParameter("UserId", (int)Session["UserId"]));
                        sqlParameters.Add(new SqlParameter("BookId", int.Parse(rows.Cells[0].Text)));
                        sqlParameters.Add(new SqlParameter("Quantity", int.Parse(rows.Cells[6].Text)));
                        if(cartid== null)
                        {
                            sqlParameters.Add(new SqlParameter("Cart", DBNull.Value));
                        }
                        else
                        {
                            sqlParameters.Add(new SqlParameter("Cart", cartid));
                        }
                        sqlParameters.Add(new SqlParameter("Total", Decimal.Parse(total.Text)));
                        command.Parameters.AddRange(sqlParameters.ToArray());
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                        DataTable table = new DataTable
                        {
                            Locale = CultureInfo.InvariantCulture
                        };
                        dataAdapter.Fill(table);
                        DataRow row = table.Rows[0];
                        cartid = (int)row["CartId"];
                    }
                }
            }
            Session["listBook"] = null;
            Response.Redirect("home.aspx");
            

        }
            
        }
    }
