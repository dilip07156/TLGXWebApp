<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchAttributeMapping.ascx.cs" Inherits="TLGX_Consumer.controls.staticdata.searchAttributeMapping" %>

<script type="text/javascript">
    function CloseAddUpdateModal() {
        $("#moAddUpdate").modal('hide');
    }
    function showAddUpdateModal() {
        $("#moAddUpdate").modal('show');
    }

</script>

<asp:UpdatePanel ID="updAttributeMappingSearch" runat="server">
    <ContentTemplate>

        <div class="container">
            <div class="form-group">
                <div class="col-sm-12">
                    <asp:ValidationSummary ID="vldAttrMapSearch" runat="server" ValidationGroup="vldgrpAttrMapSearch" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                </div>
            </div>
        </div>

        <div class="container">

            <div class="panel-group" id="accordion">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Filters</a>
                        </h4>
                    </div>
                    <div id="collapseSearch" class="panel-collapse collapse in">
                        <div class="panel-body">
                            <form class="form-horizontal">
                                <div class="row">
                                    <div class="col-lg-6">

                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlSearchSupplier">
                                                Supplier Name
                                                <asp:RequiredFieldValidator ID="vldReqddlSupplier" runat="server" ControlToValidate="ddlSearchSupplier" InitialValue="0" ErrorMessage="Please select a Supplier" Text="*" ValidationGroup="vldgrpAttrMapSearch" CssClass="text-danger">
                                                </asp:RequiredFieldValidator>
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlSearchSupplier" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlSearchAttributeType">
                                                System Attribute
                                                <asp:RequiredFieldValidator ID="vldReqddlAttributeType" runat="server" ControlToValidate="ddlSearchAttributeType" InitialValue="0" ErrorMessage="Please select a Attribute Type" Text="*" ValidationGroup="vldgrpAttrMapSearch" CssClass="text-danger">
                                                </asp:RequiredFieldValidator>
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlSearchAttributeType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-10">
                                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" CausesValidation="true" ValidationGroup="vldgrpAttrMapSearch" />
                                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" OnClick="btnReset_Click" />
                                                <asp:Button ID="btnAddNew" runat="server" CssClass="btn btn-primary btn-sm" Text="Add New" OnClick="btnAddNew_Click" OnClientClick="showAddUpdateModal();" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">

                <div class="col-md-6">
                    <h4>Search Result</h4>
                </div>
                <div class="col-md-6">
                    <div class="form-group pull-right">
                        <div class="input-group" runat="server" id="divDropdownForEntries">
                            <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                            <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged">
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
                </div>
            </div>

            <div class="panel panel-default">
                <div class="panel-body">

                    <div class="row">

                        <div class="col-md-12">
                            <div class="form-group">
                                <div id="msgAlertHdr" runat="server" style="display: none;"></div>
                            </div>
                        </div>

                        <%-- <div class="col-md-2">
                            
                        </div>--%>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="grdSearchResults" runat="server" AllowPaging="True" AllowCustomPaging="true" AutoGenerateColumns="False" DataKeyNames="MasterAttributeMapping_Id"
                                CssClass="table table-responsive table-hover table-striped table-bordered" OnPageIndexChanging="grdSearchResults_PageIndexChanging"
                                OnRowCommand="grdSearchResults_RowCommand" PagerStyle-CssClass="Page navigation" EmptyDataText="No Mapping Defined." OnRowDataBound="grdSearchResults_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="Supplier_Name" HeaderText="Supplier" SortExpression="Supplier_Name" />
                                    <asp:BoundField DataField="Supplier_Attribute_Type" HeaderText="Attribute Type" SortExpression="Supplier_Attribute_Type" />
                                    <asp:BoundField DataField="System_Attribute_Type" HeaderText="System Attribute" SortExpression="System_Attribute_Type" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />

                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                                Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("MasterAttributeMapping_Id") %>' OnClientClick="showAddUpdateModal();">
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>' CssClass="btn btn-default" CommandArgument='<%# Bind("MasterAttributeMapping_Id") %>'>
                                         <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                                        <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <PagerStyle CssClass="pagination" BorderStyle="None" />
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            </div>


        </div>

    </ContentTemplate>
</asp:UpdatePanel>

<div class="modal fade" id="moAddUpdate" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">Supplier Attributes Mapping Details
                </h4>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel ID="AddUpdAttributeMappingModal" runat="server">
                    <ContentTemplate>
                        <div class="row col-md-12">
                            <asp:ValidationSummary ID="vlsAttrMapSumm" runat="server" ValidationGroup="grpAttrMap" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                        </div>

                        <div class="row col-md-12">
                            <div id="msgAlert" runat="server" style="display: none;"></div>
                        </div>
                        <asp:FormView ID="frmAttributeMapping" DataKeyNames="MasterAttributeMapping_Id" runat="server" DefaultMode="Insert" OnItemCommand="frmAttributeMapping_ItemCommand">
                            <EditItemTemplate>

                                <div class="row col-md-12">

                                    <div class="panel panel-default">
                                        <div class="panel-heading">Update Supplier Attributes Mapping</div>
                                        <div class="panel-body">

                                            <div class="form-group row">
                                                <label class="col-md-4 col-form-label" for="ddlSuppliers">Supplier Name</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlSuppliers" runat="server" CssClass="form-control" AppendDataBoundItems="true" Enabled="false">
                                                        <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-md-4 col-form-label" for="txtSupplierAttributeName">
                                                    Supplier Attribute Type
                                                        <asp:RequiredFieldValidator ID="vldReqddlSupplier" runat="server" ControlToValidate="txtSupplierAttributeName"
                                                            InitialValue="0" ErrorMessage="Please enter Supplier Attribute Type" Text="*" ValidationGroup="grpAttrMap" CssClass="text-danger">
                                                        </asp:RequiredFieldValidator>
                                                </label>
                                                <div class="col-md-8">
                                                    <asp:TextBox runat="server" ID="txtSupplierAttributeName" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-md-4 col-form-label" for="ddlAttributeType">System Attribute</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlAttributeType" runat="server" CssClass="form-control" AppendDataBoundItems="true" Enabled="false">
                                                        <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-md-4 col-form-label" for="ddlStatus">Status</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-8">
                                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm" Text="Update" CommandName="Modify" CausesValidation="true" ValidationGroup="grpAttrMap" />
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-sm" Text="Cancel" CommandName="Cancel" OnClientClick="CloseAddUpdateModal();" />
                                                </div>
                                            </div>

                                        </div>

                                    </div>

                                </div>



                                <div class="row col-md-12">

                                    <div class="panel panel-default">
                                        <div class="panel-heading">Supplier Attributes Values Mapping</div>
                                        <div class="panel-body">

                                            <asp:GridView ID="grdMappingAttrVal" runat="server" AutoGenerateColumns="False"
                                                DataKeyNames="MasterAttributeValueMapping_Id" CssClass="table table-responsive table-hover table-striped table-bordered"
                                                OnRowCommand="grdMappingAttrVal_RowCommand" EmptyDataText="No Mapping Defined.">
                                                <Columns>
                                                    <asp:BoundField DataField="SystemMasterAttributeValue" HeaderText="System Value" SortExpression="SystemMasterAttributeValue" />
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Supplier Value
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtSupplierAttributeValue" runat="server" CssClass="form-control"
                                                                Text='<%# Bind("SupplierMasterAttributeValue") %>'>
                                                            </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="vldReqddlSupplierVal" runat="server" ControlToValidate="txtSupplierAttributeValue"
                                                                ErrorMessage="* Required" ValidationGroup='<%# "Group_" + Container.DataItemIndex %>' CssClass="text-danger"> 
                                                            </asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Active
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkAttrValIsActive" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsActive")) %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ShowHeader="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="true" CommandName="EditVal" CssClass="btn btn-default"
                                                                CommandArgument='<%# Bind("MasterAttributeValueMapping_Id") %>' ValidationGroup='<%# "Group_" + Container.DataItemIndex %>'>
                                                                            <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Update
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField Visible="false">
                                                        <HeaderTemplate>
                                                            SystemMasterAttributeValue_Id
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSystemMasterAttributeValueId" runat="server" Text='<%# Bind("SystemMasterAttributeValue_Id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>

                                            </asp:GridView>

                                        </div>

                                    </div>

                                </div>

                            </EditItemTemplate>

                            <InsertItemTemplate>


                                <div class="row col-md-12">

                                    <div class="panel panel-default">
                                        <div class="panel-heading">Add Supplier Attributes Mapping</div>

                                        <div class="panel-body">

                                            <div class="form-group row">
                                                <label class="col-md-4 col-form-label" for="ddlSuppliers">
                                                    Supplier Name
                                                        <asp:RequiredFieldValidator ID="vldReqddlSupplier" runat="server" ControlToValidate="ddlSuppliers"
                                                            InitialValue="0" ErrorMessage="Please select a Supplier" Text="*" ValidationGroup="grpAttrMap" CssClass="text-danger">
                                                        </asp:RequiredFieldValidator>
                                                </label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlSuppliers" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-md-4 col-form-label" for="txtSupplierAttributeName">
                                                    Supplier Attribute Type
                                                        <asp:RequiredFieldValidator ID="vldReqtxtSupplierAttributeName" runat="server" ControlToValidate="txtSupplierAttributeName"
                                                            ErrorMessage="Please enter Supplier Attribute Type" Text="*" ValidationGroup="grpAttrMap" CssClass="text-danger">
                                                        </asp:RequiredFieldValidator>
                                                </label>
                                                <div class="col-md-8">
                                                    <asp:TextBox runat="server" ID="txtSupplierAttributeName" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-md-4 col-form-label" for="ddlAttributeType">
                                                    System Attribute
                                                        <asp:RequiredFieldValidator ID="vldReqddlAttributeType" runat="server" ControlToValidate="ddlAttributeType"
                                                            InitialValue="0" ErrorMessage="Please select System Attribute Type" Text="*" ValidationGroup="grpAttrMap" CssClass="text-danger">
                                                        </asp:RequiredFieldValidator>
                                                </label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlAttributeType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-8">
                                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm" Text="Add" CommandName="Add" CausesValidation="true" ValidationGroup="grpAttrMap" />
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-sm" Text="Cancel" CommandName="Cancel" OnClientClick="CloseAddUpdateModal();" />
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
