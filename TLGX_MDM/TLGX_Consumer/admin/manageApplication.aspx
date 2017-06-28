<%@ Page Title="Applications" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manageApplication.aspx.cs" Inherits="TLGX_Consumer.admin.manageApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function CloseAddUpdateApplicationModal() {
            $("#moAddUpdateApplication").modal('hide');
        }
        function showAddUpdateApplicationModal() {
            $("#moAddUpdateApplication").modal('show');
            if ($('#MainContent_msgAlert').css('display') == 'block')
                $("#MainContent_msgAlert").removeAttr("style");
        }
        function pageLoad(sender, args) {
            var hv = $('#MainContent_hdnFlag').val();
            if (hv == "true") {
                CloseAddUpdateApplicationModal();
                $('#MainContent_hdnFlag').val("false");
                if ($('#MainContent_msgAlert').css('display') == 'block')
                    $("#MainContent_msgAlert").removeAttr("style");
            }
        }
    </script>
    <h2 class="page-header"><%: Title %></h2>
    <asp:UpdatePanel ID="updApplicationGrid" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-sm-6">
                                    <strong>Current Applications</strong>
                                </div>
                                <div class="col-sm-6">
                                    <asp:Button runat="server" ID="btnNewCreate" OnClick="btnNewCreate_Click" OnClientClick="showAddUpdateApplicationModal();" CssClass="btn btn-sm btn-primary pull-right" Text="Create New Application" />
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div id="dvMsg" runat="server" style="display: none;"></div>

                            <div class="form-group pull-right">
                                <div class="input-group">
                                    <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                                    <asp:DropDownList ID="ddlShowEntries" runat="server" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <asp:GridView ID="grdListOfApplication" DataKeyNames="ApplicationId" runat="server" AllowPaging="true" AllowCustomPaging="true" OnPageIndexChanging="grdListOfApplication_PageIndexChanging" CssClass="table table-hover table-striped" EmptyDataText="No Applicaton in the System" AutoGenerateColumns="False"
                                OnRowCommand="grdListOfApplication_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="ApplicationId" HeaderText="Application" Visible="false" />
                                    <asp:BoundField DataField="ApplicationName" HeaderText="Application Name" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" />

                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default" OnClientClick="showAddUpdateApplicationModal();"
                                                CommandArgument='<%# Bind("ApplicationId") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade" id="moAddUpdateApplication" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Add / Update Application Details</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="AddUpdApplicationModal" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdnFlag" runat="server" Value="" EnableViewState="false" />

                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="Application" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                    <div id="msgAlert" runat="server" style="display: none;"></div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">Application Information</div>
                                        <div class="panel-body">
                                            <asp:FormView ID="frmApplicationdetail" DataKeyNames="ApplicationId" runat="server" DefaultMode="Insert" OnItemCommand="frmApplicationdetail_ItemCommand">
                                                <EditItemTemplate>

                                                    <div class="form-group row">
                                                        <label for="txtApplicationName" class="control-label col-sm-6">
                                                            Application Name
                                                                <asp:RequiredFieldValidator ValidationGroup="Application" runat="server" ControlToValidate="txtApplicationName"
                                                                    CssClass="text-danger" ErrorMessage="The application name is required." Text="*" />
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" ID="txtApplicationName" CssClass="form-control" />

                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="txtDescription" class="control-label col-sm-6">Description</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" TextMode="MultiLine" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <div class="col-sm-6">
                                                            <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="Save" CssClass="btn btn-primary" ValidationGroup="Application" />
                                                        </div>
                                                    </div>

                                                </EditItemTemplate>
                                                <InsertItemTemplate>
                                                    <div class="form-group row">
                                                        <label for="txtApplicationName" class="control-label col-sm-6">
                                                            Application Name
                                                                <asp:RequiredFieldValidator ValidationGroup="Application" runat="server" ControlToValidate="txtApplicationName"
                                                                    CssClass="text-danger" ErrorMessage="The application name is required." Text="*" />
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" ID="txtApplicationName" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <label for="txtDescription" class="control-label col-sm-6">Description</label>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" TextMode="MultiLine" />
                                                            <div class="clear">&nbsp;</div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group row">
                                                        <div class="col-sm-6">
                                                            <asp:Button ID="btnAddApplication" runat="server" CommandName="Add" Text="Add" CssClass="btn btn-primary" ValidationGroup="Application" />
                                                        </div>
                                                    </div>
                                                </InsertItemTemplate>
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
