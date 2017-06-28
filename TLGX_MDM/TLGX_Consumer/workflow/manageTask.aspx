<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manageTask.aspx.cs" Inherits="TLGX_Consumer.workflow.manageTask" %>

<%@ Register Src="~/controls/workflow/managetask.ascx" TagPrefix="uc1" TagName="managetask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="page-header">Manage Task</h1>

    <uc1:managetask runat="server" id="managetask" />
</asp:Content>
