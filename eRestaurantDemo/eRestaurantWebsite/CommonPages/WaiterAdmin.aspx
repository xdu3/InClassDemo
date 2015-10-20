<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="WaiterAdmin.aspx.cs" Inherits="CommonPages_WaiterAdmin" %>

<%@ Register Src="~/UserControl/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <br />
    <br />
    <br />
    <h1>Waiter Admin CRUD</h1>

    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

    <br />
    <asp:Label ID="Label1" runat="server" Text="Waiter Name"></asp:Label>
    &nbsp;
    <asp:DropDownList ID="WaiterList" runat="server" AppendDataBoundItems="True" DataSourceID="WaiterObjectDataSource" DataTextField="FullName" DataValueField="WaiterID" Width="329px">
        <asp:ListItem Value="0">Select a watier</asp:ListItem>
    </asp:DropDownList>
    &nbsp;<asp:LinkButton ID="FetchWaiter" runat="server" OnClick="FetchWaiter_Click">Fetch Waiter</asp:LinkButton>
    <br />
    <br />

    <asp:ObjectDataSource ID="WaiterObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}" OnSelected="CheckForException" SelectMethod="Waiters_List" TypeName="eRestaurantSystem.BLL.AdminController"></asp:ObjectDataSource>
    <br />
    <table align="center" style="width: 70%">
        <tr>
            <td>ID</td>
            <td>
                <asp:Label ID="IDLabel" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr>
            <td>First Name</td>
            <td>
                <asp:TextBox ID="FirstNameTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Last Name</td>
            <td>
                <asp:TextBox ID="LastNameTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Address</td>
            <td>
                <asp:TextBox ID="AddressTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 24px">Phone</td>
            <td style="height: 24px">
                <asp:TextBox ID="PhoneTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Hire Date</td>
            <td>
                <asp:TextBox ID="HireDateTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Release Date</td>
            <td>
                <asp:TextBox ID="ReleaseDateTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="WaiterInsert" runat="server" OnClick="WaiterInsert_Click">Insert</asp:LinkButton>
            </td>
            <td>
                <asp:LinkButton ID="WaiterUpdate" runat="server" OnClick="WaiterUpdate_Click">Update</asp:LinkButton>
            </td>
        </tr>
    </table>
    <br />

</asp:Content>

