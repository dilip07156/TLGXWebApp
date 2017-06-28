<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="supplierCountryMapping.ascx.cs" Inherits="TLGX_Consumer.controls.geography.supplierCountryMapping" %>

                <asp:GridView ID="grdCountryMapping" runat="server" AutoGenerateColumns="False" DataKeyNames="CountryMapping_Id"  CssClass="table table-hover table-striped" EmptyDataText="No Mapped Countries">
                    <Columns>
                        <asp:BoundField DataField="Supplier_Name" HeaderText="Supplier" SortExpression="Supplier_Name" />
                        <asp:BoundField DataField="CountryName" HeaderText="Country Name" SortExpression="CountryName" />
                        <asp:BoundField DataField="CountryCode" HeaderText="Country Code" SortExpression="CountryCode" />
                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                </asp:GridView>