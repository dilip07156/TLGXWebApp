<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="search.aspx.cs" Inherits="TLGX_Consumer.staticdata.valuemapping.search" %>

<%@ Register Src="~/controls/staticdata/searchAttributeMapping.ascx" TagPrefix="uc1" TagName="searchAttributeMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <h1 class="page-header">Manage Supplier Attribute Mapping</h1>
    </div>

    <uc1:searchAttributeMapping runat="server" ID="searchAttributeMapping" />

</asp:Content>
