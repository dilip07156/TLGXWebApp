<%@ Control Language="c#" CodeBehind="manageAPILocation.ascx.cs" Inherits="TLGX_Consumer.controls.staticdataconfig.manageAPILocation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script>

    function getAPIData(fileid) {
        if (fileid != null && fileid != "") {
            $.ajax({
                type: 'GET',
                url: '../../../Service/FileProgressDashboard.ashx?FileId=' + fileid,
                dataType: "json",
                success: function (result) {
                },
                error: function () {
                    //alert("Error fetching file processing data");
                },
            });
        }
    }
    //timer logic
    var x;
    function myTimer() {
        var d = new Date();
        var hdnval = document.getElementById("hdnFileId").value;
        //alert(hdnval);
        getChartData(hdnval);
    }
    function myStopFunction() {
        clearInterval(x);
    }
    //end
    // START for adding new api
    function showFileUpload() {
        $("#moAddApi").modal('show');
    }
    function closeFileUpload() {
        $("#moAddApi").modal('hide');
    }
    //END for adding new api
    //Start View Details.
    function closeDetailsModal() {
        $("#moViewDetials").modal('hide');
    }
    <%--  function pageLoad(sender, args) {
        var hdnViewDetailsFlag = $('#<%=hdnViewDetailsFlag.ClientID%>').val();

         if (hdnViewDetailsFlag == "true") {
             closeDetailsModal();
         }
         $('#hdnViewDetailsFlag').val("false");
     }--%>
</script>
<style>

</style>
<asp:UpdatePanel ID="updUserGrid" runat="server">
    <ContentTemplate>
        <div class="panel-group" id="accordion">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Aupplier API  Search</a>
                    </h4>
                </div>

                <div id="collapseSearch" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:ValidationSummary ID="vlSumm" runat="server" ValidationGroup="vldgrpFileSearch" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                            </div>
                        </div>
                        <div class="container">
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
                                <div class="form-group row">
                                    <div class="col-sm-12">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" ValidationGroup="vldgrpFileSearch" />
                                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" OnClick="btnReset_Click" />
                                    </div>
                                    <div class="col-sm-12">&nbsp; </div>
                                </div>
                            </div>
                        </div>
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
                            DataKeyNames="Pentahocall_id">
                            <Columns>
                                <asp:BoundField HeaderText="Supplier Name" DataField="Supplier" />
                                <asp:BoundField HeaderText="Entity" DataField="Entity" />
                                <asp:BoundField HeaderText="Status" DataField="Status" />
                                <asp:BoundField HeaderText="API Job Name" DataField="ApiPath" />
                                <asp:BoundField HeaderText="Create  Date" DataField="Create_Date" />

                                <%--<asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>' CssClass="btn btn-default" CommandArgument='<%# Bind("Accommodation_Description_Id") %>'>
                                         <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                                        <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                                    </asp:LinkButton>--%>


                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnViewDetail" runat="server" CausesValidation="false" CommandName="ViewDetails" CssClass="btn btn-default" Enabled="true" OnClick="btnViewDetail_Click">
                                            <%--OnClientClick='<%# "showDetailsModal('\''"+ Convert.ToString(Eval("SupplierImportFile_Id")) + "'\'');" %>'--%>
      <%--OnClientClicking='<%#string.Format("showDetailsModal('{0}');",Eval("SupplierImportFile_Id ")) %>'                                            
                                           <%-- showDetailsModal('<%# Eval("SupplierImportFile_Id")%>');--%>
                                            
                                                 <span aria-hidden="true">View Details</span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ShowHeader="false" HeaderStyle-CssClass="Info">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                            CssClass="btn btn-default">
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
                        <div class="row">
                            <div class="form-group ">
                                <label class="control-label col-sm-4" for="ddlSupplierList">Supplier </label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlSupplierList" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSupplierList_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <br />
                            <div class="form-group ">
                                <label class="control-label col-sm-4" for="ddlEntityList">Entity</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlEntityList" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlEntityList_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <br />
                            <div class="form-group ">
                                <label class="control-label col-sm-4" for="txtApiLocation">API Path</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtApiLocation" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div id="errormsg" runat="server" style="display: none">
                            <div class="alert alert-success fade in">
                                <a href="#" class="close" data-dismiss="alert">&times;</a>
                                <p>Please Select Supplier Name and Entity Both !!!!!</p>
                            </div>
                        </div>
                        <div class="row" runat="server" id="msgaddsuccessful" style="display: none">
                            <div class="alert alert-success fade in">
                                <a href="#" class="close" data-dismiss="alert">&times;</a>
                                <strong>SuccessFully Added!!</strong>
                                <%-- </div>--%>
                                <%--<div>--%>
                                <table class="table  table-bordered">
                                    <thead>
                                        <tr>
                                            <th>Status Code</th>
                                            <th>Status Message</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td id="statuscode" runat="server"></td>
                                            <td id="statusmessage" runat="server"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="alert alert-danger fade in" runat="server" id="errorinadding" style="display: none">
                            <a href="#" class="close" data-dismiss="alert">&times;</a>
                            <strong>Error!!</strong>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnadddetails" runat="server" Text="Add Details" OnClick="btnadddetails_Click" CssClass="btn btn-primary btn-sm" Enabled="false" />
                            <%-- <asp:Button ID="btnNewReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" OnClick="btnNewReset_Click" />--%>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</div>
