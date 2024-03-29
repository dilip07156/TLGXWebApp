﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="manageFIleUpload.ascx.cs" Inherits="TLGX_Consumer.controls.staticdataconfig.manageFIleUpload" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/controls/staticdataconfig/FileMappingcharts.ascx" TagPrefix="uc1" TagName="FileMappingcharts" %>

<style>
    .h_iframe {
        height: 100%;
        width: 100%;
    }
</style>

<script type="text/javascript">

    function myTimer() {
        var d = new Date();
        var hdnval = document.getElementById("hdnFileId").value;
        //alert(hdnval);
        getChartDataFileMapping(hdnval);
    }
    function myStopFunction() {
        clearInterval(x);
    }
    //end
    function showFileUpload() {
        $("#moFileUpload").modal('show');
    }
    function closeFileUpload() {
        $("#moFileUpload").modal('hide');
    }
    function showDetailsModal(fileid) {
        $("#moViewDetials").modal('show');
        $('#moViewDetials').one('shown.bs.modal', function () {
            document.getElementById("hdnFileId").value = fileid;
            var filestatus = $("#lblstatus").text();
            getChartDataFileMapping(fileid);
            //strat timer
            x = setInterval(function () { myTimer() }, 15000);
        }
        );
        $('#moViewDetials').on('hidden.bs.modal', function () {
            //stop timer on close of modal 
            $('#moViewDetials a:first').tab('show');
            myStopFunction();
        });
    }

    function closeDetailsModal() {
        $("#moViewDetials").modal('hide');
    }

    function pageLoad(sender, args) {
        var hdnViewDetailsFlag = $('#<%=hdnViewDetailsFlag.ClientID%>').val();

        if (hdnViewDetailsFlag == "true") {
            closeDetailsModal();
        }
        $('#hdnViewDetailsFlag').val("false");
    }

</script>
<%--<script src="../../Scripts/ChartJS/raphael-min.js"></script>
<script src="../../Scripts/ChartJS/morris.min.js"></script>--%>

<style>
    .morris-hover {
        opacity: 0;
    }

    .tablestyle {
        border-bottom: 1px solid #dddddd;
    }

    @media (min-width: 768px) {
        .modal-xl {
            width: 80%;
            max-width: 1200px;
        }
    }

    .chartheight {
        height: 150px;
    }

    @media(min-width: 768px) {
        .col5 {
            width: 20%;
            float: left;
            position: relative;
            min-height: 1px;
            padding-right: 15px;
            padding-left: 15px;
        }
    }

    .TextBoxStyle {
        text-align: left;
        border-color: black;
        border-width: 1px;
        border-style: solid;
        font-family: Calibri;
        font-size: 14px;
    }
</style>

<asp:UpdatePanel ID="updUserGrid" runat="server">
    <ContentTemplate>
        <div class="panel-group" id="accordion">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">File Upload Search</a>
                    </h4>
                </div>

                <div id="collapseSearch" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:ValidationSummary ID="vlSumm" runat="server" ValidationGroup="vldgrpFileSearch" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                            </div>
                        </div>
                        <div class="container">
                            <div class="row">

                                <div class="col-sm-6">
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlSupplierName">Supplier </label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlMasterCountry">Entity</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlMasterCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlStatus">Status</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-sm-6">

                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="txtFrom">From</label>
                                        <div class="col-sm-7">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" />
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button" id="iCalFrom">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </button>
                                                </span>
                                                <cc1:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy" PopupButtonID="iCalFrom"></cc1:CalendarExtender>
                                                <cc1:FilteredTextBoxExtender ID="axfte_txtFrom" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtFrom" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="txtTo">To</label>
                                        <div class="col-sm-7">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" />
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button" id="iCalTo">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </button>
                                                </span>

                                            </div>
                                            <cc1:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy" PopupButtonID="iCalTo"></cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="axfte_txtTo" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtTo" />
                                            <asp:CompareValidator ID="vldCmpDateFromTo" runat="server" ErrorMessage="To date can't be less than from date." ControlToCompare="txtFrom" CultureInvariantValues="true" ControlToValidate="txtTo" ValidationGroup="vldgrpFileSearch" Text="*" CssClass="text-danger" Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" ValidationGroup="vldgrpFileSearch" />
                                            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" OnClick="btnReset_Click" />
                                        </div>
                                        <div class="col-sm-12">&nbsp; </div>
                                    </div>

                                </div>
                            </div>

                        </div>
                    </div>
                </div>

            </div>
        </div>

        <div class="row">

            <div class="col-lg-8">
                <h3>File Details</h3>
            </div>


            <div class="col-lg-3">

                <div class="form-group pull-right">
                    <div class="input-group" runat="server" id="divDropdownForEntries">
                        <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                        <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged">
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="col-lg-1">
                <asp:Button ID="btnNewUpload" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload" OnClientClick="showFileUpload();" />
            </div>
        </div>

        <div class="panel-group" id="searchResult">
            <div class="panel panel-default">

                <div class="panel-heading clearfix">
                    <h4 class="panel-title pull-left">
                        <a data-toggle="collapse" data-parent="#searchResult" href="#collapseSearchResult">Search Results (Total Count:
                            <asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label>)</a>
                    </h4>
                </div>

                <div id="collapseSearchResult" class="panel-collapse collapse in">
                    <div class="panel-body">

                        <div class="row">
                            <div id="dvMsg" runat="server" enableviewstate="false" style="display: none;">
                            </div>
                        </div>

                        <asp:GridView ID="gvFileUploadSearch" runat="server" AllowPaging="True" AllowCustomPaging="true"
                            EmptyDataText="No Files found for search conditions" CssClass="table table-hover table-striped"
                            AutoGenerateColumns="false" OnPageIndexChanging="gvFileUploadSearch_PageIndexChanging" PagerSettings-Position="TopAndBottom"
                            OnRowCommand="gvFileUploadSearch_RowCommand" DataKeyNames="SupplierImportFile_Id,Supplier_Id" OnRowDataBound="gvFileUploadSearch_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="Supplier Name" DataField="Supplier" />
                                <asp:BoundField HeaderText="Entity" DataField="Entity" />
                                <asp:BoundField HeaderText="File Name" DataField="OriginalFilePath" />
                                <asp:TemplateField ShowHeader="true" HeaderText="Status" ItemStyle-Width="175px">
                                    <ItemTemplate>
                                        <div class="form-inline">
                                            <div class="form-group">
                                                <img style="height: 25px; width: 25px" src='<%# Eval("STATUS").ToString() == "PROCESSED" ? "../../images/148767.png" : (Eval("STATUS").ToString() == "ERROR" ? "../../images/148766.png" : ((Eval("STATUS").ToString() == "UPLOADED" || Eval("STATUS").ToString() == "SCHEDULED" || Eval("STATUS").ToString() == "NEW") ? "../../images/148764.png" : "../../images/148853.png")) %>' />
                                                <label><%# Eval("STATUS").ToString() %></label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField HeaderText="Upload Date" DataField="CREATE_DATE" DataFormatString="{0:dd/MM/yyyy hh:mm:ss tt}" />

                                <%--<asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnViewDetail" runat="server" CausesValidation="false" CommandName='<%# Eval("STATUS").ToString() == "UPLOADED" ? "Process" : "ViewDetails" %>' 
                                            CssClass="btn btn-default" Enabled="true">
                                                 <span aria-hidden="true"><%# Eval("STATUS").ToString() == "UPLOADED" ? "PROCESS" : "View Details" %></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnViewDetail" runat="server" CausesValidation="false" CommandName='<%# Eval("STATUS").ToString() == "UPLOADED" ? "Process" : "ViewDetails" %>'
                                            CssClass="btn btn-default" Enabled="true">
                                                 <span aria-hidden="true"><%# ((Eval("STATUS").ToString() == "UPLOADED" && Eval("IsActive").ToString()=="True")  ? "PROCESS" : "ViewDetails") %></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ShowHeader="false" HeaderStyle-CssClass="Info">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                                            CssClass="btn btn-default" CommandArgument='<%# Bind("SupplierImportFile_Id") %>'>
                                                    <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat" %>'></span>
                                                    <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                    </div>
                </div>

            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>

<div class="modal fade" id="moFileUpload" role="dialog">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <div class="panel-title">
                    <h4 class="modal-title">File Upload</h4>
                </div>
            </div>
            <div class="modal-body">
                <iframe class="col-md-12" style="min-height: 500px;" frameborder="0" src="~/staticdata/files/StaticFileupload.aspx" runat="server"></iframe>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</div>

<div class="modal fade" id="moViewDetials" role="dialog" data-backdrop="static" data-keyboard="false">
    <div class="modal-dialog modal-xl">
        <div class="modal-content">
            <div class="modal-header" style="padding: 5px 5px 5px 15px;">
                <h4 class="modal-title"><b>File Status </b></h4>
                <%--  <input type="hidden" id="hdnFileId" name="hdnFileId" value="" />--%>
                <asp:HiddenField ID="hdnFileId" Value="" runat="server" ClientIDMode="Static" />
            </div>
            <div class="modal-body">

                <asp:HiddenField ID="hdnViewDetailsFlag" runat="server" ClientIDMode="Static" Value="" EnableViewState="false" />
                <uc1:FileMappingcharts runat="server" ID="FileMappingcharts" />

            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
