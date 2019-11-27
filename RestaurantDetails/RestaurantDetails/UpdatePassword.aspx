<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdatePassword.aspx.cs" Inherits="RestaurantDetails.UpdatePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
            <asp:Button ID="btnUserAccount" runat="server" PostBackUrl="~/UserAccount.aspx"
                Style="z-index: 1; left: 104px; top: 15px; position: absolute"
                Text="User Account" />

            <asp:Label ID="lblUpdatePassword" runat="server"
                Style="z-index: 1; left: 102px; top: 56px; position: absolute"
                Text="Update Password" Font-Bold="True" Font-Size="Larger"
                Font-Underline="True"></asp:Label>

            <asp:Label ID="lblCurrentPassword" runat="server"
                Style="z-index: 1; left: 53px; top: 111px; position: absolute"
                Text="Current Password:"></asp:Label>
            <asp:Label ID="lblNewPassword" runat="server"
                Style="z-index: 1; left: 72px; top: 148px; position: absolute"
                Text="New Password"></asp:Label>

            <asp:TextBox ID="txtCurrentPassword" runat="server"
                Style="z-index: 1; left: 184px; top: 111px; position: absolute"
                TextMode="Password"></asp:TextBox>
            <asp:TextBox ID="txtNewPassword" runat="server"
                Style="z-index: 1; left: 184px; top: 149px; position: absolute"
                TextMode="Password"></asp:TextBox>


            <asp:Label ID="lblConfirmPassword" runat="server"
                Style="z-index: 1; left: 50px; top: 186px; position: absolute"
                Text="Confirm Password:"></asp:Label>
            <asp:TextBox ID="txtConfirmPassword" runat="server"
                Style="z-index: 1; left: 184px; top: 188px; position: absolute"
                TextMode="Password"></asp:TextBox>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <asp:UpdateProgress  runat="server">
                        <ProgressTemplate>
                            <asp:Image runat="server" ImageUrl="~/IMAGES/giphy.gif" Style="z-index: 1; left: 355px; top: 199px; position: absolute; height: 98px; width: 144px;" />                       
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>
                <asp:Label ID="lblError" runat="server" ForeColor="Red"
                    Style="z-index: 1; left: 380px; top: 234px; position: absolute"></asp:Label>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnUpdatePassword" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

        <asp:Button ID="btnUpdatePassword" runat="server"
            Style="z-index: 1; left: 183px; top: 230px; position: absolute"
            Text="Update Password" OnClick="btnUpdatePassword_Click" />

    </form>
</body>
</html>
