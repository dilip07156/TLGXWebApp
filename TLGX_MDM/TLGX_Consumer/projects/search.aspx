<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="TLGX_Consumer.projects.search" %>

<%@ Register Src="~/controls/projects/searchProjects.ascx" TagPrefix="uc1" TagName="searchProjects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-header">Search Data Management Projects</h1>


    <uc1:searchProjects runat="server" id="searchProjects" />

</asp:Content>
