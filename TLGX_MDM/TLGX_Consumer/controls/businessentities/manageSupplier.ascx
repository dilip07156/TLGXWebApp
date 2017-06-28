<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="manageSupplier.ascx.cs" Inherits="TLGX_Consumer.controls.businessentities.manageSupplier" %>
<%@ Register Src="~/controls/geography/supplierCountryMapping.ascx" TagPrefix="uc1" TagName="supplierCountryMapping" %>
<%@ Register Src="~/controls/geography/supplierCityMapping.ascx" TagPrefix="uc1" TagName="supplierCityMapping" %>



<%@ Register Src="../integrations/supplierIntegrations.ascx" TagName="supplierIntegrations" TagPrefix="uc2" %>
<%@ Register Src="~/controls/businessentities/supplierMarket.ascx" TagPrefix="uc1" TagName="supplierMarket" %>

<%@ Register Src="~/controls/businessentities/supplierProductCategory.ascx" TagPrefix="uc1" TagName="supplierProductCategory" %>
<%@ Register Src="~/controls/businessentities/supplierStaticDataHandling.ascx" TagPrefix="uc1" TagName="supplierStaticDataHandling" %>
<%@ Register Src="~/controls/businessentities/supplierCredentials.ascx" TagPrefix="uc1" TagName="supplierCredentials" %>
<%--for charts--%>
<%@ Register Src="~/controls/staticdata/supplierWiseDataChart.ascx" TagPrefix="uc1" TagName="supplierWiseDataChart" %>


<script type="text/javascript">
    function pageLoad(sender, args) {
        var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "SupplierMarkets";
        $('#Tabs a[href="#' + tabName + '"]').tab('show');
        $("#Tabs a").click(function () {
            $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
        });
    };

</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div id="dvMsgUpdateSupplierDetails" runat="server" style="display: none;"></div>
                <asp:HiddenField ID="hdnFlag" ClientIDMode="Static" runat="server" Value="" EnableViewState="false" />
                <asp:FormView ID="frmSupplierDetail" runat="server" DataKeyNames="Supplier_Id" DefaultMode="Edit" OnItemCommand="frmSupplierDetail_ItemCommand">
                    <EditItemTemplate>
                        <h1 class="page-header">Manage Supplier:
                    <asp:Label ID="lblSupplierName" runat="server" Text='<%# Bind("Name") %>' /></h1>
                        <div class="container">
                            <div class="form-group row">
                                <div class="col-sm-9">
                                    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="SupplierEdit" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                </div>
                                <div class="col-lg-8">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label for="txtName">
                                                    Name
                                              <asp:RequiredFieldValidator ValidationGroup="SupplierEdit" runat="server" ControlToValidate="txtNameSupplierEdit"
                                                  CssClass="text-danger" ErrorMessage="The supplier name field is required." Text="*" />
                                                </label>
                                                <asp:TextBox ID="txtNameSupplierEdit" CssClass="form-control" runat="server" Text='<%# Bind("Name") %>' />
                                            </div>
                                            <div class="form-group">
                                                <label for="txtCode">Code</label>
                                                <asp:TextBox ID="txtCodeSupplierEdit" CssClass="form-control" runat="server" Enabled="false" Text='<%# Bind("Code") %>' />
                                            </div>

                                            <div class="form-group">
                                                <div class="col-sm-10">
                                                    <asp:Button ID="btnUpdateSupplier" CommandName="EditCommand" CausesValidation="true" ValidationGroup="SupplierEdit" runat="server" CssClass="btn btn-primary btn-sm" Text="Update Supplier" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label for="ddlSupplierType">
                                                    Supplier Type
                                                </label>
                                                <asp:DropDownList runat="server" ID="ddlSupplierType" CssClass="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label for="ddlSupplierType">Status</label>
                                                <asp:DropDownList runat="server" ID="ddlStatusEdit" CssClass="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">

                                    <div class="panel panel-default">
                                        <div class="panel-heading">Audit Trail</div>
                                        <div class="panel-body">

                                            <div class="form-group">
                                                <label for="txtCreateDate" class="control-label col-sm-4">Create Date</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtCreateDate" CssClass="form-control" runat="server" Enabled="false" Text='<%# Bind("Create_Date") %>' />
                                                </div>
                                            </div>
                                            <br />
                                            <br />
                                            <div class="form-group">
                                                <label for="txtCreateUser" class="control-label col-sm-4">Create By</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtCreatedBy" CssClass="form-control" runat="server" Enabled="false" Text='<%# Bind("Create_User") %>' />
                                                </div>
                                            </div>
                                            <br />
                                            <br />
                                            <div class="form-group">
                                                <label for="txtEditDate" class="control-label col-sm-4">Edit Date</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtEditDate" CssClass="form-control" runat="server" Enabled="false" Text='<%# Bind("Edit_Date") %>' />
                                                </div>
                                            </div>
                                            <br />
                                            <br />
                                            <div class="form-group">
                                                <label for="txtEditUSer" class="control-label col-sm-4">Edit User</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtEditUSer" CssClass="form-control" runat="server" Enabled="false" Text='<%# Bind("Edit_User") %>' />
                                                </div>
                                            </div>

                                        </div>

                                    </div>

                                </div>
                            </div>
                    </EditItemTemplate>
                </asp:FormView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <asp:HiddenField ID="TabName" runat="server" />
        <div class="panel panel-default">
            <div class="panel-body">
                <ul class="nav nav-tabs">
                    <li class="active"><a data-toggle="tab" aria-controls="SupplierMarkets" href="#ShowSupplierMarkets">Supplier Markets</a></li>
                    <li><a data-toggle="tab" aria-controls="ProductMapping" href="#ShowSupplierProductMapping">Product Categories</a></li>
                    <li><a data-toggle="tab" aria-controls="SupplierStaticData" href="#ShowSupplierStaticData">Static Data Handling</a></li>
                    <li><a data-toggle="tab" aria-controls="SupplierCredentials" href="#ShowSupplierCredentials">Supplier Credentials</a></li>
                     <li><a data-toggle="tab" aria-controls="SupplierStatusChart" href="#ShowSupplierStatusChart" id="ShowSupplier">Supplier Status Charts</a></li>
                </ul>
                <div class="tab-content">
                    <div id="ShowSupplierMarkets" class="tab-pane fade in active">
                        <br />
                        <uc1:supplierMarket runat="server" ID="supplierMarket" />
                    </div>
                    <div id="ShowSupplierProductMapping" class="tab-pane fade in">
                        <br />
                        <uc1:supplierProductCategory runat="server" ID="supplierProductCategory" />
                    </div>
                    <div id="ShowSupplierStaticData" class="tab-pane fade in">
                        <br />
                        <uc1:supplierStaticDataHandling runat="server" ID="supplierStaticDataHandling" />
                    </div>
                    <div id="ShowSupplierCredentials" class="tab-pane fade in">
                        <br />
                        <uc1:supplierCredentials runat="server" ID="suppliersCredentials" />
                    </div>
                    <%--for charts--%>
                    <div id="ShowSupplierStatusChart" class="tab-pane fade in">
                        <br />
                        <uc1:supplierWiseDataChart runat="server" ID="supplierWiseDataChart" />
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>




