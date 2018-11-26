<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="manageSupplierScheduleTask.ascx.cs" Inherits="TLGX_Consumer.controls.businessentities.manageSupplierScheduleTask" %>
<script src="../../Scripts/Cron/Moment.min.js"></script>
<script src="../../Scripts/Cron/later.min.js"></script>
<script src="../../Scripts/Cron/prettycron.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
       var test=prettyCron.toString("37 10 * * *");
    });
  
</script>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.1.3/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.3.1/css/all.css">
    <link rel="stylesheet" href="style.css">
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
            <div class="card-body">
                <div class="row">
                    <div class="col-6">
                        <div class="form-group">
                            <label for="ddlSupplierName">Supplier</label>
                            <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                 <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="ddlEntity">Entity</label>
                            <asp:DropDownList ID="ddlEntity" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                 <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="ddlstatus">Status</label>
                            <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                 <asp:ListItem Text="---Active---" Value="1"></asp:ListItem>
                                 <asp:ListItem Text="---Completed---" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label for="dtFrom">From </label>
                            <input type="date" class="form-control" id="dtFrom" placeholder="name@example.com" runat="server">
                        </div>
                        <div class="form-group">
                            <label for="dtTo">To </label>
                            <input type="date" class="form-control" id="dtTo" placeholder="name@example.com" runat="server">
                        </div>
                        <br>
                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" />
                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnReset_Click" Text="Reset" />
                       <%-- <button type="button" class="btn btn-primary">Search</button>
                        <button type="button" class="btn btn-primary">Reset</button>--%>
                    </div>
                </div>
            </div>
        </div>

        <br>
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
<%--                    <select class="form-control" id="ddlShowEntries">
                        <option>5</option>
                        <option>10</option>
                        <option>15</option>
                        <option>20</option>
                        <option>25</option>
                    </select>--%>
                </div>
            </div>
        </div>
        <div id="collapseSearchResult" class="panel-collapse collapse in">
                    <div class="panel-body">
                      <asp:GridView ID="grdSupplierScheduleTask" runat="server" AllowPaging="True" AutoGenerateColumns="False" AllowCustomPaging="True"
                            EmptyDataText="No Data for search conditions" CssClass="table table-hover table-striped"
                             OnSelectedIndexChanged="grdCountryMaps_SelectedIndexChanged" OnPageIndexChanging="grdSupplierScheduleTask_PageIndexChanging"
                            OnRowCommand="grdSupplierScheduleTask_RowCommand" OnSorting="grdSupplierScheduleTask_Sorting" OnDataBound="grdSupplierScheduleTask_DataBound"
                            OnRowDataBound="grdSupplierScheduleTask_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" SortExpression="SupplierName" />
                                <asp:BoundField DataField="Entity" HeaderText="Entity" SortExpression="Entity" />
                                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                <asp:BoundField DataField="ScheduledDate" HeaderText="Scheduled Date" SortExpression="ScheduledDate" />
                                <asp:BoundField DataField="PendingDays" HeaderText="Pending For Days" SortExpression="Status" />
                              
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDownloadInstruction" runat="server" CausesValidation="false" CommandName="Download Instruction" CssClass="btn btn-default"
                                            Enabled="true" CommandArgument='<%# Bind("Supplier_Id") %>'">
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Download Instruction
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnUploadFile" runat="server" CausesValidation="false" CommandName="Upload File" CssClass="btn btn-default"
                                            Enabled="true" CommandArgument='<%# Bind("Supplier_Id") %>'">
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Upload File
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnTaskCompleted" runat="server" CausesValidation="false" CommandName="Task Completed" CssClass="btn btn-default"
                                            Enabled="true" CommandArgument='<%# Bind("Supplier_Id") %>'">
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Task Completed
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
