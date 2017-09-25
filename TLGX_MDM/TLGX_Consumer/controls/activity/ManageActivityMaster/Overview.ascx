<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Overview.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityMaster.Overview" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<div class="container">
    <asp:UpdatePanel ID="updNewActivity" runat="server">
        <ContentTemplate>

            <div class="container">
                <div class="col-lg-12 row">
                    <div id="dvMsg" runat="server" style="display: none"></div>
                </div>
            </div>

            <asp:FormView ID="frmProductInfo" runat="server" DefaultMode="Edit">

                <HeaderTemplate>
                    <div class="container">
                        <div class="col-lg-12 row">
                            <asp:ValidationSummary ID="vlSum" runat="server" ValidationGroup="productInformation" DisplayMode="BulletList" ShowSummary="true" ShowMessageBox="false" CssClass="alert alert-danger" />
                        </div>
                    </div>
                </HeaderTemplate>

                <EditItemTemplate>
                    <div class="container">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-default">

                                    <div class="panel-heading">Overview</div>

                                    <div class="panel-body">

                                        <div class="col-md-6">

                                            <div class="form-group row" style="display: none">
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtActivityID" runat="server" Text='<%# Bind("Activity_ID") %>' class="form-control" />
                                                </div>
                                            </div>

                                            <div class="form-group row">

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

                                            <div class="form-group row">

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

                                        <div class="col-md-6">

                                            <div class="form-group row">
                                                <label class="control-label col-md-4" for="txtDisplayName">
                                                    Short Description
                                                </label>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtShortDescription" runat="server" Rows="3" TextMode="MultiLine" CssClass="form-control" />
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="control-label col-md-4" for="txtDisplayName">
                                                    Long Description
                                                </label>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtLongDescription" runat="server" Rows="3" TextMode="MultiLine" CssClass="form-control" />
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <asp:LinkButton ID="btnSave" runat="server" CommandName="SaveProduct" Text="Add" CssClass="btn btn-primary btn-sm pull-right" />
                                            </div>

                                        </div>

                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </EditItemTemplate>

            </asp:FormView>

            <div class="form-group">
                <div id="dvGrid" runat="server" class="control-label col-sm-12">
                    <asp:GridView ID="grdSearchResults" runat="server" AllowPaging="false" AllowCustomPaging="false" AutoGenerateColumns="False" DataKeyNames="Activity_Id" CssClass="table table-hover table-striped">
                        <Columns>
                            <asp:BoundField DataField="" HeaderText="From" />
                            <asp:BoundField DataField="" HeaderText="To" />
                            <asp:BoundField DataField="" HeaderText="Short Description" />
                            <asp:BoundField DataField="" HeaderText="Long Description" />
                            <asp:HyperLinkField Text="Edit" ControlStyle-CssClass="btn btn-default" />
                            <asp:HyperLinkField Text="Delete" ControlStyle-CssClass="btn btn-default" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</div>
