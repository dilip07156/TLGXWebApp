<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityDescription.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.ActivityDescription" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script>
    function showDescriptionModal() {
        $("#moDescription").modal('show');
    }
    function closeDescriptionModal() {
        $("#moDescription").modal('hide');
    }
</script>
<script src="../../../Scripts/bootbox.min.js"></script>
<asp:UpdatePanel ID="updMedia" runat="server">
    <ContentTemplate>

        <h4 class="panel-title pull-left">Description Details (Total Count:
                            <asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label>)</h4>
        <asp:Button CssClass="pull-right btn btn-primary" runat="server" ID="btnNewUpload" OnClick="btnNewUpload_Click" Text="Add New" OnClientClick="showDescriptionModal()" />

        <div class="col-lg-3 pull-right">
            <div class="form-group pull-right">
                <div class="input-group" runat="server" id="divDropdownForEntries">
                    <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                    <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged">
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div id="dvMsg" runat="server" style="display: none;"></div>
        <asp:GridView ID="gvDescriptionSearch" runat="server" AllowPaging="True" AllowCustomPaging="true"
            EmptyDataText="No Data Found" CssClass="table table-hover table-striped"
            AutoGenerateColumns="false" OnPageIndexChanging="gvDescriptionSearch_PageIndexChanging"
            OnRowCommand="gvDescriptionSearch_RowCommand" DataKeyNames="Activity_Description_Id" OnRowDataBound="gvDescriptionSearch_RowDataBound">
            <Columns>

                <asp:BoundField HeaderText="Type" DataField="DescriptionType" />

                <asp:BoundField HeaderText="Description" DataField="Description" />

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

    </ContentTemplate>
</asp:UpdatePanel>

<div class="modal fade" id="moDescription" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">

            <div class="modal-header">
                <h4 class="modal-title">Add / Update Descriptions</h4>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel ID="upDescriptionAddEdit" runat="server">
                    <ContentTemplate>

                        <asp:HiddenField ID="hdnDescId" runat="server" Value="" />

                        <div class="form-group row">
                            <div class="col-md-12">
                                <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="vldgrpDescriptions" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-md-6">
                                <label for="ddlDescriptionType" class="control-label-mand col-sm-6">
                                    Description Type
                                        <asp:RequiredFieldValidator ID="rddlDescriptionType" runat="server" ControlToValidate="ddlDescriptionType" ErrorMessage="Please select description type" Text="*" ValidationGroup="vldgrpDescriptions" InitialValue="0" CssClass="text-danger"></asp:RequiredFieldValidator>
                                </label>

                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlDescriptionType" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-md-12">
                                <label for="txtDescription" class="control-label-mand col-sm-3">
                                    Description
                                            <asp:RequiredFieldValidator ID="vldtxtDescription" runat="server" ControlToValidate="txtDescription" ErrorMessage="Please enter description" Text="*" ValidationGroup="vldgrpDescriptions" CssClass="text-danger"></asp:RequiredFieldValidator>
                                </label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtDescription" runat="server" Rows="5" TextMode="MultiLine" CssClass="form-control" />
                                </div>
                            </div>

                        </div>

                        <div class="form-group row">
                            <div class="col-md-12">
                                <asp:LinkButton ID="btnSave" runat="server" CausesValidation="True" CommandName="Add" Text="Save" CssClass="btn btn-primary btn-sm pull-right" ValidationGroup="vldgrpDescriptions" OnClick="btnSave_Click" />
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


