<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TLGX_Consumer._Default" %>

<%@ Register Src="~/controls/staticdata/allSupplierDataChart.ascx" TagPrefix="uc1" TagName="allSupplierDataChart" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            <div>
                <h1 class="page-header">TraveLogix Data Management</h1>
                <asp:Image ID="Image1" Height="100" runat="server" ImageUrl="~/Content/cnklogo.jpg" />
                <asp:Image ID="Image2" Height="100" runat="server" ImageUrl="~/Content/ez1logo.jpg" />
                <br />
                <br />
                <div class="well">Please log in to access the application.</div>
            </div>

        </AnonymousTemplate>

        <LoggedInTemplate>
            <div class="container">
                <uc1:allSupplierDataChart runat="server" ID="allSupplierDataChart" />
            </div>
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>
