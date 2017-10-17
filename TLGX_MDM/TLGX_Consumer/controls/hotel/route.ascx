<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="route.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.route" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:UpdatePanel ID="updPanRoute" runat="server">
    <ContentTemplate>
        <div class="container">
            <div id="dvMsg" runat="server" style="display: none;"></div>
        </div>
        <asp:FormView ID="frmRouote" runat="server" DataKeyNames="Accommodation_Route_Id" DefaultMode="Insert" OnItemCommand="frmRouote_ItemCommand">
            <HeaderTemplate>
                <div class="container">
                    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="vldgrpHowToReach" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                </div>
            </HeaderTemplate>
            <InsertItemTemplate>
                <div class="container">

                    <div class="panel panel-default">
                        <div class="panel-heading">New Route</div>
                        <div class="panel-body">

                            <div class="row">

                                <div class="col-lg-6">

                                    <div class="form-group">
                                        <label for="ddlFrom" class="control-label-mand col-sm-6">
                                            From
                                        <asp:RequiredFieldValidator ID="vldddlFrom" runat="server" ControlToValidate="ddlFrom" InitialValue="0" ErrorMessage="Please select from location" Text="*" ValidationGroup="vldgrpHowToReach" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>

                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlFrom" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="ddlModeOfTransport" class="control-label-mand col-sm-6">
                                            Mode of Transport
                                        <asp:RequiredFieldValidator ID="vldddlModeOfTransport" runat="server" ControlToValidate="ddlModeOfTransport" InitialValue="0" ErrorMessage="Please select mode of transport" Text="*" ValidationGroup="vldgrpHowToReach" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlModeOfTransport" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="ddlNameOfPlace" class="control-label-mand col-sm-6">
                                            Transport Type
                                        <asp:RequiredFieldValidator ID="vldddlNameOfPlace" runat="server" ControlToValidate="ddlTransportType" InitialValue="0" ErrorMessage="Please select transport type" Text="*" ValidationGroup="vldgrpHowToReach" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlTransportType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="txtDistanceFromProperty" class="control-label col-sm-6">Distance from Property</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtDistanceFromProperty" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="txtDescription" class="control-label-mand col-sm-6">
                                            Description
                                        <asp:RequiredFieldValidator ID="vldtxtDescription" runat="server" ControlToValidate="txtDescription" ErrorMessage="Please enter description" Text="*" ValidationGroup="vldgrpHowToReach" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />
                                        </div>
                                    </div>

                                </div>

                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label for="ddlNameOfPlace" class="control-label col-sm-6">Name of Place</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlNameOfPlace" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="txtApproximateDuration" class="control-label col-sm-6">Approximate Duration</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtApproximateDuration" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="txtValidFrom" class="control-label-mand col-sm-6">
                                            Valid From
                                        <asp:RequiredFieldValidator ID="vldtxtFrom" runat="server" ControlToValidate="txtValidFrom" ErrorMessage="Please select valid from date" Text="*" ValidationGroup="vldgrpHowToReach" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtValidFrom" runat="server" CssClass="form-control" />
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button" id="iRouteCalFrom">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </button>
                                                </span>

                                            </div>

                                            <cc1:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtValidFrom" Format="dd/MM/yyyy" PopupButtonID="iRouteCalFrom"></cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="axfte_txtValidFrom" TargetControlID="txtValidFrom" runat="server" FilterType="Numbers, Custom" ValidChars="/" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="txtValidTo" class="control-label-mand col-sm-6">
                                            Valid To
                                        <asp:RequiredFieldValidator ID="vldtxtValidTo" runat="server" ControlToValidate="txtValidTo" ErrorMessage="Please select valid to date" Text="*" ValidationGroup="vldgrpHowToReach" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtValidTo" runat="server" CssClass="form-control" />
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button" id="iRouteCalTo">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </button>
                                                </span>

                                            </div>
                                            <cc1:CalendarExtender ID="calValidTo" runat="server" TargetControlID="txtValidTo" Format="dd/MM/yyyy" PopupButtonID="iRouteCalTo"></cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="axfte_txtValidTo" TargetControlID="txtValidTo" runat="server" FilterType="Numbers, Custom" ValidChars="/" />
                                            <asp:CompareValidator ID="vldCmpDateFromTo" runat="server" CultureInvariantValues="true" ErrorMessage="To date can't be less than from date." ControlToCompare="txtValidFrom" ControlToValidate="txtValidTo" ValidationGroup="vldgrpHowToReach" Text="*" CssClass="text-danger" Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator>

                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="txtDrivingDirection" class="control-label col-sm-6">Driving Direction</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtDrivingDirection" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="btnAdd" runat="server" CausesValidation="True" CommandName="Add" Text="Add New Route" CssClass="btn btn-primary btn-sm" ValidationGroup="vldgrpHowToReach" />
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
                        <div class="panel-heading">Update Route</div>
                        <div class="panel-body">

                            <div class="row">
                                <div class="col-lg-6">

                                    <div class="form-group">
                                        <label for="ddlFrom" class="control-label-mand col-sm-6">
                                            From
                                        <asp:RequiredFieldValidator ID="vldddlFrom" runat="server" ControlToValidate="ddlFrom" InitialValue="0" ErrorMessage="Please select from location" Text="*" ValidationGroup="vldgrpHowToReach" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlFrom" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="ddlModeOfTransport" class="control-label-mand col-sm-6">
                                            Mode of Transport
                                        <asp:RequiredFieldValidator ID="vldddlModeOfTransport" runat="server" ControlToValidate="ddlModeOfTransport" InitialValue="0" ErrorMessage="Please select mode of transport" Text="*" ValidationGroup="vldgrpHowToReach" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlModeOfTransport" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="ddlNameOfPlace" class="control-label-mand col-sm-6">
                                            Transport Type
                                        <asp:RequiredFieldValidator ID="vldddlNameOfPlace" runat="server" ControlToValidate="ddlTransportType" InitialValue="0" ErrorMessage="Please select transport type" Text="*" ValidationGroup="vldgrpHowToReach" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlTransportType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="txtDistanceFromProperty" class="control-label col-sm-6">Distance from Property</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtDistanceFromProperty" runat="server" CssClass="form-control" Text='<%# Bind("DistanceFromProperty") %>' />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="txtDescription" class="control-label-mand col-sm-6">
                                            Description
                                        <asp:RequiredFieldValidator ID="vldtxtDescription" runat="server" ControlToValidate="txtDescription" ErrorMessage="Please enter description" Text="*" ValidationGroup="vldgrpHowToReach" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Text='<%# Bind("Description") %>' />
                                        </div>
                                    </div>

                                </div>

                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label for="ddlNameOfPlace" class="control-label col-sm-6">Name of Place</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlNameOfPlace" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="txtDistanceFromProperty" class="control-label col-sm-6">Approximate Duration</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtApproximateDuration" runat="server" CssClass="form-control" Text='<%# Bind("ApproximateDuration") %>' />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="txtValidFrom" class="control-label-mand col-sm-6">
                                            Valid From
                                        <asp:RequiredFieldValidator ID="vldtxtFrom" runat="server" ControlToValidate="txtValidFrom" ErrorMessage="Please select valid from date" Text="*" ValidationGroup="vldgrpHowToReach" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtValidFrom" runat="server" CssClass="form-control" Text='<%# Bind("ValidFrom", "{0:dd/MM/yyyy}") %>' />
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button" id="iRouteCalFrom">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </button>
                                                </span>

                                            </div>
                                            <cc1:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtValidFrom" Format="dd/MM/yyyy" PopupButtonID="iRouteCalFrom"></cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="axfte_txtValidFrom" TargetControlID="txtValidFrom" runat="server" FilterType="Numbers, Custom" ValidChars="/" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="txtValidTo" class="control-label-mand col-sm-6">
                                            Valid To
                                        <asp:RequiredFieldValidator ID="vldtxtValidTo" runat="server" ControlToValidate="txtValidTo" ErrorMessage="Please select valid to date" Text="*" ValidationGroup="vldgrpHowToReach" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtValidTo" runat="server" CssClass="form-control" Text='<%# Bind("ValidTo", "{0:dd/MM/yyyy}") %>' />
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button" id="iRouteCalTo">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </button>
                                                </span>

                                            </div>
                                            <cc1:CalendarExtender ID="calValidTo" runat="server" TargetControlID="txtValidTo" Format="dd/MM/yyyy" PopupButtonID="iRouteCalTo"></cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="axfte_txtValidTo" TargetControlID="txtValidTo" runat="server" FilterType="Numbers, Custom" ValidChars="/" />
                                            <asp:CompareValidator ID="vldCmpDateFromTo" runat="server" CultureInvariantValues="true" ErrorMessage="To date can't be less than from date." ControlToCompare="txtValidFrom" ControlToValidate="txtValidTo" ValidationGroup="vldgrpHowToReach" Text="*" CssClass="text-danger" Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="txtDrivingDirection" class="control-label col-sm-6">Driving Direction</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtDrivingDirection" runat="server" CssClass="form-control" Text='<%# Bind("DrivingDirection") %>' />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="btnAdd" runat="server" CausesValidation="True" CommandName="Save" Text="Update" CssClass="btn btn-primary btn-sm" ValidationGroup="vldgrpHowToReach" />
                                        </div>
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>

                </div>


            </EditItemTemplate>

        </asp:FormView>

        <div class="panel panel-default">
            <div class="panel-heading">How to Reach</div>
            <div class="panel-body">

                <asp:GridView ID="grdRoutes" runat="server" AutoGenerateColumns="False" DataKeyNames="Accommodation_Route_Id" CssClass="table table-hover table-striped" EmptyDataText="No How to Reach defined" OnRowCommand="grdRoutes_RowCommand" OnRowDataBound="grdRoutes_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="FromPlace" HeaderText="From" SortExpression="FromPlace" />
                        <asp:BoundField DataField="NameOfPlace" HeaderText="Name Of Place" SortExpression="NameOfPlace" />
                        <asp:BoundField DataField="ModeOfTransport" HeaderText="Mode Of Transport" SortExpression="ModeOfTransport" />
                        <asp:BoundField DataField="TransportType" HeaderText="Transport Type" SortExpression="TransportType" />
                        <asp:BoundField DataField="DistanceFromProperty" HeaderText="Distance from Property" SortExpression="DistanceFromProperty" />
                        <%--<asp:BoundField DataField="DistanceUnit" HeaderText="Unit" SortExpression="DistanceUnit" />--%>
                        <asp:BoundField DataField="ApproximateDuration" HeaderText="Approximate Duration" SortExpression="ApproximateDuration" />
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                        <asp:BoundField DataField="DrivingDirection" HeaderText="Driving Direction" SortExpression="DrivingDirection" />
                        <asp:BoundField DataField="ValidFrom" HeaderText="Valid From" SortExpression="ValidFrom" />
                        <asp:BoundField DataField="ValidTo" HeaderText="Valid To" SortExpression="ValidTo" />

                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                    Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Accommodation_Route_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                                    CssClass="btn btn-default" CommandArgument='<%# Bind("Accommodation_Route_Id") %>'>
                                         <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                                        <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

            </div>
        </div>

    </ContentTemplate>

</asp:UpdatePanel>
