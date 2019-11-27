<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="RestaurantDetails.Logout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lblLogout" runat="server" Font-Bold="True" Font-Size="Larger" 
            style="z-index: 1; left: 72px; top: 28px; position: absolute" 
            Text="Logout" Font-Strikeout="False" Font-Underline="True"></asp:Label>
    
    </div>
    <asp:Label ID="lblConfirmation" runat="server" 
        style="z-index: 1; left: 74px; top: 64px; position: absolute" 
        Text="Are you sure you want to logout?"></asp:Label>
    <asp:Button ID="btnYes" runat="server"  
        style="z-index: 1; left: 96px; top: 97px; position: absolute;" 
        Text="Yes" OnClick="btnYes_Click" />
    <asp:Button ID="btnNo" runat="server" PostBackUrl="~/UserAccount.aspx" 
        style="z-index: 1;left: 153px;top: 96px;position: absolute;height: 23px;width: 37px;" Text="No" OnClick="btnNo_Click" />
    </form>
</body>
</html>
