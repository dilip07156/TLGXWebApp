﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchRoomTypes.ascx.cs" Inherits="TLGX_Consumer.controls.roomtype.searchRoomTypes" %>
<script>
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
    });
</script>
<div class="navbar">
    <div class="navbar-inner">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#panSupplierSearch" data-toggle="tab">Search by Supplier</a></li>
            <li><a href="#panProductSearch" data-toggle="tab">Search by Product</a></li>
        </ul>
    </div>
</div>

<div class="tab-content">
    <div class="tab-pane active" id="panSupplierSearch">
        <!-- search filters panel -->
        <asp:UpdatePanel ID="updActivityMappingSearch" runat="server">
            <ContentTemplate>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel-group" id="accordion">
                            <div class="panel panel-default">

                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Filters</a>
                                    </h4>
                                </div>

                                <div id="collapseSearch" class="panel-collapse collapse in">

                                    <div class="panel-body">

                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <br />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="HotelSearch" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="ddlProductCategorySubType">
                                                        Supplier Name
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlSupplierNameBySupplier" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="ddlCountryBySupplier">Country</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlCountryBySupplier" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCountryBySupplier_SelectedIndexChanged">
                                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="ddlCityBySupplier">City</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlCityBySupplier" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-6">
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="ddlMappingTypeBySupplier">Mapping Type</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlMappingTypeBySupplier" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="---ALL---" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Static File" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="Dynamic Search Results" Value=""></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="ddlStatusBySupplier">
                                                        Status
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlStatusBySupplier" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-sm-6">
                                                        <asp:Button ID="btnSearchBySupplier" runat="server" OnClick="btnSearchBySupplier_Click" CssClass="btn btn-primary btn-sm" Text="Search" ValidationGroup="HotelSearch" />
                                                        <asp:Button ID="btnResetBySupplier" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnResetBySupplier_Click" Text="Reset" CausesValidation="false" />
                                                    </div>
                                                </div>

                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Search Results Panel -->



                <div class="panel-group" id="accordionProductSearchResult">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-lg-6">
                                    <h4 class="panel-title pull-left">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearchResult">Search Room Types (Total Count:
                                <asp:Label ID="lblSupplierRoomSearchCount" runat="server" Text="0"></asp:Label>)</a>
                                    </h4>
                                </div>

                                <div class="col-lg-2">
                                    <div class="form-group" id="divPagging" runat="server" style="display: none;">
                                        <label class="control-label col-sm-4" for="ddlProductBasedPageSize">Page</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlPageSizeBySupplier" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSizeBySupplier_SelectedIndexChanged" CssClass="form-control col-lg-3">
                                                <asp:ListItem Text="5" Value="5" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-4  pull right">

                                    <div class="form-group">
                                        <asp:Button ID="btnMapSelectedBySupplier" OnClick="btnMapSelectedBySupplier_Click" runat="server" Visible="false" CssClass="btn btn-primary btn-sm" Text="Map Selected" />
                                        <asp:Button ID="btnMapAllBySupplier" OnClick="btnMapAllBySupplier_Click" runat="server" Visible="false" CssClass="btn btn-primary btn-sm" Text="Map All" />

                                        <asp:Button ID="btnTTFUSelectedBySupplier" runat="server" OnClick="btnTTFUSelectedBySupplier_Click" Visible="false" CssClass="btn btn-success btn-sm" Text="TTFU Selected" />
                                        <asp:Button ID="btnTTFUAllBySupplier" OnClick="btnTTFUAllBySupplier_Click" runat="server" Visible="false" CssClass="btn btn-success btn-sm" Text="TTFU All" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div id="divMsgForMapping" runat="server" style="display: none;"></div>
                                    <asp:GridView ID="grdRoomTypeMappingSearchResultsBySupplier" runat="server" AllowPaging="True" AllowCustomPaging="true" AutoGenerateColumns="False"
                                        DataKeyNames="Accommodation_SupplierRoomTypeMapping_Id,Accommodation_Id"
                                        CssClass="table table-responsive table-hover table-striped table-bordered" PagerStyle-CssClass="Page navigation" EmptyDataText="No Mapping Defined."
                                        OnRowCommand="grdRoomTypeMappingSearchResultsBySupplier_RowCommand" OnPageIndexChanging="grdRoomTypeMappingSearchResultsBySupplier_PageIndexChanging" OnRowDataBound="grdRoomTypeMappingSearchResultsBySupplier_RowDataBound">
                                        <Columns>
                                            <asp:BoundField HeaderText="Supplier" DataField="SupplierName" />
                                            <asp:BoundField HeaderText="TLGX Id" DataField="CommonProductId" />
                                            <asp:BoundField HeaderText="Product Name" DataField="ProductName" />
                                            <asp:BoundField HeaderText="Location" DataField="Location" />
                                            <asp:TemplateField HeaderText="Has Rooms">
                                                <ItemTemplate>
                                                    <%# Convert.ToInt32(Eval("NumberOfRooms")) > 0 ? "<span class='label label-success'>" + Convert.ToString(Eval("NumberOfRooms")) + "</span>" : "<span class='label label-danger'>No</span>" %>
                                                    <%--<input type="button" value='<%# Convert.ToInt32(Eval("NumberOfRooms")) > 0 ? Convert.ToString(Eval("NumberOfRooms")) : "No" %>' class='<%# Convert.ToInt32(Eval("NumberOfRooms")) > 0 ? "Greenbtn" : "Redbtn" %>' />--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="S Room Id" DataField="SupplierRoomId" />
                                            <asp:BoundField HeaderText="Supplier Room Type Name" DataField="SupplierRoomName"/>
                                            <asp:TemplateField HeaderText="Suggested Room Info">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlSuggestedRoomInGridBySupplier" CssClass="form-control" runat="server"></asp:DropDownList>
                                                    <input type="text" ID="txtSuggestedRoomInfoInGridBySupplier" value='<%# Eval("Tx_StrippedName") %>' class="form-control" runat="server"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="A?">
                                                <ItemTemplate>
                                                    <input type="button" value='<%# Convert.ToInt32(Eval("NumberOfRooms")) > 0 ? Convert.ToString(Eval("NumberOfRooms")) : "No" %>' class='<%# Convert.ToInt32(Eval("NumberOfRooms")) > 0 ? "Greenbtn" : "Redbtn" %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlMappingStatusInGridBySupplier" CssClass="form-control" runat="server">
                                                          <asp:ListItem Value="0">-?-</asp:ListItem>
                                                        <asp:ListItem Value="Add">Add</asp:ListItem>
                                                        <asp:ListItem Value="Mapped">Mapped</asp:ListItem>
                                                        <asp:ListItem Value="Unmapped">Unmapped</asp:ListItem>
                                                        <asp:ListItem Value="Review">Review</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="false">
                                                <ItemTemplate>
                                                    <input type="checkbox" runat="server" id="chkSelect" onclick="SelectedRow(this);" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Product Based Search -->
    <div class="tab-pane fade in" id="panProductSearch">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel-group" id="accordionTab2">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordionTab2" href="#collapseSearchTab2">Search Filters</a>
                                    </h4>
                                </div>
                                <div id="collapseSearchTab2" class="panel-collapse collapse in">
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <br />
                                                <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="HotelSearch" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="ddlCountryByProduct">Country</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlCountryByProduct" OnSelectedIndexChanged="ddlCountryByProduct_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
                                                            <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="ddlCityByProduct">City</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlCityByProduct" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="txtCommonByProduct">Common Hotel ID</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtCommonByProduct" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="txtHotelNameByProduct">Hotel Name</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtHotelNameByProduct" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="ddlStatus">
                                                        Status
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlStatusByProduct" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-sm-6">
                                                        <asp:Button ID="btnSearchByProduct" runat="server" OnClick="btnSearchByProduct_Click" CssClass="btn btn-primary btn-sm" Text="Search" ValidationGroup="HotelSearch" />
                                                        <asp:Button ID="btnResetByProduct" runat="server" OnClick="btnResetByProduct_Click" CssClass="btn btn-primary btn-sm" Text="Reset" CausesValidation="false" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel-group" id="accordionProductSearchResultByProduct">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-lg-6">
                                    <h4 class="panel-title pull-left">
                                        <a data-toggle="collapse" data-parent="#accordionProductSearchResultByProduct" href="#collapseSearchResultByProduct">Products (Total Count:
                                <asp:Label ID="lblProductCount" runat="server" Text="0"></asp:Label>)</a>
                                    </h4>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                     <div id="div1" runat="server" style="display: none;"></div>
                                    <asp:GridView ID="grdRoomTypeMappingSearchResultsByProduct" runat="server" AllowPaging="True" AllowCustomPaging="true" AutoGenerateColumns="False"
                                        CssClass="table table-responsive table-hover table-striped table-bordered" PagerStyle-CssClass="Page navigation" EmptyDataText="No Product Found."
                                        OnRowCommand="grdRoomTypeMappingSearchResultsByProduct_RowCommand" OnPageIndexChanging="grdRoomTypeMappingSearchResultsByProduct_PageIndexChanging" 
                                        OnRowDataBound="grdRoomTypeMappingSearchResultsByProduct_RowDataBound">
                                        <Columns>
                                            <asp:BoundField HeaderText="Supplier" DataField="SupplierName" />
                                            <asp:BoundField HeaderText="TLGX Id" DataField="CommonProductId" />
                                            <asp:BoundField HeaderText="Product Name" DataField="ProductName" />
                                            <asp:BoundField HeaderText="Location" DataField="Location" />
                                            <asp:TemplateField HeaderText="Has Rooms">
                                                <ItemTemplate>
                                                    <input type="button" value='<%# Convert.ToInt32(Eval("NumberOfRooms")) > 0 ? Convert.ToString(Eval("NumberOfRooms")) : "No" %>' class='<%# Convert.ToInt32(Eval("NumberOfRooms")) > 0 ? "Greenbtn" : "Redbtn" %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NumberOfRooms" />
                                            <asp:BoundField HeaderText="S Room Id" DataField="SupplierRoomId" />
                                            <asp:BoundField HeaderText="Supplier Room Type Name" />
                                            <asp:BoundField HeaderText="Suggested Room Info">
                                                <HeaderStyle BackColor="Turquoise" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="A?">
                                                <ItemTemplate>
                                                    <input type="button" value='<%# Convert.ToInt32(Eval("NumberOfRooms")) > 0 ? Convert.ToString(Eval("NumberOfRooms")) : "No" %>' class='<%# Convert.ToInt32(Eval("NumberOfRooms")) > 0 ? "Greenbtn" : "Redbtn" %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Status">
                                                <HeaderStyle BackColor="Turquoise" />
                                            </asp:BoundField>
                                            <asp:TemplateField ShowHeader="false">
                                                <ItemTemplate>
                                                    <input type="checkbox" runat="server" id="chkSelect" onclick="SelectedRow(this);" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>
                                </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <table class="table table-hover table-striped">
                        <thead>
                            <tr>
                                <th>Product Id</th>
                                <th>Product Name</th>
                                <th>Country</th>
                                <th>City</th>
                                <th>Product Status</th>
                                <th>Mapping Status</th>
                                <th>Has Product Room Types?</th>
                                <th>Static Pending</th>
                                <th>Dynamic Pending</th>
                                <th></th>

                            </tr>
                        </thead>


                        <tbody>
                            <tr>
                                <td>10123</td>
                                <td>Abba Queensgate Hotel</td>
                                <td>United Kingdom</td>
                                <td>London</td>
                                <td>Active</td>
                                <td>Pending</td>
                                <td>Yes (3) </td>
                                <td>3</td>
                                <td>4</td>
                                <td>
                                    <asp:Button ID="Button4" runat="server" CssClass="btn btn-default " Text="Select" ValidationGroup="HotelSearch" />
                                </td>
                            </tr>


                        </tbody>


                    </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
