<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manage.aspx.cs" Inherits="TLGX_Consumer.clients.manage" %>

<%@ Register Src="~/controls/businessentities/manageClient.ascx" TagPrefix="uc1" TagName="manageClient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:manageClient runat="server" id="manageClient" />
</asp:Content>
