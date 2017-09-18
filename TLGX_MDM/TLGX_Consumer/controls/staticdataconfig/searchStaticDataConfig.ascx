<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchStaticDataConfig.ascx.cs" Inherits="TLGX_Consumer.controls.staticdataconfig.searchStaticDataConfig" %>
<style type="text/css">
    .modalPopup {
        background-color: #696969;
        filter: alpha(opacity=40);
        opacity: 0.7;
        xindex: -1;
    }

    .progress, .alert {
        margin: 15px;
    }

    .alert {
        display: none;
    }
</style>
<script type="text/javascript">
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_beginRequest(BeginRequestHandler);
    prm.add_endRequest(EndRequestHandler);

    function showManageModal() {
        $("#moAddConfig").modal('show');
    }
    function closeManageModal() {
        $("#moAddConfig").modal('hide');
    }
    function pageLoad(sender, args) {
        var hv = $('#hdnFlag').val();
        if (hv == "true") {
            closeManageModal();
            $('#hdnFlag').val("false");
        }
    }

    function ddlForChanged(ddl) {
        var FOR = ddl.options[ddl.selectedIndex].text;
        var myVal = document.getElementById("<%=vddlAddSupplier.ClientID%>");
        if (FOR == 'MAPPING') {
            ValidatorEnable(myVal, true);
        }
        else {
            ValidatorEnable(myVal, false);
        }
    }

</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <div class="panel-group" id="accordion">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Mapping Search</a>
                    </h4>
                </div>
                <div id="collapseSearch" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <div class="container">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlFor">For </label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlFor" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlSupplierName">Supplier </label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlEntity">Entity</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlEntity" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlStatus">Status</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" />
                                            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" OnClick="btnReset_Click" />
                                        </div>
                                        <div class="col-sm-12">&nbsp; </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <br />

        <br />
        <div class="row">

            <div class="panel-group" id="accordionResult">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="panel-title row">
                            <div class="col-lg-8">
                                <a data-toggle="collapse" data-parent="#accordionResult" href="#collapseSearchResult">
                                    <h4 class="panel-title">Search Results (Total Count:
                                <asp:Label ID="lblTotalUploadConfig" runat="server" Text="0"></asp:Label>)</h4>
                                </a>
                            </div>
                            <div class="col-lg-3">

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
                            <div class="col-lg-1">
                                <asp:Button ID="btnAddConfig" runat="server" CssClass="btn btn-primary btn-sm" Text="Add New" OnClick="btnAddConfig_Click" OnClientClick="showManageModal();" />
                            </div>
                        </div>
                    </div>
                    <div id="collapseSearchResult" class="panel-collapse collapse in">
                        <div class="panel-body">
                            <div class="container">
                                <div class="panel-group" id="accordionSearchResult">
                                    <div class="form-group">
                                        <div id="dvMsg" runat="server" style="display: none;"></div>
                                    </div>
                                    <div class="col-lg-12">
                                        <asp:GridView ID="grdMappingConfig" runat="server" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="No Static Updates"
                                            CssClass="table table-hover table-striped" OnDataBound="grdMappingConfig_DataBound" OnRowCommand="grdMappingConfig_RowCommand"
                                            AllowCustomPaging="true" OnPageIndexChanging="grdMappingConfig_PageIndexChanging" DataKeyNames="SupplierImportAttribute_Id,Supplier_Id"
                                            OnRowDataBound="grdMappingConfig_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="For" HeaderText="For" />
                                                <asp:BoundField DataField="Supplier" HeaderText="Supplier Name" />
                                                <asp:BoundField DataField="Entity" HeaderText="Entity" />
                                                <asp:BoundField DataField="CREATE_DATE" HeaderText="Created" />
                                                <asp:BoundField DataField="EDIT_DATE" HeaderText="Last Edited" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                                <asp:HyperLinkField DataNavigateUrlFields="SupplierImportAttribute_Id" DataNavigateUrlFormatString="~/staticdata/config/manage.aspx?Config_Id={0}" Text="Select" ControlStyle-Font-Bold="true" NavigateUrl="~/staticdata/config/manage.aspx" ControlStyle-CssClass="btn btn-primary btn-sm" />
                                                <asp:TemplateField ShowHeader="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("Status").ToString() == "ACTIVE" ? "SoftDelete" : "UnDelete"   %>' CssClass="btn btn-default" CommandArgument='<%# Bind("SupplierImportAttribute_Id") %>'>
                                         <span aria-hidden="true" class='<%# Eval("Status").ToString() == "ACTIVE" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                                        <%# Eval("Status").ToString() == "ACTIVE" ? "Delete" : "UnDelete"   %>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pagination-ys" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <hr />
    </ContentTemplate>
</asp:UpdatePanel>

<div class="modal fade" id="moAddConfig" role="dialog">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">

            <div class="modal-header">
                <div class="panel panel-default">
                    <h4 class="modal-title">Add Mapping </h4>
                </div>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel ID="UpnlAddConfig" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnFlag" runat="server" ClientIDMode="Static" Value="" EnableViewState="false" />

                        <div class="row">
                            <div class="col-lg-3">
                                <div class="form-group">
                                    <div class="col-sm-4">
                                        <label class="control-label col-sm-4" for="ddlAddFor">
                                            For
                                                    <asp:RequiredFieldValidator ID="vddlAddFor" runat="server" ErrorMessage="*" ControlToValidate="ddlAddFor" InitialValue="0" CssClass="text-danger" ValidationGroup="AddConfig"></asp:RequiredFieldValidator>
                                        </label>
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlAddFor" runat="server" CssClass="form-control" AppendDataBoundItems="true" onchange="ddlForChanged(this);">
                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="ddlAddSupplier">
                                        Supplier 
                                                <asp:RequiredFieldValidator ID="vddlAddSupplier" runat="server" ErrorMessage="*" ControlToValidate="ddlAddSupplier" InitialValue="0" CssClass="text-danger" ValidationGroup="AddConfig"></asp:RequiredFieldValidator>
                                    </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlAddSupplier" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-3">
                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="ddlAddEntity">
                                        Entity
                                                <asp:RequiredFieldValidator ID="vddlAddEntity" runat="server" ErrorMessage="*" ControlToValidate="ddlAddEntity" InitialValue="0" CssClass="text-danger" ValidationGroup="AddConfig"></asp:RequiredFieldValidator>
                                    </label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlAddEntity" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>


                            </div>
                            <div class="col-lg-3">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-sm" Text="Add" CausesValidation="true" ValidationGroup="AddConfig" CommandName="Add" OnClick="btnAdd_Click" />
                                        <asp:Button ID="btnResetAdd" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" CausesValidation="false" CommandName="Reset" OnClick="btnResetAdd_Click" />
                                    </div>
                                    <div class="col-sm-12">&nbsp; </div>
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
