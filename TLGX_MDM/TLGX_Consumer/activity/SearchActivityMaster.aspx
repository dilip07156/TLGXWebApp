<%@ Page Title="Activity Search" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="TLGX_Consumer.activity.search" %>


<%@ Register Src="~/controls/activity/SearchActivityMasterControl.ascx" TagPrefix="uc1" TagName="SearchActivityMasterControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">

        <h1 class="page-header">Search Activity Master</h1>

        <uc1:SearchActivityMasterControl runat="server" ID="SearchActivityMasterControl" />

    </div>

</asp:Content>
