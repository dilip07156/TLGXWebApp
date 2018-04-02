<%@ Page Language="C#" Title="Edit Zone-City Master" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ZoneCityMasterEdit.aspx.cs" Inherits="TLGX_Consumer.geography.ZoneCityMasterEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBAbYHJn_5Kubmfa4-nYyAf_WpHB9mbfvc&libraries=places"></script>
    <style>
        #iw-container {
            margin-bottom: 10px;
        }

            #iw-container .iw-content {
                font-size: 13px;
                line-height: 18px;
                font-weight: 400;
                margin-right: 1px;
                padding: 2px;
                max-height: 140px;
                overflow-y: auto;
                overflow-x: hidden;
            }

            #iw-container .iw-title {
                font-family: 'Open Sans Condensed', sans-serif;
                font-size: 12px;
                font-weight: 400;
                padding: 5px;
                background-color: #48b5e9;
                color: white;
                margin: 0;
                border-radius: 2px 2px 0 0;
            }
    </style>
    <script>
        //to set active to Tab

        function initializeMyMap() {
            var mapOptions;
            $("#hdnupdateMap").val("HotelMap");
            $("#dvMapHotel").empty();
            var lat = $("#MainContent_txtEditLatitude").val();
            var longg = $("#MainContent_txtEditLongitude").val();
            var zoneLatLong = new google.maps.LatLng(lat, longg);
            //range in meter
            var radius = parseFloat($("#MainContent_ddlZoneRadius option:selected").val()) * 1000;
            //containerForMap
            var mapCanvas = document.getElementById("dvMapHotel");
            //maintain Zoom Level of Map
            
            if (sessionStorage.mapLat != null && sessionStorage.mapLng != null && sessionStorage.mapZoom != null) {
                mapOptions = {
                    center: new google.maps.LatLng(sessionStorage.mapLat, sessionStorage.mapLng),
                    zoom: parseInt(sessionStorage.mapZoom),
                    scaleControl: true,
                    styles: [{
                        featureType: "poi.business",
                        elementType: "labels",
                        stylers: [{ visibility: "off" }]
                    }]
                };
            } 
            else {
                //optionsForMap
                mapOptions ={
                                center: zoneLatLong,
                                zoom: 13,
                                scaleControl: true,
                                styles: [{
                                            featureType: "poi.business",
                                            elementType: "labels",
                                            stylers: [{ visibility: "off" }]
                                        }]
                            };
                }

            //createMAP
            map = new google.maps.Map(mapCanvas, mapOptions);

            //Set sessionStorage Variables

            mapCentre = map.getCenter();
            sessionStorage.mapLat = mapCentre.lat();
            sessionStorage.mapLng = mapCentre.lng();
            sessionStorage.mapZoom = map.getZoom();

            google.maps.event.addListener(map, "center_changed", function () {
                //Set local storage variables.
                mapCentre = map.getCenter();

                sessionStorage.mapLat = mapCentre.lat();
                sessionStorage.mapLng = mapCentre.lng();
                sessionStorage.mapZoom = map.getZoom();
            });

            google.maps.event.addListener(map, "zoom_changed", function () {
                //Set local storage variables.
                mapCentre = map.getCenter();

                sessionStorage.mapLat = mapCentre.lat();
                sessionStorage.mapLng = mapCentre.lng();
                sessionStorage.mapZoom = map.getZoom();
            });
            //End sessionStorage

            //create Circle
            var myzoneCircle = new google.maps.Circle({
                center: zoneLatLong,
                radius: radius,
                strokeColor: "#0000FF",
                strokeOpacity: 0.8,
                strokeWeight: 2,
                fillColor: "#0000FF",
                fillOpacity: 0.1
            });
            myzoneCircle.setMap(map);

            // a new Info Window  created
            infoWindow = new google.maps.InfoWindow();

            // Event that closes the InfoWindow with a click on the map
            google.maps.event.addListener(map, 'click', function () {
                infoWindow.close();
            });

            // displayMarkers() function is called to begin the markers creation
            displayMarkers();
        }

        $(document).ready(function () {
            sessionStorage.clear();
            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "personal";
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
            initializeMyMap();
        });

        //handling postback
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "personal";
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
            // re-bind your jQuery events here
            var a = $("#hdnupdateMap").val();
            if (a == "HotelMap")
                initializeMyMap();
            else if (a == "LatLongMap")
                GetLatLongOnMap();
            else initializeMyMap();

        });
        //end
        function displayMarkers() {
            //range in km
            //var range = parseFloat($("#MainContent_ddlZoneRadius option:selected").val());
            var ZoneId = $("#hdnZone_id").val();
            $.ajax({
                url: '../../../Service/GetZoneHotelsForMap.ashx',
                data: { 'ZoneId': ZoneId },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    // debugger;
                    if (result != null) {
                        // this variable sets the map bounds and zoom level according to markers position
                       // var bounds = new google.maps.LatLngBounds();
                        for (var i = 0; i < result.length; i++) {
                            var markerLatLng = new google.maps.LatLng(result[i].Latitude, result[i].Longitude);
                            var hotelName = result[i].HotelName;
                            var zoneProdMapId = result[i].ZoneProductMapping_Id;
                            var includestatus = result[i].Included;
                            var markerHotels;
                            //if (result[i].Distance <= range)
                            if (result[i].Included == true)
                            {
                                markerHotels = createGreenMarker(markerLatLng, hotelName, zoneProdMapId);
                            }
                            else {
                                markerHotels = createRedMarker(markerLatLng, hotelName, zoneProdMapId);
                            }
                            InfWindowoAndMarkerListener(markerHotels, hotelName, zoneProdMapId, includestatus);
                            // createMarker(markerLatLng, hotelName, acco_Id);
                            // Marker’s Lat. and Lng. values are added to bounds variable
                          //  bounds.extend(markerLatLng);
                        }
                        // Finally the bounds variable is used to set the map bounds
                        // with API’s fitBounds() function
                       // map.fitBounds(bounds);
                    }
                }

            });
        }
        function createGreenMarker(Markerlatlng, Hotelname,zoneProdMapId) {
            // create marker
            markerHotels = new google.maps.Marker({
                map: map,
                position: Markerlatlng,
                icon: "http://maps.google.com/mapfiles/ms/micons/green-dot.png",
                title: Hotelname,
                id: zoneProdMapId
            });

            return markerHotels;
        }
        function createRedMarker(Markerlatlng, Hotelname, zoneProdMapId) {
            // create marker
            markerHotels = new google.maps.Marker({
                map: map,
                position: Markerlatlng,
                icon: "http://maps.google.com/mapfiles/ms/micons/red-dot.png",
                title: Hotelname,
                id: zoneProdMapId
            });

            return markerHotels;
        }
        function InfWindowoAndMarkerListener(markerHotels, hotelName, zoneProdMapId, includestatus) {
            //  This event expects a click on a marker When this event is fired the infowindow content is created and the infowindow is opened
            google.maps.event.addListener(markerHotels, 'click', function () {
                IncludeExcludeHotels(this.id, includestatus, markerHotels);

                // Variable to define the HTML content to be inserted in the infowindow
             /*   var iwContent = '<div id="iw-container">' +
                '<div class="iw-title">' + hotelName + '</div>' +
                '</div>' + '<div class="iw-content"> Status :' + includestatus + '<br />' + '</div>';
                 for content of infowindow: '<div class="iw-content">' + zoneProdMapId + '<br />' + '</div>'
                 including content to the infowindow
                infoWindow.setContent(iwContent);

                 opening the infowindow in the current map and at the current marker location
                infoWindow.open(map, markerHotels);*/

            });

        }
        function IncludeExcludeHotels(zoneProdMapId, includestatus, markerHotels) {
            $.ajax({
                url: '../../../Service/IncludeExcludeZoneHotels.ashx',
                data: { 'zoneProdMapId': zoneProdMapId, 'includestatus': includestatus },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    debugger;
                    if (result != null) {
                        if (result.StatusCode == 1) {
                            if(includestatus==true)
                                markerHotels.setIcon("http://maps.google.com/mapfiles/ms/micons/red-dot.png");
                            else if(includestatus==false)
                                markerHotels.setIcon("http://maps.google.com/mapfiles/ms/micons/green-dot.png");
                            initializeMyMap();
                        }
                    }
                }

            });
            //Set local storage variables.
            mapCentre = map.getCenter();
            sessionStorage.mapLat = mapCentre.lat();
            sessionStorage.mapLng = mapCentre.lng();
            sessionStorage.mapZoom = map.getZoom();
        }
        function GetLatLongOnMap() {
            $("#hdnupdateMap").val("LatLongMap");
            //onOverlay();
            $("#dvMapHotel").empty();
            var lat = $("#MainContent_txtEditLatitude").val();
            var longg = $("#MainContent_txtEditLongitude").val();
            var zoneName = $('#MainContent_txtEditZoneName').val();
            var country = $('#MainContent_ddlMasterCountryEdit').find("option:selected").text();
            var address = zoneName + ',' + country;
            var geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'address': address }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {

                    var centerLatLong = new google.maps.LatLng(results[0].geometry.location.lat(), results[0].geometry.location.lng());
                    var mapInDvMapHotel = document.getElementById("dvMapHotel");

                    //optionsForMap
                    var newMapOptions = { center: centerLatLong, zoom: 13 };

                    //createMAP
                    Newmap = new google.maps.Map(mapInDvMapHotel, newMapOptions);
                    google.maps.event.addListener(Newmap, 'click', function (e) {
                        $("#MainContent_txtEditLatitude").val(e.latLng.lat());
                        $("#MainContent_txtEditLongitude").val(e.latLng.lng());
                    });
                }
                else {
                    alert("Request failed.")
                }
            });
        }
    </script>
    <asp:UpdatePanel ID="uppnl" runat="server">
        <ContentTemplate>
            <div class="row page-header">
                <div class="col-md-8">
                    <h3>Edit Zone -<strong>
                        <asp:Label ID="lblEditZoneName" runat="server"></asp:Label></strong></h3>
                </div>
                <div class="col-md-4">
                    <div class="pull-right" style="margin-top: 25px !important;">
                        <asp:Button runat="server" ID="btnRedirectToSearch" OnClick="btnRedirectToSearch_Click" CssClass="btn btn-link" Text="Go Back to ZoneSearch Page" />
                    </div>
                </div>
            </div>

            <div style="display: none" runat="server" id="dvUpdateMsg"></div>
            <div class="row">
                <div class="panel-group" id="">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <asp:HiddenField ID="hdnZone_id" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hdnupdateMap" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hdnCountryId" runat="server" ClientIDMode="Static" />
                            <div class="row">
                                <div class="col-md-12" style="display: none" id="dvmsgUpdateZone" runat="server"></div>
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="control-label col-md-4" for="txtEditZoneName">Name</label>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtEditZoneName" runat="server" CssClass="form-control">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="control-label col-md-4" for="txtEditLatitude">Latitude</label>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtEditLatitude" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="control-label col-md-4" for="ddlMasterCountryEdit">Country</label>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlMasterCountryEdit" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" Enabled="False">
                                                <%--onclientclick="changeCountry()"--%>
                                                <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">

                                    <div class="form-group row">
                                        <label class="control-label col-md-4" for="ddlEditZoneType">Zone type</label>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlEditZoneType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="control-label col-md-4" for="txtEditLongitude">Longitude</label>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtEditLongitude" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="control-label col-md-4" for="ddlZoneRadius">Include Hotels Upto Range (km)</label>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlZoneRadius" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <div class="col-md-6">
                                            <div class="col-md-6">
                                                <button id="btnUpdateLatLong" onclick="GetLatLongOnMap()" class="btn btn-primary btn-sm">Get LatLong on Map</button>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Button ID="btnUpdateZoneMaster" runat="server" CssClass="btn btn-primary btn-sm" Text="Save and Show Hotels" OnClick="btnUpdateZoneMaster_Click" OnClientClick="initializeMyMap()" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div id="Tabs" class="panel-body" role="tabpanel">
                                    <ul class="nav nav-tabs tabs" role="tablist">

                                        <li class="active"><a role="tab" data-toggle="tab" aria-controls="MapHotels" href="#MapHotels">Map Hotels</a></li>
                                        <li><a role="tab" data-toggle="tab" aria-controls="ShowZoneHotelList" href="#ShowZoneHotelList">Zone Hotel-List</a></li>
                                        <li><a role="tab" data-toggle="tab" aria-controls="ShowZoneCities" href="#ShowZoneCities">Zone Cities</a></li>
                                    </ul>
                                    <div class="tab-content">
                                        <!--For Cities-->
                                       
                                                <div role="tabpanel" id="ShowZoneCities" class="tab-pane fade in">
                                                       <br />
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <asp:DropDownList ID="ddlMasterCityEdit" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                                <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="col-md-8 pull-left">
                                                            <asp:Button ID="btnAddZoneCity" runat="server" CssClass="btn btn-primary btn-sm" Text="Add City" OnClick="btnAddZoneCity_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <br />
                                                        <div style="display: none" runat="server" id="dvMsgaddZoneCity"></div>
                                                        <asp:GridView ID="grdZoneCities" runat="server" AutoGenerateColumns="False"
                                                            EmptyDataText="No cities found for thiz Zone " CssClass="table table-hover table-striped" DataKeyNames="ZoneCityMapping_Id" OnRowCommand="grdZoneCities_RowCommand" OnRowDataBound="grdZoneCities_RowDataBound">
                                                            <Columns>
                                                                <asp:BoundField DataField="CityName" HeaderText="City Name" />
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                       <asp:LinkButton ID="btndelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"    %>'
                                                        CssClass="btn btn-default" CommandArgument='<%# Bind("ZoneCityMapping_Id") %>'>
                                                    <span aria-hidden="true" class='<%# Eval("IsActive").ToString() ==  "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat" %>'></span>
                                                    <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"     %>
                                                    </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                          
                                        <!--For Hotel-List-->
                                    
                                                <div role="tabpanel" id="ShowZoneHotelList" class="tab-pane fade in">
                                                       <br />
                                                    <div class="col-md-12">
                                                        <div class="col-md-3 form-group">
                                                        <h4>Search Results (Total Count:<asp:Label ID="lblTotalCount" runat="server" Text="0"></asp:Label>)</h4>
                                                         </div>
                                                        <div class="col-md-6 form-group">
                                                            <div class="input-group col-md-3  " runat="server" id="divDropdownForDistance">
                                                                <label class="input-group-addon" for="ddlShowDistance">Distance(Km)</label>
                                                                <asp:DropDownList ID="ddlShowDistance" runat="server" AutoPostBack="true" CssClass="form-control" Width="180%"  OnSelectedIndexChanged="ddlShowDistance_SelectedIndexChanged">
                                                               
                                                                </asp:DropDownList>
                                                            </div>
                                                         </div>
                                                        <div class="form-group col-md-3 pull-right " id="div1">
                                                            <%--<asp:Button ID="btnMapSelected" runat="server" Text="Include Selected" CssClass="btn btn-primary  " OnClick="btnMapSelected_Click" />--%>

                                                            <asp:Button ID="btnMapAll" runat="server" Text="Include All" CssClass="btn btn-primary" OnClick="btnMapAll_Click"/>
                                                                   </div>
                                                   
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <div class="col-md-12">
                                                        <asp:GridView ID="grdZoneHotelSearch" runat="server" AutoGenerateColumns="False"
                                                            EmptyDataText="No Hotels found for thiz Zone " CssClass="table table-hover table-striped" DataKeyNames="ZoneProductMapping_Id,Accommodation_Id">
                                                            <Columns>
                                                                <asp:BoundField DataField="HotelName" HeaderText="HotelName" />
                                                                <asp:BoundField DataField="Distance" HeaderText="Distance(km)" HtmlEncode="False" DataFormatString="{0:n2}" />
                                                                <asp:BoundField DataField="StarRating" HeaderText="StarRating" />
                                                                <asp:BoundField DataField="City" HeaderText="City" />
                                                                <asp:TemplateField HeaderText="Include/Exclude hotel">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkncludeExclude" runat="server" CausesValidation="false" Checked='<%# Convert.ToBoolean(Eval("Included")) %>' Enabled="true"/>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                     
                                        <!--For Hotel-Map-->
                                      
                                                <div role="tabpanel" id="MapHotels" class="tab-pane fade in  active">
                                                    <br />
                                                    <br />
                                                    <div id="dvMapHotel" style="width: 100%; height: 500px">
                                                    </div>
                                                </div>
                                        
                                    </div>
                                </div>
                                <asp:HiddenField runat="server" id="TabName" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
