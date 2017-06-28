<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manage.aspx.cs" Inherits="TLGX_Consumer.activity.ckis.manage" %>

<%@ Register Src="~/controls/activity/ckis/manageCKISMaster.ascx" TagPrefix="uc1" TagName="manageCKISMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-header">Mangage</h1>

    <uc1:manageCKISMaster runat="server" id="manageCKISMaster" />

</asp:Content>
