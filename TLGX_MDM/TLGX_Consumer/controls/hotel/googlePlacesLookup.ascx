<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="googlePlacesLookup.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.googlePlacesLookup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBAbYHJn_5Kubmfa4-nYyAf_WpHB9mbfvc&libraries=places"></script>
<script src="../../Scripts/Google_Related/jquery.dataTables.min.js"></script>
<style type="text/css">
    .hide {
        display: none;
    }
</style>
<script type="text/javascript">
    //Get Query string value
    function getParameterByName(name, url) {
        if (!url) url = window.location.href;
        name = name.replace(/[\[\]]/g, "\\$&");
        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, " "));
    }
    //Handler call by Ajax
    function SendDataToServer(localproducts) {
        $.ajax({
            type: 'POST',
            url: '../../../Service/AddUpdateNearByPlaces.ashx',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(localproducts),
            responseType: "json",
            success: function (result) {
                $('#msgSuccessful').attr('display', 'block');
            },
            failure: function () {
            }
        });
    }
    //Add selected Places
    function AddSelectedLookups() {
        debugger;
        var table = $('#tblBodyForPlaces');
        var localproducts = {};
        var Hotel_Id = getParameterByName('Hotel_Id');
        localproducts.Accomodation_Id = Hotel_Id;
        localproducts.PlaceCategory = $('#MainContent_inandaround_googlePlacesLookup_ddlPlaceCategory option:selected').text().toLowerCase();;
        localproducts.GooglePlaceNearBy = [];

        table.find('tr').each(function (i, el) {
            debugger;
            var $tds = $(this).find('td');
            if ($tds.find('input').is(":checked")) {
                localproducts.GooglePlaceNearBy.push(JSON.parse($tds.eq(3).text()));
            }
        });
        SendDataToServer(localproducts);
    }
    //Add All Places 
    function AddAllLookups() {
        var table = $('#tblBodyForPlaces');
        var localproducts = {};
        var Hotel_Id = getParameterByName('Hotel_Id');
        localproducts.Accomodation_Id = Hotel_Id;
        localproducts.PlaceCategory = $('#MainContent_inandaround_googlePlacesLookup_ddlPlaceCategory option:selected').text().toLowerCase();;
        localproducts.GooglePlaceNearBy = [];
        table.find('tr').each(function (i, el) {
            debugger;
            var $tds = $(this).find('td');
            localproducts.GooglePlaceNearBy.push(JSON.parse($tds.eq(3).text()));
        });
        SendDataToServer(localproducts);

    }
    //To get lat & lng by place id --It's not working
    var newCodes = function (placeId) {
        debugger;
        var map = new google.maps.Map(document.getElementById('mapdiv'), {
            center: { lat: -33.866, lng: 151.196 },
            zoom: 15
        });
        var infowindow = new google.maps.InfoWindow();
        var service = new google.maps.places.PlacesService(map);

        service.getDetails({
            placeId: placeId
        }, function (place, status) {
            if (status === google.maps.places.PlacesServiceStatus.OK) {
                callback(results);
                var latitude = place.geometry.location.lat();
                var longitude = place.geometry.location.lng();
                return [latitude, longitude];
            }
        });
    }
    //Button call function to get Latitude and Longitude

    function getNearByPlaces() {
        var G_PlaceID = $('#MainContent_inandaround_googlePlacesLookup_hdnG_PlaceID').val();
        var latitude = $('#MainContent_inandaround_googlePlacesLookup_hdnLat').val();
        var longitude = $('#MainContent_inandaround_googlePlacesLookup_hdnLong').val();
        var fulladdress = $('#MainContent_inandaround_googlePlacesLookup_hdnAddress').val();
        if (typeof G_PlaceID == 'undefined' || G_PlaceID.trim() == null || G_PlaceID.trim() == '') {
            if (typeof latitude == 'undefined' || latitude.trim() == null || latitude.trim() == '') {
                if (typeof fulladdress == 'undefined' || fulladdress.trim() == null || fulladdress.trim() == '') {
                    $('#msgNoDataFoundForSearchByNearByPlaces').attr('display', 'block');
                }
            }
        }
        var lat = "";
        var long = "";
        var placeId = "";
        if (typeof G_PlaceID != 'undefined' && G_PlaceID.trim() != null && G_PlaceID.trim() != '') {
            //var codes = newCodes(G_PlaceID);
            //var lat = codes[0];
            //var long = codes[1];
        }
        else if ((typeof latitude != 'undefined' && latitude != null && latitude.trim() != '') && (typeof longitude != 'undefined' && longitude != null && longitude.trim() != '')) {
            lat = latitude;
            long = longitude;
        }
        else if (typeof fulladdress != 'undefined' && fulladdress != null && fulladdress.trim() != '') {
            // var address = document.getElementById("address").value;
            var geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'address': fulladdress }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    lat = results[0].geometry.location.lat();
                    long = results[0].geometry.location.lng();
                    placeId = results[0].place_id;
                    //latlng = new google.maps.LatLng(latitude, longitude);
                }

            });

        }

        if (lat != null && long != null) {
            getNearByPlacesWithLatLong(lat, long)
        }
        else {
            $('#msgNoDataFoundForSearchByNearByPlaces').attr('display', 'block');
        }
    }
    //Render the table and marker with latitude and longitude
    function getNearByPlacesWithLatLong(latitude, longitude) {
        var map;
        var infowindow;
        var placetype = $('#MainContent_inandaround_googlePlacesLookup_ddlPlaceCategory option:selected').text().toLowerCase();
        var itemcount = $('#MainContent_inandaround_googlePlacesLookup_ddlNoOfItem').val();
        var radius = $('#MainContent_inandaround_googlePlacesLookup_ddlRadius').val();

        if (latitude != null && longitude != null) {
            var pyrmont = new google.maps.LatLng(latitude, longitude);
            map = new google.maps.Map(document.getElementById('mapdiv'), {
                center: pyrmont,
                zoom: 12
            });
            infowindow = new google.maps.InfoWindow();
            var service = new google.maps.places.PlacesService(map);
            service.nearbySearch({
                location: pyrmont,
                radius: radius,
                type: placetype
            }, callback);


            function callback(results, status) {
                if (status === google.maps.places.PlacesServiceStatus.OK) {
                    var itemcountresult = (itemcount < results.length ? itemcount : results.length)
                    ListDownResult(results, itemcountresult);
                    for (var i = 0; i < itemcountresult; i++)
                        createMarker(results[i]);

                }
            }
            function ListDownResult(results, itemcountresult) {
                var trText = "";
                for (var i = 0; i < itemcountresult; i++) {
                    trText += "<tr>";
                    for (var j = 0; j < 4; j++) {
                        if (j == 0) { trText += "<td>" + results[i].name + "</td>" }
                        else if (j == 1) { trText += "<td>" + results[i].vicinity + "</td>" }
                        else if (j == 2) { trText += "<td>" + "<input type='checkbox' id=checkbox_" + i + " />" + "</td>" }
                        else if (j == 3) { trText += "<td class='hide'>" + JSON.stringify(results[i]) + "</td>" }
                    }
                    trText += "</tr>";
                }
                document.getElementById("tblBodyForPlaces").innerHTML = "";
                document.getElementById("tblBodyForPlaces").innerHTML = trText;

                $('#tabularData').DataTable({
                    retrieve: true,
                    paging: false,
                    searching: false
                });
            }
            function createMarker(place) {
                var placeLoc = place.geometry.location;
                var marker = new google.maps.Marker({
                    map: map,
                    position: place.geometry.location
                });

                google.maps.event.addListener(marker, 'click', function () {
                    infowindow.setContent(place.name);
                    infowindow.open(map, this);
                });
            }
        }
    }
</script>
<div class="form-group row">

    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="vldInAndAroundLookup" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
    <div id="msgAlert" runat="server" style="display: none;"></div>
    <div id="msgSuccessful" style="display: none;">
        <script type="text/javascript">
            setTimeout(function () { $("#msgSuccessful").fadeTo(500, 0).slideUp(500) }, 3000);

        </script>

        <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a><strong>Success!</strong> <span>Dynamic Attributes has been added successfully
    </div>
    <div id="msgNoDataFoundForSearchByNearByPlaces" style="display: none;">
        <script type="text/javascript">
            setTimeout(function () { $("#msgNoDataFoundForSearchByNearByPlaces").fadeTo(500, 0).slideUp(500) }, 3000);
        </script>
        <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a><strong>Warning!</strong> <span>Required details are missing. Please update these in overview page.</span>
        <ul>
            <li>Latitude & Longitude</li>
            <li>Hotel Address</li>
        </ul>
    </div>


</div>
<div class="panel panel-default">
    <div class="panel-heading">Add Nearby Place</div>
    <div class="panel-body">
        <div class="col-md-6">
            <div class="form-group row">
                <label class="control-label-mand col-sm-6" for="ddlPlaceCategory">
                    Place Category
                        <asp:RequiredFieldValidator ID="vldddlPlaceCategory" CssClass="text-danger" ControlToValidate="ddlPlaceCategory"
                            InitialValue="0" runat="server" ErrorMessage="Please select place category" Text="*" ValidationGroup="vldInAndAroundLookup"></asp:RequiredFieldValidator></label>
                <div class="col-sm-6">
                    <asp:DropDownList ID="ddlPlaceCategory" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <asp:LinkButton ID="btnAdd" runat="server" CommandName="Add" OnClientClick="getNearByPlaces();" ValidationGroup="vldInAndAroundLookup" Text="Lookup Places" CssClass="btn btn-primary btn-sm" />
        </div>
        <div class="col-md-6">
            <div class="form-group row">
                <label class="control-label col-sm-6" for="txtCountLookups">
                    Number to get<asp:RequiredFieldValidator ID="rfvNoToGet" CssClass="text-danger" ControlToValidate="ddlNoOfItem"
                        runat="server" ErrorMessage="Please enter number to get" Text="*" InitialValue="0" ValidationGroup="vldInAndAroundLookup"></asp:RequiredFieldValidator></label>
                <div class="col-sm-6">
                    <asp:DropDownList ID="ddlNoOfItem" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="15">15</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group row">
                <label class="control-label col-sm-6" for="txtRadius">
                    Radius<asp:RequiredFieldValidator ID="rfvtxtRadius" CssClass="text-danger" ControlToValidate="ddlRadius"
                        runat="server" ErrorMessage="Please enter radius" Text="*" ValidationGroup="vldInAndAroundLookup"></asp:RequiredFieldValidator>
                </label>
                <div class="col-sm-6">
                    <asp:DropDownList ID="ddlRadius" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem Value="500">500</asp:ListItem>
                        <asp:ListItem Value="1000">1000</asp:ListItem>
                        <asp:ListItem Value="2000">2000</asp:ListItem>
                        <asp:ListItem Value="5000">5000</asp:ListItem>
                        <asp:ListItem Value="10000">10000</asp:ListItem>
                        <asp:ListItem Value="20000">20000</asp:ListItem>
                        <asp:ListItem Value="30000">30000</asp:ListItem>
                        <asp:ListItem Value="50000">50000</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdnLat" runat="server" />
        <asp:HiddenField ID="hdnLong" runat="server" />
        <asp:HiddenField ID="hdnG_PlaceID" runat="server" />
        <asp:HiddenField ID="hdnAddress" runat="server" />


    </div>
</div>
<div class="panel panel-default">
    <div class="panel-heading">

        <div class="row">
            <div class="col-lg-9">Lookup Results</div>
            <div class="col-lg-3">
                <div class="pull-right">
                    <asp:LinkButton OnClientClick="AddSelectedLookups();" ID="lnkbtnMapSelected" runat="server" Text="Map Selected" CssClass="btn btn-primary btn-sm" />
                    <asp:LinkButton OnClientClick="AddAllLookups();" ID="lnkbtnMapAll" runat="server" Text="Map All" CssClass="btn btn-primary btn-sm" />
                </div>
            </div>

        </div>
    </div>
    <div class="panel-body">
        <div class="col-md-6">
            <table id="tabularData" class="table table-bordered table-hover table-condensed">
                <thead>
                    <tr class="active">
                        <th>Name</th>
                        <th>Vicinity</th>
                        <th>Include</th>
                        <th class="hide">Object</th>
                    </tr>
                </thead>
                <tbody id="tblBodyForPlaces"></tbody>
            </table>
        </div>
        <div class="col-md-6">
            <div id="mapdiv" style="width: 410px; height: 450px;"></div>
        </div>
    </div>
</div>

