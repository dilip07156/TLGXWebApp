<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="TLGX_Consumer.orgstructure.search" %>

<%@ Register Src="~/controls/businessentities/searchOrgStructure.ascx" TagPrefix="uc1" TagName="searchOrgStructure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <h1 class="page-header">Org Structure Search</h1>
    <uc1:searchOrgStructure runat="server" id="searchOrgStructure" />

</asp:Content>
