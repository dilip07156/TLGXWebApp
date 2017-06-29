﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="manageSupplier.ascx.cs" Inherits="TLGX_Consumer.controls.businessentities.manageSupplier" %>
<%@ Register Src="~/controls/geography/supplierCountryMapping.ascx" TagPrefix="uc1" TagName="supplierCountryMapping" %>
<%@ Register Src="~/controls/geography/supplierCityMapping.ascx" TagPrefix="uc1" TagName="supplierCityMapping" %>



<%@ Register Src="../integrations/supplierIntegrations.ascx" TagName="supplierIntegrations" TagPrefix="uc2" %>
<%@ Register Src="~/controls/businessentities/supplierMarket.ascx" TagPrefix="uc1" TagName="supplierMarket" %>

<%@ Register Src="~/controls/businessentities/supplierProductCategory.ascx" TagPrefix="uc1" TagName="supplierProductCategory" %>
<%@ Register Src="~/controls/businessentities/supplierStaticDataHandling.ascx" TagPrefix="uc1" TagName="supplierStaticDataHandling" %>
<%@ Register Src="~/controls/businessentities/supplierCredentials.ascx" TagPrefix="uc1" TagName="supplierCredentials" %>
<%--for charts--%>
<%--<%@ Register Src="~/controls/staticdata/supplierWiseDataChart.ascx" TagPrefix="uc1" TagName="supplierWiseDataChart" %>--%>
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
<script type="text/javascript">
  
    function getChartData() {
        //get supplierid from page;
        var sid = '<%=this.Request.QueryString["Supplier_Id"]%>';
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
                    if (result[0].MappingStatsFor[iNodes].MappingFor == "HotelRum") {
                        var per = result[0].MappingStatsFor[iNodes].MappedPercentage;
                        $(".hotelrumper").append(per + "%");
                        var resultDataForHotelRum = result[0].MappingStatsFor[iNodes].MappingData;
                        for (var iHotelRumMappingData = 0 ; iHotelRumMappingData < resultDataForHotelRum.length; iHotelRumMappingData++) {
                            if (resultDataForHotelRum[iHotelRumMappingData].Status != "ALL") {
                                hotelroomArray.push(resultDataForHotelRum[iHotelRumMappingData]);
                                $("#detailhotelrum").append(resultDataForHotelRum[iHotelRumMappingData].Status + "&nbsp;&nbsp;:&nbsp;&nbsp;" + resultDataForHotelRum[iHotelRumMappingData].TotalCount + "<br>");
                            }
                            else {
                                $("#hotelrumTotal").append("Total&nbsp;&nbsp;:&nbsp;&nbsp;" + resultDataForHotelRum[iHotelRumMappingData].TotalCount);
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
                    element: 'hotelrum',
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
                    $("#hotelrum").append("<br/><br/>No Static Data Found").addClass("nodata");
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
<script type="text/javascript">
    function pageLoad(sender, args) {
        var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "SupplierMarkets";
        $('#Tabs a[href="#' + tabName + '"]').tab('show');
        $("#Tabs a").click(function () {
            $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
        });
    };
    $(document).on('shown.bs.tab', 'a[data-toggle="tab"]', function (e) {
        if (e.target.id == "ShowSupplier") {
            getChartData();
        }
    })

</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div id="dvMsgUpdateSupplierDetails" runat="server" style="display: none;"></div>
                <asp:HiddenField ID="hdnFlag" ClientIDMode="Static" runat="server" Value="" EnableViewState="false" />
                <asp:FormView ID="frmSupplierDetail" runat="server" DataKeyNames="Supplier_Id" DefaultMode="Edit" OnItemCommand="frmSupplierDetail_ItemCommand">
                    <EditItemTemplate>
                        <h1 class="page-header">Manage Supplier:
                    <asp:Label ID="lblSupplierName" runat="server" Text='<%# Bind("Name") %>' /></h1>
                        <div class="container">
                            <div class="form-group row">
                                <div class="col-sm-9">
                                    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="SupplierEdit" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                </div>
                                <div class="col-lg-8">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label for="txtName">
                                                    Name
                                              <asp:RequiredFieldValidator ValidationGroup="SupplierEdit" runat="server" ControlToValidate="txtNameSupplierEdit"
                                                  CssClass="text-danger" ErrorMessage="The supplier name field is required." Text="*" />
                                                </label>
                                                <asp:TextBox ID="txtNameSupplierEdit" CssClass="form-control" runat="server" Text='<%# Bind("Name") %>' />
                                            </div>
                                            <div class="form-group">
                                                <label for="txtCode">Code</label>
                                                <asp:TextBox ID="txtCodeSupplierEdit" CssClass="form-control" runat="server" Enabled="false" Text='<%# Bind("Code") %>' />
                                            </div>

                                            <div class="form-group">
                                                <div class="col-sm-10">
                                                    <asp:Button ID="btnUpdateSupplier" CommandName="EditCommand" CausesValidation="true" ValidationGroup="SupplierEdit" runat="server" CssClass="btn btn-primary btn-sm" Text="Update Supplier" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label for="ddlSupplierType">
                                                    Supplier Type
                                                </label>
                                                <asp:DropDownList runat="server" ID="ddlSupplierType" CssClass="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label for="ddlSupplierType">Status</label>
                                                <asp:DropDownList runat="server" ID="ddlStatusEdit" CssClass="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">

                                    <div class="panel panel-default">
                                        <div class="panel-heading">Audit Trail</div>
                                        <div class="panel-body">

                                            <div class="form-group">
                                                <label for="txtCreateDate" class="control-label col-sm-4">Create Date</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtCreateDate" CssClass="form-control" runat="server" Enabled="false" Text='<%# Bind("Create_Date") %>' />
                                                </div>
                                            </div>
                                            <br />
                                            <br />
                                            <div class="form-group">
                                                <label for="txtCreateUser" class="control-label col-sm-4">Create By</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtCreatedBy" CssClass="form-control" runat="server" Enabled="false" Text='<%# Bind("Create_User") %>' />
                                                </div>
                                            </div>
                                            <br />
                                            <br />
                                            <div class="form-group">
                                                <label for="txtEditDate" class="control-label col-sm-4">Edit Date</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtEditDate" CssClass="form-control" runat="server" Enabled="false" Text='<%# Bind("Edit_Date") %>' />
                                                </div>
                                            </div>
                                            <br />
                                            <br />
                                            <div class="form-group">
                                                <label for="txtEditUSer" class="control-label col-sm-4">Edit User</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtEditUSer" CssClass="form-control" runat="server" Enabled="false" Text='<%# Bind("Edit_User") %>' />
                                                </div>
                                            </div>

                                        </div>

                                    </div>

                                </div>
                            </div>
                    </EditItemTemplate>
                </asp:FormView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <asp:HiddenField ID="TabName" runat="server" />
        <div class="panel panel-default">
            <div class="panel-body">
                <ul class="nav nav-tabs">
                    <li class="active"><a data-toggle="tab" aria-controls="SupplierMarkets" href="#ShowSupplierMarkets">Supplier Markets</a></li>
                    <li><a data-toggle="tab" aria-controls="ProductMapping" href="#ShowSupplierProductMapping">Product Categories</a></li>
                    <li><a data-toggle="tab" aria-controls="SupplierStaticData" href="#ShowSupplierStaticData">Static Data Handling</a></li>
                    <li><a data-toggle="tab" aria-controls="SupplierCredentials" href="#ShowSupplierCredentials">Supplier Credentials</a></li>
                    <li><a data-toggle="tab" aria-controls="SupplierStatusChart" href="#ShowSupplierStatusChart" id="ShowSupplier">Supplier Status Charts</a></li>
                </ul>
                <div class="tab-content">
                    <div id="ShowSupplierMarkets" class="tab-pane fade in active">
                        <br />
                        <uc1:supplierMarket runat="server" ID="supplierMarket" />
                    </div>
                    <div id="ShowSupplierProductMapping" class="tab-pane fade in">
                        <br />
                        <uc1:supplierProductCategory runat="server" ID="supplierProductCategory" />
                    </div>
                    <div id="ShowSupplierStaticData" class="tab-pane fade in">
                        <br />
                        <uc1:supplierStaticDataHandling runat="server" ID="supplierStaticDataHandling" />
                    </div>
                    <div id="ShowSupplierCredentials" class="tab-pane fade in">
                        <br />
                        <uc1:supplierCredentials runat="server" ID="suppliersCredentials" />
                    </div>
                    <%--for charts--%>
                    <div id="ShowSupplierStatusChart" class="tab-pane fade in">
                        <br />
                      <%--<uc1:supplierWiseDataChart runat="server" ID="supplierWiseDataChart" />--%>
                           <div id="nodatafound" style="display:none"></div>
                            <div class="row" style="width: 100%; height: auto; ">
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
        <div class="col5 col-sm-6" id="hotelrumdiv" style="text-align: center">
            <div class="panel  panel-default">
                <div class="panel-heading">
                    <i class="fa fa-bar-chart-o fa-fw"></i>
                    <h3><b>Room Mapped</b><br />
                        <b class="rumper"></b></h3>
                </div>
                <div id="hotelrum"></div>
                <div class="panel-body">
                    <b><span id="detailhotelrum" style="font-size: small"></span></b>
                </div>
                <div class="panel-body">
                    <b><span class="nxtrundate"></span></b>
                </div>
                <div class="panel-footer">
                    <h4><b id="hotelrumTotal"></b></h4>
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

                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>




