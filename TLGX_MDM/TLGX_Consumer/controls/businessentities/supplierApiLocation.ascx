<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="supplierApiLocation.ascx.cs" Inherits="TLGX_Consumer.controls.businessentities.supplierApiLocation" %>
<asp:UpdatePanel ID="panSupplierApiLoc" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div id="dvMsg" runat="server" style="display: none;"></div>

        <div class="col-sm-12">
            <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="supplierApiLoc" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
        </div>

        <div class="panel panel-default">
            <div class="panel-body">
                <div class="form-inline">

                    <div class="col-md-3">
                        <label for="ddlSupplierApiLocEntity">
                            Entity
                            <asp:RequiredFieldValidator ID="rfvSupApiLocEntity" ValidationGroup="supplierApiLoc" runat="server" ControlToValidate="ddlSupplierApiLocEntity"
                                CssClass="text-danger" ErrorMessage="Please select an Entity." InitialValue="0" Text="*" />
                        </label>
                        <asp:DropDownList ID="ddlSupplierApiLocEntity" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <div class="form-group row">
                            <label for="txtSupplierApiLocEndPoint" class="control-label col-sm-4">
                                Api Location
                                <asp:RequiredFieldValidator ID="rfvSupApiLocEndPoint" ValidationGroup="supplierApiLoc" runat="server" ControlToValidate="txtSupplierApiLocEndPoint"
                                    CssClass="text-danger" ErrorMessage="Please enter Api Location." Text="*" />
                            </label>
                            <asp:TextBox ID="txtSupplierApiLocEndPoint" runat="server" CssClass="form-control col-sm-8" />
                        </div>
                    </div>

                    <div class="col-md-3">
                        <label for="ddlSupplierApiLocStatus" class="control-label">
                            Status
                            <asp:RequiredFieldValidator ID="rfvSupApiLocStatus" ValidationGroup="supplierApiLoc" runat="server" ControlToValidate="ddlSupplierApiLocStatus"
                                CssClass="text-danger" ErrorMessage="Please select a Status." InitialValue="0" Text="*" />
                        </label>
                        <asp:DropDownList ID="ddlSupplierApiLocStatus" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-1">
                        <asp:LinkButton ID="lnkButtonAddUpdate" CommandName="Add" runat="server" CssClass="btn btn-primary btn-sm"
                            CausesValidation="true" ValidationGroup="supplierApiLoc" OnClick="lnkButtonAddUpdate_Click">Add</asp:LinkButton>
                    </div>

                    <div class="col-md-1">
                        <asp:LinkButton ID="lnkButtonReset" CommandName="Reset" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkButtonReset_Click">Reset</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>

        <asp:GridView ID="gvSupplierApiLoc" runat="server" EmptyDataText="No Api Location has been defined." CssClass="table table-hover table-striped"
            AutoGenerateColumns="false" DataKeyNames="ApiLocation_Id" OnRowCommand="gvSupplierApiLoc_RowCommand">
            <Columns>
                <asp:BoundField HeaderText="Entity" DataField="Entity" />
                <asp:BoundField HeaderText="API Path" DataField="ApiEndPoint" />
                <asp:BoundField HeaderText="Status" DataField="Status" />
                <asp:BoundField HeaderText="Created By" DataField="Create_User" />
                <asp:BoundField HeaderText="Created Date" DataField="Create_Date" />
                <asp:BoundField HeaderText="Last Edited By" DataField="Edit_User" />
                <asp:BoundField HeaderText="Last Edited Date" DataField="Edit_Date" />
                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Select" CommandArgument='<%#Bind("ApiLocation_Id") %>' CssClass="btn btn-default" Enabled="true">
                                            <span aria-hidden="true">Edit</span>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pagination-ys" />
        </asp:GridView>
    </ContentTemplate>
</asp:UpdatePanel>

