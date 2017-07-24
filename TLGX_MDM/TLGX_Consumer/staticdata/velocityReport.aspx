<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="velocityReport.aspx.cs" Inherits="TLGX_Consumer.staticdata.velocityReport" MasterPageFile="~/Site.Master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <style>
         @media(min-width: 992px) {
            .col5 {
                width: 20%;
                float: left;
                position: relative;
                min-height: 1px;
                padding-right: 15px;
                padding-left: 15px;
            }
        }
         .btnwidth{
                 width: 100px;
         }
    </style>

    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Velocity Dashboard</h1>
        </div>
    </div>
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Filters</a>
                </h4>
            </div>
            <div id="collapseSearch" class="panel-collapse collapse in" aria-expanded="true">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="ddlSupplierName" class="control-label-mand ">Select Supplier </label>
                                <asp:DropDownList runat="server" ID="ddlSupplierName" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">--All Suppliers--</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="txtFrom" class="control-label-mand ">Date From </label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" Text='<%# Bind("FromDate", "{0:dd/MM/yyyy}") %>' Width="240px" />
                                    <span class="input-group-btn" style="display: block">
                                        <button class="btn btn-default" type="button" id="iCalFrom">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </button>
                                    </span>

                                </div>
                                <cc1:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy" PopupButtonID="iCalFrom"></cc1:CalendarExtender>
                                <cc1:FilteredTextBoxExtender ID="axfte_txtFrom" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtFrom" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="txtTo" class="control-label-mand">Date To   </label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" Text='<%# Bind("ToDate", "{0:dd/MM/yyyy}") %>' Width="240px" />
                                    <span class="input-group-btn" style="display: block">
                                        <button class="btn btn-default" type="button" id="iCalTo">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </button>
                                    </span>
                                </div>

                                <cc1:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy" PopupButtonID="iCalTo"></cc1:CalendarExtender>
                                <cc1:FilteredTextBoxExtender ID="axfte_txtTo" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtTo" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col5 col-sm-2">
                                <asp:Button ID="Btncountry" runat="server" Text="Country" CssClass="btn btn-primary btnwidth" />
                        </div>
                          <div class="col5 col-sm-2">
                                <asp:Button ID="Btncity" runat="server" Text="City" CssClass="btn btn-primary btnwidth" />
                        </div>
                          <div class="col5 col-sm-2">
                                <asp:Button ID="Btnproduct" runat="server" Text="Hotel" CssClass="btn btn-primary btnwidth" />
                        </div>
                          <div class="col5 col-sm-2">
                                <asp:Button ID="Btnhotelroom" runat="server" Text="Hotel Rooms" CssClass="btn btn-primary btnwidth " />
                        </div>
                          <div class="col5 col-sm-2">
                                <asp:Button ID="Btnactivity" runat="server" Text="Activity" CssClass="btn btn-primary  btnwidth" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="container" id="tables">
        <asp:Table ID="Tblcountry" runat="server"></asp:Table>
    </div>
</asp:Content>
