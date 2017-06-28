<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="suppliermapping.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.suppliermapping" %>
<asp:GridView ID="grdPotentialMapping" runat="server" AutoGenerateColumns="False" DataKeyNames="map_id" DataSourceID="ObjectDataSource1" CssClass="table table-hover table-striped" AllowPaging="True" PageSize="5">
    <Columns>
        <asp:BoundField DataField="supplier" HeaderText="supplier" SortExpression="supplier" />
        <asp:BoundField DataField="sup_code" HeaderText="sup_code" SortExpression="sup_code" />

        <asp:CommandField ShowSelectButton="True" />

    </Columns>
</asp:GridView>    



<h4>Existing Mapping</h4>
<p>Grid will contains confirmed maps for Supplier Product to TLGX Product</p>
<asp:GridView ID="grdExistingMapping" runat="server" AutoGenerateColumns="False" DataKeyNames="map_id" DataSourceID="ObjectDataSource1" CssClass="table table-hover table-striped" AllowPaging="True">
    <Columns>
        <asp:BoundField DataField="supplier" HeaderText="supplier" SortExpression="supplier" />
        <asp:BoundField DataField="sup_code" HeaderText="sup_code" SortExpression="sup_code" />

        <asp:CommandField ShowSelectButton="True" />

    </Columns>
</asp:GridView>      
    </ContentTemplate>
</asp:UpdatePanel>