<%@ Page Title="Role Admin" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" CodeBehind="RoleAdmin.aspx.cs" Inherits="TLGX_Consumer.admin.RoleAdmin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function confirmDelete() {
            var iscomfirm = false;
            iscomfirm = confirm("Are you sure you want to delete role ?");
            return iscomfirm;
        }
        function HideLabel() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("#MainContent_dvMsg").style.display = "none";
            }, seconds * 1000);
        };
        function CloseAddUpdateRoleModal() {
            $("#moAddUpdateRole").modal('hide');
        }
        function showAddUpdateRoleModal() {
            $("#moAddUpdateRole").modal('show');
            if ($('#MainContent_msgAlert').css('display') == 'block')
                $('#MainContent_msgAlert').css('display', 'none');
        }
        function pageLoad(sender, args) {
            var hv = $('#MainContent_hdnFlag').val();
            if (hv == "true") {
                CloseAddUpdateRoleModal();
                $('#MainContent_hdnFlag').val("false");
            }
        }
    </script>
    <h2><%: Title %>.</h2>

    <asp:UpdatePanel ID="updUserGrid" runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <hr />
                <div class="input-group col-md-6 input-group-lg">
                    <label class="input-group-addon" for="ddlApplilcation"><strong>Manage Role For Application: </strong></label>
                    <asp:DropDownList ID="ddlApplilcation" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlApplilcation_SelectedIndexChanged" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>

            <%--Role Details Grid--%>
            <br />
            <div class="row" id="divRoleDetails" runat="server" style="display: none;">
                <div class="col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-sm-6">
                                    <strong>Current System Users</strong>
                                </div>
                                <div class="col-sm-6">
                                    <asp:Button runat="server" ID="btnNewCreate" OnClick="btnNewCreate_Click" OnClientClick="showAddUpdateRoleModal();" CssClass="btn btn-sm btn-primary pull-right" Text="Create New Role" />
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-md-12">
                                <div id="dvMsg" runat="server" enableviewstate="false" style="display: none;"></div>
                                <div class="form-group pull-right">
                                    <div class="input-group">
                                        <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                                        <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged" CssClass="form-control">
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
                            </div>
                            <div class="form-group col-md-12">
                                <asp:GridView ID="gvRoles" runat="server" AllowPaging="true" AllowCustomPaging="true" CssClass="table table-hover table-striped"
                                    AutoGenerateColumns="false" DataKeyNames="RoleID" OnPageIndexChanging="gvRoles_PageIndexChanging" OnRowDeleting="gvRoles_RowDeleting" EmptyDataText="No Role Found!!"
                                    OnRowCommand="gvRoles_RowCommand" OnRowDataBound="gvRoles_RowDataBound">
                                    <Columns>
                                        <asp:BoundField HeaderText="RoleName" DataField="RoleName" />
                                        <asp:CommandField ShowDeleteButton="True" ButtonType="Link" />
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

    <div class="modal fade" id="moAddUpdateRole" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Add / Update Role Details</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="AddUpdRoleModal" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdnFlag" runat="server" Value="" EnableViewState="false" />
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="Role" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                    <div id="msgAlert" runat="server" style="display: none;"></div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">Role Information</div>
                                        <div class="panel-body">
                                            <asp:FormView ID="frmRoledetail" DataKeyNames="RoleId" runat="server" DefaultMode="Insert" OnItemCommand="frmRoledetail_ItemCommand">

                                                <InsertItemTemplate>
                                                    <div class="form-group row">
                                                        <label for="lblApplication" class="control-label col-md-6">Application Name:</label>
                                                        <div class="col-md-6">
                                                            <asp:Label runat="server" ID="lblApplication" CssClass="control-label " />
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="txtRoleName" class="control-label col-md-6">Role Name:<asp:RequiredFieldValidator runat="server" ControlToValidate="txtRoleName" Text="*" ValidationGroup="Role" CssClass="text-danger" ErrorMessage="The Role name is required." /></label>
                                                        <div class="col-md-6">
                                                            <asp:TextBox runat="server" ID="txtRoleName" CssClass="form-control" />

                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <div class="col-md-6">
                                                            <asp:Button ID="btnAdd" runat="server" CommandName="Add" Text="Add" CssClass="btn btn-primary" ValidationGroup="Role" />
                                                        </div>
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
</asp:Content>
