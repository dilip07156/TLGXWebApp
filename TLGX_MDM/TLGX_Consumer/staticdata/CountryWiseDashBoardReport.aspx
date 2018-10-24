<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CountryWiseDashBoardReport.aspx.cs" Inherits="TLGX_Consumer.staticdata.CountryWiseDashBoardReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Hotel Mapping Country Report</h1>
        </div>
    </div>
    <div class="container">
        <div style="width: 100%; height: 100% ; overflow-x:scroll">
            <rsweb:ReportViewer ID="CountryReportViewer" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="100%" AsyncRendering="False" SizeToReportContent="True" ZoomMode="FullPage" ShowFindControls="False">
                <LocalReport ReportPath="staticdata\HotelMappingCountryReport.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </div>
    </div>


</asp:Content>
