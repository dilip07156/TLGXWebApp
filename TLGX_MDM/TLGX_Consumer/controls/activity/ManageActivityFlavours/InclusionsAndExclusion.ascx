<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InclusionsAndExclusion.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.InclusionsAndExclusion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script>
    function showAddEditModal() {
        $("#moAddEditInclusionsExclusions").modal('show');
    }
</script>
<style>
    @media (min-width: 768px) {
        .modal-xl {
            width: 80%;
            max-width: 1200px;
        }
    }
</style>

<asp:UpdatePanel ID="updPanInclusionExclusion" runat="server">
    <ContentTemplate>

        <div class="panel-group">

            <div id="dvMsg" runat="server" style="display: none;"></div>

            <div class="panel-heading clearfix row">
                <asp:Button CssClass="pull-right btn btn-primary" runat="server" ID="btnAddNewInclusion" Text="Add New" OnClick="btnAddNewInclusion_Click" OnClientClick="showAddEditModal();" />
            </div>

            <div class="form-group">
                <h3 class="panel-title pull-left">Inclusion Details (Total Count:
            <asp:Label ID="lblTotalRecordsInclusions" runat="server" Text="0"></asp:Label>)</h3>
            </div>
            <asp:GridView ID="gvActInclusionSearch" runat="server"
                EmptyDataText="No Activity Inclusion Found" CssClass="table table-hover table-striped"
                AutoGenerateColumns="false" OnRowCommand="gvActInclusionSearch_RowCommand" DataKeyNames="Activity_Inclusions_Id"
                OnRowDataBound="gvActInclusionSearch_RowDataBound">
                <Columns>
                    <asp:BoundField HeaderText="Inclusion For" DataField="InclusionFor" />
                    <asp:BoundField HeaderText="Description" DataField="InclusionDescription" />

                    <asp:TemplateField ShowHeader="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CssClass="btn btn-default" CommandName="Editing"
                                CommandArgument='<%# Bind("Activity_Inclusions_Id") %>' Enabled='<%# (bool)Eval("IsActive") %>' OnClientClick="showAddEditModal();"> 
                                  <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp;Edit
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

            <div class="form-group">
                <h3 class="panel-title pull-left">Exclusion Details (Total Count:
            <asp:Label ID="lblTotalRecordsExclusions" runat="server" Text="0"></asp:Label>)</h3>
            </div>

            <asp:GridView ID="gvActExclusionSearch" runat="server"
                EmptyDataText="No Activity Exclusion Found" CssClass="table table-hover table-striped"
                AutoGenerateColumns="false" DataKeyNames="Activity_Inclusions_Id"
                OnRowCommand="gvActExclusionSearch_RowCommand" OnRowDataBound="gvActExclusionSearch_RowDataBound">

                <Columns>
                    <asp:BoundField HeaderText="Exclusion For" DataField="InclusionFor" />
                    <asp:BoundField HeaderText="Description" DataField="InclusionDescription" />

                    <asp:TemplateField ShowHeader="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CssClass="btn btn-default" CommandName="Editing"
                                CommandArgument='<%# Bind("Activity_Inclusions_Id") %>' Enabled='<%# (bool)Eval("IsActive") %>' OnClientClick="showAddEditModal();"> 
                                  <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp;Edit
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

        </div>

    </ContentTemplate>
</asp:UpdatePanel>

<div class="modal fade" id="moAddEditInclusionsExclusions" role="dialog">
    <div class="modal-dialog modal-xl">

        <div class="modal-content">

            <div class="modal-header">
                <h4 class="modal-title">Add/Edit</h4>
            </div>

            <div class="modal-body">
                <asp:UpdatePanel ID="updIncExc" runat="server">
                    <ContentTemplate>

                        <div class="row">
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <asp:ValidationSummary ID="vldSumActivity" runat="server" ValidationGroup="NewActivity" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                    <div id="divMsgAlertIncExc" runat="server" style="display: none"></div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12 row">
                            <div class="panel panel-default">
                                <div class="panel-heading">Add/Edit Inclusions & Exclusions</div>
                                <div class="panel-body">

                                    <asp:HiddenField ID="hdnId" runat ="server" />

                                    <div class="form-group">
                                        <div class="col-sm-6">
                                            <div class="form-group row">
                                                <label class="control-label col-sm-6" for="chkIsInclusion">Is Inclusion</label>
                                                <div class="col-sm-6">
                                                    <asp:CheckBox ID="chkIsInclusion" runat="server" />
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="control-label col-sm-6" for="ddlInclusionFor">For</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList runat="server" ID="ddlInclusionFor" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="control-label col-sm-6" for="ddlInclusionType">Type</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList runat="server" ID="ddlInclusionType" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="control-label col-sm-6" for="txtFrom">From</label>
                                                <div class="col-sm-6">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" />
                                                        <span class="input-group-btn">
                                                            <button class="btn btn-default" type="button" id="iCalFrom">
                                                                <span class="glyphicon glyphicon-calendar"></span>
                                                            </button>
                                                        </span>

                                                    </div>
                                                    <cc1:CalendarExtender ID="CalFromDate" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy" PopupButtonID="iCalFrom"></cc1:CalendarExtender>
                                                    <cc1:FilteredTextBoxExtender ID="axfte_txtFrom" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtFrom" />
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="control-label col-sm-6" for="txtTo">To</label>
                                                <div class="col-sm-6">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" />
                                                        <span class="input-group-btn">
                                                            <button class="btn btn-default" type="button" id="iCalTo">
                                                                <span class="glyphicon glyphicon-calendar"></span>
                                                            </button>
                                                        </span>

                                                    </div>
                                                    <cc1:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy" PopupButtonID="iCalTo"></cc1:CalendarExtender>
                                                    <cc1:FilteredTextBoxExtender ID="axfte_txtTo" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtTo" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-6">

                                            <div class="form-group row">
                                                <label class="control-label col-sm-6" for="txtName">Name</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" ID="txtName" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="control-label col-sm-6" for="txtDescription">Description</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-6 pull-right">
                                                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="btn btn-primary" />
                                                    <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-primary" />
                                                </div>
                                            </div>
                                        </div>
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


