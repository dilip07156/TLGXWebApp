﻿<%@ Page Title="SupplierAPIData" Language="C#" EnableViewStateMac="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="APIData.aspx.cs" Inherits="TLGX_Consumer.staticdata.files.APIData" %>

<%@ Register Src="~/controls/staticdataconfig/manageAPILocation.ascx" TagPrefix="uc1" TagName="manageAPILocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">

        <h1 class="page-header">Supplier API Data</h1>

        <uc1:manageAPILocation runat="server" ID="manageAPILocation" />

    </div>

</asp:Content>
