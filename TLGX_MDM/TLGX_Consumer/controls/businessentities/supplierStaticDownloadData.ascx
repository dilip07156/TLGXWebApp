<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="supplierStaticDownloadData.ascx.cs" Inherits="TLGX_Consumer.controls.businessentities.supplierStaticDownloadData" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:UpdatePanel ID="supplerStaticDownloadData" runat="server">
    <ContentTemplate>
        <script type="text/javascript">

            function confirmDelete() {
                var iscomfirm = false;
                iscomfirm = confirm("Are you sure you want to delete row ?");
                return iscomfirm;
            }
        </script>

        <div id="dvMsg" runat="server" style="display: none;"></div>

        <div class="col-sm-12">
            <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="suppStaticDownloadData" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
        </div>

        <div class="panel panel-default">
            <div class="panel-heading">
                Add Supplier Static Download Data
            </div>

            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group row">
                            <label class="control-label col-sm-2" for="txtUrl">URL</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtUrl" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSupplier_Url" ValidationGroup="suppStaticDownloadData" runat="server" ControlToValidate="txtUrl" CssClass="text-danger" ErrorMessage="Please enter Url." Text="*" />
                            </div>
                        </div>
                        <br />
                        <div class="form-group row">
                            <label class="control-label col-sm-2" for="txtUsername">Username</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <br />

                        <div class="form-group row">
                            <label class="control-label col-sm-2" for="txtPassword">Password</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <br />

                        <div class="col-md-6">
                            <div class="col-md-3">
                                <asp:LinkButton ID="lnkButtonAddUpdate" CommandName="Add" runat="server" CssClass="btn btn-primary btn-sm"
                                    CausesValidation="true" ValidationGroup="suppStaticDownloadData" OnClick="lnkButtonAddUpdate_Click">Add</asp:LinkButton>
                            </div>
                            <div class="col-md-3">
                                <asp:LinkButton ID="lnkButtonReset" CommandName="Reset" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkButtonReset_Click">Reset</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group row">
                            <label class="control-label col-sm-2" for="txtPassword">Description</label>
                            <div class="col-sm-10">
                                <textarea id="txtDescription" runat="server" cssclass="form-control" cols="80" rows="10"></textarea>
                                <asp:RequiredFieldValidator ID="rfvSupplier_Description" ValidationGroup="suppStaticDownloadData" runat="server" ControlToValidate="txtDescription"
                                    CssClass="text-danger" ErrorMessage="Please enter Description." Text="*" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>


        <h4 class="panel-title pull-left" hidden>Description Details (Total Count:
                            <asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label>)</h4>
        <div class="container">
            <div class="row">
                <div class="col-lg-3 pull-right">
                    <div class="form-group pull-right">
                        <div class="input-group" runat="server" id="divDropdownForEntries">
                            <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                            <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged">
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:GridView ID="gvSupplerStaticDownloadData" runat="server"  AllowPaging="True" AllowCustomPaging="true" EmptyDataText="No Suppler Static Download Data Available." CssClass="table table-hover table-striped" 
           OnPageIndexChanging="gvSupplerStaticDownloadData_PageIndexChanging"
           AutoGenerateColumns="false" DataKeyNames="SupplierCredentialsId" 
            OnRowCommand="gvSupplerStaticDownloadData_RowCommand" OnRowDataBound="gvSupplerStaticDownloadData_RowDataBound">

            <Columns>
                <asp:BoundField HeaderText="URL" DataField="URL" />
                <asp:BoundField HeaderText="Username" DataField="Username" />
                <asp:BoundField HeaderText="Password" DataField="Password" />
                <asp:BoundField HeaderText="Description" DataField="Description" />
                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Select" CommandArgument='<%#Bind("SupplierCredentialsId") %>' CssClass="btn btn-default" Enabled="true">
                                            <span aria-hidden="true">Edit</span>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>

                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>' CssClass="btn btn-default" CommandArgument='<%# Bind("SupplierCredentialsId") %>'>
                                         <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'></span>
                                        <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pagination-ys" />
        </asp:GridView>

    </ContentTemplate>
</asp:UpdatePanel>


