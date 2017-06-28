﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation ="false" AutoEventWireup="true" CodeBehind="managekeywords.aspx.cs" Inherits="TLGX_Consumer.masters.managekeywords" %>

<%@ Register Src="~/controls/keywords/keywordManager.ascx" TagPrefix="uc1" TagName="keywordManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-header">Keyword Manager</h1>
    <uc1:keywordManager runat="server" id="keywordManager" />
</asp:Content>
