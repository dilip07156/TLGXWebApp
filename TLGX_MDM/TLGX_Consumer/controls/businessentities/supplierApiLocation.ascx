<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="supplierApiLocation.ascx.cs" Inherits="TLGX_Consumer.controls.businessentities.supplierApiLocation" %>
<asp:UpdatePanel ID="panSupplierApiLoc" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div id="dvMsg" runat="server" style="display: none;"></div>
        <div class="panel panel-default">
            <div class="panel-body">

                <asp:FormView ID="frmSupplierApiLoc" runat="server" DataKeyNames="ApiLocation_Id" DefaultMode="Insert" OnItemCommand="frmSupplierApiLoc_ItemCommand">
                    <InsertItemTemplate>
                        <div class="form-inline">
                            <label for="ddlSupplierApiLocEntity">Entity</label>
                            <asp:DropDownList ID="ddlSupplierApiLocEntity" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                            </asp:DropDownList>

                            <label for="txtSupplierApiLocEndPoint">Api Location</label>
                            <asp:TextBox ID="txtSupplierApiLocEndPoint" runat="server" CssClass="form-control" />

                            <label for="ddlSupplierApiLocStatus">Status</label>
                            <asp:DropDownList ID="ddlSupplierApiLocStatus" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                            </asp:DropDownList>

                            <asp:LinkButton ID="lnkButton" CommandName="Add" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="true">Add</asp:LinkButton>
                        </div>
                    </InsertItemTemplate>
                    <EditItemTemplate>
                        <div class="form-inline">
                            <label for="ddlSupplierApiLocEntity">Entity</label>
                            <asp:DropDownList ID="ddlSupplierApiLocEntity" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                            </asp:DropDownList>

                            <label for="txtSupplierApiLocEndPoint">Api Location</label>
                            <asp:TextBox ID="txtSupplierApiLocEndPoint" runat="server" CssClass="form-control" />

                            <label for="ddlSupplierApiLocStatus">Status</label>
                            <asp:DropDownList ID="ddlSupplierApiLocStatus" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                            </asp:DropDownList>

                            <asp:LinkButton ID="lnkButton" CommandName="Modify" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="true">Modify</asp:LinkButton>
                        </div>
                    </EditItemTemplate>
                </asp:FormView>

            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:GridView ID="gvNewTabData" runat="server" AllowPaging="True" AllowCustomPaging="true"
    EmptyDataText="No Data Found" CssClass="table table-hover table-striped"
    AutoGenerateColumns="false" DataKeyNames="ApiLocation_Id">
    <Columns>
        <asp:BoundField HeaderText="Entity" DataField="Entity" />
        <asp:BoundField HeaderText="Path" DataField="ApiEndPoint" />
        <asp:BoundField HeaderText="Status" DataField="Status" />
        <asp:BoundField HeaderText="CreatedBy" DataField="Create_User" />
        <asp:BoundField HeaderText="CreatedDate" DataField="Create_Date" />
        <asp:BoundField HeaderText="LastEditedBy" DataField="Edit_User" />
        <asp:BoundField HeaderText="LasteEditedDate" DataField="Edit_Date" />
        <asp:TemplateField ShowHeader="false">
            <ItemTemplate>
                <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Edit" CssClass="btn btn-default" Enabled="true">
                                            <span aria-hidden="true">Edit</span>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <PagerStyle CssClass="pagination-ys" />
</asp:GridView>
