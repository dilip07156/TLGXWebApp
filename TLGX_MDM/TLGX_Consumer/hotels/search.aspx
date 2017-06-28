<%@ Page Title="Hotel Search" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="search.aspx.cs" Inherits="TLGX_Consumer.hotels.search" %>


<%@ Register Src="~/controls/hotel/searchHotelascx.ascx" TagPrefix="uc1" TagName="searchHotelascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-header">Hotel Search</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc1:searchHotelascx runat="server" ID="searchHotelascx" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
