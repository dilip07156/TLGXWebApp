<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExportSupplierReport.aspx.cs" Inherits="TLGX_Consumer.staticdata.ExportSupplierReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Export Supplier Static Data Mapping Report</h1>
        </div>
    </div>

    <asp:UpdatePanel runat="server" ID="PnlUpdateDiv">
        <ContentTemplate>
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-body">

                        <div class="col-md-6">

                            <div class="form-group row">
                                <label class="control-label col-sm-4" for="ddlAccoPriority">
                                    Accomodation Priority
                                </label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlAccoPriority" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group row">
                                <label class="control-label col-sm-4" for="chkIsMDMDataOnly">MDM Acco Data Only</label>
                                <div class="col-sm-8">
                                    <asp:CheckBox ID="chkIsMDMDataOnly" runat="server" />
                                </div>
                            </div>

                            <div class="form-group row">
                                <label class="control-label col-sm-4" for="ddlSupplierPriority">Supplier Priority</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList runat="server" ID="ddlSupplierPriority" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">--All Priority--</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group row">
                                <label class="control-label col-sm-4" for="ddlSupplierName">Suppliers</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList runat="server" ID="ddlSupplierName" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">--All Suppliers--</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>


                            <div class="form-group row">
                                <div class="col-sm-4">
                                    <asp:Button runat="server" Text="View Report" CssClass="btn btn-sm btn-primary" ID="btnViewReport" OnClick="btnViewReport_Click"></asp:Button>
                                </div>
                            </div>

                        </div>

                        <div class="col-md-12" id="report">
                            <div style="width: 100%; height: 100%; overflow-x: scroll">
                                <rsweb:ReportViewer ID="ExportSupplierDetailsReport" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="100%" AsyncRendering="False" SizeToReportContent="true" ZoomMode="FullPage" ShowFindControls="False">
                                    <LocalReport ReportPath="staticdata/ExportSupplierRDLCReport.rdlc">
                                        <DataSources>
                                            <rsweb:ReportDataSource Name="DsExportSupplierReport" />
                                        </DataSources>
                                    </LocalReport>
                                </rsweb:ReportViewer>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
