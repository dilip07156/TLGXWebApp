<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="citySearchManager.ascx.cs" Inherits="TLGX_Consumer.controls.geography.citySearchManager" %>
<script type="text/javascript">
    function showCityMappingModal() {
        //var country = $('#<%= ddlCountry.ClientID %>').val();
        var country = $("#MainContent_citySearchManager_ddlCountry option:selected").text();
        $("#lblCountryForCity").text(country);
        $("#moCityAddUpdate").modal('show');
    }
    function closeCityMappingModal() {
        $("#moCityAddUpdate").modal('hide');
    }
    function pageLoad(sender, args) {
        var hvCity = $('#MainContent_citySearchManager_ddlCountry').val();

        if (hvCity == "true") {
            closeCityMappingModal();
            $('#MainContent_citySearchManager_ddlCountry').val("false");
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

                    <%--Original / Previous Div design--%>
                    <%--<div class="row">
                        <div class="form-inline">
                            <div class="col-sm-12">
                            </div>
                        </div>
                        <asp:LinkButton ID="btnGetCities" runat="server" CommandName="Search" Text="Search" CssClass="btn btn-primary btn-sm" ValidationGroup="vldgrpHealthAndSafety" OnClick="btnGetCities_Click" />
                    </div>--%>

                    <%--New Div design--%>
                    <div class="panel-body">
                        <div class="col-lg-4">
                            <div class="form-group row ">
                                <div class="col-md-4">
                                    <label for="ddlCountry">
                                        Country
                            <asp:RequiredFieldValidator ValidationGroup="CityManager" runat="server" ControlToValidate="ddlCountry"
                                Text="*" CssClass="text-danger" InitialValue="0" ErrorMessage="Please select a country before search !" />
                                    </label>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" CssClass="form-control" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group row ">
                                <div class="col-md-4">
                                    <Label runat="server" AssociatedControlID="ddlRank" CssClass="col-md-4 control-label">Rank
                                    </Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlRank" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-4">
                            <div class="form-group row ">
                                <div class="col-md-4">
                                    <label for="txtCityName">CityName</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtCityName" runat="server" CssClass="form-control" />
                                </div>
                            </div>

                            <div class="form-group row ">
                                <div class="col-md-4">
                                    <label runat="server" associatedcontrolidl="ddlPriority">Priority</label>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlPriority" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-4">
                            <div class="form-group row ">
                                <div class="col-md-4">
                                    <label runat="server" associatedcontrolid="ddlKey">
                                        Key
                                    </label>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlKey" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group row ">
                                <div class="col-md-4">
                                    <asp:LinkButton ID="btnGetCities" runat="server" CommandName="Search" Text="Search" CssClass="btn btn-primary btn-sm" ValidationGroup="CityManager" OnClick="btnGetCities_Click" />
                                </div>
                                <div class="col-md-8">
                                    <asp:LinkButton ID="btnNewCity" runat="server" CommandName="Create" Text="Create City" CssClass="btn btn-primary btn-sm pull-right" OnClick="btnNewCity_Click" OnClientClick="showCityMappingModal();" Visible="false" />
                                </div>
                            </div>
                        </div>
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
                                                <asp:BoundField DataField="Code" HeaderText="Code" />
                                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                                <asp:BoundField DataField="StateCode" HeaderText="State Code" />
                                                <asp:BoundField DataField="StateName" HeaderText="State Name" />
                                                <asp:BoundField DataField="CountryName" HeaderText="CountryName" />
                                                <asp:BoundField DataField="Key" HeaderText="Key" />
                                                <asp:BoundField DataField="Rank" HeaderText="Rank" />
                                                <asp:BoundField DataField="Priority" HeaderText="Priority" />
                                                <asp:BoundField DataField="Edit_Date" HeaderText="Edit_Date" />
                                                <asp:BoundField DataField="Edit_User" HeaderText="Edit_User" />
                                                <asp:BoundField HeaderText="# Hotels" DataField="TotalHotelRecords" />
                                                <asp:BoundField HeaderText="# Attractions" DataField="TotalAttractionsRecords" />
                                                <asp:BoundField HeaderText="# Supplier City" DataField="TotalSupplierCityRecords" />
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

                                                <div class="form-group row">
                                                    <label for="ddlStateName" class="col-md-4 col-form-label">
                                                        Key
                                                    </label>
                                                    <asp:RequiredFieldValidator ValidationGroup="City" runat="server" ControlToValidate="txtKey" Text="*"
                                                        CssClass="text-danger" ErrorMessage="The Key field is required." />
                                                    </label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtKey" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label for="ddlStateName" class="col-md-4 col-form-label">
                                                        Rank
                                                    </label>
                                                    <asp:RequiredFieldValidator ValidationGroup="City" runat="server" ControlToValidate="txtRank" Text="*"
                                                        CssClass="text-danger" ErrorMessage="The Rank field is required." />
                                                    </label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtRank" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label for="ddlStateName" class="col-md-4 col-form-label">
                                                        Priority
                                                    </label>
                                                    <asp:RequiredFieldValidator ValidationGroup="City" runat="server" ControlToValidate="txtPriority" Text="*"
                                                        CssClass="text-danger" ErrorMessage="The Priority field is required." />
                                                    </label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtPriority" runat="server" CssClass="form-control" />
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




