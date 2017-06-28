<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="supplierCityMapping.ascx.cs" Inherits="TLGX_Consumer.controls.geography.supplierCityMapping" %>
                <asp:GridView ID="grdCityMapping" runat="server" AutoGenerateColumns="False" DataKeyNames="CityMapping_Id"   CssClass="table table-hover table-striped" EmptyDataText="No Mapped Cities">
                    <Columns>
                        <asp:BoundField DataField="CityMapping_Id" HeaderText="CityMapping_Id" ReadOnly="True" SortExpression="CityMapping_Id" />
                        <asp:BoundField DataField="Country_Id" HeaderText="Country_Id" SortExpression="Country_Id" />
                        <asp:BoundField DataField="City_Id" HeaderText="City_Id" SortExpression="City_Id" />
                        <asp:BoundField DataField="CityName" HeaderText="CityName" SortExpression="CityName" />
                        <asp:BoundField DataField="CityCode" HeaderText="CityCode" SortExpression="CityCode" />
                        <asp:BoundField DataField="Supplier_Id" HeaderText="Supplier_Id" SortExpression="Supplier_Id" />
                    </Columns>
                </asp:GridView>
