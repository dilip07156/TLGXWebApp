<%@ Page Language="C#" Title="Edit Zone-City Master" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ZoneCityMasterEdit.aspx.cs" Inherits="TLGX_Consumer.geography.ZoneCityMasterEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function ChangedCountry() {
            var newCountry = $("#MainContent_ddlMasterCountryEdit").val();
            var oldCountry = $("#hdnCountryId").val();
            if (newCountry != oldCountry) {
                var result = window.confirm('Country for this zone has been changed..!All cities will be deleted..');
                if (result == true)
                    $("#HdnCountryChangeFlag").val("True");
                else
                    $("#HdnCountryChangeFlag").val("False");
            }
            else $("#HdnCountryChangeFlag").val("False");
        }
    </script>

    <div class="row page-header">
        <div class="col-sm-8">
            <h3>Edit Zone -<strong>
                <asp:Label ID="lblEditZoneName" runat="server"></asp:Label></strong></h3>
        </div>
        <div class="col-sm-4">
            <div class="pull-right" style="margin-top: 25px !important;">
                <asp:Button runat="server" ID="btnRedirectToSearch" OnClick="btnRedirectToSearch_Click" CssClass="btn btn-link" Text="Go Back to ZoneSearch Page" />
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdateEditZone" runat="server">
        <ContentTemplate>
            <div style="display: none" runat="server" id="dvUpdateMsg"></div>
            <div class="row">
                <div class="panel-group" id="">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <asp:HiddenField ID="hdnCountryId" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="HdnCountryChangeFlag" runat="server" ClientIDMode="Static" />
                            <div class="row">
                                <div class="col-sm-12" style="display: none" id="dvmsgUpdateZone" runat="server"></div>
                                <div class="col-sm-6">
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="txtEditZoneName">Name</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtEditZoneName" runat="server" CssClass="form-control">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="txtEditLatitude">Latitude</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtEditLatitude" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlMasterCountryEdit">Country</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlMasterCountryEdit" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlMasterCountryEdit_SelectedIndexChanged">
                                                <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-6">

                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlEditZoneType">Zone type</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlEditZoneType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="txtEditLongitude">Longitude</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtEditLongitude" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-6">
                                            <asp:Button ID="btnUpdateZoneMaster" runat="server" CssClass="btn btn-primary btn-sm" Text="UPDATE" OnClick="btnUpdateZoneMaster_Click" OnClientClick="ChangedCountry()" />
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div id="Tabs" class="panel-body" role="tabpanel">
                                    <ul class="nav nav-tabs tabs" role="tablist">
                                        <li class="active"><a role="tab" data-toggle="tab" aria-controls="ShowZoneCities" href="#ShowZoneCities" id="ShowSupplier">Zone Cities</a></li>
                                        <li><a role="tab" data-toggle="tab" aria-controls="ShowZoneHotelList" href="#ShowZoneHotelList">Zone Hotel-List</a></li>
                                        <li><a role="tab" data-toggle="tab" aria-controls="MapHotels" href="#MapHotels">Map Hotels</a></li>
                                    </ul>
                                    <div class="tab-content">

                                        <!--For Cities-->
                                        <div role="tabpanel" id="ShowZoneCities" class="tab-pane fade in active">
                                            <br />
                                            <div class="col-sm-12">
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="ddlMasterCityEdit" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                   
                                                </div>
                                                <div class="col-sm-8 pull-left">
                                                    <asp:Button ID="btnAddZoneCity" runat="server" CssClass="btn btn-primary btn-sm" Text="Add City" OnClick="btnAddZoneCity_Click" />
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <br />
                                                <div style="display: none" runat="server" id="dvMsgaddZoneCity"></div>
                                                <asp:GridView ID="grdZoneCities" runat="server" AutoGenerateColumns="False"
                                                    EmptyDataText="No cities found for thiz Zone " CssClass="table table-hover table-striped" DataKeyNames="ZoneCityMapping_Id">
                                                    <Columns>
                                                        <asp:BoundField DataField="CityName" HeaderText="City Name" />
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDeleteZoneCity" runat="server" CausesValidation="false" CommandName='<%# Eval("Status").ToString() == "Fasle" ? "UnDelete" : "SoftDelete"  %>'
                                                                    CssClass="btn btn-default" CommandArgument='<%# Bind("ZoneCityMapping_Id") %>'>
                                                    <span aria-hidden="true" class='<%# Eval("Status").ToString() == "False" ? "glyphicon glyphicon-repeat" : "glyphicon glyphicon-remove" %>'></span>
                                                    <%# Eval("Status").ToString() == "False" ? "UnDelete" : "Delete"   %>
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

                                         </div>

                                        <!--For Hotel-Map-->
                                         <div role="tabpanel" id="MapHotels" class="tab-pane fade in">
                                            <br />

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
</asp:Content>
