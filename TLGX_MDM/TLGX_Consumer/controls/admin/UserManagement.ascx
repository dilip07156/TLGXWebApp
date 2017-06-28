<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserManagement.ascx.cs" Inherits="TLGX_Consumer.controls.admin.UserManagement" %>
<script type="text/javascript">
    function closeAddUpdateUserModal() {
        $("#moAddUpdateUser").modal('hide');
    }
    function showAddUpdateUserModal() {
        var ddlValue = $("#MainContent_UserManagement_ddlApplilcation option:selected").text();
        $("#lblApplication").text(ddlValue);
        $("#moAddUpdateUser").modal('show');
    }
    function pageLoad(sender, args) {
        var hv = $('#MainContent_UserManagement_hdnFlag').val();
        if (hv == "true") {
            closeAddUpdateUserModal();
        }
        $('#MainContent_UserManagement_hdnFlag').val("false");
    }
</script>
<asp:UpdatePanel ID="updUserGrid" runat="server">
    <ContentTemplate>
        <div class="form-horizontal">
            <div class="input-group col-md-6 input-group-lg">
                <label class="input-group-addon" for="ddlApplilcation"><strong>Manage User For Application: </strong></label>
                <asp:DropDownList ID="ddlApplilcation" runat="server" OnSelectedIndexChanged="ddlApplilcation_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <br />
        <div class="row" id="divUserDetails" runat="server" style="display: none;">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-sm-6">
                                <strong>Current System Users</strong>
                            </div>
                            <div class="col-sm-6">
                                <asp:Button runat="server" ID="btnNewCreate" OnClick="btnNewCreate_Click" OnClientClick="showAddUpdateUserModal();" CssClass="btn btn-sm btn-primary pull-right" Text="Create New User" />
                            </div>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div id="dvMsg" runat="server" style="display: none;"></div>
                        <div class="form-group pull-right">
                            <div class="input-group">
                                <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                                <asp:DropDownList ID="ddlShowEntries" runat="server" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                    <asp:ListItem>5</asp:ListItem>
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
                        <asp:GridView ID="grdListOfUsers" DataKeyNames="Userid" runat="server" AllowPaging="true" AllowCustomPaging="true" OnPageIndexChanging="grdListOfUsers_PageIndexChanging" CssClass="table table-hover table-striped" EmptyDataText="No Users in the System" AutoGenerateColumns="False"
                            OnRowCommand="grdListOfUsers_RowCommand" OnRowDataBound="grdListOfUsers_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="UserName" HeaderText="Username" />
                                <asp:BoundField DataField="EntityType" HeaderText="Entity Type" />
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default" OnClientClick="showAddUpdateUserModal();"
                                            CommandArgument='<%# Bind("Userid") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>' CssClass="btn btn-default" CommandArgument='<%# Bind("Userid") %>'>
                                         <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                                        <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" BorderStyle="None" />
                        </asp:GridView>

                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<%--<h4 class="page-header">Add / Update User</h4>--%>

<div class="modal fade" id="moAddUpdateUser" role="dialog">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <div class="input-group">
                    <h4 class="input-group-addon">Add / Update User For </h4>
                    <strong>
                        <label id="lblApplication" class="form-control"></label>
                    </strong>
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
                                        <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="User" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                        <div id="msgAlert" runat="server" style="display: none;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="panel panel-default">
                                    <div class="panel-heading">User Information</div>
                                    <div class="panel-body">
                                        <asp:FormView ID="frmUserdetail" runat="server" DefaultMode="Insert" OnItemCommand="frmUserdetail_ItemCommand">
                                            <EditItemTemplate>
                                                <div class="form-group row ">
                                                    <label for="txtEmail" class="col-md-4 col-form-label">Email</label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" TextMode="Email" />
                                                        <asp:RequiredFieldValidator ValidationGroup="User" runat="server" ControlToValidate="txtEmail"
                                                            CssClass="text-danger" ErrorMessage="The email field is required." />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="ddlManager" class="col-md-4 col-form-label">Manager</label>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList runat="server" ID="ddlManager" CssClass="form-control"></asp:DropDownList>
                                                        <div class="clear">&nbsp;</div>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="ddlEntityType" class="col-md-4 col-form-label">Entity Type</label>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlEntityType_SelectedIndexChanged" runat="server" ID="ddlEntityType" CssClass="form-control"></asp:DropDownList>
                                                        <div class="clear">&nbsp;</div>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label for="ddlEntity" class="col-md-4 col-form-label">Entity</label>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList runat="server" ID="ddlEntity" CssClass="form-control"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlEntity" runat="server" ValidationGroup="User" ControlToValidate="ddlEntity"
                                                            CssClass="text-danger" ErrorMessage="The entity selection is required." InitialValue="0" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="Password" class="col-md-4 col-form-label">Password</label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" ID="txtPswd" TextMode="Password" CssClass="form-control" />
                                                        <div class="clear">&nbsp;</div>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="txtConfirmPswd" class="col-md-4 col-form-label">Confirm password</label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" ID="txtConfirmPswd" TextMode="Password" CssClass="form-control" />
                                                        <asp:CompareValidator runat="server" ControlToCompare="txtPswd" ValidationGroup="User" ControlToValidate="txtConfirmPswd"
                                                            CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="offset-md-4 col-md-8">
                                                        <asp:ValidationSummary runat="server" CssClass="text-danger" />
                                                        <asp:Button ID="btnEditUser" CommandName="Edit" runat="server" Text="Update" CssClass="btn btn-primary btn-sm" ValidationGroup="User" />
                                                    </div>
                                                </div>
                                                </div>
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="col-md-4 control-label">Email</asp:Label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" TextMode="Email" />
                                                            <asp:RequiredFieldValidator ValidationGroup="User" runat="server" ControlToValidate="txtEmail"
                                                                CssClass="text-danger" ErrorMessage="The email field is required." />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <asp:Label runat="server" AssociatedControlID="ddlManager" CssClass="col-md-4 control-label">Manager</asp:Label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList runat="server" ID="ddlManager" CssClass="form-control"></asp:DropDownList>
                                                            <div class="clear">&nbsp;</div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <asp:Label runat="server" AssociatedControlID="ddlEntityType" CssClass="col-md-4 control-label">Entity Type</asp:Label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlEntityType_SelectedIndexChanged" runat="server" ID="ddlEntityType" CssClass="form-control"></asp:DropDownList>
                                                            <div class="clear">&nbsp;</div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <asp:Label runat="server" AssociatedControlID="ddlEntity" CssClass="col-md-4 control-label">Entity</asp:Label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList runat="server" ID="ddlEntity" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlEntity" runat="server" ValidationGroup="User" ControlToValidate="ddlEntity"
                                                                CssClass="text-danger" ErrorMessage="The entity selection is required." Enabled="false" InitialValue="0" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">

                                                        <asp:Label runat="server" AssociatedControlID="txtPswd" CssClass="col-md-4 control-label">Password</asp:Label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox runat="server" ID="txtPswd" TextMode="Password" CssClass="form-control" />
                                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="User" ControlToValidate="txtPswd"
                                                                CssClass="text-danger" ErrorMessage="The password field is required." />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <asp:Label runat="server" AssociatedControlID="txtConfirmPswd" CssClass="col-md-4 control-label">Confirm password</asp:Label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox runat="server" ID="txtConfirmPswd" TextMode="Password" CssClass="form-control" />
                                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="User" ControlToValidate="txtConfirmPswd"
                                                                CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                                                            <asp:CompareValidator runat="server" ControlToCompare="txtPswd" ValidationGroup="User" ControlToValidate="txtConfirmPswd"
                                                                CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <div class="col-md-12">
                                                            <asp:Button ID="btnAddUser" runat="server" CommandName="Add" Text="Add" CssClass="btn btn-primary btn-sm" ValidationGroup="User" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </InsertItemTemplate>
                                        </asp:FormView>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="panel panel-default">
                                    <div class="panel-heading">Roles</div>
                                    <div class="panel-body">
                                        <asp:GridView ID="grdRoles" runat="server" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="Id" Visible="false" />
                                                <asp:BoundField HeaderText="RoleName" DataField="RoleName" />
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkAddRole" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
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
