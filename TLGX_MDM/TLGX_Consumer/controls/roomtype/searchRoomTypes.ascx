<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchRoomTypes.ascx.cs" Inherits="TLGX_Consumer.controls.roomtype.searchRoomTypes" %>
<%@ Register Src="~/controls/staticdata/UpdateSupplierProductMapping.ascx" TagPrefix="uc1" TagName="UpdateSupplierProductMapping" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script src="../../../Scripts/autosize.min.js"></script>
<script src="../../../Scripts/JqueryUI/jquery-ui.js"></script>
<link href="../../../Scripts/JqueryUI/jquery-ui.css" rel="stylesheet" />
<%--<link href="../../../Scripts/Styles/bootstrap.min.css" rel="stylesheet" />--%>
<link href="../../../Scripts/dataTables.bootstrap.min.css" rel="stylesheet" />
<link href="../../../Scripts/jquery.dataTables.min.css" rel="stylesheet" />
<script type="text/javascript" src="../../../Content/JS_Defined/searchRoomTypes.js"></script>
<link rel="Stylesheet" type="text/css" href="../../../Content/Style_Defined/searchRoomType.css" />

<script type="text/javascript">
    function showProductMappingModal() {
        //alert('Hi');
        $("#moProductMapping").modal('show');
    }
    function closeProductMappingModal() {
        //alert("Hi");
        $("#moProductMapping").modal('hide');
    }

    $(document).ready(function () {
        callajax();
    });
</script>
<div class="navbar">
    <div class="navbar-inner">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#panSupplierSearch" data-toggle="tab">Search by Supplier</a></li>
            <%--<li><a href="#panProductSearch" data-toggle="tab">Search by Product</a></li>--%>
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
                                            <div class="col-lg-4">
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="ddlProductCategorySubType">
                                                        Supplier Name
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlSupplierNameBySupplier" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">---ALL---</asp:ListItem>
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
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="txtSupplierRoomName">
                                                        Supplier RoomName
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox runat="server" ID="txtSupplierRoomName" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-4">
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
                                                    <label class="control-label col-sm-6" for="txtProductNameBySupplier">
                                                        Product Name
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox runat="server" ID="txtProductNameBySupplier" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="ddlPriority">
                                                        Accommodation Priority
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlPriority" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-4">
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="txtCommonProdId">
                                                        Company Hotel ID 
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                            ControlToValidate="txtCompanyHotelId" runat="server"
                                                            ErrorMessage="Only Numbers allowed"
                                                            ValidationExpression="\d+" ForeColor="Red">
                                                        </asp:RegularExpressionValidator>
                                                    </label>

                                                    <div class="col-sm-6">
                                                        <asp:TextBox runat="server" ID="txtCompanyHotelId" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="txtTLGXAccoId">
                                                        TLGX AccoId
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox runat="server" ID="txtTLGXAccoId" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="control-label col-sm-6" for="txtTLGXAccoRoomId">
                                                        TLGX Acco RoomId
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox runat="server" ID="txtTLGXAccoRoomId" CssClass="form-control"></asp:TextBox>
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
                                <div class="col-lg-4 ">
                                    &nbsp;
                                </div>

                                <div class="col-lg-2">
                                    <div class="form-group pull-right" id="divPagging" runat="server" style="display: none;">
                                        <div class="input-group">
                                            <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
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
                            </div>
                            <div class="floatingButton">
                                <asp:UpdatePanel runat="server" ID="upnlbtns">
                                    <ContentTemplate>
                                        <asp:Button ID="btnAddSelected" OnClientClick="javascript:return confirm('Are you really sure you want to do this?');" OnClick="btnAddSelected_Click" Visible="false" runat="server" CssClass="btn btn-primary btn-sm" Text="Add Selected" />
                                        <asp:Button ID="btnMapSelectedBySupplier" OnClientClick="javascript:return confirm('Are you really sure you want to do this?');" OnClick="btnMapSelectedBySupplier_Click" runat="server" Visible="false" CssClass="btn btn-primary btn-sm" Text="Map Selected" />
                                        <asp:Button ID="btnMapAllBySupplier" OnClientClick="javascript:return confirm('Are you really sure you want to do this?');" OnClick="btnMapAllBySupplier_Click" runat="server" Visible="false" CssClass="btn btn-primary btn-sm" Text="Map All" />

                                        <asp:Button ID="btnTTFUSelectedBySupplier" runat="server" OnClick="btnTTFUSelectedBySupplier_Click" Visible="false" CssClass="btn btn-success btn-sm" Text="TTFU Selected" />
                                        <asp:Button ID="btnTTFUAllBySupplier" OnClick="btnTTFUAllBySupplier_Click" runat="server" Visible="false" CssClass="btn btn-success btn-sm" Text="TTFU All" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                        <ContentTemplate>
                                            <div id="divMsgForMapping" runat="server" style="display: none;"></div>
                                            <asp:GridView ID="grdRoomTypeMappingSearchResultsBySupplier" runat="server" AllowPaging="True" AllowCustomPaging="true" AutoGenerateColumns="False"
                                                DataKeyNames="Accommodation_SupplierRoomTypeMapping_Id,Accommodation_Id,Supplier_Id,SupplierProductId"
                                                CssClass="table table-responsive table-hover table-striped table-bordered" PagerStyle-CssClass="Page navigation" EmptyDataText="No Mapping Defined."
                                                OnRowCommand="grdRoomTypeMappingSearchResultsBySupplier_RowCommand" OnPageIndexChanging="grdRoomTypeMappingSearchResultsBySupplier_PageIndexChanging">
                                                <Columns>
                                                    <%--<asp:BoundField HeaderText="Hotel ID" DataField="CommonProductId" />--%>
                                                    <asp:TemplateField HeaderText="System Product Name" ItemStyle-Width="12%">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblProductName" Text='<%# Eval("ProductName") %>'></asp:Label><br />
                                                            <%--  <strong>(<asp:Label runat="server" ID="lblHotelID" Text='<%# Eval("CommonProductId") %>'></asp:Label>), </strong>--%>
                                                            <strong>(
                                                                <asp:LinkButton runat="server" Text='<%# Eval("CommonProductId") %>' CommandName="OPENSPM"></asp:LinkButton>), </strong>
                                                            <asp:Label runat="server" ID="lblLocation" Text='<%# Eval("Location") %>'></asp:Label></strong>
                                                       

                                                            <%# Convert.ToInt32(Eval("NumberOfRooms")) > 0 ? "<h4><span class='label label-success '>" + Convert.ToString(Eval("NumberOfRooms")) + "</span></h4>" : "<h5><span class='label label-danger'>No</span></h5>" %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField HeaderText="Product Name" DataField="ProductName" ItemStyle-Width="10%" />--%>
                                                    <%--<asp:BoundField HeaderText="City Name (Country)" DataField="Location" />--%>
                                                    <%-- <asp:TemplateField HeaderText="TLGX Rooms">
                                                <ItemTemplate>
                                                   
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                    <%--<asp:BoundField HeaderText="Supplier" DataField="SupplierName" />--%>
                                                    <asp:TemplateField HeaderText="Supplier" ItemStyle-Wrap="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSuppName" ClientIDMode="Static" Text='<%# Eval("SupplierName") %>'></asp:Label>
                                                            <p>
                                                                (<asp:Label runat="server" ID="lblSupplierProdCode" Text='<%# Eval("SupplierProductId") %>'></asp:Label>
                                                                -
                                                                <asp:Label runat="server" ID="lblSupplierProdName" Text='<%# Eval("SupplierProductName") %>'></asp:Label>
                                                                )
                                                            </p>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:BoundField HeaderText="Supplier ID" DataField="SupplierRoomTypeCode" />--%>
                                                    <%-- <asp:BoundField  DataField="SupplierRoomName"    />--%>
                                                    <asp:TemplateField HeaderText="Supplier Room Type Name" ItemStyle-Width="12%" ItemStyle-Wrap="true">
                                                        <ItemTemplate>
                                                            <%-- <a href="#" data-toggle="popover" title="Popover Header" data-content="Some content inside the popover">Toggle popover</a>--%>
                                                            <strong>(<asp:Label runat="server" ID="lblSupplierRoomTypeCode" Text='<%# Eval("SupplierRoomTypeCode") %>'></asp:Label>)</strong>
                                                            <asp:Label runat="server" ID="lblSupplierRoomTypeName" ClientIDMode="Static" Text='<%# Eval("SupplierRoomName") %>'></asp:Label>
                                                            <a href="#" id="aHelp" runat="server" onmouseover="DisplayToolTip(this);" onclick="return false" onmouseout="HideToolTip(this);" data-toggle="popover" class="glyphicon glyphicon-info-sign Tooltipicon" title=''></a>
                                                            <div id="divToolTip" class="TooltipDescriptionHide">
                                                                <%#Convert.ToString(Eval("RoomDescription")) %>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Suggested Room Info" ItemStyle-Width="18%">
                                                        <ItemTemplate>
                                                            <textarea runat="server" id="txtSuggestedRoomInfoInGridBySupplier" disabled="disabled" value='<%# Eval("Tx_StrippedName") %>' class="form-control"></textarea>
                                                            <a onclick="showOnlineSuggestionModal(this);" class="btn btn-primary" style="display: none">Check Online</a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Attribute Flags" ItemStyle-Width="7%">
                                                        <ItemTemplate>
                                                            <asp:DataList ID="lstAlias" ClientIDMode="Static" runat="server" DataSource='<%# Bind("RoomTypeAttributes") %>'
                                                                RepeatLayout="Table" RepeatColumns="3" RepeatDirection="Horizontal" ItemStyle-Wrap="true" CssClass="">
                                                                <ItemTemplate>
                                                                    <h4><span aria-hidden="true" data-toggle="tooltip" data-placement="left" class="glyphicon glyphicon-<%# Eval("IconClass") %>" title="<%# Eval("SystemAttributeKeyword") + " : " + Eval("SupplierRoomTypeAttribute")  %>  "></span></h4>
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="TLGX Room Info (Room Category)" ItemStyle-Width="25%">
                                                        <ItemTemplate>

                                                            <button class="btn btn-primary" onclientclick="Static" type="button" runat="server" id="btnSuggestion" text="Check" onclick="BindRTDetails(this);">Check</button><br />
                                                            <asp:DataList ID="lstMappedRoomList" ClientIDMode="Static" runat="server" DataSource='<%# Bind("MappedRoomInfo") %>'
                                                                RepeatLayout="Table" RepeatColumns="1" RepeatDirection="Vertical" ItemStyle-Wrap="false" CssClass="table">
                                                                <ItemTemplate>
                                                                    <label><%# Eval("Accommodation_RoomInfo_Name") + " : " + Eval("Accommodation_RoomInfo_Category") + " : " + Eval("MappingStatus") %></label>
                                                                </ItemTemplate>
                                                            </asp:DataList>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="12%" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblStatus" ClientIDMode="Static" Text='<%# Eval("MappingStatus") %>'></asp:Label>
                                                            <asp:DropDownList ID="ddlMappingStatusInGridBySupplier" CssClass="form-control MappingStatus" runat="server">
                                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                <asp:ListItem Value="ADD">ADD</asp:ListItem>
                                                                <asp:ListItem Value="MAPPED">MAPPED</asp:ListItem>
                                                                <asp:ListItem Value="UNMAPPED">UNMAPPED</asp:ListItem>
                                                                <asp:ListItem Value="REVIEW">REVIEW</asp:ListItem>
                                                                <asp:ListItem Value="AUTOMAPPED">AUTOMAPPED</asp:ListItem>


                                                            </asp:DropDownList>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Add" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <input type="checkbox" runat="server" id="chkSelect" onclick="SelectedRow(this);" disabled />
                                                            <input type="hidden" class="hidnAcoo_Id" value='<%# Eval("Accommodation_Id") %>' />
                                                            <input type="hidden" class="hdnRoomCount" id="hdnRoomCount" runat="server" value='<%# Eval("NumberOfRooms") %>' />
                                                            <input type="hidden" class="hdnAccommodation_RoomInfo_Id" runat="server" id="hdnAccommodation_RoomInfo_Id" value='<%# Eval("Accommodation_RoomInfo_Id") %>' />
                                                            <input type="hidden" class="hdnAccommodation_RoomInfo_Name" value='<%# Eval("Accommodation_RoomInfo_Name") %>' />
                                                            <input type="hidden" class="hdnAccommodation_SupplierRoomTypeMapping_Id" runat="server" id="hdnAccommodation_SupplierRoomTypeMapping_Id" value='<%# Eval("Accommodation_SupplierRoomTypeMapping_Id") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Is Not Training Data" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <h4><span aria-hidden="true" data-toggle="tooltip" data-placement="left" class="glyphicon glyphicon-<%# Eval("IsNotTraining").ToString() == "True" ? "ok" : "" %>"></span></h4>
                                                            <label id="lblIsNotTraining" style="display:none"><%# Eval("IsNotTraining").ToString() %></label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <PagerStyle CssClass="pagination-ys" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div class="modal fade" id="modalOnlineSuggestion" role="dialog">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="panel-title">
                        <h4 class="modal-title">Online Suggestion For (<label id="lblForSupplierRoomTypeName"></label>
                            )</h4>
                    </div>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <table class='table table-bordered'>
                                <tr>
                                    <th style='font-size: 14px; width: 30%;'>Syntactic</th>
                                    <th style='font-size: 14px; width: 30%;'>Semantic (Unsupervised)</th>
                                    <th style='font-size: 14px; width: 30%;'>Semantic (Supervised)</th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <div class="ulRoomInfoOnline" id="ulRoomInfoOnlineSyntactic">
                                                    <div id="loadingOnlineSyntactic" style="padding: 5px;">
                                                        <img alt="Loading..." src="../../../images/ajax-loader.gif" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <div class="ulRoomInfoOnline" id="ulRoomInfoOnlineSemanticUnSup">
                                                    <div id="loadingOnlineSemanticUnSup" style="padding: 5px;">
                                                        <img alt="Loading..." src="../../../images/ajax-loader.gif" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <div class="ulRoomInfoOnline" id="ulRoomInfoOnlineSemanticSup">
                                                    <div id="loadingOnlineSemanticSup" style="padding: 5px;">
                                                        <img alt="Loading..." src="../../../images/ajax-loader.gif" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>


                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalTLGXRoomInfo" role="dialog">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="panel-title">
                        <h4 class="modal-title">Supplier Room Type Name : 
                            <label id="lblForTLGXRoomInfoName"></label>
                        </h4>
                        <div id="divAttribute"></div>
                        <br />
                        <div id="divActionButtons"></div>
                        <br />
                        <div id="responseMessage" style="display: none;"></div>
                    </div>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>

                            <div id="loading" style="padding: 5px;">
                                <img alt="Loading..." src="../../../images/ajax-loader.gif" />
                            </div>

                            <div class="ulRoomInfoStyle" id="ulRoomInfo">
                            </div>

                            <asp:HiddenField runat="server" ID="hdnAccommodation_SupplierRoomInfo_IdPopUp" ClientIDMode="Static" />
                            <asp:HiddenField runat="server" ID="hdnAccommodation_IdPopUp" ClientIDMode="Static" />
                            <asp:HiddenField runat="server" ID="hdnControlID" ClientIDMode="Static" />

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
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
                                            <asp:BoundField HeaderText="Hotel ID" DataField="CommonProductId" />
                                            <asp:BoundField HeaderText="Product Name" DataField="ProductName" />
                                            <asp:BoundField HeaderText="City Name ( Country)" DataField="Location" />
                                            <asp:TemplateField HeaderText="Has Rooms">
                                                <ItemTemplate>
                                                    <input type="button" value='<%# Convert.ToInt32(Eval("NumberOfRooms")) > 0 ? Convert.ToString(Eval("NumberOfRooms")) : "No" %>' class='<%# Convert.ToInt32(Eval("NumberOfRooms")) > 0 ? "Greenbtn" : "Redbtn" %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NumberOfRooms" />
                                            <asp:BoundField HeaderText="Supplier ID" DataField="SupplierRoomId" />
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


<script type="text/javascript" src="../../../Scripts/jquery.dataTables.min.js"></script>
<%--UpdateSupplierMapping PopUp--%>
<div class="modal fade" id="moProductMapping" role="dialog" data-backdrop="static" data-keyboard="false">
    <div class="modal-dialog modal-lg x-lg">
        <div class="modal-content">

            <div class="modal-header">
                <div class="panel-heading">
                    <h4 class="modal-title">Update Supplier Product Mapping</h4>
                </div>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel ID="UpdProductMapModal" runat="server">
                    <ContentTemplate>
                        <uc1:UpdateSupplierProductMapping runat="server" ID="UpdateSupplierProductMapping" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
