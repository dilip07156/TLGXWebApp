<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="manageFIleUpload.ascx.cs" Inherits="TLGX_Consumer.controls.staticdataconfig.manageFIleUpload" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script>
    function getChartData(fileid) {
        if (fileid != null && fileid != "") {
            //console.log(fileid);

            var colorarray = ["#007F00", "#faebd7"];
            var readarray = [];
            var txarray = [];
            var maparray = [];
            var ttfuarray = [];
            var matcharray = [];
            $.ajax({
                type: 'GET',
                url: '../../../Service/FileProgressDashboard.ashx?FileId=' + fileid,
                dataType: "json",
                success: function (result) {
                    $("#read").empty();
                    $("#map").empty();
                    $("#tx").empty();
                    $("#match").empty();
                    $("#ttfu").empty();
                    $('#tblveboselog').empty();
                    $("#tblstatastic").empty();
                    // debugger;
                    //process charts
                    for (var inode = 0; inode < result.ProgressLog.length; inode++) {
                        if (result.ProgressLog[inode].Step == "READ") {
                            var a = result.ProgressLog[inode].PercentageValue;
                            readarray.push({ label: "Completed", value: a });
                            if (a != 100) {
                                var b = (100 - a);
                                readarray.push({ label: "Remaining", value: b });
                            }
                        }
                        else if (result.ProgressLog[inode].Step == "TRANSFORM") {
                            var a = result.ProgressLog[inode].PercentageValue;
                            txarray.push({ label: "Completed", value: a });
                            if (a != 100) {
                                var b = (100 - a);
                                txarray.push({ label: "Remaining", value: b });
                            }
                            $("#currentbatch").text(result.ProgressLog[inode].CurrentBatch);
                            $("#totalbatch").text(result.ProgressLog[inode].TotalBatch);
                        }
                        else if (result.ProgressLog[inode].Step == "MAP") {
                            var a = result.ProgressLog[inode].PercentageValue;
                            maparray.push({ label: "Completed", value: a });
                            if (a != 100) {
                                var b = (100 - a);
                                maparray.push({ label: "Remaining", value: b });
                            }
                        }
                        else if (result.ProgressLog[inode].Step == "MATCH") {
                            var a = result.ProgressLog[inode].PercentageValue;
                            matcharray.push({ label: "Completed", value: a });
                            if (a != 100) {
                                var b = (100 - a);
                                matcharray.push({ label: "Remaining", value: b });
                            }
                            $("#Mcurrentbatch").html(result.ProgressLog[inode].CurrentBatch);
                            $("#Mtotalbatch").html(result.ProgressLog[inode].TotalBatch);
                        }
                        else {
                            var a = result.ProgressLog[inode].PercentageValue;
                            ttfuarray.push({ label: "Completed", value: a });
                            if (a != 100) {
                                var b = (100 - a);
                                ttfuarray.push({ label: "Remaining", value: b });
                            }
                        }
                    }
                    Morris.Donut({
                        element: 'read',
                        data: readarray,
                        colors: colorarray,
                        resize: true,
                        hideHover: "always"
                    });
                    Morris.Donut({
                        element: 'tx',
                        data: txarray,
                        colors: colorarray,
                        resize: true,
                        hideHover: "always"
                    });
                    Morris.Donut({
                        element: 'map',
                        data: maparray,
                        colors: colorarray,
                        resize: true,
                        hideHover: "always"
                    });
                    Morris.Donut({
                        element: 'match',
                        data: matcharray,
                        colors: colorarray,
                        resize: true,
                        hideHover: "always"
                    });
                    Morris.Donut({
                        element: 'ttfu',
                        data: ttfuarray,
                        colors: colorarray,
                        resize: true,
                        hideHover: "always"
                    });

                    //verbose log
                    for (var i = 0; i < result.VerboseLog.length; i++) {
                        var d = new Date(parseInt(result.VerboseLog[i].TimeStamp.substr(6)));
                        var date = d.toLocaleString("en-GB");
                        var tr;
                        tr = $('<tr/>');
                        tr.append("<td>" + date + "</td>");
                        tr.append("<td>" + result.VerboseLog[i].Step + "</td>");
                        tr.append("<td>" + result.VerboseLog[i].Message + "</td>");
                        $("#verboselog table").append(tr);
                        if (i == result.VerboseLog.length - 1) {
                            //timediff = new Date(parseInt(result.VerboseLog[0].TimeStamp.substr(6))) - new Date(parseInt(result.VerboseLog[i].TimeStamp.substr(6)));
                            //timediff /= 60000;
                            var end_actual_time = new Date(parseInt(result.VerboseLog[0].TimeStamp.substr(6)));
                            var start_actual_time = new Date(parseInt(result.VerboseLog[i].TimeStamp.substr(6)));
                            var diff = end_actual_time - start_actual_time;
                            var diffSeconds = diff / 1000;
                            var HH = Math.floor(diffSeconds / 3600);
                            var MM = Math.floor(diffSeconds % 3600) / 60;

                            var formatted = ((HH < 10) ? ("0" + HH) : HH) + ":" + ((MM < 10) ? ("0" + MM) : MM);
                            // alert(formatted);
                            debugger;
                            $("#MainContent_manageFIleUpload_frmViewDetailsConfig_lbltimeDiff").text(formatted);
                        }
                    }
                    //File Statistics
                    for (var i = 0; i < result.FileStatistics.length; i++) {
                        tr = $('<tr/>');
                        tr.append("<td>" + result.FileStatistics[i].TotalRows + "</td>");
                        tr.append("<td>" + result.FileStatistics[i].Mapped + "</td>");
                        tr.append("<td>" + result.FileStatistics[i].Unmapped + "</td>");
                        $("#dvstatastic table").append(tr);
                    }
                },
                error: function () {
                    //  debugger;
                    alert("Error fetching file processing data");
                },
            });
        }
    }

</script>
<script type="text/javascript">
    var x = setInterval(myTimer, 5000);
    function myTimer() {
        var d = new Date();
        var hdnval = document.getElementById("hdnFileId").value;
       // console.log(hdnval);
        getChartData(hdnval);
    }

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
        });
        $('#moViewDetials').on('hidden.bs.modal', function () {
            debugger;
            //  window.clearInterval(x);
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
    function OnClientUploadComplete() {
        var ddlSupplierList = document.getElementById("<%=ddlSupplierList.ClientID%>");
        var ddlEntityList = document.getElementById("<%=ddlEntityList.ClientID%>");
    }
</script>
<script src="../../Scripts/ChartJS/raphael-min.js"></script>
<script src="../../Scripts/ChartJS/morris.min.js"></script>
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

    @media(min-width: 992px) {
        .col5 {
            width: 20%;
            float: left;
            position: relative;
            min-height: 1px;
            padding-right: 15px;
            padding-left: 15px;
        }
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
                <h3>Mapping Details</h3>
            </div>


            <div class="col-lg-3">

                <div class="form-group pull-right">
                    <div class="input-group" runat="server" id="divDropdownForEntries">
                        <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                        <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>35</asp:ListItem>
                            <asp:ListItem>40</asp:ListItem>
                            <asp:ListItem>45</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>

            </div>

            <div class="col-lg-1">
                <asp:Button ID="btnNewUpload" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload" OnClientClick="showFileUpload();" OnClick="btnNewUpload_Click" />
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
                            EmptyDataText="No Mappings for search conditions" CssClass="table table-hover table-striped"
                            AutoGenerateColumns="false" OnPageIndexChanging="gvFileUploadSearch_PageIndexChanging"
                            OnRowCommand="gvFileUploadSearch_RowCommand" DataKeyNames="SupplierImportFile_Id,Supplier_Id" OnRowDataBound="gvFileUploadSearch_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="Supplier Name" DataField="Supplier" />
                                <asp:BoundField HeaderText="Entity" DataField="Entity" />
                                <asp:BoundField HeaderText="File Name" DataField="OriginalFilePath" />
                                <asp:BoundField HeaderText="Status" DataField="STATUS" />
                                <asp:BoundField HeaderText="Upload Date" DataField="CREATE_DATE" />

                                <%--<asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>' CssClass="btn btn-default" CommandArgument='<%# Bind("Accommodation_Description_Id") %>'>
                                         <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                                        <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                                    </asp:LinkButton>--%>

                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnProcess" runat="server" CausesValidation="false" CommandName="Process" CssClass='<%# Eval("STATUS").ToString() == "UPLOADED" ? "btn btn-default" : "btn btn-default disabled" %>'>
                                            <span aria-hidden="true"><%# Eval("STATUS").ToString() == "UPLOADED" ? "PROCESS" : "PROCESSED   " %></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnViewDetail" runat="server" CausesValidation="false" CommandName="ViewDetails" CssClass="btn btn-default" Enabled="true">
                                            <%--OnClientClick='<%# "showDetailsModal('\''"+ Convert.ToString(Eval("SupplierImportFile_Id")) + "'\'');" %>'--%>
      <%--OnClientClicking='<%#string.Format("showDetailsModal('{0}');",Eval("SupplierImportFile_Id ")) %>'                                            
                                           <%-- showDetailsModal('<%# Eval("SupplierImportFile_Id")%>');--%>
                                            
                                                 <span aria-hidden="true">View Details</span>
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
    <div class="modal-dialog modal-md">
        <div class="modal-content">

            <div class="modal-header">
                <div class="panel-title">
                    <h4 class="modal-title">File Upload</h4>
                </div>
            </div>

            <div class="modal-body">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="col-sm-12">
                            <div class="form-group row">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Upload" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                <div id="dvmsgUploadCompleted" runat="server" enableviewstate="false" style="display: none;">
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="form-group row">
                                <label class="control-label col-sm-4" for="ddlSupplierList">
                                    Supplier
                            <asp:RequiredFieldValidator ValidationGroup="Upload" Text="*" ID="rfvddlSupplierList" runat="server" ControlToValidate="ddlSupplierList"
                                CssClass="text-danger" ErrorMessage="The Suppplier is required." InitialValue="0" />
                                </label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlSupplierList" runat="server" OnSelectedIndexChanged="ddlSupplierList_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true">
                                        <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group row">
                                <label class="control-label col-sm-4" for="ddlEntityList">
                                    Entity
                              <asp:RequiredFieldValidator ValidationGroup="Upload" Text="*" runat="server" ControlToValidate="ddlEntityList"
                                  CssClass="text-danger" ID="rfvddlEntityList" ErrorMessage="The Entity is required." InitialValue="0" />
                                </label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlEntityList" runat="server" OnSelectedIndexChanged="ddlEntityList_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true">
                                        <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group row">
                                <label class="control-label col-sm-4" for="FileUpld">
                                    File Path
                                </label>
                                <div class="col-sm-8">
                                    <cc1:AjaxFileUpload Enabled="false" runat="server" ID="FileUpld" ClearFileListAfterUpload="true" OnClientUploadComplete="OnClientUploadComplete()"
                                        OnUploadComplete="FileUpld_UploadComplete" MaximumNumberOfFiles="1" Width="279px" />
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnNewReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" OnClick="btnNewReset_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</div>

<div class="modal fade" id="moViewDetials" role="dialog" data-backdrop="static" data-keyboard="false">
    <div class="modal-dialog modal-xl">
        <div class="modal-content">
            <div class="modal-header" style="padding: 5px 5px 5px 15px;">
                <h4 class="modal-title"><b>File Status </b><span>
                    <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button></span></h4>
                <input type="hidden" id="hdnFileId" name="hdnFileId" value="" />
            </div>
            <div class="modal-body">
                <div class="container">
                    <div class="row">
                        <%--<div class="col-md-12">--%>
                        <asp:UpdatePanel ID="pnlViewDetails" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdnViewDetailsFlag" runat="server" ClientIDMode="Static" Value="" EnableViewState="false" />
                                <asp:FormView ID="frmViewDetailsConfig" runat="server" DataKeyNames="SupplierImportFile_Id" OnItemCommand="frmViewDetailsConfig_ItemCommand" Width="1130px">
                                    <ItemTemplate>
                                        <div class="col-md-12">
                                            <div class="col-sm-2 col5">
                                                <label class="col-form-label">Supplier</label>
                                                <asp:Label ID="lblSupplier" CssClass="form-control" runat="server" Text='<%# Bind("Supplier") %>'></asp:Label>
                                            </div>
                                            <div class="col-sm-2 col5">
                                                <label class="col-form-label">Entity</label>
                                                <asp:Label ID="lblEntity" CssClass="form-control" runat="server" Text='<%# Bind("Entity") %>'></asp:Label>
                                            </div>
                                            <div class="col-sm-2 col5">
                                                <label class="col-form-label">File</label>
                                                <asp:Label ID="lblPath" CssClass="form-control" runat="server" Text='<%# Bind("OriginalFilePath") %>'></asp:Label>
                                            </div>
                                            <div class="col-sm-2 col5">
                                                <label class="col-form-label">Status</label>
                                                <asp:Label ID="lblstatus" CssClass="form-control" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                                            </div>
                                            <div class="col-sm-2 col5">
                                                <label class="col-form-label">Elapsed Time</label>
                                                <asp:Label ID="lbltimeDiff" CssClass="form-control" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:FormView>
                            </ContentTemplate>
                            <%--<Triggers>
                        <asp:PostBackTrigger ControlID="btnDownload" />
                    </Triggers>--%>
                        </asp:UpdatePanel>
                        <%--</div>--%>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-sm-2 " id="readdiv">
                                <div class="panel  panel-default " style="text-align: center">
                                    <div class="panel-heading" style="padding-top: 5px; padding-bottom: 5px">
                                        <i class="fa fa-bar-chart-o fa-fw"></i>
                                        <h4 style="margin-top: 5px"><b>READ</b></h4>
                                    </div>
                                    <div id="read" class="chartheight" style="height: 160px"></div>
                                </div>
                            </div>
                            <div class="col-sm-8">
                                <div class=" panel panel-default ">
                                    <div class="panel-heading" style="text-align: center; padding-top: 5px; padding-bottom: 5px">
                                        <i class="fa fa-bar-chart-o fa-fw"></i>
                                        <h4 style="margin-top: 5px"><b>BATCH &nbsp;:&nbsp;</b><span id="currentbatch" style="font: bold">0</span>/<span id="totalbatch" style="font: bold">0</span></h4>
                                    </div>
                                    <div class="panel-body" style="padding: 0px;">
                                        <div class="col-sm-4" id="txdiv" style="text-align: center">
                                            <i class="fa fa-bar-chart-o fa-fw"></i>
                                            <b>TRANSFORM</b>
                                            <div id="tx" class="chartheight"></div>
                                        </div>
                                        <div class="col-sm-4" id="mapdiv" style="text-align: center">
                                            <i class="fa fa-bar-chart-o fa-fw"></i>
                                            <b>MAP</b>
                                            <div id="map" class="chartheight"></div>
                                        </div>
                                        <div class="col-sm-4" id="ttfudiv" style="text-align: center">
                                            <i class="fa fa-bar-chart-o fa-fw"></i>
                                            <b>TTFU</b>
                                            <div id="ttfu" class="chartheight"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2 offset1" id="matchdiv" style="text-align: center">
                                <div class="panel  panel-default">
                                    <div class="panel-heading" style="padding-top: 5px; padding-bottom: 5px">
                                        <i class="fa fa-bar-chart-o fa-fw"></i>
                                        <h4 style="margin-top: 5px; text-align: left"><b>MATCH&nbsp;:&nbsp;</b><span id="Mcurrentbatch" style="font: bold"></span>/<span id="Mtotalbatch" style="font: bold"></span></h4>
                                    </div>
                                    <%-- <div class="panel-body" style="height: 190px;">--%>
                                    <div id="match" class="chartheight" style="height: 160px"></div>
                                    <%-- </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading" style="padding-top: 5px; padding-bottom: 5px">
                                        <h4 style="margin-top: 5px"><b>VERBOSE LOG</b></h4>
                                    </div>
                                    <div id="verboselog" style="overflow: scroll; height: 200px">
                                        <table class="table table-fixed table-condensed">
                                            <thead>
                                                <tr>
                                                    <th>Date</th>
                                                    <th>Step</th>
                                                    <th>Message</th>
                                                </tr>
                                            </thead>
                                            <tbody id="tblveboselog">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading" style="padding-top: 5px; padding-bottom: 5px">
                                        <h4 style="margin-top: 5px"><b>File Statistic</b></h4>
                                    </div>
                                    <div id="dvstatastic" style="height: 50px">
                                        <table class="table table-fixed table-condensed">
                                            <thead>
                                                <tr>
                                                    <th>TotalRows</th>
                                                    <th>Mapped</th>
                                                    <th>Unmapped</th>
                                                </tr>
                                            </thead>
                                            <tbody id="tblstatastic">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%--<asp:LinkButton ID="btnPrevious" runat="server" Enabled="false" Visible="false" CssClass="btn btn-default" CommandName="Previous">
                                         <span aria-hidden="true" class="glyphicon glyphicon-arrow-left"></span>
                                </asp:LinkButton>

                                <asp:LinkButton ID="btnNext" runat="server" Enabled="false" Visible="false" CssClass="btn btn-default pull-right" CommandName="Next">
                                         <span aria-hidden="true" class="glyphicon glyphicon-arrow-right"></span>
                                </asp:LinkButton>
                                <asp:Label ID="lblTotalCount" runat="server"></asp:Label>
                                <asp:Repeater ID="rptrErrorLog" runat="server">
                                    <HeaderTemplate>
                                        <table class="table table-bordered table-striped">
                                            <th>Error Date</th>
                                            <th>Error Details</th>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <tr>
                                            <td><span><%# Eval("Error_DATE") %></span></td>

                                            <td>
                                                <table>
                                                    <tr class="tablestyle">
                                                        <td><b>Error Code: </b></td>
                                                        <td><span><%# Eval("ErrorCode") %></span></td>
                                                    </tr>
                                                    <tr class="tablestyle">
                                                        <td><b>Error Description: </b></td>
                                                        <td style="word-wrap: break-word;"><span><%# Eval("ErrorDescription") %></span></td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>Error Type: </b></td>
                                                        <td><span><%# Eval("ErrorType") %></span></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                                </div>
                                </div>--%>

                <%--                <div class="form-group row">
                    <div class="col-sm-4">
                        <asp:Button ID="btnArchive" CssClass="btn btn-primary btn-sm" runat="server" Text="Archive File" CommandName="Archive" />
                        <asp:Button ID="btnDownload" CssClass="btn btn-primary btn-sm" runat="server" Text="Export To CSV" Visible="false" CommandName="Download" />
                    </div>
                </div>--%>
            </div>
            <%-- <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>--%>
        </div>
    </div>
    <script>
      
    </script>
</div>




