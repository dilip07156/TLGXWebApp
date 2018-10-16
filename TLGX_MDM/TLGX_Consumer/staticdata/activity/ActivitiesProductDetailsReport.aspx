<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ActivitiesProductDetailsReport.aspx.cs" Inherits="TLGX_Consumer.staticdata.activity.ActivitiesProductDetailsReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">

        <h1 class="page-header">Export Activity Product Details</h1>

        <asp:UpdatePanel runat="server" ID="PnlUpdate">
            <ContentTemplate>
                <div class="panel panel-default">
                    <div class="panel-body">

                        <div class="form-group col-md-3">
                            <label class="control-label col-sm-4" for="ddlSupplierName">
                                Supplier Name
                            </label>
                            <div class="col-sm-8">
                                <asp:DropDownList runat="server" ID="ddlSupplierName" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">--All Suppliers--</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group col-md-3">
                            <label class="control-label col-sm-4" for="ddlCountry">
                                Country
                            </label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group col-md-3">
                            <label class="control-label col-sm-4" for="ddlCity">
                                City
                            </label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <asp:Button runat="server" Text="View Report" CssClass="btn btn-sm btn-primary" ID="btnViewReport" OnClick="btnViewReport_Click"></asp:Button>
                        </div>

                        <div id="report" runat="server" style="width: 100%; height: 100%; overflow-x: scroll">
                            <rsweb:ReportViewer ID="ReportViewerActivityProductDetails" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="100%" AsyncRendering="False" SizeToReportContent="true" ZoomMode="FullPage" ShowFindControls="False">
                                <LocalReport ReportPath="~/staticdata/activity/ActivitiesProductDetailsRDLCReport.rdlc">
                                    <DataSources>
                                        <rsweb:ReportDataSource Name="DsActivitiesProductDetails" />
                                    </DataSources>
                                </LocalReport>
                            </rsweb:ReportViewer>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>
