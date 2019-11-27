<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WaiterRegistration.aspx.cs" Inherits="RestaurantDetails.WaiterRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            margin-top: 7px;
        }

        .auto-style2 {
            z-index: 1;
            left: 185px;
            top: 78px;
            position: absolute;
        }

        .auto-style3 {
            z-index: 1;
            left: 367px;
            top: 76px;
            position: absolute;
            width: 189px;
            right: 1308px;
        }

        .auto-style4 {
            z-index: 1;
            left: 187px;
            top: 223px;
            position: absolute;
            right: 1170px;
        }

        .auto-style5 {
            z-index: 1;
            left: 368px;
            top: 225px;
            position: absolute;
            width: 181px;
        }
    </style>
</head>
<body style="height: 432px">
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>

            <asp:Button ID="btnLogin" runat="server" PostBackUrl="~/UserLogin.aspx"
                Style="z-index: 1; left: 64px; top: 15px; position: absolute; width: 136px;"
                Text=" Got to Login" />

            <asp:Label ID="lblRegister" runat="server"
                Style="z-index: 1; left: 98px; top: 40px; position: absolute"
                Text="Registration Form" Font-Bold="True" Font-Size="Larger"
                Font-Underline="True"></asp:Label>

            <asp:Label ID="lblUsername" runat="server"
                Style="z-index: 1; left: 102px; top: 77px; position: absolute; height: 19px"
                Text="Username:"></asp:Label>
            <asp:Label ID="lblPassword" runat="server"
                Style="z-index: 1; left: 103px; top: 111px; position: absolute"
                Text="Password:"></asp:Label>
            <asp:Label ID="lblConfirmPassword" runat="server"
                Style="z-index: 1; left: 50px; top: 148px; position: absolute"
                Text="Confirm Password:"></asp:Label>
            <asp:TextBox ID="txtUsername" runat="server" AutoPostBack="True" OnTextChanged="TextBox1_TextChanged" CssClass="auto-style2"></asp:TextBox>
            <asp:Label ID="UserName_exists" runat="server" CssClass="auto-style3"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server"
                Style="z-index: 1; left: 184px; top: 111px; position: absolute"
                TextMode="Password"></asp:TextBox>
            <asp:TextBox ID="txtConfirmPassword" runat="server"
                Style="z-index: 1; left: 184px; top: 149px; position: absolute"
                TextMode="Password"></asp:TextBox>

            <asp:DropDownList ID="ddlRestaurants" runat="server"
                Style="z-index: 1; left: 184px; top: 297px; position: absolute;">
            </asp:DropDownList>
            <asp:Button ID="btnRegister" runat="server"
                Style="z-index: 1; left: 174px; top: 346px; position: absolute"
                Text="Register" OnClick="btnRegister_Click" />
            <asp:Label ID="lblError" runat="server" ForeColor="Red"
                Style="z-index: 1; left: 173px; top: 392px; position: absolute"></asp:Label>
        </div>
        <asp:Label ID="lblRealName" runat="server"
            Style="z-index: 1; left: 100px; top: 186px; position: absolute"
            Text="Full Name:"></asp:Label>
        <asp:TextBox ID="txtRealName" runat="server"
            Style="z-index: 1; left: 184px; top: 184px; position: absolute"></asp:TextBox>
        <asp:TextBox ID="txtEmailAddress" runat="server" AutoPostBack="True" CssClass="auto-style4" OnTextChanged="txtEmailAddress_TextChanged"></asp:TextBox>

        <asp:Label ID="EmailExist" runat="server" CssClass="auto-style5"></asp:Label>

        <asp:Label ID="lblEmailAddress" runat="server"
            Style="z-index: 1; left: 75px; top: 226px; position: absolute"
            Text="Email Address:"></asp:Label>
        <p class="auto-style1">
            <asp:Label ID="lblRestaurant" runat="server"
                Style="z-index: 1; left: 131px; top: 303px; position: absolute; width: 47px;"
                Text="Role :"></asp:Label>
            <asp:Label ID="lblRestaurant0" runat="server"
                Style="z-index: 1; left: 100px; top: 267px; position: absolute"
                Text="Restaurant:"></asp:Label>



        </p>
        <asp:DropDownList ID="ddlRestaurants0" runat="server"
            Style="z-index: 1; left: 185px; top: 268px; position: absolute">
        </asp:DropDownList>
        <p>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <p Style="z-index: 1; left: 584px; top: 110px; position: absolute; height: 204px; width: 249px;" >Working on the data</p>
                                <asp:Image runat="server" ImageUrl="~/IMAGES/giphy.gif" Style="z-index: 1; left: 584px; top: 133px; position: absolute; height: 204px; width: 249px;" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnRegister" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </p>
    </form>
</body>
</html>
