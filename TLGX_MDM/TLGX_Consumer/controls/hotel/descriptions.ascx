<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="descriptions.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.descriptions" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:UpdatePanel ID="updPanDescriptions" runat="server">
    <ContentTemplate>
        <div id="dvMsg" runat="server" style="display: none;"></div>

        <div class="container">
            <div class="form-group">
                <div class="col-sm-12">
                    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="vldgrpDescriptions" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                </div>
            </div>
        </div>

        <asp:FormView ID="frmDescriptionDetail" runat="server" DataKeyNames="Accommodation_Description_Id" DefaultMode="Insert" OnItemCommand="frmDescriptionDetail_ItemCommand">
            <InsertItemTemplate>

                <div class="container">
                    <div class="panel panel-default">
                        <div class="panel-heading">Add</div>
                        <div class="panel-body">

                            <div class="row">

                                <div class="col-lg-6">

                                    <div class="form-group">
                                        <label for="txtDescription" class="control-label-mand col-sm-6">
                                            Description
                                            <asp:RequiredFieldValidator ID="vldtxtDescription" runat="server" ControlToValidate="txtDescription" ErrorMessage="Please enter description" Text="*" ValidationGroup="vldgrpDescriptions" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtDescription" runat="server" Rows="5" TextMode="MultiLine" CssClass="form-control" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="btnSave" runat="server" CausesValidation="True" CommandName="Add" Text="Add Description" CssClass="btn btn-primary btn-sm" ValidationGroup="vldgrpDescriptions" />
                                        </div>
                                    </div>

                                </div>

                                <div class="col-lg-6">


                                    <div class="form-group">

                                        <label for="ddlDescriptionType" class="control-label-mand col-sm-6">
                                            Description Type
                                        <asp:RequiredFieldValidator ID="vldddlDescriptionType" runat="server" ControlToValidate="ddlDescriptionType" ErrorMessage="Please select description type" Text="*" ValidationGroup="vldgrpDescriptions" InitialValue="0" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>

                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlDescriptionType" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                    </div>

                                    <div class="form-group">

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
                                            </div>
                                            <cc1:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy" PopupButtonID="iCalFrom"></cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="axfte_txtFrom" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtFrom" />
                                        </div>
                                    </div>

                                    <div class="form-group">

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

                            </div>

                        </div>

                    </div>


                </div>

            </InsertItemTemplate>

            <EditItemTemplate>


                <div class="container">
                    <div class="panel panel-default">
                        <div class="panel-heading">Modify Description</div>
                        <div class="panel-body">

                            <div class="row">

                                <div class="col-lg-6">

                                    <div class="form-group">
                                        <label for="txtDescription" class="control-label-mand col-sm-6">
                                            Description
                                            <asp:RequiredFieldValidator ID="vldtxtDescription" runat="server" ControlToValidate="txtDescription" ErrorMessage="Please enter description" Text="*" ValidationGroup="vldgrpDescriptions" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtDescription" runat="server" Rows="5" TextMode="MultiLine" CssClass="form-control" Text='<%# Bind("Description") %>' />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="btn" runat="server" CausesValidation="True" CommandName="Modify" Text="Modify Description" CssClass="btn btn-primary btn-sm" ValidationGroup="vldgrpDescriptions" />
                                        </div>
                                    </div>

                                </div>

                                <div class="col-lg-6">


                                    <div class="form-group">

                                        <label for="ddlDescriptionType" class="control-label-mand col-sm-6">
                                            Description Type
                                        <asp:RequiredFieldValidator ID="vldddlDescriptionType" runat="server" ControlToValidate="ddlDescriptionType" ErrorMessage="Please select description type" Text="*" ValidationGroup="vldgrpDescriptions" InitialValue="0" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>

                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlDescriptionType" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                    </div>

                                    <div class="form-group">

                                        <label for="txtFrom" class="control-label-mand col-sm-6">
                                            From
                                        <asp:RequiredFieldValidator ID="vldtxtFrom" runat="server" ControlToValidate="txtFrom" ErrorMessage="Please select from date" Text="*" ValidationGroup="vldgrpDescriptions" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" Text='<%# Bind("FromDate", "{0:dd/MM/yyyy}") %>' />
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button" id="iCalFrom">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </button>
                                                </span>
                                            </div>
                                            <cc1:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy" PopupButtonID="iCalFrom"></cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="axfte_txtFrom" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtFrom" />
                                        </div>
                                    </div>

                                    <div class="form-group">

                                        <label for="txtTo" class="control-label-mand col-sm-6">
                                            To
                                        <asp:RequiredFieldValidator ID="vldtxtTo" runat="server" ControlToValidate="txtTo" ErrorMessage="Please select to date" Text="*" ValidationGroup="vldgrpDescriptions" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" Text='<%# Bind("ToDate", "{0:dd/MM/yyyy}") %>' />
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

                            </div>

                        </div>


                    </div>
                </div>

            </EditItemTemplate>
        </asp:FormView>

        <div class="container">
            <div class="panel panel-default">
                <div class="panel-heading">Descriptions</div>
                <br />
                <div class="panel-body">
                    <asp:GridView ID="grdDescriptionList" runat="server" AutoGenerateColumns="False" DataKeyNames="Accommodation_Description_Id" EmptyDataText="No Descriptions Found" CssClass="table table-hover table-striped" OnRowCommand="grdDescriptionList_RowCommand" OnRowDataBound="grdDescriptionList_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="DescriptionType" HeaderText="Type" SortExpression="DescriptionType" />
                            <asp:BoundField DataField="ToDate" HeaderText="ToDate" SortExpression="ToDate" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="FromDate" HeaderText="FromDate" SortExpression="FromDate" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />

                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default" Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Accommodation_Description_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>' CssClass="btn btn-default" CommandArgument='<%# Bind("Accommodation_Description_Id") %>'>
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
