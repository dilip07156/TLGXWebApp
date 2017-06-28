<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="TLGX_Consumer.activity.ckis.search" %>

<%@ Register Src="~/controls/activity/ckis/searchCKISMasters.ascx" TagPrefix="uc1" TagName="searchCKISMasters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-header">Search CKIS Masters</h1>

    <uc1:searchCKISMasters runat="server" id="searchCKISMasters" />

</asp:Content>
