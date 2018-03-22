<%@ Page Title="Zone Master" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Zone.aspx.cs" Inherits="TLGX_Consumer.geography.Zone" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Src="~/controls/geography/zoneManager.ascx" TagPrefix="uc1" TagName="zoneManager" %>--%>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1 class="page-header">Zone Manager</h1>
    </div>
    <%--<uc1:zoneManager runat="server" id="zoneManager" />--%>
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

    </script>

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
                                                    <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
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
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-6">
                                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" />
                                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" OnClick="btnReset_Click" />
                                            </div>
                                            <div class="col-sm-6">
                                                <%--<asp:Button ID="btnBulkMap" Visible="false" runat="server" CssClass="btn btn-primary btn-sm" Text="Bulk Add City Mapping" OnClick="btnBulkMap_Click" />--%>
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
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>25</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
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
                                        OnPageIndexChanging="grdZoneSearch_PageIndexChanging" OnRowCommand="grdZoneSearch_RowCommand" OnRowDataBound="grdZoneSearch_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Zone_Type" HeaderText="Zone Type" />
                                            <asp:BoundField DataField="CountryName" HeaderText="Country" />
                                            <asp:BoundField DataField="CityName" HeaderText="City" />
                                            <asp:BoundField DataField="Zone_Name" HeaderText="Zone Name" />
                                            <%--<asp:BoundField DataField="Status" HeaderText="Status" />--%>
                                            <asp:BoundField DataField="" HeaderText="Number Of Hotels" />
                                            <asp:TemplateField HeaderText="view-Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                                        Enabled="true" CommandArgument='<%#Bind("Zone_id")%>' OnClientClick="showEditZoneModal();">
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp View/Edit
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" CausesValidation="false" CommandName='<%# Eval("Status").ToString() == "false" ? "UnDelete" : "SoftDelete"   %>'
                                                        CssClass="btn btn-default" CommandArgument='<%# Bind("Zone_id") %>'>
                                                    <span aria-hidden="true" class='<%# Eval("Status").ToString() == "false" ? "glyphicon glyphicon-repeat" : "glyphicon glyphicon-remove" %>'></span>
                                                    <%# Eval("Status").ToString() == "false" ? "UnDelete" : "Delete"   %>
                                                    </asp:LinkButton>
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
                                                    <div class="col-sm-12">
                                                        <button type="button" id="btnGetLatLong" class="btn btn-primary btn-sm" onclick="getLatLong()">Get Latitude and Longitude</button>
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
                                                    <div class="pull-right">
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
