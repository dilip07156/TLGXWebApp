<%@ Page Title="Port Search" Language="C#" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="portSearch.aspx.cs" Inherits="TLGX_Consumer.geography.portSearch" %>
<%@ Register Src="~/controls/geography/uc_portSearch.ascx" TagPrefix="uc1" TagName="uc_portSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-header">Manage Ports</h1>
    <uc1:uc_portSearch runat="server" ID="uc_portSearch" />
</asp:Content>
