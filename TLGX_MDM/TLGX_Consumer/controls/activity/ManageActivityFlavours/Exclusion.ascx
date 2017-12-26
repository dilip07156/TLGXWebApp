<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Exclusion.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.Exclusion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript">

    //(function pageLoad() {
    //    //debugger;
    //    showEditModal()
    //})();

    function showEditModalE() {
        //debugger;
        //var x = $("#MainContent_Inclusions_Inclusion_hdnFlag").val("hello");
        //if (x.val() == "hello")
        //{
        //    alert(x.val());
        $("#moEditExclusion").modal('show');
        //    $('#moEditExclusion').modal({
        //        backdrop: 'static',
        //        keyboard: false
        //    });
        //}

    }
    function closeEditModalE() {
        $("#moEditExclusion").modal('destroy');
    }

</script>
<div class="form-group">
    <div class="form-group">
        <h4 class="panel-title pull-left">Exclusion Details (Total Count:
            <asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label>)</h4>
    </div>
    <div class="form-group">
        <div class="col-lg-3 pull-right row">
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
    </div>

    <div class="row">
        <div class="panel-body">
            <asp:GridView ID="gvActInclusionSearch" runat="server" AllowPaging="True" AllowCustomPaging="true"
                EmptyDataText="No Activity Exclusion Found" CssClass="table table-hover table-striped"
                AutoGenerateColumns="false" OnPageIndexChanging="gvActInclusionSearch_PageIndexChanging"
                OnRowCommand="gvActInclusionSearch_RowCommand" DataKeyNames="Activity_Inclusions_Id">
                <%--OnRowDataBound="gvActInclusionSearch_RowDataBound"--%>
                <Columns>
                    <asp:BoundField HeaderText="Exclusion For" DataField="InclusionFor" />
                    <asp:BoundField HeaderText="Description" DataField="InclusionDescription" />
                    <asp:TemplateField ShowHeader="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Editing" CssClass="btn btn-default" CommandArgument='<%# Bind("Activity_Inclusions_Id") %>' Enabled='<%# Eval("IsActive") %>' OnClientClick="showEditModalE();"> 
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

<div class="modal fade" id="moEditExclusion" role="dialog">
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
                                                    <label class="control-label col-sm-6" for="txtFromE">From</label>
                                                    <div class="col-sm-6">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtFromE" runat="server" CssClass="form-control" />
                                                            <span class="input-group-btn">
                                                                <button class="btn btn-default" type="button" id="iCalFromE">
                                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                                </button>
                                                            </span>

                                                        </div>
                                                        <cc1:CalendarExtender ID="CalFromDateE" runat="server" TargetControlID="txtFromE" Format="dd/MM/yyyy" PopupButtonID="iCalFromE"></cc1:CalendarExtender>
                                                        <cc1:FilteredTextBoxExtender ID="axfte_txtFromE" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtFromE" />
                                                        <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="To date can't be less than from date." ControlToCompare="txtFrom" CultureInvariantValues="true" ControlToValidate="txtTo" ValidationGroup="vldgrpFileSearch" Text="*" CssClass="text-danger" Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator>--%>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="txtToE">To</label>
                                                    <div class="col-sm-6">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtToE" runat="server" CssClass="form-control" />
                                                            <span class="input-group-btn">
                                                                <button class="btn btn-default" type="button" id="iCalToE">
                                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                                </button>
                                                            </span>

                                                        </div>
                                                        <cc1:CalendarExtender ID="calToDateE" runat="server" TargetControlID="txtToE" Format="dd/MM/yyyy" PopupButtonID="iCalToE"></cc1:CalendarExtender>
                                                        <cc1:FilteredTextBoxExtender ID="axfte_txtToE" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtToE" />
                                                        <%--<asp:CompareValidator ID="vldCmpDateFromTo" runat="server" ErrorMessage="To date can't be less than from date." ControlToCompare="txtFrom" CultureInvariantValues="true" ControlToValidate="txtTo" ValidationGroup="vldgrpFileSearch" Text="*" CssClass="text-danger" Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-6">

                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="txtName">Exclusion Name</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox runat="server" ID="txtName" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="txtDescription">Exclusion Description</label>
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


