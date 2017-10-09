<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Inclusions.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.Inclusions" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script>
    function showFileUpload() {
        $("#moAddInclusions").modal('show');
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
<asp:UpdatePanel ID="updMedia" runat="server">
    <ContentTemplate>
        <div class="panel-group" id="searchResult">
            <div class="panel panel-default">
                <div class="panel-heading clearfix">
                    <h4 class="panel-title pull-left">
                        <a data-toggle="collapse" data-parent="#searchResult" href="#collapseSearchResult">Activity Media (Total Count:
                            <asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label>)</a></h4>
                     <asp:Button CssClass="pull-right btn btn-primary" runat="server" ID="btnNewUpload" OnClick="btnNewUpload_Click" Text="Add New"  OnClientClick=""/>
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
                        <asp:GridView ID="gvActInclusionSearch" runat="server" AllowPaging="True" AllowCustomPaging="true"
                            EmptyDataText="No Activity Inclusions Found for search conditions" CssClass="table table-hover table-striped"
                            AutoGenerateColumns="false" OnPageIndexChanging="gvActInclusionSearch_PageIndexChanging"
                            OnRowCommand="gvActInclusionSearch_RowCommand" DataKeyNames="Activity_Inclusions_Id">
                            <Columns>
                                <asp:BoundField HeaderText="Inclusion Type" DataField="InclusionType" />
                                <asp:BoundField HeaderText="Inclusion Name" DataField="InclusionName" />
                                <asp:BoundField HeaderText="Inclusion For" DataField="InclusionFor" />
                                <asp:BoundField HeaderText="Description" DataField="InclusionDescription" />
                                 <asp:BoundField HeaderText="Is Inclusion" DataField="IsInclusion" />
                                <asp:BoundField HeaderText="Upload Date" DataField="Create_Date" DataFormatString="{0:dd/MM/yyyy hh:mm:ss tt}" />

                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Editing" CssClass="btn btn-default"
                                            Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Activity_Inclusions_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField ShowHeader="false" HeaderStyle-CssClass="Info">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                                            CssClass="btn btn-default" CommandArgument='<%# Bind("Activity_Inclusions_Id") %>'>
                                                    <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat" %>'></span>
                                                    <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                    </div>
                </div>

            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>

<div class="modal fade" id="moAddInclusions" role="dialog">
    <div class="modal-dialog modal-xl">
        <div class="modal-content">
            <div class="modal-header">
                <div class="panel-title">
                    <h4 class="modal-title">Add Activity Inclusions</h4>
                </div>
            </div>
            <div class="modal-body">
                
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</div>