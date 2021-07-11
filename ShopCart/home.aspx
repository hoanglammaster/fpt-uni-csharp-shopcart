<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="ShopCart.home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>List Books</title>

    <style type="text/css">
.gridview__column--image img{
    width: 200px;
    height: 200px;
}
.form__gridview--left{
    float: left;
    width : 80%;
}
.form__cart--right{
    float: left;
    width: 15%;
}
.cart__container{
    width: 70px;
    height: 70px;
    background-color: rgb(230,230,230);
    margin: auto;
    text-align : center;
    vertical-align: middle;
}
</style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="form__gridview--left">
            <asp:GridView ID="sourceBooks"  CssClass ="gridview__column--image" AutoGenerateColumns="false" EnableViewState="true" runat="server">
                <Columns >
                    <asp:BoundField  DataField="Id" HeaderText="Id"/>
                    <asp:BoundField  DataField="Name" HeaderText="Book Name"/>
                    <asp:BoundField  DataField="Price" HeaderText="Prices"/>
                    <asp:BoundField  DataField="Quantity" HeaderText="Quantiy In Stocks"/>
                     <asp:ImageField DataImageUrlField="ImageURL" HeaderText="Image"></asp:ImageField>
                    <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="addToCart" runat="server" Text="Add To Cart" OnClick="addToCart_Click" />
            </ItemTemplate>
        </asp:TemplateField>
                 </Columns>
            </asp:GridView>
            
        </div>
        <div class="form__cart--right">
            <div class="cart__container">
                <div>
                <asp:Label runat="server" ID="cart" EnableViewState="true" Text="0"></asp:Label>
                    <a href = "cart.aspx"><img src="img/cart.png"/></a>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
