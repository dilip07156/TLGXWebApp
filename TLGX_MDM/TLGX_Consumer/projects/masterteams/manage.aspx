<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manage.aspx.cs" Inherits="TLGX_Consumer.projects.masters.projectteams" %>

<%@ Register Src="~/controls/projects/manageMasterTeam.ascx" TagPrefix="uc1" TagName="manageMasterTeam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-header"></h1>       
        <uc1:manageMasterTeam runat="server" id="manageMasterTeam" />
</asp:Content>
