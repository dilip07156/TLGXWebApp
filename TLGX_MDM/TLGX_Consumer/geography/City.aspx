<%@ Page Title="City Mapping" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="City.aspx.cs" Inherits="TLGX_Consumer.geography.City" %>

<%@ Register Src="~/controls/geography/cityManager.ascx" TagPrefix="uc1" TagName="cityManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container page-header">
        <div class="col-lg-2 ">
            <h1>City Manager</h1>
        </div>
        <div class="col-lg-10 ">
            <div class="pull-right" style="margin-top: 25px !important;">
                <asp:Button runat="server" ID="btnRedirectToSearch" OnClick="btnRedirectToSearch_Click" CssClass="btn btn-link" Text="Go Back to Search Page" />

            </div>
        </div>

    </div>

    <uc1:cityManager runat="server" ID="cityManager" />

</asp:Content>
