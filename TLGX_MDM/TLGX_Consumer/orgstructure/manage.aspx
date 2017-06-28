<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manage.aspx.cs" Inherits="TLGX_Consumer.orgstructure.manage" %>

<%@ Register Src="~/controls/businessentities/manageOrgstructure.ascx" TagPrefix="uc1" TagName="manageOrgstructure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:manageOrgstructure runat="server" id="manageOrgstructure" />

</asp:Content>
