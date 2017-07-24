<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchSupplier.ascx.cs" Inherits="TLGX_Consumer.controls.businessentities.SearchSupplier" %>

<script type="text/javascript">

    function showCreateSupplierModal() { $("#moCreateSupplier").modal('show'); }
    function closeCreateSupplierModal() { $("#moCreateSupplier").modal('hide'); }

    function pageLoad(sender, args) {
        var hv = $('#hdnFlag').val();
        if (hv == "true") {
            closeCreateSupplierModal();
            $('#hdnFlag').val("false");
        }
    }
</script>
<asp:UpdatePanel runat="server" ID="updPnlSupplierLust">
    <ContentTemplate>
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
                                            <label class="control-label col-sm-4" for="txtSupplierCode">Supplier Code</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtSupplierCode" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="txtSupplierName">Supplier Name</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtSupplierName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlSupplierType">Type</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlSupplierType" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-10">
                                                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" />
                                                <asp:Button ID="btnReset" OnClick="btnReset_Click" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlProductCategory">Product Category</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlProductCategory" runat="server" OnSelectedIndexChanged="ddlProductCategory_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true">
                                                    <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlProductCategorySubType">Category Sub Type</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlProductCategorySubType" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlStatus">Status</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack="true">
                                                    <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-10">
                                                <%-- <asp:UpdatePanel runat="server" ID="updpSupplierCreate">
                                                    <ContentTemplate>--%>
                                                <asp:Button runat="server" ID="btnNewCreate" OnClientClick="showCreateSupplierModal();" OnClick="btnNewCreate_Click" CssClass="btn btn-sm btn-primary pull-right" Text="Create New Supplier" />
                                                <%-- </ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="form-group pull-right">
                        <div class="input-group" runat="server" id="divDropdownForEntries">
                            <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                            <asp:DropDownList ID="ddlShowEntries" runat="server" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
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
        </div>
        <div class="container">
            <div class="panel-group" id="accordionSupplierSearch">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title"><a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch1">Search Results (Total Records: 
                            <asp:Label ID="lblTotalRecords" runat="server" Text="{0}"></asp:Label>)
                        </a></h4>
                    </div>
                    <div id="collapseSearch1" class="panel-collapse collapse in">
                        <div class="panel-body">
                            <div class="col-md-12">
                                <div id="dvMsg" runat="server" style="display: none;"></div>
                            </div>

                            <asp:GridView ID="grdSupplierList" runat="server" AllowCustomPaging="true" AllowPaging="true" AutoGenerateColumns="False" DataKeyNames="Supplier_Id"
                                CssClass="table table-hover table-striped table-bordered table-fixed" EmptyDataText="No Data Found" OnPageIndexChanging="grdSupplierList_PageIndexChanging" OnSelectedIndexChanging="grdSupplierList_SelectedIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                    <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
                                    <asp:BoundField DataField="SupplierType" HeaderText="SupplierType" SortExpression="Code" />
                                    <%--<asp:BoundField DataField="Create_User" HeaderText="Create_User" SortExpression="Create_User" />--%>
                                    <asp:BoundField DataField="Edit_User" HeaderText="Edit User" SortExpression="Edit_User" />
                                    <asp:BoundField DataField="Edit_Date" HeaderText="Edit Date" SortExpression="Edit_Date" />
                                    <asp:BoundField HeaderText="Status" DataField="StatusCode" />
                                    <asp:HyperLinkField DataNavigateUrlFields="Supplier_Id" DataNavigateUrlFormatString="~/suppliers/Manage?Supplier_Id={0}" Text="Manage" NavigateUrl="~/suppliers/Manage.aspx" ControlStyle-CssClass="btn btn-default"/>
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
<div class="modal fade" id="moCreateSupplier" role="dialog">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <div class="input-group">
                    <h4 class="input-group-addon">Add Supplier</h4>
                </div>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel ID="UpdUserAddModal" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnFlag" ClientIDMode="Static" runat="server" Value="" EnableViewState="false" />
                        <div class="container">
                            <div class="row">
                                <div class="form-group">
                                    <div class="col-sm-9">
                                        <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="Supplier" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                        <div id="msgAlert" runat="server" style="display: none;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading">Supplier Information</div>
                                    <div class="panel-body">
                                        <asp:FormView ID="frmSupplierdetail" CssClass="col-lg-12" runat="server" DefaultMode="Insert" OnItemCommand="frmSupplierdetail_ItemCommand">
                                            <InsertItemTemplate>
                                                <div class="col-lg-12">
                                                    <div class="col-lg-6">
                                                        <div class="form-group row">
                                                            <asp:Label runat="server" AssociatedControlID="txtSupplierNameCreate" CssClass="col-md-4 control-label">Name
                                                                            <asp:RequiredFieldValidator ValidationGroup="Supplier" runat="server" ControlToValidate="txtSupplierNameCreate"
                                                                                CssClass="text-danger" ErrorMessage="The supplier name field is required." Text="*" />
                                                            </asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtSupplierNameCreate" CssClass="form-control" />

                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <asp:Label runat="server" AssociatedControlID="txtSupplierCodeCreate" CssClass="col-md-4 control-label">Code
                                                                              <asp:RequiredFieldValidator ValidationGroup="Supplier" runat="server" ControlToValidate="txtSupplierCodeCreate"
                                                                                CssClass="text-danger" ErrorMessage="The Supplier code field is required."  Text="*"/>
                                                            </asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtSupplierCodeCreate" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="form-group row">
                                                            <asp:Label runat="server" AssociatedControlID="ddlSupplierTypeCreate" CssClass="col-md-4 control-label">Type
                                                                             <asp:RequiredFieldValidator ValidationGroup="Supplier" runat="server" ControlToValidate="ddlSupplierTypeCreate"
                                                                                CssClass="text-danger" ErrorMessage="The supplier type is required." InitialValue ="0"  Text="*"/>
                                                            </asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList runat="server" ID="ddlSupplierTypeCreate" CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <asp:Label runat="server" AssociatedControlID="ddlStatusCreate" CssClass="col-md-4 control-label">Status
                                                                             <asp:RequiredFieldValidator ValidationGroup="Supplier" runat="server" ControlToValidate="ddlStatusCreate"
                                                                                CssClass="text-danger" ErrorMessage="The status is required." InitialValue ="0"  Text="*"/>
                                                            </asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList runat="server" ID="ddlStatusCreate" CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row pull-right col-md-2">
                                                    <asp:ValidationSummary runat="server" CssClass="text-danger" />
                                                    <asp:Button ID="btnCreateSuppplier" CommandName="Add" runat="server" Text="Save" CssClass="btn btn-primary btn-md" ValidationGroup="Supplier" />
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
