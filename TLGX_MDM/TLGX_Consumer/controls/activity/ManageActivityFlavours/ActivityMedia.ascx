<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityMedia.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.ActivityMedia" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:UpdatePanel ID="updMedia" runat="server">
    <ContentTemplate>
       
        <div class="panel-group" id="searchResult">
            <div class="panel panel-default">

                <div class="panel-heading clearfix">
                    <h4 class="panel-title pull-left">
                        <a data-toggle="collapse" data-parent="#searchResult" href="#collapseSearchResult">Activity Media (Total Count:
                            <asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label>)</a>
                    </h4>
                </div>

                <div id="collapseSearchResult" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <asp:GridView ID="gvActMediaSearch" runat="server" AllowPaging="True" AllowCustomPaging="true"
                            EmptyDataText="No Media Found for search conditions" CssClass="table table-hover table-striped"
                            AutoGenerateColumns="false" OnPageIndexChanging="gvActMediaSearch_PageIndexChanging"
                            OnRowCommand="gvActMediaSearch_RowCommand" DataKeyNames="Activity_Media_Id" >
                            <Columns>
                                <asp:BoundField HeaderText="Media Name" DataField="MediaName" />
                                <asp:BoundField HeaderText="Media_URL" DataField="Media_URL" />
                                <asp:BoundField HeaderText="Category" DataField="Category" />
                                <asp:BoundField HeaderText="Sub Category" DataField="SubCategory" />
                                 <asp:BoundField HeaderText="Media Type" DataField="MediaType" />
                                <asp:BoundField HeaderText="Upload Date" DataField="Create_Date" DataFormatString="{0:dd/MM/yyyy hh:mm:ss tt}" />

                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Editing" CssClass="btn btn-default"
                                            Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Activity_Media_Id") %>'>
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
