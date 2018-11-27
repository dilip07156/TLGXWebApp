<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="overview.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.overview" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/controls/hotel/contacts.ascx" TagPrefix="uc1" TagName="contacts" %>
<%@ Register Src="~/controls/hotel/DynamicAttributesForHotel.ascx" TagPrefix="uc2" TagName="DynamicAttributesForHotel" %>
<%@ Register Src="~/controls/hotel/AddressCheck.ascx" TagPrefix="uc1" TagName="AddressCheck" %>

<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBAbYHJn_5Kubmfa4-nYyAf_WpHB9mbfvc"></script>

<script type="text/javascript">

    function showModal() {
        $("#myModal").modal('show');
    }

    $(function () {
        $("#btnShow").click(function () {
            showModal();
        });
    });

</script>

<script type="text/javascript">

    $(function () {
        $("#btnAddressLookUp").click(showAddressCheckModal);
    });

    function showAddressCheckModal() {
        $("#modAddressCheck").modal('show');
    }


</script>

<script type="text/javascript">

    var map;

    function initMap() {

        var latitude = parseFloat(document.getElementById("MainContent_overview_frmHotelOverview_txtHotelLat").value);
        var longitude = parseFloat(document.getElementById("MainContent_overview_frmHotelOverview_txtHotelLon").value);
        var myLatlng = { lat: latitude, lng: longitude };

        map = new google.maps.Map(document.getElementById("mapDiv"), {
            center: myLatlng,
            zoom: 18
        });

        var infowindow = new google.maps.InfoWindow({
            content: document.getElementById("MainContent_overview_frmHotelOverview_txtDisplayName").value
        });

        var marker = new google.maps.Marker({
            position: myLatlng,
            map: map,
            title: document.getElementById("MainContent_overview_frmHotelOverview_txtDisplayName").value
        });

        marker.addListener('click', function () {
            infowindow.open(map, marker);
        });

        map.setCenter(myLatlng);

    };

    //$(function () {
    //    $("#btnShowMaps").click(initMap);
    //});

    $(document).ready(function () {
        $('#myGeoLocModal').on('shown.bs.modal', function () {
            initMap();
        });
        if (typeof message != 'undefined' && typeof messagetype != 'undefined' && typeof divid != 'undefined') {
            ShowMessage(message, messagetype, divid);
        }
    });

    //$(function () {
    //    $('#datetimepicker1').datetimepicker();
    //});

</script>

<%--<script type="text/javascript">

    function initAddressCheckMap(lat,lng,div,hotel) {

        var latitude = parseFloat(lat);
        var longitude = parseFloat(lng);
        var myLatlng = { lat: latitude, lng: longitude };

        var map = new google.maps.Map(document.getElementById(div), {
            center: myLatlng,
            zoom: 18
        });

        var infowindow = new google.maps.InfoWindow({
            content: document.getElementById(hotel).value
        });

        var marker = new google.maps.Marker({
            position: myLatlng,
            map: map,
            title: document.getElementById(hotel).value
        });

        marker.addListener('click', function () {
            infowindow.open(map, marker);
        });

        map.setCenter(myLatlng);

    };

</script>--%>

<script type="text/javascript">
    function ShowMessage(message, messagetype, divid) {
        var cssclass;
        switch (messagetype) {
            case 'Success':
                cssclass = 'alert-success'
                break;
            case 'Error':
                cssclass = 'alert-danger'
                break;
            case 'Warning':
                cssclass = 'alert-warning'
                break;
            default:
                cssclass = 'alert-info'
        }
        $(divid).append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');
    }
</script>
<div class="container">
    <div class="row">
        <div class="col-lg-12">
            <div id="dvMsg" runat="server" style="display: none;"></div>
        </div>
    </div>
</div>
<asp:FormView ID="frmHotelOverview" runat="server" DataKeyNames="Accommodation_Id" DefaultMode="Edit" OnItemCommand="frmHotelOverview_ItemCommand">

    <HeaderTemplate>
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="HotelOverView" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                </div>
            </div>
        </div>
    </HeaderTemplate>

    <EditItemTemplate>

        <div class="container">

            <div class="row">

                <div class="col-lg-8">

                    <div class="panel panel-default">
                        <div class="panel-heading">Hotel Name and Key Facts</div>
                        <div class="panel-body">
                            <div class="form-group" style="display: none">
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtHotelID" runat="server" Text='<%# Bind("Accommodation_Id") %>' CssClass="form-control" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label-mand col-sm-4" for="txtHotelName">
                                    Name
                                    <asp:RequiredFieldValidator ID="vtxtHotelName" runat="server" ErrorMessage="Please enter Hotel Name" Text="*" ControlToValidate="txtHotelName" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator>
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtHotelName" runat="server" Text='<%# Bind("HotelName") %>' CssClass="form-control" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label-mand col-sm-4" for="txtDisplayName">
                                    Display
                                    <asp:RequiredFieldValidator ID="vtxtDisplayName" runat="server" ErrorMessage="Please enter Hotel Display Name" Text="*" ControlToValidate="txtDisplayName" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtDisplayName" runat="server" Text='<%# Bind("DisplayName") %>' CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="panel panel-default">

                        <div class="panel-heading">Hotel Address</div>
                        <div class="panel-body">

                            <div class="form-group">
                                <label class="control-label-mand col-sm-4" for="txtStreet">
                                    Street
                                    <asp:RequiredFieldValidator ID="vtxtStreet" runat="server" ErrorMessage="Please enter Street" Text="*" ControlToValidate="txtStreet" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtStreet" runat="server" CssClass="form-control" Text='<%# Bind("StreetName") %>' />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-4" for="txtStreet2">Street 2</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtStreet2" runat="server" CssClass="form-control" Text='<%# Bind("StreetNumber") %>' />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-4" for="txtStreet3">Street 3</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtStreet3" runat="server" CssClass="form-control" Text='<%# Bind("Street3") %>' />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-4" for="txtStreet4">Street 4</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtStreet4" runat="server" CssClass="form-control" Text='<%# Bind("Street4") %>' />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-4" for="txtStreet5">Street 5</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtStreet5" runat="server" CssClass="form-control" Text='<%# Bind("Street5") %>' />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-4" for="ddlSuburbs">Suburbs / Downtown</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlSuburbs" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem>-Select-</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label-mand col-sm-4" for="txtPostalCode">
                                    Postal Code
                                    <asp:RequiredFieldValidator ID="vtxtPostalCode" runat="server" ErrorMessage="Please enter Postal Code" Text="*" ControlToValidate="txtPostalCode" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control" Text='<%# Bind("PostalCode") %>' />
                                </div>
                            </div>

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <label class="control-label-mand col-sm-4" for="ddlCountry">
                                            Country
                                            <asp:RequiredFieldValidator ID="vddlCountry" runat="server" ErrorMessage="Please select Country" Text="*" ControlToValidate="ddlCountry" InitialValue="0" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="ddlState">State / Region</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem>-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label-mand col-sm-4" for="ddlCity">
                                            City
                                            <asp:RequiredFieldValidator ID="vddlCity" runat="server" ErrorMessage="Please select City" Text="*" ControlToValidate="ddlCity" InitialValue="0" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="ddlArea">
                                            Area
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlArea" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label col-sm-4" for="ddlLocation">
                                            Location
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>

                                            <br />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="form-group">
                                <label class="control-label col-sm-4" for="btnAddressLookUp">
                                    &nbsp;
                                </label>
                                <div class="col-sm-8">
                                    <asp:LinkButton ID="btnAddressLookUp" runat="server" CausesValidation="True"
                                        CommandName="CheckAddress" Text="Check Address" CssClass="btn btn-primary btn-sm" />
                                </div>
                            </div>

                        </div>
                    </div>

                    <div class="modal fade" id="modAddressCheck" role="dialog">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">

                                <div class="modal-header">
                                    <div class="panel panel-default">
                                        <h4 class="modal-title"></h4>
                                        <div class="panel-heading">
                                            <b>Address Check</b>
                                            <%--<asp:UpdateProgress ID="updProgress" AssociatedUpdatePanelID="upAddressLookUp" runat="server">
                                                <ProgressTemplate>
                                                    <div class="loading-panel">
                                                        <div class="loading-container">
                                                            <img alt="progress" src="../../images/ajaxloadernew.gif" />
                                                        </div>
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>--%>
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        </div>

                                        <div>
                                            <asp:ValidationSummary ID="vlsSummGeoLookUpByAddress" runat="server" ValidationGroup="GeoLookUpByAddress" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                        </div>
                                        <div>
                                            <asp:ValidationSummary ID="vlsSummGeoLookUpByLatLng" runat="server" ValidationGroup="GeoLookUpByLatLng" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                        </div>
                                    </div>
                                </div>

                                <div class="modal-body">

                                    <asp:UpdatePanel ID="upAddressLookUp" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>

                                            <div class="col-sm-5">

                                                <div class="form-group">
                                                    <label class="control-label col-sm-6" for="txtAddressCheck_HotelName">
                                                        Hotel Name
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtAddressCheck_HotelName" runat="server" CssClass="form-control" ReadOnly="true" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="control-label-mand col-sm-6" for="txtAddressCheck_Street">
                                                        Street
                                                     <asp:RequiredFieldValidator ID="vldReqFld_txtAddressCheck_Street" runat="server" ErrorMessage="Please enter Street" Text="*" ControlToValidate="txtAddressCheck_Street" CssClass="text-danger" ValidationGroup="GeoLookUpByAddress"></asp:RequiredFieldValidator></label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtAddressCheck_Street" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="control-label col-sm-6" for="txtAddressCheck_Street2">Street 2</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtAddressCheck_Street2" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="control-label col-sm-6" for="txtAddressCheck_Street3">Street 3</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtAddressCheck_Street3" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="control-label col-sm-6" for="txtAddressCheck_Street4">Street 4</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtAddressCheck_Street4" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="control-label col-sm-6" for="txtAddressCheck_Street5">Street 5</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtAddressCheck_Street5" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="control-label col-sm-6" for="txtAddressCheck_Suburbs">
                                                        Suburbs / Downtown
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtAddressCheck_Suburbs" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="control-label col-sm-6" for="txtAddressCheck_PostalCode">
                                                        Postal Code
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtAddressCheck_PostalCode" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="control-label col-sm-6" for="txtAddressCheck_Country">Country</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtAddressCheck_Country" runat="server" CssClass="form-control" ReadOnly="true" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="control-label col-sm-6" for="txtAddressCheck_State">
                                                        State / Region
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtAddressCheck_State" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="control-label-mand col-sm-6" for="txtAddressCheck_City">
                                                        City
                                                        <asp:RequiredFieldValidator ID="vldReqFld_txtAddressCheck_City" runat="server" ErrorMessage="Please enter City" Text="*" ControlToValidate="txtAddressCheck_City" CssClass="text-danger" ValidationGroup="GeoLookUpByAddress"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtAddressCheck_City" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="control-label col-sm-6" for="txtAddressCheck_Area">
                                                        Area
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtAddressCheck_Area" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="control-label col-sm-6" for="txtAddressCheck_Location">
                                                        Location
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtAddressCheck_Location" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="control-label col-sm-6" for="txtAddressCheck_Lat">
                                                        Latitude
                                                        <asp:RequiredFieldValidator ID="vldReqFld_txtAddressCheck_Lat" runat="server" ErrorMessage="Please enter Latitude"
                                                            Text="*" ControlToValidate="txtAddressCheck_Lat" CssClass="text-danger" ValidationGroup="GeoLookUpByLatLng">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="vldRegex_txtAddressCheck_Lat" runat="server" ErrorMessage="Invalid Latitude"
                                                            Text="*" ControlToValidate="txtAddressCheck_Lat" CssClass="text-danger" ValidationGroup="GeoLookUpByLatLng"
                                                            ValidationExpression="^(\+|-)?(?:90(?:(?:\.0{1,10})?)|(?:[0-9]|[1-8][0-9])(?:(?:\.[0-9]{1,10})?))$">
                                                        </asp:RegularExpressionValidator>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtAddressCheck_Lat" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="control-label col-sm-6" for="txtAddressCheck_Lng">
                                                        Longitude
                                                        <asp:RequiredFieldValidator ID="vldReqFld_txtAddressCheck_Lng" runat="server" ErrorMessage="Please enter Longitude"
                                                            Text="*" ControlToValidate="txtAddressCheck_Lng" CssClass="text-danger" ValidationGroup="GeoLookUpByLatLng">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="vldRegex_txtAddressCheck_Lng" runat="server" ErrorMessage="Invalid Longitude"
                                                            Text="*" ControlToValidate="txtAddressCheck_Lng" CssClass="text-danger" ValidationGroup="GeoLookUpByLatLng"
                                                            ValidationExpression="^(\+|-)?(?:180(?:(?:\.0{1,10})?)|(?:[0-9]|[1-9][0-9]|1[0-7][0-9])(?:(?:\.[0-9]{1,10})?))$">
                                                        </asp:RegularExpressionValidator>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtAddressCheck_Lng" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="col-sm-7">

                                                <div class="panel panel-default">

                                                    <div class="panel-heading">

                                                        <div class="row" id="alert_addresslookup" runat="server">
                                                        </div>

                                                        <br />

                                                        <div class="row">
                                                            <div class="col-sm-4"><strong>Geo LookUp</strong></div>
                                                            <div class="col-sm-4">
                                                                <asp:LinkButton ID="btnGeoLookUpByAddress" runat="server" CausesValidation="True" ValidationGroup="GeoLookUpByAddress"
                                                                    CommandName="GeoLookUpAddress" Text="Check By Address" CssClass="btn btn-primary btn-sm pull-right" />
                                                            </div>

                                                            <div class="col-sm-4">
                                                                <asp:LinkButton ID="btnGeoLookUpByLatLng" runat="server" CausesValidation="True" ValidationGroup="GeoLookUpByLatLng"
                                                                    CommandName="GeoLookUpLatLng" Text="Check By LatLng" CssClass="btn btn-primary btn-sm pull-right" />
                                                            </div>
                                                        </div>

                                                    </div>

                                                    <asp:Repeater ID="repGeoTopResult" runat="server">

                                                        <ItemTemplate>

                                                            <asp:Repeater ID="repGeoResult" runat="server" DataSource='<%# DataBinder.Eval(Container.DataItem, "results") %>'>

                                                                <ItemTemplate>

                                                                    <div class="row">
                                                                        <br />

                                                                        <div class="col-sm-6">
                                                                            <div class="form-group">
                                                                                <label class="control-label col-sm-4" for="lblAddressCheck_Latitude">
                                                                                    Latitude
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:Label ID="lblAddressCheck_Latitude" runat="server" CssClass="form-control" Text=' <%# ((TLGX_Consumer.MDMSVC.Result)Container.DataItem).geometry.location.lat %>'></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-sm-6">
                                                                            <div class="form-group">
                                                                                <label class="control-label col-sm-4" for="lblAddressCheck_Longitude">
                                                                                    Longitude
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:Label ID="lblAddressCheck_Longitude" runat="server" CssClass="form-control" Text=' <%# ((TLGX_Consumer.MDMSVC.Result)Container.DataItem).geometry.location.lng %>'></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <br />
                                                                    </div>

                                                                    <asp:Repeater ID="repGeoAddress" runat="server" DataSource='<%# DataBinder.Eval(Container.DataItem, "address_components") %>' OnItemDataBound="repGeoAddress_ItemDataBound" OnItemCommand="repGeoAddress_ItemCommand">
                                                                        <HeaderTemplate>
                                                                            <table class="table table-bordered table-hover">
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td style="background-color: lavenderblush">
                                                                                    <b>
                                                                                        <asp:Repeater ID="repAddressTypes" runat="server" DataSource='<%# DataBinder.Eval(Container.DataItem, "types") %>'>
                                                                                            <ItemTemplate>
                                                                                                <%#Container.DataItem %>
                                                                                            </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                    </b>
                                                                                </td>

                                                                                <td>
                                                                                    <asp:Label ID="lblAddressLongName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "long_name") %>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlMapGeoAddressTo" runat="server" CssClass="form-control GeoAddressSelect" AppendDataBoundItems="true">
                                                                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:LinkButton ID="btnAddressUpdate" runat="server" CausesValidation="True" CommandName="UpdateAddress" Text="Update Address" CssClass="btn btn-primary btn-sm pull-right" />
                                                                                </td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <hr>
                                                                                </td>
                                                                            </tr>

                                                                            </table> 
                                                                        </FooterTemplate>
                                                                    </asp:Repeater>

                                                                </ItemTemplate>

                                                            </asp:Repeater>

                                                            <br />

                                                            <div class="row">
                                                                <div class="col-sm-12">
                                                                    <div class='<%# DataBinder.Eval(Container.DataItem, "status").ToString() != "OK" ? "alert alert-danger" : "" %>'>
                                                                        <%# DataBinder.Eval(Container.DataItem, "status").ToString() != "OK" ? DataBinder.Eval(Container.DataItem, "status").ToString() : string.Empty %>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <%--                                                            <strong>
                                                                <asp:Label ID="lblGeoAddressCheckResultStatus" runat="server" Text='<%# "Status : " + DataBinder.Eval(Container.DataItem, "status") %>'></asp:Label>
                                                            </strong>--%>
                                                        </ItemTemplate>


                                                    </asp:Repeater>

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

                    <div class="panel panel-default">
                        <div class="panel-heading">Geo Location</div>
                        <div class="panel-body">

                            <div class="form-group" runat="server" id="alert_GeoCode">
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-4" for="txtHotelLat">
                                    Hotel Latitude
                                    <asp:RegularExpressionValidator ID="vldRegex_txtHotelLat" runat="server" ErrorMessage="Invalid Latitude"
                                        Text="*" ControlToValidate="txtHotelLat" CssClass="text-danger" ValidationGroup="HotelOverView"
                                        ValidationExpression="^(\+|-)?(?:90(?:(?:\.0{1,10})?)|(?:[0-9]|[1-8][0-9])(?:(?:\.[0-9]{1,10})?))$">
                                    </asp:RegularExpressionValidator>
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtHotelLat" runat="server" CssClass="form-control" Text='<%# Bind("Latitude") %>' />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-4" for="txtHotelLon">
                                    Hotel Longtitude
                                    <asp:RegularExpressionValidator ID="vldRegex_txtHotelLon" runat="server" ErrorMessage="Invalid Longitude"
                                        Text="*" ControlToValidate="txtHotelLon" CssClass="text-danger" ValidationGroup="HotelOverView"
                                        ValidationExpression="^(\+|-)?(?:180(?:(?:\.0{1,10})?)|(?:[0-9]|[1-9][0-9]|1[0-7][0-9])(?:(?:\.[0-9]{1,10})?))$">
                                    </asp:RegularExpressionValidator>
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtHotelLon" runat="server" CssClass="form-control" Text='<%# Bind("Longitude") %>' />

                                    <br />

                                    <%--<asp:LinkButton ID="btnGetGeoAddress" runat="server" CausesValidation="false" CommandName="GetGeoAddressByLatLng" Text="Get Geo Address" CssClass="btn btn-primary btn-sm" />--%>
                                    <asp:Button runat="server" ID="btnGeoLocate" Text="Fetch Geo Code" CommandName="GetGeoLatLng" CssClass="btn btn-primary btn-sm" CausesValidation="false" />
                                    <button type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#myGeoLocModal" id="btnShowMaps">Preview</button>

                                    <!-- Modal -->
                                    <div id="myGeoLocModal" class="modal fade" role="dialog">
                                        <div class="modal-dialog  modal-lg">

                                            <!-- Modal content-->
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                    <h4 class="modal-title">Hotel Address Map</h4>
                                                </div>
                                                <div class="modal-body" id="mapDiv" style="height: 500px">
                                                </div>
                                                <div class="modal-footer">
                                                    <button id="btnGeoModalClose" type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>


                            </div>

                        </div>
                    </div>

                    <div class="panel panel-default">
                        <div class="panel-heading">Contact Details</div>
                        <div class="panel-body">
                            <uc1:contacts runat="server" ID="contacts" />
                        </div>
                    </div>

                    <div class="panel panel-default">
                        <div class="panel-heading">Dynamic Product Attributes</div>
                        <div class="panel-body">
                            <uc2:DynamicAttributesForHotel runat="server" ID="DynamicAttributesForHotel" />
                        </div>
                    </div>

                    <div class="panel panel-default">
                        <div class="panel-heading">Internal Remarks</div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label class="control-label col-sm-2" for="txtInternalRemarks">Internal Remarks</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtInternalRemarks" runat="server" Text='<%# Bind("InternalRemarks") %>' Rows="2" TextMode="MultiLine" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="col-lg-4">

                    <div class="panel panel-default">
                        <div class="panel-heading">Key Facts</div>
                        <div class="panel-body">
                              <div class="form-group">
                                <label class="control-label col-sm-6" for="blnRTCompleted">Room Mapping Complete</label>
                                <div class="col-sm-6">
                                    <asp:CheckBox ID="blnRTCompleted" runat="server" CssClass="form-control" Checked='<%# Bind("IsRoomMappingCompleted") %>' />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-6" for="blnMysteryProduct">Mystery Product</label>
                                <div class="col-sm-6">
                                    <asp:CheckBox ID="blnMysteryProduct" runat="server" CssClass="form-control" Checked='<%# Bind("IsMysteryProduct") %>' />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-6" for="txtYearBuilt">
                                    Year Built
                                    <asp:RegularExpressionValidator ID="rvtxtYearBuilt" runat="server" ErrorMessage="Invalid Year Built" Text="*" ControlToValidate="txtYearBuilt" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RegularExpressionValidator>
                                </label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtYearBuilt" runat="server" Text='<%# Bind("YearBuilt") %>' CssClass="form-control" MaxLength="4" />
                                    <cc1:FilteredTextBoxExtender ID="axfte_txtYearBuilt" runat="server" FilterType="Numbers" TargetControlID="txtYearBuilt" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-6" for="txtTotalFloor">
                                    Total Floors
                                    <asp:RegularExpressionValidator ID="rvtxtTotalFloor" runat="server" ErrorMessage="Invalid Total Floor" Text="*" ControlToValidate="txtTotalFloor" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RegularExpressionValidator>
                                </label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtTotalFloor" runat="server" Text='<%# Bind("TotalFloors") %>' CssClass="form-control" />
                                    <cc1:FilteredTextBoxExtender ID="axfte_txtTotalFloor" runat="server" FilterType="Numbers" TargetControlID="txtTotalFloor" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-6" for="txtTotalRooms">
                                    Total Rooms
                                    <asp:RegularExpressionValidator ID="rvtxtTotalRooms" runat="server" ErrorMessage="Invalid Total Rooms" Text="*" ControlToValidate="txtTotalRooms" ValidationExpression="^[1-9]\d*$" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RegularExpressionValidator>
                                </label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtTotalRooms" runat="server" Text='<%# Bind("TotalRooms") %>' CssClass="form-control" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label-mand col-sm-6" for="txtCheckinTime">
                                    Check In
                                    <asp:RequiredFieldValidator ID="vtxtCheckinTime" runat="server" ErrorMessage="Please enter Check In Time" Text="*" ControlToValidate="txtCheckinTime" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revtxtCheckinTime" runat="server" ErrorMessage="Invalid Check In Time." Text="*" ControlToValidate="txtCheckinTime" CssClass="text-danger" ValidationGroup="HotelOverView" ValidationExpression="^(?:[01][0-9]|2[0-3]):[0-5][0-9]$"></asp:RegularExpressionValidator>
                                </label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtCheckinTime" runat="server" Text='<%# Bind("CheckInTime") %>' CssClass="form-control" />
                                    <cc1:MaskedEditExtender ID="txtCheckinTime_MaskEditExtender" runat="server" AcceptAMPM="false"
                                        Mask="99:99" MaskType="Time" PromptCharacter="_" TargetControlID="txtCheckinTime"
                                        UserTimeFormat="TwentyFourHour" InputDirection="LeftToRight"></cc1:MaskedEditExtender>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label-mand col-sm-6" for="txtCheckOut">
                                    Check Out
                                    <asp:RequiredFieldValidator ID="vtxtCheckOut" runat="server" ErrorMessage="Please enter Check Out Time" Text="*" ControlToValidate="txtCheckOut" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator></label>
                                <asp:RegularExpressionValidator ID="revtxtCheckOut" runat="server" ErrorMessage="Invalid Check Out Time." Text="*" ControlToValidate="txtCheckOut" CssClass="text-danger" ValidationGroup="HotelOverView" ValidationExpression="^(?:[01][0-9]|2[0-3]):[0-5][0-9]$"></asp:RegularExpressionValidator>
                                </label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtCheckOut" runat="server" Text='<%# Bind("CheckOutTime") %>' CssClass="form-control" />
                                    <cc1:MaskedEditExtender ID="txtCheckinTime_txtCheckOut" runat="server" AcceptAMPM="false"
                                        Mask="99:99" MaskType="Time" PromptCharacter="_" TargetControlID="txtCheckOut"
                                        UserTimeFormat="TwentyFourHour"></cc1:MaskedEditExtender>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-6" for="ddlCompanyRating">Company Rating</label>
                                <div class="col-sm-6">

                                    <asp:DropDownList ID="ddlCompanyRating" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem>-Select-</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label-mand col-sm-6" for="ddlStarRating">
                                    Star Rating
                                    <asp:RequiredFieldValidator ID="vddlStarRating" runat="server" ErrorMessage="Please enter Star Rating" Text="*" ControlToValidate="ddlStarRating" InitialValue="0" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator>
                                </label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlStarRating" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <!-- <div class="form-group">
                                <div class='input-group date' id='datetimepicker1'>
                                    <input type='text' class="form-control" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>-->

                            <div class="form-group">
                                <label class="control-label col-sm-6" for="txtRatingDate">
                                    Rating Date
                                    <asp:RegularExpressionValidator ID="vldRegex_txtRatingDate" runat="server" CssClass="text-danger"
                                        ControlToValidate="txtRatingDate" ValidationGroup="HotelOverView" ErrorMessage="Please enter Rating Date in dd/MM/yyyy format" Text="*"
                                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                                    </asp:RegularExpressionValidator>
                                </label>
                                <div class="col-sm-6">

                                    <div class="input-group">
                                        <asp:TextBox ID="txtRatingDate" runat="server" Text='<%#Eval("RatingDate", "{0:dd/MM/yyyy}") %>' CssClass="form-control" ReadOnly="true" />
                                        <%-- <span class="input-group-btn">
                                            <button class="btn btn-default" type="button" id="iCal">
                                                <span class="glyphicon glyphicon-calendar"></span>
                                            </button>
                                        </span>--%>
                                    </div>

                                    <%-- <cc1:CalendarExtender ID="txtRatingDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtRatingDate" Animated="true" PopupButtonID="iCal">
                                    </cc1:CalendarExtender>--%>
                                    <cc1:FilteredTextBoxExtender ID="txtRatingDate_axfte" runat="server" FilterType="Numbers, Custom"
                                        ValidChars="/" TargetControlID="txtRatingDate" />

                                </div>
                                <!-- need to handle the binding of the date control better here -->
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-6" for="txtCompanyRecommended">Company Recom</label>
                                <div class="col-sm-6">
                                    <asp:CheckBox ID="chkCompanyRecommended" runat="server" CssClass="form-control" Checked='<%# Bind("CompanyRecommended") %>' />
                                </div>
                            </div>

                        </div>
                    </div>

                    <div class="panel panel-default">
                        <div class="panel-heading">Association</div>

                        <div class="panel-body">

                            <div class="form-group">
                                <label class="control-label col-sm-6" for="ddlChain">Chain</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlChain" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem>-Select-</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-6" for="ddlBrand">Brand</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlBrand" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem>-Select-</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-6" for="ddlAffiliation">Affiliation</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlAffiliation" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem>-Select-</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-6" for="txtAwardsReceived">Awards Received</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtAwardsReceived" runat="server" Text='<%# Bind("AwardsReceived") %>' CssClass="form-control" />
                                </div>
                            </div>

                        </div>
                    </div>

                    <div class="panel panel-default">
                        <div class="panel-heading">System Ids</div>
                        <div class="panel-body">

                            <div class="form-group">
                                <label class="control-label col-sm-6" for="ddlProductCategorySubType">Subcategory</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlProductCategorySubType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem>-Select-</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-6" for="txtCompanyName">Company</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtCompanyName" runat="server" Text='<%# Bind("CompanyName") %>' CssClass="form-control" Enabled="false" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-6" for="txtCompanyHotelID">Company Id</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtCompanyHotelID" runat="server" Text='<%# Bind("CompanyHotelID") %>' CssClass="form-control" Enabled="false" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-6" for="txtCommonHotelId">Common Id</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtCommonHotelId" runat="server" Text='<%# Bind("CompanyHotelID") %>' CssClass="form-control" Enabled="false" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-sm-6" for="txtFinanceControlId">Finance Id</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtFinanceControlId" runat="server" Text='<%# Bind("FinanceControlID") %>' CssClass="form-control" Enabled="false" />
                                </div>
                            </div>

                        </div>
                    </div>

                    <!-- ValidationGroup="HotelOverView" -->

                    <!--
                    <asp:LinkButton ID="btnLock" runat="server" CausesValidation="True" CommandName="LockProduct" Text="Unlock Product" CssClass="btn btn-primary btn-sm" />
                    <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="True" CommandName="SaveProduct" Text="Edit Product" CssClass="btn btn-primary btn-sm" />


                        -->

                    <asp:LinkButton ID="btnSave" runat="server" CausesValidation="True" CommandName="SaveProduct" Text="Update Product" CssClass="btn btn-primary btn-sm" ValidationGroup="HotelOverView" />
                    <asp:LinkButton ID="btnCancel" runat="server" CausesValidation="False" CommandName="CancelProduct" Text="Cancel" CssClass="btn btn-primary btn-sm" />
                    <!-- Modal -->
                    <div class="modal fade" id="myModal" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <div class="panel panel-default">
                                        <h4 class="modal-title"></h4>
                                        <div class="panel-heading">
                                            Update Hotel
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-body">
                                    <asp:Label ID="lblMessage" runat="server" Text="Product already Exists"></asp:Label>
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

    </EditItemTemplate>

</asp:FormView>



