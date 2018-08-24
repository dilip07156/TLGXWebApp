<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Prices.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.Prices" %>
<script>
    function showPricesModal() {
        $("#moPrices").modal('show');
    }
    function closeDescriptionModal() {
        $("#moPrices").modal('hide');
    }
    function pageLoad(sender, args) {
        var hv = $('#MainContent_Prices_hdnFlag').val();
        //alert(hv + " : : HiddenFlag")
        if (hv == "true") {
            closeDescriptionModal();
            $('#MainContent_Prices_hdnFlag').val("false");
        }
    }
</script>
<%--<div class="panel panel-default" id="searchResult">
    <div class="panel-heading clearfix">--%>
<h4 class="panel-title pull-left">Prices Details (Total Count:
                            <asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label>)</h4>
<asp:Button CssClass="pull-right btn btn-primary" runat="server" ID="btnNewUpload" OnClick="btnNewUpload_Click" Text="Add New Price" OnClientClick="showPriceModal()" Visible="false"/>
<div class="col-lg-3 pull-right">
    <div class="form-group pull-right">
        <div class="input-group" runat="server" id="divDropdownForEntries">
            <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
            <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged">
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>25</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
                <asp:ListItem>100</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
</div>

<asp:GridView ID="gvPricesSearch" runat="server" AllowPaging="True" AllowCustomPaging="true"
    EmptyDataText="No data Found" CssClass="table table-hover table-striped" 
    AutoGenerateColumns="false" OnPageIndexChanging="gvPricesSearch_PageIndexChanging"
    OnRowCommand="gvPricesSearch_RowCommand" DataKeyNames="Activity_Prices_Id" OnRowDataBound="gvPricesSearch_RowDataBound">
    <Columns>
        <asp:BoundField HeaderText="Price Currency" DataField="PriceCurrency" />
        <asp:BoundField HeaderText="Price" DataField="Price" />
        <asp:BoundField HeaderText="Price For" DataField="Price_For" />
        <asp:BoundField HeaderText="Price Code" DataField="PriceCode" />
        <asp:BoundField HeaderText="Price Basis" DataField="PriceBasis" />
        <asp:BoundField HeaderText="Price Type" DataField="Price_Type" />
        <asp:BoundField HeaderText="Price OptionCode" DataField="Price_OptionCode" />
        <asp:BoundField HeaderText="Price InternalOptionCode" DataField="Price_InternalOptionCode" />
        <asp:BoundField HeaderText="Market" DataField="Market" />
        <asp:BoundField HeaderText="FromPax" DataField="FromPax" />
        <asp:BoundField HeaderText="ToPax" DataField="ToPax" />
        <asp:BoundField HeaderText="Person Type" DataField="PersonType" />
        <asp:BoundField HeaderText="AgeFrom" DataField="AgeFrom" />
        <asp:BoundField HeaderText="AgeToe" DataField="AgeTo" />
        <asp:BoundField HeaderText="Price ValidFrom" DataField="Price_ValidFrom" HtmlEncode="False" DataFormatString="{0:d}" />
        <asp:BoundField HeaderText="Price ValidTo" DataField="Price_ValidTo" HtmlEncode="False" DataFormatString="{0:d}" />
    </Columns>
    <PagerStyle CssClass="pagination-ys" />
</asp:GridView>
