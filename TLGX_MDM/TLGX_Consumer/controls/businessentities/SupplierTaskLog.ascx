<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SupplierTaskLog.ascx.cs" Inherits="TLGX_Consumer.controls.businessentities.SupplierTaskLog" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<script src="../../Scripts/Cron/Moment.min.js"></script>
<script src="../../Scripts/Cron/later.min.js"></script>
<script src="../../Scripts/Cron/prettycron.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        var test = prettyCron.toString("37 10 * * *");
    });

</script>

<!DOCTYPE html>
<html lang="en">

<head>
    <%--<meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.1.3/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.3.1/css/all.css">--%>
    <title>

    </title>
</head>

<body>
    <br>
    <div class="container">
        <hr>
        <h2>Main Navigation</h2>
        <hr>
        <h1>Pending Static Data File Handling</h1>
        <br>
        <div class="card">
            <div class="card-header">
                Search Tasks
            </div>         
                
            <%--<div class="card-body">
                <div class="row">
                    <div class="col-6">
                        <div class="form-group row">
                            <label for="ddlSupplierName">Supplier</label>
                            <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group row">
                            <label for="ddlEntity">Entity</label>
                            <asp:DropDownList ID="ddlEntity" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group row">
                            <label for="ddlstatus">Status</label>
                            <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                <asp:ListItem Text="---Active---" Value="1"></asp:ListItem>
                                <asp:ListItem Text="---Completed---" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group row">
                            <label for="dtFrom">From </label>
                            <input type="date" class="form-control" id="dtFrom" placeholder="name@example.com" runat="server">
                        </div>
                        <div class="form-group row">
                            <label for="dtTo">To </label>
                            <input type="date" class="form-control" id="dtTo" placeholder="name@example.com" runat="server">
                        </div>
                        <br>
                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" />
                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnReset_Click" Text="Reset" />

                    </div>
                </div>
            </div>--%>

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
                                                <label for="ddlEntity">Entity</label>
                                                    <asp:DropDownList ID="ddlEntity" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label for="ddlstatus">Status</label>
                                                <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Completed" Value="2"></asp:ListItem>
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
                                             <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" />
                                             <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnReset_Click" Text="Reset" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
             </asp:UpdatePanel>   

            <br>
            <div class="panel-body">
            <div class="row">
                <div class="col-8">
                    <h3>Search Results</h3>
                </div>
                <div class="col-4">
                    <div class="form-group">
                        <label for="exampleFormControlSelect1">page Size</label>
                        <asp:DropDownList ID="ddlShowEntries" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                            <asp:ListItem Text="25" Value="25"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
                </div>
        </div>
        <div id="collapseSearchResult" class="panel-collapse collapse in">
            <div class="panel-body">
                <asp:GridView ID="grdSupplierScheduleTask" runat="server" AllowPaging="True" AutoGenerateColumns="False" AllowCustomPaging="True"
                    EmptyDataText="No Data for search conditions" CssClass="table table-hover table-striped"
                    OnPageIndexChanging="grdSupplierScheduleTask_PageIndexChanging"
                    OnRowCommand="grdSupplierScheduleTask_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="SuppllierName" HeaderText="Supplier Name" SortExpression="SupplierName" />
                        <asp:BoundField DataField="Entity" HeaderText="Entity" SortExpression="Entity" />
                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                        <asp:BoundField DataField="ScheduledDate" HeaderText="Scheduled Date" SortExpression="ScheduledDate" />
                        <asp:BoundField DataField="PendingDays" HeaderText="Pending For Days" SortExpression="Status" />

                        <asp:TemplateField ShowHeader="false">

                            <ItemTemplate>
                                <asp:LinkButton ID="btndownload" runat="server" CausesValidation="false" CommandName="Download" CommandArgument='<%#Bind("logid") %>' CssClass="btn btn-default" Enabled="true">
                                            <span aria-hidden="true">Download Instruction</span>
                                </asp:LinkButton>
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnupload" runat="server" CausesValidation="false" CommandName="Upload" CommandArgument='<%#Bind("logid") %>' CssClass="btn btn-default" Enabled="true">
                                            <span aria-hidden="true">Upload File</span>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnTaskComp" runat="server" CausesValidation="false" CommandName="TaskComplete" CommandArgument='<%#Bind("logid") %>' CssClass="btn btn-default" Enabled="true">
                                            <span aria-hidden="true">Task Complete</span>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>
            </div>
        </div>


    </div>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.13.0/umd/popper.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.1.3/js/bootstrap.min.js"></script>
</body>

</html>

