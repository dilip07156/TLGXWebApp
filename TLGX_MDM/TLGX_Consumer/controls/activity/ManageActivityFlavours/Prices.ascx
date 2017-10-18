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
<div class="panel panel-default"  id="searchResult">
                <div class="panel-heading clearfix">
                    <h4 class="panel-title pull-left">
                        <a data-toggle="collapse" data-parent="#searchResult" href="#collapseSearchResult">Prices Details (Total Count:
                            <asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label>)</a></h4>
                    <asp:Button CssClass="pull-right btn btn-primary" runat="server" ID="btnNewUpload" OnClick="btnNewUpload_Click" Text="Add New Price" OnClientClick="showPriceModal()" />
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

                </div>
                <div id="collapseSearchResult" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <div id="dvMsg" runat="server" style="display: none;"></div>
                        <asp:GridView ID="gvPricesSearch" runat="server" AllowPaging="True" AllowCustomPaging="true"
                            EmptyDataText="No data Found" CssClass="table table-hover table-striped"
                            AutoGenerateColumns="false" OnPageIndexChanging="gvPricesSearch_PageIndexChanging"
                            OnRowCommand="gvPricesSearch_RowCommand" DataKeyNames="Activity_Description_Id" OnRowDataBound="gvPricesSearch_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="Price Code" DataField="PriceCode"  />
                                <asp:BoundField HeaderText="Price Basis" DataField="PriceBasis" />
                                <asp:BoundField HeaderText="Price Net" DataField="PriceNet" />
                                <asp:BoundField HeaderText="Price Currency" DataField="PriceCurrency" />
                                <asp:BoundField HeaderText="Created Date" DataField="Create_Date" DataFormatString="{0:dd/MM/yyyy} " />
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                            Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Activity_Prices_Id") %>' OnClientClick="showPricesModal()">
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false" HeaderStyle-CssClass="Info">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                                            CssClass="btn btn-default" CommandArgument='<%# Bind("Activity_Description_Id") %>'>
                                                    <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat" %>'></span>
                                                    <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                    </div>
                </div>

            </div>