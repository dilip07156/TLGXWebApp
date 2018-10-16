<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="cityManager.ascx.cs" Inherits="TLGX_Consumer.controls.geography.cityMapper" %>
<script type="text/javascript">
    function showCityMappingModal() {
        //var country = $('#<%= ddlCountry.ClientID %>').val();
        var country = $("#MainContent_cityManager_ddlCountry option:selected").text();
        $("#lblCountryForCity").text(country);
        $("#moCityAddUpdate").modal('show');
    }
    function closeCityMappingModal() {
        $("#moCityAddUpdate").modal('hide');
    }
    function pageLoad(sender, args) {
        var hvCity = $('#MainContent_cityManager_hdnFlagCity').val();

        if (hvCity == "true") {
            closeCityMappingModal();
            $('#MainContent_cityManager_hdnFlagCity').val("false");
        }
    }

</script>

<asp:UpdatePanel ID="panSearchConditions" runat="server">
    <ContentTemplate>
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="row">
                        <div class="form-group">
                            <div class="col-sm-12">
                                <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="CityManager" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-inline">
                            <div class="col-sm-12">

                                <label for="ddlCountry">
                                    Country
                            <asp:RequiredFieldValidator ValidationGroup="CityManager" runat="server" ControlToValidate="ddlCountry"
                                Text="*" CssClass="text-danger" InitialValue="0" ErrorMessage="Please select a country before search !" />
                                </label>
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                </asp:DropDownList>


                                <label for="txtCityName">CityName</label>
                                <asp:TextBox ID="txtCityName" runat="server" CssClass="form-control" />

                                <asp:LinkButton ID="btnGetCities" runat="server" CommandName="Search" Text="Search" CssClass="btn btn-primary btn-sm" ValidationGroup="CityManager" OnClick="btnGetCities_Click" />
                                <asp:LinkButton ID="btnNewCity" runat="server" CommandName="Create" Text="Create City" CssClass="btn btn-primary btn-sm pull-right" OnClick="btnNewCity_Click" OnClientClick="showCityMappingModal();" Visible="false" />

                            </div>
                        </div>



                        <%--<asp:LinkButton ID="btnGetCities" runat="server" CommandName="Search" Text="Search" CssClass="btn btn-primary btn-sm" ValidationGroup="vldgrpHealthAndSafety" OnClick="btnGetCities_Click" />--%>
                    </div>
                </div>
                <div class="panel-body">
                    <div runat="server" id="cityResult" style="display: none;">
                        <div class="panel-group" id="accordionSupplierSearch">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title"><a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Results</a></h4>
                                </div>
                                <div id="collapseSearch" class="panel-collapse collapse in">
                                    <div class="panel-body">
                                        <div id="dvMsgCity2" runat="server" style="display: none;"></div>
                                        <asp:HiddenField ID="hdnFlagCity" runat="server" Value="" EnableViewState="false" />
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

                                        <asp:GridView ID="grdCityList" EmptyDataText="No Data Found." runat="server" AllowPaging="True" AllowCustomPaging="true" AllowSorting="True" AutoGenerateColumns="False"
                                            DataKeyNames="City_Id, Country_Id" CssClass="table table-hover table-striped" OnPageIndexChanging="grdCityList_PageIndexChanging" OnRowCommand="grdCityList_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
                                                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                                <asp:BoundField DataField="StateCode" HeaderText="State Code" SortExpression="StateCode" />
                                                <asp:BoundField DataField="StateName" HeaderText="State Name" SortExpression="StateName" />
                                                <asp:BoundField DataField="CountryName" HeaderText="CountryName" SortExpression="CountryName" />
                                                <asp:BoundField DataField="Edit_Date" HeaderText="Edit_Date" SortExpression="Edit_Date" />
                                                <asp:BoundField DataField="Edit_User" HeaderText="Edit_User" SortExpression="Edit_User" />
                                                <asp:BoundField HeaderText="# Hotels" DataField="TotalHotelRecords" SortExpression="TotalHotelRecords" />
                                                <asp:BoundField HeaderText="# Attractions" DataField="TotalAttractionsRecords" SortExpression="TotalAttractionsRecords" />
                                                <asp:BoundField HeaderText="# Supplier City" DataField="TotalSupplierCityRecords" SortExpression="TotalSupplierCityRecords" />
                                                <%-- <asp:HyperLinkField DataNavigateUrlFields="City_Id" ControlStyle-CssClass="btn btn-default" DataNavigateUrlFormatString="~/geography/city?City_Id={0}" Text="Manage" NavigateUrl="~/geography/city" HeaderText="Manage" />--%>
                                                <asp:TemplateField ShowHeader="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default btn-sm"
                                                            Enabled="true" CommandArgument='<%# Bind("City_Id") %>'>
                                                            Manage
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                            <PagerStyle CssClass="pagination-ys" />
                                        </asp:GridView>
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






<asp:UpdatePanel ID="updatePanel1" runat="server">
    <ContentTemplate>



        <div class="container">
            <div class="row">
                <div class="col-lg-3">
                    <div class="panel panel-default">
                        <div class="panel-heading">City Details</div>
                        <asp:ValidationSummary ID="vlsum" runat="server" ValidationGroup="CityDetails" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                        <div class="panel-body">
                            <div id="dvMsgCity" runat="server" style="display: none;"></div>

                            <asp:FormView ID="frmCityMaster" runat="server" DataKeyNames="City_Id" DefaultMode="Edit" CssClass="form-group" OnItemCommand="frmCityMaster_ItemCommand">
                                <EditItemTemplate>

                                    <label for="txtCityName">City Name</label>
                                    <asp:TextBox ID="txtCityName" runat="server" CssClass="form-control" Text='<%# Bind("Name") %>' Enabled="true" />

                                    <label for="txtCityCode">City Code</label>
                                    <asp:TextBox ID="txtCityCode" runat="server" CssClass="form-control" Text='<%# Bind("Code") %>' Enabled="false" />

                                    <label for="ddlState">
                                        State
                                        <asp:RequiredFieldValidator ValidationGroup="CityDetails" runat="server" ControlToValidate="ddlState"
                                            Text="*" CssClass="text-danger" InitialValue="0" ErrorMessage="Please select a State." />
                                    </label>

                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                    </asp:DropDownList>

                                    <label for="txtStateCode">State Code</label>
                                    <asp:TextBox ID="txtStateCode" runat="server" CssClass="form-control" Text='<%# Bind("Code") %>' Enabled="false" />

                                    <label for="txtKey">Key</label>
                                    <asp:RequiredFieldValidator ValidationGroup="CityDetails" runat="server" ControlToValidate="txtKey"
                                        Text="*" CssClass="text-danger" InitialValue="0" ErrorMessage="Please enter the Key." />
                                    <asp:TextBox ID="txtKey" runat="server" CssClass="form-control" Text='<%# Bind("Key") %>' />

                                    <label for="txtRank">Rank</label>
                                    <asp:RequiredFieldValidator ValidationGroup="CityDetails" runat="server" ControlToValidate="txtRank"
                                        Text="*" CssClass="text-danger" InitialValue="0" ErrorMessage="Please enter the Rank." />
                                    <asp:TextBox ID="txtRank" runat="server" CssClass="form-control" Text='<%# Bind("Rank") %>' />

                                    <label for="txtPriority">Priority</label>
                                    <asp:RequiredFieldValidator ValidationGroup="CityDetails" runat="server" ControlToValidate="txtPriority"
                                        Text="*" CssClass="text-danger" InitialValue="0" ErrorMessage="Please enter the Priority." />
                                    <asp:TextBox ID="txtPriority" runat="server" CssClass="form-control" Text='<%# Bind("Priority") %>' />

                                    <label for="txtGooglePlaceId">Google Place Id</label>
                                    <asp:TextBox ID="txtGooglePlaceId" runat="server" CssClass="form-control" Text='<%# Bind("Google_PlaceId") %>' Enabled="false" />

                                    <label for="txtEditUser">Edit User</label>
                                    <asp:TextBox ID="txtEditUser" runat="server" CssClass="form-control" Text='<%# Bind("Edit_User") %>' Enabled="false" />

                                    <label for="txtEditDate">Edit Date</label>
                                    <asp:TextBox ID="txtEditDate" runat="server" CssClass="form-control" Text='<%# Bind("Edit_Date") %>' Enabled="false" />

                                    <br />
                                    <asp:Button ID="btnUpdateCityMaster" runat="server" Text="Update" CommandName="UpdateCityManager" CssClass="btn btn-primary btn-sm" ValidationGroup="CityDetails" />


                                </EditItemTemplate>
                            </asp:FormView>
                        </div>
                    </div>

                </div>

                <div class="col-lg-9">

                    <ul class="nav nav-tabs">
                        <li class="active"><a data-toggle="tab" href="#CityAreas">Below City Areas</a></li>
                        <li><a data-toggle="tab" href="#SupplierCityMapping">Supplier City Mapping</a></li>
                    </ul>

                    <div class="tab-content">
                        <div id="CityAreas" class="tab-pane fade in active">
                            <h4>City Areas</h4>
                            <div class="row">
                                <div class="col-lg-8">

                                    <asp:GridView ID="grdCityAreas" DataKeyNames="CityArea_Id" runat="server" CssClass="table table-hover table-striped" AutoGenerateColumns="False"
                                        EmptyDataText="No City Areas defined" OnSelectedIndexChanged="grdCityAreas_SelectedIndexChanged">
                                        <Columns>
                                            <asp:BoundField HeaderText="Name" DataField="Name" SortExpression="Name" />
                                            <asp:BoundField HeaderText="Code" DataField="Code" SortExpression="Code" />
                                            <asp:CommandField ShowSelectButton="True" SelectText="Edit" />
                                        </Columns>
                                    </asp:GridView>
                                    <br />

                                </div>
                                <div class="col-lg-4">
                                    <asp:ValidationSummary ID="vlSumCityArea" runat="server" DisplayMode="BulletList" ValidationGroup="vlgrpCityArea" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                    <asp:FormView ID="frmCityArea" runat="server" DataKeyNames="CityArea_Id" DefaultMode="Insert" OnItemCommand="frmCityArea_ItemCommand" OnItemInserting="frmCityArea_ItemInserting">
                                        <InsertItemTemplate>
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Add City Area</div>
                                                <div class="panel-body">

                                                    <label for="txtCityAreaName">
                                                        Area Name 
                                                        <asp:RequiredFieldValidator ID="vlCityArea" runat="server" ControlToValidate="txtCityAreaName" ValidationGroup="vlgrpCityArea" ErrorMessage="Please Enter the Area Name." Text="*" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:TextBox ID="txtCityAreaName" runat="server" CssClass="form-control" />

                                                    <label for="txtCityAreaCode">Area Code</label>
                                                    <asp:TextBox ID="txtCityAreaCode" runat="server" CssClass="form-control" />


                                                    <br />
                                                    <asp:Button ID="btnCityArea" runat="server" Text="Add" CommandName="Add" CssClass="btn btn-primary btn-sm" ValidationGroup="vlgrpCityArea" />
                                                </div>
                                            </div>


                                        </InsertItemTemplate>

                                        <EditItemTemplate>
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Update City Area</div>
                                                <div class="panel-body">

                                                    <label for="txtCityAreaName">
                                                        Name
                                                        <asp:RequiredFieldValidator ID="vlCityAreaLoc" runat="server" ControlToValidate="txtCityAreaName" ValidationGroup="vlgrpCityArea" ErrorMessage="Please Enter the Area Name." Text="*" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:TextBox ID="txtCityAreaName" runat="server" CssClass="form-control" Text='<%# Bind("Name") %>' />

                                                    <label for="txtCityAreaCode">Code</label>
                                                    <asp:TextBox ID="txtCityAreaCode" runat="server" CssClass="form-control" Text='<%# Bind("Code") %>' />


                                                    <br />
                                                    <asp:Button ID="btnCityArea" runat="server" Text="Save" CommandName="Save" CssClass="btn btn-primary btn-sm" ValidationGroup="vlgrpCityArea" />
                                                    <asp:Button ID="btnresetCityAreaLocation" runat="server" Text="Reset" CssClass="btn btn-primary btn-sm" OnClick="btnresetCityAreaLocation_Click" />

                                                </div>
                                            </div>

                                        </EditItemTemplate>

                                    </asp:FormView>


                                </div>
                            </div>

                            <h4>Area Locations</h4>

                            <div class="row" runat="server" id="dvCityAreaLocations" visible="false">
                                <div class="col-lg-8">

                                    <asp:GridView ID="grdCityAreaLocation" DataKeyNames="CityAreaLocation_Id" runat="server" CssClass="table table-hover table-striped" AutoGenerateColumns="false"
                                        EmptyDataText="No City Areas Locations defined" OnSelectedIndexChanged="grdCityAreaLocation_SelectedIndexChanged">
                                        <Columns>
                                            <asp:BoundField HeaderText="Name" DataField="Name" SortExpression="Name" />
                                            <asp:BoundField HeaderText="Code" DataField="Code" SortExpression="Code" />
                                            <asp:CommandField ShowSelectButton="True" SelectText="Edit" />
                                        </Columns>
                                    </asp:GridView>




                                </div>
                                <div class="col-lg-4" runat="server" id="dvCityAreaLocationDetail">
                                    <asp:ValidationSummary ID="vlSumCityAreaLoc" runat="server" DisplayMode="BulletList" ValidationGroup="vlgrpCityAreaLoc" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                    <asp:FormView ID="frmCityAreaLocation" runat="server" DataKeyNames="CityAreaLocation_Id" DefaultMode="Insert" OnItemCommand="frmCityAreaLocation_ItemCommand">
                                        <InsertItemTemplate>
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Add City Area Location </div>
                                                <div class="panel-body">

                                                    <label for="txtCityAreaName">
                                                        Area Name
                                                        <asp:RequiredFieldValidator ID="vlCityAreaLoc" runat="server" ControlToValidate="txtCityAreaName" ValidationGroup="vlgrpCityAreaLoc" ErrorMessage="Please Enter the Area Location Name." Text="*" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:TextBox ID="txtCityAreaName" runat="server" CssClass="form-control" />

                                                    <label for="txtCityAreaCode">Area Code</label>
                                                    <asp:TextBox ID="txtCityAreaCode" runat="server" CssClass="form-control" />


                                                    <br />
                                                    <asp:Button ID="btnCityAreaLocation" runat="server" Text="Add" CommandName="Add" CssClass="btn btn-primary btn-sm" ValidationGroup="vlgrpCityAreaLoc" />
                                                </div>
                                            </div>


                                        </InsertItemTemplate>

                                        <EditItemTemplate>
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Update City Area Location</div>
                                                <div class="panel-body">

                                                    <label for="txtCityAreaName">
                                                        Name
                                                        <asp:RequiredFieldValidator ID="vlCityAreaLoc" runat="server" ControlToValidate="txtCityAreaName" ValidationGroup="vlgrpCityAreaLoc" ErrorMessage="Please Enter the Area Name Location." Text="*" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                    </label>
                                                    <asp:TextBox ID="txtCityAreaName" runat="server" CssClass="form-control" Text='<%# Bind("Name") %>' />

                                                    <label for="txtCityAreaCode">Code</label>
                                                    <asp:TextBox ID="txtCityAreaCode" runat="server" CssClass="form-control" Text='<%# Bind("Code") %>' />


                                                    <br />
                                                    <asp:Button ID="btnCityAreaLocation" runat="server" Text="Save" CommandName="Save" CssClass="btn btn-primary btn-sm" ValidationGroup="vlgrpCityAreaLoc" />
                                                </div>
                                            </div>

                                        </EditItemTemplate>

                                    </asp:FormView>

                                </div>
                            </div>

                        </div>
                        <div id="SupplierCityMapping" class="tab-pane fade in">
                        </div>
                    </div>
                </div>
            </div>


        </div>



    </ContentTemplate>
</asp:UpdatePanel>

<!--Add/Update 'City' Modal-->
<div class="modal fade" id="moCityAddUpdate" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <div class="panel panel-default">
                    <div class="input-group">
                        <h4 class="input-group-addon">Add City for </h4>
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
                            <%--<EditItemTemplate>
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
                            </EditItemTemplate>--%>
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
                                                    <asp:LinkButton ID="btnSave" runat="server" CausesValidation="True" CommandName="AddCity" Text="Save City" CssClass="btn btn-primary btn-sm" ValidationGroup="City" />
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

