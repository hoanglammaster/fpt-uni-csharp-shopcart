<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="ShopCart.home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>List Books</title>
    <link rel="stylesheet" type="text/css" href="css/global.css" />

</head>
<body>
    <form id="form1" runat="server">
        <div class="form__container--left">
            <asp:GridView ID="sourceBooks" CssClass="form__table--containter" AutoGenerateColumns="false" EnableViewState="true" runat="server">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" />
                    <asp:BoundField DataField="Name" HeaderText="Book Name" />
                    <asp:BoundField DataField="Price" HeaderText="Prices" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantiy In Stocks" />
                    <asp:ImageField DataImageUrlField="ImageURL" HeaderText="Image"></asp:ImageField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="addToCart" runat="server" Text="Add To Cart" OnClick="addToCart_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
        <div class="form__container--right">
            <div class="cart__container">
                <div>
                    <a href="cart.aspx">
                        <img src="img/cart.png" /></a>
                    <asp:Label CssClass="cart__number" runat="server" ID="cart" EnableViewState="true" Text="0"></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
