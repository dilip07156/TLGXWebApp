<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ActivitiesReport.aspx.cs" Inherits="TLGX_Consumer.staticdata.activity.ActivitiesReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:ScriptManagerProxy runat="server" ID="scriptmanagerproxy"></asp:ScriptManagerProxy>--%>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>

    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Export Activity Count</h1>
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="PnlUpdateDiv">
        <ContentTemplate>
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="col-md-8">
                            <div class="col-md-2">
                                <label>Report Type</label>
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlReportType" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--Select Report--</asp:ListItem>
                                    <asp:ListItem>Activities Count By Country</asp:ListItem>
                                    <asp:ListItem>Activities Count By City</asp:ListItem>
                                    <asp:ListItem Selected="True">Activities Count By Supplier</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <asp:Button runat="server" Text="View Report" CssClass="btn btn-sm btn-primary" ID="btnViewReport" OnClick="btnViewReport_Click"></asp:Button>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div id="dvMsgAlert" runat="server" data-auto-dismiss="2000" style="display: none"></div>
                        </div>

                        <div class="col-md-12" id="report" runat="server">
                            <div style="width: 100%; height: 100%">
                                <rsweb:ReportViewer ID="ReportVieweractivity" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="100%" AsyncRendering="False" SizeToReportContent="true" ZoomMode="FullPage" ShowFindControls="False">
                                    <LocalReport ReportPath="staticdata/activity/ActivitiesCountReport.rdlc">
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

