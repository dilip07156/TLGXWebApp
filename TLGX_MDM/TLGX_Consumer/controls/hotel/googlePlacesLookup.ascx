<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="googlePlacesLookup.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.googlePlacesLookup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCMaiicvgxsZeI8LqtPfEE6k7dufNqWsEA&libraries=places" async defer></script>
<script type="text/javascript">
    function getNearByPlaces() {
        debugger;
        var map;
        var infowindow;
        var latitude = $('#MainContent_inandaround_googlePlacesLookup_hdnLat').val();
        var longitude = $('#MainContent_inandaround_googlePlacesLookup_hdnLong').val();
        var G_PlaceID = $('#MainContent_inandaround_googlePlacesLookup_hdnG_PlaceID').val();
        //var address = document.getElementById("address").value;
        //var geocoder = new google.maps.Geocoder();
        //geocoder.geocode({ 'address': address }, function (results, status) {

        //    if (status == google.maps.GeocoderStatus.OK) {
        //        latitude = results[0].geometry.location.lat();
        //        longitude = results[0].geometry.location.lng();
        //        inputPlaceId = results[0].place_id;
        //latlng = new google.maps.LatLng(latitude, longitude);
        if (latitude != null && longitude != null) {
            var pyrmont = new google.maps.LatLng(latitude, longitude);
            debugger;
            map = new google.maps.Map(document.getElementById('mapdiv'), {
                center: pyrmont,
                zoom: 15
            });
            debugger;
            infowindow = new google.maps.InfoWindow();
            var service = new google.maps.places.PlacesService(map);
            service.nearbySearch({
                location: pyrmont,
                radius: 500,
                type: 'atm'
            }, callback);


            function callback(results, status) {
                debugger;
                if (status === google.maps.places.PlacesServiceStatus.OK) {
                    for (var i = 0; i < results.length; i++) {
                        debugger;
                        // createMarker(results[i]);
                    }
                }
            }

            function createMarker(place) {
                debugger;
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
            //    }
            //    else
            //        alert(status);
            //});
        }
    }
</script>
<div class="form-group row">

    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="vldInAndAroundLookup" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
    <div id="msgAlert" runat="server" style="display: none;"></div>
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
                    Number to get<asp:RequiredFieldValidator ID="rfvNoToGet" CssClass="text-danger" ControlToValidate="txtCountLookups"
                        runat="server" ErrorMessage="Please enter number to get" Text="*" ValidationGroup="vldInAndAroundLookup"></asp:RequiredFieldValidator></label>
                <div class="col-sm-6">
                    <asp:TextBox ID="txtCountLookups" runat="server" CssClass="form-control" />
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtCountLookups" />
                </div>
            </div>

            <div class="form-group row">
                <label class="control-label col-sm-6" for="txtRadius">
                    Radius<asp:RequiredFieldValidator ID="rfvtxtRadius" CssClass="text-danger" ControlToValidate="txtRadius"
                        runat="server" ErrorMessage="Please enter radius" Text="*" ValidationGroup="vldInAndAroundLookup"></asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ID="cmpvtxtCountLookups" CssClass="text-danger" ControlToValidate="txtRadius" ErrorMessage="Please enter value less then 50" ValueToCompare="50" Operator="LessThanEqual" Type="Integer" Text="*" ValidationGroup="vldInAndAroundLookup"></asp:CompareValidator>
                </label>
                <div class="col-sm-6">
                    <asp:TextBox ID="txtRadius" runat="server" CssClass="form-control" />
                    <cc1:FilteredTextBoxExtender ID="axfte_txtMediaPosition" runat="server" FilterType="Numbers" TargetControlID="txtRadius" />
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
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Add" Text="Map Selected" CssClass="btn btn-primary btn-sm" />
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Add" Text="Map All" CssClass="btn btn-primary btn-sm" />
                </div>
            </div>

        </div>
    </div>
    <div class="panel-body">
        <asp:GridView ID="grdNearByPlaces" runat="server" CssClass="table table-hover table-striped" AutoGenerateColumns="false" EmptyDataText="No result Found !">
            <Columns>
                <asp:BoundField HeaderText="Name" />
                <asp:BoundField HeaderText="Types" />
                <asp:BoundField HeaderText="Vicinity" />
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkGooglePlaceID" runat="server" />
                    </ItemTemplate>
                    <HeaderTemplate>
                        Include
                    </HeaderTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <%-- <table class="table table-hover table-striped">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Type(s)</th>
                    <th>Vicinity</th>
                    <th>Include</th>
                </tr>

            </thead>

            <tbody>
                <tr>
                    <td>Kensington Gardens</td>
                    <td>lodging, point_of_interest, establishment</td>
                    <td>11 Craven Road, London</td>
                    <td>
                        <input type="checkbox" value="">
                    </td>
                </tr>

                <tr>
                    <td>Kew Gardens</td>
                    <td>lodging, point_of_interest, establishment</td>
                    <td>11 Craven Road, London</td>
                    <td>
                        <input type="checkbox" value="">
                    </td>
                </tr>

                <tr>
                    <td>Green Park</td>
                    <td>lodging, point_of_interest, establishment</td>
                    <td>11 Craven Road, London</td>
                    <td>
                        <input type="checkbox" value="">
                    </td>
                </tr>


            </tbody>

        </table>--%>
    </div>
</div>

