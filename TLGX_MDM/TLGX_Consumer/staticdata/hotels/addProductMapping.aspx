<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="addProductMapping.aspx.cs" Inherits="TLGX_Consumer.staticdata.hotels.addProductMapping" %>

<%@ Register Src="~/controls/staticdata/bulkHotelMapping.ascx" TagPrefix="uc1" TagName="bulkHotelMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-title">Add Suppler Product Mapping</h1>
    <br />
    <uc1:bulkHotelMapping runat="server" id="bulkHotelMapping" />

</asp:Content>
