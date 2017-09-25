<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Media.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityMaster.Media" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<div class="container">
    <asp:UpdatePanel ID="updNewActivity" runat="server">
        <ContentTemplate>

            <div class="container">
                <div class="col-lg-12 row">
                    <div id="dvMsg" runat="server" style="display: none"></div>
                </div>
            </div>

            <%--<asp:FormView ID="frmProductInfo" runat="server" DefaultMode="Edit">

    <HeaderTemplate>
        <div class="container">
            <div class="col-lg-12 row">
                <asp:ValidationSummary ID="vlSum" runat="server" ValidationGroup="productInformation" DisplayMode="BulletList" ShowSummary="true" ShowMessageBox="false" CssClass="alert alert-danger" />
            </div>
        </div>
    </HeaderTemplate>

    <EditItemTemplate>--%>
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">

                            <div class="panel-heading">Media</div>

                            <div class="panel-body">

                                <div class="col-md-6">

                                    <div class="form-group col-sm-12">
                                        <label class="control-label col-sm-6" for="ddlFileCategory">
                                            File Category
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlFileCategory" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group col-sm-12 row">
                                        <label class="control-label col-sm-6" for="txtProductName">File Name</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtFileName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-md-6">

                                    

                                    <div class="form-group col-sm-12 row">
                                        <label class="control-label col-sm-6" for="txtProductName">File Path</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtFilePath" runat="server" CssClass="form-control"></asp:TextBox>
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

                                    <div class="form-group col-sm-12 row">
                                        <asp:LinkButton ID="btnSave" runat="server" CommandName="SaveProduct" Text="Add" CssClass="btn btn-primary btn-sm pull-right" />
                                    </div>

                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <%--</EditItemTemplate>

</asp:FormView>--%>

            <div class="form-group">
                <div id="dvGrid" runat="server" class="control-label col-sm-12">
                    <asp:GridView ID="grdSearchResults" runat="server" AllowPaging="false" AllowCustomPaging="false" AutoGenerateColumns="False" DataKeyNames="Activity_Id" CssClass="table table-hover table-striped">
                        <Columns>
                            <asp:BoundField DataField="" HeaderText="File Category" />
                            <asp:BoundField DataField="" HeaderText="File Name" />
                            <asp:BoundField DataField="" HeaderText="File URL" />
                            <asp:BoundField DataField="" HeaderText="File Description" />
                            <asp:BoundField DataField="" HeaderText="From" />
                            <asp:BoundField DataField="" HeaderText="To" />
                            <asp:HyperLinkField Text="Edit" ControlStyle-CssClass="btn btn-default" />
                            <asp:HyperLinkField Text="Delete" ControlStyle-CssClass="btn btn-default" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</div>
