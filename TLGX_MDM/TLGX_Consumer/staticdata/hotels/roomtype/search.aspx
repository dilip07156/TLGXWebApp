<%@ Page Title="Room Type Mapping" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="search.aspx.cs" Inherits="TLGX_Consumer.staticdata.hotels.roomtype.search" %>

<%@ Register Src="~/controls/roomtype/searchRoomTypes.ascx" TagPrefix="uc1" TagName="searchRoomTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../../Content/JS_Defined/searchRoomTypes.js"></script>

    <h1 class="page-header">Room Type Mapping</h1>
    <uc1:searchRoomTypes runat="server" ID="searchRoomTypes" />
</asp:Content>
