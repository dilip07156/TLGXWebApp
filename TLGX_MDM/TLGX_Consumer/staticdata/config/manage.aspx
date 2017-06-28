<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manage.aspx.cs" Inherits="TLGX_Consumer.staticdata.config.manage" %>

<%@ Register Src="~/controls/staticdataconfig/manageStaticDataConfig.ascx" TagPrefix="uc1" TagName="manageStaticDataConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-header">Manage Mapping Config</h1>

    <uc1:manageStaticDataConfig runat="server" id="manageStaticDataConfig" />

</asp:Content>
