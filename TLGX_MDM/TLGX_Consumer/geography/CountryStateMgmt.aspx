<%@ Page Title="Manage Country" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CountryStateMgmt.aspx.cs" Inherits="TLGX_Consumer.geography.CountryStateMgmt" %>

<%@ Register Src="~/controls/geography/countryStateMgmt.ascx" TagPrefix="uc1" TagName="countryStateMgmt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:countryStateMgmt runat="server" id="countryStateMgmt" />
</asp:Content>
