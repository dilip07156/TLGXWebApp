<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClassificationAttributes.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.ClassificationAttributes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:UpdatePanel ID="updCA" runat="server">
    <ContentTemplate>


        <div class="panel-group" id="searchResult">
            <div class="panel panel-default">

                <div class="panel-heading clearfix">
                    <h4 class="panel-title pull-left">
                        <a data-toggle="collapse" data-parent="#searchResult" href="#collapseSearchResult">Search Results (Total Count:
                            <asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label>)</a></h4>
                    <asp:Button CssClass="pull-right btn btn-primary" runat="server" ID="btnNewUpload" OnClick="btnNewUpload_Click" Text="Add New" />
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

                        <div class="row">
                            <div id="dvMsg" runat="server" enableviewstate="false" style="display: none;">
                            </div>
                        </div>

                        <asp:GridView ID="gvActivityCASearch" runat="server" AllowPaging="True" AllowCustomPaging="true"
                            EmptyDataText="No Classification Attributes Found for search conditions" CssClass="table table-hover table-striped"
                            AutoGenerateColumns="false" OnPageIndexChanging="gvActivityCASearch_PageIndexChanging"
                            OnRowCommand="gvActivityCASearch_RowCommand" DataKeyNames="Activity_ClassificationAttribute_Id">
                            <Columns>
                                <asp:BoundField HeaderText="Attribute Type" DataField="AttributeType" />
                                <asp:BoundField HeaderText="Attribute SubType" DataField="AttributeSubType" />
                                <asp:BoundField HeaderText="Attribute Value" DataField="AttributeValue" />
                                <asp:BoundField HeaderText="Internal Flag" DataField="InternalOnly" />
                                <asp:BoundField HeaderText="Upload Date" DataField="CreateDate" DataFormatString="{0:dd/MM/yyyy hh:mm:ss tt}" />

                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Editing" CssClass="btn btn-default"
                                            Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Activity_ClassificationAttribute_Id") %>'>
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
                                            CssClass="btn btn-default" CommandArgument='<%# Bind("Activity_ClassificationAttribute_Id") %>'>
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
