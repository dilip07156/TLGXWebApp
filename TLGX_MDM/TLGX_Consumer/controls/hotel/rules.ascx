<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="rules.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.rules" %>
<asp:UpdatePanel ID="updPanHotelRules" runat="server">
    <ContentTemplate>
        <div id="dvMsg" runat="server" style="display: none;"></div>
        <HeaderTemplate>
            <div class="container">
                <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="vldGrpRules" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
            </div>
        </HeaderTemplate>


        <asp:FormView ID="frmRule" runat="server" DataKeyNames="Accommodation_RuleInfo_Id" DefaultMode="Insert" OnItemCommand="frmRule_ItemCommand">

            <InsertItemTemplate>
                <div class="container">
                    <div class="form-inline">
                        <div class="panel panel-default">
                            <div class="panel-heading">Add New Hotel Rule</div>
                            <div class="panel-body">

                                <div class="form-inline">
                                    <label class="control-label-mand" for="ddlRuleName">
                                        Rule Name
                                        <asp:RequiredFieldValidator ID="vldddlRuleName" runat="server" ControlToValidate="ddlRuleName" ErrorMessage="Please select rule name" Text="*" InitialValue="0" ValidationGroup="vldGrpRules" CssClass="text-danger"></asp:RequiredFieldValidator></label>
                                        <asp:DropDownList runat="server" ID="ddlRuleName" CssClass="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                        </asp:DropDownList>


                                    <label class="control-label-mand" for="txtRuleText">Description
                                    <asp:RequiredFieldValidator ID="vldddlDescriptionType" runat="server" ControlToValidate="txtRuleText" ErrorMessage="Please enter rule description" Text="*" ValidationGroup="vldGrpRules" CssClass="text-danger"></asp:RequiredFieldValidator></label>

                                    <asp:TextBox ID="txtRuleText" runat="server" CssClass="form-control" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <label class="control-label" for="txtRuleText">Is Internal Rule</label>
                                    <asp:CheckBox ID="chkIsInternal" runat="server" />&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="btnAddRule" CommandName="Add" runat="server" CssClass="btn btn-primary btn-sm" ValidationGroup="vldGrpRules" CausesValidation="true">Add New Rule</asp:LinkButton>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </InsertItemTemplate>

            <EditItemTemplate>
                <div class="container">
                    <div class="panel panel-default">
                        <div class="panel-heading">Update Hotel Rule</div>
                        <div class="panel-body">

                            <div class="form-inline">
                                <label class="control-label-mand" for="ddlRuleName">
                                    Rule Name
                                    <asp:RequiredFieldValidator ID="vldddlRuleName" runat="server" ControlToValidate="ddlRuleName" ErrorMessage="Please select rule name" Text="*" InitialValue="0" ValidationGroup="vldGrpRules" CssClass="text-danger"></asp:RequiredFieldValidator></label>
                                    <asp:DropDownList runat="server" ID="ddlRuleName" CssClass="form-control">
                                        <asp:ListItem>-Select-</asp:ListItem>
                                    </asp:DropDownList>



                                <label class="control-label-mand" for="txtRuleText">Description
                                <asp:RequiredFieldValidator ID="vldddlDescriptionType" runat="server" ControlToValidate="txtRuleText" ErrorMessage="Please enter rule description" Text="*" ValidationGroup="vldGrpRules" CssClass="text-danger"></asp:RequiredFieldValidator></label>
                                <asp:TextBox ID="txtRuleText" runat="server" CssClass="form-control" Text='<%# Bind("Description") %>' />

                                &nbsp;&nbsp;&nbsp;
                                <label class="control-label" for="txtRuleText">Is Internal Rule</label>
                                <asp:CheckBox ID="chkIsInternal" runat="server" />
                                    &nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="btnAddRule" runat="server" CommandName="Modify" CssClass="btn btn-primary btn-sm" CausesValidation="true" ValidationGroup="vldGrpRules">Update</asp:LinkButton>
                            </div>

                        </div>
                    </div>
                </div>


            </EditItemTemplate>

        </asp:FormView>





        <div class="container">
            <div class="panel panel-default">
                <div class="panel-heading">Hotel Rules</div>
                <div class="panel-body">



                    <asp:GridView ID="grdHOtelRUles" runat="server" AutoGenerateColumns="False" DataKeyNames="Accommodation_RuleInfo_Id" EmptyDataText="No Hotel Rules for this hotel" CssClass="table table-hover table-striped" OnRowCommand="grdHOtelRUles_RowCommand" OnRowDataBound="grdHOtelRUles_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="RuleType" HeaderText="RuleType" SortExpression="RuleType" />
                            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                            <asp:BoundField DataField="IsInternal" HeaderText="Is Internal" SortExpression="IsInternal" />
                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                        Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Accommodation_RuleInfo_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                                        CssClass="btn btn-default" CommandArgument='<%# Bind("Accommodation_RuleInfo_Id") %>'>
                                         <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                                        <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>

