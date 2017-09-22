<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductStatus.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityMaster.ProductStatus" %>
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

    Effective From ( Date)
    Reason : Long Description
    Save Button : On Click it would save the status

            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">

                            <div class="panel-heading">Product Status</div>

                            <div class="panel-body">

                                <div class="col-md-6">

                                    <div class="form-group col-sm-12">
                                        <label class="control-label col-sm-6" for="ddlMarket">
                                            Market
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlMarket" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group col-sm-12">
                                        <label class="control-label col-sm-6" for="ddlStatus">
                                            Status
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group col-sm-12">
                                        <label class="control-label col-sm-6" for="txtFrom">
                                            From
                                        <%--<asp:RequiredFieldValidator ID="vldtxtFrom" runat="server" ControlToValidate="txtFrom" ErrorMessage="Please select from date" Text="*" ValidationGroup="vldgrpDescriptions" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
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

                                </div>

                                <div class="col-md-6">

                                    <div class="form-group">
                                        <label for="txtDescription" class="control-label-mand col-sm-6">
                                            Reason
                                            <%--<asp:RequiredFieldValidator ID="vldtxtDescription" runat="server" ControlToValidate="txtReason" ErrorMessage="Please enter description" Text="*" ValidationGroup="vldgrpDescriptions" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
                                        </label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    
                                    <div class="form-group col-sm-12 row">
                                        <asp:LinkButton ID="btnNewActivity" runat="server" Text="Add New" CssClass="btn btn-primary btn-sm pull-right"  OnClientClick="showAddNewActivityModal();" />
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
                            <asp:BoundField DataField="" HeaderText="Market" />
                            <asp:BoundField DataField="" HeaderText="Status" />
                            <asp:BoundField DataField="" HeaderText="Effective From" />
                            <asp:BoundField DataField="" HeaderText="Reason" />
                            <asp:BoundField DataField="" HeaderText="Action" />
                            <asp:HyperLinkField Text="Edit" ControlStyle-CssClass="btn btn-default" />
                            <asp:HyperLinkField Text="Delete" ControlStyle-CssClass="btn btn-default" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</div>
