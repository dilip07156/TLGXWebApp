<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="TLGX_Consumer.activity.search" %>


<%@ Register Src="~/controls/activity/searchActivityProductName.ascx" TagPrefix="uc1" TagName="searchActivityProductName" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h1 class="page-header">Search Activity Product Name</h1>

    <uc1:searchActivityProductName runat="server" id="searchActivityProductName" />

    </asp:Content>
