﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchRoomTypes.ascx.cs" Inherits="TLGX_Consumer.controls.roomtype.searchRoomTypes" %>
<script src="../../../Scripts/autosize.min.js"></script>
<style>
    .paddingleft {
        margin-left: 6px !important;
    }

    .roomtype {
        white-space: normal !important;
        word-wrap: break-all;
    }
</style>
<script>
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
    });
    function pageLoad(sender, args) {
        var ta = document.querySelectorAll('textarea');
        autosize(ta);
    }
    function SelectedRow(element) {
        var row = $(element).parent().parent().closest('tr').next();
        if (row != null)
            if (row.find('.dropdownforBind') != null)
                row.find('.dropdownforBind').focus();
    }
    function showLoadingImage() {
        $('#loading').show();
    }
    function hideLoadingImage() {
        $('#loading').hide();
    }
    function mySelectedID(selectedcheckboxval) {
        var roomName = selectedcheckboxval.parentElement.parentElement.firstChild.textContent;
        var tillUL = selectedcheckboxval.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement;
        var Button = tillUL.firstElementChild.firstChild;
        var tr = selectedcheckboxval.parentElement.parentElement.parentElement.getElementsByTagName("tr");
        for (var i = 1 ; i < tr.length ; i++) {
            tr[i].childNodes[0].firstChild.checked = false;
            tr[i].className = "row";
        }
        selectedcheckboxval.parentElement.parentElement.className += " alert alert-success";
        Button.textContent = selectedcheckboxval.parentElement.parentElement.firstChild.nextSibling.nextSibling.textContent;
        selectedcheckboxval.parentElement.getElementsByClassName("checkboxClass")[0].checked = true;
        var hdnAccommodation_RoomInfo_Id = tillUL.parentElement.parentElement.lastElementChild.getElementsByClassName("hdnAccommodation_RoomInfo_Id")[0];
        hdnAccommodation_RoomInfo_Id.value = selectedcheckboxval.parentElement.parentElement.lastElementChild.firstChild.textContent;

        //Setting check box and Dropdown to Mapped
        var checkBoxForSelectedRow = tillUL.parentElement.parentElement.lastElementChild.firstElementChild;
        if (checkBoxForSelectedRow != null)
            checkBoxForSelectedRow.checked = true;

        var MappingStatusDdl = tillUL.parentElement.parentElement.getElementsByClassName("MappingStatus")[0];
        if (MappingStatusDdl != null) {
            for (var i = 0; i < MappingStatusDdl.options.length; i++) {
                if (MappingStatusDdl.options[i].text == "MAPPED") {
                    MappingStatusDdl.options[i].selected = true;
                    break;
                }
            }
        }

    }
    function BindRTDetails(controlval) {
        showLoadingImage();
        var acco_id = $(controlval).parent().parent().parent().find('.hidnAcoo_Id').val();
        var ulRoomInfo = $(controlval).parent().find('#ulRoomInfo');
        var acco_roomType_id = $(controlval).parent().parent().parent().find('.hdnAccommodation_RoomInfo_Id').val();
        debugger;
        //if (ulRoomInfo != null && ulRoomInfo[0].innerHTML.trim() == "") {
        if (ulRoomInfo != null && ulRoomInfo[0].getElementsByTagName("table")[0] === undefined) {
            if (acco_id != null && ulRoomInfo != null) {
                $.ajax({
                    url: '../../../Service/RoomCategoryAutoComplete.ashx',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: {
                        'acco_id': acco_id,
                        'type': 'fillcategorywithdetails'
                    },
                    responseType: "json",
                    success: function (result) {
                        var value = JSON.stringify(result);
                        var listItems = '';
                        if (result != null) {
                            var def = '<table class="table-bordered">  <tr class="row"><th class="col-md-1"></th><th class="col-md-5">Room Name</th> <th class="col-md-4">Room Category</th>';
                            def = def + ' <th class="col-md-1">Bed Type</th> <th class="col-md-1">Is Smoking</th></tr>';
                            var li = def;
                            var licheckbox = '<input type="checkbox" class="checkboxClass" id="myCheck" onclick="mySelectedID(this)">';
                            var licheckboxWithChecked = '<input type="checkbox" checked="true" class="checkboxClass" id="myCheck" onclick="mySelectedID(this)">';

                            var td = '<td class="col-md-3">';
                            var td4 = '<td class="col-md-4" style="word-wrap:  break-all;">';
                            var td1 = '<td class="col-md-1">';
                            var td2 = '<td class="col-md-2">';


                            var lic = ' <td style="display: none;" id="tdRoomInfoId">';
                            var licClose = '</table>';
                            var tdc = '</td>';
                            for (var i = 0; i < result.length; i++) {
                                if (acco_roomType_id != null && acco_roomType_id == result[i].Accommodation_RoomInfo_Id) {
                                    li = li + '<tr class="row alert alert-success">';
                                    li = li + td1 + licheckboxWithChecked + tdc;
                                }
                                else {
                                    li = li + '<tr class="row">';
                                    li = li + td1 + licheckbox + tdc;
                                }

                                li = li + td4 + result[i].RoomName + tdc;
                                li = li + td4 + result[i].RoomCategory + tdc;
                                li = li + td2 + result[i].BedType + tdc;
                                li = li + td1 + result[i].IsSomking + tdc;
                                li = li + lic + result[i].Accommodation_RoomInfo_Id + tdc + "</tr>";
                            }
                            li = li + licClose;

                            hideLoadingImage();
                            ulRoomInfo[0].innerHTML = li;
                        }
                    },
                    failure: function () {
                    }
                });
            }
        }
    }

</script>
<style>
    .floatingButton {
        position: fixed;
        bottom: 15px;
        z-index: 1000;
    }
</style>

<div class="navbar">
    <div class="navbar-inner">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#panSupplierSearch" data-toggle="tab">Search by Supplier</a></li>
            <li><a href="#panProductSearch" data-toggle="tab">Search by Product</a></li>
        </ul>
    </div>
</div>

<div class="tab-content">
    <div class="tab-pane active" id="panSupplierSearch">
        <!-- search filters panel -->
        <asp:UpdatePanel ID="updActivityMappingSearch" runat="server">
            <ContentTemplate>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel-group" id="accordion">
                            <div class="panel panel-default">

                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Filters</a>
                                    </h4>
                                </div>

                                <div id="collapseSearch" class="panel-collapse collapse in">

                                    <div class="panel-body">

                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <br />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="HotelSearch" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="ddlProductCategorySubType">
                                                        Supplier Name
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlSupplierNameBySupplier" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">---ALL---</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="ddlCountryBySupplier">Country</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlCountryBySupplier" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCountryBySupplier_SelectedIndexChanged">
                                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="ddlCityBySupplier">City</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlCityBySupplier" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-6">
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="ddlMappingTypeBySupplier">Mapping Type</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlMappingTypeBySupplier" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Static File" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="Dynamic Search Results" Value=""></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="ddlStatusBySupplier">
                                                        Status
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlStatusBySupplier" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="txtProductNameBySupplier">
                                                        Product Name
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox runat="server" ID="txtProductNameBySupplier" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="txtSupplierRoomName">
                                                        Supplier RoomName
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox runat="server" ID="txtSupplierRoomName" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <div class="col-sm-6">
                                                        <asp:Button ID="btnSearchBySupplier" runat="server" OnClick="btnSearchBySupplier_Click" CssClass="btn btn-primary btn-sm" Text="Search" ValidationGroup="HotelSearch" />
                                                        <asp:Button ID="btnResetBySupplier" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnResetBySupplier_Click" Text="Reset" CausesValidation="false" />
                                                    </div>
                                                </div>

                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Search Results Panel -->



                <div class="panel-group" id="accordionProductSearchResult">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-lg-6">
                                    <h4 class="panel-title pull-left">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearchResult">Search Room Types (Total Count:
                                <asp:Label ID="lblSupplierRoomSearchCount" runat="server" Text="0"></asp:Label>)</a>
                                    </h4>
                                </div>
                                <div class="col-lg-4 ">
                                    &nbsp;
                                </div>

                                <div class="col-lg-2">
                                    <div class="form-group pull-right" id="divPagging" runat="server" style="display: none;">
                                        <div class="input-group">
                                            <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                                            <asp:DropDownList ID="ddlPageSizeBySupplier" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSizeBySupplier_SelectedIndexChanged" CssClass="form-control col-lg-3">
                                                <asp:ListItem Text="5" Value="5" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="floatingButton">
                                <asp:UpdatePanel runat="server" ID="upnlbtns">
                                    <ContentTemplate>
                                        <asp:Button ID="btnMapSelectedBySupplier" OnClick="btnMapSelectedBySupplier_Click" runat="server" Visible="false" CssClass="btn btn-primary btn-sm" Text="Map Selected" />
                                        <asp:Button ID="btnMapAllBySupplier" OnClick="btnMapAllBySupplier_Click" runat="server" Visible="false" CssClass="btn btn-primary btn-sm" Text="Map All" />

                                        <asp:Button ID="btnTTFUSelectedBySupplier" runat="server" OnClick="btnTTFUSelectedBySupplier_Click" Visible="false" CssClass="btn btn-success btn-sm" Text="TTFU Selected" />
                                        <asp:Button ID="btnTTFUAllBySupplier" OnClick="btnTTFUAllBySupplier_Click" runat="server" Visible="false" CssClass="btn btn-success btn-sm" Text="TTFU All" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div id="divMsgForMapping" runat="server" style="display: none;"></div>
                                    <asp:GridView ID="grdRoomTypeMappingSearchResultsBySupplier" runat="server" AllowPaging="True" AllowCustomPaging="true" AutoGenerateColumns="False"
                                        DataKeyNames="Accommodation_SupplierRoomTypeMapping_Id,Accommodation_Id"
                                        CssClass="table table-responsive table-hover table-striped table-bordered" PagerStyle-CssClass="Page navigation" EmptyDataText="No Mapping Defined."
                                        OnRowCommand="grdRoomTypeMappingSearchResultsBySupplier_RowCommand" OnPageIndexChanging="grdRoomTypeMappingSearchResultsBySupplier_PageIndexChanging" OnRowDataBound="grdRoomTypeMappingSearchResultsBySupplier_RowDataBound">
                                        <Columns>
                                            <%--<asp:BoundField HeaderText="Hotel ID" DataField="CommonProductId" />--%>
                                            <asp:TemplateField HeaderText="Product Name" ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblProductName" Text='<%# Eval("ProductName") %>'></asp:Label><br />
                                                    <strong>(
                                                    <asp:Label runat="server" ID="lblHotelID" Text='<%# Eval("CommonProductId") %>'></asp:Label>)</strong>

                                                    <%# Convert.ToInt32(Eval("NumberOfRooms")) > 0 ? "<h4><span class='label label-success '>" + Convert.ToString(Eval("NumberOfRooms")) + "</span></h4>" : "<h5><span class='label label-danger'>No</span></h5>" %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField HeaderText="Product Name" DataField="ProductName" ItemStyle-Width="10%" />--%>
                                            <asp:BoundField HeaderText="City Name (Country)" DataField="Location" />
                                            <%-- <asp:TemplateField HeaderText="TLGX Rooms">
                                                <ItemTemplate>
                                                   
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:BoundField HeaderText="Supplier" DataField="SupplierName" />
                                            <%-- <asp:BoundField HeaderText="Supplier ID" DataField="SupplierRoomTypeCode" />--%>
                                            <%-- <asp:BoundField  DataField="SupplierRoomName"    />--%>
                                            <asp:TemplateField HeaderText="Supplier Room Type Name" ItemStyle-Width="12%" ItemStyle-Wrap="true">
                                                <ItemTemplate>
                                                    <%-- <a href="#" data-toggle="popover" title="Popover Header" data-content="Some content inside the popover">Toggle popover</a>--%>
                                                    <strong>(
                                                    <asp:Label runat="server" ID="lblSupplierRoomTypeCode" Text='<%# Eval("SupplierRoomTypeCode") %>'></asp:Label>)</strong>
                                                    <asp:Label runat="server" ID="lblSupplierRoomTypeName" Text='<%# Eval("SupplierRoomName") %>'></asp:Label>
                                                    <a href="#" data-toggle="popover" class="glyphicon glyphicon-info-sign" title='<%#Convert.ToString(Eval("RoomDescription")) %>' data-content='<%#Server.HtmlDecode(Convert.ToString(Eval("RoomDescription"))) %>'></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Suggested Room Info">
                                                <ItemTemplate>
                                                    <textarea runat="server" id="txtSuggestedRoomInfoInGridBySupplier" value='<%# Eval("Tx_StrippedName") %>' class="form-control"></textarea>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TLGX Room Info">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlSuggestedRoomInGridBySupplier" CssClass="form-control dropdownforBind " runat="server" onfocus="fillDropDown(this,true);" onclick="fillDropDown(this,true);" onchange="RemoveExtra(this,false);">
                                                    </asp:DropDownList>
                                                    <div class="dropdown" runat="server" id="ddlSuggestions">
                                                        <button class="btn btn-primary dropdown-toggle roomtype" style="width: 120px;" type="button" runat="server" id="btnSuggestionis" data-toggle="dropdown" onclick="BindRTDetails(this);">
                                                            -Select- 
                                                            <span class="caret paddingleft"></span>
                                                        </button>
                                                        <div class="dropdown-menu" id="ulRoomInfo" style="width: 430%; max-height: 200px; overflow-y: scroll;">
                                                            <div id="loading">
                                                                <img alt="Loading..." src="../../../images/ajax-loader.gif" />
                                                            </div>
                                                        </div>

                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Attribute Flags" ItemStyle-Width="7%">
                                                <ItemTemplate>
                                                    <asp:DataList ID="lstAlias" runat="server" DataSource='<%# Bind("RoomTypeAttributes") %>'
                                                        RepeatLayout="Table" RepeatColumns="3" RepeatDirection="Horizontal" ItemStyle-Wrap="true" CssClass="">
                                                        <ItemTemplate>
                                                            <h4><span aria-hidden="true" data-toggle="tooltip" data-placement="left" class="glyphicon glyphicon-<%# Eval("IconClass") %>" title="<%# Eval("SystemAttributeKeyword") + " : " + Eval("SupplierRoomTypeAttribute")  %>  "></span></h4>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlMappingStatusInGridBySupplier" CssClass="form-control MappingStatus" runat="server">
                                                        <asp:ListItem Value="ADD">ADD</asp:ListItem>
                                                        <asp:ListItem Value="MAPPED">MAPPED</asp:ListItem>
                                                        <asp:ListItem Value="UNMAPPED">UNMAPPED</asp:ListItem>
                                                        <asp:ListItem Value="REVIEW">REVIEW</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="false">
                                                <ItemTemplate>
                                                    <input type="checkbox" runat="server" id="chkSelect" onclick="SelectedRow(this);" />
                                                    <input type="hidden" class="hidnAcoo_Id" value='<%# Eval("Accommodation_Id") %>' />
                                                    <input type="hidden" class="hdnRoomCount" id="hdnRoomCount" runat="server" value='<%# Eval("NumberOfRooms") %>' />
                                                    <input type="hidden" class="hdnAccommodation_RoomInfo_Id" runat="server" id="hdnAccommodation_RoomInfo_Id" value='<%# Eval("Accommodation_RoomInfo_Id") %>' />
                                                    <input type="hidden" class="hdnAccommodation_RoomInfo_Name" value='<%# Eval("Accommodation_RoomInfo_Name") %>' />

                                                    <%-- <input type="hidden" class="hdnRoomDescription" id="hdnRoomDescription" runat="server" value='<%# Eval("RoomDescription") %>' />--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Product Based Search -->
    <div class="tab-pane fade in" id="panProductSearch">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel-group" id="accordionTab2">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordionTab2" href="#collapseSearchTab2">Search Filters</a>
                                    </h4>
                                </div>
                                <div id="collapseSearchTab2" class="panel-collapse collapse in">
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <br />
                                                <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="HotelSearch" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="ddlCountryByProduct">Country</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlCountryByProduct" OnSelectedIndexChanged="ddlCountryByProduct_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
                                                            <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="ddlCityByProduct">City</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlCityByProduct" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="txtCommonByProduct">Common Hotel ID</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtCommonByProduct" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="txtHotelNameByProduct">Hotel Name</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtHotelNameByProduct" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="ddlStatus">
                                                        Status
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlStatusByProduct" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-sm-6">
                                                        <asp:Button ID="btnSearchByProduct" runat="server" OnClick="btnSearchByProduct_Click" CssClass="btn btn-primary btn-sm" Text="Search" ValidationGroup="HotelSearch" />
                                                        <asp:Button ID="btnResetByProduct" runat="server" OnClick="btnResetByProduct_Click" CssClass="btn btn-primary btn-sm" Text="Reset" CausesValidation="false" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel-group" id="accordionProductSearchResultByProduct">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-lg-6">
                                    <h4 class="panel-title pull-left">
                                        <a data-toggle="collapse" data-parent="#accordionProductSearchResultByProduct" href="#collapseSearchResultByProduct">Products (Total Count:
                                <asp:Label ID="lblProductCount" runat="server" Text="0"></asp:Label>)</a>
                                    </h4>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div id="div1" runat="server" style="display: none;"></div>
                                    <asp:GridView ID="grdRoomTypeMappingSearchResultsByProduct" runat="server" AllowPaging="True" AllowCustomPaging="true" AutoGenerateColumns="False"
                                        CssClass="table table-responsive table-hover table-striped table-bordered" PagerStyle-CssClass="Page navigation" EmptyDataText="No Product Found."
                                        OnRowCommand="grdRoomTypeMappingSearchResultsByProduct_RowCommand" OnPageIndexChanging="grdRoomTypeMappingSearchResultsByProduct_PageIndexChanging"
                                        OnRowDataBound="grdRoomTypeMappingSearchResultsByProduct_RowDataBound">
                                        <Columns>
                                            <asp:BoundField HeaderText="Supplier" DataField="SupplierName" />
                                            <asp:BoundField HeaderText="Hotel ID" DataField="CommonProductId" />
                                            <asp:BoundField HeaderText="Product Name" DataField="ProductName" />
                                            <asp:BoundField HeaderText="City Name ( Country)" DataField="Location" />
                                            <asp:TemplateField HeaderText="Has Rooms">
                                                <ItemTemplate>
                                                    <input type="button" value='<%# Convert.ToInt32(Eval("NumberOfRooms")) > 0 ? Convert.ToString(Eval("NumberOfRooms")) : "No" %>' class='<%# Convert.ToInt32(Eval("NumberOfRooms")) > 0 ? "Greenbtn" : "Redbtn" %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NumberOfRooms" />
                                            <asp:BoundField HeaderText="Supplier ID" DataField="SupplierRoomId" />
                                            <asp:BoundField HeaderText="Supplier Room Type Name" />
                                            <asp:BoundField HeaderText="Suggested Room Info">
                                                <HeaderStyle BackColor="Turquoise" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="A?">
                                                <ItemTemplate>
                                                    <input type="button" value='<%# Convert.ToInt32(Eval("NumberOfRooms")) > 0 ? Convert.ToString(Eval("NumberOfRooms")) : "No" %>' class='<%# Convert.ToInt32(Eval("NumberOfRooms")) > 0 ? "Greenbtn" : "Redbtn" %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Status">
                                                <HeaderStyle BackColor="Turquoise" />
                                            </asp:BoundField>
                                            <asp:TemplateField ShowHeader="false">
                                                <ItemTemplate>
                                                    <input type="checkbox" runat="server" id="chkSelect" onclick="SelectedRow(this);" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <table class="table table-hover table-striped">
                    <thead>
                        <tr>
                            <th>Product Id</th>
                            <th>Product Name</th>
                            <th>Country</th>
                            <th>City</th>
                            <th>Product Status</th>
                            <th>Mapping Status</th>
                            <th>Has Product Room Types?</th>
                            <th>Static Pending</th>
                            <th>Dynamic Pending</th>
                            <th></th>

                        </tr>
                    </thead>


                    <tbody>
                        <tr>
                            <td>10123</td>
                            <td>Abba Queensgate Hotel</td>
                            <td>United Kingdom</td>
                            <td>London</td>
                            <td>Active</td>
                            <td>Pending</td>
                            <td>Yes (3) </td>
                            <td>3</td>
                            <td>4</td>
                            <td>
                                <asp:Button ID="Button4" runat="server" CssClass="btn btn-default " Text="Select" ValidationGroup="HotelSearch" />
                            </td>
                        </tr>


                    </tbody>


                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
