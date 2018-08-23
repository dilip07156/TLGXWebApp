<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNonOperatingDays.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.ucNonOperatingDays" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<div class="panel-group">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4 class="panel-title">
                <a data-toggle="collapse"  href="#collapse1">Non Operating Date</a>
            </h4>
        </div>
        <div id="collapse1" class="panel-collapse collapse in" aria-expanded="true">
            <div class="panel-body" >
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <div id="dvMsg" runat="server" style="display: none;"></div>
                            </div>
                        </div>
                        <div class="col-sm-5">
                            <label class="control-label col-sm-4" for="txtFrom">
                                From Date
                            </label>
                            <div class="col-sm-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control"/>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button" id="iCalFrom" runat="server">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </button>
                                    </span>
                                </div>
                                <cc1:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFrom" SelectedDate="<%# DateTime.Today %>" Format="dd/MM/yyyy" PopupButtonID="iCalFrom"></cc1:CalendarExtender>
                                <cc1:FilteredTextBoxExtender ID="axfte_txtFrom" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtFrom" />

                            </div>
                        </div>
                        <div class="col-sm-5">
                            <label class="control-label col-sm-4" for="txtTo">
                                To Date
                            </label>
                            <div class="col-sm-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txtTo" runat="server" CssClass="form-control"/>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button" id="iCalTo" runat="server">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </button>
                                    </span>
                                </div>
                                <cc1:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy" SelectedDate="<%# DateTime.Today %>" PopupButtonID="iCalTo"></cc1:CalendarExtender>
                                <cc1:FilteredTextBoxExtender ID="axfte_txtTo" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtTo" />
                            </div>
                        </div>
                        <div class="col-sm-2" style="padding-left: 0px;">
                            <label class="control-label col-sm-6" for="txtFrom"> Add </label>
                            <div class="input-group col-sm-6">
                                <asp:LinkButton CssClass="btn btn-default" runat="server" ID="btnAddDays" CommandName="AddDays" OnClick="addNonOperatingDate_Click">
                                <i class="glyphicon glyphicon-plus"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="col-lg-2 pull-right">
                    <div class="form-group pull-right">
                        <div class="input-group" runat="server" id="divDropdownForEntries">
                            <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                            <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged">
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div id="dvMsgAlert" runat="server" data-auto-dismiss="2000" style="display: none"></div>
                </div>
                <asp:GridView ID="gvNonOperatingData" runat="server" DataKeyNames="Activity_DaysOfOperation_Id" AutoGenerateColumns="false" CssClass="table table-bordered table-striped"
                    ShowHeaderWhenEmpty="false" Style="overflow-x: scroll" AllowPaging="True" AllowCustomPaging="true" OnPageIndexChanging="gvNonOperatingData_PageIndexChanging">
                    <Columns>
                        <asp:BoundField HeaderText="From Date" DataField="FromDate" HtmlEncode="False" DataFormatString="{0:d}"/>
                        <asp:BoundField HeaderText="To Date" DataField="EndDate" HtmlEncode="False" DataFormatString="{0:d}"/>
                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:LinkButton CssClass="btn btn-default" runat="server" ID="LinkButton1" CommandName="RemoveNonOperatingDays" OnClick="deleteNonOperatingDate_Click">
                                      <i class="glyphicon glyphicon-trash"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>
            </div>
        </div>
    </div>
</div>
