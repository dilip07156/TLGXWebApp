<%@ Page Title="City Mapping" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="City.aspx.cs" Inherits="TLGX_Consumer.geography.City" %>

<%@ Register Src="~/controls/geography/cityManager.ascx" TagPrefix="uc1" TagName="cityManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1 class="page-header">City Manager</h1>
    </div>
    <uc1:cityManager runat="server" ID="cityManager" />

</asp:Content>
