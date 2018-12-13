<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="TLGX_Consumer.staticdata.config.search" %>

<%@ Register Src="~/controls/staticdataconfig/searchStaticDataConfig.ascx" TagPrefix="uc1" TagName="searchStaticDataConfig" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1 class="page-header">Mapping File Config</h1>

        <uc1:searchStaticDataConfig runat="server" ID="searchStaticDataConfig" />
    </div>
</asp:Content>
