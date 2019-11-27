<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateChefRestaurant.aspx.cs" Inherits="RestaurantDetails.UpdateChefRestaurant" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            bottom: 114px;
        }

        .auto-style2 {
            z-index: 1;
            left: 104px;
            top: 15px;
            position: absolute;
        }

        .auto-style5 {
            z-index: 1;
            position: absolute;
            left: 306px;
            top: 261px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>

            <asp:Button ID="btnUserAccount" runat="server" PostBackUrl="~/UserAccount.aspx"
                Text="User Account" CssClass="auto-style2" OnClick="btnUserAccount_Click" />

            <asp:Label ID="lblTitle" runat="server"
                Style="z-index: 1; left: 102px; top: 56px; position: absolute"
                Text="Update Chef Restaurant" Font-Bold="True" Font-Size="Larger"
                Font-Underline="True"></asp:Label>

            <asp:Label ID="lblRestaurant" runat="server"
                Style="z-index: 1; left: 97px; top: 103px; position: absolute"
                Text="Current Restaurant:"></asp:Label>

            <asp:Label ID="lblSwitchToRestaurant" runat="server"
                Style="z-index: 1; left: 106px; top: 136px; position: absolute"
                Text="Switch to Restaurant:"></asp:Label>


            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <asp:Image runat="server" ImageUrl="~/IMAGES/giphy.gif" Style="z-index: 1; left: 281px; top: 235px; position: absolute; height: 77px; width: 125px;" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnUpdateRestaurant" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:TextBox ID="txtRestaurant" runat="server" ReadOnly="True"
                Style="z-index: 1; left: 224px; top: 102px; position: absolute"></asp:TextBox>
            <asp:ListBox ID="lstRestaurants" runat="server" Style="z-index: 1; left: 106px; top: 165px; position: absolute; height: 72px; width: 180px" CssClass="auto-style1"></asp:ListBox>
            <asp:Label ID="lblError" runat="server" ForeColor="Red" CssClass="auto-style5"></asp:Label>

            <asp:Button ID="btnUpdateRestaurant" runat="server"
                Style="z-index: 1; left: 106px; top: 254px; position: absolute"
                Text="Update Restaurant" OnClick="btnUpdateRestaurant_Click" />

        </div>
    </form>
</body>
</html>
