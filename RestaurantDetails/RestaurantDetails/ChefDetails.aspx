<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChefDetails.aspx.cs" Inherits="RestaurantDetails.ChefDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1 {
            height: 861px;
            width: 1284px;
        }

        .auto-style1 {
            z-index: 1;
            left: 351px;
            position: absolute;
            top: 162px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>

        <asp:Button ID="btnUserAccount" runat="server" PostBackUrl="~/UserAccount.aspx"
            Style="z-index: 1; left: 104px; top: 15px; position: absolute"
            Text="User Account" />

        <div>
            <asp:Label ID="lblChefDetails" runat="server" Font-Bold="True"
                Font-Size="Larger"
                Style="z-index: 1; left: 100px; top: 48px; position: absolute"
                Text="Chef Details" Font-Underline="True"></asp:Label>
        </div>
        <asp:Label ID="lblRestaurant" runat="server"
            Style="z-index: 1; left: 90px; top: 90px; position: absolute; height: 19px;"
            Text="Restaurant:"></asp:Label>
        <asp:TextBox ID="txtRestaurant" runat="server" ReadOnly="True"
            Style="z-index: 1; left: 165px; top: 87px; position: absolute"></asp:TextBox>

        <asp:Label ID="lblError" runat="server" ForeColor="Red"
            Style="z-index: 1; left: 321px; top: 89px; position: absolute"></asp:Label>

        <asp:Label ID="lblWaiters" runat="server"
            Style="z-index: 1; left: 102px; top: 125px; position: absolute"
            Text="Waiters(s) at your restaurant:"></asp:Label>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <asp:Image runat="server" ImageUrl="~/IMAGES/giphy.gif" Style="z-index: 1; left: 258px; top: 232px; position: absolute; height: 71px; width: 150px;" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>

                <asp:ListBox ID="lstWaiters" runat="server" Style="z-index: 1; left: 102px; top: 159px; position: absolute; height: 72px; width: 180px"></asp:ListBox>
                <asp:Label ID="lblSuccess" runat="server" Style="z-index: 1; left: 278px; top: 252px; position: absolute"></asp:Label>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnRemoveWaiter" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>


        <asp:Button ID="btnRemoveWaiter" runat="server"
            Style="z-index: 1; left: 102px; top: 250px; position: absolute"
            Text="Remove Waiter" OnClick="btnRemoveWaiter_Click" />
        <p>

            <asp:Label ID="lblCuisines" runat="server"
                Style="z-index: 1; left: 100px; top: 305px; position: absolute"
                Text="Cuisines at your restaurant:"></asp:Label>

        </p>

        <div style="z-index: 1; left: 110px; top: 340px; position: absolute;">
            <asp:Repeater ID="rptCuisines" runat="server" OnItemCommand="rptCuisines_ItemCommand">
                <ItemTemplate>
                    Cuisine Region:
        <strong><%#Eval("CuisineRegion") %></strong><br />
                    Cuisine Name:
        <strong><%#Eval("CuisineName") %></strong><br />
                </ItemTemplate>
                <SeparatorTemplate>
                    <div style="width: 300px;">
                        <hr />
                    </div>
                </SeparatorTemplate>
            </asp:Repeater>
            <asp:Label ID="Repeater_details" runat="server" ForeColor="#009900" CssClass="auto-style1"></asp:Label>
        </div>

        <p>
            &nbsp;
        </p>








    </form>
</body>
</html>
