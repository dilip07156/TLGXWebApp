<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileMappingcharts.ascx.cs" Inherits="TLGX_Consumer.controls.staticdataconfig.FileMappingcharts" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script type="text/javascript">
    function getChartDataFileMapping(fileid) {
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
                    $("#filepath").show();
                    $("#readdiv").show();
                    $("#txdiv").show();
                    $("#mapdiv").show();
                    $("#ttfudiv").show();
                    $("#filestatdiv").removeClass("col-md-10");
                    $("#verlogdiv").removeClass("col-md-10");
                    $("#errorlogtab").css("display", "none");
                    $("#read").empty();
                    $("#map").empty();
                    $("#tx").empty();
                    $("#match").empty();
                    $("#ttfu").empty();
                    $('#tblveboselog').empty();
                    $('#tblerrorlog').empty();
                    $("#tblstatastic").empty();
                    $("#currentbatch").empty();
                    $("#totalbatch").empty();
                    $("#Mcurrentbatch").empty();
                    $("#Mtotalbatch").empty();
                    $("#Rcurrentbatch").empty();
                    $("#Rtotalbatch").empty();
                    $("#lblSupplier").empty();
                    $("#lblEntity").empty();
                    $("#lblPath").empty();
                    $("#lblstatus").empty();
                    $("#lbltimeDiff").empty();
                    //file Details
                    for (var inode = 0; inode < result.FileDetails.length; inode++) {
                        if (result.FileDetails[0].Mode == "RE_RUN") {
                            $("#filepath").hide();
                            $("#readdiv").hide();
                            $("#txdiv").hide();
                            $("#mapdiv").hide();
                            $("#ttfudiv").hide();
                            $("#filestatdiv").addClass("col-md-10");
                            $("#verlogdiv").addClass("col-md-10");
                        }
                        $("#lblSupplier").val(result.FileDetails[0].Supplier);
                        $("#lblEntity").val(result.FileDetails[0].Entity);
                        $("#lblPath").val(result.FileDetails[0].OriginalFilePath);
                        $("#lblstatus").val(result.FileDetails[0].STATUS);
                        var status = result.FileDetails[0].STATUS;
                        if (status == "ERROR" || status == "PROCESSED") {
                            myStopFunction();
                        }
                    }
                    //process charts
                    for (var inode = 0; inode < result.ProgressLog.length; inode++) {
                        if (result.ProgressLog[inode].Step == "READ") {
                            var a = result.ProgressLog[inode].PercentageValue;
                            readarray.push({ label: "Completed", value: a });
                            if (a != 100) {
                                var b = (100 - a);
                                readarray.push({ label: "Remaining", value: b });
                            }
                            $("#Rcurrentbatch").text(result.ProgressLog[inode].CurrentBatch);
                            $("#Rtotalbatch").text(result.ProgressLog[inode].TotalBatch);
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
                        else if (result.ProgressLog[inode].Step == "TTFU") {
                            var a = result.ProgressLog[inode].PercentageValue;
                            ttfuarray.push({ label: "Completed", value: a });
                            if (a != 100) {
                                var b = (100 - a);
                                ttfuarray.push({ label: "Remaining", value: b });
                            }
                        }
                        else if (result.ProgressLog[inode].Step == "MATCH") {
                            var a = result.ProgressLog[inode].PercentageValue;
                            matcharray.push({ label: "Completed", value: a });
                            if (a != 100) {
                                var b = (100 - a);
                                matcharray.push({ label: "Remaining", value: b });
                            }
                            $("#Mcurrentbatch").text(result.ProgressLog[inode].CurrentBatch);
                            $("#Mtotalbatch").text(result.ProgressLog[inode].TotalBatch);
                            //stop timer on completion of remaining jobs 
                            //if (a == 100)
                            //    myStopFunction(x);
                            //end
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
                            var end_actual_time = new Date(parseInt(result.VerboseLog[0].TimeStamp.substr(6)));
                            var start_actual_time = new Date(parseInt(result.VerboseLog[i].TimeStamp.substr(6)));
                            var diff = end_actual_time - start_actual_time;
                            var diffSeconds = diff / 1000;
                            var HH = Math.floor(diffSeconds / 3600);
                            var MM = (Math.floor(diffSeconds % 3600) / 60).toFixed(2);
                            var formatted = ((HH < 10) ? ("0" + HH) : HH) + ":" + ((MM < 10) ? ("0" + MM) : MM);
                            $("#lbltimeDiff").val(formatted);
                        }
                    }
                    //error log
                    for (var i = 0; i < result.ErrorLog.length; i++) {
                        if (result.ErrorLog.length > 0) {
                            $("#errorlogtab").css("display", "block");
                            var d = new Date(parseInt(result.ErrorLog[i].Error_DATE.substr(6)));
                            var date = d.toLocaleString("en-GB");
                            var tr;
                            tr = $('<tr/>');
                            tr.append("<td>" + date + "</td>");
                            tr.append("<td>" + result.ErrorLog[i].ErrorCode + "</td>");
                            tr.append("<td>" + result.ErrorLog[i].ErrorDescription + "</td>");
                            tr.append("<td>" + result.ErrorLog[i].ErrorType + "</td>");
                            $("#errorlog table").append(tr);
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
                    //alert("Error fetching file processing data");
                },
            });
        }
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

<div class="row">
    <div class="col-md-12">

        <div class="col-sm-2 col5">
            <label class="col-form-label" for="lblSupplier">Supplier</label>
            <input type="text" readonly id="lblSupplier" class="form-control " />
        </div>
        <div class="col-sm-2 col5">
            <label class="col-form-label" for="lblEntity">Entity</label>
            <input type="text" readonly id="lblEntity" class="form-control" />
        </div>
        <div class="col-sm-2 col5" id="filepath">
            <label class="col-form-label" for="lblPath" >File</label>
            <input type="text" readonly id="lblPath" class="form-control" />
        </div>
        <div class="col-sm-2 col5">
            <label class="col-form-label" for="lblstatus">Status</label>
            <input type="text" readonly id="lblstatus" class="form-control" />
        </div>
        <div class="col-sm-2 col5">
            <label class="col-form-label" for="lbltimeDiff">Elapsed Time</label>
            <input type="text" readonly id="lbltimeDiff" class="form-control" />
        </div>

    </div>
</div>
<br />
<div class="row">
    <div class="col-md-12">
        <div class="col-sm-2 " id="readdiv">
            <div class="panel  panel-default " style="text-align: center">
                <div class="panel-heading" style="padding-top: 5px; padding-bottom: 5px">
                    <i class="fa fa-bar-chart-o fa-fw"></i>
                    <h4 style="margin-top: 5px"><b>READ : </b><span id="Rcurrentbatch" style="font: bold"></span>/<span id="Rtotalbatch" style="font: bold"></span></h4>
                </div>
                <div id="read" class="chartheight" style="height: 160px"></div>
            </div>
        </div>
        <div class="col-sm-10">
            <div class=" panel panel-default ">
                <div class="panel-heading" style="text-align: center; padding-top: 5px; padding-bottom: 5px">
                    <i class="fa fa-bar-chart-o fa-fw"></i>
                    <h4 style="margin-top: 5px"><b>BATCH &nbsp;:&nbsp;</b><span id="currentbatch" style="font: bold">0</span>/<span id="totalbatch" style="font: bold">0</span></h4>
                </div>
                <div class="panel-body" style="padding: 0px;">
                    <div class="col-sm-3" id="txdiv" style="text-align: center">
                        <i class="fa fa-bar-chart-o fa-fw"></i>
                        <b>TRANSFORM</b>
                        <div id="tx" class="chartheight"></div>
                    </div>
                    <div class="col-sm-3" id="mapdiv" style="text-align: center">
                        <i class="fa fa-bar-chart-o fa-fw"></i>
                        <b>MAP</b>
                        <div id="map" class="chartheight"></div>
                    </div>
                    <div class="col-sm-3" id="ttfudiv" style="text-align: center">
                        <i class="fa fa-bar-chart-o fa-fw"></i>
                        <b>TTFU</b>
                        <div id="ttfu" class="chartheight"></div>
                    </div>
                    <div class="col-sm-3" id="matchdiv" style="text-align: center">
                        <i class="fa fa-bar-chart-o fa-fw"></i>
                        <b style="margin-top: 5px; text-align: left"><b>MATCH&nbsp;:&nbsp;</b><span id="Mcurrentbatch" style="font: bold"></span>/<span id="Mtotalbatch" style="font: bold"></span></b>
                        <div id="match" class="chartheight" style="height: 160px"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<br />
<div class="row">
    <div class="col-md-12">
        <div class="col-md-12" id="verlogdiv">
            <div class="panel panel-default">
                <div id="Tabs" class="panel-body" role="tabpanel">
                    <ul class="nav nav-tabs tabs" role="tablist">
                        <li class="active" role="presentation"><a role="tab" data-toggle="tab" href="#ShowVerboselog">Verbose Log</a></li>
                        <li id="errorlogtab" role="presentation" style="display: none"><a role="tab" data-toggle="tab" href="#ShowErrorlog">Error Log &nbsp;&nbsp <span id="erroralert" class="glyphicon glyphicon-alert" style="color: red"></span></a></li>
                    </ul>
                    <div class="tab-content">
                        <div role="tabpanel" id="ShowVerboselog" class="tab-pane fade in active">
                            <div id="verboselog" style="overflow: scroll; height: 200px">
                                <table class="table table-fixed table-condensed">
                                    <thead>
                                        <tr>
                                            <th style="width:110px;">Date</th>
                                            <th style="width:1px;">Step</th>
                                            <th>Message</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tblveboselog">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div role="tabpanel" id="ShowErrorlog" class="tab-pane ">
                            <div id="errorlog" style="overflow: scroll; height: 200px">
                                <table class="table table-fixed table-condensed">
                                    <thead>
                                        <tr>
                                            <th>Date</th>
                                            <th>Error Code</th>
                                            <th>Error Type</th>
                                            <th>Description</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tblerrorlog">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-12">
        <div class="col-md-12" id="filestatdiv">
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
