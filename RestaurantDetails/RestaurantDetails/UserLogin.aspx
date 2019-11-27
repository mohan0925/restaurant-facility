<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="RestaurantDetails.UserLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            z-index: 1;
            left: 275px;
            top: 224px;
            position: absolute;
        }
        .auto-style2 {
            z-index: 1;
            left: 277px;
            top: 260px;
            position: absolute;
        }
    </style>
</head>
<body>
   <form id="form1" runat="server">
    <div>

        <asp:Label ID="lblUsername" runat="server" Text="Username:" 
            style="z-index: 1; top: 88px; position: absolute; left: 97px; " width="64"></asp:Label>
        <asp:TextBox ID="txtUsername" runat="server" 
            style="z-index: 1; left: 181px; top: 88px; position: absolute"></asp:TextBox>
        
        <asp:Label ID="lblPassword" runat="server" Text="Password:" 
            style="z-index: 1; left: 97px; top: 129px; position: absolute"></asp:Label>
        <asp:TextBox ID="txtPassword" runat="server" 
            style="z-index: 1; left: 181px; top: 128px; position: absolute" 
            TextMode="Password"></asp:TextBox>
        
        <asp:Label ID="lblLoginPortal" runat="server" Font-Bold="True" 
            Font-Size="Larger" style="z-index: 1; left: 97px; top: 40px; position: absolute" 
            Text="Login Portal" Font-Underline="True"></asp:Label>
        <p>     
            <asp:Button ID="btnLogin" runat="server" Text="Login" 
                style="z-index: 1; left: 195px; top: 184px; position: absolute" OnClick="btnLogin_Click" 
                 />      
        </p>
        <asp:Label ID="lblError" runat="server" ForeColor="Red" 
            style="z-index: 1; left: 279px; top: 187px; position: absolute"></asp:Label>

      <asp:Label ID="ForgotPassword" runat="server" 
            style="z-index: 1; left: 140px; top: 226px; position: absolute" 
            Text="Forgot Password?"></asp:Label>
      <asp:Button ID="Forgot" runat="server" 
        Text="Get Password" PostBackUrl="forgotpassword.aspx" CssClass="auto-style1" />

    
        <asp:Label ID="lblRegister" runat="server" 
            style="z-index: 1; left: 151px; top: 262px; position: absolute" 
            Text="New User?"></asp:Label>
        
    </div>
    <asp:Button ID="btnRegister" runat="server" 
        Text="Register" PostBackUrl="WaiterRegistration.aspx" CssClass="auto-style2" />

    </form>
</body>
</html>
