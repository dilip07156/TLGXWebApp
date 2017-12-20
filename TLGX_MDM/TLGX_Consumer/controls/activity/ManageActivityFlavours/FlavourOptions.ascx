<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FlavourOptions.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.FlavourOptions" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:UpdatePanel ID="updPanFlavourOptions" runat="server">
    <ContentTemplate>
        <div id="dvMsg" runat="server" style="display: none;"></div>

        <div class="form-group">
            <h4 class="panel-title pull-left">Flavour Options (Total Count:
            <asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label>)</h4>
        </div>

        <div class="row col-lg-3 pull-right">
            <div class="form-group pull-right">
                <div class="input-group" runat="server" id="divDropdownForEntries">
                    <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                    <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" CssClass="form-control">
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <asp:GridView ID="gvActFlavourOptins" runat="server" AllowPaging="True" AllowCustomPaging="true"
            EmptyDataText="No Data Found" CssClass="table table-hover table-striped"
            AutoGenerateColumns="false" OnPageIndexChanging="gvActFlavourOptins_PageIndexChanging"
            OnRowCommand="gvActFlavourOptins_RowCommand" DataKeyNames="Activity_FlavourOptions_Id"
            OnRowDataBound="gvActFlavourOptins_RowDataBound">
            <Columns>
                <asp:BoundField HeaderText="Flavour Name" DataField="Activity_FlavourName" />
                <asp:BoundField HeaderText="Option Name" DataField="Activity_OptionName" />
                <asp:BoundField HeaderText="Option Code" DataField="Activity_OptionCode" />
                <asp:BoundField HeaderText="Option Description" DataField="Activity_OptionDescription" />
            </Columns>
            <PagerStyle CssClass="pagination-ys" />
        </asp:GridView>

    </ContentTemplate>
</asp:UpdatePanel>
