<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierTaskCalender.aspx.cs" Inherits="TLGX_Consumer.suppliers.SupplierTaskCalender" %>

<%@ Register Src="~/controls/businessentities/SupplierTaskCalender.ascx" TagPrefix="uc1" TagName="SupplierTaskCalender" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1 class="page-header">Supplier Search</h1>
    </div>
    <uc1:SupplierTaskCalender runat="server" ID="oSupplierTaskCalender" />
</asp:Content