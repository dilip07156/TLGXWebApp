<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchActivityMasterControl.ascx.cs" Inherits="TLGX_Consumer.controls.activity.SearchActivityMasterControl" %>

<script type="text/javascript">
    function showAddNewActivityModal() {
        $("#moAddNewActivityModal").modal('show');
    }
    function closeAddNewActivityModal() {
        $("#moAddNewActivityModal").modal('hide');
    }
    //function page_load(sender, args) {
    //    closeAddNewActivityModal();
    //}
</script>
<asp:UpdatePanel ID="updSearchDDLChange" runat="server">
    <ContentTemplate>
        <div class="row">
            <div class="col-sm-12">
                <div class="panel-group" id="accordion">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Activities</a>
                            </h4>
                        </div>

                        <div id="collapseSearch" class="panel-collapse collapse in">

                            <div class="panel-body">

                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <br />
                                        <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="HotelSearch" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                    </div>
                                </div>

                                <div class="form-group col-sm-6">

                                    <div class="form-group col-sm-12">
                                        <label class="control-label col-sm-6" for="ddlCountry">Country</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                <asp:ListItem Text="-ALL-" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="-ALL UNMAPPED-" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group col-sm-12">
                                        <label class="control-label col-sm-6" for="ddlCity">City</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="-ALL-" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="-ALL UNMAPPED-" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group col-sm-12">
                                        <label class="control-label col-sm-6" for="ddlSupplier">
                                            Supplier
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="-ALL-" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <%-- <div class="form-group col-sm-12">
                                        <label class="control-label col-sm-6" for="ddlStatus">
                                            Status
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="-ALL-" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>--%>

                                    <div class="form-group col-sm-12">
                                        <label class="control-label col-sm-6" for="txtProductName">Product Name</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group col-sm-12">
                                        <label class="control-label col-sm-6" for="chkNoSuitableFor">Suitable For is not defined</label>
                                        <div class="col-sm-6">
                                            <asp:CheckBox ID="chkNoSuitableFor" runat="server" />
                                        </div>
                                    </div>

                                    <div class="form-group col-sm-12">
                                        <label class="control-label col-sm-6" for="chkNoPhysicalIntensity">Physical Intensity is not defined</label>
                                        <div class="col-sm-6">
                                            <asp:CheckBox ID="chkNoPhysicalIntensity" runat="server" />
                                        </div>
                                    </div>

                                    <div class="form-group col-sm-12">
                                        <label class="control-label col-sm-6" for="chkNoOperatingSchedule">Operating Schedule is not defined</label>
                                        <div class="col-sm-6">
                                            <asp:CheckBox ID="chkNoOperatingSchedule" runat="server" />
                                        </div>
                                    </div>

                                    <div class="form-group col-sm-12">
                                        <label class="control-label col-sm-6" for="chkNoSession">Session is not defined</label>
                                        <div class="col-sm-6">
                                            <asp:CheckBox ID="chkNoSession" runat="server" />
                                        </div>
                                    </div>

                                </div>

                                <div class="form-group col-sm-6">

                                    <div class="form-group col-sm-12 row">
                                        <label class="control-label col-sm-6" for="ddlProductCategorySubType">
                                            Category SubType
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlProductCategorySubType" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlProductCategorySubType_SelectedIndexChanged">
                                                <asp:ListItem Text="-ALL-" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="-ALL UNMAPPED-" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group col-sm-12 row">
                                        <label class="control-label col-sm-6" for="ddlProductType">
                                            Product Type
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged">
                                                <asp:ListItem Text="-ALL-" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="-ALL UNMAPPED-" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group col-sm-12 row">
                                        <label class="control-label col-sm-6" for="ddlProductSubType">
                                            Product Sub Type
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlProductSubType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="-ALL-" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="-ALL UNMAPPED-" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>


                                    <div class="form-group col-sm-12 row">
                                        <div class="form-group col-sm-6">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" />
                                            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" CausesValidation="false" OnClick="btnReset_Click" />
                                        </div>
                                        <div class="form-group col-sm-6">
                                            <asp:LinkButton ID="btnNewActivity" runat="server" Text="Add New" CssClass="btn btn-primary btn-sm pull-right" OnClientClick="showAddNewActivityModal();" />
                                        </div>
                                    </div>
                                </div>

                            </div>



                        </div>
                    </div>
                </div>
            </div>

            <div id="dvPageSize" runat="server" class="pull-right">
                <div class="col-sm-12">
                    <div class="form-group">
                        <div class="input-group">
                            <label class="input-group-addon" for="ddlPageSize"><strong>Page Size</strong></label>
                            <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control" AutoPostBack="true" Width="100px" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                <asp:ListItem Text="25" Value="25" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                <asp:ListItem Text="100" Value="100"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel-group" id="searchResult">
            <div class="panel panel-default">

                <div class="panel-heading clearfix">
                    <h4 class="panel-title pull-left">
                        <a data-toggle="collapse" data-parent="#searchResult" href="#collapseSearchResult">Search Results (Total Count:
                            <asp:Label ID="lblTotalRecords" runat="server"></asp:Label>)</a>
                    </h4>
                </div>

                <div id="collapseSearchResult" class="panel-collapse collapse in">
                    <div class="panel-body">

                        <asp:GridView ID="gvActivitySearch" runat="server" AllowPaging="True" AllowCustomPaging="true"
                            EmptyDataText="No data for search conditions" CssClass="table table-hover table-striped"
                            AutoGenerateColumns="false" DataKeyNames="Activity_Flavour_Id" OnPageIndexChanging="gvActivitySearch_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="CommonProductNameSubType_Id" HeaderText="Common Product ID" />
                                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                <asp:BoundField DataField="ProductCategorySubType" HeaderText="Product Category Sub Type" />
                                <asp:BoundField DataField="ProductType" HeaderText="Product Type" />
                                <asp:BoundField DataField="ProductNameSubType" HeaderText="Product Name Sub Type" />
                                <asp:BoundField DataField="Country" HeaderText="Country" />
                                <asp:BoundField DataField="City" HeaderText="City" />
                                <asp:BoundField DataField="SupplierCode" HeaderText="Supplier" />
                                <asp:HyperLinkField DataNavigateUrlFields="Activity_Flavour_Id" DataNavigateUrlFormatString="~/activity/ManageActivityFlavour.aspx?Activity_Flavour_Id={0}" Text="Select" ControlStyle-Font-Bold="true" NavigateUrl="~/activity/ManageActivityFlavour.aspx" ControlStyle-CssClass="btn btn-default btn-sm" />
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                    </div>
                </div>

            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>

<div class="modal fade" id="moAddNewActivityModal" role="dialog">
    <div class="modal-dialog modal-md">
        <div class="modal-content">

            <div class="modal-header">
                <div class="input-group">
                    <h4>Add New Activity</h4>
                </div>
            </div>

            <div class="modal-body">
                <asp:UpdatePanel ID="updNewActivity" runat="server">
                    <ContentTemplate>

                        <div class="container">
                            <div class="row">
                                <div class="form-group">
                                    <div class="col-sm-6">
                                        <asp:ValidationSummary ID="vldSumActivity" runat="server" ValidationGroup="NewActivity" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                        <div id="divMsgAlertActivity" runat="server" style="display: none"></div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="container">
                            <div class="row col-lg-6">
                                <div class="panel-body">
                                    <div class="form-group row">
                                        <label for="txtProductNameAdd" class="col-md-6 col-form-label">Product Name</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtProductNameAdd" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="frmddlCategorySubType" class="col-md-6">
                                            Category Sub-Type
                                                        <asp:RequiredFieldValidator runat="server" InitialValue="0" ValidationGroup="NewActivity" Text="*" CssClass="text-danger" ControlToValidate="frmddlCategorySubType" ErrorMessage="Please select Category Sub Type."></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="frmddlCategorySubType" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="frmddlProductType" class="col-md-6">
                                            Product Type
                                                        <asp:RequiredFieldValidator runat="server" InitialValue="0" ValidationGroup="NewActivity" Text="*" CssClass="text-danger" ControlToValidate="frmddlProductType" ErrorMessage="Please select Product Type."></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="frmddlProductType" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="frmddlCountry" class="col-md-6">
                                            Country
                                                        <asp:RequiredFieldValidator runat="server" InitialValue="0" ValidationGroup="NewActivity" Text="*" CssClass="text-danger" ControlToValidate="frmddlCountry" ErrorMessage="Please select Country."></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="frmddlCountry" runat="server" CssClass="form-control" AutoPostBack="true">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label for="frmddlCity" class="col-md-6">
                                            City
                                                        <asp:RequiredFieldValidator runat="server" InitialValue="0" ValidationGroup="NewActivity" Text="*" CssClass="text-danger" ControlToValidate="frmddlCity" ErrorMessage="Please select City."></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="frmddlCity" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row pull-right">
                                        <div class="col-md-12">
                                            <asp:LinkButton ID="btnSaveActivity" CommandName="AddActivity" runat="server" Text="Save" CssClass="btn btn-primary btn-md" ValidationGroup="NewActivity" CausesValidation="True" />
                                            <asp:LinkButton ID="btnAddReset" CommandName="ResetActivity" runat="server" Text="Reset" CssClass="btn btn-primary btn-md" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div id="dvGrid" runat="server" class="control-label col-sm-12">
                                <asp:GridView ID="grdSearchResults" runat="server" AllowPaging="false" AllowCustomPaging="false" AutoGenerateColumns="False" DataKeyNames="Activity_Id" CssClass="table table-hover table-striped">
                                    <Columns>
                                        <asp:BoundField DataField="CommonProductID" HeaderText="Product ID" />
                                        <asp:BoundField DataField="Product_Name" HeaderText="Product_Name" />
                                        <asp:BoundField DataField="ProductCategorySubType" HeaderText="Category Sub Type" />
                                        <asp:BoundField DataField="ProductType" HeaderText="Product Type" />
                                        <asp:BoundField DataField="Country" HeaderText="Country" />
                                        <asp:BoundField DataField="City" HeaderText="City" />
                                        <asp:HyperLinkField DataNavigateUrlFields="Activity_Id" DataNavigateUrlFormatString="~/activity/ManageActivityFlavour.aspx?Activity_Flavour_Id={0}" Text="Select" ControlStyle-Font-Bold="true" NavigateUrl="~/activity/ManageActivityFlavour.aspx" />
                                    </Columns>
                                </asp:GridView>
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

