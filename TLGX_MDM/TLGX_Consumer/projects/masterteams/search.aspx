<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="TLGX_Consumer.projects.masterteams.search" %>
<%@ Register Src="~/controls/projects/searchMasterTeams.ascx" TagPrefix="uc1" TagName="searchMasterTeams" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-header">Search Data Management Workflow Teams</h1>
    <uc1:searchMasterTeams runat="server" id="searchMasterTeams" />
</asp:Content>
