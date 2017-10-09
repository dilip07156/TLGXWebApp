<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClassificationAttributes.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.ClassificationAttributes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="updCA" runat="server">
    <ContentTemplate>
        <div class="panel-group" id="accordion">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Media Search</a>
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
                                <div class="col-sm-6">
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="txtAttributeType">Attribute Type </label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtAttributeType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="txtAttribueValue">Attribute Value </label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtAttribueValue" runat="server" CssClass="form-control" AppendDataBoundItems="true"> </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="TxtMediaName">Attribute Sub type </label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:TextBox>
                                        </div>
                                    </div>

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
        </div>

        <div class="row">

            <div class="col-lg-8">
                <h3>Classification Attributes Search Details</h3>
            </div>


            <div class="col-lg-3">

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

            <div class="col-lg-1">
                <asp:Button ID="btnNewUpload" runat="server" CssClass="btn btn-primary btn-sm" Text="Upload" OnClientClick="showFileUpload();" OnClick="btnNewUpload_Click" />
            </div>
        </div>


        <div class="panel-group" id="CAsearchResult">
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

                        <asp:GridView ID="gvActivityCASearch" runat="server" AllowPaging="True" AllowCustomPaging="true"
                            EmptyDataText="No Classification Attributes Found for search conditions" CssClass="table table-hover table-striped"
                            AutoGenerateColumns="false" OnPageIndexChanging="gvActivityCASearch_PageIndexChanging"
                            OnRowCommand="gvActivityCASearch_RowCommand" DataKeyNames="Activity_ClassificationAttribute_Id,Activity_Flavour_Id">
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
