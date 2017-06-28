<%@ Page Title="" Language="C#" EnableEventValidation="false"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="TLGX_Consumer.staticdata.countrycity" %>

<%@ Register Src="~/controls/staticdata/searchCountryMap.ascx" TagPrefix="uc1" TagName="searchCountryMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-header">Manage Supplier Country Mapping</h1>
    <uc1:searchCountryMap runat="server" ID="searchCountryMap" />
</asp:Content>
