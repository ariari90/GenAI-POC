<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestForm.aspx.cs" Inherits="TestAuthSite.TestForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="font-family:Calibri">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <label>Token</label>
                </td>
                <td>
                    <asp:Label ID="token" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <label>UserName</label>
                </td>
                <td>
                    <asp:TextBox id="username" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label>Password</label>
                </td>
                <td>
                    <asp:TextBox ID="password" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        
            <asp:Button ID="Authenticate_Btn" runat="server" OnClick="Authenticate_Btn_Click" Text="Authenticate" />
        </p>
    </div>
    </form>
</body>
</html>
