<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manage.aspx.cs" Inherits="TLGX_Consumer.projects.manage" %>

<%@ Register Src="~/controls/projects/manageProjects.ascx" TagPrefix="uc1" TagName="manageProjects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-header">Manage Project</h1>

    <uc1:manageProjects runat="server" id="manageProjects" />



</asp:Content>
