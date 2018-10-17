<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CountryWiseDashBoardReport.aspx.cs" Inherits="TLGX_Consumer.staticdata.CountryWiseDashBoardReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Country Wise Dashboard report</h1>
        </div>
    </div>

    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Filters</a>
                </h4>
            </div>
            <div id="collapseSearch" class="panel-collapse collapse in" aria-expanded="true">
                <asp:UpdatePanel runat="server" ID="PnlUpdateDiv">
                    <ContentTemplate>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlRegion">
                                            Region
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlCountry">
                                            Country
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-4">
                                    <asp:Button runat="server" Text="View Report" ID="btnviewreport" CssClass="btn btn-primary " OnClick="btnviewreport_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>

    <div class="container">
        <div style="width: 100%; height: 100%">
        <%--    <rsweb:ReportViewer ID="CountryReportViewer" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="100%" AsyncRendering="False" SizeToReportContent="True" ZoomMode="FullPage" ShowFindControls="False">
                <LocalReport ReportPath="staticdata\rptCountry_NewDashreport.rdlc">

                </LocalReport>
            </rsweb:ReportViewer>--%>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="staticdata\rptCountry_NewDashreport.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </div>
    </div>


</asp:Content>
