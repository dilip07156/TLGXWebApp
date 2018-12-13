<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SupplierTaskLog.ascx.cs" Inherits="TLGX_Consumer.controls.businessentities.SupplierTaskLog" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/controls/businessentities/supplierStaticDownloadData.ascx" TagPrefix="uc1" TagName="supplierStaticDownloadData" %>

<style type="text/css">
    .AlgRgh {
        text-align: right;
        font-family: Verdana, Arial, Helvetica, sans-serif;
    }
    .x-lg {
          width: 80%;          
        }
</style>

<%--<script src="../../Scripts/Cron/Moment.min.js"></script>
<script src="../../Scripts/Cron/later.min.js"></script>
<script src="../../Scripts/Cron/prettycron.js"></script>--%>
<script type="text/javascript">
    $(document).ready(function () {
        //var test = prettyCron.toString("37 10 * * *");
    });

    //Start View Details.
    function showDetailsModal(pentahoid, name, entity, status, path) {
        $("#moViewDetials").modal('show');
        $('#moViewDetials').one('shown.bs.modal', function () {
            $("#MainContent_SupplierTaskLog_txtSupplier").val(name);
            $("#MainContent_SupplierTaskLog_txtEntity").val(entity);
            $("#MainContent_SupplierTaskLog_txtPath").val(path);
            $("#MainContent_SupplierTaskLog_txtStatus").val(status);
            document.getElementById("hdnPentahoid").value = pentahoid;
            getviewDetailsData(pentahoid);
            //start timer
            // timer = setInterval(function () { myTimer() }, 3000);

        });
        //$('#moViewDetials').on('hidden.bs.modal', function () {
        //    $("#tblsteps").empty();
        //    //stop timer on close of modal 
        //    myStopFunction();
        //});
    }

    function getviewDetailsData(pentahoid) {
        if (pentahoid != null && pentahoid != "") {
            $.ajax({
                type: 'GET',
                url: '../../../Service/PentahoStepsInfo.ashx?Pentahoid=' + pentahoid,
                dataType: "json",
                success: function (result) {
                    if (result != null) {
                        $("#tblsteps").empty();
                        for (var i = 0; i < result.Stepstatuslist.Stepstatus.length; i++) {
                            var a = result.Stepstatuslist.Stepstatus[i];
                            var tr;
                            tr = $('<tr/>');
                            if (a.StatusDescription == "Running")
                                tr.addClass("warning");
                            else if (a.StatusDescription == "Finished")
                                tr.addClass("success");
                            else if (a.StatusDescription == "Stopped")
                                tr.addClass("danger");
                            else
                                tr.removeClass();

                            tr.append("<td>" + a.Stepname + "</td>");
                            tr.append("<td>" + a.Copy + "</td>");
                            tr.append("<td>" + a.LinesRead + "</td>");
                            tr.append("<td>" + a.LinesWritten + "</td>");
                            tr.append("<td>" + a.LinesInput + "</td>");
                            tr.append("<td>" + a.LinesOutput + "</td>");
                            tr.append("<td>" + a.LinesUpdated + "</td>");
                            tr.append("<td>" + a.LinesRejected + "</td>");
                            tr.append("<td>" + a.Errors + "</td>");
                            tr.append("<td>" + a.StatusDescription + "</td>");
                            tr.append("<td>" + a.Seconds + "</td>");
                            tr.append("<td>" + a.Speed + "</td>");
                            tr.append("<td>" + a.Priority + "</td>");
                            $("#steps table").append(tr);
                        }
                    }
                    else {

                    }
                },
                error: function () {
                },
            });
        }
    }

    function showFileUpload(suppilerid, entity) {
        var url = "/staticdata/files/StaticFileupload.aspx?Supplier_Id=" + suppilerid + "&entity=" + entity;
        $('iframe').prop('src', url);
        $("#moFileUpload").modal('show');
    }
    
    function showInstruction() {       
        //var url = "/suppliers/SupplierStaticDownloadData.aspx?Supplier_Id=" + suppilerid;       
        //$('iframe').prop('src', url);
        $("#moDownloadInstruction").modal('show');
    }

    function closeFileUpload() {
        $("#moFileUpload").modal('hide');
    }

    function showlogModel(TaskId) {
        $("#mologViewDetials").modal('show');
        var Tasks = [];

        $.ajax({
            type: 'GET',
            url: '../../../Service/GetTaskLogs.ashx?TaskId=' + TaskId,
            dataType: "json",
            async: false,
            success: function (result) {

                if (result != null) {
                    $("#tbllogsteps").empty();
                    for (var i = 0; i < result.length; i++) {
                        var a = result[i];
                        var tr;
                        tr = $('<tr/>');
                        tr.append("<td>" + a.StatusMessage + "</td>");
                        tr.append("<td>" + a.LogType + "</td>");
                        tr.append("<td>" + a.Remarks + "</td>");
                        tr.append("<td>" + convert(new Date(parseInt(a.CreateDate.replace("/Date(", "").replace(")/"))).toString()) + "</td>");
                        $("#logsteps table").append(tr);
                    }
                }
                else {

                }
            },
            error: function () {
            },
        });


    }

    function convert(str) {

        var date = new Date(str),
            mnth = ("0" + (date.getMonth() + 1)).slice(-2),
            day = ("0" + date.getDate()).slice(-2);
        return [date.getFullYear(), mnth, day].join("-");
    }

</script>

<!DOCTYPE html>
<html lang="en">

<head>
    <title></title>
</head>

<body>
    <br>
    <div class="container">       
        <h1>Pending Static Data File Handling</h1>
        <br>
        <div class="card">
            <div class="card-header">
                Search Tasks
            </div>

            <%--<div class="card-body">
                <div class="row">
                    <div class="col-6">
                        <div class="form-group row">
                            <label for="ddlSupplierName">Supplier</label>
                            <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group row">
                            <label for="ddlEntity">Entity</label>
                            <asp:DropDownList ID="ddlEntity" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group row">
                            <label for="ddlstatus">Status</label>
                            <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                <asp:ListItem Text="---Active---" Value="1"></asp:ListItem>
                                <asp:ListItem Text="---Completed---" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group row">
                            <label for="dtFrom">From </label>
                            <input type="date" class="form-control" id="dtFrom" placeholder="name@example.com" runat="server">
                        </div>
                        <div class="form-group row">
                            <label for="dtTo">To </label>
                            <input type="date" class="form-control" id="dtTo" placeholder="name@example.com" runat="server">
                        </div>
                        <br>
                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" />
                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnReset_Click" Text="Reset" />

                    </div>
                </div>
            </div>--%>

            <asp:UpdatePanel runat="server" ID="Pnlupdatesearch" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Filters</a>
                                    </h4>
                                </div>
                                <div id="collapseSearch" class="panel-collapse collapse in" aria-expanded="true">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label for="ddlSupplierName" class="control-label-mand ">Select Supplier </label>
                                                        <asp:DropDownList runat="server" ID="ddlSupplierName" CssClass="form-control" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">--All Suppliers--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label for="ddlEntity">Entity</label>
                                                        <asp:DropDownList ID="ddlEntity" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label for="ddlstatus">Status</label>
                                                    <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                        <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                                                        <asp:ListItem Text="Error" Value="Error"></asp:ListItem>
                                                        <asp:ListItem Text="Running" Value="Running"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label for="txtFrom" class="control-label-mand ">Date From </label>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" Width="240px" />
                                                            <span class="input-group-btn" style="display: block">
                                                                <button class="btn btn-default" type="button" id="iCalFrom" runat="server">
                                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                                </button>
                                                            </span>

                                                        </div>
                                                        <cc1:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy" PopupButtonID="iCalFrom"></cc1:CalendarExtender>
                                                        <cc1:FilteredTextBoxExtender ID="axfte_txtFrom" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtFrom" />
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label for="txtTo" class="control-label-mand">Date To   </label>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" Width="240px" />
                                                            <span class="input-group-btn" style="display: block">
                                                                <button class="btn btn-default" type="button" id="iCalTo" runat="server">
                                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                                </button>
                                                            </span>
                                                        </div>

                                                        <cc1:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy" PopupButtonID="iCalTo"></cc1:CalendarExtender>
                                                        <cc1:FilteredTextBoxExtender ID="axfte_txtTo" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtTo" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="col-md-12"></div>
                                                <div class="col-md-12">
                                                    <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" />
                                                    <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnReset_Click" Text="Reset" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <br>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-4">
                                <div class="form-group">
                                    <label for="exampleFormControlSelect1">page Size</label>
                                    <asp:DropDownList ID="ddlShowEntries" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged">
                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>


        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="panel-group" id="accordionResult">
                    <div class="panel panel-default">
                        <div class="panel-heading clearfix">
                            <h4 class="panel-title pull-left">
                                <a data-toggle="collapse" data-parent="#accordionResult" href="#collapseSearchResult">Search Results (Total Count:
                                    <asp:Label ID="lblTotalCount" runat="server" Text="0"></asp:Label>)</a></h4>
                        </div>
                        <div id="msgAlert" runat="server" style="display: none;"></div>
                        <div id="collapseSearchResult" class="panel-collapse collapse in">
                            <div class="panel-body">
                                <asp:GridView ID="grdSupplierScheduleTask" runat="server" AllowPaging="True" AllowCustomPaging="true" AutoGenerateColumns="False"
                                    EmptyDataText="No Data for search conditions" CssClass="table table-hover"
                                    OnPageIndexChanging="grdSupplierScheduleTask_PageIndexChanging" DataKeyNames="Suppllier_ID,LogType,Status,Pentahocall_id,ApiPath,APIStatus"
                                    OnRowCommand="grdSupplierScheduleTask_RowCommand" OnDataBound="grdSupplierScheduleTask_DataBound" OnRowDataBound="grdSupplierScheduleTask_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="SuppllierName" HeaderText="Supplier Name" SortExpression="SupplierName" />
                                        <asp:BoundField DataField="Entity" HeaderText="Entity" SortExpression="Entity" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                        <asp:BoundField DataField="ScheduledDate" HeaderText="Scheduled Date" SortExpression="ScheduledDate" />
                                        <asp:BoundField DataField="PendingFordays" HeaderText="Pending For Days" SortExpression="Status" />
                                        <asp:BoundField DataField="LogType" HeaderText="Job Type" SortExpression="SupplierName" />

                                        <asp:TemplateField ShowHeader="true" HeaderText="View Logs">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnlogs" runat="server" CausesValidation="false" CommandName="Upload"
                                                    OnClientClick='<%# string.Format("return showlogModel(\"{0}\");", Eval("Task_Id")) %>'
                                                    CommandArgument='<%#Bind("Task_Id") %>' CssClass="btn btn-default" Enabled="true">
                                            <span aria-hidden="true">View Logs</span>
                                                </asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="true" HeaderText="Download Instruction">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDownload" runat="server" CausesValidation="false" CommandName="Download"                                                     
                                                    CommandArgument='<%#Bind("Task_Id") %>' CssClass="btn btn-default" Enabled="true">
                                            <span aria-hidden="true">Download Instruction</span>
                                                </asp:LinkButton>
                                                <div id="download" style="text-align: center">
                                                    <asp:Label ID="lbldownload" runat="server" Text="API Call" Visible="false"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="true" HeaderText="Upload File">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnupload" runat="server" CausesValidation="false" CommandName="Upload"
                                                    OnClientClick='<%# string.Format("return showFileUpload(\"{0}\", \"{1}\");", Eval("Suppllier_ID"), Eval("Entity")) %>'
                                                    CommandArgument='<%#Bind("Task_Id") %>' CssClass="btn btn-default" Enabled="true">
                                            <span aria-hidden="true">Upload File</span>
                                                </asp:LinkButton>
                                                <div id="download" style="text-align: center">
                                                    <asp:Label ID="lblUpload" runat="server" Text="API Call" Visible="false"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="true" HeaderText="Task Complete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnTaskComp" runat="server" CausesValidation="false" CommandName="TaskComplete" CommandArgument='<%#Bind("Task_Id") %>' CssClass="btn btn-default" Enabled="true">
                                            <span aria-hidden="true">Task Complete</span>
                                                </asp:LinkButton>
                                                <div id="download" style="text-align: center">
                                                    <asp:Label ID="lblTask" runat="server" Text="API Call" Visible="false"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="true" HeaderText="View Detail">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDetail" runat="server" CausesValidation="false" CommandName="View"
                                                    OnClientClick='<%# string.Format("return showDetailsModal(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\");", Eval("Pentahocall_id"), Eval("SuppllierName"),Eval("Entity"),Eval("APIStatus"),Eval("ApiPath")) %>'
                                                    CommandArgument='<%#Bind("Task_Id") %>' CssClass="btn btn-default" Enabled="true">
                                            <span aria-hidden="true">View Detail</span>
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

    </div>
    <div class="modal fade" id="moViewDetials" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title"><b>View Supplier API Progress Details </b></h4>
                    <br />
                    <div class="row">
                        <div class="col-sm-3">
                            <label class="col-form-label" for="txtSupplier">Supplier</label>
                            <asp:TextBox ID="txtSupplier" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                            <label class="col-form-label " for="txtEntity">Entity</label>
                            <%--<label id="lblEntity" class="form-control"></label>--%>
                            <asp:TextBox ID="txtEntity" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                            <label class="col-form-label">API Job Name</label>
                            <asp:TextBox ID="txtPath" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                            <label class="col-form-label">Status</label>
                            <asp:TextBox ID="txtStatus" CssClass="form-control" runat="server" value="" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <input type="hidden" id="hdnPentahoid" name="hdnPentahoid" value="" />
                <div class="modal-body modal-scroll">
                    <div id="steps">
                        <table class="table  table-bordered border-collapse table-striped  table-fixed">
                            <thead>
                                <tr>
                                    <th>Stepname</th>
                                    <th>Copy</th>
                                    <th>Read</th>
                                    <th>Written</th>
                                    <th>Input</th>
                                    <th>Output</th>
                                    <th>Updated</th>
                                    <th>Rejected</th>
                                    <th>Errors</th>
                                    <th>Active</th>
                                    <th>Time</th>
                                    <th>Speed</th>
                                    <th>pr/in/out</th>
                                </tr>
                            </thead>
                            <tbody id="tblsteps">
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="moFileUpload" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="panel-title">
                        <h4 class="modal-title">File Upload</h4>
                    </div>
                </div>
                <div class="modal-body">
                    <iframe name="iframe_upload" class="col-md-12" src="~/staticdata/files/StaticFileupload.aspx" style="min-height: 500px;" frameborder="0" runat="server"></iframe>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>    

    <div class="modal fade" id="moDownloadInstruction" role="dialog" data-backdrop="static" data-keyboard="false">
    <div class="modal-dialog modal-lg x-lg">
        <div class="modal-content">

            <div class="modal-header">
                <div class="panel-title">
                    <h4 class="modal-title">Download Instruction</h4>
                </div>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel ID="UpdProductMapModal" runat="server">
                    <ContentTemplate>
                        <uc1:supplierStaticDownloadData runat="server" ID="supplierStaticDownloadData" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>

    <div class="modal fade" id="mologViewDetials" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                 <div class="modal-header">
                <div class="panel-heading">
                    <h4 class="modal-title">Logs for Job</h4>
                </div>
            </div>
                <input type="hidden" id="hdnTaskid" name="hdnTaskid" value="" />
                <div class="modal-body modal-scroll">
                    <div id="logsteps">
                        <table class="table  table-bordered border-collapse table-striped  table-fixed">
                            <thead>
                                <tr>
                                    <th>StatusMessage</th>
                                    <th>LogType</th>
                                    <th>Remark</th>
                                    <th>CreateDate</th>
                                </tr>
                            </thead>
                            <tbody id="tbllogsteps">
                            </tbody>
                        </table>
                    </div>
                </div>


                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>


</body>

</html>

