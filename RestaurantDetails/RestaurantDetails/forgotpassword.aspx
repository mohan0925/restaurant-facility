<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="forgotpassword.aspx.cs" MaintainScrollPositionOnPostBack="true" Inherits="RestaurantDetails.forgotpassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
        .auto-style1 {
            width: auto;
            position: inherit;
            z-index: auto;
            top: auto;
            right: auto;
            bottom: auto;
            left: auto;
            height: 26px;
        }
        .auto-style2 {
            height: 26px;
            width: 219px;
        }
        .auto-style3 {
            height: 23px;
            width: 219px;
        }
        .auto-style4 {
            width: 219px;
        }
        .auto-style6 {
            position: inherit;
            z-index: auto;
            top: auto;
            right: auto;
            bottom: auto;
            left: auto;
        }
    </style>

</head>
<body>

    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>   
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" 	>
        <ContentTemplate>
               <asp:Button ID="Userlogin" runat="server" align="center" Font-Bold="True" Font-Size="Medium" 
                            Text="Login page" PostBackUrl="UserLogin.aspx"  style="margin-top: 0px;"  />
            <table align="center" style="height: 248px;width: 638px;">
                <tr>
                    <td align="center" style="height: 185px;">
                    
                        <table class="style7">
                            <tr>
                                <td align="right"  style="font-weight: bold;" class="auto-style1">
                                    Email</td>
                                <td align="left" class="auto-style2">
                        <asp:TextBox ID="EmailID" runat="server" Width="211px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="font-weight: bold;width: auto; position: inherit; z-index: auto; height: auto; top: auto; right: auto; bottom: auto; left: auto;width: 335px; height: 23px;">
                                    User Role</td>
                                <td align="left" class="auto-style3">
                         
                                    <asp:DropDownList ID="waiterrole" runat="server" CssClass="auto-style6" Width="216px">
                                    </asp:DropDownList>
                         
                                    </td>
                            </tr>
                            <tr>
                                <td style="width: 335px;">
                                    &nbsp;</td>
                                <td align="left" class="auto-style4">
                        <asp:Button ID="Button1" runat="server" Font-Bold="True" Font-Size="Medium" 
                            Text="Get Password" OnClick="Button1_Click1" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
        <tr>
            <td align="center">
                <br />
                <asp:Literal ID="Label_Forgotpwd" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;</td>
        </tr>
        </table>
            </ContentTemplate>
           </asp:UpdatePanel>
        </div>
    </form>




</body>
</html>

