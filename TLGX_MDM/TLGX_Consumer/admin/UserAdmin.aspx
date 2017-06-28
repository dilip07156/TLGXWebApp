<%@ Page Title="User Admin" Language="C#" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="UserAdmin.aspx.cs" Inherits="TLGX_Consumer.admin.UserAdmin" %>

<%@ Register Src="~/controls/admin/UserManagement.ascx" TagPrefix="uc1" TagName="UserManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-header"><%: Title %></h2>
    <uc1:UserManagement runat="server" ID="UserManagement" />
</asp:Content>
