<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PricesNDeals.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.PricesNDeals" %>


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
<asp:UpdatePanel ID="updPanPricesNDeals" runat="server">
    <ContentTemplate>
        <div id="dvMsg" runat="server" style="display: none;"></div>
        <div class="container">
            <div class="form-group col-md-12">
                <asp:LinkButton ID="btnNewActivity" runat="server" Text="Add New" CssClass="btn btn-primary btn-sm pull-right" OnClientClick="showAddNewActivityModal();" /><%--OnClick="btnNewActivity_Click"--%>
            </div>
        </div>
        <headertemplate>
            <div class="container">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="vldGrpRules" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
            </div>
        </headertemplate>

        <div class="container">
            <div class="panel panel-default">
                <div class="panel-heading">Prices And Deals</div>
                <div class="panel-body">
                    <asp:GridView ID="grdPricesNDeals" runat="server" AllowPaging="true" AllowCustomPaging="true" AutoGenerateColumns="False" DataKeyNames="Activity_Flavour_Id" EmptyDataText="No Hotel Rules for this hotel" CssClass="table table-hover table-striped">
                        <%--OnRowCommand="grdPricesNDeals_RowCommand" OnRowDataBound="grdPricesNDeals_RowDataBound"--%>
                        <Columns>
                            <asp:BoundField DataField="RuleType" HeaderText="RuleType" SortExpression="RuleType" />
                            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                            <asp:BoundField DataField="IsInternal" HeaderText="Is Internal" SortExpression="IsInternal" />
                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                        Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Activity_Policy_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                                        CssClass="btn btn-default" CommandArgument='<%# Bind("Activity_Policy_Id") %>'>
                                         <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                                        <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pagination-ys" />
                    </asp:GridView>
                </div>

            </div>
        </div>


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
                                            <div class="col-sm-12">
                                                <asp:ValidationSummary ID="vldSumActivity" runat="server" ValidationGroup="NewActivity" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                                <div id="divMsgAlertActivity" runat="server" style="display: none"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <asp:FormView ID="frmPolicy" runat="server" DataKeyNames="Activity_Policy_Id" DefaultMode="Insert">
                                    <%--OnItemCommand="frmRule_ItemCommand"--%>

                                    <InsertItemTemplate>
                                        <div class="">
                                            <div class="form-group">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">Add New Hotel Rule</div>
                                                    <div class="panel-body">

                                                        <div class="form-group">
                                                            <label class="control-label-mand" for="ddlRuleName">
                                                                Rule Name
                                        <asp:RequiredFieldValidator ID="vldddlRuleName" runat="server" ControlToValidate="ddlRuleName" ErrorMessage="Please select rule name" Text="*" InitialValue="0" ValidationGroup="vldGrpRules" CssClass="text-danger"></asp:RequiredFieldValidator></label>
                                                            <asp:DropDownList runat="server" ID="ddlRuleName" CssClass="form-control" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                            </asp:DropDownList>


                                                            <label class="control-label-mand" for="txtRuleText">
                                                                Description
                                    <asp:RequiredFieldValidator ID="vldddlDescriptionType" runat="server" ControlToValidate="txtRuleText" ErrorMessage="Please enter rule description" Text="*" ValidationGroup="vldGrpRules" CssClass="text-danger"></asp:RequiredFieldValidator></label>

                                                            <asp:TextBox ID="txtRuleText" runat="server" CssClass="form-control" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                    <label class="control-label" for="txtRuleText">Is Internal Rule</label>
                                                            <asp:CheckBox ID="chkIsInternal" runat="server" />&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="btnAddRule" CommandName="Add" runat="server" CssClass="btn btn-primary btn-sm" ValidationGroup="vldGrpRules" CausesValidation="true">AddNew Rule</asp:LinkButton>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </InsertItemTemplate>

                                    <EditItemTemplate>
                                        <div class="">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Update Hotel Rule</div>
                                                <div class="panel-body">

                                                    <div class="form-group">
                                                        <label class="control-label-mand" for="ddlRuleName">
                                                            Rule Name
                                    <asp:RequiredFieldValidator ID="vldddlRuleName" runat="server" ControlToValidate="ddlRuleName" ErrorMessage="Please select rule name" Text="*" InitialValue="0" ValidationGroup="vldGrpRules" CssClass="text-danger"></asp:RequiredFieldValidator></label>
                                                        <asp:DropDownList runat="server" ID="ddlRuleName" CssClass="form-control">
                                                            <asp:ListItem>-Select-</asp:ListItem>
                                                        </asp:DropDownList>



                                                        <label class="control-label-mand" for="txtRuleText">
                                                            Description
                                <asp:RequiredFieldValidator ID="vldddlDescriptionType" runat="server" ControlToValidate="txtRuleText" ErrorMessage="Please enter rule description" Text="*" ValidationGroup="vldGrpRules" CssClass="text-danger"></asp:RequiredFieldValidator></label>
                                                        <asp:TextBox ID="txtRuleText" runat="server" CssClass="form-control" Text='<%# Bind("Description") %>' />

                                                        &nbsp;&nbsp;&nbsp;
                                <label class="control-label" for="txtRuleText">Is Internal Rule</label>
                                                        <asp:CheckBox ID="chkIsInternal" runat="server" />
                                                        &nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="btnAddRule" runat="server" CommandName="Modify" CssClass="btn btn-primary btn-sm" CausesValidation="true" ValidationGroup="vldGrpRules">Update Rule</asp:LinkButton>
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
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>

                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>