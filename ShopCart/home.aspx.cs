using ShopCart.BLL;
using ShopCart.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShopCart
{
    public partial class home : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                sourceBooks.DataSource = BookBll.GetListAllBooks();
                Session["UserId"] = 1000;
                DataBind();
            }
            
        
        }

        protected void addToCart_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            GridViewRow row = (GridViewRow)button.Parent.Parent;
            int quantity = Convert.ToInt32(sourceBooks.Rows[row.RowIndex].Cells[3].Text);
            if (quantity <= 1)
            {
                return;
            }
            int bookInCart = Convert.ToInt32(cart.Text);
            
            bookInCart++;
            cart.Text = bookInCart.ToString();
            
            int bookId =Convert.ToInt32(sourceBooks.Rows[row.RowIndex].Cells[0].Text);
            sourceBooks.Rows[row.RowIndex].Cells[3].Text = (quantity - 1).ToString();
            AddToSession(bookId);
        }
        private void AddToSession(int bookId)
        {
            if(Session["listBook"] == null)
            {
                Dictionary<int, int> listBook = new Dictionary<int, int>();
                listBook.Add(bookId, 1);
                Session["listBook"] = listBook;
            }
            else
            {
                Dictionary<int, int> listBook = (Dictionary<int, int>)Session["listBook"];
                if (listBook.ContainsKey(bookId)){
                    int bookInSession;
                    listBook.TryGetValue(bookId, out bookInSession);
                    listBook[bookId] = bookInSession + 1;
                }
                else
                {
                    listBook.Add(bookId, 1);
                    Session["listBook"] = listBook;
                }
                Session["listBook"] = listBook;
            }
        }

       
    }
}