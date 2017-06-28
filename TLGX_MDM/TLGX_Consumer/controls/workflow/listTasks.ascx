<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="listTasks.ascx.cs" Inherits="TLGX_Consumer.controls.workflow.listTasks" %>
<asp:GridView ID="grdTaskList" runat="server" AutoGenerateColumns="False" EmptyDataText="No Tasks">
    <Columns>
        <asp:BoundField HeaderText="Name" DataField="Name" />
        <asp:BoundField HeaderText="Created" DataField="Create_Date" />
        <asp:HyperLinkField Text="View" />
    </Columns>
</asp:GridView>

