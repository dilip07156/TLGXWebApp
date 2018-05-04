<%@ Page Title="City Master" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchCityMaster.aspx.cs" Inherits="TLGX_Consumer.geography.SearchCityMaster" %>

<%@ Register Src="~/controls/geography/citySearchManager.ascx" TagPrefix="uc1" TagName="citySearchManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container ">

        <h1 class="page-header">City Manager</h1>
    </div>
    <uc1:citySearchManager runat="server" ID="citySearchManager" />

</asp:Content>
