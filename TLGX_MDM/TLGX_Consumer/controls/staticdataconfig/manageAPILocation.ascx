<%@ Control Language="c#" CodeBehind="manageAPILocation.ascx.cs" Inherits="TLGX_Consumer.controls.staticdataconfig.manageAPILocation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script>
    function getviewDetailsData(pentahoid) {
        if (pentahoid != null && pentahoid != "") {
            $.ajax({
                type: 'GET',
                url: '../../../Service/PentahoStepsInfo.ashx?Pentahoid=' + pentahoid,
                dataType: "json",
                success: function (result) {
                    if (result != null) {
                        $("#tblsteps").empty();
                        for (var i = 0; i < result.Stepstatuslist.Stepstatus.length; i++) {
                            var a = result.Stepstatuslist.Stepstatus[i];
                            var tr;
                            tr = $('<tr/>');
                            tr.append("<td>" + a.Stepname + "</td>");
                            tr.append("<td>" + a.Copy + "</td>");
                            tr.append("<td>" + a.LinesRead + "</td>");
                            tr.append("<td>" + a.LinesWritten + "</td>");
                            tr.append("<td>" + a.LinesInput + "</td>");
                            tr.append("<td>" + a.LinesOutput + "</td>");
                            tr.append("<td>" + a.LinesUpdated + "</td>");
                            tr.append("<td>" + a.LinesRejected + "</td>");
                            tr.append("<td>" + a.Errors + "</td>");
                            tr.append("<td>" + a.StatusDescription + "</td>");
                            tr.append("<td>" + a.Seconds + "</td>");
                            tr.append("<td>" + a.Speed + "</td>");
                            tr.append("<td>" + a.Priority + "</td>");
                            $("#steps table").append(tr);
                        }
                    }
                    else {

                    }
                },
                error: function () {
                },
            });
        }
    }
    //timer logic
    var timer;
    function myTimer() {
        //alert("Timer");
        var hdnval = document.getElementById("hdnPentahoid").value;
        getviewDetailsData(hdnval);
    }
    function myStopFunction() {
        clearInterval(timer);
    }
    //end
    // START for adding new api
    function showFileUpload() {
        $("#moAddApi").modal('show');
    }
    function closeFileUpload() {
        $("#moAddApi").modal('hide');
    }
    //Start View Details.
    function showDetailsModal(pentahoid, name, entity, status, path) {
        $("#moViewDetials").modal('show');
        $('#moViewDetials').one('shown.bs.modal', function () {
            $("#MainContent_manageAPILocation_txtSupplier").val(name);
            $("#MainContent_manageAPILocation_txtEntity").val(entity);
            $("#MainContent_manageAPILocation_txtPath").val(path);
            $("#MainContent_manageAPILocation_txtStatus").val(status);
            document.getElementById("hdnPentahoid").value = pentahoid;
            getviewDetailsData(pentahoid);
            //start timer
            timer = setInterval(function () { myTimer() }, 3000);

        });
        $('#moViewDetials').on('hidden.bs.modal', function () {
            $("#tblsteps").empty();
            //stop timer on close of modal 
            myStopFunction();
        });
    }

</script>
<style>
    @media (min-width: 768px) {
        .modal-xl {
            width: 80%;
            max-width: 1200px;
        }
    }
</style>
<asp:UpdatePanel ID="updUserGrid" runat="server">
    <ContentTemplate>
        <div class="panel-group" id="accordion">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Supplier API  Search</a>
                    </h4>
                </div>

                <div id="collapseSearch" class="panel-collapse collapse in">
                    <div class="panel-body">
                       <%-- <div class="container">--%>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlSupplierName">Supplier </label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlMasterCountry">Entity</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlMasterCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlStatus">Status</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                    <div class="col-sm-12">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" ValidationGroup="vldgrpFileSearch" />
                                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" OnClick="btnReset_Click" />
                                    </div>
                            </div>
                       <%-- </div>--%>
                    </div>
                </div>

            </div>
        </div>
        <div class="row">
            <div class="col-lg-8">
                <h3>Mapping Details</h3>
            </div>
            <div class="col-lg-3">

                <div class="form-group pull-right">
                    <div class="input-group" runat="server" id="divDropdownForEntries">
                        <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                        <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>35</asp:ListItem>
                            <asp:ListItem>40</asp:ListItem>
                            <asp:ListItem>45</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>

            </div>
            <div class="col-lg-1">
                <asp:Button ID="btnNewUpload" runat="server" CssClass="btn btn-primary btn-sm" Text="Add New" OnClientClick="showFileUpload();" OnClick="btnNewUpload_Click" />
            </div>
        </div>
        <div class="panel-group" id="searchResult">
            <div class="panel panel-default">
                <div class="panel-heading clearfix">
                    <h4 class="panel-title pull-left">
                        <a data-toggle="collapse" data-parent="#searchResult" href="#collapseSearchResult">Search Results (Total Count:
                            <asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label>)</a>
                    </h4>
                </div>
                <div id="collapseSearchResult" class="panel-collapse collapse in">
                    <div class="panel-body">

                        <div class="row">
                            <div id="dvMsg" runat="server" enableviewstate="false" style="display: none;">
                            </div>
                        </div>
                        <asp:GridView ID="gvSupplierApiSearch" runat="server" AllowPaging="True" AllowCustomPaging="true"
                            EmptyDataText="No Mappings for search conditions" CssClass="table table-hover table-striped"
                            AutoGenerateColumns="false" OnPageIndexChanging="gvSupplierApiSearch_PageIndexChanging"
                            DataKeyNames="SupplierApiCallLog_Id,Pentahocall_id">
                            <Columns>
                                <asp:BoundField HeaderText="Supplier Name" DataField="Supplier" />
                                <asp:BoundField HeaderText="Entity" DataField="Entity" />
                                <asp:BoundField HeaderText="Status" DataField="Status" />
                                <asp:BoundField HeaderText="API Job Name" DataField="ApiPath" />
                                <asp:BoundField HeaderText="Create  Date" DataField="Create_Date"  DataFormatString="{0:dd/MM/yyyy hh:mm:ss tt}"/>
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <%--<asp:LinkButton ID="btnViewDetail" runat="server" CausesValidation="false"
                                            OnClientClick=<%# "javascript:showDetailsModal('" + Eval("Pentahocall_id") + "'," + this + ")" %>
                                            CommandName="ViewDetails" CssClass="btn btn-default" Enabled="true" CommandArgument='<%# Bind("Pentahocall_id") %>'>
                                                 <span aria-hidden="true">View Details</span>
                                        </asp:LinkButton>--%>
                                        <asp:LinkButton ID="btnViewDetail" runat="server" CausesValidation="false"
                                            OnClientClick='<%# string.Format("return showDetailsModal(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\");", Eval("Pentahocall_id"), Eval("Supplier"),Eval("Entity"),Eval("Status"),Eval("ApiPath")) %>'
                                            CommandName="ViewDetails" CssClass="btn btn-default" Enabled="true" CommandArgument='<%# Bind("Pentahocall_id") %>'>
                                                 <span aria-hidden="true">View Details</span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false" HeaderStyle-CssClass="Info">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                            CssClass="btn btn-default" Enabled="false">
                                                    <span aria-hidden="true" >Delete</span>
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

    </ContentTemplate>
</asp:UpdatePanel>


<div class="modal fade" id="moAddApi" role="dialog">
    <div class="modal-dialog modal-md">
        <div class="modal-content">
            <div class="modal-header">
                <div class="panel-title">
                    <h4 class="modal-title">Add new Supplier API</h4>
                </div>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div id="dvError" runat="server" style="display: none;"></div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="control-label col-sm-4" for="ddlSupplierList">Supplier </label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlSupplierList" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSupplierList_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row ">
                                <label class="control-label col-sm-4" for="ddlEntityList">Entity</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlEntityList" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlEntityList_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="control-label col-sm-4" for="txtApiLocation">API Path</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtApiLocation" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnadddetails" runat="server" Text="Add Details" OnClick="btnadddetails_Click" CssClass="btn btn-primary btn-sm" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</div>

<div class="modal fade" id="moViewDetials" role="dialog" data-backdrop="static" data-keyboard="false">
    <div class="modal-dialog modal-xl">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title"><b>View Details </b></h4>
            </div>
            <input type="hidden" id="hdnPentahoid" name="hdnPentahoid" value="" />
            <div class="modal-body">
               <%-- <div class="container">--%>
                    <div class="row">
                        <div class="col-sm-3">
                            <label class="col-form-label" for="txtSupplier">Supplier</label>
                            <asp:TextBox ID="txtSupplier" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                            <label class="col-form-label " for="txtEntity">Entity</label>
                            <%--<label id="lblEntity" class="form-control"></label>--%>
                            <asp:TextBox ID="txtEntity" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                            <label class="col-form-label">API Job Name</label>
                            <asp:TextBox ID="txtPath" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                            <label class="col-form-label">Status</label>
                            <asp:TextBox ID="txtStatus" CssClass="form-control" runat="server" value="" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div id="steps" >
                        <table class="table  table-bordered border-collapse table-striped  table-fixed" style="overflow-y: scroll; height: 400px;">
                            <thead>
                                <tr>
                                    <th>Stepname</th>
                                    <th>Copy</th>
                                    <th>Read</th>
                                    <th>Written</th>
                                    <th>Input</th>
                                    <th>Output</th>
                                    <th>Updated</th>
                                    <th>Rejected</th>
                                    <th>Errors</th>
                                    <th>Active</th>
                                    <th>Time</th>
                                    <th>Speed</th>
                                    <th>pr/in/out</th>
                                </tr>
                            </thead>
                            <tbody id="tblsteps" >
                            </tbody>
                        </table>
                    </div>
                <%--</div>--%>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
