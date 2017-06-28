<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="bulkHotelMapping.ascx.cs" Inherits="TLGX_Consumer.controls.staticdata.bulkHotelMapping" %>
<script src="../../Scripts/JqueryUI/jquery-ui.js"></script>
<link href="../../Scripts/JqueryUI/jquery-ui.css" rel="stylesheet" />
<script type="text/javascript">
    function ShowSearchPanel(controlID) {
        var str = controlID.id;
        var panelSearch = document.getElementById(str.replace("btnManualSearch", "pnlManualSearch"));
        var panelGrid = document.getElementById(str.replace("btnManualSearch", "pnlSupDumpGrid"));
    }

</script>
<style>
    .ColumnHide {
        display: none;
    }
</style>
<!-- might need multiple update panels -->
<asp:UpdatePanel ID="panHotelSearch" runat="server">
    <ContentTemplate>


        <div class="navbar" id="dvLinks" runat="server">
            <div class="navbar-inner">
                <ul class="nav nav-tabs">
                    <li class="active"><a href="#panProductSearch" data-toggle="tab">Search by Product</a></li>
                    <li><a href="#panSupplierSearch" data-toggle="tab">Search by Supplier</a></li>

                </ul>
            </div>
        </div>


        <div class="tab-content">
            <div class="tab-pane fade in" id="panSupplierSearch" runat="server">

                <div class="row">
                    <div class="col-lg-4">




                        <div class="form-group">
                            <label class="control-label col-sm-4" for="ddlSupplierName">
                                Supplier Name
                            </label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Value="">-Select-</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>

                    <div class="col-lg-4">
                        <div class="form-group">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" CausesValidation="false" />
                        </div>
                    </div>

                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <br />
                        <h4>Supplier Dump - Unmapped Products</h4>
                        <hr />
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-sm" Text="Map All" OnClick="btnSearch_Click" />
                        <asp:Button ID="Button3" runat="server" CssClass="btn btn-primary btn-sm" Text="Map Selected" CausesValidation="false" />
                        <asp:Button ID="Button4" runat="server" CssClass="btn btn-primary btn-sm" Text="Manual Search" CausesValidation="false" />

                        <br />
                        <asp:GridView ID="grdSupplierPending" runat="server" DataKeyNames="Accommodation_ProductMapping_Id" AutoGenerateColumns="False" CssClass="table table-hover table-striped" AllowPaging="true" OnRowCommand="grdSupplierPending_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="SupplierName" HeaderText="Supplier" />
                                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                <asp:BoundField DataField="SupplierProductReference" HeaderText="ID" />
                                <asp:BoundField DataField="TelephoneNumber" HeaderText="Phone" />
                                <asp:BoundField DataField="CountryName" HeaderText="COuntry" />
                                <asp:BoundField DataField="CityName" HeaderText="City" />
                                <asp:BoundField DataField="PostCode" HeaderText="Postal Code" />
                                <asp:CheckBoxField HeaderText="Select" />
                                <asp:ButtonField HeaderText="Select" Text="Map" CommandName="Map" ControlStyle-CssClass="btn btn-primary btn-sm" />
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <br />
                        <h4>Hotel Products</h4>
                        <hr />
                        <asp:GridView ID="grdHotelToMap" runat="server" AllowCustomPaging="true" DataKeyNames="AccomodationId" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="No Static Updates" CssClass="table table-hover table-striped" OnPageIndexChanging="grdProductSearch_PageIndexChanging" OnRowCommand="grdProductSearch_RowCommand">
                            <Columns>
                                <asp:BoundField HeaderText="Id" DataField="CompanyHotelId" />
                                <asp:BoundField DataField="Country" HeaderText="Country Name" />
                                <asp:BoundField DataField="City" HeaderText="City Name" />
                                <asp:BoundField DataField="HotelName" HeaderText="Hotel Name" />
                                <asp:BoundField HeaderText="Chain" />
                                <asp:BoundField HeaderText="Brand" />
                                <asp:BoundField HeaderText="Location" />
                                <asp:BoundField HeaderText="Area" />
                                <asp:ButtonField HeaderText="Map" Text="Select" CommandName="Select" ControlStyle-CssClass="btn btn-primary btn-sm" />
                                <asp:ButtonField HeaderText="Map" Text="Map" CommandName="Map" ControlStyle-CssClass="btn btn-primary btn-sm" />
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" HorizontalAlign="Left" />
                        </asp:GridView>



                    </div>
                </div>
            </div>

            <div class="tab-pane active" id="panProductSearch" runat="server">

                <div class="row" id="dvProdSearch" runat="server">

                    <div class="col-lg-4">




                        <div class="form-group">
                            <label class="control-label col-sm-4" for="ddlCountry">
                                Country
                            </label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="">-Select-</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-sm-4" for="ddlCity">
                                City
                            </label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Value="">-Select-</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-sm-4" for="ddlChain">
                                Chain
                            </label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlChain" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Value="">-Select-</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-sm-4" for="ddlBrand">
                                Brand
                            </label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlBrand" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Value="">-Select-</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-sm-4" for="txtProductName">
                                Product Name
                            </label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-sm-4" for="ddlProductStatus">
                                Product Status
                            </label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlProductStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Value="">-Select-</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>


                    </div>

                    <div class="col-lg-4">

                        <div class="form-group">
                            <label class="control-label col-sm-4" for="ddlProductBasedPageSize">Page Size</label>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlProductBasedPageSize" runat="server" CssClass="form-control col-lg-3" AutoPostBack="true">
                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="25" Value="25" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="form-group">

                            <asp:Button ID="btnSearchHotels" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearchHotels_Click" />
                            <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" CausesValidation="false" />

                        </div>


                        <br />
                        <br />



                    </div>

                </div>

                <div class="row" id="dvProdResult" runat="server">
                    <div class="col-lg-12">

                        <br />

                        <h4>Travel ERP (Total Count:
                            <asp:Label ID="lblProductSearch" runat="server" Text="0"></asp:Label>)</h4>
                        <hr />
                        <asp:GridView ID="grdProductSearch" runat="server" AllowCustomPaging="true" DataKeyNames="AccomodationId" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="No Static Updates" CssClass="table table-hover table-striped" OnPageIndexChanging="grdProductSearch_PageIndexChanging" OnRowCommand="grdProductSearch_RowCommand">
                            <Columns>
                                <asp:BoundField HeaderText="Id" DataField="CompanyHotelId" />
                                <asp:BoundField DataField="Country" HeaderText="Country Name" />
                                <asp:BoundField DataField="City" HeaderText="City Name" />
                                <asp:BoundField DataField="HotelName" HeaderText="Hotel Name" />
                                <asp:BoundField HeaderText="Chain" />
                                <asp:BoundField HeaderText="Brand" />
                                <asp:BoundField HeaderText="Location" />
                                <asp:BoundField HeaderText="Area" />
                                <asp:ButtonField HeaderText="Map" Text="Map" CommandName="Map" ControlStyle-CssClass="btn btn-primary btn-sm" />
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" HorizontalAlign="Left" />
                        </asp:GridView>


                    </div>
                </div>
                <div class="panel-group" id="accordionProdSupDump">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" data-parent="#accordion" href="#collapseProdSupDump">Supplier Dump (Total Count:
                                        <asp:Label ID="lblSupDump" runat="server" Text="0"></asp:Label>)</a>
                            </h4>
                        </div>
                        <div id="collapseProdSupDump" class="panel-collapse collapse in">
                            <div class="panel-body">
                                <div class="row" id="dvProdSupDump" runat="server">
                                    <div class="col-lg-12">
                                        <div class="col-lg-9">
                                            <table class="table table-hover table-bordered ">
                                                <tr>
                                                    <th>System Country</th>
                                                    <th>System City</th>
                                                    <th>System Hotel</th>
                                                    <th>Telephone</th>
                                                    <th>Address</th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblSupCountry" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSupCity" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSupProductName" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSupTelephone" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSupAddress" runat="server"></asp:Label>

                                                        <%-- <textarea id="lblSupAddress" runat="server" rows="5" readonly="readonly" class="word-wrap: break-word;"></textarea>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="col-lg-3">
                                            <div style="text-align: right">
                                                <asp:Button ID="btnMap" runat="server" CssClass="btn btn-primary btn-sm" Text="Map Selected" OnClick="btnMap_Click" />
                                                <asp:Button ID="btnMapAll" runat="server" CssClass="btn btn-primary btn-sm" Text="Map All" CausesValidation="false" OnClick="btnMapAll_Click" />
                                                <asp:Button ID="btnManualSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Manual Search" CausesValidation="false"
                                                    OnClientClick="ShowSearchPanel(this);" OnClick="btnManualSearch_Click" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="col-lg-12 row">
                                            <div class="panel-group">
                                                <div class="panel panel-default">
                                                    <div class="panel-body">
                                                        <br />
                                                        <asp:Panel ID="pnlManualSearch" runat="server">
                                                            <br />
                                                            <br />
                                                            <div class="container">
                                                                <div class="row">
                                                                    <div class="col-lg-4">

                                                                        <div class="form-group row">
                                                                            <label class="control-label col-sm-4" for="txtSupCountry">System Country</label>
                                                                            <div class="col-sm-8">
                                                                                <asp:TextBox ID="txtSupCountry" runat="server" CssClass="form-control col-lg-3"></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group row">
                                                                            <label class="control-label col-sm-4" for="txtSupCity">System City</label>
                                                                            <div class="col-sm-8">
                                                                                <asp:TextBox ID="txtSupCity" runat="server" CssClass="form-control col-lg-3"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group row">
                                                                            <label class="control-label col-sm-4" for="txtSupProductName">Suppliler Hotel</label>
                                                                            <div class="col-sm-8">
                                                                                <asp:TextBox ID="txtSupProductName" runat="server" CssClass="form-control col-lg-3"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-4">
                                                                        <div class="form-group row">
                                                                            <label class="control-label col-sm-4" for="txtSupAddress">Address</label>
                                                                            <div class="col-sm-8">
                                                                                <asp:TextBox ID="txtSupAddress" runat="server" CssClass="form-control col-lg-3"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4" for="txtSupTelephone">Telephone</label>
                                                                            <div class="col-sm-8">
                                                                                <asp:TextBox ID="txtSupTelephone" runat="server" CssClass="form-control col-lg-3"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-lg-4">
                                                                        <div class="form-group">
                                                                            <label class="control-label col-sm-4" for="ddlSupCountry">Page Size</label>
                                                                            <div class="col-sm-4">
                                                                                <asp:DropDownList ID="ddlPageSizeSupDump" runat="server" CssClass="form-control col-lg-1" AutoPostBack="true">
                                                                                    <asp:ListItem Text="5" Value="5" Selected="True"></asp:ListItem>
                                                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="col-sm-8">
                                                                                <asp:Button ID="btnSupManualSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSupManualSearch_Click" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />

                                            <asp:Panel ID="pnlSupDumpGrid" runat="server">
                                                <div id="divMsgForMapping" runat="server" style="display: none;"></div>
                                                <!-- first run will be an auto lookup against the product detail -->
                                                <asp:GridView ID="grdSupplierDump" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-striped"
                                                    DataKeyNames="Accommodation_ProductMapping_Id,Accommodation_Id" AllowCustomPaging="true" AllowPaging="true" EmptyDataText="No Data Found"
                                                    OnPageIndexChanging="grdSupplierDump_PageIndexChanging">
                                                    <Columns>
                                                        <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                                                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                                        <asp:BoundField DataField="SupplierProductReference" HeaderText="Supplier ID" />
                                                        <asp:BoundField DataField="TelephoneNumber" HeaderText="Phone" />
                                                        <asp:BoundField DataField="FullAddress" HeaderText="Address" />
                                                        <%--<asp:BoundField DataField="CountryName" HeaderText="Country" />
                                    <asp:BoundField DataField="CityName" HeaderText="City" />
                                    <asp:BoundField DataField="PostCode" HeaderText="Postal Code" />--%>
                                                        <asp:TemplateField ShowHeader="false">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" CausesValidation="false" CommandName="Select"
                                                                    Enabled="true" HeaderText="Select" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:ButtonField HeaderText="Map Row" Text="Map" ControlStyle-CssClass="btn btn-primary btn-sm" />--%>
                                                    </Columns>
                                                    <PagerStyle CssClass="pagination-ys" HorizontalAlign="Left" />

                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel-group" id="accordionMappedData">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseMappedData">Mapped Data (Total Count:
            <asp:Label ID="lblMappedData" runat="server" Text="0"></asp:Label>)</a>
                    </h4>
                </div>
                <div id="collapseMappedData" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <div style="text-align: right">
                            <asp:Button ID="btnUnmapSelected" runat="server" CssClass="btn btn-primary btn-sm" Text="UnMap Selected" OnClick="btnUnmapSelected_Click" />
                            <asp:Button ID="btnUnmapAll" runat="server" CssClass="btn btn-primary btn-sm" Text="UnMap All" CausesValidation="false" OnClick="btnUnmapAll_Click" />
                        </div>
                        <br />
                        <div id="divMsgForUnMapping" runat="server" style="display: none;"></div>
                        <!-- if you adjust the grid you will need to adjust the codebehin that is generating the super header as well as binding in the ACCO DATA -->
                        <asp:GridView ID="grdAccoMaps" runat="server" AllowPaging="True" AllowCustomPaging="true" AutoGenerateColumns="False"
                            EmptyDataText="No Static Updates" CssClass="table table-hover table-striped" OnDataBound="grdAccoMaps_DataBound"
                            DataKeyNames="Accommodation_ProductMapping_Id,Accommodation_Id" OnPageIndexChanging="grdAccoMaps_PageIndexChanging" OnRowCommand="grdAccoMaps_RowCommand">

                            <Columns>
                                <asp:BoundField DataField="MapId" HeaderText="Map Id" />
                                <asp:BoundField DataField="SupplierId" HeaderText="Supplier Id" ItemStyle-CssClass="ColumnHide" ControlStyle-CssClass="ColumnHide" HeaderStyle-CssClass="ColumnHide" />
                                <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                                <asp:BoundField DataField="SupplierProductReference" HeaderText="Supplier ID" />
                                <asp:BoundField DataField="ProductName" HeaderText="ProductName" />
                                <asp:BoundField DataField="Street" HeaderText="Street" />
                                <asp:BoundField DataField="TelephoneNumber" HeaderText="Tel" />
                                <asp:BoundField DataField="CountryCode" HeaderText="CountryCode" />
                                <asp:BoundField DataField="CityCode" HeaderText="CityCode" />
                                <asp:BoundField DataField="ProductId" HeaderText="Product Id">
                                    <HeaderStyle BackColor="Turquoise" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SystemProductName" HeaderText="Name">
                                    <HeaderStyle BackColor="Turquoise" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SystemCountryName" HeaderText="Country">
                                    <HeaderStyle BackColor="Turquoise" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SystemCityName" HeaderText="City">
                                    <HeaderStyle BackColor="Turquoise" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="UnMap" CssClass="btn btn-default"
                                            Enabled="true" CommandArgument='<%# Bind("Accommodation_ProductMapping_Id") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Remove Map
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" CausesValidation="false" CommandName="Select"
                                            Enabled="true" HeaderText="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <PagerStyle CssClass="pagination-ys" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>

</asp:UpdatePanel>
