﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manageSupplierImports.aspx.cs" Inherits="TLGX_Consumer.staticdata.manageSupplierImports" EnableEventValidation="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Scripts/ChartJS/morris.css" rel="stylesheet" media="all" />
    <style>
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

        .nxtrundate {
            font-size: small;
        }

        .nodata {
            font-weight: bold;
            font-size: small;
            height:100px;
            text-align: center;
        }

        table{
                 width:100%;
        }
        .chartheight{
            height:200px;
        }
    </style>
    <script type="text/javascript">
        var colorsArray = [];
       // var isPageLoad = false;
        while (colorsArray.length < 200) {
            do {
                var color = Math.floor((Math.random() * 1000000) + 2);
            }
            while (colorsArray.indexOf(color) >= 0);
            colorsArray.push("#" + ("ffffff" + color.toString(16)).slice(-6));
        }

        function getChartData() {
            var sid = $('#MainContent_ddlSupplierName').val();
            if (sid == '0') {
                //For all supplier Show both Div
                //$('#supplierwisedata').show();
               // $('#allsupplierdata').show();
                $('#ReportViewersupplierwise').hide();
                getAllSupplierData();
                sid = '00000000-0000-0000-0000-000000000000'
            }
            else {
                //$('#supplierwisedata').show();
               // $('#allsupplierdata').hide();
                $('#ReportViewersupplierwise').hide();
            }
            $.ajax({
                url: '../../../Service/SupplierWiseDataForChart.ashx',
                data: { 'Supplier_Id': sid },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    //debugger;
                    var contryArray = [];
                    var cityArray = [];
                    var productArray = [];
                    var activityArray = [];
                    var hotelroomArray = [];
                    var iNodes = 0;
                    var iMappingData = 0;
                    var nxtrun = result[0].NextRun;
                    var date = new Date(nxtrun);
                    if (nxtrun == "Not Scheduled") {
                        $(".nxtrundate").hide();
                    }
                    else if (nxtrun == "1/1/0001 12:00:00 AM") {
                        $(".nxtrundate").append("Next Run is Not scheduled for this supplier");
                    }
                    else {
                        $(".nxtrundate").append("Next Run is scheduled on :&nbsp <br/>" + date);
                    }
                    //Need to get  Data
                    for (; iNodes < result[0].MappingStatsFor.length; iNodes++) {
                        if (result[0].MappingStatsFor[iNodes].MappingFor == "Country") {
                            var per = result[0].MappingStatsFor[iNodes].MappedPercentage;
                            $(".countryper").append(per + "%");
                            var resultDataForCountry = result[0].MappingStatsFor[iNodes].MappingData;
                            for (var iCountryMappingData = 0; iCountryMappingData < resultDataForCountry.length; iCountryMappingData++) {
                                if (resultDataForCountry[iCountryMappingData].Status != "ALL") {
                                    contryArray.push(resultDataForCountry[iCountryMappingData]);
                                    $("#detailcountry").append(resultDataForCountry[iCountryMappingData].Status + "&nbsp;&nbsp;:&nbsp;&nbsp;" + resultDataForCountry[iCountryMappingData].TotalCount + "<br>");
                                   
                                }
                                else {
                                    $("#countryTotal").append("Total&nbsp;&nbsp;:&nbsp;&nbsp;" + resultDataForCountry[iCountryMappingData].TotalCount);
                                    if(resultDataForCountry[iCountryMappingData].SuppliersCount!= null)
                                    { $("#countrySuppliersCount").append("Total Suppliers&nbsp;:&nbsp;" + resultDataForCountry[iCountryMappingData].SuppliersCount);}
                                }
                            }
                        }
                        if (result[0].MappingStatsFor[iNodes].MappingFor == "City") {
                            var per = result[0].MappingStatsFor[iNodes].MappedPercentage;
                            $(".cityper").append(per + "%");
                            var resultDataForCity = result[0].MappingStatsFor[iNodes].MappingData;
                            for (var iCityMappingData = 0; iCityMappingData < resultDataForCity.length; iCityMappingData++) {
                                if (resultDataForCity[iCityMappingData].Status != "ALL") {
                                    cityArray.push(resultDataForCity[iCityMappingData]);
                                    $("#detailcity").append(resultDataForCity[iCityMappingData].Status + "&nbsp;&nbsp;:&nbsp;&nbsp;" + resultDataForCity[iCityMappingData].TotalCount + "<br>");
                                    
                                }
                                else {
                                    $("#cityTotal").append("Total&nbsp;&nbsp;:&nbsp;&nbsp;" + resultDataForCity[iCityMappingData].TotalCount);
                                    if (resultDataForCity[iCityMappingData].SuppliersCount != null)
                                    { $("#citySuppliersCount").append("Total Suppliers&nbsp;:&nbsp;" + resultDataForCity[iCityMappingData].SuppliersCount); }
                                }
                            }

                        }
                        if (result[0].MappingStatsFor[iNodes].MappingFor == "Product") {
                            var per = result[0].MappingStatsFor[iNodes].MappedPercentage;
                            $(".productper").append(per + "%");
                            var resultDataForProduct = result[0].MappingStatsFor[iNodes].MappingData;
                            for (var iProductMappingData = 0 ; iProductMappingData < resultDataForProduct.length; iProductMappingData++) {
                                if (resultDataForProduct[iProductMappingData].Status != "ALL") {
                                    productArray.push(resultDataForProduct[iProductMappingData]);
                                    $("#detailproduct").append(resultDataForProduct[iProductMappingData].Status + "&nbsp;&nbsp;:&nbsp;&nbsp;" + resultDataForProduct[iProductMappingData].TotalCount + "<br>");
                                    
                                }
                                else {
                                    $("#productTotal").append("Total&nbsp;&nbsp;:&nbsp;&nbsp;" + resultDataForProduct[iProductMappingData].TotalCount);
                                    if (resultDataForProduct[iProductMappingData].SuppliersCount != null)
                                    { $("#productSuppliersCount").append("Total Suppliers&nbsp;:&nbsp;" + resultDataForProduct[iProductMappingData].SuppliersCount); }
                                }
                            }
                        }
                        if (result[0].MappingStatsFor[iNodes].MappingFor == "Activity") {
                            var per = result[0].MappingStatsFor[iNodes].MappedPercentage;
                            $(".activityper").append(per + "%");
                            var resultDataForActivity = result[0].MappingStatsFor[iNodes].MappingData;
                            for (var iActivityMappingData = 0 ; iActivityMappingData < resultDataForActivity.length; iActivityMappingData++) {
                                if (resultDataForActivity[iActivityMappingData].Status != "ALL") {
                                    activityArray.push(resultDataForActivity[iActivityMappingData]);
                                    $("#detailactivity").append(resultDataForActivity[iActivityMappingData].Status + "&nbsp;&nbsp;:&nbsp;&nbsp;" + resultDataForActivity[iActivityMappingData].TotalCount + "<br>");
                                   
                                }
                                else {
                                    $("#activityTotal").append("Total&nbsp;&nbsp;:&nbsp;&nbsp;" + resultDataForActivity[iActivityMappingData].TotalCount);
                                    if (resultDataForActivity[iActivityMappingData].SuppliersCount != null)
                                    { $("#activitySuppliersCount").append("Total Suppliers&nbsp;:&nbsp;" + resultDataForActivity[iActivityMappingData].SuppliersCount); }
                                }
                            }
                        }
                        if (result[0].MappingStatsFor[iNodes].MappingFor == "HotelRoom") {
                            var per = result[0].MappingStatsFor[iNodes].MappedPercentage;
                            $(".HotelRoomper").append(per + "%");
                            var resultDataForHotelRoom = result[0].MappingStatsFor[iNodes].MappingData;
                            for (var iHotelRoomMappingData = 0 ; iHotelRoomMappingData < resultDataForHotelRoom.length; iHotelRoomMappingData++) {
                                if (resultDataForHotelRoom[iHotelRoomMappingData].Status != "ALL") {
                                    hotelroomArray.push(resultDataForHotelRoom[iHotelRoomMappingData]);
                                    $("#detailHotelRoom").append(resultDataForHotelRoom[iHotelRoomMappingData].Status + "&nbsp;&nbsp;:&nbsp;&nbsp;" + resultDataForHotelRoom[iHotelRoomMappingData].TotalCount + "<br>");
                                }
                                else {
                                    $("#HotelRoomTotal").append("Total&nbsp;&nbsp;:&nbsp;&nbsp;" + resultDataForHotelRoom[iHotelRoomMappingData].TotalCount);
                                    if (resultDataForHotelRoom[iHotelRoomMappingData].SuppliersCount != null)
                                    { $("#HotelRoomSuppliersCount").append("Total Suppliers&nbsp;:&nbsp;" + resultDataForHotelRoom[iHotelRoomMappingData].SuppliersCount); }
                                }
                            }
                        }
                    }
                    //changing key names to label and value Start
                    for (var i = 0; i < contryArray.length; i++) {
                        var o = contryArray[i];
                        o.label = o.Status;
                        delete o.Status;
                        o.value = o.TotalCount;
                        delete o.TotalCount;
                    }
                    for (var i = 0; i < cityArray.length; i++) {
                        var o = cityArray[i];
                        o.label = o.Status;
                        delete o.Status;
                        o.value = o.TotalCount;
                        delete o.TotalCount;
                    }

                    for (var i = 0; i < productArray.length; i++) {
                        var o = productArray[i];
                        o.label = o.Status;
                        delete o.Status;
                        o.value = o.TotalCount;
                        delete o.TotalCount;
                    }
                    for (var i = 0; i < activityArray.length; i++) {
                        var o = activityArray[i];
                        o.label = o.Status;
                        delete o.Status;
                        o.value = o.TotalCount;
                        delete o.TotalCount;
                    }
                    for (var i = 0; i < hotelroomArray.length; i++) {
                        var o = hotelroomArray[i];
                        o.label = o.Status;
                        delete o.Status;
                        o.value = o.TotalCount;
                        delete o.TotalCount;
                    }

                    //-- Changing Key names End

                    Morris.Donut({
                        element: 'country',
                        data: contryArray,
                        colors: [
                            '#007F00',
                            '#e7bd0d',
                            '#e14949'
                        ],
                        resize: true,

                    });
                    Morris.Donut({
                        element: 'city',
                        data: cityArray,
                        colors: [
                            '#007F00',
                            '#e7bd0d',
                            '#e14949'
                        ],
                        resize: true
                    });
                    Morris.Donut({
                        element: 'product',
                        data: productArray,
                        colors: [
                            '#007F00',
                            '#e7bd0d',
                            '#e14949'
                        ],
                        resize: true
                    });
                    Morris.Donut({
                        element: 'activity',
                        data: activityArray,
                        colors: [
                            '#007F00',
                            '#e7bd0d',
                            '#e14949'
                        ],
                        resize: true
                    });
                    Morris.Donut({
                        element: 'HotelRoom',
                        data: hotelroomArray,
                        colors: [
                            '#007F00',
                            '#e7bd0d',
                            '#e14949'
                        ],
                        resize: true
                    });


                    if (contryArray.length == 0) {
                        $("#country").append("<br/><br/>No Static Data Found").addClass("nodata");
                    }
                    if (cityArray.length == 0) {
                        $("#city").append("<br/><br/>No Static Data Found").addClass("nodata");
                    }
                    if (productArray.length == 0) {
                        $("#product").append("<br/><br/>No Static Data Found").addClass("nodata");
                    }
                    if (activityArray.length == 0) {
                        $("#activity").append("<br/><br/>No Static Data Found").addClass("nodata");
                    }
                    if (hotelroomArray.length == 0) {
                        $("#HotelRoom").append("<br/><br/>No Static Data Found").addClass("nodata");
                    }

                },
                error: function (xhr, status, error) {
                   // alert("failed to load file");
                }
            });
        }
         function getAllSupplierData() {
            $.ajax({
                url: '../../../Service/AllSupplierDataForChart.ashx',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                type: 'GET',
                success: function (result) {
                    var allcontryArray = [];
                    var allcityArray = [];
                    var allproductArray = [];
                    var allactivityArray = [];
                    var allhotelroomArray = [];
                    var inodes = 0;
                    for (; inodes < result.length; inodes++) {
                        if (result[inodes].Mappingfor == "Country") {
                            delete result[inodes].Mappingfor;
                            allcontryArray.push(result[inodes]);
                            $("#alldetailcountry").append(result[inodes].SupplierName + "&nbsp;&nbsp;:&nbsp;&nbsp;" + result[inodes].totalcount + "<br/>")

                        }
                        if (result[inodes].Mappingfor == "City") {
                            delete result[inodes].Mappingfor;
                            allcityArray.push(result[inodes]);
                            $("#alldetailcity").append(result[inodes].SupplierName + "&nbsp;&nbsp;:&nbsp;&nbsp;" + result[inodes].totalcount + "<br/>")

                        }
                        if (result[inodes].Mappingfor == "Product") {
                            delete result[inodes].Mappingfor;
                            allproductArray.push(result[inodes]);
                            $("#alldetailproduct").append(result[inodes].SupplierName + "&nbsp;&nbsp;:&nbsp;&nbsp;" + result[inodes].totalcount + "<br/>")

                        }
                        if (result[inodes].Mappingfor == "Activity") {
                            delete result[inodes].Mappingfor;
                            allactivityArray.push(result[inodes]);
                            $("#alldetailactivity").append(result[inodes].SupplierName + "&nbsp;&nbsp;:&nbsp;&nbsp;" + result[inodes].totalcount + "<br/>")

                        }
                        if (result[inodes].Mappingfor == "HotelRoom") {
                            delete result[inodes].Mappingfor;
                            allhotelroomArray.push(result[inodes]);
                            $("#alldetailHotelRoom").append(result[inodes].SupplierName + "&nbsp;&nbsp;:&nbsp;&nbsp;" + result[inodes].totalcount + "<br/>")

                        }
                    }
                    //changing labels
                    for (var i = 0; i < allcontryArray.length; i++) {
                        var o = allcontryArray[i];
                        o.label = o.SupplierName;
                        delete o.SupplierName;
                        o.value = o.totalcount;
                        delete o.totalcount;
                    }
                    for (var i = 0; i < allcityArray.length; i++) {
                        var o = allcityArray[i];
                        o.label = o.SupplierName;
                        delete o.SupplierName;
                        o.value = o.totalcount;
                        delete o.totalcount;
                    }
                    for (var i = 0; i < allproductArray.length; i++) {
                        var o = allproductArray[i];
                        o.label = o.SupplierName;
                        delete o.SupplierName;
                        o.value = o.totalcount;
                        delete o.totalcount;
                    }
                    for (var i = 0; i < allactivityArray.length; i++) {
                        var o = allactivityArray[i];
                        o.label = o.SupplierName;
                        delete o.SupplierName;
                        o.value = o.totalcount;
                        delete o.totalcount;
                    }
                    for (var i = 0; i < allhotelroomArray.length; i++) {
                        var o = allhotelroomArray[i];
                        o.label = o.SupplierName;
                        delete o.SupplierName;
                        o.value = o.totalcount;
                        delete o.totalcount;
                    }
                    //end

                    var allcountryChart = Morris.Donut({
                        element: 'allcountry',
                        data: allcontryArray,
                        colors: colorsArray,
                        resize: true,
                    })
                    var allcityChart = Morris.Donut({
                        element: 'allcity',
                        data: allcityArray,
                        colors: colorsArray,
                        resize: true,
                    })
                    var allproductChart = Morris.Donut({
                        element: 'allproduct',
                        data: allproductArray,
                        colors: colorsArray,
                        resize: true,
                    })
                    var allactivityChart = Morris.Donut({
                        element: 'allactivity',
                        data: allactivityArray,
                        colors: colorsArray,
                        resize: true,
                    })
                    var allHotelRoomChart = Morris.Donut({
                        element: 'allHotelRoom',
                        data: allhotelroomArray,
                        colors: colorsArray,
                        resize: true,
                    })
                    ////For legends.
                    allcountryChart.options.data.forEach(function (label, i) {
                        var legendItem = $('<span></span>').text(label['label'] + " ( " + label['value'] + " )").prepend('<br><span>&nbsp;</span>');
                        legendItem.find('span')
                          .css('backgroundColor', allcountryChart.options.colors[i])
                        .css('width', '20px')
                          .css('display', 'inline-block')
                          .css('margin', '5px');
                        $('#legendco').append(legendItem)
                    });
                    allcityChart.options.data.forEach(function (label, i) {
                        var legendItem = $('<span></span>').text(label['label'] + " ( " + label['value'] + " )").prepend('<br><span>&nbsp;</span>');
                        legendItem.find('span')
                          .css('backgroundColor', allcityChart.options.colors[i])
                        .css('width', '20px')
                          .css('display', 'inline-block')
                          .css('margin', '5px');
                        $('#legendci').append(legendItem)
                    });
                    allproductChart.options.data.forEach(function (label, i) {
                        var legendItem = $('<span></span>').text(label['label'] + " ( " + label['value'] + " )").prepend('<br><span>&nbsp;</span>');
                        legendItem.find('span')
                          .css('backgroundColor', allproductChart.options.colors[i])
                        .css('width', '20px')
                          .css('display', 'inline-block')
                          .css('margin', '5px');
                        $('#legendpr').append(legendItem)
                    });
                    allactivityChart.options.data.forEach(function (label, i) {
                        var legendItem = $('<span></span>').text(label['label'] + " ( " + label['value'] + " )").prepend('<br><span>&nbsp;</span>');
                        legendItem.find('span')
                          .css('backgroundColor', allactivityChart.options.colors[i])
                        .css('width', '20px')
                          .css('display', 'inline-block')
                          .css('margin', '5px');
                        $('#legendac').append(legendItem)
                    });
                    allHotelRoomChart.options.data.forEach(function (label, i) {
                        var legendItem = $('<span></span>').text(label['label'] + " ( " + label['value'] + " )").prepend('<br><span>&nbsp;</span>');
                        legendItem.find('span')
                          .css('backgroundColor', allHotelRoomChart.options.colors[i])
                        .css('width', '20px')
                          .css('display', 'inline-block')
                          .css('margin', '5px');
                        $('#legendhr').append(legendItem)
                    });
                    if (allcontryArray.length == 0) {
                        $("#allcountry").append("<br/><br/>No Static Data Found").addClass("nodata");
                    }
                    if (allcityArray.length == 0) {
                        $("#allcity").append("<br/><br/>No Static Data Found").addClass("nodata");
                    }
                    if (allproductArray.length == 0) {
                        $("#allproduct").append("<br/><br/>No Static Data Found").addClass("nodata");
                    }
                    if (allactivityArray.length == 0) {
                        $("#allactivity").append("<br/><br/>No Static Data Found").addClass("nodata");
                    }
                    if (allhotelroomArray.length == 0) {
                        $("#allHotelRoom").append("<br/><br/>No Static Data Found").addClass("nodata");
                    }
                },
                error: function (xhr, status, error) {
                   // alert("failed to load  all supplier data file");
                }
            });
        }
         $(document).ready(function () {
             $("a[title=PDF]").remove();
             $("a[title=Word]").remove();
             $("#ctl00_MainContent_ReportViewer1_ctl05_ctl04_ctl00_Menu > div").eq(1).remove();
             $("#ctl00_MainContent_ReportViewer1_ctl05_ctl04_ctl00_Menu > div").eq(2).remove();
         });
        $(window).on('load', function () {
            //debugger;
            $("#MainContent_btnExportCsv").click(function () {
               // alert('Export');
                $('#ReportViewersupplierwise').show();
            });

            $("#btnUpdateSupplier").click(function () {
                $("#ReportViewersupplierwise").hide();
            });
                getChartData();
        });
    </script>
    <script src="../Scripts/ChartJS/raphael-min.js"></script>
    <script src="../Scripts/ChartJS/morris.min.js"></script>
    <script src="../Scripts/ChartJS/xepOnline.jqPlugin.008.js"></script>


    <div class="row">
        <div class="col-md-6">
            <h1 class="page-header" style=" border-bottom:none">Suppliers Status</h1>
        </div>

        <div class="col-md-6 ">
            <div class="form-inline">
                <br />
                <br />
                <div class="form-group pull-right ">
                    <asp:DropDownList runat="server" ID="ddlSupplierName" CssClass="form-control" AppendDataBoundItems="true">
                        <asp:ListItem Value="0">--All Suppliers--</asp:ListItem>
                    </asp:DropDownList>
                    <%--<asp:Button ID="btnUpdateSupplier" runat="server" CssClass="btn btn-primary btn-sm" Text="View Status" />--%>
                    <button id="btnUpdateSupplier" class="btn btn-primary btn-sm" >View Status</button>
                    <asp:Button runat="server" Text="Export" CssClass="btn btn-sm btn-primary" ID="btnExportCsv" OnClick="btnExportCsv_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>
    <hr />  
    <%--for first three charts--%>
    <div class="row" id="supplierwisedata" runat="server">
        <div class="col5 col-sm-6" id="countrydiv" style="text-align: center">
            <div class="panel  panel-default">
                <div class="panel-heading">
                    <i class="fa fa-bar-chart-o fa-fw"></i>
                    <h3><b>Country Mapped</b><br />
                        <b class="countryper"></b></h3>
                </div>
                <div id="country" class="chartheight"></div>
                <div class="panel-body">
                    <b><span id="detailcountry" style="font-size: small"></span></b>
                </div>
                <div class="panel-body">
                    <b><span class="nxtrundate"></span></b>
                </div>
                <div class="panel-footer">
                    <h4><b id="countryTotal"></b></h4>
                    <h4><b id="countrySuppliersCount"></b></h4>
                </div>
            </div>
        </div>
        <div class="col5 col-sm-6 " id="citydiv" style="text-align: center">
            <div class="panel  panel-default">
                <div class="panel-heading">
                    <i class="fa fa-bar-chart-o fa-fw"></i>
                    <h3><b>City Mapped</b><br />
                        <b class="cityper"></b></h3>
                </div>
                <div id="city" class="chartheight"></div>
                <div class="panel-body">
                    <b><span id="detailcity" style="font-size: small"></span></b>
                </div>
                <div class="panel-body" style="text-align: center">
                    <b><span class="nxtrundate"></span></b>
                </div>
                <div class="panel-footer ">
                     <h4><b id="cityTotal"></b></h4>
                      <h4><b id="citySuppliersCount"></b></h4>
                </div>
            </div>
        </div>
        <div class="col5 col-sm-6" id="productdiv" style="text-align: center">
            <div class="panel  panel-default">
                <div class="panel-heading">
                    <i class="fa fa-bar-chart-o fa-fw"></i>
                    <h3><b>Hotel Mapped</b><br />
                        <b class="productper"></b></h3>
                </div>
                <div id="product" class="chartheight"></div>
                <div class="panel-body">
                    <b><span id="detailproduct" style="font-size: small"></span></b>
                </div>
                <div class="panel-body">
                    <b><span class="nxtrundate"></span></b>
                </div>
                <div class="panel-footer">
                    <h4><b id="productTotal"></b></h4>
                      <h4><b id="productSuppliersCount"></b></h4>
                </div>
            </div>
        </div>
        <div class="col5 col-sm-6" id="HotelRoomdiv" style="text-align: center">
            <div class="panel  panel-default">
                <div class="panel-heading">
                    <i class="fa fa-bar-chart-o fa-fw"></i>
                    <h3><b>Room Mapped</b><br />
                        <b class="HotelRoomper"></b></h3>
                </div>
                <div id="HotelRoom" class="chartheight"></div>
                <div class="panel-body">
                    <b><span id="detailHotelRoom" style="font-size: small"></span></b>
                </div>
                <div class="panel-body">
                    <b><span class="nxtrundate"></span></b>
                </div>
                <div class="panel-footer">
                    <h4><b id="HotelRoomTotal"></b></h4>
                     <h4><b id="HotelRoomSuppliersCount"></b></h4>
                </div>
            </div>
        </div>
        <div class="col5 col-sm-6" id="activitydiv" style="text-align: center">
            <div class="panel  panel-default">
                <div class="panel-heading">
                    <i class="fa fa-bar-chart-o fa-fw"></i>
                    <h3><b>Activity Mapped</b><br />
                        <b class="activityper"></b></h3>
                </div>
                <div id="activity" class="chartheight"></div>
                <div class="panel-body">
                    <b><span id="detailactivity" style="font-size: small"></span></b>
                </div>
                <div class="panel-body">
                    <b><span class="nxtrundate"></span></b>
                </div>
                <div class="panel-footer">
                    <h4><b id="activityTotal"></b></h4>
                      <h4><b id="activitySuppliersCount"></b></h4>
                </div>
            </div>
        </div>
    </div>
    <%-- for last three pie charts--%>
    <div class="row" id="allsupplierdata"  runat="server">
        <div class="col5 col-sm-6" id="allcountrydiv" style="text-align: left">
            <div class="panel  panel-default">
                <div class="panel-heading">
                    <i class="fa fa-bar-chart-o fa-fw"></i>
                    <h3><b>Country UnMapped</b></h3>
                </div>
                <div id="allcountry" class="chartheight"></div>
                <div class="panel-body">
                    <div id="legendco" class="donut-legend"></div>
                </div>
            </div>
        </div>
        <div class="col5 col-sm-6" id="allcitydiv" style="text-align: left">
            <div class="panel  panel-default">
                <div class="panel-heading">
                    <i class="fa fa-bar-chart-o fa-fw"></i>
                    <h3><b>City UnMapped</b></h3>
                </div>
                <div id="allcity" class="chartheight"></div>
                <div class="panel-body">
                    <div id="legendci" class="donut-legend"></div>
                </div>
            </div>
        </div>
        <div class="col5 col-sm-6" id="allproductdiv" style="text-align: left">
            <div class="panel  panel-default">
                <div class="panel-heading">
                    <i class="fa fa-bar-chart-o fa-fw"></i>
                    <h3><b>Hotel UnMapped</b></h3>
                </div>
                <div id="allproduct" class="chartheight"></div>
                <div class="panel-body">
                    <div id="legendpr" class="donut-legend"></div>
                </div>
            </div>
        </div>
        <div class="col5 col-sm-6" id="allHotelRoomdiv" style="text-align: left">
            <div class="panel  panel-default">
                <div class="panel-heading">
                    <i class="fa fa-bar-chart-o fa-fw"></i>
                    <h3><b>Room UnMapped</b></h3>
                </div>
                <div id="allHotelRoom" class="chartheight"></div>
                <div class="panel-body">
                    <div id="legendhr" class="donut-legend"></div>
                </div>
            </div>
        </div>
        <div class="col5 col-sm-6" id="allactivitydiv" style="text-align: left">
            <div class="panel  panel-default">
                <div class="panel-heading">
                    <i class="fa fa-bar-chart-o fa-fw"></i>
                    <h3><b>Activity UnMapped</b></h3>
                </div>
                <div id="allactivity" class="chartheight"></div>
                <div class="panel-body">
                    <div id="legendac" class="donut-legend"></div>
                </div>
            </div>
        </div>
    </div>
    <%--Export Report--%>
    <div class="container" id="report" runat="server">
        <div style="width: 100%; height: 100%">
            <rsweb:ReportViewer ID="ReportViewersupplierwise" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="100%" AsyncRendering="False" SizeToReportContent="true" ZoomMode="FullPage" ShowFindControls="False">
                <LocalReport ReportPath="staticdata\rptSupplierwiseReport.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource Name="DataSet1" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
        </div>
    </div>
</asp:Content>
