<%@ Control Language="C#" AutoEventWireup="true"   CodeBehind="supplierWiseDataChart.ascx.cs" Inherits="TLGX_Consumer.controls.staticdata.supplierWiseDataChart" %>
<link href="../../Scripts/ChartJS/morris.css" rel="stylesheet" />

<style type="text/css">
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
    }
   
</style>
<script>
 $(document).ready(function () {
    getChartData();
    });
</script>
<script type="text/javascript">
  
    function getChartData() {
        //get supplierid from page;
        var sid = '<%=this.Request.QueryString["Supplier_Id"]%>';
        var PriorityId = '0';
        $.ajax({
            url: '../../../Service/SupplierWiseDataForChart.ashx',
            data: { 'Supplier_Id': sid, 'PriorityId': PriorityId },
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
                if (nxtrun == "Not Scheduled") {
                    $(".nxtrundate").hide();
                }
                else if (nxtrun == null) {
                    $(".nxtrundate").append("Next Run is Not scheduled for this supplier");
                }
                else {
                    var t = nxtrun.split(/[- :]/);
                    var date = new Date(Date.UTC(t[2], t[1] - 1, t[0], t[3], t[4], t[5]));
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
                                if (resultDataForCountry[iCountryMappingData].Status == "UNMAPPED") {
                                    $("#allcountryTotal").append("Total&nbsp;&nbsp;:&nbsp;&nbsp;" + resultDataForCountry[iCountryMappingData].TotalCount);
                                }
                            }
                            else {
                                $("#countryTotal").append("Total&nbsp;&nbsp;:&nbsp;&nbsp;" + resultDataForCountry[iCountryMappingData].TotalCount);



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
                                if (resultDataForCity[iCityMappingData].Status == "UNMAPPED") {
                                    $("#allcityTotal").append("Total&nbsp;&nbsp;:&nbsp;&nbsp;" + resultDataForCity[iCityMappingData].TotalCount);
                                }
                            }
                            else {
                                $("#cityTotal").append("Total&nbsp;&nbsp;:&nbsp;&nbsp;" + resultDataForCity[iCityMappingData].TotalCount);

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
                                if (resultDataForProduct[iProductMappingData].Status == "UNMAPPED") {
                                    $("#allproductTotal").append("Total&nbsp;&nbsp;:&nbsp;&nbsp;" + resultDataForProduct[iProductMappingData].TotalCount);
                                }
                            }
                            else {
                                $("#productTotal").append("Total&nbsp;&nbsp;:&nbsp;&nbsp;" + resultDataForProduct[iProductMappingData].TotalCount);
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
                                if (resultDataForActivity[iActivityMappingData].Status == "UNMAPPED") {
                                    $("#allactivityTotal").append("Total&nbsp;&nbsp;:&nbsp;&nbsp;" + resultDataForActivity[iActivityMappingData].TotalCount);
                                }
                            }
                            else {
                                $("#activityTotal").append("Total&nbsp;&nbsp;:&nbsp;&nbsp;" + resultDataForActivity[iActivityMappingData].TotalCount);
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
                alert("failed to load file");
            }


        });
    }
</script>
<script src="../../Scripts/ChartJS/raphael-min.js"></script>
<script src="../../Scripts/ChartJS/morris.min.js"></script>
<div id="nodatafound" style="display:none"></div>
<div class="row">
        <div class="col5 col-sm-6" id="countrydiv" style="text-align: center">
            <div class="panel  panel-default">
                <div class="panel-heading">
                    <i class="fa fa-bar-chart-o fa-fw"></i>
                    <h3><b>Country Mapped</b><br />
                        <b class="countryper"></b></h3>
                </div>
                <div id="country"></div>
                <div class="panel-body">
                    <b><span id="detailcountry" style="font-size: small"></span></b>
                </div>
                <div class="panel-body">
                    <b><span class="nxtrundate"></span></b>
                </div>
                <div class="panel-footer">
                    <h4><b id="countryTotal"></b></h4>
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
                <div id="city"></div>
                <div class="panel-body">
                    <b><span id="detailcity" style="font-size: small"></span></b>
                </div>
                <div class="panel-body" style="text-align: center">
                    <b><span class="nxtrundate"></span></b>
                </div>
                <div class="panel-footer ">
                    <h4><b id="cityTotal"></b></h4>
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
                <div id="product"></div>
                <div class="panel-body">
                    <b><span id="detailproduct" style="font-size: small"></span></b>
                </div>
                <div class="panel-body">
                    <b><span class="nxtrundate"></span></b>
                </div>
                <div class="panel-footer">
                    <h4><b id="productTotal"></b></h4>
                </div>
            </div>
        </div>
        <div class="col5 col-sm-6" id="HotelRoomdiv" style="text-align: center">
            <div class="panel  panel-default">
                <div class="panel-heading">
                    <i class="fa fa-bar-chart-o fa-fw"></i>
                    <h3><b>Room Mapped</b><br />
                        <b class="rumper"></b></h3>
                </div>
                <div id="HotelRoom"></div>
                <div class="panel-body">
                    <b><span id="detailHotelRoom" style="font-size: small"></span></b>
                </div>
                <div class="panel-body">
                    <b><span class="nxtrundate"></span></b>
                </div>
                <div class="panel-footer">
                    <h4><b id="HotelRoomTotal"></b></h4>
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
                <div id="activity"></div>
                <div class="panel-body">
                    <b><span id="detailactivity" style="font-size: small"></span></b>
                </div>
                <div class="panel-body">
                    <b><span class="nxtrundate"></span></b>
                </div>
                <div class="panel-footer">
                    <h4><b id="activityTotal"></b></h4>
                </div>
            </div>
        </div>
    </div>

