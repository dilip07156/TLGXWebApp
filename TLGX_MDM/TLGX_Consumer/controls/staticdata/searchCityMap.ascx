<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchCityMap.ascx.cs" Inherits="TLGX_Consumer.controls.staticdata.CityMap" %>

<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBAbYHJn_5Kubmfa4-nYyAf_WpHB9mbfvc&libraries=places"></script>
<style>
    /* Always set the map height explicitly to define the size of the div
       * element that contains the map. */
    #map {
        height: 100%;
    }

    .hideColumn {
        display: none;
    }

    .fullWidth {
        width: 100%;
    }

    .HotelListrowPadding {
        padding: 5px;
    }

    .HotelInfo {
        height: 80px;
        overflow-y: scroll;
    }

    #modalHotelList {
        z-index: 2000;
    }

    .controls {
        background-color: #fff;
        border-radius: 2px;
        border: 1px solid transparent;
        box-shadow: 0 2px 6px rgba(0, 0, 0, 0.3);
        box-sizing: border-box;
        font-family: Roboto;
        font-size: 15px;
        font-weight: 300;
        height: 29px;
        margin-left: 17px;
        margin-top: 10px;
        outline: none;
        padding: 0 11px 0 13px;
        text-overflow: ellipsis;
        width: 400px;
    }

        .controls:focus {
            border-color: #4d90fe;
        }

    .title {
        font-weight: bold;
    }

    #infowindow-content {
        display: none;
    }

    #map #infowindow-content {
        display: inline;
    }

    .x-lg {
        width: 1200px;
    }
</style>

<script type="text/javascript">
    $(document).ready(function () {
        //$("#btnLocateMap").click(function () {

        //       });
    });

    function callmap() {
        var geocoder = new google.maps.Geocoder();
        var inputCityName = document.getElementById('MainContent_CityMap_frmEditCityMap_txtAddCityName');
        var inputCityCode = document.getElementById('MainContent_CityMap_frmEditCityMap_txtAddCode');
        var inputCountryName = document.getElementById('MainContent_CityMap_frmEditCityMap_txtAddCityCName');
        var inputCountryCode = document.getElementById('MainContent_CityMap_frmEditCityMap_txtAddCityCCode');
        var inputStateName = document.getElementById('MainContent_CityMap_frmEditCityMap_ddlAddCityState');
        var inputStateCode = document.getElementById('MainContent_CityMap_frmEditCityMap_txtAddCitySCode');
        var inputPlaceId = document.getElementById('MainContent_CityMap_frmEditCityMap_txtAddCityPlaceId');
        var input = document.getElementById('pac-input');
        var statename = "";
        //var con = document.getElementById('txtCon').value;
        //var city = document.getElementById('txtCity').value;
        if (inputStateName.value != "0") {
            statename = "," + inputStateName.options[inputStateName.selectedIndex].text;
        }
        var com = inputCityName.value + statename + "," + inputCountryName.value;
        geocoder.geocode({ 'address': com }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                var x = results[0].geometry.location.lat();
                var y = results[0].geometry.location.lng();
                inputPlaceId.value = results[0].place_id;
                //Getting State name from result set
                var address_components = results[0].address_components;
                var state;
                if (typeof address_components != "undefined") {
                    for (var i = 0; i < address_components.length; i++) {
                        var types = address_components[i].types;
                        if (typeof types != "undefined") {
                            for (var j = 0; j < types.length; j++) {
                                var typesg = types[j];
                                if (typeof typesg != "undefined" && typesg == "administrative_area_level_1") {
                                    state = address_components[i].long_name;
                                    var ddlstate = $('#MainContent_CityMap_frmEditCityMap_ddlAddCityState');
                                    ddlstate.find("option").prop('selected', false).filter(function () {
                                        return $(this).text() == state;
                                    }).attr("selected", "selected");
                                }
                            }
                        }
                    }
                }
                var latlng = new google.maps.LatLng(x, y);
                var myOptions = {
                    zoom: 14,
                    center: latlng,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };
                map = new google.maps.Map(document.getElementById("map"), myOptions);
                var marker = new google.maps.Marker({
                    position: new google.maps.LatLng(x, y),
                    map: map,
                    title: com
                });
                var infowindow = new google.maps.InfoWindow({
                    content: com
                });
                infowindow.open(map, marker);
                google.maps.event.addDomListener(window, 'load');
                SetStateCode();
            } else {
                res.innerHTML = "Enter correct Details: " + status;
            }
        });
    }
    function SetStateCode() {
        var ddlstate = $('#MainContent_CityMap_frmEditCityMap_ddlAddCityState').val();
        var statecode = $('#MainContent_CityMap_frmEditCityMap_txtAddCitySCode');


        if (ddlstate != null) {
            $.ajax({
                url: '../../../Service/GetCodeById_Service.ashx',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: { 'state_id': ddlstate },
                responseType: "json",
                success: function (result) {
                    statecode.val(result);
                },
                failure: function () {
                }
            });
        }
    }
    function closeDvMsg2() {
        setTimeout(function () {
            document.getElementById("<%=frmEditCityMap.FindControl("dvMsg2").ClientID %>").style.display = "none";
        }, 3000);
    }

    function ddlStatusChanged(ddl) {
        debugger;
        var ddlStatus = $('#MainContent_CityMap_frmEditCityMap_ddlStatus option:selected').html();
        var mySystemCountryName = document.getElementById("MainContent_CityMap_frmEditCityMap_vddlSystemCountryName");
        var mySystemCityName = document.getElementById("MainContent_CityMap_frmEditCityMap_vddlSystemCityName");
        var mySystemAddCityName = document.getElementById("MainContent_CityMap_frmEditCityMap_vddlSystemCountryNameAddCity");
        if (ddlStatus == 'DELETE') {
            ValidatorEnable(mySystemCountryName, false);
            ValidatorEnable(mySystemCityName, false);
            ValidatorEnable(mySystemAddCityName, false);
        }
        else {
            ValidatorEnable(mySystemCountryName, true);
            ValidatorEnable(mySystemCityName, true);
            ValidatorEnable(mySystemAddCityName, true);
        }
    }
</script>
<script type="text/javascript">


    function showCityMappingModal() {
        $("#moCityMapping").modal('show');
    }
    function closeCityMappingModal() {
        $("#moCityMapping").modal('hide');
    }
    function showSelectCityCodeModal() {
        $("#modalHotelList").modal('show');
    }
    function closeSelectCityCodeModal() {
        $("#modalHotelList").modal('hide');
    }
    function pageLoad(sender, args) {
        var hv = $('#MainContent_CityMap_hdnFlag').val();
        if (hv == "true") {
            closeCityMappingModal();
            $('#MainContent_CityMap_hdnFlag').val("false");
        }
    }
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
    });

    function callchange() {
        //initAutocomplete("search");
        //var e = $.Event("keypress", { which: 13 });
        //$('#pac-input').trigger(e);
    }

    function SelectedRow(element) {
        var ddlStatus = $('#MainContent_CityMap_ddlStatus option:selected').html();
        if (ddlStatus == "REVIEW") {
            element.parentNode.parentNode.nextSibling.childNodes[15].lastElementChild.focus();
        }
        else if (ddlStatus == "UNMAPPED") {
            element.parentNode.parentNode.nextSibling.childNodes[12].lastElementChild.focus();

        }
    }
    function MatchedSelect(elem) {
        elem.parentNode.parentNode.nextSibling.childNodes[14].lastElementChild.focus();
    }
    //var onClick = true;
    //Fill City dropdown in Grid
    function fillDropDown(record, onClick) {
        //alert(onClick);
        if (onClick) {
            var country_id = record.parentNode.parentNode.childNodes[16].lastElementChild.value;
            if (country_id != null) {
                //Getting Dropdown
                var currentRow = $(record).parent().parent();
                var CityDDL = currentRow.find("td:eq(11)").find('select');
                var selectedText = CityDDL.find("option:selected").text();
                var selectedOption = CityDDL.find("option");
                var selectedVal = CityDDL.val();
                if (CityDDL != null && CityDDL.is("select")) {
                    $.ajax({
                        url: '../../../Service/CityFillForDDL.ashx',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: { 'country_Id': country_id },
                        responseType: "json",
                        success: function (result) {
                            CityDDL.find("option:not(:first)").remove();
                            var value = JSON.stringify(result);
                            var listItems = '';
                            if (result != null) {
                                for (var i = 0; i < result.length; i++) {
                                    listItems += "<option value='" + result[i].City_Id + "'>" + result[i].Name + "</option>";
                                }
                                CityDDL.append(listItems);
                            }

                            CityDDL.find("option").prop('selected', false).filter(function () {
                                return $(this).text() == selectedText;
                            }).attr("selected", "selected");
                        },
                        failure: function () {
                        }
                    });
                }

            }
        }
    }
    function RemoveExtra(record, onClick) {
        if (!onClick) {
            var currentRow = $(record).parent().parent();
            var CityDDL = currentRow.find("td:eq(11)").find('select');
            var selectedText = CityDDL.find("option:selected").text();
            var selectedVal = CityDDL.val();
            CityDDL.find("option:not(:first)").remove();
            var listItems = "<option selected = 'selected' value='" + selectedVal + "'>" + selectedText + "</option>";
            CityDDL.append(listItems);
            var city_id = record.parentNode.parentNode.childNodes[16].firstElementChild;
            city_id.value = selectedVal;
        }
    }
</script>

<asp:UpdatePanel ID="updCityMapping" runat="server">
    <ContentTemplate>
        <div class="panel-group" id="accordion">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">City Mapping Search</a>
                    </h4>
                </div>


                <div id="collapseSearch" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <div class="container">
                            <div class="row">

                                <div class="col-sm-6">
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlSupplierName">Supplier Name</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlMasterCountry">System Country</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlMasterCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlMasterCountry_SelectedIndexChanged">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlCity">System City</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlStatus">Mapping Status</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>


                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="txtSuppName">Supplier Country Name</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtSuppCountry" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="form-group row ">
                                        <label class="control-label col-sm-4" for="txtSuppName">Supplier City Name</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtSuppCity" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-sm-6">

                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlShowEntries">Entries</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlShowEntries" runat="server" CssClass="form-control">
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
                                    <div class="form-group row">
                                        <div class="col-sm-12">&nbsp;</div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-6">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" />
                                            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" OnClick="btnReset_Click" />
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:Button ID="btnBulkMap" Visible="false" runat="server" CssClass="btn btn-primary btn-sm" Text="Bulk Add City Mapping" OnClick="btnBulkMap_Click" />

                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-12"></div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-12">&nbsp;</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="panel-group" id="accordionResult">
                    <div class="panel panel-default">
                        <div class="panel-heading clearfix">
                            <h4 class="panel-title pull-left">
                                <a data-toggle="collapse" data-parent="#accordionResult" href="#collapseSearchResult">Search Results (Total Count:
                                    <asp:Label ID="lblTotalCount" runat="server" Text="0"></asp:Label>)</a></h4>

                            <div class="form-group pull-right">
                                <asp:Button ID="btnMapSelected" runat="server" CssClass="btn btn-primary btn-sm" Text="Map Selected" OnClick="btnMapSelected_Click" />
                                <asp:Button ID="btnMapAll" runat="server" CssClass="btn btn-primary btn-sm" Text="Map All" OnClick="btnMapAll_Click" />
                            </div>
                        </div>


                        <div id="collapseSearchResult" class="panel-collapse collapse in">
                            <div class="panel-body">
                                <div class="form-group">
                                    <div id="dvMsg1" runat="server" style="display: none;"></div>
                                </div>

                                <asp:GridView ID="grdCityMaps" runat="server" AllowPaging="True" AllowCustomPaging="true" AutoGenerateColumns="False"
                                    EmptyDataText="No Static Updates" CssClass="table table-hover table-striped" DataKeyNames="CityMapping_Id,Supplier_Id,Country_Id,City_Id,Master_CityName"
                                    OnSelectedIndexChanged="grdCityMaps_SelectedIndexChanged" OnPageIndexChanging="grdCityMaps_PageIndexChanging" OnRowCommand="grdCityMaps_RowCommand"
                                    OnRowDataBound="grdCityMaps_RowDataBound" OnDataBound="grdCityMaps_DataBound1">

                                    <Columns>
                                        <asp:BoundField DataField="MapId" HeaderText="Map Id" />
                                        <asp:BoundField DataField="SupplierName" HeaderText="Name" />
                                        <asp:BoundField DataField="CountryCode" HeaderText="Country Code" />
                                        <asp:BoundField DataField="CountryName" HeaderText="Country Name" />
                                        <%--<asp:BoundField DataField="StateNameWithCode" HeaderText="State" />--%>
                                        <asp:TemplateField ShowHeader="true" HeaderText="State">
                                            <ItemTemplate>
                                                <%# Eval("StateName") + (!string.IsNullOrWhiteSpace(Convert.ToString(Eval("StateCode"))) ? "(" + Eval("StateCode") + ")" : string.Empty) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--  <asp:BoundField DataField="CityCode" HeaderText="City Code" ItemStyle-Width="5%" />--%>
                                        <asp:TemplateField HeaderText="City Code" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnSelectCityCode" Text='<%# Bind("CityCode") %>' runat="server" CausesValidation="false" CommandName="SelectCityCode" CommandArgument='<%# Bind("CityMapping_Id") %>' OnClientClick="showSelectCityCodeModal();">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="City Name" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnSelectCityName" Text='<%# Bind("CityName") %>' runat="server" CausesValidation="false" CommandName="SelectCityCode" CommandArgument='<%# Bind("CityMapping_Id") %>' OnClientClick="showSelectCityCodeModal();">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="CityName" HeaderText="City Name" ItemStyle-Width="5%" />--%>
                                        <asp:BoundField DataField="MasterCountryCode" HeaderText="Country Code">
                                            <HeaderStyle BackColor="Turquoise" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MasterCountryName" HeaderText="Country Name" ItemStyle-Width="7%">
                                            <HeaderStyle BackColor="Turquoise" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MasterStateName" HeaderText="State Name">
                                            <HeaderStyle BackColor="Turquoise" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MasterCityCode" HeaderText="City Code">
                                            <HeaderStyle BackColor="Turquoise" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Master_CityName" HeaderText="City Name">
                                            <HeaderStyle BackColor="Turquoise" />
                                        </asp:BoundField>
                                        <asp:TemplateField ShowHeader="true" HeaderText="City Name">
                                            <HeaderStyle BackColor="Turquoise" />
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlGridCity" runat="server" CssClass="form-control" onfocus="fillDropDown(this,true);" onchange="RemoveExtra(this,false);" onclick="fillDropDown(this,true);" AppendDataBoundItems="true" AutoPostBack="false">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                                    Enabled="true" CommandArgument='<%# Bind("CityMapping_Id") %>' OnClientClick="showCityMappingModal();">
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Select
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <%-- <asp:CheckBox ID="chkSelect" runat="server" CausesValidation="false" CommandName="Select" AutoPostBack="true"
                                            Enabled="true" HeaderText="Select" OnCheckedChanged="chkSelect_CheckedChanged" />--%>

                                                <input type="checkbox" runat="server" id="chkSelect" onclick="SelectedRow(this);" />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="hideColumn" HeaderStyle-CssClass="hideColumn">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnCityId" Value="" runat="server" />
                                                <asp:HiddenField ID="hdnCountryId" Value='<%# Bind("Country_Id") %>' runat="server" />
                                                <asp:HiddenField ID="hdnSupplierId" Value='<%# Bind("Country_Id") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" HorizontalAlign="Left" />


                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ContentTemplate>
</asp:UpdatePanel>

<br />
<div class="modal fade" id="modalHotelList" role="dialog">
    <div class="modal-dialog modal-md">
        <div class="modal-content">
            <div class="modal-header">
                <div class="panel-title">
                    <h4 class="modal-title">Hotel List for Selected City</h4>
                </div>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdvListOfHotel" runat="server" EmptyDataText="No Data Found" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" AllowPaging="true" DataKeyNames="CityMapping_Id,GoFor" AllowCustomPaging="true" OnPageIndexChanging="grdvListOfHotel_PageIndexChanging"
                            CssClass="fullWidth HotelList table table-hover table-striped">
                            <Columns>
                                <asp:TemplateField ItemStyle-CssClass="HotelListrowPadding">
                                    <ItemTemplate>
                                        <strong><%# Eval("HotelName")%></strong> &nbsp;&nbsp;
                                        <%# Eval("Address")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" HorizontalAlign="right" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
<br />
<!-- OPEN IN MODAL -->
<div class="modal fade" id="moCityMapping" role="dialog">
    <div class="modal-dialog modal-lg x-lg">
        <div class="modal-content">

            <div class="modal-header">
                <div class="panel-title">
                    <h4 class="modal-title">Update Supplier City Mapping</h4>
                </div>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel ID="UpdCityMapModal" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnFlag" runat="server" Value="" EnableViewState="false" />
                        <input type="hidden" id="hdnLat" enableviewstate="false" />
                        <input type="hidden" id="hdnLong" enableviewstate="false" />
                        <asp:FormView ID="frmEditCityMap" runat="server" DefaultMode="Insert" DataKeyNames="CityMapping_Id" OnItemCommand="frmEditCityMap_ItemCommand">
                            <EditItemTemplate>
                                <!-- should be edit item template, but using insert just to show UI -->
                                <div class="container">

                                    <div class="row">

                                        <div class="col-lg-3">

                                            <div class="panel panel-default">
                                                <div class="panel-heading">Supplier</div>
                                                <div class="panel-body">
                                                    <table class="table table-condensed">
                                                        <tbody>
                                                            <tr>
                                                                <td><strong>Supplier</strong></td>
                                                                <td>
                                                                    <asp:Label ID="lblSupplierName" runat="server" Text=""></asp:Label>&nbsp;
                                                                     <asp:Label ID="lblSupplierCode" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><strong>Country</strong></td>
                                                                <td>
                                                                    <asp:Label ID="lblSupCountryName" runat="server" Text=""></asp:Label>&nbsp;
                                                                     <asp:Label ID="lblSupCountryCode" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><strong>State</strong></td>
                                                                <td>
                                                                    <asp:Label ID="lblStateName" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><strong>City Name</strong></td>
                                                                <td>
                                                                    <asp:Label ID="lblCityName" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><strong>City Code</strong></td>
                                                                <td>
                                                                    <asp:Label ID="lblCityCode" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-lg-3">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">System</div>
                                                <div class="panel-body">


                                                    <div class="form-group">
                                                        <label class="control-label col-sm-5" for="ddlSystemCountryName">
                                                            Country
                                                            <asp:RequiredFieldValidator ID="vddlSystemCountryName" runat="server" ErrorMessage="*" ControlToValidate="ddlSystemCountryName" InitialValue="0" CssClass="text-danger" ValidationGroup="CityMappingPop"></asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ID="vddlSystemCountryNameAddCity" runat="server" ErrorMessage="*" ControlToValidate="ddlSystemCountryName" InitialValue="0" CssClass="text-danger" ValidationGroup="AddCity"></asp:RequiredFieldValidator>

                                                        </label>
                                                        <div class="col-sm-7">
                                                            <asp:DropDownList ID="ddlSystemCountryName" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSystemCountryName_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="control-label col-sm-5" for="txtSystemCountryCode">Code</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtSystemCountryCode" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="control-label col-sm-5" for="txtSystemStateName">State</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtSystemStateName" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="control-label col-sm-5" for="txtSystemStateCode">Code</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtSystemStateCode" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-5" for="ddlSystemCityName">City<asp:RequiredFieldValidator ID="vddlSystemCityName" runat="server" ErrorMessage="*" ControlToValidate="ddlSystemCityName" InitialValue="0" CssClass="text-danger" ValidationGroup="CityMappingPop"></asp:RequiredFieldValidator></label>
                                                        <div class="col-sm-7">
                                                            <asp:DropDownList ID="ddlSystemCityName" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSystemCityName_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-5" for="txtSystemCityCode">Code</label>
                                                        <div class="col-sm-7">
                                                            <asp:TextBox ID="txtSystemCityCode" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" style="text-align: right">
                                                        <div class="col-sm-12">
                                                            &nbsp;
                                                        </div>
                                                    </div>
                                                    <div class="form-group" style="text-align: right">
                                                        <div class="col-sm-12">
                                                            <asp:Button ID="btnAddCity" runat="server" CssClass="btn btn-primary btn-sm" Text="Add City" CommandName="OpenAddCity" CausesValidation="true" ValidationGroup="AddCity" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-12">
                                                            <div id="dvMsg2" runat="server" style="display: none;"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="col-lg-3">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Status</div>
                                                <div class="panel-body">

                                                    <div class="form-group">
                                                        <label for="ddlStatus">
                                                            Status
                                                            <asp:RequiredFieldValidator ID="vddlStatus" runat="server" ErrorMessage="*" ControlToValidate="ddlStatus" InitialValue="0" CssClass="text-danger" ValidationGroup="CityMappingPop"></asp:RequiredFieldValidator>
                                                        </label>
                                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true" onchange="ddlStatusChanged(this);">
                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>

                                                    <div class="form-group">
                                                        <label for="txtSystemRemark">Remark</label>

                                                        <asp:TextBox ID="txtSystemRemark" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control"></asp:TextBox>

                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-lg-3">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Actions</div>
                                                <div class="panel-body" style="padding-bottom: 0px;">
                                                    <div class="form-group" style="padding-bottom: 5px;">
                                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm" Text="Save" CommandName="Add" ValidationGroup="CityMappingPop" CausesValidation="true" OnClientClick="ddlStatusChanged(ddlSystemCountryName);" />
                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-sm" Text="Cancel" CommandName="Cancel" data-dismiss="modal" CausesValidation="false" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Hotel List for Selected City</div>
                                                <div class="panel-body">
                                                    <div class="form-group HotelInfo">
                                                        <asp:GridView runat="server" EmptyDataText="No Data Found" DataKeyNames="CityMapping_Id,GoFor" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" ID="grdvListOfHotelOnSelection" AllowPaging="true" AllowCustomPaging="true" OnPageIndexChanging="grdvListOfHotelOnSelection_PageIndexChanging"
                                                            CssClass="fullWidth HotelList table table-hover table-striped">
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-CssClass="HotelListrowPadding">
                                                                    <ItemTemplate>
                                                                        <strong><%# Eval("HotelName")%></strong> &nbsp;&nbsp;
                                                                           <%# Eval("Address")%>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerStyle CssClass="pagination-ys" HorizontalAlign="right" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" id="dvAddCity" runat="server" style="display: none;">
                                        <div class="col-lg-3">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Add City</div>
                                                <div class="panel-body">
                                                    <div class="form-group" style="display: none;">
                                                        <label class="control-label col-sm-4" for="pac-input">
                                                            &nbsp;</label>
                                                        <div class="col-sm-8">
                                                            <input id="pac-input" type="text" class="form-control" />
                                                            <asp:TextBox ID="txtAddCityPlaceId" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4" for="txtAddCityName">
                                                            Name
                                                            <asp:RequiredFieldValidator ID="vtxtAddCityName" runat="server" ErrorMessage="*" ControlToValidate="txtAddCityName" CssClass="text-danger" ValidationGroup="AddCityForm"></asp:RequiredFieldValidator>
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtAddCityName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" style="display: none;">
                                                        <label class="control-label col-sm-4" for="txtAddCode">Code</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtAddCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4" for="txtAddCityCCode">Country</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtAddCityCName" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4" for="txtAddCityCCode">Code</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtAddCityCCode" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4" for="ddlStatus">
                                                            State</label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlAddCityState" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlAddCityState_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4" for="txtAddCitySCode">Code</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtAddCitySCode" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-12">
                                                            &nbsp;
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-12">
                                                            <input type="button" class="btn btn-primary btn-sm" value="Locate On Map" onclick="callmap();" />
                                                            <asp:Button ID="btnAddCityForm" runat="server" CssClass="btn btn-primary btn-sm" Text="Add City" CommandName="AddCity" CausesValidation="true" ValidationGroup="AddCityForm" OnClientClick="closeDvMsg2();" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="panel panel-default">
                                                <div class="form-group form-inline">
                                                    Similar Records&nbsp;&nbsp;
                                                    <div class="input-group">
                                                        <label class="input-group-addon" for="ddlSimilarProducts">Page Size</label>
                                                        <asp:DropDownList ID="ddlSimilarProducts" runat="server" CssClass="form-control col-lg-3" AutoPostBack="true" OnSelectedIndexChanged="ddlSimilarProducts_SelectedIndexChanged">
                                                            <asp:ListItem Text="5" Value="5" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                            <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                            <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <asp:GridView ID="grdSimilarProducts" runat="server" AllowPaging="True" AllowCustomPaging="true" AutoGenerateColumns="False"
                                                        EmptyDataText="No Static Updates" CssClass="table table-hover table-striped" OnPageIndexChanging="grdSimilarProducts_PageIndexChanging"
                                                        DataKeyNames="CityName" ShowHeader="false" ShowFooter="false">
                                                        <Columns>
                                                            <asp:BoundField DataField="CityName" HeaderText="City Name" />
                                                        </Columns>
                                                        <PagerStyle CssClass="pagination-ys" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Map Lookup</div>
                                                <div class="panel-body">
                                                    <div class="form-group">
                                                        <div id="map" style="height: 400px"></div>
                                                        <div id="infowindow-content">
                                                            <span id="place-name" class="title"></span>
                                                            <br>
                                                            Place ID <span id="place-id"></span>
                                                            <br>
                                                            <span id="place-address"></span>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </EditItemTemplate>
                        </asp:FormView>
                        <div class="row" runat="server" id="Div1">
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <div id="dvMsg" runat="server" style="display: none;"></div>
                                </div>
                                <div class="panel-default" runat="server" id="dvMatchingRecords">
                                    <div class="panel-heading">
                                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="panel-body">
                                        <div class="form-group form-inline">
                                            <div class="input-group">
                                                <label class="input-group-addon" for="ddlProductBasedPageSize">Page Size</label>
                                                <asp:DropDownList ID="ddlMatchingPageSize" runat="server" CssClass="form-control col-lg-3" AutoPostBack="true" OnSelectedIndexChanged="ddlMatchingPageSize_SelectedIndexChanged">
                                                    <asp:ListItem Text="5" Value="5" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="input-group">&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                            <div class="input-group">
                                                <label class="input-group-addon" for="ddlProductBasedPageSize">Status</label>
                                                <asp:DropDownList ID="ddlMatchingStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlMatchingStatus_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="input-group">&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                            <div class="input-group">&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <asp:CheckBox runat="server" ID="ckboxIsExactMatch" CssClass="form-control" AutoPostBack="true" OnCheckedChanged="ckboxIsExactMatch_CheckedChanged" />
                                                    <%--<input type="checkbox" aria-label="Checkbox for following text input" cssclass="form-control" runat="server" id="ckboxIsExactMatch" />--%>
                                                </span>
                                                <label class="input-group-addon" for="ckboxIsExactMatch">Match Entire Word</label>
                                            </div>
                                            <div class="form-group pull-right">
                                                <asp:Button ID="btnMatchedMapSelected" Visible="false" runat="server" CssClass="btn btn-primary btn-sm" Text="Map Selected" OnClick="btnMatchedMapSelected_Click" CommandName="MapSelected" CausesValidation="false" />
                                                <asp:Button ID="btnMatchedMapAll" Visible="false" runat="server" CssClass="btn btn-primary btn-sm" Text="Map All" CommandName="MapAll" OnClick="btnMatchedMapAll_Click" CausesValidation="false" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:GridView ID="grdMatchingCity" runat="server" AllowPaging="True" AllowCustomPaging="true" AutoGenerateColumns="False"
                                                EmptyDataText="No Static Updates" CssClass="table table-hover table-striped" DataKeyNames="CityMapping_Id,Supplier_Id,Country_Id,City_Id"
                                                OnSelectedIndexChanged="grdMatchingCity_SelectedIndexChanged" OnPageIndexChanging="grdMatchingCity_PageIndexChanging" OnRowCommand="grdMatchingCity_RowCommand"
                                                OnRowDataBound="grdMatchingCity_RowDataBound" OnDataBound="grdMatchingCity_DataBound">

                                                <Columns>
                                                    <asp:BoundField DataField="MapId" HeaderText="Map Id" />
                                                    <asp:BoundField DataField="SupplierName" HeaderText="Name" />
                                                    <asp:BoundField DataField="CountryCode" HeaderText="Country Code" />
                                                    <asp:BoundField DataField="CountryName" HeaderText="Country Name" />
                                                    <asp:TemplateField ShowHeader="true" HeaderText="State Name (State Code)">
                                                        <ItemTemplate>
                                                            <%# Eval("StateName") + (!string.IsNullOrWhiteSpace(Convert.ToString(Eval("StateCode"))) ? "(" + Eval("StateCode") + ")" : string.Empty) %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:BoundField DataField="StateNameWithCode" HeaderText="State Name (State Code)" />--%>
                                                    <asp:TemplateField HeaderText="City Code" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnSelectCityCode" Text='<%# Bind("CityCode") %>' runat="server" CausesValidation="false" CommandName="SelectCityCode" CommandArgument='<%# Bind("CityMapping_Id") %>' OnClientClick="showSelectCityCodeModal();">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="City Name" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnSelectCityName" Text='<%# Bind("CityName") %>' runat="server" CausesValidation="false" CommandName="SelectCityName" CommandArgument='<%# Bind("CityMapping_Id") %>' OnClientClick="showSelectCityCodeModal();">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField DataField="CityCode" HeaderText="City Code" />
                                                    <asp:BoundField DataField="CityName" HeaderText="City Name" />--%>
                                                    <asp:BoundField DataField="MasterCountryCode" HeaderText="Country Code">
                                                        <HeaderStyle BackColor="Turquoise" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="MasterCountryName" HeaderText="Country Name">
                                                        <HeaderStyle BackColor="Turquoise" />
                                                    </asp:BoundField>
                                                    <%--<asp:BoundField DataField="StateName" HeaderText="State Name (State Code)">
                                                        <HeaderStyle BackColor="Turquoise" />
                                                    </asp:BoundField>--%>
                                                    <asp:TemplateField ShowHeader="true" HeaderStyle-BackColor="Turquoise" HeaderText="State Name (State Code)">
                                                        <ItemTemplate>
                                                            <span aria-hidden="true"><%# Eval("MasterStateName") + (!string.IsNullOrWhiteSpace(Convert.ToString(Eval("MasterStateCode"))) ? "(" + Eval("MasterStateCode") + ")" : string.Empty) %></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="MasterCityCode" HeaderText="City Code">
                                                        <HeaderStyle BackColor="Turquoise" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Master_CityName" HeaderText="City Name">
                                                        <HeaderStyle BackColor="Turquoise" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                                    <asp:TemplateField ShowHeader="false">
                                                        <ItemTemplate>
                                                            <%-- <asp:CheckBox ID="chkSelect" runat="server" CausesValidation="false" CommandName="Select" AutoPostBack="false"
                                                                Enabled="true" HeaderText="Select" />--%>
                                                            <input type="checkbox" runat="server" id="chkSelect" onclick="MatchedSelect(this);" />

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

            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
