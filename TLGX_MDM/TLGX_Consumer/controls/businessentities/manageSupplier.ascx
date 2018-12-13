<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="manageSupplier.ascx.cs" Inherits="TLGX_Consumer.controls.businessentities.manageSupplier" %>
<%@ Register Src="~/controls/geography/supplierCountryMapping.ascx" TagPrefix="uc1" TagName="supplierCountryMapping" %>
<%@ Register Src="~/controls/geography/supplierCityMapping.ascx" TagPrefix="uc1" TagName="supplierCityMapping" %>
<%@ Register Src="../integrations/supplierIntegrations.ascx" TagName="supplierIntegrations" TagPrefix="uc2" %>
<%@ Register Src="~/controls/businessentities/supplierMarket.ascx" TagPrefix="uc1" TagName="supplierMarket" %>
<%@ Register Src="~/controls/businessentities/supplierProductCategory.ascx" TagPrefix="uc1" TagName="supplierProductCategory" %>
<%@ Register Src="~/controls/businessentities/supplierStaticDataHandling.ascx" TagPrefix="uc1" TagName="supplierStaticDataHandling" %>
<%@ Register Src="~/controls/businessentities/supplierCredentials.ascx" TagPrefix="uc1" TagName="supplierCredentials" %>
<%@ Register Src="~/controls/businessentities/supplierApiLocation.ascx" TagPrefix="uc1" TagName="supplierApiLocation" %>
<%@ Register Src="~/controls/businessentities/supplierStaticDownloadData.ascx" TagPrefix="uc1" TagName="supplierStaticDownloadData" %>


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
        height: 100px;
        text-align: center;
    }

    .chartheight {
        height: 200px;
    }
</style>
<script type="text/javascript">

    function getChartData() {
        var sid = '<%=this.Request.QueryString["Supplier_Id"]%>';
        var PriorityId = '0';
        var ProductCategory = '0';
        var IsMDM = "false";
        $.ajax({
            url: '../../../Service/SupplierWiseDataForChart.ashx',
            data: { 'Supplier_Id': sid, 'PriorityId': PriorityId, 'ProductCategory': ProductCategory, 'IsMDM': IsMDM },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                $("#country").empty();
                $("#detailcountry").empty();
                $("#countryTotal").empty();

                $("#city").empty();
                $("#detailcity").empty();
                $("#cityTotal").empty();

                $("#product").empty();
                $("#detailproduct").empty();
                $("#productTotal").empty();

                $("#activity").empty();
                $("#detailactivity").empty();
                $("#activityTotal").empty();

                $("#HotelRoom").empty();
                $("#detailHotelRoom").empty();
                $("#HotelRoomTotal").empty();

                $(".nxtrundate").empty();
                $(".countryper").empty();
                $(".cityper").empty();
                $(".productper").empty();
                $(".HotelRoomper").empty();
                $(".activityper").empty();
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
                        for (var iProductMappingData = 0; iProductMappingData < resultDataForProduct.length; iProductMappingData++) {
                            if (resultDataForProduct[iProductMappingData].Status != "ALL") {
                                productArray.push(resultDataForProduct[iProductMappingData]);
                                $("#detailproduct").append(resultDataForProduct[iProductMappingData].Status + "&nbsp;&nbsp;:&nbsp;&nbsp;" + resultDataForProduct[iProductMappingData].TotalCount + "<br>");

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
                        for (var iActivityMappingData = 0; iActivityMappingData < resultDataForActivity.length; iActivityMappingData++) {
                            if (resultDataForActivity[iActivityMappingData].Status != "ALL") {
                                activityArray.push(resultDataForActivity[iActivityMappingData]);
                                $("#detailactivity").append(resultDataForActivity[iActivityMappingData].Status + "&nbsp;&nbsp;:&nbsp;&nbsp;" + resultDataForActivity[iActivityMappingData].TotalCount + "<br>");

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
                        for (var iHotelRoomMappingData = 0; iHotelRoomMappingData < resultDataForHotelRoom.length; iHotelRoomMappingData++) {
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
<script type="text/javascript">
    $(document).ready(function () {
        getChartData();
        <%--var DownloadInfo = $('<%=Request.QueryString["DownloadInfo"]%>');
        if (DownloadInfo != null) {--%>
           // $('#ShowSupplierStatusChart').removeClass('active');
        $('#ShowSupplierStatusChart').addClass('active');
        $('ul#someList li:first').addClass('active');
          //  $('ul#someList li:nth-child(4)').addClass('active');
        //}
    });
</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
                                            <div class="form-group">
                                                <label for="ddlPriorityEdit">Priority</label>
                                                <asp:DropDownList runat="server" ID="ddlPriorityEdit" CssClass="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                    <asp:ListItem Value="1">1</asp:ListItem>
                                                    <asp:ListItem Value="2">2</asp:ListItem>
                                                    <asp:ListItem Value="3">3</asp:ListItem>
                                                    <asp:ListItem Value="4">4</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label for="chkIsFullPull">IsFullPull Supplier</label>&nbsp; &nbsp;&nbsp;
                                                     <asp:CheckBox ID="chkIsFullPull" runat="server" />
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
            <div id="Tabs" class="panel-body" role="tabpanel">
                <ul id="someList" class="nav nav-tabs tabs" role="tablist">
                    <li class="active"><a role="tab" data-toggle="tab" aria-controls="SupplierStatusChart" href="#ShowSupplierStatusChart" id="ShowSupplier">Supplier Status Charts</a></li>
                    <li><a role="tab" data-toggle="tab" aria-controls="ProductMapping" href="#ShowSupplierProductMapping">Product Categories</a></li>
                    <li><a role="tab" data-toggle="tab" aria-controls="SupplierStaticData" href="#ShowStaticDataUpdateSchedule">Static Data Update Schedule</a></li>
                    <li><a role="tab" data-toggle="tab" aria-controls="SupplierStaticDownloadData" href="#ShowSupplierStaticDownloadData" id="SupplierStaticDownloadData">Supplier Static Download Data</a></li>
                    <li><a role="tab" data-toggle="tab" aria-controls="SupplierApiLocation" href="#ShowSupplierApiLocation" id="apiLocation">Supplier API Location</a></li>
                    <li><a role="tab" data-toggle="tab" aria-controls="SupplierMarkets" href="#ShowSupplierMarkets">Supplier Markets</a></li>
                    <li><a role="tab" data-toggle="tab" aria-controls="SupplierCredentials" href="#ShowSupplierCredentials">Supplier Credentials</a></li>
                </ul>
                <div class="tab-content">
                    <%--for charts--%>
                    <div role="tabpanel" id="ShowSupplierStatusChart" class="tab-pane fade in active">
                        <br />
                        <div id="nodatafound" style="display: none"></div>
                        <div class="row" style="width: 100%; height: auto;">
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
                                        <br />
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
                                    <div id="city" class="chartheight"></div>
                                    <div class="panel-body">
                                        <b><span id="detailcity" style="font-size: small"></span></b>
                                        <br />
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
                                    <div id="product" class="chartheight"></div>
                                    <div class="panel-body">
                                        <b><span id="detailproduct" style="font-size: small"></span></b>
                                        <br />
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
                                            <b class="HotelRoomper"></b></h3>
                                    </div>
                                    <div id="HotelRoom" class="chartheight"></div>
                                    <div class="panel-body">
                                        <b><span id="detailHotelRoom" style="font-size: small"></span></b>
                                        <br />
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
                                    <div id="activity" class="chartheight"></div>
                                    <div class="panel-body">
                                        <b><span id="detailactivity" style="font-size: small"></span></b>
                                        <br />
                                        <b><span class="nxtrundate"></span></b>
                                    </div>
                                    <div class="panel-footer">
                                        <h4><b id="activityTotal"></b></h4>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--End--%>
                    <div role="tabpanel" id="ShowSupplierProductMapping" class="tab-pane fade in">
                        <br />
                        <uc1:supplierProductCategory runat="server" ID="supplierProductCategory" />
                    </div>
                    <div role="tabpanel" id="ShowStaticDataUpdateSchedule" class="tab-pane fade in">
                        <br />
                        <uc1:supplierStaticDataHandling runat="server" ID="supplierStaticDataHandling" />
                    </div>
                    <div role="tabpanel" id="ShowSupplierStaticDownloadData" class="tab-pane fade in">
                        <br />
                        <uc1:supplierStaticDownloadData runat="server" ID="supplierStaticDownloadData" />
                    </div>
                    <div role="tabpanel" id="ShowSupplierApiLocation" class="tab-pane fade in">
                        <br />
                        <uc1:supplierApiLocation runat="server" ID="supplierApiLocation" />
                    </div>
                    <div role="tabpanel" id="ShowSupplierMarkets" class="tab-pane fade in">
                        <br />
                        <uc1:supplierMarket runat="server" ID="supplierMarket" />
                    </div>
                    <div role="tabpanel" id="ShowSupplierCredentials" class="tab-pane fade in">
                        <br />
                        <uc1:supplierCredentials runat="server" ID="suppliersCredentials" />
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>




