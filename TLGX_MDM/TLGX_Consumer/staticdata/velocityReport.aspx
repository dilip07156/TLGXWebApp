<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="velocityReport.aspx.cs" Inherits="TLGX_Consumer.staticdata.velocityReport" MasterPageFile="~/Site.Master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <style>
        .btnwidth {
            margin-top: 18px;
        }
    </style>

    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Velocity Dashboard</h1>
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="Pnlupdatesearch" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Filters</a>
                            </h4>
                        </div>
                        <div id="collapseSearch" class="panel-collapse collapse in" aria-expanded="true">
                            <div class="panel-body">
                                <div id="dvMsg" runat="server" style="display: none;"></div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="ddlSupplierName" class="control-label-mand ">Select Supplier </label>
                                                <asp:DropDownList runat="server" ID="ddlSupplierName" CssClass="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">--All Suppliers--</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="ddlPriority" class="control-label-mand ">Select Priority </label>
                                                <asp:DropDownList runat="server" ID="ddlPriority" CssClass="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">--All Priority--</asp:ListItem>
                                                     <asp:ListItem Value="1">1</asp:ListItem>
                                                     <asp:ListItem Value="2">2</asp:ListItem>
                                                     <asp:ListItem Value="3">3</asp:ListItem>
                                                     <asp:ListItem Value="4">4</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label for="ddlSupplierName" class="control-label-mand ">Select Date </label>
                                            <asp:DropDownList runat="server" ID="ddlDateOptions" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDateOptions_SelectedIndexChanged" AutoPostBack="true">
                                                <%--OnSelectedIndexChanged="ddlDateOptions_SelectedIndexChanged" AutoPostBack="True" --%>
                                                <asp:ListItem Value="0">Yesterday</asp:ListItem>
                                                <asp:ListItem Value="1">Today</asp:ListItem>
                                                <asp:ListItem Value="2">This week</asp:ListItem>
                                                <asp:ListItem Value="3">Last week</asp:ListItem>
                                                <asp:ListItem Value="4">This month</asp:ListItem>
                                                <asp:ListItem Value="5">Last Month</asp:ListItem>
                                                <asp:ListItem Value="6">Specific Date</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="txtFrom" class="control-label-mand ">Date From </label>
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" Width="240px" />
                                                    <span class="input-group-btn" style="display: block">
                                                        <button class="btn btn-default" type="button" id="iCalFrom" runat="server">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </button>
                                                    </span>

                                                </div>
                                                <cc1:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy" PopupButtonID="iCalFrom"></cc1:CalendarExtender>
                                                <cc1:FilteredTextBoxExtender ID="axfte_txtFrom" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtFrom" />
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="txtTo" class="control-label-mand">Date To   </label>
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" Width="240px" />
                                                    <span class="input-group-btn" style="display: block">
                                                        <button class="btn btn-default" type="button" id="iCalTo" runat="server">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </button>
                                                    </span>
                                                </div>

                                                <cc1:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy" PopupButtonID="iCalTo"></cc1:CalendarExtender>
                                                <cc1:FilteredTextBoxExtender ID="axfte_txtTo" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtTo" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="col-md-12"></div>
                                        <div class="col-md-12">
                                            <asp:Button ID="btnViewStatus" runat="server" Text="View Status" CssClass="btn btn-primary btnwidth btnwidth" OnClick="btnViewStatus_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlDateOptions" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="PnlUpdateDiv">
        <ContentTemplate>
            <%--   Geography--%>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading"><strong>Geography</strong> </div>
                        <div class="panel-body">
                            <div class="col-md-6">
                                <div class="panel-heading"><strong>Country</strong> </div>
                                <asp:GridView ID="gvcountry" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped"
                                    ShowHeaderWhenEmpty="false" EmptyDataText="No mapping activity has not been done for this date range.">
                                    <Columns>
                                        <asp:BoundField HeaderText="UserName" DataField="Username" />
                                        <asp:BoundField HeaderText="Count" DataField="Totalcount" />
                                    </Columns>
                                </asp:GridView>
                                <div class="panel-footer">
                                    <strong>Remaining</strong>:&nbsp&nbsp
                                        <asp:Label runat="server" ID="lblcountry"></asp:Label><br />
                                    <strong>Estimate</strong>:&nbsp&nbsp
                                        <asp:Label runat="server" ID="lblcountryestimate"></asp:Label>
                                    &nbsp Days
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="panel-heading"><strong>City</strong> </div>
                                <asp:GridView ID="gvcity" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped"
                                    ShowHeaderWhenEmpty="false" EmptyDataText="No mapping activity has not been done for this date range.">
                                    <Columns>
                                        <asp:BoundField HeaderText="UserName" DataField="Username" />
                                        <asp:BoundField HeaderText="Count" DataField="Totalcount" />
                                    </Columns>
                                </asp:GridView>
                                <div class="panel-footer">
                                    <strong>Remaining</strong>:&nbsp&nbsp
                                        <asp:Label runat="server" ID="lblcity"></asp:Label><br />
                                    <strong>Estimate</strong>:&nbsp&nbsp
                                        <asp:Label runat="server" ID="lblcityestimate"></asp:Label>&nbsp Days
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--Accomodation--%>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading"><strong>Accomodation</strong> </div>
                        <div class="panel-body">
                            <div class="col-md-6">
                                <div class="panel-heading"><strong>Hotel</strong> </div>
                                <asp:GridView ID="gvproduct" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped"
                                    ShowHeaderWhenEmpty="false" EmptyDataText="No mapping activity has not been done for this date range.">
                                    <Columns>
                                        <asp:BoundField HeaderText="UserName" DataField="Username" />
                                        <asp:BoundField HeaderText="Count" DataField="Totalcount" />
                                    </Columns>
                                </asp:GridView>
                                <div class="panel-footer">
                                    <strong>Remaining</strong>:&nbsp&nbsp
                                        <asp:Label runat="server" ID="lblproduct"></asp:Label><br />
                                    <strong>Estimate</strong>:&nbsp&nbsp
                                        <asp:Label runat="server" ID="lblproductestimate"></asp:Label>&nbsp Days
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="panel-heading"><strong>Room Type</strong> </div>
                                <asp:GridView ID="gvroomtype" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" EmptyDataText="No mapping activity has not been done for this date range."
                                    ShowHeaderWhenEmpty="false">
                                    <Columns>
                                        <asp:BoundField HeaderText="UserName" DataField="Username" />
                                        <asp:BoundField HeaderText="Count" DataField="Totalcount" />
                                    </Columns>
                                </asp:GridView>
                                <div class="panel-footer">
                                    <strong>Remaining</strong>:&nbsp&nbsp
                                        <asp:Label runat="server" ID="lblhotelroom"></asp:Label>
                                    <br />
                                    <strong>Estimate</strong>:&nbsp&nbsp
                                        <asp:Label runat="server" ID="lblhotelroomestimate"></asp:Label>&nbsp Days
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--Activity--%>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading"><strong>Activity</strong> </div>
                        <div class="panel-body">
                            <div class="col-md-6">
                                <div class="panel-heading"><strong>Activity</strong> </div>
                                <asp:GridView ID="gvactivity" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped"
                                    ShowHeaderWhenEmpty="false" EmptyDataText="No mapping activity has not been done for this date range.">
                                    <Columns>
                                        <asp:BoundField HeaderText="UserName" DataField="Username" />
                                        <asp:BoundField HeaderText="Count" DataField="Totalcount" />
                                    </Columns>
                                </asp:GridView>
                                <div class="panel-footer">
                                    <strong>Remaining</strong>:&nbsp&nbsp
                                        <asp:Label runat="server" ID="lblactivity"></asp:Label>
                                    <br />
                                    <strong>Estimate</strong>:&nbsp&nbsp
                                        <asp:Label runat="server" ID="lblactivityestimate"></asp:Label>&nbsp Days
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
