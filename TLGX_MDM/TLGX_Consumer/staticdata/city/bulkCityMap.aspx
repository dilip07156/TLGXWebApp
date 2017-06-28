<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="bulkCityMap.aspx.cs" Inherits="TLGX_Consumer.staticdata.city.bulkCityMap" %>

<%@ Register Src="~/controls/staticdata/bulkCityMapping.ascx" TagPrefix="uc1" TagName="bulkCityMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-header">Bulk City Map</h1>
    <br />
    <uc1:bulkCityMapping runat="server" ID="bulkCityMapping" />



    <br />


</asp:Content>
