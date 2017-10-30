<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityDescription.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.ActivityDescription" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script>
    function showDescriptionModal() {
        $("#moDescription").modal('show');
    }
    function closeDescriptionModal() {
        $("#moDescription").modal('hide');
    }
    function pageLoad(sender, args) {
        var hv = $('#MainContent_ActivityDescription_hdnFlag').val();
        //alert(hv + " : : HiddenFlag")
        if (hv == "true") {
            closeDescriptionModal();
            $('#MainContent_ActivityDescription_hdnFlag').val("false");
        }
    }

</script>
<script src="../../../Scripts/bootbox.min.js"></script>
<asp:UpdatePanel ID="updMedia" runat="server">
    <ContentTemplate>
        <%--<div class="panel-group" id="searchResult">
            <div class="panel panel-default">
                <div class="panel-heading clearfix">--%>
                    <h4 class="panel-title pull-left">
                        Description Details (Total Count:
                            <asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label>)</h4>
                    <asp:Button CssClass="pull-right btn btn-primary" runat="server" ID="btnNewUpload" OnClick="btnNewUpload_Click" Text="Add New" OnClientClick="showDescriptionModal()" />
                    <div class="col-lg-3 pull-right">
                        <div class="form-group pull-right">
                            <div class="input-group" runat="server" id="divDropdownForEntries">
                                <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                                <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>25</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                <%--</div>--%>
                <%--<div id="collapseSearchResult" class="panel-collapse collapse in">
                    <div class="panel-body">--%>
                        <div id="dvMsg" runat="server" style="display: none;"></div>
                        <asp:GridView ID="gvDescriptionSearch" runat="server" AllowPaging="True" AllowCustomPaging="true"
                            EmptyDataText="No Data Found" CssClass="table table-hover table-striped"
                            AutoGenerateColumns="false" OnPageIndexChanging="gvDescriptionSearch_PageIndexChanging"
                            OnRowCommand="gvDescriptionSearch_RowCommand" DataKeyNames="Activity_Description_Id" OnRowDataBound="gvDescriptionSearch_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="FromDate" DataField="FromDate" DataFormatString="{0:dd/MM/yyyy} " />
                                <asp:BoundField HeaderText="ToDate" DataField="ToDate" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField HeaderText="Name" DataField="Description_Name" />
                                <asp:BoundField HeaderText="Description" DataField="Description" />
                                <asp:BoundField HeaderText="Type" DataField="DescriptionType" />
                                <asp:BoundField HeaderText="SubType" DataField="DescriptionSubType" />
                                <asp:BoundField HeaderText="Description For" DataField="DescriptionFor" />
                                <asp:BoundField HeaderText="Source" DataField="Source" />
                                <asp:BoundField HeaderText="Language Code" DataField="Language_Code" />
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                            Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Activity_Description_Id") %>' OnClientClick="showDescriptionModal()">
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false" HeaderStyle-CssClass="Info">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                                            CssClass="btn btn-default" CommandArgument='<%# Bind("Activity_Description_Id") %>'>
                                                    <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat" %>'></span>
                                                    <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                    <%--</div>
                </div>--%>

            <%--</div>
        </div>--%>

    </ContentTemplate>
</asp:UpdatePanel>

<div class="modal fade" id="moDescription" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">

            <div class="modal-header">
                <h4 class="modal-title">Attribute Details</h4>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel ID="upDescriptionAddEdit" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnFlag" runat="server" Value="" EnableViewState="false" />

                        <div class="form-group">
                            <div class="col-sm-12">
                                <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="vldgrpDescriptions" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                            </div>
                        </div>
                        <asp:FormView ID="frmDescription" runat="server" DataKeyNames="Activity_Description_Id" DefaultMode="Insert" OnItemCommand="frmDescription_ItemCommand">
                            <InsertItemTemplate>
                                <div class="panel panel-default">
                                    <div class="panel-heading">Add</div>
                                    <div class="panel-body">

                                        <div class=" form-group row">
                                            <div class="col-md-6">
                                                <label for="txtName" class="control-label-mand col-sm-6">
                                                    Name
                                            <asp:RequiredFieldValidator ID="rtxtName" runat="server" ControlToValidate="txtName" ErrorMessage="Please enter description Name" Text="*" ValidationGroup="vldgrpDescriptions" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                </label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtName" runat="server" Rows="5" CssClass="form-control" />
                                                </div>
                                            </div>

                                            <div class="col-md-6">
                                                <label for="txtDescriptionFor" class="control-label-mand col-sm-6">
                                                    Description For
                                            <asp:RequiredFieldValidator ID="rtxtDescriptionFor" runat="server" ControlToValidate="txtDescriptionFor" ErrorMessage="Please enter description Name" Text="*" ValidationGroup="vldgrpDescriptions" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                </label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtDescriptionFor" runat="server" Rows="5" CssClass="form-control" />
                                                </div>

                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-md-6">
                                                <label for="ddlDescriptionType" class="control-label-mand col-sm-6">
                                                    Description Type
                                        <%--<asp:RequiredFieldValidator ID="rddlDescriptionType" runat="server" ControlToValidate="ddlDescriptionType" ErrorMessage="Please select description type" Text="*" ValidationGroup="vldgrpDescriptions" InitialValue="0" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
                                                </label>

                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlDescriptionType" runat="server" CssClass="form-control" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlDescriptionType_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-6">
                                                <label for="ddlDescriptionSubType" class="control-label-mand col-sm-6">
                                                    Description  Sub Type
                                        <%--<asp:RequiredFieldValidator ID="rddlDescriptionSubType" runat="server" ControlToValidate="ddlDescriptionSubType" ErrorMessage="Please select description type" Text="*" ValidationGroup="vldgrpDescriptions" InitialValue="0" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
                                                </label>

                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlDescriptionSubType" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-md-6">
                                                <label for="txtFrom" class="control-label-mand col-sm-6">
                                                    From
                                        <asp:RequiredFieldValidator ID="vldtxtFrom" runat="server" ControlToValidate="txtFrom" ErrorMessage="Please select from date" Text="*" ValidationGroup="vldgrpDescriptions" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                </label>
                                                <div class="col-sm-6">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" />
                                                        <span class="input-group-btn">
                                                            <button class="btn btn-default" type="button" id="iCalFrom">
                                                                <span class="glyphicon glyphicon-calendar"></span>
                                                            </button>
                                                        </span>
                                                         <cc1:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy" PopupButtonID="iCalFrom"></cc1:CalendarExtender>
                                                    <cc1:FilteredTextBoxExtender ID="axfte_txtFrom" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtFrom" />
                                                
                                                    </div>
                                                   </div>
                                            </div>

                                            <div class="col-md-6">
                                                <label for="txtTo" class="control-label-mand col-sm-6">
                                                    To
                                        <asp:RequiredFieldValidator ID="vldtxtTo" runat="server" ControlToValidate="txtTo" ErrorMessage="Please select to date" Text="*" ValidationGroup="vldgrpDescriptions" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                </label>
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
                                                    <asp:CompareValidator ID="vldCmpDateFromTo" runat="server" ErrorMessage="To date can't be less than from date." ControlToCompare="txtFrom" CultureInvariantValues="true" ControlToValidate="txtTo" ValidationGroup="vldgrpDescriptions" Text="*" CssClass="text-danger" Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-md-6">
                                                <label for="txtDescription" class="control-label-mand col-sm-6">
                                                    Description
                                            <asp:RequiredFieldValidator ID="vldtxtDescription" runat="server" ControlToValidate="txtDescription" ErrorMessage="Please enter description" Text="*" ValidationGroup="vldgrpDescriptions" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                </label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtDescription" runat="server" Rows="5" TextMode="MultiLine" CssClass="form-control" />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="col-sm-12">
                                                    <label for="txtSource" class="control-label-mand col-sm-6">
                                                        Source
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSource" ErrorMessage="Please enter Source" Text="*" ValidationGroup="vldgrpDescriptions" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtSource" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 ">
                                                    <div class="col-sm-3">
                                                         <asp:LinkButton ID="btnSave" runat="server" CausesValidation="True" CommandName="Add" Text="Add" CssClass="btn btn-primary btn-sm pull-right" ValidationGroup="vldgrpDescriptions" />
                                                     </div>
                                               </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </InsertItemTemplate>
                            <EditItemTemplate>
                                <div class="panel panel-default">
                                    <div class="panel-heading">Update</div>
                                    <div class="panel-body">
                                        <div class=" form-group row">
                                            <div class="col-md-6">
                                                <label for="txtName" class="control-label-mand col-sm-6">
                                                    Name
                                                    <asp:RequiredFieldValidator ID="rtxtName" runat="server" ControlToValidate="txtName" ErrorMessage="Please enter description Name" Text="*" ValidationGroup="vldgrpDescriptions" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                </label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtName" runat="server" Rows="5" CssClass="form-control" Text='<%# Bind("Description_Name") %>' />
                                                </div>
                                            </div>

                                            <div class="col-md-6">
                                                <label for="txtDescriptionFor" class="control-label-mand col-sm-6">
                                                    Description For
                                                    <asp:RequiredFieldValidator ID="rtxtDescriptionFor" runat="server" ControlToValidate="txtDescriptionFor" ErrorMessage="Please enter description Name" Text="*" ValidationGroup="vldgrpDescriptions" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                </label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtDescriptionFor" runat="server" Rows="5" CssClass="form-control"  Text='<%# Bind("DescriptionFor") %>'/>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-md-6">
                                                <label for="ddlDescriptionType" class="control-label-mand col-sm-6">
                                                    Description Type
                                                <%--<asp:RequiredFieldValidator ID="rddlDescriptionType" runat="server" ControlToValidate="ddlDescriptionType" ErrorMessage="Please select description type" Text="*" ValidationGroup="vldgrpDescriptions" InitialValue="0" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
                                                </label>

                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlDescriptionType" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-6">
                                                <label for="ddlDescriptionSubType" class="control-label-mand col-sm-6">
                                                    Description  Sub Type
                                                <%--<asp:RequiredFieldValidator ID="rddlDescriptionSubType" runat="server" ControlToValidate="ddlDescriptionSubType" ErrorMessage="Please select description type" Text="*" ValidationGroup="vldgrpDescriptions" InitialValue="0" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
                                                </label>

                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlDescriptionSubType" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-md-6">
                                                <label class="control-label col-sm-6" for="txtFrom">From</label>
                                                <div class="col-sm-6">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" />
                                                        <span class="input-group-btn">
                                                            <button class="btn btn-default" type="button" id="iCalFrom">
                                                                <span class="glyphicon glyphicon-calendar"></span>
                                                            </button>
                                                        </span>
                                                        <cc1:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy" PopupButtonID="iCalFrom"></cc1:CalendarExtender>
                                                        <cc1:FilteredTextBoxExtender ID="axfte_txtFrom" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtFrom" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-6">
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
                                                    <asp:CompareValidator ID="vldCmpDateFromTo" runat="server" ErrorMessage="To date can't be less than from date." ControlToCompare="txtFrom" CultureInvariantValues="true" ControlToValidate="txtTo" ValidationGroup="vldgrpDescriptions" Text="*" CssClass="text-danger" Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-md-6">
                                                <label for="txtDescription" class="control-label-mand col-sm-6">
                                                    Description
                                                    <asp:RequiredFieldValidator ID="vldtxtDescription" runat="server" ControlToValidate="txtDescription" ErrorMessage="Please enter description" Text="*" ValidationGroup="vldgrpDescriptions" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                </label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtDescription" runat="server" Rows="5" TextMode="MultiLine" CssClass="form-control"  Text='<%# Bind("Source") %>' />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="col-sm-12">
                                                    <label for="txtSource" class="control-label-mand col-sm-6">
                                                        Source
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSource" ErrorMessage="Please enter Source" Text="*" ValidationGroup="vldgrpDescriptions" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtSource" runat="server" CssClass="form-control" Text='<%# Bind("Source") %>' />
                                                    </div>
                                                </div>
                                                 
                                                <div class="col-sm-12 ">
                                                    <div class="col-sm-3">
                                                      <asp:LinkButton ID="btnUpdate" runat="server" CausesValidation="True" CommandName="Modify" Text="Update" CssClass="btn btn-primary btn-sm pull-right" ValidationGroup="vldgrpDescriptions" />
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
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>


