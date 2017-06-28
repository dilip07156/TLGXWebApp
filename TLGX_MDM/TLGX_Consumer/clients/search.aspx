<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="TLGX_Consumer.clients.search" %>

<%@ Register Src="~/controls/businessentities/searchClient.ascx" TagPrefix="uc1" TagName="searchClient" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-header">Client Search</h1>

    <uc1:searchClient runat="server" id="searchClient" />
</asp:Content>
