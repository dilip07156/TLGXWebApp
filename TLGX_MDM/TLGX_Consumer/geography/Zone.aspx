<%@ Page Title="Zone Master" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Zone.aspx.cs" Inherits="TLGX_Consumer.geography.Zone" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <style>
        @media (min-width: 768px) {
            .modal-lg {
                width: 80%;
                max-width: 1200px;
            }
        }
    </style>

    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBAbYHJn_5Kubmfa4-nYyAf_WpHB9mbfvc&libraries=places"></script>
    <script>
        function showAddZoneModal() {
            $("#moAddZoneModal").modal('show');
        }
        function closeAddZoneModal() {
            $("#moAddZoneModal").modal('hide');
        }

        function getLatLong() {
            var zoneName = $('#MainContent_txtAddZoneName').val();
            var city = $('#MainContent_ddlMasterCityAddModal').find("option:selected").text();
            var country = $('#MainContent_ddlMasterCountryAddModal').find("option:selected").text();
            if (zoneName !== '' && city != '' && country != '') {
                var address = zoneName + ',' + city + ',' + country;
                var geocoder = new google.maps.Geocoder();
                geocoder.geocode({ 'address': address }, function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        var latitude = results[0].geometry.location.lat();
                        var longitude = results[0].geometry.location.lng();
                        $('#MainContent_txtLatitude').val(latitude);
                        $('#MainContent_txtLongitude').val(longitude);
                        $("#hdnPlaceId").val(results[0].place_id);
                    } else {
                        alert("Request failed.")
                    }
                });
            }
        }
        function GetLatLongOnMap() {
            $("#MainContent_dvLatLongMap").empty();
            $("#MainContent_dvLatLongMap").show();
            var zoneName = $('#MainContent_txtAddZoneName').val();
            var country = $('#MainContent_ddlMasterCountryAddModal').find("option:selected").text();
            var address = zoneName + ',' + country;
            var geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'address': address }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    var lat = results[0].geometry.location.lat();
                    var longg = results[0].geometry.location.lng();
                    var zoneLatLong = new google.maps.LatLng(lat, longg);
                    var mapCanvasgetLatLong = document.getElementById("MainContent_dvLatLongMap");

                    //optionsForMap
                    var mapOptions = { center: zoneLatLong, zoom: 13 };

                    //createMAP
                    map = new google.maps.Map(mapCanvasgetLatLong, mapOptions);
                    google.maps.event.addListener(map, 'click', function (e) {
                        //infoWindow.close();
                        $("#MainContent_txtLatitude").val(e.latLng.lat());
                        $("#MainContent_txtLongitude").val(e.latLng.lng());
                    });
                }
                else {
                    alert("Request failed.")
                }
            });
        }
    </script>

    <div class="container">
        <h1 class="page-header">Zone Manager</h1>
    </div>

    <!--search region-->
    <asp:UpdatePanel ID="updZoneMasterSearch" runat="server">
        <ContentTemplate>
            <div class="panel-group" id="accordion">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Zone Master</a>
                        </h4>
                    </div>
                    <div id="collapseSearch" class="panel-collapse collapse in">
                        <div class="panel-body">
                            <div class="container">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlMasterCountry">Country</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlMasterCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlMasterCountry_SelectedIndexChanged">
                                                    <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlMasterCity">City</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlMasterCity" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-6">
                                                <asp:Button ID="btnAddZone" runat="server" CssClass="btn btn-primary btn-sm" Text="Add" OnClientClick="showAddZoneModal()" OnClick="btnAddZone_Click" />
                                            </div>
                                            <div class="col-sm-6"></div>
                                        </div>
                                    </div>

                                    <div class="col-sm-6">

                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlZoneType">Zone type</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlZoneType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlStatus">Status</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                     <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                                                    <asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-6">
                                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" />
                                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" OnClick="btnReset_Click" />
                                            </div>
                                            <div class="col-sm-6">
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--Search Grid-->
            <asp:UpdatePanel ID="UPsearchGrid" runat="server">
                <ContentTemplate>
                    <div class="panel-group" id="accordionResult">
                        <div class="panel panel-default">
                            <div class="panel-heading clearfix">
                                <div class="col-md-6">
                                    <h4 class="panel-title pull-left">
                                        <a data-toggle="collapse" data-parent="#accordionResult" href="#collapseSearchResult">Search Results (Total Count:
                                    <asp:Label ID="lblTotalCount" runat="server" Text="0"></asp:Label>)
                                        </a>
                                    </h4>
                                </div>
                                <div class="input-group col-md-3 pull-right" runat="server" id="divDropdownForEntries">
                                    <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                                    <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged">
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="25">25</asp:ListItem>
                                        <asp:ListItem Value="50">50</asp:ListItem>
                                        <asp:ListItem Value="100">100</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                            </div>

                            <div id="collapseSearchResult" class="panel-collapse collapse in">
                                <div class="panel-body">
                                    <div class="form-group">
                                        <div id="dvMsgDeleted" runat="server" style="display: none;"></div>
                                    </div>

                                    <asp:GridView ID="grdZoneSearch" runat="server" AllowPaging="True" AllowCustomPaging="true" AutoGenerateColumns="False"
                                        EmptyDataText="No Zones Found" CssClass="table table-hover table-striped" DataKeyNames="Zone_id,Latitude,Longitude"
                                        OnPageIndexChanging="grdZoneSearch_PageIndexChanging" OnRowCommand="grdZoneSearch_RowCommand" OnRowDataBound="grdZoneSearch_RowDataBound"
                                        EnableViewState="true">
                                        <Columns>
                                            <asp:BoundField DataField="Zone_Type" HeaderText="Zone Type" />
                                            <asp:BoundField DataField="CountryName" HeaderText="Country" />
                                            <asp:BoundField DataField="CityName" HeaderText="City" />
                                            <asp:BoundField DataField="Zone_Name" HeaderText="Zone Name" />
                                            <asp:BoundField DataField="NoOfHotels" HeaderText="Number Of Hotels" />
                                            <asp:TemplateField HeaderText="view-Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                                        Enabled="true" CommandArgument='<%#Bind("Zone_id")%>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp View/Edit
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"    %>'
                                                        CssClass="btn btn-default" CommandArgument='<%# Bind("Zone_id") %>'>
                                                    <span aria-hidden="true" class='<%# Eval("IsActive").ToString() ==  "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat" %>'></span>
                                                    <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"     %>
                                                    </asp:LinkButton>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                        <PagerStyle CssClass="pagination-ys"/>

                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!--ADD Modal-->
    <div class="modal fade" id="moAddZoneModal" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header" style="padding: 5px 5px 5px 15px;">
                    <h4 class="modal-title"><b>Add Zone Master </b></h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div style="display: none" runat="server" id="dvmsgAdd"></div>
                            <div class="panel-group" id="">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <asp:HiddenField ClientIDMode="Static" ID="hdnPlaceId" runat="server"/>
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4" for="txtAddZoneName">Name</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtAddZoneName" runat="server" CssClass="form-control">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4" for="ddlMasterCountryAddModal">Country</label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlMasterCountryAddModal" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlMasterCountryAddModal_SelectedIndexChanged">
                                                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4" for="txtLatitude">Latitude</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtLatitude" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4" for="ddlAddHotelIncludeRange">Include Hotels Upto Range (km)</label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlAddHotelIncludeRange" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-sm-12">
                                                        <button type="button" id="btnGetLatLong" class="btn btn-primary btn-sm" onclick="getLatLong()">Get Latitude and Longitude</button>
                                                        <button type="button" id="btnGetLatLongOnMap" class="btn btn-primary btn-sm" onclick="GetLatLongOnMap()">Get LatLong On Map</button>
                                                    </div>
                                                </div>
                                              
                                            </div>

                                            <div class="col-sm-6">
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4" for="ddlAddZoneType">Zone type</label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlAddZoneType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4" for="ddlMasterCityAddModal">City</label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlMasterCityAddModal" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <%-- <div class="form-group row">
                                                <label class="control-label col-sm-4" for="ddlAddStatus">Status</label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlAddStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>--%>
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4" for="txtLongitude">Longitude</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtLongitude" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-sm-4"></div>
                                                    <div  class="col-sm-8">
                                                        <div class="col-sm-6">
                                                            <asp:Button ID="btnSaveZoneMaster" runat="server" CssClass="btn btn-primary btn-sm" Text="Save" OnClick="btnSaveZoneMaster_Click" />
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <asp:Button ID="btnCancelZoneMaster" runat="server" CssClass="btn btn-primary btn-sm" Text="Cancel" OnClick="btnCancelZoneMaster_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div id="dvLatLongMap" style="height: 300px" runat="server">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
