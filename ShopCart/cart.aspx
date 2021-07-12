<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cart.aspx.cs" Inherits="ShopCart.cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="css/global.css" />
    <title>Cart
    </title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form__container--left">
            <asp:GridView CssClass="form__table--containter" runat="server" ID="sourceBook" AutoGenerateColumns="false" EnableViewState="true">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" />
                    <asp:BoundField DataField="Name" HeaderText="Book Name" />
                    <asp:BoundField DataField="Price" HeaderText="Prices" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity In Stocks" />
                    <asp:ImageField DataImageUrlField="ImageURL" HeaderText="Image"></asp:ImageField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="add" runat="server" Text="+" OnClick="add_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Order" HeaderText="Number Order" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="minus" runat="server" Text="-" OnClick="minus_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="remove" runat="server" Text="Remove" OnClick="remove_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="form__container--right">
            <div class="cart__container">
                <asp:Label CssClass="cart__number" runat="server" EnableViewState="true" ID="total"></asp:Label>
                <asp:Button runat="server" ID="Buy" Text="Buy Now!" OnClick="Buy_Click" />
            </div>
        </div>
    </form>
</body>
</html>
