<%@ Page Title="Supplier Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manage.aspx.cs" Inherits="TLGX_Consumer.suppliers.manage" %>

<%@ Register Src="~/controls/businessentities/manageSupplier.ascx" TagPrefix="uc1" TagName="manageSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:manageSupplier runat="server" ID="manageSupplier" />
</asp:Content>
