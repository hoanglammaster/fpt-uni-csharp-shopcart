<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cart.aspx.cs" Inherits="ShopCart.cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView runat="server" ID="sourceBook" AutoGenerateColumns="false" EnableViewState="true">
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
        <div>
            <asp:Label runat="server" EnableViewState="true" ID="total"></asp:Label>
            <asp:Button runat="server" ID="Buy" Text="Buy Now!" OnClick="Buy_Click"/>
        </div>
    </form>
</body>
</html>
