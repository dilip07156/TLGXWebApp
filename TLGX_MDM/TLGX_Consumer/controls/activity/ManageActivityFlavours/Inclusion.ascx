<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Inclusion.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.Inclusion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript">
    function showAddEditModalI() {
        $("#moEditInclusions").modal('show');
    }
</script>

<asp:UpdatePanel ID="updPanInclusion" runat="server">
    <ContentTemplate>
        <div id="dvMsg" runat="server" style="display: none;"></div>

        <headertemplate>
            <div class="container">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="vldGrpRules" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
            </div>
        </headertemplate>

        <div class="form-group">
            <h4 class="panel-title pull-left">Inclusion Details (Total Count:
            <asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label>)</h4>
        </div>

        <div class="row col-lg-3 pull-right">
            <div class="form-group pull-right">
                <div class="input-group" runat="server" id="divDropdownForEntries">
                    <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                    <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" CssClass="form-control">
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <asp:GridView ID="gvActInclusionSearch" runat="server" AllowPaging="True" AllowCustomPaging="true"
            EmptyDataText="No Activity Inclusion Found" CssClass="table table-hover table-striped"
            AutoGenerateColumns="false" OnPageIndexChanging="gvActInclusionSearch_PageIndexChanging"
            OnRowCommand="gvActInclusionSearch_RowCommand" DataKeyNames="Activity_Inclusions_Id"
            OnRowDataBound="gvActInclusionSearch_RowDataBound">
            <Columns>
                <asp:BoundField HeaderText="Inclusion For" DataField="InclusionFor" />
                <asp:BoundField HeaderText="Description" DataField="InclusionDescription" />

                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CssClass="btn btn-default"
                            CommandArgument='<%# Bind("Activity_Inclusions_Id") %>' Enabled='<%# (bool)Eval("IsActive") %>' OnClientClick="showAddEditModalI();"> 
                                  <span aria-hidden="true" class="glyphicon glyphicon-edit"> Edit</span>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ShowHeader="false" HeaderStyle-CssClass="Info">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                            CssClass="btn btn-default" CommandArgument='<%# Bind("Activity_Inclusions_Id") %>'>
                                                    <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat" %>'></span>
                                                    <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
            <PagerStyle CssClass="pagination-ys" />
        </asp:GridView>

    </ContentTemplate>
</asp:UpdatePanel>

<div class="modal fade" id="moEditInclusions" role="dialog">
    <div class="modal-dialog modal-xl">

        <div class="modal-content">

            <div class="modal-header">
                <h4 class="modal-title">Edit Activity Inclusion</h4>
            </div>

            <div class="modal-body">
                <asp:UpdatePanel ID="updEditActivity" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnFlag" runat="server" Value="" EnableViewState="false" />
                        <div class="row">
                            <div class="form-group">
                                <div class="col-sm-12">

                                    <div id="divMsgAlertExc" runat="server" style="display: none"></div>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-sm-12 row">
                                <div class="panel panel-default">
                                    <div class="panel-heading">Edit Inclusion</div>
                                    <div class="panel-body">

                                        <asp:FormView ID="frmInclusion" runat="server" DataKeyNames="Activity_Inclusions_Id" DefaultMode="Insert" OnItemCommand="frmInclusion_ItemCommand">

                                            <HeaderTemplate>
                                                <div class="form-group">
                                                    <asp:ValidationSummary ID="vldSumActivity" runat="server" ValidationGroup="NewActivity" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                                </div>
                                            </HeaderTemplate>
                                            
                                            <EditItemTemplate>
                                                <div class="form-group">
                                                    <div class="col-sm-6">
                                                        <div class="form-group row">
                                                            <label class="control-label col-sm-6" for="chkIsInclusion">Is Inclusion</label>
                                                            <div class="col-sm-6">
                                                                <asp:CheckBox ID="chkIsInclusion" runat="server" Checked='<%# Bind("IsInclusion") %>' />
                                                            </div>
                                                        </div>

                                                        <div class="form-group row">
                                                            <label class="control-label col-sm-6" for="ddlInclusionFor">Inclusion For</label>
                                                            <div class="col-sm-6">
                                                                <asp:DropDownList runat="server" ID="ddlInclusionFor" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>

                                                        <div class="form-group row">
                                                            <label class="control-label col-sm-6" for="ddlInclusionType">Inclusion Type</label>
                                                            <div class="col-sm-6">
                                                                <asp:DropDownList runat="server" ID="ddlInclusionType" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>

                                                        <div class="form-group row">
                                                            <label class="control-label col-sm-6" for="txtFromI">From</label>
                                                            <div class="col-sm-6">
                                                                <div class="input-group">
                                                                    <asp:TextBox ID="txtFromI" runat="server" CssClass="form-control" />
                                                                    <span class="input-group-btn">
                                                                        <button class="btn btn-default" type="button" id="iCalFromI">
                                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                                        </button>
                                                                    </span>

                                                                </div>
                                                                <cc1:CalendarExtender ID="CalFromDateI" runat="server" TargetControlID="txtFromI" Format="dd/MM/yyyy" PopupButtonID="iCalFromI"></cc1:CalendarExtender>
                                                                <cc1:FilteredTextBoxExtender ID="axfte_txtFromI" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtFromI" />

                                                            </div>
                                                        </div>

                                                        <div class="form-group row">
                                                            <label class="control-label col-sm-6" for="txtToI">To</label>
                                                            <div class="col-sm-6">
                                                                <div class="input-group">
                                                                    <asp:TextBox ID="txtToI" runat="server" CssClass="form-control" />
                                                                    <span class="input-group-btn">
                                                                        <button class="btn btn-default" type="button" id="iCalToI">
                                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                                        </button>
                                                                    </span>

                                                                </div>
                                                                <cc1:CalendarExtender ID="calToDateI" runat="server" TargetControlID="txtToI" Format="dd/MM/yyyy" PopupButtonID="iCalToI"></cc1:CalendarExtender>
                                                                <cc1:FilteredTextBoxExtender ID="axfte_txtToI" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtToI" />

                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-6">

                                                        <div class="form-group row">
                                                            <label class="control-label col-sm-6" for="txtName">Inclusion Name</label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox runat="server" ID="txtName" CssClass="form-control" Text='<%# Bind("InclusionName") %>'></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="form-group row">
                                                            <label class="control-label col-sm-6" for="txtDescription">Inclusion Description</label>
                                                            <div class="col-sm-6">
                                                                <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" Text='<%# Bind("InclusionDescription") %>' TextMode="MultiLine"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="form-group row">
                                                            <div class="pull-right">
                                                                <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="Update" CssClass="btn btn-primary" /><%--OnClick="btnAdd_Click"--%>
                                                                <asp:Button ID="btnReset" runat="server" CommandName="Reset" Text="Reset" CssClass="btn btn-primary" /><%--OnClick="btnReset_Click"--%>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </EditItemTemplate>

                                        </asp:FormView>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button>
            </div>

        </div>

    </div>

</div>
