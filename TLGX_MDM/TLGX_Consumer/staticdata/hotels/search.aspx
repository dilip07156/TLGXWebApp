﻿<%@ Page Title="Hotel Mapping" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="TLGX_Consumer.staticdata.searchAccommodationProductMapping" %>

<%@ Register Src="~/controls/staticdata/searchAccoMapping.ascx" TagPrefix="uc1" TagName="searchAccoMapping" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">

        <h1 class="page-header">Product Supplier Mapping - Accommodation</h1>

        <uc1:searchAccoMapping runat="server" ID="searchAccoMapping" />

    </div>
</asp:Content>
