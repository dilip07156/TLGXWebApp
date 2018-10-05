<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="countryManager.ascx.cs" Inherits="TLGX_Consumer.controls.geography.countrymapper" %>
<%@ Register Src="~/controls/geography/supplierCountryMapping.ascx" TagPrefix="uc1" TagName="supplierCountryMapping" %>

<script type="text/javascript">

    function showCountryMappingModal() {
        //alert("Hi");
        $("#moStateUpdate").modal('show');
    }
    function closeCountryMappingModal() {
        //alert("Hi");
        $("#moStateUpdate").modal('close');
    }
    function closeAddUpdateCountryModal() {
        $("#moAddUpdateCountry").modal('hide');
    }
    function showAddUpdateCountryModal() {
        $("#moAddUpdateCountry").modal('show');
    }
    function pageLoad(sender, args) {
        //var hv = $('#MainContent_UserManagement_hdnFlag').val();
        //if (hv == "true") {
        //    closeAddUpdateUserModal();
        //    $('#MainContent_UserManagement_hdnFlag').val("false");
        //}
    }
</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group row ">
                                <asp:Label runat="server" AssociatedControlID="ddlKey" CssClass="col-md-4 control-label">Key
                                </asp:Label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlKey" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group row">
                                <asp:Label runat="server" AssociatedControlID="ddlRank" CssClass="col-md-4 control-label">Rank
                                </asp:Label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlRank" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group row">
                                <asp:Label runat="server" AssociatedControlIDl="ddlRegion" CssClass="col-md-4 control-label">Region</asp:Label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <%--Varun Added--%>

                            <div class="form-group row">
                                <asp:Label runat="server" AssociatedControlID="txtCountryNameSearch" CssClass="col-md-4 control-label">Countries:
                                </asp:Label>
                                <div class="col-md-8">
                                    <div class="form-group form-inline">
                                        <asp:TextBox ID="txtCountryNameSearch" Width="50%" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:Button ID="btnFilter" runat="server" CssClass="btn btn-primary btn-sm" Text="Find" OnClick="btnFilter_Click" />
                                        <asp:Button runat="server" ID="btnNewCreate" OnClientClick="showAddUpdateCountryModal();" CssClass="btn btn-sm btn-primary pull-right" Text="Create New Country" />
                                    </div>
                                </div>
                            </div>

                            <%--varun Ended--%>
                        </div>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-group pull-right col-md-2 " id="divEntries" runat="server">
                        <div class="input-group">
                            <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                            <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged" CssClass="form-control">
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
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:GridView ID="grdCurrentCountryList" runat="server" AllowPaging="true" AllowCustomPaging="true" AutoGenerateColumns="False" CssClass="table table-hover table-striped" DataKeyNames="Country_Id"
                                OnSelectedIndexChanged="grdCurrentCountryList_SelectedIndexChanged" EmptyDataText="No Data Found !" OnPageIndexChanging="grdCurrentCountryList_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                    <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                    <asp:BoundField DataField="RegionName" HeaderText="Region" SortExpression="RegionName" />
                                    <asp:BoundField DataField="Key" HeaderText="Key" SortExpression="Key" />
                                    <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank" />
                                    <asp:BoundField DataField="Priority" HeaderText="Priority" SortExpression="Priority" />
                                    <asp:BoundField HeaderText="# Hotels" />
                                    <asp:BoundField HeaderText="# Attractions" />
                                    <asp:BoundField HeaderText="# Holidays" />
                                    <asp:HyperLinkField DataNavigateUrlFields="Country_Id" DataNavigateUrlFormatString="~/geography/CountryStateMgmt?Country_Id={0}"
                                        Text="Select" NavigateUrl="~/geography/CountryStateMgmt" HeaderText="Manage" ControlStyle-CssClass="btn btn-default" />
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

<%--Add / Update Country--%>
<div class="modal fade" id="moAddUpdateCountry" role="dialog">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <div class="input-group">
                    <h4 class="input-group-addon">Add / Update Country </h4>
                    <%-- <strong>
                        <label id="lblApplication" class="form-control"></label>
                    </strong>--%>
                </div>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel ID="UpdUserAddModal" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnFlag" runat="server" Value="" EnableViewState="false" />
                        <div class="container">
                            <div class="row">
                                <div class="form-group">
                                    <div class="col-sm-9">
                                        <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="Country" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                        <div id="msgAlert" runat="server" style="display: none;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading">Country Information</div>
                                    <div class="panel-body">
                                        <asp:FormView ID="frmCountrydetail" CssClass="col-lg-12" runat="server" DefaultMode="Insert" OnItemCommand="frmCountrydetail_ItemCommand">
                                            <InsertItemTemplate>
                                                <div class="col-lg-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h5>Country Basic Details</h5>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="col-lg-6">
                                                                <div class="form-group row">
                                                                    <asp:Label runat="server" AssociatedControlID="txtCountryName" CssClass="col-md-4 control-label">Country Name
                                                                            <asp:RequiredFieldValidator ValidationGroup="Country" runat="server" ControlToValidate="txtCountryName"
                                                                                CssClass="text-danger" ErrorMessage="The country name field is required." Text="*" />
                                                                    </asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txtCountryName" CssClass="form-control" />

                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <asp:Label runat="server" AssociatedControlID="txtCountryCode" CssClass="col-md-4 control-label">Contry Code
                                                                              <asp:RequiredFieldValidator ValidationGroup="Country" runat="server" ControlToValidate="txtCountryCode"
                                                                                CssClass="text-danger" ErrorMessage="The country code field is required."  Text="*"/>
                                                                    </asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txtCountryCode" CssClass="form-control" />

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6">
                                                                <div class="form-group row">
                                                                    <asp:Label runat="server" AssociatedControlID="ddlStatus" CssClass="col-md-4 control-label">Status
                                                                             <asp:RequiredFieldValidator ValidationGroup="Country" runat="server" ControlToValidate="ddlStatus"
                                                                                CssClass="text-danger" ErrorMessage="The status is required." InitialValue ="0"  Text="*"/>
                                                                    </asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                                                                            <asp:ListItem Value="0">--Select --</asp:ListItem>
                                                                            <asp:ListItem Value="Active">Active</asp:ListItem>
                                                                            <asp:ListItem Value="InActive">InActive</asp:ListItem>
                                                                        </asp:DropDownList>

                                                                    </div>

                                                                </div>

                                                                <div class="form-group row">
                                                                    <asp:Label runat="server" AssociatedControlID="txtKey" CssClass="col-md-4 control-label">Key
                                        <asp:RequiredFieldValidator ValidationGroup="Country" runat="server" ControlToValidate="txtKey" Text="*"
                                            CssClass="text-danger" ErrorMessage="The Key field is required." />
                                                                    </asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txtKey" CssClass="form-control" />
                                                                    </div>
                                                                </div>

                                                            </div>

                                                            <div class="col-lg-6">
                                                                <div class="form-group row ">
                                                                    <asp:Label runat="server" AssociatedControlID="txtRegionName" CssClass="col-md-4 control-label">Region Name
                                        <asp:RequiredFieldValidator ValidationGroup="Country" runat="server" ControlToValidate="txtRegionName"
                                            CssClass="text-danger" Text="*" ErrorMessage="The Region name field is required." />
                                                                    </asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txtRegionName" CssClass="form-control" />
                                                                    </div>
                                                                </div>

                                                                <div class="form-group row">
                                                                    <asp:Label runat="server" AssociatedControlID="txtRegionCode" CssClass="col-md-4 control-label">Region Code
                                        <asp:RequiredFieldValidator ValidationGroup="Country" runat="server" ControlToValidate="txtCountryCode" Text="*"
                                            CssClass="text-danger" ErrorMessage="The Region Code field is required." />
                                                                    </asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txtRegionCode" CssClass="form-control" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-6">
                                                                <div class="form-group row ">
                                                                    <asp:Label runat="server" AssociatedControlID="txtRank" CssClass="col-md-4 control-label">Rank
                                        <asp:RequiredFieldValidator ValidationGroup="Country" runat="server" ControlToValidate="txtRank"
                                            CssClass="text-danger" Text="*" ErrorMessage="The Rank field is required." />
                                                                    </asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txtRank" CssClass="form-control" />
                                                                    </div>
                                                                </div>

                                                                <div class="form-group row">
                                                                    <asp:Label runat="server" AssociatedControlID="txtPriority" CssClass="col-md-4 control-label">Priority
                                        <asp:RequiredFieldValidator ValidationGroup="Country" runat="server" ControlToValidate="txtPriority" Text="*"
                                            CssClass="text-danger" ErrorMessage="The Priority field is required." />
                                                                    </asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txtPriority" CssClass="form-control" />
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h5>ISO Details</h5>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="col-lg-6">
                                                                <div class="row">
                                                                    <div class="form-group">
                                                                        <label for="txt_MARC" class="col-md-4 control-label">MARC</label>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox runat="server" ID="txt_MARC" CssClass="form-control" />
                                                                            <div class="clear">&nbsp;</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="form-group">
                                                                        <label for="txt_WMO" class="col-md-4 control-label">WMO</label>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox runat="server" ID="txt_WMO" CssClass="form-control" />
                                                                            <div class="clear">&nbsp;</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <label for="txt_ISOofficial_name_en" class="col-md-4 col-form-label">ISO Official Name</label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txt_ISOofficial_name_en" CssClass="form-control" />
                                                                        <div class="clear">&nbsp;</div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <label for="txt_ISO3166_1_Alpha_2" class="col-md-4 col-form-label">
                                                                        ISO3166-1-Alpha-2
                                                                        <asp:RequiredFieldValidator ValidationGroup="Country" runat="server" ControlToValidate="txt_ISO3166_1_Alpha_2" Text="*"
                                                                            CssClass="text-danger" ErrorMessage="The ISO3166-1-Alpha-2 field is required." />
                                                                    </label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txt_ISO3166_1_Alpha_2" CssClass="form-control" />
                                                                        <div class="clear">&nbsp;</div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <label for="txt_ISO3166_1_Alpha_3" class="col-md-4 col-form-label">
                                                                        ISO3166-1-Alpha-3
                                                                        <asp:RequiredFieldValidator ValidationGroup="Country" runat="server" ControlToValidate="txt_ISO3166_1_Alpha_3" Text="*"
                                                                            CssClass="text-danger" ErrorMessage="The ISO3166-1-Alpha-3 field is required." />
                                                                    </label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txt_ISO3166_1_Alpha_3" CssClass="form-control" />
                                                                        <div class="clear">&nbsp;</div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <label for="txt_ISO3166_1_M49" class="col-md-4 col-form-label">ISO3166-1-M49</label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txt_ISO3166_1_M49" CssClass="form-control" />
                                                                        <div class="clear">&nbsp;</div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <label for="txt_ISO3166_1_ITU" class="col-md-4 col-form-label">ISO3166-1-ITU</label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txt_ISO3166_1_ITU" CssClass="form-control" />
                                                                        <div class="clear">&nbsp;</div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <label for="txt_ISO4217_currency_alphabetic_code" class="col-md-4 col-form-label">Currency Alphabetic Code</label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txt_ISO4217_currency_alphabetic_code" CssClass="form-control" />
                                                                        <div class="clear">&nbsp;</div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <label for="txt_ISO4217_currency_country_name" class="col-md-4 col-form-label">Currency Country Name</label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txt_ISO4217_currency_country_name" CssClass="form-control" />
                                                                        <div class="clear">&nbsp;</div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <label for="txt_ISO4217_currency_minor_unit" class="col-md-4 col-form-label">Currency Minor Unit</label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txt_ISO4217_currency_minor_unit" CssClass="form-control" />
                                                                        <div class="clear">&nbsp;</div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <label for="txt_GooglePlaceID" class="col-md-4 col-form-label">Google Place ID</label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txt_GooglePlaceID" ReadOnly="true" CssClass="form-control" />
                                                                        <div class="clear">&nbsp;</div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6">
                                                                <div class="row">
                                                                    <div class="form-group">
                                                                        <label for="txt_DS" class="col-md-4 control-label">DS</label>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox runat="server" ID="txt_DS" CssClass="form-control" />
                                                                            <div class="clear">&nbsp;</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="form-group">
                                                                        <label for="txt_Dial" class="col-md-4 control-label">Dial</label>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox runat="server" ID="txt_Dial" CssClass="form-control" />
                                                                            <div class="clear">&nbsp;</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <label for="txt_ISO4217_currency_name" class="col-md-4 col-form-label">Currency Name</label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txt_ISO4217_currency_name" CssClass="form-control" />
                                                                        <div class="clear">&nbsp;</div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <label for="txt_ISO4217_currency_numeric_code" class="col-md-4 col-form-label">Currency Numeric Code</label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txt_ISO4217_currency_numeric_code" CssClass="form-control" />
                                                                        <div class="clear">&nbsp;</div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <label for="txt_ISO3166_1_Capital" class="col-md-4 col-form-label">ISO3166-1-Capital</label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txt_ISO3166_1_Capital" CssClass="form-control" />
                                                                        <div class="clear">&nbsp;</div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <label for="txt_ISO3166_1_Continent" class="col-md-4 col-form-label">ISO3166-1-Continent</label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txt_ISO3166_1_Continent" CssClass="form-control" />
                                                                        <div class="clear">&nbsp;</div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <label for="txt_ISO3166_1_TLD" class="col-md-4 col-form-label">ISO3166-1-TLD</label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txt_ISO3166_1_TLD" CssClass="form-control" />
                                                                        <div class="clear">&nbsp;</div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <label for="txt_ISO3166_1_Languages" class="col-md-4 col-form-label">ISO3166-1-Languages</label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txt_ISO3166_1_Languages" CssClass="form-control" />
                                                                        <div class="clear">&nbsp;</div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <label for="txt_ISO3166_1_Geoname_ID" class="col-md-4 col-form-label">ISO3166-1-Geoname ID</label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txt_ISO3166_1_Geoname_ID" CssClass="form-control" />
                                                                        <div class="clear">&nbsp;</div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <label for="txt_ISO3166_1_EDGAR" class="col-md-4 col-form-label">ISO3166-1-EDGAR</label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txt_ISO3166_1_EDGAR" CssClass="form-control" />
                                                                        <div class="clear">&nbsp;</div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row pull-right col-md-2">
                                                    <asp:ValidationSummary runat="server" CssClass="text-danger" />
                                                    <asp:Button ID="btnEditUser" CommandName="Add" runat="server" Text="Save" CssClass="btn btn-primary btn-md" ValidationGroup="Country" />
                                                </div>
                                            </InsertItemTemplate>
                                        </asp:FormView>
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




