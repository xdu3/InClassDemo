<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SpecialEventAdmin.aspx.cs" Inherits="SampleWeb_SpecialEventAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
   
    <table align="center" style="width: 70%">
        <tr>
            <td align="right" style="width:50%">select and event;</td>
            <td>
                <asp:DropDownList ID="SpecialEventList" runat="server" AppendDataBoundItems="True" DataSourceID="ODSSpecialEvents" DataTextField="Description" DataValueField="EventCode">
                    <asp:ListItem Value="z">Select event</asp:ListItem>
                </asp:DropDownList>
                <asp:LinkButton ID="FetchRegistrations" runat="server">Fetch Registrations</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="center">                
               <asp:GridView ID="en" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="ODSReservations">
                   <AlternatingRowStyle BackColor="#CCCCCC" Font-Size="Larger" />
                   <Columns>
                       <asp:BoundField DataField="CustomerName" HeaderText="Name" SortExpression="CustomerName">
                       <ItemStyle HorizontalAlign="Left" />
                       </asp:BoundField>
                       <asp:BoundField DataField="ReservationDate" DataFormatString="{0:MMM dd,yyyy}" HeaderText="Data" SortExpression="ReservationDate">
                       <HeaderStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="NumberInParty" HeaderText="Size" SortExpression="NumberInParty">
                       <HeaderStyle HorizontalAlign="Right" />
                       </asp:BoundField>
                       <asp:BoundField DataField="ContactPhone" HeaderText="Phone" SortExpression="ContactPhone">
                       <HeaderStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField DataField="ReservationStatus" HeaderText="Status" SortExpression="ReservationStatus">
                       <HeaderStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                   </Columns>
                   <EmptyDataTemplate>
                       No data to display at this time
                   </EmptyDataTemplate>
                   <PagerSettings Mode="NumericFirstLast" FirstPageText="Start" LastPageText="end" PageButtonCount="4" Position="Top" />
                </asp:GridView>
            </td>

        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="ODSSpecialEvents" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SpecialEvents_List" TypeName="eRestaurantSystem.BLL.AdminController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODSReservations" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetReservationsByEventCode" TypeName="eRestaurantSystem.BLL.AdminController">
        <SelectParameters>
            <asp:ControlParameter ControlID="SpecialEventList" Name="eventcode" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

