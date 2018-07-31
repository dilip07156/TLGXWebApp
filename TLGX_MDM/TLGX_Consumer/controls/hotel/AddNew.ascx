<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddNew.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.AddNew" %>

<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDhxUUgHmu48Zv0_ECSms00t9OzxZkE1h0&libraries=places"></script>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<style>
    /* Always set the map height explicitly to define the size of the div
       * element that contains the map. */
    #map {
        height: 100%;
    }
    /* Optional: Makes the sample page fill the window. */

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
</style>


<script type="text/javascript">
    function showModal() {
        $("#myModal").modal('show');
    }

    $(function () {
        $("#btnShow").click(function () {
            showModal();
        });
    });
    function formreset() {
        var elements = document.getElementsByTagName("input");

        for (var ii = 0; ii < elements.length; ii++) {
            if (elements[ii].type == "text") {
                elements[ii].value = "";
            }
        }
    }


    function callmap() {
        var geocoder = new google.maps.Geocoder();
        var infowindow = new google.maps.InfoWindow;

        var vrmap = document.getElementById("MainContent_AddNew_map");
        var inputProductName = document.getElementById('MainContent_AddNew_txtHotelName');
        var inputStreet = document.getElementById('MainContent_AddNew_txtStreet');
        var inputStreet2 = document.getElementById('MainContent_AddNew_txtStreet2');
        var inputPostCode = document.getElementById('MainContent_AddNew_txtPostalCode');
        var inputCountry = document.getElementById('MainContent_AddNew_ddlAddCountry');
        var inputCity = document.getElementById('MainContent_AddNew_ddlAddCity');
        var inputPlaceId = document.getElementById('MainContent_AddNew_txtAddCityPlaceId');
        var hdnStreet = document.getElementById('MainContent_AddNew_hdnStreet');
        var hdnStreet2 = document.getElementById('MainContent_AddNew_hdnStreet2');
        var hdnCity = document.getElementById('MainContent_AddNew_hdnCity');
        var hdnState = document.getElementById('MainContent_AddNew_hdnState');
        var hdnCountry = document.getElementById('MainContent_AddNew_hdnCountry');
        var hdnPostCode = document.getElementById('MainContent_AddNew_hdnPostCode');
        var hdnLat = document.getElementById('MainContent_AddNew_hdnLat');
        var hdnLng = document.getElementById('MainContent_AddNew_hdnLng');
        var input = document.getElementById('pac-input');
        var vrZoom = 18;
        if (vrmap == null) {
            vrmap = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_ucAddNew_map");

            if (vrmap != null) {
                inputProductName = document.getElementById('MainContent_searchAccoMapping_UpdateSupplierProductMapping_ucAddNew_txtHotelName');
                inputStreet = document.getElementById('MainContent_searchAccoMapping_UpdateSupplierProductMapping_ucAddNew_txtStreet');
                inputStreet2 = document.getElementById('MainContent_searchAccoMapping_UpdateSupplierProductMapping_ucAddNew_txtStreet2');
                inputPostCode = document.getElementById('MainContent_searchAccoMapping_UpdateSupplierProductMapping_ucAddNew_txtPostalCode');
                inputCountry = document.getElementById('MainContent_searchAccoMapping_UpdateSupplierProductMapping_ucAddNew_ddlAddCountry');
                inputCity = document.getElementById('MainContent_searchAccoMapping_UpdateSupplierProductMapping_ucAddNew_ddlAddCity');
                inputPlaceId = document.getElementById('MainContent_searchAccoMapping_UpdateSupplierProductMapping_ucAddNew_txtAddCityPlaceId');
                hdnStreet = document.getElementById('MainContent_searchAccoMapping_UpdateSupplierProductMapping_ucAddNew_hdnStreet');
                hdnStreet2 = document.getElementById('MainContent_searchAccoMapping_UpdateSupplierProductMapping_ucAddNew_hdnStreet2');
                hdnCity = document.getElementById('MainContent_searchAccoMapping_UpdateSupplierProductMapping_ucAddNew_hdnCity');
                hdnState = document.getElementById('MainContent_searchAccoMapping_UpdateSupplierProductMapping_ucAddNew_hdnState');
                hdnCountry = document.getElementById('MainContent_searchAccoMapping_UpdateSupplierProductMapping_ucAddNew_hdnCountry');
                hdnPostCode = document.getElementById('MainContent_searchAccoMapping_UpdateSupplierProductMapping_ucAddNew_hdnPostCode');
                hdnLat = document.getElementById('MainContent_searchAccoMapping_UpdateSupplierProductMapping_ucAddNew_hdnLat');
                hdnLng = document.getElementById('MainContent_searchAccoMapping_UpdateSupplierProductMapping_ucAddNew_hdnLng');
                input = document.getElementById('pac-input');
                vrZoom = 16;
            }
        }
        hdnStreet.value = "";
        hdnStreet2.value = "";
        hdnCity.value = "";
        hdnState.value = "";
        hdnCountry.value = "";
        hdnPostCode.value = "";
        hdnLat.value = "";
        hdnLat.value = "";
        inputPlaceId.value = "";
        var streetname = "";
        var streetname2 = "";
        var postcode = "";
        var postcode = "";
        var country = "";
        var city = "";
        var address = inputProductName.value;
        //var con = document.getElementById('txtCon').value;
        //var city = document.getElementById('txtCity').value;
        if (inputStreet.value != "") {
            address = address + "," + inputStreet.value;
        }
        if (inputStreet2.value != "") {
            address = address + "," + inputStreet2.value;
        }
        if (inputPostCode.value != "") {
            address = address + "," + inputPostCode.value;
        }
        if (inputCity.value != "0") {
            address = address + "," + inputCity.options[inputCity.selectedIndex].text;
        }
        if (inputCountry.value != "0") {
            address = address + "," + inputCountry.options[inputCountry.selectedIndex].text;
        }
        var com = address;
        var markercontent = com;
        var mapAddress = "";
        geocoder.geocode({ 'address': com }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                var x = results[0].geometry.location.lat();
                var y = results[0].geometry.location.lng();
                hdnLat.value = x;
                hdnLng.value = y;
                inputPlaceId.value = results[0].place_id;

                var latlng = new google.maps.LatLng(x, y);
                mapAddress = new Array(results[0].address_components);
                var addressArray, addressTypes;
                var long_name, addressType, street_number, route, sublocality, locality, administrative_area_level_2, administrative_area_level_1, country, postal_code;
                if (mapAddress != null) {
                    if (mapAddress.length > 0) {
                        var arrayList = new Array(mapAddress[0])[0];
                        for (var i = 0 ; i < arrayList.length ; i++) {
                            addressTypes = new Array(arrayList[i].types);
                            addressType = addressTypes[0][0];
                            if (addressType != null) {
                                long_name = arrayList[i].long_name;
                                if (addressType == "street_number") {
                                    hdnStreet.value = long_name;

                                }
                                if (addressType == "route") {
                                    hdnStreet.value = hdnStreet.value + "," + long_name;
                                }
                                if (addressType == "sublocality") {
                                    hdnStreet2.value = long_name;
                                }
                                if (addressType == "locality") {
                                    hdnCity.value = long_name;
                                }
                                //if (addressType == "administrative_area_level_2") {
                                //    administrative_area_level_2 = long_name;
                                //}
                                if (addressType == "administrative_area_level_1") {
                                    hdnState.value = long_name;
                                }
                                if (addressType == "country") {
                                    hdnCountry.value = long_name;
                                }
                                if (addressType == "postal_code") {
                                    hdnPostCode.value = long_name;
                                }
                            }
                        }
                        if (inputStreet.value == "") {
                            if (hdnStreet.value != "") {
                                inputStreet.value = hdnStreet.value;
                            }
                        }
                        if (inputStreet2.value == "") {
                            if (hdnStreet2.value != "") {
                                inputStreet2.value = hdnStreet2.value;
                            }
                        }
                        if (inputPostCode.value == "") {
                            if (hdnPostCode.value != "") {
                                inputPostCode.value = hdnPostCode.value;
                            }
                        }
                    }
                }
                var myOptions = {
                    zoom: vrZoom,
                    center: latlng,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };
                map = new google.maps.Map(vrmap, myOptions);

                var service = new google.maps.places.PlacesService(map);
                service.getDetails({
                    placeId: results[0].place_id
                }, function (place, status1) {
                    if (status1 === google.maps.places.PlacesServiceStatus.OK) {
                        markercontent = "<div><strong>" + place.name + "</strong><br>" +
                        "Place ID: " + place.place_id + "<br>" +
                        place.formatted_address + "</div>";
                        infowindow.setContent(markercontent);
                    }
                    else {
                        geocoder.geocode({ 'placeId': results[0].place_id }, function (details, status2) {
                            if (status2 === 'OK') {
                                if (details[0]) {
                                    markercontent = details[0].formatted_address;
                                    infowindow.setContent(markercontent);
                                }
                                else {
                                    infowindow.setContent(markercontent);
                                }
                            }
                            else {
                                infowindow.setContent(markercontent);
                            }
                        });
                    }
                });

                var marker = new google.maps.Marker({
                    position: new google.maps.LatLng(x, y),
                    map: map,
                    title: com
                });

                //var infowindow = new google.maps.InfoWindow({
                //    content: com
                //});
                infowindow.open(map, marker);
                google.maps.event.addDomListener(window, 'load');
            } else {
                res.innerHTML = "Enter correct Details: " + status;
            }
        });
    }
</script>


<div class="container">
    <div class="row">
        <div class="col-lg-12" id="dvvlsSumm" runat="server">
            <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="HotelOverView" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
        </div>
    </div>
</div>

<div class="container">
    <div class="row">
        <div class="col-lg-12" id="dvExistingRecords" runat="server">
            <div class="panel panel-default">
                <div class="panel-heading">Existing Hotels</div>
                <div class="panel-body">
                    <div class="form-group" id="dvGridExist" runat="server">
                        <div class="control-label col-sm-12">
                            <asp:GridView ID="gvGridExist" runat="server" AllowPaging="false" AllowCustomPaging="false" AutoGenerateColumns="False" DataKeyNames="AccomodationId" CssClass="table table-hover table-striped"
                                OnRowCommand="gvGridExist_RowCommand" OnDataBound="gvGridExist_DataBound">
                                <Columns>
                                    <asp:BoundField DataField="CompanyHotelId" HeaderText="Hotel Id" InsertVisible="False" ReadOnly="True" SortExpression="CompanyHotelId" />
                                    <asp:BoundField DataField="Country" HeaderText="Country Name" SortExpression="Country" />
                                    <asp:BoundField DataField="City" HeaderText="City Name" SortExpression="City" />
                                    <asp:BoundField DataField="HotelName" HeaderText="Hotel Name" SortExpression="HotelName" />
                                    <asp:BoundField DataField="FullAddress" HeaderText="Address" SortExpression="FullAddress" />
                                    <asp:BoundField DataField="MapCount" HeaderText="No. of Map" SortExpression="MapCount" />
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                                Enabled="true" CommandArgument='<%# Bind("AccomodationId") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Use This
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDeactivate" runat="server" CausesValidation="false" CommandName="OpenDeactivate" CssClass="btn btn-default"
                                                Enabled="true" CommandArgument='<%# Bind("AccomodationId") %>'>
                                                <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Deactivate
                                            </asp:LinkButton><br />
                                            <asp:DropDownList ID="ddlExistingHotels" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="false">
                                                <asp:ListItem Value="0">Use New</asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <asp:TextBox ID="txtDeactiveRemark" runat="server" CssClass="form-control" />
                                            <br />
                                            <asp:LinkButton ID="btnProceedDeactivate" runat="server" CausesValidation="false" CommandName="ProceedDeactivate" CssClass="btn btn-default"
                                                Enabled="true" CommandArgument='<%# Bind("AccomodationId") %>'>
                                                <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Deactivate
                                            </asp:LinkButton><br />
                                            <asp:LinkButton ID="btnCancel" runat="server" CausesValidation="false" CommandName="CancelDeactivate" CssClass="btn btn-default"
                                                Enabled="true" CommandArgument='<%# Bind("AccomodationId") %>'>
                                                <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Cancel
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-4">
            <div class="panel panel-default">
                <div class="panel-heading">Add Hotel</div>
                <div class="panel-body">
                    <div class="form-group" style="display: none;">
                        <label class="control-label col-sm-4" for="pac-input">
                            &nbsp;</label>
                        <div class="col-sm-8">
                            <input id="pac-input" type="text" class="form-control" />
                            <asp:TextBox ID="txtAddCityPlaceId" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:HiddenField ID="hdnStreet" runat="server" />
                            <asp:HiddenField ID="hdnStreet2" runat="server" />
                            <asp:HiddenField ID="hdnCity" runat="server" />
                            <asp:HiddenField ID="hdnState" runat="server" />
                            <asp:HiddenField ID="hdnCountry" runat="server" />
                            <asp:HiddenField ID="hdnPostCode" runat="server" />
                            <asp:HiddenField ID="hdnLat" runat="server" />
                            <asp:HiddenField ID="hdnLng" runat="server" />
                        </div>
                    </div>
                    <div class="form-group" id="dvAddProductCategorySubType" runat="server">
                        <label class="control-label-mand col-lg-4" for="ddlAddProductCategorySubType">
                            Subcategory
                            <asp:RequiredFieldValidator ID="vddlAddProductCategorySubType" runat="server" ErrorMessage="Please select SubCategory" Text="*" ControlToValidate="ddlAddProductCategorySubType" InitialValue="0" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlAddProductCategorySubType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label-mand col-lg-4" for="txtHotelName">
                            Name
                            <asp:RequiredFieldValidator ID="vtxtHotelName" runat="server" ErrorMessage="Please enter Hotel Name" Text="*" ControlToValidate="txtHotelName" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtHotelName" runat="server" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label-mand col-lg-4" for="txtStreet">
                            Street
                            <asp:RequiredFieldValidator ID="vtxtStreet" runat="server" ErrorMessage="Please enter Street" Text="*" ControlToValidate="txtStreet" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator></label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtStreet" runat="server" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label-mand col-lg-4" for="txtStreet2">Street 2</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtStreet2" runat="server" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label-mand col-lg-4" for="txtPostalCode">
                            Postal Code
                            <asp:RequiredFieldValidator ID="vtxtPostalCode" runat="server" ErrorMessage="Please enter Postal Code" Text="*" ControlToValidate="txtPostalCode" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator></label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label-mand col-lg-4" for="ddlAddCountry">
                            Country
                            <asp:RequiredFieldValidator ID="vddlAddCountry" runat="server" ErrorMessage="Please select Country" Text="*" ControlToValidate="ddlAddCountry" InitialValue="0" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator>

                        </label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlAddCountry" AutoPostBack="true" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlAddCountry_SelectedIndexChanged">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                      <div class="form-group">
                        <label class="control-label col-lg-4" for="ddlAddState">
                            State
                            <%--<asp:RequiredFieldValidator ID="vddlAddState" runat="server" ErrorMessage="Please select State" Text="*" ControlToValidate="ddlAddState" InitialValue="0" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator>--%>

                        </label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlAddState" runat="server" AutoPostBack="true" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlAddState_SelectedIndexChanged">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label-mand col-lg-4" for="ddlAddCity">
                            City
                            <asp:RequiredFieldValidator ID="vddlAddCity" runat="server" ErrorMessage="Please select City" Text="*" ControlToValidate="ddlAddCity" InitialValue="0" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator></label>
                        <div class="col-lg-8">
                            <asp:DropDownList ID="ddlAddCity" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-default" id="dvKeyFacts" runat="server">
                <div class="panel-heading">Key Facts</div>
                <div class="panel-body">

                    <div class="form-group">
                        <label class="control-label-mand col-sm-6" for="txtCheckinTime">
                            Check In
                            <asp:RequiredFieldValidator ID="vtxtCheckinTime" runat="server" ErrorMessage="Please enter CheckIn Time" Text="*" ControlToValidate="txtCheckinTime" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtCheckinTime" runat="server" ErrorMessage="Invalid Check In Time." Text="*" ControlToValidate="txtCheckinTime" CssClass="text-danger" ValidationGroup="HotelOverView" ValidationExpression="^(?:[01][0-9]|2[0-3]):[0-5][0-9]$"></asp:RegularExpressionValidator>
                        </label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtCheckinTime" runat="server" class="form-control" />
                            <cc1:MaskedEditExtender ID="txtCheckinTime_MaskEditExtender" runat="server" AcceptAMPM="false"
                                Mask="99:99" MaskType="Time" PromptCharacter="_" TargetControlID="txtCheckinTime"
                                UserTimeFormat="TwentyFourHour"></cc1:MaskedEditExtender>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label-mand col-sm-6" for="txtCheckOut">
                            Check Out
                            <asp:RequiredFieldValidator ID="vtxtCheckOut" runat="server" ErrorMessage="Please enter CheckOut Time" Text="*" ControlToValidate="txtCheckOut" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtCheckOut" runat="server" ErrorMessage="Invalid Check Out Time." Text="*" ControlToValidate="txtCheckOut" CssClass="text-danger" ValidationGroup="HotelOverView" ValidationExpression="^(?:[01][0-9]|2[0-3]):[0-5][0-9]$"></asp:RegularExpressionValidator>
                        </label>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtCheckOut" runat="server" class="form-control" />
                            <cc1:MaskedEditExtender ID="txtCheckOut_MaskEditExtender" runat="server" AcceptAMPM="false"
                                Mask="99:99" MaskType="Time" PromptCharacter="_" TargetControlID="txtCheckOut"
                                UserTimeFormat="TwentyFourHour"></cc1:MaskedEditExtender>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label-mand col-sm-6" for="ddlStarRating">
                            Star Rating
                            <asp:RequiredFieldValidator ID="vddlStarRating" runat="server" ErrorMessage="Please select Star Rating" Text="*" ControlToValidate="ddlStarRating" InitialValue="0" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator>
                        </label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlStarRating" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <asp:LinkButton ID="btnMapLookup" runat="server" CausesValidation="false" CommandName="SaveProduct" Text="Locate On Map" CssClass="btn btn-primary btn-sm" OnCommand="btnMapLookup_Command" OnClientClick="callmap();" />

            <input type="button" class="btn btn-primary btn-sm" value="Locate On Map" onclick="callmap();" style="display: none" />
            <asp:LinkButton ID="btnAdd" runat="server" CausesValidation="True" CommandName="SaveProduct" Text="Add New Product" CssClass="btn btn-primary btn-sm" ValidationGroup="HotelOverView" OnCommand="btnAdd_Command" OnClientClick="callmap();" />
            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" CausesValidation="false" OnClientClick="formreset();" OnClick="btnReset_Click" />

        </div>
        <div id="dvMainMapDv" runat="server">
            <div class="panel panel-default">
                <div class="panel-heading">Map Lookup</div>
                <div class="panel-body">
                    <div class="form-group">
                        <div id="map" style="height: 360px" runat="server"></div>
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
    <div class="row">
        <div class="col-lg-4">


            <!-- Modal -->
            <div class="modal fade" id="myModal" role="dialog">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <div class="panel panel-default">
                                <h4 class="modal-title"></h4>
                                <div class="panel-heading">
                                    Add Hotel
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                </div>
                            </div>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <asp:Label ID="lblMessage" runat="server" Text="Product already Exists" class="control-label col-sm-12"></asp:Label>
                            </div>
                            <div class="form-group">
                                <div id="dvGrid" runat="server" class="control-label col-sm-12">
                                    <asp:GridView ID="grdSearchResults" runat="server" AllowPaging="false" AllowCustomPaging="false" AutoGenerateColumns="False" DataKeyNames="AccomodationId" CssClass="table table-hover table-striped">
                                        <Columns>
                                            <asp:BoundField DataField="CompanyHotelId" HeaderText="Hotel Id" InsertVisible="False" ReadOnly="True" SortExpression="CompanyHotelId" />
                                            <asp:BoundField DataField="Country" HeaderText="Country Name" SortExpression="Country" />
                                            <asp:BoundField DataField="City" HeaderText="City Name" SortExpression="City" />
                                            <asp:BoundField DataField="HotelName" HeaderText="Hotel Name" SortExpression="HotelName" />
                                            <asp:HyperLinkField DataNavigateUrlFields="AccomodationId" DataNavigateUrlFormatString="~/hotels/manage.aspx?Hotel_Id={0}" Text="Select" NavigateUrl="~/hotels/manage.aspx" HeaderText="Hotel" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary btn-sm">OK</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
