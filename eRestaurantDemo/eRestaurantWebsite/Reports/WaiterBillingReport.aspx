<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="WaiterBillingReport.aspx.cs" Inherits="Reports_WaiterBillingReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <h1>WaiterBillingReport</h1>
    <rsweb:ReportViewer ID="WaiterBillingReportViewer" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="800px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="600px">
        <LocalReport ReportPath="Reports\WaiterBilling.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="WaiterBillingReportObjectDataSource" Name="WaiterBillingDS" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:ObjectDataSource ID="WaiterBillingReportObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetWaiterBillingReport" TypeName="eRestaurantSystem.BLL.AdminController"></asp:ObjectDataSource>
</asp:Content>

