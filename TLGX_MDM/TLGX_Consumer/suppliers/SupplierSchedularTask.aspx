<%@ Page Title="Supplier Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupplierSchedularTask.aspx.cs" Inherits="TLGX_Consumer.suppliers.SupplierSchedularTask" %>

<%@ Register Src="~/controls/businessentities/SupplierTaskLog.ascx" TagPrefix="uc1" TagName="SupplierTaskLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:SupplierTaskLog runat="server" ID="SupplierTaskLog" />
</asp:Content>


