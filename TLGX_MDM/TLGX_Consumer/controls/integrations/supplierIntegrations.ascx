<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="supplierIntegrations.ascx.cs" Inherits="TLGX_Consumer.controls.integrations.supplierIntegrations" %>
                <asp:GridView ID="grdSupplierIntegrations" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="SupplierIntegration_Id" HeaderText="SupplierIntegration_Id" SortExpression="SupplierIntegration_Id" />
                        <asp:BoundField DataField="Supplier_Id" HeaderText="Supplier_Id" SortExpression="Supplier_Id" />
                        <asp:BoundField DataField="IntegrationName" HeaderText="IntegrationName" SortExpression="IntegrationName" />
                        <asp:BoundField DataField="ProductCategory" HeaderText="ProductCategory" SortExpression="ProductCategory" />
                        <asp:BoundField DataField="ProductCategorySubType" HeaderText="ProductCategorySubType" SortExpression="ProductCategorySubType" />
                        <asp:BoundField DataField="Region" HeaderText="Region" SortExpression="Region" />
                        <asp:BoundField DataField="OwnerCompany" HeaderText="OwnerCompany" SortExpression="OwnerCompany" />
                        <asp:BoundField DataField="OwnerBusinessSPOC" HeaderText="OwnerBusinessSPOC" SortExpression="OwnerBusinessSPOC" />
                    </Columns>
                </asp:GridView>
