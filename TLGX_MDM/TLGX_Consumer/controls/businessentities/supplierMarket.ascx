<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="supplierMarket.ascx.cs" Inherits="TLGX_Consumer.controls.businessentities.supplierMarket" %>
<asp:UpdatePanel ID="panSupplierMarkets" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div id="dvMsg" runat="server" style="display: none;"></div>
        <div class="panel panel-default">
            <div class="panel-body">
                <asp:FormView ID="frmSupplierMarket" runat="server" DataKeyNames="Supplier_Market_Id" DefaultMode="Insert" OnItemCommand="frmSupplierMarket_ItemCommand">
                    <InsertItemTemplate>
                        <div class="form-inline">
                            <label for="txtSupplierMarketName">Name</label>
                            <asp:TextBox ID="txtSupplierMarketName" runat="server" CssClass="form-control" />
                            <label for="txtSupplierMarketCode">Code</label>
                            <asp:TextBox ID="txtSupplierMarketCode" runat="server" CssClass="form-control" />
                            <asp:LinkButton ID="lnkButton" CommandName="Add" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="true">Add</asp:LinkButton>
                        </div>
                    </InsertItemTemplate>
                    <EditItemTemplate>
                        <div class="form-inline">
                            <label for="txtSupplierMarketName">Name</label>
                            <asp:TextBox ID="txtSupplierMarketName" runat="server" CssClass="form-control" Text='<%# Bind("Name") %>' />
                            <label for="txtSupplierMarketCode">Code</label>
                            <asp:TextBox ID="txtSupplierMarketCode" runat="server" CssClass="form-control" Text='<%# Bind("Code") %>' />
                            <asp:LinkButton ID="lnkButton" CommandName="Modify" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="true">Modify</asp:LinkButton>
                        </div>
                    </EditItemTemplate>
                </asp:FormView>
            </div>
        </div>
        <br />
        <asp:GridView ID="grdSupplierMarkets" runat="server" AutoGenerateColumns="False" DataKeyNames="Supplier_Market_Id" EmptyDataText="No Supplier Markets specified" CssClass="table table-hover table-striped" OnRowCommand="grdSupplierMarkets_RowCommand" OnRowDataBound="grdSupplierMarkets_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Code" HeaderText="Code" />
                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                            Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Supplier_Market_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>' CssClass="btn btn-default" CommandArgument='<%# Bind("Supplier_Market_Id") %>'>
                                         <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                                        <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </ContentTemplate>
</asp:UpdatePanel>
