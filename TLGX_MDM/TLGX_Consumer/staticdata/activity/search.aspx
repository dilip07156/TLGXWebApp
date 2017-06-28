<%@ Page Title="Mapping - Activities" enableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="TLGX_Consumer.staticdata.activity" %>

<%@ Register Src="~/controls/staticdata/searchActivityMapping.ascx" TagPrefix="uc1" TagName="searchActivityMapping" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <h1 class="page-header">Product Supplier Mapping - Activities</h1>
    <uc1:searchActivityMapping runat="server" ID="searchActivityMapping" />
</asp:Content>
