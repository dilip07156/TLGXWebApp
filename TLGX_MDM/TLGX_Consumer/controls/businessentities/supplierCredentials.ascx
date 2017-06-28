<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="supplierCredentials.ascx.cs" Inherits="TLGX_Consumer.controls.businessentities.supplierCredentials" %>
<script type="text/javascript">

    function showAddUpdatesupplierCredentialsModal() {
        $("#moAddUpdatesupplierCredentials").modal('show');
    }
    function closeAddUpdatesupplierCredentialsModal() {
        $("#moAddUpdatesupplierCredentials").modal('close');
    }
    function pageLoad(sender, args) {
        var hv = $('#MainContent_UserManagement_hdnFlag').val();
        if (hv == "true") {
            closeAddUpdateUserModal();
            $('#MainContent_UserManagement_hdnFlag').val("false");
        }
    }
</script>
<asp:UpdatePanel runat="server" ID="supplierCredentials">
    <ContentTemplate>
        <asp:Button runat="server" ID="btnNewCreate" OnClientClick="showAddUpdatesupplierCredentialsModal();" CssClass="btn btn-sm btn-primary pull-right" Text="Create New Credintials" />
    </ContentTemplate>
</asp:UpdatePanel>
<div class="modal fade" id="moAddUpdatesupplierCredentials" role="dialog">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <div class="input-group">
                    <h4 class="input-group-addon"><strong>Add / Update Supplier Credentials </strong></h4>
                </div>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel ID="UpdSupplierCredentialsModal" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnFlag" runat="server" Value="" EnableViewState="false" />
                        <div class="container">
                            <div class="row">
                                <div class="form-group">
                                    <div class="col-sm-9">
                                        <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="supplierCredentials" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading">Supplier Credentials Information</div>
                                    <div class="panel-body">
                                        <asp:FormView ID="frmCountrydetail" CssClass="col-lg-12" runat="server" DefaultMode="Insert">
                                            <InsertItemTemplate>
                                                <div class="form-group row col-md-12">
                                                    <div class="col-md-6">
                                                        <div class="form-group row">
                                                            <label for="ddlSupplierTypeCredentials" class="col-md-4 col-form-label">
                                                                Supplier Type
                                                                 <asp:RequiredFieldValidator ValidationGroup="supplierCredentials" runat="server" ControlToValidate="ddlSupplierTypeCredentials"
                                                                     CssClass="text-danger" ErrorMessage="The OAG Port name is required." Text="*" />
                                                            </label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList runat="server" ID="ddlSupplierTypeCredentials" CssClass="form-control">
                                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <%-- <div class="form-group row">
                                                            <label for="txtCompanySupplierID" class="col-md-4 col-form-label">
                                                                Supplier ID
                                                            </label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtCompanySupplierID" Enabled="false" CssClass="form-control" />
                                                            </div>
                                                        </div>--%>
                                                        <%--<div class="form-group row">
                                                            <label for="txtSupplierCredentialsID" class="col-md-4 col-form-label">
                                                                Credentials ID
                                                            </label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtSupplierCredentialsID" Enabled="false" CssClass="form-control" />
                                                            </div>
                                                        </div>--%>
                                                        <div class="form-group row">
                                                            <label for="ddlCredentialsType" class="col-md-4 col-form-label">
                                                                Credentials Type
                                                                 <asp:RequiredFieldValidator ValidationGroup="supplierCredentials" runat="server" ControlToValidate="ddlCredentialsType"
                                                                     CssClass="text-danger" ErrorMessage="The OAG Port name is required." Text="*" />
                                                            </label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList runat="server" ID="ddlCredentialsType" CssClass="form-control">
                                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="ddlClientType" class="col-md-4 col-form-label">
                                                                Client Type
                                                                 <asp:RequiredFieldValidator ValidationGroup="supplierCredentials" runat="server" ControlToValidate="ddlClientType"
                                                                     CssClass="text-danger" ErrorMessage="The OAG Port name is required." Text="*" />
                                                            </label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList runat="server" ID="ddlClientType" CssClass="form-control">
                                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group row">
                                                            <label for="ddlSupplierEnablerCategory" class="col-md-4 col-form-label">
                                                                Supplier Enabler Category
                                                                 <asp:RequiredFieldValidator ValidationGroup="supplierCredentials" runat="server" ControlToValidate="ddlSupplierEnablerCategory"
                                                                     CssClass="text-danger" ErrorMessage="The Supplier Enabler Category is required." Text="*" />
                                                            </label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList runat="server" ID="ddlSupplierEnablerCategory" CssClass="form-control">
                                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtSupplierName" class="col-md-4 col-form-label">
                                                                Supplier Name
                                                            </label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtSupplierName" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="txtCredentialsName" class="col-md-4 col-form-label">
                                                                Credentials Name
                                                            </label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtCredentialsName" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label for="ddlCredentialsCategory" class="col-md-4 col-form-label">
                                                                Credentials Category
                                                                 <asp:RequiredFieldValidator ValidationGroup="supplierCredentials" runat="server" ControlToValidate="ddlCredentialsCategory"
                                                                     CssClass="text-danger" ErrorMessage="The Credentials Category is required." Text="*" />
                                                            </label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList runat="server" ID="ddlCredentialsCategory" CssClass="form-control">
                                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row col-md-12">
                                                            <label>
                                                                <input type="checkbox" runat="server" id="chckbIsPublishedFareCredentials">
                                                                Is Published Fare Credentials</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group row col-md-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading"><strong>Ownership</strong></div>
                                                        <div class="panel-body">
                                                            <asp:RadioButtonList ID="rdbFrequency" ClientIDMode="Static" CssClass="radioButtonList" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                                                <asp:ListItem>Company</asp:ListItem>
                                                                <asp:ListItem>Supplier</asp:ListItem>
                                                                <asp:ListItem>Client</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <div class="clear row">&nbsp;</div>
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading"><strong>Supplier</strong></div>
                                                                <div class="panel-body">
                                                                    <div class="form-group row col-md-12">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group row">
                                                                                <label for="ddlSupplierTypeCredentials" class="col-md-4 col-form-label">
                                                                                    Supplier Type
                                                                 <asp:RequiredFieldValidator ValidationGroup="supplierCredentials" runat="server" ControlToValidate="ddlSupplierTypeCredentials"
                                                                     CssClass="text-danger" ErrorMessage="The OAG Port name is required." Text="*" />
                                                                                </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:DropDownList runat="server" ID="DropDownList1" CssClass="form-control">
                                                                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group row">
                                                                                <label for="txtSupplierName" class="col-md-4 col-form-label">
                                                                                    Supplier Name
                                                                                </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox runat="server" ID="TextBox1" CssClass="form-control" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="panel panel-default">
                                                                <div class="panel-heading"><strong>Client</strong></div>
                                                                <div class="panel-body">
                                                                    <div class="form-group row col-md-12">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group row">
                                                                                <label for="ddlSupplierTypeCredentials" class="col-md-4 col-form-label">
                                                                                    Client Type
                                                                 <asp:RequiredFieldValidator ValidationGroup="supplierCredentials" runat="server" ControlToValidate="ddlSupplierTypeCredentials"
                                                                     CssClass="text-danger" ErrorMessage="The OAG Port name is required." Text="*" />
                                                                                </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:DropDownList runat="server" ID="DropDownList2" CssClass="form-control">
                                                                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group row">
                                                                                <label for="txtSupplierName" class="col-md-4 col-form-label">
                                                                                    Client Name
                                                                                </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox runat="server" ID="TextBox2" CssClass="form-control" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading"><strong>Product</strong></div>
                                                        <div class="panel-body">
                                                            <div class="form-group row col-md-12">
                                                                <div class="col-md-5">
                                                                    <div class="form-group row">
                                                                        <label for="ddlProductCategory" class="col-md-4 col-form-label">
                                                                            Product Category
                                                               <%--  <asp:RequiredFieldValidator ValidationGroup="supplierCredentials" runat="server" ControlToValidate="ddlProductCategory"
                                                                     CssClass="text-danger" ErrorMessage="The OAG Port name is required." Text="*" />--%>
                                                                        </label>
                                                                        <div class="col-md-8">
                                                                            <asp:DropDownList runat="server" ID="ddlProductCategory" CssClass="form-control">
                                                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-5">
                                                                    <div class="form-group row">
                                                                        <label for="ddpProductCategorySubType" class="col-md-4 col-form-label">
                                                                            Product Category Sub Type
                                                                        </label>
                                                                        <div class="col-md-8">
                                                                            <asp:DropDownList runat="server" ID="ddpProductCategorySubType" CssClass="form-control">
                                                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:Button ID="btnSaveProducts" runat="server" Text="Add / Save" CssClass="btn btn-primary btn-md" />
                                                                </div>
                                                            </div>
                                                            <div>
                                                                <asp:GridView ID="grdSupplierCredentialProductDetails" runat="server" AllowCustomPaging="true" AllowPaging="true" AutoGenerateColumns="False"
                                                                    CssClass="table table-hover table-striped table-bordered table-fixed" EmptyDataText="No Data Found">
                                                                    <Columns>
                                                                        <%-- <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                                                        <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
                                                                        <asp:BoundField HeaderText="SupplierType" SortExpression="Code" />
                                                                        <asp:BoundField DataField="Create_User" HeaderText="Create_User" SortExpression="Create_User" />
                                                                        <asp:HyperLinkField DataNavigateUrlFields="Supplier_Id" DataNavigateUrlFormatString="~/suppliers/Manage?Supplier_Id={0}" Text="Manage" NavigateUrl="~/suppliers/Manage.aspx" />--%>
                                                                    </Columns>
                                                                    <%--<PagerStyle CssClass="pagination-ys" />--%>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row col-md-2">
                                                    <asp:Button ID="btnAddSupplierCredentials" CommandName="Add" runat="server" Text="Save" CssClass="btn btn-primary btn-md" ValidationGroup="Country" />
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
