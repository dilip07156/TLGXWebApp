<%@ Page Title="Site Map" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="sitemap.aspx.cs" Inherits="TLGX_Consumer.admin.sitemap" %>

<asp:Content ID="ContentBody" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function confirmDelete() {
            var iscomfirm = false;
            iscomfirm = confirm("Are you sure you want to delete role ?");
            return iscomfirm;
        }
        function closeAddUpdateSiteMapModal() {
            $("#moAddUpdateSiteMap").modal('hide');
        }
        function showAddUpdateSiteMapModal() {
            var ddlValue = $("#MainContent_ddlApplilcation option:selected").text();
            $("#lblApplication").text(ddlValue);
            $("#moAddUpdateSiteMap").modal('show');
        }
        function pageLoad(sender, args) {
            var hv = $('#MainContent_hdnFlag').val();
            if (hv == "true") {
                closeAddUpdateSiteMapModal();
                $('#MainContent_hdnFlag').val("false");
            }
        }
    </script>
    <h1 class="page-header">Site Map</h1>
    <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
    <asp:UpdatePanel ID="udpSiteMap" runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <div class="input-group col-md-6 input-group-lg">
                    <label class="input-group-addon" for="ddlApplilcation"><strong>Manage Site Map For Application: </strong></label>
                    <asp:DropDownList ID="ddlApplilcation" OnSelectedIndexChanged="ddlApplilcation_SelectedIndexChanged" runat="server" AutoPostBack="true" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <br />
            <div class="row" id="divSiteMapDetails" runat="server" style="display: none;">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-sm-6">
                                <strong>Current Site Maps</strong>
                            </div>
                            <div class="col-sm-6">
                                <asp:Button runat="server" ID="btnNewCreate" OnClick="btnNewCreate_Click" OnClientClick="showAddUpdateSiteMapModal();" CssClass="btn btn-sm btn-primary pull-right" Text="Create New Site Map" />
                            </div>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="col-md-12">
                            <div id="dvMsg" runat="server" enableviewstate="false" style="display: none;"></div>
                        </div>
                        <div class="container" id="dvScroll" onscroll="setScrollPosition(this.scrollTop);">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:GridView ID="grdSiteMap" runat="server" AutoGenerateColumns="false"
                                        EmptyDataText="No Sitemap defined for the seleced application"
                                        DataKeyNames="SiteMap_ID" CssClass="table table-hover table-striped"
                                        OnRowCommand="grdSiteMap_RowCommand"
                                        OnRowDataBound="grdSiteMap_RowDataBound" SelectedRowStyle-CssClass="highlight">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                            <asp:BoundField DataField="Title" HeaderText="Title" />
                                            <asp:BoundField DataField="Description" HeaderText="Description" />
                                            <asp:BoundField DataField="Url" HeaderText="Url" />
                                            <asp:BoundField DataField="ParentTitle" HeaderText="Parent Node" />
                                            <asp:BoundField DataField="Roles" HeaderText="Roles" />
                                            <asp:BoundField DataField="IsSiteMapNode" HeaderText="SiteMapNode" />
                                            <asp:TemplateField ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" OnClientClick="showAddUpdateSiteMapModal();" CommandName="Select" CssClass="btn btn-default" Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("ID") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>' CssClass="btn btn-default" CommandArgument='<%# Bind("ID") %>'>
                                         <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                                        <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <%--<div class="col-lg-3">
                            <asp:TreeView ID="TreeView1" runat="server" ImageSet="Simple" NodeIndent="10" DataSourceID="AspNetSqlSiteMapProvider">
                                <HoverNodeStyle Font-Underline="True" ForeColor="#DD5555" />
                                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="0px"
                                    NodeSpacing="0px" VerticalPadding="0px"></NodeStyle>
                                <ParentNodeStyle Font-Bold="False" />
                                <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px"
                                    ForeColor="#DD5555" />
                            </asp:TreeView>
                            <asp:SiteMapDataSource ID="AspNetSqlSiteMapProvider" runat="server" ShowStartingNode="false" />

                        </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="modal fade" id="moAddUpdateSiteMap" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="input-group">
                        <h4 class="input-group-addon">Add / Update SiteMap Details For </h4>
                        <strong>
                            <label id="lblApplication" class="form-control"></label>
                        </strong>
                    </div>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="AddUpdRoleModal" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdnFlag" runat="server" Value="" EnableViewState="false" />
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="Role" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                    <div id="msgAlert" runat="server" style="display: none;"></div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">SiteMap Information</div>
                                        <div class="panel-body">
                                            <asp:FormView ID="frmSiteNode" runat="server" DataKeyNames="ID" DefaultMode="Insert" OnItemCommand="frmSiteNode_ItemCommand">
                                                <HeaderTemplate>
                                                    <div class="container">
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <div class="col-sm-6">
                                                                    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="SiteMap" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </HeaderTemplate>
                                                <InsertItemTemplate>
                                                    <div class="row">
                                                        <div class="col-lg-6">
                                                            <div class="form-group">
                                                                <label for="txtTitle">Title</label>

                                                                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" />
                                                                <asp:RequiredFieldValidator ID="rfvtxtTitle" runat="server" ControlToValidate="txtTitle" ErrorMessage="Please enter title" Text="*" ValidationGroup="SiteMap" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="ddlParent">Parent Node</label>
                                                                <asp:DropDownList ID="ddlParent" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txtUrl">URL</label>
                                                                <asp:TextBox ID="txtUrl" runat="server" CssClass="form-control" />
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="chkbIsSiteMapNode">SiteMap Node</label>
                                                                <asp:CheckBox ID="chkbIsSiteMapNode" runat="server" />
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txtDescription">Description</label>
                                                                <asp:TextBox ID="txtDescription" runat="server" Rows="5" TextMode="MultiLine" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <label for="chkListRoles">Roles</label>
                                                            <asp:CheckBoxList ID="chkListRoles" runat="server"></asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6">
                                                            <asp:LinkButton ID="btnAdd" runat="server" CommandName="Add" ValidationGroup="SiteMap" Text="Add Sitemap Node" CssClass="btn btn-primary btn-sm" />
                                                        </div>
                                                    </div>

                                                </InsertItemTemplate>
                                                <EditItemTemplate>
                                                    <div class="row">
                                                        <div class="col-lg-6">
                                                            <div class="form-group">
                                                                <asp:HiddenField ID="hdnSiteMapID" runat="server" />
                                                                <label for="txtTitle">Title</label>
                                                                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" />
                                                                <asp:RequiredFieldValidator ID="rfvtxtTitle" runat="server" ControlToValidate="txtTitle" ErrorMessage="Please enter title" Text="*" ValidationGroup="SiteMap" CssClass="text-danger"></asp:RequiredFieldValidator>

                                                            </div>

                                                            <div class="form-group">
                                                                <label for="ddlParent">Parent Node</label>
                                                                <asp:DropDownList ID="ddlParent" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group">
                                                                <label for="txtUrl">URL</label>
                                                                <asp:TextBox ID="txtUrl" runat="server" CssClass="form-control" />
                                                            </div>
                                                            <div class="form-group">
                                                                <asp:CheckBox ID="chkbIsSiteMapNode" Text="SiteMap Node" runat="server" />
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txtDescription">Description</label>
                                                                <asp:TextBox ID="txtDescription" runat="server" Rows="5" TextMode="MultiLine" CssClass="form-control" />
                                                            </div>

                                                        </div>
                                                        <div class="col-lg-6">
                                                            <label for="chkListRoles">Roles</label>
                                                            <asp:CheckBoxList ID="chkListRoles" runat="server"></asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6">
                                                            <asp:LinkButton ID="btnSave" runat="server" CommandName="Modify" Text="Update Sitemap Node" ValidationGroup="SiteMap" CssClass="btn btn-primary btn-sm" />
                                                        </div>
                                                    </div>
                                                </EditItemTemplate>
                                            </asp:FormView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
