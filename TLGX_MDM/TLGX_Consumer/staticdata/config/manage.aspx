<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manage.aspx.cs" Inherits="TLGX_Consumer.staticdata.config.manage" %>

<%@ Register Src="~/controls/staticdataconfig/manageStaticDataConfig.ascx" TagPrefix="uc1" TagName="manageStaticDataConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<h1 class="page-header">Manage Mapping Config</h1>--%>
       <div class="row page-header">
       <div class="col-md-8">
                    <h1>Manage Mapping Config</h1>
                </div>
                <div class="col-md-4">
                    <div class="pull-right" style="margin-top: 25px !important;">
                        <asp:Button runat="server" ID="btnRedirectToSearch" onclick="btnRedirectToSearch_Click" CssClass="btn btn-link" Text="Go Back to MappingFileConfig Search Page" />
                    </div>
                </div>
           </div>
    <uc1:manageStaticDataConfig runat="server" id="manageStaticDataConfig" />

</asp:Content>
