<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Policy.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.Policy" %>


<script type="text/javascript">
    function showAddNewPolicyModal() {
        $("#moAddPolicy").modal('show');
    }
    //function closeAddNewActivityModal() {
    //    $("#moAddNewActivityModal").modal('hide');
    //}
    //function page_load(sender, args) {
    //    closeAddNewActivityModal();
    //}
</script>
<asp:UpdatePanel ID="updPanPolicy" runat="server">
    <ContentTemplate>
        <div id="dvMsg" runat="server" style="display: none;"></div>

        <div class="container">
            <div class="form-group col-md-12">
                <asp:LinkButton ID="btnNewActivity" runat="server" Text="Add New" CssClass="btn btn-primary btn-sm pull-right" OnClientClick="showAddNewPolicyModal();" /><%--OnClick="btnNewActivity_Click"--%>
            </div>
        </div>

        <headertemplate>
            <div class="container">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="vldGrpRules" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
            </div>
        </headertemplate>

        <div class="panel panel-default">
            <div class="panel-heading">Policy</div>
            <div class="panel-body">

                <div class="row col-lg-3 pull-right">
                    <div class="form-group pull-right">
                        <div class="input-group" runat="server" id="divDropdownForEntries">
                            <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                            <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" CssClass="form-control"
                                OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <asp:GridView ID="grdPolicy" runat="server" AllowPaging="true" AllowCustomPaging="true" AutoGenerateColumns="False" DataKeyNames="Activity_Flavour_Id"
                    EmptyDataText="No Hotel Rules for this hotel" CssClass="table table-hover table-striped" OnRowCommand="grdPolicy_RowCommand" OnRowDataBound="grdPolicy_RowDataBound">
                    <%--OnRowDataBound="grdPolicy_RowDataBound"--%>
                    <Columns>
                        <asp:BoundField DataField="Policy_Type" HeaderText="Policy Type" SortExpression="Policy_Type" />
                        <asp:BoundField DataField="PolicyName" HeaderText="Policy Name" SortExpression="PolicyName" />
                        <asp:BoundField DataField="PolicyDescription" HeaderText="Policy Description" SortExpression="PolicyDescription" />
                        <asp:TemplateField ShowHeader="false">

                            <ItemTemplate>
                                <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                    Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Activity_Policy_Id") %>' OnClientClick="showAddNewPolicyModal();">
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ShowHeader="false" HeaderStyle-CssClass="Info">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                                    CssClass="btn btn-default" CommandArgument='<%# Bind("Activity_Policy_Id") %>'>
                                                    <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat" %>'></span>
                                                    <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>
            </div>

        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<div class="modal fade" id="moAddPolicy" role="dialog">
    <div class="modal-dialog modal-xl">

        <div class="modal-content">

            <div class="modal-header">
                <h4 class="modal-title">Add New</h4>
            </div>

            <div class="modal-body">

                <asp:UpdatePanel ID="updNewActivity" runat="server">
                    <ContentTemplate>

                        <div class="row">
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <asp:ValidationSummary ID="vldSumActivity" runat="server" ValidationGroup="NewActivity" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                    <div id="divMsgAlertPolicy" runat="server" style="display: none"></div>
                                </div>
                            </div>
                        </div>

                        <asp:FormView ID="frmPolicy" runat="server" DataKeyNames="Activity_Policy_Id" DefaultMode="Insert" CssClass="col-md-12" OnItemCommand="frmPolicy_ItemCommand">

                            <InsertItemTemplate>

                                <div class="col-sm-12 row">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">Add New Policy</div>
                                        <div class="panel-body">

                                            <div class="form-group">
                                                <div class="col-sm-6">

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="ddlPolicyType">Policy Type</label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList runat="server" ID="ddlPolicyType" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="txtName">Policy Name</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" ID="txtName" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="txtDescription">Policy Description</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="col-sm-6">

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="chkIsActive">Active</label>
                                                        <div class="col-sm-6">
                                                            <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Bind("IsActive") %>' />
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="chkIsAllow">Allow</label>
                                                        <div class="col-sm-6">
                                                            <asp:CheckBox ID="chkIsAllow" runat="server" Checked='<%# Bind("AllowedYN") %>' />
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-sm-6 pull-right">
                                                            <asp:Button ID="btnAdd" runat="server" Text="Add" CommandName="Add" CssClass="btn btn-primary" />
                                                            <asp:Button ID="btnReset" runat="server" Text="Reset" CommandName="Reset" CssClass="btn btn-primary" />
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </InsertItemTemplate>

                            <EditItemTemplate>

                                <div class="col-sm-12 row">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">Add New Policy</div>
                                        <div class="panel-body">

                                            <div class="form-group">
                                                <div class="col-sm-6">

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="ddlPolicyType">Policy Type</label>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList runat="server" ID="ddlPolicyType" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="txtName">Policy Name</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" ID="txtName" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="txtDescription">Policy Description</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="col-sm-6">

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="chkIsActive">Active</label>
                                                        <div class="col-sm-6">
                                                            <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Bind("IsActive") %>' />
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-6" for="chkIsAllow">Allow</label>
                                                        <div class="col-sm-6">
                                                            <asp:CheckBox ID="chkIsAllow" runat="server" Checked='<%# Bind("AllowedYN") %>' />
                                                        </div>
                                                    </div>

                                                    <div class="form-group row">
                                                        <div class="col-sm-6 pull-right">
                                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="Update" CssClass="btn btn-primary" />
                                                            <asp:Button ID="btnReset" runat="server" Text="Reset" CommandName="Reset" CssClass="btn btn-primary" />
                                                        </div>
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

            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button>
            </div>

        </div>

    </div>

</div>




