<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manage.aspx.cs" Inherits="TLGX_Consumer.activity.manage" %>

<%@ Register Src="~/controls/activity/manageActivityProductName.ascx" TagPrefix="uc1" TagName="manageActivityProductName" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:manageActivityProductName runat="server" id="manageActivityProductName" />
</asp:Content>
