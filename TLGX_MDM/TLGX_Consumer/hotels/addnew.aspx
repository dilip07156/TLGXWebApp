<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" EnableEventValidation="false" CodeBehind="addnew.aspx.cs" Inherits="TLGX_Consumer.hotels.addnew" %>

<%@ Register Src="~/controls/hotel/AddNew.ascx" TagPrefix="uc1" TagName="AddNew" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="page-header">Add New Hotel</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <div style="text-align: right;">
                            * Mandatory fields are marked as underlined
                        </div>
                    </div>
                </div>
            </div>
            <uc1:AddNew runat="server" ID="AddNew" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

