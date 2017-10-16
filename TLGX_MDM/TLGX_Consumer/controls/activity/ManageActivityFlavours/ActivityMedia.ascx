<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityMedia.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.ActivityMedia" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script>
    function showMediaModal() {
        //alert("Hi");
        $("#moMedia").modal('show');
    }
    function closeMediaModal() {
        //alert("Hi");
        $("#moMedia").modal('hide');
    }
    function pageLoad(sender, args) {
        var hv = $('#MainContent_ActivityMedia_hdnFlag').val();
        if (hv == "true") {
            closeMediaModal();
            $('#MainContent_ActivityMedia_hdnFlag').val("false");
        }
    }

</script>
<script src="../../../Scripts/bootbox.min.js"></script>

<asp:UpdatePanel ID="updMedia" runat="server">
    <ContentTemplate>

        <div class="panel-group" id="searchResult">
            <div class="panel panel-default">
                <div class="panel-heading clearfix">
                    <h4 class="panel-title pull-left">
                        <a data-toggle="collapse" data-parent="#searchResult" href="#collapseSearchResult">Activity Media Details (Total Count:
                            <asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label>)</a></h4>
                    <asp:Button CssClass="pull-right btn btn-primary" runat="server" ID="btnNewUpload" OnClick="btnNewUpload_Click" Text="Add New" OnClientClick="showMediaModal()" />
                    <div class="col-lg-3 pull-right">
                        <div class="form-group pull-right">
                            <div class="input-group" runat="server" id="divDropdownForEntries">
                                <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                                <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>25</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                </div>
                <div id="collapseSearchResult" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <div id="dvMsg" runat="server" style="display: none;"></div>
                        <asp:GridView ID="gvActMediaSearch" runat="server" AllowPaging="True" AllowCustomPaging="true"
                            EmptyDataText="No Media Found for search conditions" CssClass="table table-hover table-striped"
                            AutoGenerateColumns="false" OnPageIndexChanging="gvActMediaSearch_PageIndexChanging"
                            OnRowCommand="gvActMediaSearch_RowCommand" DataKeyNames="Activity_Media_Id" OnRowDataBound="gvActMediaSearch_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="ValidFrom" DataField="ValidFrom" DataFormatString="{0:dd/MM/yyyy} " />
                                <asp:BoundField HeaderText="ValidTo" DataField="ValidTo" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField HeaderText="Category" DataField="Category" />
                                <asp:BoundField HeaderText="Sub Category" DataField="SubCategory" />
                                <asp:BoundField HeaderText="Media Type" DataField="MediaType" />
                                <asp:BoundField HeaderText="File Master" DataField="MediaFileMaster" />
                                <asp:BoundField HeaderText="Media Name" DataField="MediaName" />
                                <asp:BoundField HeaderText="Media_URL" DataField="Media_URL" />
                                <asp:BoundField HeaderText="Media Position" DataField="Media_Position" />
                                <asp:BoundField HeaderText="MediaID" DataField="MediaID" />
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                            Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Activity_Media_Id") %>'   OnClientClick="showMediaModal()">
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--OnClientClick='<%# "showDetailsModal('\''"+ Convert.ToString(Eval("SupplierImportFile_Id")) + "'\'');" %>'--%>
                                <%--OnClientClicking='<%#string.Format("showDetailsModal('{0}');",Eval("SupplierImportFile_Id ")) %>'                                            
                                           <%-- showDetailsModal('<%# Eval("SupplierImportFile_Id")%>');--%>
                                <asp:TemplateField ShowHeader="false" HeaderStyle-CssClass="Info">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                                            CssClass="btn btn-default" CommandArgument='<%# Bind("Activity_Media_Id") %>'>
                                                    <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat" %>'></span>
                                                    <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
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

<div class="modal fade" id="moMedia" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">

            <div class="modal-header">
                <h4 class="modal-title">Attribute Details</h4>
            </div>

            <div class="modal-body">
                <asp:UpdatePanel ID="upMediaAddEdit" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnFlag" runat="server" Value="" EnableViewState="false" />
                        <asp:FormView ID="frmMedia" runat="server" DataKeyNames="Activity_Media_Id" DefaultMode="Insert" OnItemCommand="frmMedia_ItemCommand">
                            <InsertItemTemplate>
                            </InsertItemTemplate>
                        </asp:FormView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>


