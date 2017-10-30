<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityStatus.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.ActivityStatus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="updPanContacts" runat="server" UpdateMode="Conditional">
    <ContentTemplate>

        <div class="form-group">
            <div id="dvMsgStatus" runat="server" style="display: none;"></div>
        </div>

        <asp:FormView ID="frmStatusDetails" runat="server" DataKeyNames="Activity_Status_Id" DefaultMode="Insert" OnItemCommand="frmStatusDetails_ItemCommand">

            <HeaderTemplate>
                <div class="form-group">
                    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="ActivityStatus" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                </div>
            </HeaderTemplate>

            <InsertItemTemplate>
                <div class="form-group row">
                    <div class="col-md-12">
                        <form id="uriForm" class="form-horizontal">

                            <div class="form-group row">
                                <div class="col-md-6">
                                    <label class="control-label col-sm-2" for="txtStatus">
                                        Status
                                    </label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label class="control-label col-sm-3" for="txtMarket">
                                        Company market
                                    </label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtMarket" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-md-6">
                                    <label class="control-label col-sm-2" for="txtFrom">From</label>
                                    <div class="col-sm-10">
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
                                    <label class="control-label col-sm-3" for="txtTo">To</label>
                                    <div class="col-sm-9">
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
                                        <asp:CompareValidator ID="vldCmpDateFromTo" runat="server" ErrorMessage="To date can't be less than from date." ControlToCompare="txtFrom" CultureInvariantValues="true" ControlToValidate="txtTo" ValidationGroup="ActivityStatus" Text="*" CssClass="text-danger" Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-md-6">
                                    <label class="control-label col-sm-3" for="txtReason">
                                        Deactivation Reason
                                    </label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" TextMode="multiline" Columns="50" Rows="5"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-2 ">
                                        <asp:LinkButton ID="btnAdd" runat="server" CommandName="Add" Text="Add" CssClass="btn btn-primary btn-sm" ValidationGroup="ActivityStatus" CausesValidation="true" />
                                     </div>
                                </div>
                            </div>
                            <div class="form-group row" style="display: none;">
                                <label class="control-label col-sm-2" for="txtEmail">Legacy Product ID</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtLegacyProductId" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </form>
                    </div>

                </div>

            </InsertItemTemplate>

            <EditItemTemplate>
                <div class="form-group row">
                    <div class="col-md-12">
                        <form id="uriForm" class="form-horizontal">

                            <div class="form-group row">
                                <div class="col-md-6">
                                    <label class="control-label col-sm-2" for="txtStatus">
                                        Status
                                    </label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label class="control-label col-sm-2" for="txtMarket">
                                        Company market
                                    </label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="txtMarket" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-md-6">
                                    <label class="control-label col-sm-2" for="txtFrom">From</label>
                                    <div class="col-sm-10">
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
                                    <label class="control-label col-sm-3" for="txtTo">To</label>
                                    <div class="col-sm-9">
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
                                        <asp:CompareValidator ID="vldCmpDateFromTo" runat="server" ErrorMessage="To date can't be less than from date." ControlToCompare="txtFrom" CultureInvariantValues="true" ControlToValidate="txtTo" ValidationGroup="ActivityStatus" Text="*" CssClass="text-danger" Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator>
                                    </div>
                                </div>
                            </div>
                          
                            <div class="form-group row">
                                <div class="col-md-6">
                                    <label class="control-label col-sm-3" for="txtReason">
                                        Deactivation Reason
                                    </label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" TextMode="multiline" Columns="50" Rows="5"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-2 ">
                                        <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Edit" Text="Update" CssClass="btn btn-primary btn-sm" ValidationGroup="ActivityStatus" CausesValidation="true" />
                                     </div>
                                </div>
                            </div>

                            <div class="form-group row" style="display: none;">
                                <label class="control-label col-sm-2" for="txtEmail">Legacy Product ID</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtLegacyProductId" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </EditItemTemplate>

        </asp:FormView>

        <br />

        <asp:GridView ID="grdStatusDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="Activity_Status_Id" CssClass="table table-hover table-striped" EmptyDataText="No Status details Found" OnRowCommand="grdStatusDetails_RowCommand" OnRowDataBound="grdStatusDetails_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Telephone" />
                <asp:BoundField DataField="CompanyMarket" HeaderText="Company Market" SortExpression="CompanyMarket" />
                <asp:BoundField DataField="DeactivationReason" HeaderText="Deactivation Reason" SortExpression="DeactivationReason" />
                <asp:BoundField DataField="From" HeaderText="From " SortExpression="From" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="To" HeaderText="To " SortExpression="To" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="Legacy_Product_ID" HeaderText="Product ID" SortExpression="Legacy_Product_ID" Visible="false" />
                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                            Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Activity_Status_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                            CssClass="btn btn-default" CommandArgument='<%# Bind("Activity_Status_Id") %>'>
                                         <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'></span>
                                        <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div>
            <asp:Button ID="btnAddFormView" runat="server" OnClick="btnAddFormView_Click" Text="Add New" CssClass="btn btn-primary btn-sm pull-right" />
        </div>
    </ContentTemplate>

</asp:UpdatePanel>
