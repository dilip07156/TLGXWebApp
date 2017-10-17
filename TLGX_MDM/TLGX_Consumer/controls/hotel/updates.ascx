<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="updates.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.updates" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:UpdatePanel ID="updPanHOtelUpdate" runat="server">

    <ContentTemplate>

        <div class="container">

            <div id="dvMsg" runat="server" style="display: none;"></div>

        </div>

        <asp:FormView ID="frmHotelUpdate" runat="server" DataKeyNames="Accommodation_HotelUpdates_Id" DefaultMode="Insert" OnItemCommand="frmHotelUpdate_ItemCommand">

            <HeaderTemplate>
                <div class="container">
                    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="vldgrpHotelUpdates" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                </div>
            </HeaderTemplate>

            <InsertItemTemplate>

                <div class="container">
                    <div class="panel panel-default">
                        <div class="panel-heading">Add Hotel Update</div>
                        <div class="panel-body">

                            <div class="row">

                                <div class="col-lg-6">

                                    <div class="form-group">
                                        <label for="txtDescription" class="control-label-mand col-sm-6">
                                            Description
                                            <asp:RequiredFieldValidator ID="vldtxtDescription" runat="server" ControlToValidate="txtDescription" ErrorMessage="Please enter description" Text="*" ValidationGroup="vldgrpHotelUpdates" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtDescription" runat="server" Rows="5" TextMode="MultiLine" CssClass="form-control" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="btnSave" runat="server" CausesValidation="True" CommandName="Add" Text="Add Hotel Update" CssClass="btn btn-primary btn-sm" ValidationGroup="vldgrpHotelUpdates" />
                                        </div>
                                    </div>

                                </div>

                                <div class="col-lg-6">

                                    <div class="form-group">
                                        <label for="ddlDescriptionType" class="control-label-mand col-sm-6">
                                            Description Source
                                            <asp:RequiredFieldValidator ID="vldUpdateSource" runat="server" ControlToValidate="ddlHotelUpdateSource" ErrorMessage="Please select description source" Text="*" ValidationGroup="vldgrpHotelUpdates" InitialValue="0" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlHotelUpdateSource" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="txtFrom" class="control-label-mand col-sm-6">
                                            From
                                            <asp:RequiredFieldValidator ID="vldtxtFrom" runat="server" ControlToValidate="txtFrom" ErrorMessage="Please select from date" Text="*" ValidationGroup="vldgrpHotelUpdates" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" />
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button" id="iFrom">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </button>
                                                </span>

                                            </div>
                                            <cc1:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy" PopupButtonID="iFrom"></cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="axfte_txtFrom" TargetControlID="txtFrom" runat="server" FilterType="Numbers, Custom" ValidChars="/" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtTo" class="control-label-mand col-sm-6">
                                            To
                                            <asp:RequiredFieldValidator ID="vldtxtTo" runat="server" ControlToValidate="txtTo" ErrorMessage="Please select to date" Text="*" ValidationGroup="vldgrpHotelUpdates" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" />
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button" id="iTo">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </button>
                                                </span>

                                            </div>
                                            <cc1:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy" PopupButtonID="iTo"></cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="axfte_txtTo" TargetControlID="txtTo" runat="server" FilterType="Numbers, Custom" ValidChars="/" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="chkIsInternal" class="control-label col-sm-6">
                                            Is Internal Update
                                        </label>
                                        <div class="col-sm-6">
                                            <div class="input-group">
                                                <asp:CheckBox ID="chkIsInternal" runat="server" />
                                            </div>
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
                        <div class="panel-heading">Add Hotel Update</div>
                        <div class="panel-body">

                            <div class="row">

                                <div class="col-lg-6">

                                    <div class="form-group">
                                        <label for="txtDescription" class="control-label-mand col-sm-6">
                                            Description
                                    <asp:RequiredFieldValidator ID="vldtxtDescription" runat="server" ControlToValidate="txtDescription" ErrorMessage="Please enter description" Text="*" ValidationGroup="vldgrpHotelUpdates" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtDescription" runat="server" Rows="5" TextMode="MultiLine" CssClass="form-control" Text='<%# Bind("Description") %>' />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="btnSave" runat="server" CausesValidation="True" CommandName="Modify" Text="Update" CssClass="btn btn-primary btn-sm" ValidationGroup="vldgrpHotelUpdates" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-6">

                                    <div class="form-group">
                                        <label for="ddlDescriptionType" class="control-label-mand col-sm-6">
                                            Description Source
                                    <asp:RequiredFieldValidator ID="vldUpdateSource" runat="server" ControlToValidate="ddlHotelUpdateSource" ErrorMessage="Please select description source" Text="*" ValidationGroup="vldgrpHotelUpdates" InitialValue="0" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlHotelUpdateSource" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="txtFrom" class="control-label-mand col-sm-6">
                                            From
                                    <asp:RequiredFieldValidator ID="vldtxtFrom" runat="server" ControlToValidate="txtFrom" ErrorMessage="Please select from date" Text="*" ValidationGroup="vldgrpHotelUpdates" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" Text='<%# Bind("FromDate", "{0:dd/MM/yyyy}") %>' />
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button" id="iFrom">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </button>
                                                </span>
                                            </div>
                                            <cc1:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy" PopupButtonID="iFrom"></cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="axfte_txtFrom" TargetControlID="txtFrom" runat="server" FilterType="Numbers, Custom" ValidChars="/" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtTo" class="control-label-mand col-sm-6">
                                            To
                                    <asp:RequiredFieldValidator ID="vldtxtTo" runat="server" ControlToValidate="txtTo" ErrorMessage="Please select to date" Text="*" ValidationGroup="vldgrpHotelUpdates" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" Text='<%# Bind("FromDate", "{0:dd/MM/yyyy}") %>' />
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button" id="iTo">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </button>
                                                </span>
                                            </div>
                                            <cc1:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy" PopupButtonID="iTo"></cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="axfte_txtTo" TargetControlID="txtTo" runat="server" FilterType="Numbers, Custom" ValidChars="/" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="chkIsInternal" class="control-label col-sm-6">
                                            Is Internal Update
                                        </label>
                                        <div class="col-sm-6">
                                            <div class="input-group">
                                                <asp:CheckBox ID="chkIsInternal" runat="server" />
                                            </div>
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

            <asp:GridView ID="grdHotelupdates" runat="server" DataKeyNames="Accommodation_HotelUpdates_Id" CssClass="table table-hover table-striped" AutoGenerateColumns="false"
                EmptyDataText="There are no Hotel Updates for this property" OnRowCommand="grdHotelupdates_RowCommand" OnRowDataBound="grdHotelupdates_RowDataBound">
                <Columns>

                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="FromDate" HeaderText="From" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="ToDate" HeaderText="To" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Source" HeaderText="Source" />
                    <asp:BoundField DataField="IsInternal" HeaderText="Is Internal" SortExpression="IsInternal" />
                    <asp:TemplateField ShowHeader="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Accommodation_HotelUpdates_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ShowHeader="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                                CssClass="btn btn-default" CommandArgument='<%# Bind("Accommodation_HotelUpdates_Id") %>'>
                                         <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                                        <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>

    </ContentTemplate>
</asp:UpdatePanel>
