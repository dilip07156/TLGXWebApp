<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manage.aspx.cs" Inherits="TLGX_Consumer.staticdata.hotels.roomtype.manage" %>

<%@ Register Src="~/controls/roomtype/manageRoomTypeMapping.ascx" TagPrefix="uc1" TagName="manageRoomTypeMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-header">Hotel: Manage Room Type Mapping</h1>

    <uc1:manageRoomTypeMapping runat="server" id="manageRoomTypeMapping" />


</asp:Content>
