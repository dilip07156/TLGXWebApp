<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="rollOffReports.aspx.cs" Inherits="TLGX_Consumer.hotels.rollOffReports" EnableEventValidation="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        function validatedate() {
            $("#dateerror").hide();
            var startDate = new Date($('#MainContent_fromDate').val());
            var endDate = new Date($('#MainContent_toDate').val());
            days = (endDate - startDate) / (1000 * 60 * 60 * 24);
            if (days > 90 || startDate > endDate) {
                $("#dateerror").show();
                $("#dateerror").append("Please select date greater than From Date and between 90 days..!!!")
                $("#MainContent_btnRuleCsv").css("pointer-events", "none");
                $("#MainContent_btnStatusCsv").css("pointer-events", "none");
                $("#MainContent_btnUpdateCsv").css("pointer-events", "none");
            }

        }
    </script>
    <div class="row">
            <div class="col-md-12">
                <h1 class="page-header">ROLL OFF REPORTS</h1>
            </div>
   </div>
    <div class="row">
        <div class=" col-md-6">
            <div class="form-group">
                <label for="txtFrom" class="control-label-mand col-sm-6"> From </label>
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

            <div class="form-group">
                <label for="txtTo" class="control-label-mand col-sm-6"> To   </label>
                <div class="col-sm-6">
                    <div class="input-group">
                        <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" Text='<%# Bind("ToDate", "{0:dd/MM/yyyy}") %>'  />
                        <span class="input-group-btn">
                            <button class="btn btn-default" type="button" id="iCalTo">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </button>
                        </span>
                    </div>

                    <cc1:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy" PopupButtonID="iCalTo" ></cc1:CalendarExtender>
                    <cc1:FilteredTextBoxExtender ID="axfte_txtTo" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtTo" />
                </div>
            </div>
        </div>
        <div id="errordiv" runat="server" class="col-md-6 alert alert-danger" >
            <p id="nulldate" runat="server"></p>
            <%--<p id="errorfromdate" runat="server" ></p>
            <p id="errortodate" runat="server"></p>--%>
            <p id="errorrange" runat="server"></p>
        </div>
    </div>
     <br />
    <div class="row">
        <div class="col-sm-4">
            <asp:Label ID="lblrule" runat="server" Text="Rules report" CssClass="font-weight: bold; "></asp:Label>&nbsp:&nbsp
                 <%--<asp:Button runat="server" Text="" ></asp:Button>--%>
            <asp:Button runat="server" Text="View Status" ID="btnRuleCsv" OnClick="btnRuleCsv_Click" CssClass="btn btn-primary "></asp:Button>
        </div>
        <div class="col-sm-4">
            <asp:Label ID="lblstatus" runat="server" Text="Status report" CssClass="font-weight: bold; "></asp:Label>&nbsp:&nbsp
                 <%--<asp:Button runat="server" Text="" ></asp:Button>--%>
            <asp:Button runat="server" Text="View Status" ID="btnStatusCsv" OnClick="btnStatusCsv_Click" CssClass="btn btn-primary"></asp:Button>
        </div>
        <div class="col-sm-4">
            <asp:Label ID="lblupdate" runat="server" Text="Updates report" CssClass="font-weight: bold; "></asp:Label>&nbsp:&nbsp
                 <%--<asp:Button runat="server" Text="" ></asp:Button>--%>
            <asp:Button runat="server" Text="View Status" ID="btnUpdateCsv" OnClick="btnUpdateCsv_Click" CssClass="btn btn-primary"></asp:Button>
        </div>
    </div>
    <hr />
    <div class="row col-md-12">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="100%" AsyncRendering="False" SizeToReportContent="True">
            <LocalReport ReportPath="hotels\rptRuleReport.rdlc">
            </LocalReport>

        </rsweb:ReportViewer>
    </div>
</asp:Content>
