<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Inclusion.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.Inclusion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript">
    function showEditModal() {
        $("#moEditInclusions").modal('show');
    }
</script>
<div class="col-lg-3 pull-right">
    <div class="form-group pull-right">
        <div class="input-group" runat="server" id="divDropdownForEntries">
            <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
            <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" CssClass="form-control">
                <%--OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged"--%>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>25</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
                <asp:ListItem>100</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
</div>

<div class="row">
    <div class="panel-body">
        <asp:GridView ID="gvActInclusionSearch" runat="server" AllowPaging="True" AllowCustomPaging="true"
            EmptyDataText="No Activity Exclusion Found" CssClass="table table-hover table-striped"
            AutoGenerateColumns="false" OnPageIndexChanging="gvActInclusionSearch_PageIndexChanging"
            OnRowCommand="gvActInclusionSearch_RowCommand" DataKeyNames="Activity_Inclusions_Id">
            <%--OnRowDataBound="gvActInclusionSearch_RowDataBound"--%>
            <Columns>

                <asp:BoundField HeaderText="Exclusion Type" DataField="InclusionType" />
                <asp:BoundField HeaderText="Exclusion Name" DataField="InclusionName" />
                <asp:BoundField HeaderText="Exclusion For" DataField="InclusionFor" />
                <asp:BoundField HeaderText="Description" DataField="InclusionDescription" />
                <asp:BoundField HeaderText="Is Inclusion" DataField="IsInclusion" />
                <asp:BoundField HeaderText="Upload Date" DataField="Create_Date" DataFormatString="{0:dd/MM/yyyy hh:mm:ss tt}" />

                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Editing" CssClass="btn btn-default" CommandArgument='<%# Bind("Activity_Inclusions_Id") %>' Enabled='<%# Eval("IsActive") %>' OnClientClick="showEditModal();"> 
                                  <span aria-hidden="true">Edit</span>
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
</div>

<div class="modal fade" id="moEditInclusions" role="dialog">
    <div class="modal-dialog modal-xl">

        <div class="modal-content">

            <div class="modal-header">
                <h4 class="modal-title">Edit Activity Inclusion</h4>
            </div>

            <div class="modal-body">
                <asp:UpdatePanel ID="updEditActivity" runat="server">
                    <ContentTemplate>

                        <div class="row">
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <asp:ValidationSummary ID="vldSumActivity" runat="server" ValidationGroup="NewActivity" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                    <div id="divMsgAlertExc" runat="server" style="display: none"></div>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-sm-12 row">
                                <div class="panel panel-default">
                                    <div class="panel-heading">Edit Inclusion</div>
                                    <div class="panel-body">

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
                                                        <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="To date can't be less than from date." ControlToCompare="txtFrom" CultureInvariantValues="true" ControlToValidate="txtTo" ValidationGroup="vldgrpFileSearch" Text="*" CssClass="text-danger" Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator>--%>
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
                                                        <%--<asp:CompareValidator ID="vldCmpDateFromTo" runat="server" ErrorMessage="To date can't be less than from date." ControlToCompare="txtFrom" CultureInvariantValues="true" ControlToValidate="txtTo" ValidationGroup="vldgrpFileSearch" Text="*" CssClass="text-danger" Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-6">

                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="txtName">Inclusion Name</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox runat="server" ID="txtName" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="txtDescription">Inclusion Description</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-12 row">
                                <div class="panel panel-default">
                                    <div class="panel-heading">Add/Edit Inclusion Details</div>
                                    <div class="panel-body">
                                        <asp:FormView ID="frmInclusionDetails" runat="server" DataKeyNames="Activity_Inclusions_Id" DefaultMode="Insert" OnItemCommand="frmInclusionDetails_ItemCommand">

                                            <HeaderTemplate>
                                                <div class="form-group">
                                                    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="ActivityStatus" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                                </div>
                                            </HeaderTemplate>

                                            <InsertItemTemplate>
                                                <div class="form-group row">
                                                    <div class="col-md-12">

                                                        <div class="form-group row">

                                                            <div class="form-group col-md-3">
                                                                <label class="control-label col-sm-4" for="ddlType">
                                                                    Type
                                                                </label>
                                                                <div class="col-sm-8">
                                                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-md-3">
                                                                <label class="control-label col-sm-4" for="txtName">
                                                                    Name
                                                                </label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-md-3">
                                                                <label class="control-label col-sm-4" for="txtDescription">
                                                                    Description
                                                                </label>
                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="multiline" Rows="3" Columns="50"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-md-3">
                                                                <asp:Button runat="server" ID="btnAddDetails" Text="Add" CssClass="btn btn-primary" />
                                                                <asp:Button runat="server" ID="btnResetDetails" Text="Reset" CssClass="btn btn-primary" />
                                                            </div>
                                                        </div>


                                                        <%--<div class="form-group row">
                                                                
                                                            </div>--%>
                                                    </div>

                                                </div>

                                            </InsertItemTemplate>

                                            <EditItemTemplate>
                                                <div class="form-group row">
                                                    <div class="col-md-12">


                                                        <div class="form-group row">
                                                            <div class="col-md-6">
                                                                <label class="control-label col-sm-2" for="ddlType">
                                                                    Type
                                                                </label>
                                                                <div class="col-sm-10">
                                                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <label class="control-label col-sm-3" for="txtName">
                                                                    Name
                                                                </label>
                                                                <div class="col-sm-9">
                                                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div class="form-group row">
                                                            <div class="col-md-6">
                                                                <label class="control-label col-sm-3" for="txtDescription">
                                                                    Description
                                                                </label>
                                                                <div class="col-sm-9">
                                                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="multiline" Columns="50" Rows="5"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </EditItemTemplate>

                                        </asp:FormView>

                                        <div class="row">
                                            <div class="panel-body">
                                                <asp:GridView ID="gvActInclusionDetails" runat="server" AllowPaging="True" AllowCustomPaging="true"
                                                    EmptyDataText="No Activity Inclusion Found" CssClass="table table-hover table-striped"
                                                    AutoGenerateColumns="false" DataKeyNames="Activity_Inclusions_Id">
                                                    <%--OnPageIndexChanging="gvActInclusionDetails_PageIndexChanging"
                                                OnRowCommand="gvActInclusionDetails_RowCommand"--%>
                                                    <Columns>

                                                        <asp:BoundField HeaderText="Inclusion Type" DataField="InclusionType" />
                                                        <asp:BoundField HeaderText="Inclusion Name" DataField="InclusionName" />
                                                        <asp:BoundField HeaderText="Inclusion For" DataField="InclusionFor" />
                                                        <asp:BoundField HeaderText="Description" DataField="InclusionDescription" />
                                                        <asp:BoundField HeaderText="Is Inclusion" DataField="IsInclusion" />
                                                        <asp:BoundField HeaderText="Upload Date" DataField="Create_Date" DataFormatString="{0:dd/MM/yyyy hh:mm:ss tt}" />

                                                        <asp:TemplateField ShowHeader="false">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Editing1" CssClass="btn btn-default" CommandArgument='<%# Bind("Activity_Inclusions_Id") %>' Enabled='<%# Eval("IsActive") %>'> 
                                  <span aria-hidden="true">Edit</span>
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
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-12 row">
                                <div class="pull-right">
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" /><%--OnClick="btnAdd_Click"--%>
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-primary" /><%--OnClick="btnReset_Click"--%>
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
