<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="missingAttributeReports.aspx.cs" Inherits="TLGX_Consumer.hotels.missingAttributeReports" EnableEventValidation="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <style>
        #ctl00_MainContent_rvMissingAttributeReport_fixedTable {
            height: 100%;
            width: 100%;
        }
    </style>
    <script>
        $(document).ready(function () {

            $("a[title=PDF]").remove();
            $("a[title=Word]").remove();
            $("#ctl00_MainContent_rvMissingAttributeReport_ctl05_ctl04_ctl00_Menu > div").eq(1).remove();
            $("#ctl00_MainContent_rvMissingAttributeReport_ctl05_ctl04_ctl00_Menu > div").eq(2).remove();
        });
        
    </script>
    <!-- Heading -->
    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Missing Attribute Reports</h1>
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
                <div class="panel-body">
                    <div class="row">
                        <div class=" col-md-6">
                            <div class="form-group row">
                                <label for="txtFrom" class="control-label-mand col-sm-6">From </label>
                                <div class="col-sm-6">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" Text='<%# Bind("FromDate", "{0:dd/MM/yyyy}") %>' />
                                        <span class="input-group-btn">
                                            <button class="btn btn-default" type="button" id="iCalFrom">
                                                <span class="glyphicon glyphicon-calendar"></span>
                                            </button>
                                        </span>
                                    </div>
                                    <cc1:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy" PopupButtonID="iCalFrom"></cc1:CalendarExtender>
                                    <cc1:FilteredTextBoxExtender ID="axfte_txtFrom" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtFrom" />
                                </div>
                            </div>

                            <div class="form-group row">
                                <label for="txtTo" class="control-label-mand col-sm-6">To   </label>
                                <div class="col-sm-6">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" Text='<%# Bind("ToDate", "{0:dd/MM/yyyy}") %>' />
                                        <span class="input-group-btn">
                                            <button class="btn btn-default" type="button" id="iCalTo">
                                                <span class="glyphicon glyphicon-calendar"></span>
                                            </button>
                                        </span>
                                    </div>

                                    <cc1:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy" PopupButtonID="iCalTo"></cc1:CalendarExtender>
                                    <cc1:FilteredTextBoxExtender ID="axfte_txtTo" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtTo" />
                                </div>
                            </div>
                        </div>
                        <div id="errordiv" runat="server" class="col-md-6 alert alert-danger">
                            <p id="nulldate" runat="server"></p>
                            <p id="errorrange" runat="server"></p>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-4">
                                 <asp:Button ID="btnGenerateReport" runat="server" Text="View Report" OnClick="btnGenerateReport_Click" CssClass="btn btn-primary"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="container">
        <div class="row" style="width:100%; height:100%; overflow-x:scroll">
            <rsweb:ReportViewer ID="rvMissingAttributeReport" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" 
                WaitMessageFont-Size="14pt" Width="100%" Height="100%" AsyncRendering="False" SizeToReportContent="true" ZoomMode="FullPage" ShowFindControls="False">
                <LocalReport ReportPath="hotels\rptMissingAttribute.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </div>
    </div>
</asp:Content>

