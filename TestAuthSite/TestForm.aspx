<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestForm.aspx.cs" Inherits="TestAuthSite.TestForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" xmlns:mso="urn:schemas-microsoft-com:office:office" xmlns:msdt="uuid:C2F41010-65B3-11d1-A29F-00AA00C14882">
<head runat="server">
    <title></title>

</head>
<body style="font-family: Calibri">
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
                        <asp:TextBox ID="username" runat="server"></asp:TextBox>
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
            <p>
                <asp:Button ID="Authenticate_Btn" runat="server" OnClick="Authenticate_Btn_Click" Text="Authenticate" />
            </p>
        </div>
        <hr />
        <div>
            <table>
                <tr>
                    <td>
                        <label>UniqueId</label>
                    </td>
                    <td>
                        <asp:TextBox ID="UniqueId" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>RequestType</label>
                    </td>
                    <td>
                        <asp:DropDownList ID="RequestTypeCombo" runat="server">
                            <asp:ListItem>AccountInfo</asp:ListItem>
                            <asp:ListItem>HoldingsInfo</asp:ListItem>
                            <asp:ListItem>UpdateDetails</asp:ListItem>
                            <asp:ListItem>Transaction</asp:ListItem>
                            <asp:ListItem>AccountAndHoldings</asp:ListItem>
                            <asp:ListItem>Extra</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <p>
                <asp:Button ID="GetDataBtn" runat="server" Text="Get Data" OnClick="GetDataBtn_Click" />
            </p>
            
        </div>
        <hr />
        <div>

        <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Height="50px" Width="125px" DataSourceID="ObjectDataSource1" Visible="False">
            <EditRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <Fields>
                <asp:TemplateField HeaderText="AccountInfoResponse">
                    <ItemTemplate>
                        <asp:DetailsView ID="DetailsView2" runat="server" AutoGenerateRows="False" DataSource='<%# new List<Common.Entities.InfoServiceResponse>{ (Common.Entities.InfoServiceResponse) Eval("AccountInfoResponse") } %>' Height="50px" Width="125px">
                            <Fields>
                                <asp:TemplateField HeaderText="PersonalInfo">
                                    <ItemTemplate>
                                        <asp:DetailsView ID="DetailsView3" runat="server" DataSource='<%# new List<Common.Entities.PersonalInfo>{ (Common.Entities.PersonalInfo) Eval("PersonalInfo") } %>' Height="50px" Width="125px">
                                            
                                        </asp:DetailsView>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Fields>
                        </asp:DetailsView>
                        <asp:DetailsView ID="DetailsView4" runat="server" DataSource='<%# new List<Common.Entities.InfoServiceResponse>{ (Common.Entities.InfoServiceResponse) Eval("AccountInfoResponse") } %>' Height="50px" Width="125px" AutoGenerateRows="False">
                            <Fields>
                                <asp:TemplateField HeaderText="BankInfo">
                                    <ItemTemplate>
                                        <asp:DetailsView ID="DetailsView5" runat="server" DataSource='<%# new List<Common.Entities.BankInfo> { (Common.Entities.BankInfo)Eval("BankInfo") } %>' Height="50px" Width="125px">
                                        </asp:DetailsView>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Fields>
                        </asp:DetailsView>
                        <asp:DetailsView ID="DetailsView8" runat="server" AutoGenerateRows="False" DataSource='<%# new List<Common.Entities.InfoServiceResponse>{ (Common.Entities.InfoServiceResponse) Eval("AccountInfoResponse") } %>' Height="50px" Width="125px">
                            <Fields>
                                <asp:TemplateField HeaderText="Schemes">
                                    <ItemTemplate>
                                        <asp:GridView ID="GridView1" runat="server" DataSource='<%# Bind("Schemes") %>'>
                                        </asp:GridView>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Fields>
                        </asp:DetailsView>
                        <asp:DetailsView ID="DetailsView6" runat="server" AutoGenerateRows="False" DataSource='<%# new List<Common.Entities.InfoServiceResponse>{ (Common.Entities.InfoServiceResponse) Eval("AccountInfoResponse") } %>' Height="50px" Width="125px">
                            <Fields>
                                <asp:TemplateField HeaderText="PreferredScheme">
                                    <ItemTemplate>
                                        <asp:DetailsView ID="DetailsView7" runat="server" DataSource='<%# new List<Common.Entities.SchemeInfo>{ (Common.Entities.SchemeInfo) Eval("PreferredScheme") } %>' Height="50px" Width="125px">
                                        </asp:DetailsView>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Fields>
                        </asp:DetailsView>
                    </ItemTemplate>
                </asp:TemplateField>
            </Fields>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
        </asp:DetailsView>
        
        </div>
        
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetResponse" TypeName="TestAuthSite.AggregatorResponseDataAccess" ></asp:ObjectDataSource>
        
        <asp:Label ID="ErrorMessageLabel" runat="server" ForeColor="#993300"></asp:Label>
        
    </form>
</body>
</html>
