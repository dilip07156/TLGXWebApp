<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupplierSchedularTask.aspx.cs" Inherits="TLGX_Consumer.suppliers.SupplierSchedularTask" %>

<%@ Register Src="~/controls/businessentities/manageSupplierScheduleTask.ascx" TagPrefix="uc2" TagName="manageSupplierScheduleTask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc2:manageSupplierScheduleTask runat="server" ID="manageSupplierScheduleTask" />
</asp:Content>


