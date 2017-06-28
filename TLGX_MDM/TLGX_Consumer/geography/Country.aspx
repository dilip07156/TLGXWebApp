<%@ Page Title="Country Search" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Country.aspx.cs" Inherits="TLGX_Consumer.geography.Country" %>

<%@ Register Src="~/controls/geography/countryManager.ascx" TagPrefix="uc1" TagName="countryManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-header">Country Manager</h1>
    <uc1:countryManager runat="server" ID="countryManager" />
</asp:Content>
