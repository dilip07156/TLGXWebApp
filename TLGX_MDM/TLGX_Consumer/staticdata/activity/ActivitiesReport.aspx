<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ActivitiesReport.aspx.cs" Inherits="TLGX_Consumer.staticdata.activity.ActivitiesReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:ScriptManagerProxy runat="server" ID="scriptmanagerproxy"></asp:ScriptManagerProxy>--%>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>

    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Export Activity Statistics</h1>
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="PnlUpdateDiv">
        <ContentTemplate>
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <label class="control-label" for="ddlSupplierName">Supplier Name</label>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList runat="server" ID="ddlSupplierName" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">--All Suppliers--</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                                <label class="control-label" for="ddlCountry">Country</label>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                    <asp:ListItem Text="-ALL-" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="-ALL Countries-" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                                <label class="control-label" for="ddlCity">City</label>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="-ALL-" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="-ALL Cities-" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                                <asp:Button runat="server" Text="View Report" CssClass="btn btn-sm btn-primary" ID="btnViewReport" OnClick="btnViewReport_Click"></asp:Button>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div id="dvMsgAlert" runat="server" data-auto-dismiss="2000" style="display: none"></div>
                        </div>

                        <div class="col-md-12" id="report" runat="server">
                            <div style="width: 100%; height: 100%">
                                <rsweb:ReportViewer ID="ReportVieweractivity" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="100%" AsyncRendering="False" SizeToReportContent="true" ZoomMode="FullPage" ShowFindControls="False">
                                    <LocalReport ReportPath="~/staticdata/activity/ActivitiesCountReport.rdlc">
                                        <DataSources>
                                            <rsweb:ReportDataSource Name="DsAcitivityCount" />
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

