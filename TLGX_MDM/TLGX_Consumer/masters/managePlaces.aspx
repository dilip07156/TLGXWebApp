<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="managePlaces.aspx.cs" Inherits="TLGX_Consumer.masters.managePlaces" %>
<%@ Register Src="~/controls/places/placeManager.ascx" TagPrefix="uc1" TagName="placeManager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-header">Manage Place Masters</h1>
        <uc1:placeManager runat="server" id="placeManager" />
</asp:Content>
