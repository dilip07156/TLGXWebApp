<%@ Page Title="Supplier Search" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="TLGX_Consumer.suppliers.search" %>

<%@ Register Src="~/controls/businessentities/SearchSupplier.ascx" TagPrefix="uc1" TagName="SearchSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1 class="page-header">Supplier Search</h1>
    </div>
    <uc1:SearchSupplier runat="server" ID="SearchSupplier" />

</asp:Content>
