<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="status.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.status" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:UpdatePanel runat="server" ID="updPanStatus">
    <ContentTemplate>
        <div class="container">
            <div id="dvMsg" runat="server" style="display: none;"></div>
        </div>
        <asp:FormView ID="frmAccommodationStatus" DataKeyNames="Accommodation_Status_Id" runat="server" DefaultMode="Insert" OnItemCommand="frmAccommodationStatus_ItemCommand">

            <HeaderTemplate>
                <div class="container">
                    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="vldgrpStatus" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                </div>
            </HeaderTemplate>

            <InsertItemTemplate>

                <div class="container">

                    <div class="panel panel-default">
                        <div class="panel-heading">Add Status</div>
                        <div class="panel-body">

                            <div class="row">

                                <div class="col-lg-4">

                                    <div class="form-group">
                                        <label class="control-label-mand col-sm-4" for="ddlCompanyMarket">
                                            Market
                                    <asp:RequiredFieldValidator ID="vldCompanyMarket" runat="server" ControlToValidate="ddlCompanyMarket" ErrorMessage="Please select market" Text="*" ValidationGroup="vldgrpStatus" InitialValue="0" CssClass="text-danger"></asp:RequiredFieldValidator></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlCompanyMarket" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label-mand col-sm-4" for="ddlStatus">
                                            Status
                                    <asp:RequiredFieldValidator ID="vldStatus" runat="server" ControlToValidate="ddlStatus" ErrorMessage="Please select status" Text="*" ValidationGroup="vldgrpStatus" InitialValue="0" CssClass="text-danger"></asp:RequiredFieldValidator></label>

                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-8">
                                            <asp:LinkButton ID="btnAdd" runat="server" CommandName="Add" Text="Add Status" CssClass="btn btn-primary btn-sm" ValidationGroup="vldgrpStatus" CausesValidation="true" />
                                        </div>
                                    </div>

                                </div>


                                <div class="col-lg-4">

                                    <div class="form-group">
                                        <label class="control-label-mand col-sm-4" for="txtFrom">
                                            From
                                    <asp:RequiredFieldValidator ID="vldtxtFrom" runat="server" ControlToValidate="txtFrom" ErrorMessage="Please select from date" Text="*" ValidationGroup="vldgrpStatus" CssClass="text-danger"></asp:RequiredFieldValidator></label>


                                        <div class="col-sm-8">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" />
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button" id="iStatusFrom">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </button>
                                                </span>
                                            </div>
                                            <cc1:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy" PopupButtonID="iStatusFrom"></cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="axfte_txtFrom" TargetControlID="txtFrom" runat="server" FilterType="Numbers, Custom" ValidChars="/" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label-mand col-sm-4" for="txtTo">
                                            To
                                    <asp:RequiredFieldValidator ID="vldTextTo" runat="server" ControlToValidate="txtTo" ErrorMessage="Please select to date" Text="*" ValidationGroup="vldgrpStatus" CssClass="text-danger"></asp:RequiredFieldValidator></label>


                                        <div class="col-sm-8">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" />
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button" id="iStatusTo">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </button>
                                                </span>
                                            </div>
                                            <cc1:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy" PopupButtonID="iStatusTo"></cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="axfte_txtTo" TargetControlID="txtTo" runat="server" FilterType="Numbers, Custom" ValidChars="/" />
                                        </div>
                                    </div>

                                </div>


                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="control-label-mand col-sm-4" for="txtDeactivationReason">
                                            Reason
                                    <asp:RequiredFieldValidator ID="vldReason" runat="server" ControlToValidate="txtDeactivationReason" ErrorMessage="Please enter reason" Text="*" ValidationGroup="vldgrpStatus" CssClass="text-danger"></asp:RequiredFieldValidator></label>

                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtDeactivationReason" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />
                                        </div>
                                    </div>
                                    <br />
                                </div>


                            </div>


                        </div>
                    </div>

                </div>
            </InsertItemTemplate>

            <EditItemTemplate>
                <div class="container">

                    <div class="panel panel-default">
                        <div class="panel-heading">Update Status</div>
                        <div class="panel-body">


                            <div class="row">

                                <div class="col-lg-4">

                                    <div class="form-group">
                                        <label class="control-label-mand col-sm-4" for="ddlCompanyMarket">
                                            Market
                                    <asp:RequiredFieldValidator ID="vldCompanyMarket" runat="server" ControlToValidate="ddlCompanyMarket" ErrorMessage="Please select market" Text="*" ValidationGroup="vldgrpStatus" InitialValue="0" CssClass="text-danger"></asp:RequiredFieldValidator></label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlCompanyMarket" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label-mand col-sm-4" for="ddlStatus">
                                            Status
                                    <asp:RequiredFieldValidator ID="vldStatus" runat="server" ControlToValidate="ddlStatus" ErrorMessage="Please select status" Text="*" ValidationGroup="vldgrpStatus" InitialValue="0" CssClass="text-danger"></asp:RequiredFieldValidator></label>

                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-8">
                                            <asp:LinkButton ID="btnUpdateDescription" runat="server" CommandName="Save" Text="Update" CssClass="btn btn-primary btn-sm" ValidationGroup="vldgrpStatus" />
                                        </div>
                                    </div>

                                </div>


                                <div class="col-lg-4">

                                    <div class="form-group">
                                        <label class="control-label-mand col-sm-4" for="txtFrom">
                                            From
                                    <asp:RequiredFieldValidator ID="vldtxtFrom" runat="server" ControlToValidate="txtFrom" ErrorMessage="Please select from date" Text="*" ValidationGroup="vldgrpStatus" CssClass="text-danger"></asp:RequiredFieldValidator></label>


                                        <div class="col-sm-8">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" Text='<%# Bind("From", "{0:dd/MM/yyyy}") %>' ValidationGroup="vldgrpStatus" />
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button" id="iStatusFrom">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </button>
                                                </span>
                                            </div>
                                            <cc1:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy" PopupButtonID="iStatusFrom"></cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="axfte_txtFrom" TargetControlID="txtFrom" runat="server" FilterType="Numbers, Custom" ValidChars="/" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label-mand col-sm-4" for="txtTo">
                                            To
                                    <asp:RequiredFieldValidator ID="vldTextTo" runat="server" ControlToValidate="txtTo" ErrorMessage="Please select to date" Text="*" ValidationGroup="vldgrpStatus" CssClass="text-danger"></asp:RequiredFieldValidator></label>


                                        <div class="col-sm-8">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" Text='<%# Bind("To", "{0:dd/MM/yyyy}") %>' ValidationGroup="vldgrpStatus" />
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button" id="iStatusTo">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </button>
                                                </span>
                                            </div>
                                            <cc1:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy" PopupButtonID="iStatusTo"></cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="axfte_txtTo" TargetControlID="txtTo" runat="server" FilterType="Numbers, Custom" ValidChars="/" />
                                        </div>
                                    </div>

                                </div>


                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="control-label-mand col-sm-4" for="txtDeactivationReason">
                                            Reason
                                    <asp:RequiredFieldValidator ID="vldReason" runat="server" ControlToValidate="txtDeactivationReason" ErrorMessage="Please enter reason" Text="*" ValidationGroup="vldgrpStatus" CssClass="text-danger"></asp:RequiredFieldValidator></label>

                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtDeactivationReason" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Text='<%# Bind("DeactivationReason") %>' />
                                        </div>
                                    </div>
                                    <br />
                                </div>


                            </div>


                        </div>
                    </div>

                </div>
            </EditItemTemplate>


        </asp:FormView>

        <br />

        <div class="container">

            <div class="panel panel-default">
                <div class="panel-heading">Existing Status</div>
                <div class="panel-body">


                    <asp:GridView ID="grdStatusList" runat="server" DataKeyNames="Accommodation_Status_Id" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-hover table-striped" EmptyDataText="No Deactivations set" OnRowCommand="grdStatusList_RowCommand" OnRowDataBound="grdStatusList_RowDataBound">

                        <Columns>
                            <asp:BoundField DataField="CompanyMarket" HeaderText="Company Market" SortExpression="CompanyMarket" />
                            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                            <asp:BoundField DataField="From" HeaderText="From" SortExpression="From" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="To" HeaderText="To" SortExpression="To" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="DeactivationReason" HeaderText="Reason" SortExpression="DeactivationReason" />
                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                        Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Accommodation_Status_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                                        CssClass="btn btn-default" CommandArgument='<%# Bind("Accommodation_Status_Id") %>'>
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
