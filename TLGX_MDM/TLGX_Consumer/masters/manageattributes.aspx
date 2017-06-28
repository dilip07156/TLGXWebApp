<%@ Page Title="Manage Attribute" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manageattributes.aspx.cs" Inherits="TLGX_Consumer.masters.manageattributes" %>

<%@ Register Src="~/controls/attributes/attributeManager.ascx" TagPrefix="uc1" TagName="attributeManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1 class="page-header">Manage Attribute Masters</h1>
    </div>
    <uc1:attributeManager runat="server" ID="attributeManager" />
</asp:Content>
