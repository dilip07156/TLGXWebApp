<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PortManage.aspx.cs" Inherits="TLGX_Consumer.geography.PortManage" %>

<%@ Register Src="~/controls/geography/uc_portManage.ascx" TagPrefix="uc1" TagName="uc_portManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:uc_portManage runat="server" ID="uc_portManage" />
</asp:Content>
