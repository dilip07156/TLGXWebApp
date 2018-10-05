<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="countryStateMgmt.ascx.cs" Inherits="TLGX_Consumer.controls.geography.countryStateMgmt" %>
<script type="text/javascript">
    function showStateMappingModal() {
        var country = $('#<%= txtCountryName.ClientID %>').val();
        $("#lblCountry").text(country);
        $("#moStateUpdate").modal('show');
    }
    function closeStateMappingModal() {
        $("#moStateUpdate").modal('hide');
    }
    function showCityMappingModal() {
        var country = $('#<%= txtCountryName.ClientID %>').val();
        $("#lblCountryForCity").text(country);
        $("#moCityAddUpdate").modal('show');
    }
    function closeCityMappingModal() {
        $("#moCityAddUpdate").modal('hide');
    }
    function pageLoad(sender, args) {
        var hvCountry = $('#MainContent_countryStateMgmt_hdnFlagCountry').val();
        var hvState = $('#MainContent_countryStateMgmt_hdnFlagState').val();
        var hvCity = $('#MainContent_countryStateMgmt_hdnFlagCity').val();

        if (hvState == "true") {
            closeStateMappingModal();
            $('#MainContent_countryStateMgmt_hdnFlagState').val("false");
        }
        if (hvCity == "true") {

            closeCityMappingModal();
            $('#MainContent_countryStateMgmt_hdnFlagCity').val("false");
        }
    }
</script>
<div class="container" runat="server" id="dvChildtables">
    <h2 class="page-header">
        <label runat="server" id="lblCountryName"></label>
    </h2>
    <div class="panel panel-default">
        <div class="panel-body">
            <ul class="nav nav-tabs">
                <li class="active"><a data-toggle="tab" href="#CountryDetails">Country Details</a></li>
                <li><a data-toggle="tab" href="#StateMasters">State Masters</a></li>
                <li><a data-toggle="tab" href="#CityMasters">City Masters</a></li>
            </ul>
            <div class="tab-content">
                <div class="clear">&nbsp;</div>
                <div id="CountryDetails" class="tab-pane fade in active">
                    <div class="col-lg-12">
                        <div id="dvMsgCountry" runat="server" style="display: none;"></div>
                        <asp:HiddenField ID="hdnFlagCountry" runat="server" Value="" EnableViewState="false" />
                        <div class="container">
                            <div class="row">
                                <div class="form-group">
                                    <div class="col-sm-9">
                                        <asp:ValidationSummary ID="vldsmCoutry" runat="server" ValidationGroup="Country" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                        <div id="Div1" runat="server" style="display: none;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h5>Country Basic Details</h5>
                            </div>
                            <div class="panel-body">
                                <div class="col-lg-6">
                                    <div class="form-group row ">
                                        <asp:Label runat="server" AssociatedControlID="txtCountryName" CssClass="col-md-4 control-label">Country Name
                                        <asp:RequiredFieldValidator ValidationGroup="Country" runat="server" ControlToValidate="txtCountryName"
                                            CssClass="text-danger" Text="*" ErrorMessage="The email field is required." />
                                        </asp:Label>
                                        <div class="col-md-8">
                                            <asp:TextBox runat="server" ID="txtCountryName" CssClass="form-control" />
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <asp:Label runat="server" AssociatedControlID="txtCountryCode" CssClass="col-md-4 control-label">Contry Code
                                        <asp:RequiredFieldValidator ValidationGroup="Country" runat="server" ControlToValidate="txtCountryCode" Text="*"
                                            CssClass="text-danger" ErrorMessage="The email field is required." />
                                        </asp:Label>
                                        <div class="col-md-8">
                                            <asp:TextBox runat="server" ID="txtCountryCode" CssClass="form-control" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group row">
                                        <asp:Label runat="server" AssociatedControlID="ddlStatus" CssClass="col-md-4 control-label">Status</asp:Label>
                                        <div class="col-md-8">
                                            <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                                                <asp:ListItem Value="Active">Active</asp:ListItem>
                                                <asp:ListItem Value="InActive">InActive</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<div class="clear">&nbsp;</div>--%>
                                        </div>
                                    </div>
                                    <%--Varun Added--%>

                                    <div class="form-group row">
                                        <asp:Label runat="server" AssociatedControlID="txtKey" CssClass="col-md-4 control-label">Key
                                        <asp:RequiredFieldValidator ValidationGroup="Country" runat="server" ControlToValidate="txtKey" Text="*"
                                            CssClass="text-danger" ErrorMessage="The Key field is required." />
                                        </asp:Label>
                                        <div class="col-md-8">
                                            <asp:TextBox runat="server" ID="txtKey" CssClass="form-control" />
                                        </div>
                                    </div>

                                    <%--varun Ended--%>
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
                                        <asp:RequiredFieldValidator ValidationGroup="Country" runat="server" ControlToValidate="txtRegionCode" Text="*"
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
                                        <label for="txt_GooglePlaceID" class="col-md-4 col-form-label">GooglePlaceID</label>
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
                    <div class="form-group row">
                        <div class="col-md-1 pull-right">
                            <asp:ValidationSummary runat="server" CssClass="text-danger" />
                            <asp:Button ID="btnUpdateCountry" runat="server" Text="Update" OnClick="btnUpdateCountry_Click" CssClass="btn btn-primary btn-md" />
                        </div>
                    </div>
                </div>
                <div id="StateMasters" class="tab-pane fade in">
                    <asp:UpdatePanel ID="UpMediaModal" runat="server">
                        <ContentTemplate>
                            <div id="dvMsgState" runat="server" style="display: none;"></div>
                            <div class="form-group col-md-2">
                                <div class="input-group">
                                    <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                                    <asp:DropDownList ID="ddlShowEntriesState" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlShowEntriesState_SelectedIndexChanged" CssClass="form-control">
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
                            <div class="form-group pull-right col-md-2">
                                <asp:Button runat="server" ID="btnNewState" OnClientClick="showStateMappingModal();" CssClass="btn btn-sm btn-primary pull-right" Text="Create New State" OnClick="btnNewState_Click" />
                            </div>
                            <asp:GridView ID="grdStateList" runat="server" AllowCustomPaging="true" AllowPaging="true" AutoGenerateColumns="False" DataKeyNames="State_Id"
                                CssClass="table table-hover table-striped" OnRowCommand="grdStateList_RowCommand" OnPageIndexChanging="grdStateList_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="State_Code" HeaderText="StateCode" SortExpression="State_Code" />
                                    <asp:BoundField DataField="State_Name" HeaderText="Name" SortExpression="State_Name" />
                                    <asp:BoundField DataField="StateName_LocalLanguage" HeaderText="Local Lang" SortExpression="StateName_LocalLanguage" />
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default" OnClientClick="showStateMappingModal();"
                                                CommandArgument='<%# Bind("State_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pagination-ys" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div id="CityMasters" class="tab-pane fade in">
                    <asp:UpdatePanel ID="updtCityPage" runat="server">
                        <ContentTemplate>
                            <div id="dvMsgCity" runat="server" style="display: none;"></div>
                            <asp:HiddenField ID="hdnFlagCity" runat="server" Value="" EnableViewState="false" />
                            <div class="form-group col-md-2">
                                <div class="input-group">
                                    <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                                    <asp:DropDownList ID="ddlShowEntriesCity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlShowEntriesCity_SelectedIndexChanged" CssClass="form-control">
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
                            <div class="form-group pull-right col-md-2">
                                <asp:Button runat="server" ID="btnNewCity" OnClick="btnNewCity_Click" OnClientClick="showCityMappingModal();" CssClass="btn btn-sm btn-primary pull-right" Text="Create New City" />

                            </div>
                            <asp:GridView ID="grdCityList" runat="server"
                                OnPageIndexChanging="grdCityList_PageIndexChanging" AllowPaging="True" AllowCustomPaging="true" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="City_Id" CssClass="table table-hover table-striped">
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                    <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
                                    <asp:BoundField DataField="Edit_Date" HeaderText="Edit_Date" SortExpression="Edit_Date" />
                                    <asp:BoundField DataField="Edit_User" HeaderText="Edit_User" SortExpression="Edit_User" />
                                    <asp:BoundField HeaderText="# Hotels" />
                                    <asp:BoundField HeaderText="# Attractions" />
                                    <asp:BoundField HeaderText="# Holidays" />
                                    <asp:HyperLinkField DataNavigateUrlFields="City_Id" DataNavigateUrlFormatString="~/geography/city?City_Id={0}" Text="Select" NavigateUrl="~/geography/city" HeaderText="Manage" />
                                </Columns>
                                <PagerStyle CssClass="pagination-ys" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="modal fade" id="moStateUpdate" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <div class="panel panel-default">
                    <div class="input-group">
                        <h4 class="input-group-addon">Add/Update State for </h4>
                        <strong>
                            <label id="lblCountry" class="form-control"></label>
                        </strong>
                    </div>
                </div>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel ID="UpdCountryMapModal" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnFlagState" runat="server" Value="" EnableViewState="false" />
                        <div class="container">
                            <div class="row">
                                <div class="form-group">
                                    <div class="col-sm-6">
                                        <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="State" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                        <div id="msgAlertState" runat="server" style="display: none;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:FormView ID="frmStateUpdate" runat="server" DataKeyNames="State_Id" DefaultMode="Insert" OnItemCommand="frmStateUpdate_ItemCommand">
                            <EditItemTemplate>
                                <div class="container">
                                    <div class="row col-lg-6">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">Add State Information</div>
                                            <div class="panel-body">
                                                <div class="form-group row">
                                                    <label for="txtStateName" class="col-md-4 col-form-label">
                                                        State Name
                                                            <asp:RequiredFieldValidator ValidationGroup="State" Text="*" runat="server" ControlToValidate="txtStateName"
                                                                CssClass="text-danger" ErrorMessage="The State Name is required." />
                                                    </label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtStateName" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="txtStateCode" class="col-md-4 col-form-label">
                                                        State Code
                                                            <asp:RequiredFieldValidator ValidationGroup="State" Text="*" runat="server" ControlToValidate="txtStateCode"
                                                                CssClass="text-danger" ErrorMessage="The State Code is required." />
                                                    </label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtStateCode" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="txtLocalLanguage" class="col-md-4 col-form-label">
                                                        Local Language
                                                          <%--  <asp:RequiredFieldValidator ValidationGroup="State" Text="*" runat="server" ControlToValidate="txtLocalLanguage"
                                                                CssClass="text-danger" ErrorMessage="The State Name is required." />--%>
                                                    </label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtLocalLanguage" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>

                                                <div class="row pull-right col-md-3">
                                                    <asp:LinkButton ID="btnSave" runat="server" CausesValidation="True" CommandName="Save" Text="Update State" CssClass="btn btn-primary btn-sm" ValidationGroup="State" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <div class="container">
                                    <div class="row col-lg-6">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">Add State Information</div>
                                            <div class="panel-body">
                                                <div class="form-group row">
                                                    <label for="txtStateName" class="col-md-4 col-form-label">
                                                        State Name
                                                            <asp:RequiredFieldValidator ValidationGroup="State" Text="*" runat="server" ControlToValidate="txtStateName"
                                                                CssClass="text-danger" ErrorMessage="The State Name is required." />
                                                    </label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtStateName" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="txtStateCode" class="col-md-4 col-form-label">
                                                        State Code
                                                            <asp:RequiredFieldValidator ValidationGroup="State" Text="*" runat="server" ControlToValidate="txtStateCode"
                                                                CssClass="text-danger" ErrorMessage="The State Code is required." />
                                                    </label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtStateCode" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="txtLocalLanguage" class="col-md-4 col-form-label">
                                                        Local Language
                                                          <%--  <asp:RequiredFieldValidator ValidationGroup="State" Text="*" runat="server" ControlToValidate="txtLocalLanguage"
                                                                CssClass="text-danger" ErrorMessage="The State Name is required." />--%>
                                                    </label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtLocalLanguage" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>

                                                <div class="row pull-right col-md-3">
                                                    <asp:LinkButton ID="btnSave" runat="server" CausesValidation="True" CommandName="Add" Text="Save State" CssClass="btn btn-primary btn-sm" ValidationGroup="State" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </InsertItemTemplate>
                        </asp:FormView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
<div class="modal fade" id="moCityAddUpdate" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <div class="panel panel-default">
                    <div class="input-group">
                        <h4 class="input-group-addon">Add/Update City for </h4>
                        <strong>
                            <label id="lblCountryForCity" class="form-control"></label>
                        </strong>
                    </div>
                </div>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel ID="updpnlCity" runat="server">
                    <ContentTemplate>
                        <div class="container">
                            <div class="row">
                                <div class="form-group">
                                    <div class="col-sm-6">
                                        <asp:ValidationSummary ID="vldsumCity" runat="server" ValidationGroup="City" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                        <div id="dvmsgCityAlert" runat="server" style="display: none;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:FormView ID="frmvwCity" runat="server" DataKeyNames="City_Id" DefaultMode="Insert" OnItemCommand="frmvwCity_ItemCommand">
                            <EditItemTemplate>
                                <div class="container">
                                    <div class="row col-lg-6">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">Update City Information</div>
                                            <div class="panel-body">
                                                <div class="form-group row">
                                                    <label for="txtCityName" class="col-md-4 col-form-label">
                                                        City Name
                                            <asp:RequiredFieldValidator ValidationGroup="City" runat="server" ControlToValidate="txtCityName" Text="*"
                                                CssClass="text-danger" ErrorMessage="The city field is required." />
                                                    </label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtCityName" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="txtCityCode" class="col-md-4 col-form-label">
                                                        City Code
                                                    </label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtCityCode" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="ddlStateName">
                                                        State 
                                            <asp:RequiredFieldValidator ValidationGroup="City" runat="server" ControlToValidate="ddlStates" Text="*"
                                                CssClass="text-danger" ErrorMessage="The state is required." InitialValue="0" />
                                                    </label>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList ID="ddlStates" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="ddlStateName" class="col-md-4 col-form-label">
                                                        Status
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="Active">Active</asp:ListItem>
                                                            <asp:ListItem Value="InActive">InActive</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row pull-right col-md-3">
                                                    <asp:LinkButton ID="btnSave" runat="server" CausesValidation="True" CommandName="Save" Text="Update" CssClass="btn btn-primary btn-sm" ValidationGroup="City" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <div class="container">
                                    <div class="row col-lg-6">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">Add City Information</div>
                                            <div class="panel-body">
                                                <div class="form-group row">
                                                    <label for="txtCityName" class="col-md-4 col-form-label">
                                                        City Name
                                            <asp:RequiredFieldValidator ValidationGroup="City" runat="server" ControlToValidate="txtCityName" Text="*"
                                                CssClass="text-danger" ErrorMessage="The city field is required." />
                                                    </label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtCityName" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group row" style="display: none">
                                                    <label for="txtCityCode" class="col-md-4 col-form-label">
                                                        City Code
                                                    </label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtCityCode" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="ddlStateName" class="col-md-4 col-form-label">
                                                        State 
                                            <asp:RequiredFieldValidator ValidationGroup="City" runat="server" ControlToValidate="ddlStates" Text="*"
                                                CssClass="text-danger" ErrorMessage="The state is required." InitialValue="0" />
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="ddlStates" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="ddlStateName" class="col-md-4 col-form-label">
                                                        Status
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="Active">Active</asp:ListItem>
                                                            <asp:ListItem Value="InActive">InActive</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row pull-right col-md-3">
                                                    <asp:LinkButton ID="btnSave" runat="server" CausesValidation="True" CommandName="Add" Text="Save City" CssClass="btn btn-primary btn-sm" ValidationGroup="City" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </InsertItemTemplate>
                        </asp:FormView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
