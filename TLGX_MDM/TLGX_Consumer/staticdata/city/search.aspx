<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="search.aspx.cs" Inherits="TLGX_Consumer.staticdata.searchcity" %>

<%@ Register Src="~/controls/staticdata/SearchCityMap.ascx" TagPrefix="uc1" TagName="CityMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1 class="page-header">Manage Supplier City Mapping</h1>
        <uc1:CityMap runat="server" ID="CityMap" />
    </div>
</asp:Content>
