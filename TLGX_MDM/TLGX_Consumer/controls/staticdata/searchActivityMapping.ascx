<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchActivityMapping.ascx.cs" Inherits="TLGX_Consumer.controls.staticdata.searchActivityMapping" %>
<script src="../../Scripts/JqueryUI/jquery-ui.js"></script>
<link href="../../Scripts/JqueryUI/jquery-ui.css" rel="stylesheet" />
<style type="text/css">
    .hideColumn {
        display: none;
    }

    .fontForModel {
        font-size: 12px !important;
    }
</style>
<script type="text/javascript">
    function CloseAddUpdateActivityMappingModal() {
        $("#moAddUpdateActivityMapping").modal('hide');
    }
    function showAddUpdateActivityMappingModal() {
        $("#moAddUpdateActivityMapping").modal('show');
    }
    function ClosemoProductWiseMapping() {
        $("#moProductWiseMapping").modal('hide');
    }
    function showmoProductWiseMapping() {
        $("#moProductWiseMapping").modal('show');
    }
    function SelectedRow(element) {
        debugger;
        var ddlStatus = $('#MainContent_searchActivityMapping_ddlMappingStatus option:selected').html();
        if (ddlStatus == "REVIEW") {
            element.parentNode.parentNode.nextSibling.childNodes[12].lastElementChild.focus();
        }
        else if (ddlStatus == "UNMAPPED") {
            element.parentNode.parentNode.nextSibling.childNodes[10].lastElementChild.focus();

        }
    }
    function MatchedSelect(elem) {
        elem.parentNode.parentNode.nextSibling.childNodes[12].lastElementChild.focus();
    }
    function fillDropDown(record, onClick) {
        if (onClick) {
            var countryname = record.parentNode.parentNode.childNodes[14].firstElementChild.value.split(',')[0];
            var cityname = record.parentNode.parentNode.childNodes[14].firstElementChild.value.split(',')[1];

            if (countryname == "" && cityname == "") {
                countryname = record.parentNode.parentNode.childNodes[14].lastElementChild.value.split(',')[0];
                cityname = record.parentNode.parentNode.childNodes[14].lastElementChild.value.split(',')[1];

            }
            if (countryname != null || cityname != null) {
                //Getting Dropdown
                var currentRow = $(record).parent().parent();
                var ActivityDDL = currentRow.find("td:eq(9)").find('select');
                var selectedText = ActivityDDL.find("option:selected").text();
                var selectedOption = ActivityDDL.find("option");
                var selectedVal = ActivityDDL.val();
                if (countryname == null || countryname == "") {
                    countryname = "null";
                }
                if (cityname == null || cityname == "") {
                    cityname = "null";
                }
                //Setting null 
                var dataObj = {
                    'EntityType': 'activity',
                    'countryname': countryname,
                    'cityname': cityname
                }

                if (ActivityDDL != null) {
                    $.ajax({
                        url: '../../../Service/ToFillDDL.ashx',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: dataObj,
                        responseType: "json",
                        success: function (result) {
                            ActivityDDL.find("option:not(:first)").remove();
                            var value = JSON.stringify(result);
                            var listItems = '';
                            if (result != null) {
                                for (var i = 0; i < result.length; i++) {
                                    listItems += "<option value='" + result[i].Activity_Id + "'>" + result[i].Product_Name + "</option>";
                                }
                                ActivityDDL.append(listItems);
                            }

                            ActivityDDL.find("option").prop('selected', false).filter(function () {
                                return $(this).text() == selectedText;
                            }).attr("selected", "selected");
                        },
                        failure: function () {
                        }
                    });
                }

            }
        }
    }
    function RemoveExtra(record, onClick) {
        if (!onClick) {
            debugger;
            var currentRow = $(record).parent().parent();
            var ActivityDDL = currentRow.find("td:eq(9)").find('select');
            var selectedText = ActivityDDL.find("option:selected").text();
            var selectedVal = ActivityDDL.val();
            ActivityDDL.find("option:not(:first)").remove();
            var listItems = "<option selected = 'selected' value='" + selectedVal + "'>" + selectedText + "</option>";
            ActivityDDL.append(listItems);
            var city_id = record.parentNode.parentNode.childNodes[15].firstElementChild;
            city_id.value = selectedVal;
        }
    }

    $(document).ready(function () {
        callajax();
    });
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        callajax();
    });
    function callajax() {
        $("#MainContent_searchActivityMapping_txtCKISProductNameActivityByProduct").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '../../Service/ActivityNameAutoComplete.ashx',
                    dataType: "json",
                    data: {
                        term: request.term,
                        country: $("[id*=ddlSystemCountryActivityByProduct]").children("option:selected").text(),
                        city: $("[id*=ddlSystemCityActivityByProduct]").children("option:selected").text(),
                        ckisproducttype: $("[id*=ddlCKISProductTypeActivityByProduct]").children("option:selected").text(),
                    },
                    success: function (data) {
                        response(data);
                    }
                });
            },
            min_length: 3,
            delay: 300
        });
    }


</script>
<div class="navbar">
    <div class="navbar-inner">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#panSupplierSearch" data-toggle="tab">Search by Supplier</a></li>
            <li><a href="#panCKISProductSearch" data-toggle="tab">Search by CKIS Product</a></li>
        </ul>
    </div>
</div>

<!-- navigation options -->
<div class="tab-content">
    <div class="tab-pane active" id="panSupplierSearch">
        <asp:UpdatePanel ID="updActivityMappingSearch" runat="server">
            <ContentTemplate>
                <div class="panel-group" id="accordionSupplierSearch">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4 class="panel-title"><a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Filters</a>
                        </div>
                        <div id="collapseSearch" class="panel-collapse collapse in">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlSupplierName">
                                                Supplier Name
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true">
                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlSystemCountryName">
                                                System Country Name
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlSystemCountryName" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSystemCountryName_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlSystemCityName">
                                                System City Name
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlSystemCityName" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlMappingStatus">
                                                Mapping Status
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlMappingStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="txtSuppCountryName">Supplier Country Name</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtSuppCountryName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="txtSuppCityName">Supplier City Name</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtSuppCityName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="txtSuppProductName">Supplier Product Name</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtSuppProductName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="txtSuppProductName">Search By Keyword</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtKeyWordBySupplier" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlPageSize">Page Size</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control col-lg-3" AutoPostBack="true">
                                                    <asp:ListItem Text="5" Value="5" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row col-md-5 pull-right">
                                            <asp:Button ID="btnActivitySearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnActivitySearch_Click" />
                                            <asp:Button ID="btnActivityReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" CausesValidation="false" OnClick="btnActivityReset_Click" />
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-group" id="accordionSearchResult">
                    <div class="panel panel-default">
                        <div class="panel-heading clearfix">
                            <h4 class="panel-title pull-left">
                                <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearchResult">Search Results (Total Count:
                                <asp:Label ID="lblActivityCount" runat="server" Text="0"></asp:Label>)</a>
                            </h4>
                            <div class="form-group pull-right">
                                <asp:Button ID="btnMapSelected" Visible="false" runat="server" CssClass="btn btn-primary btn-sm" Text="Map Selected" OnClick="btnMapSelected_Click" />
                                <asp:Button ID="btnMapAll" Visible="false" runat="server" CssClass="btn btn-primary btn-sm" Text="Map All" OnClick="btnMapAll_Click" />
                            </div>
                        </div>
                        <div id="collapseSearchResult" class="panel-collapse collapse in">
                            <div class="panel-body">
                                <div id="divMsgForMapping" runat="server" style="display: none;"></div>

                                <asp:GridView ID="grdActivitySearchResults" runat="server" AllowPaging="True" AllowCustomPaging="true" AutoGenerateColumns="False" DataKeyNames="ActivitySupplierProductMapping_Id,Supplier_Id,SupplierCountryName,SupplierCityName,Activity_ID,CKIS_Master"
                                    CssClass="table table-responsive table-hover table-striped table-bordered" OnPageIndexChanging="grdActivitySearchResults_PageIndexChanging"
                                    OnRowCommand="grdActivitySearchResults_RowCommand" OnDataBinding="grdActivitySearchResults_DataBinding" PagerStyle-CssClass="Page navigation" EmptyDataText="No Mapping Defined." OnRowDataBound="grdActivitySearchResults_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="MapID" HeaderText="Map Id" />
                                        <asp:BoundField DataField="SupplierName" HeaderText="Supplier" />
                                        <asp:BoundField DataField="SuplierProductCode" HeaderText="ID Supplier" />
                                        <asp:BoundField DataField="SupplierCountryName" HeaderText="Country" />
                                        <asp:BoundField DataField="SupplierCityName" HeaderText="City" />
                                        <asp:BoundField DataField="SupplierProductName" HeaderText="Name" />
                                        <asp:BoundField DataField="ourRef" HeaderText="Our Ref" />
                                        <asp:BoundField DataField="SystemCountryName" HeaderText="Country">
                                            <HeaderStyle BackColor="Turquoise" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SystemCityName" HeaderText="City">
                                            <HeaderStyle BackColor="Turquoise" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CKIS_Master" HeaderText="CKIS Master">
                                            <HeaderStyle BackColor="Turquoise" />
                                        </asp:BoundField>
                                        <asp:TemplateField ShowHeader="true" HeaderText="CKIS Master">
                                            <HeaderStyle BackColor="Turquoise" />
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlGridProduct" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="false"
                                                    onfocus="fillDropDown(this,true);" onchange="RemoveExtra(this,false);" onclick="fillDropDown(this,true);">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="MappingStatus" HeaderText="Status" />
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                                    Enabled="true" CommandArgument='<%# Bind("ActivitySupplierProductMapping_Id") %>' OnClientClick="showAddUpdateActivityMappingModal();">
                                                            <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Select
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <%-- <asp:CheckBox ID="chkSelect" runat="server" CausesValidation="false" CommandName="Select" AutoPostBack="true"
                                                    Enabled="true" HeaderText="Select" OnCheckedChanged="chkSelect_CheckedChanged" />--%>
                                                <input type="checkbox" runat="server" id="chkSelect" onclick="SelectedRow(this);" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="hideColumn" HeaderStyle-CssClass="hideColumn">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnCountryCityNameSystem" Value='<%# string.Concat(Eval("SystemCountryName"),",",Eval("SystemCityName")) %>' runat="server" />
                                                <asp:HiddenField ID="hdnCountryCityName" Value='<%# string.Concat(Eval("SupplierCountryName"),",",Eval("SupplierCityName")) %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="hideColumn" HeaderStyle-CssClass="hideColumn">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnActivityId" Value="" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="modal fade" id="moAddUpdateActivityMapping" role="dialog">
            <!-- SUPPLIER WISE MAPPING SCREEN MODAL -->
            <div id="moCityMapping">
                <div class="">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Supplier-wise Activity Mapping</h4>
                        </div>
                        <div class="modal-body fontForModel">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">Supplier Static Information</div>
                                                <div class="panel-body">
                                                    <!-- panel should scoll -->
                                                    <table class="table table-condensed">
                                                        <tbody>
                                                            <tr>
                                                                <td><strong>Supplier Name</strong></td>
                                                                <td>
                                                                    <asp:Label ID="lblSupplierName" runat="server" Text=""></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td><strong>ID</strong></td>
                                                                <td>
                                                                    <asp:Label ID="lblSupplierProductCode" runat="server" Text=""></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td><strong>Type</strong></td>
                                                                <td>
                                                                    <asp:Label ID="lblSupplierProductType" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><strong>Country</strong></td>
                                                                <td>
                                                                    <asp:Label ID="lblSupplierCountryName" runat="server" Text=""></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td><strong>City</strong></td>
                                                                <td>
                                                                    <asp:Label ID="lblSupplierCityName" runat="server" Text=""></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td><strong>Name</strong></td>
                                                                <td>
                                                                    <asp:Label ID="lblSupplierProductName" runat="server" Text=""></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td><strong>Address</strong></td>
                                                                <td>
                                                                    <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td><strong>Duration</strong></td>
                                                                <td>
                                                                    <asp:Label ID="lblSupplierProDuration" runat="server" Text=""></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td><strong>DeparturePoint</strong></td>
                                                                <td>
                                                                    <asp:Label ID="lblDeparturePoint" runat="server" Text=""></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td><strong>Currency</strong></td>
                                                                <td>
                                                                    <asp:Label ID="lblCurrency" runat="server" Text=""></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td><strong>DepartureTime</strong></td>
                                                                <td>
                                                                    <asp:Label ID="lblDepartureTime" runat="server" Text=""></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td><strong>ProductValidFor</strong></td>
                                                                <td>
                                                                    <asp:Label ID="lblProductValidFor" runat="server" Text=""></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td><strong>Latitude</strong></td>
                                                                <td>
                                                                    <asp:Label ID="lblLatitude" runat="server" Text=""></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td><strong>Longitude</strong></td>
                                                                <td>
                                                                    <asp:Label ID="lblLongitude" runat="server" Text=""></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td><strong>Theme</strong></td>
                                                                <td>
                                                                    <asp:Label ID="lblTheme" runat="server" Text=""></asp:Label></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>

                                                    <p><strong>Inclusions</strong></p>
                                                    <hr />
                                                    <p>
                                                        <asp:Label ID="lblInclusions" runat="server" Text=""></asp:Label>
                                                    </p>

                                                    <p><strong>Exclusions</strong></p>
                                                    <hr />
                                                    <p>
                                                        <asp:Label ID="lblExclusions" runat="server" Text=""></asp:Label>
                                                    </p>
                                                    <p>
                                                        <strong>Introduction
                                                        </strong>
                                                    </p>
                                                    <hr />
                                                    <p>
                                                        <asp:Label ID="lblIntroduction" runat="server" Text=""></asp:Label>
                                                    </p>

                                                    <p>
                                                        <strong>Conditions
                                                        </strong>
                                                    </p>
                                                    <hr />
                                                    <p>
                                                        <asp:Label ID="lblConditions" runat="server" Text=""></asp:Label>
                                                    </p>
                                                    <p>
                                                        <strong>AdditionalInformation
                                                        </strong>
                                                    </p>
                                                    <hr />
                                                    <p>
                                                        <asp:Label ID="lblAdditionalInformation" runat="server" Text=""></asp:Label>
                                                    </p>
                                                    <p>
                                                        <strong>ProductDescription
                                                        </strong>
                                                    </p>
                                                    <hr />
                                                    <p>
                                                        <asp:Label ID="lblProductDescription" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-lg-8">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">TLGX Activity Sub Types</div>
                                                <div class="panel-body">
                                                    <!-- panel should scoll -->
                                                    <div id="dvmsg" runat="server" style="display: none;" enableviewstate="false"></div>
                                                    <div class="row">
                                                        <div class="col-lg-6">
                                                            <h4>
                                                                <asp:Label ID="lblProductName" runat="server" Text=""></asp:Label></h4>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <span>
                                                                <asp:Label ID="lblActivityNo" runat="server" Text=""></asp:Label>
                                                                out of
                                                                <asp:Label ID="lblTotalCountActivity" runat="server" Text=""></asp:Label>
                                                                Products</span>
                                                            <asp:Button ID="btnPrevious" OnClick="btnPrevious_Click" runat="server" Enabled="false" CssClass="btn btn-default " Text="<" />
                                                            <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" CssClass="btn btn-default " Text=">" />
                                                            <a href="#" data-toggle="collapse" class="btn btn-default" id="filterlink">Filter</a>
                                                            <asp:Button ID="btnMapActivityMap" OnClick="btnMapActivityMap_Click" runat="server" CssClass="btn btn-primary " Text="Map" />
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <ajaxToolkit:CollapsiblePanelExtender ID="cpe" runat="Server"
                                                                TargetControlID="pnlFilter"
                                                                CollapsedSize="0"
                                                                Collapsed="true"
                                                                ExpandControlID="filterlink"
                                                                CollapseControlID="filterlink"
                                                                AutoCollapse="False"
                                                                AutoExpand="False"
                                                                TextLabelID="Label1"
                                                                ExpandDirection="Vertical" />
                                                            <asp:Panel ID="pnlFilter" runat="server">
                                                                <div id="demo" class="panel panel-default">
                                                                    <div class="panel-body">
                                                                        <div class="col-lg-6">
                                                                            <div class="form-group row">
                                                                                <label class="control-label col-sm-4" for="ddlSupplierName">
                                                                                    CKIS Type
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlActivityFilterCKISType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group row">
                                                                                <label class="control-label col-sm-4" for="ddlSupplierName">
                                                                                    Country
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlActivityFilterCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlActivityFilterCountry_SelectedIndexChanged" AutoPostBack="true">
                                                                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group row">
                                                                                <label class="control-label col-sm-4" for="txtfltProductName">
                                                                                    Search By Keyword
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtfilterActivityByKeyWord" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-6">
                                                                            <div class="form-group row">
                                                                                <label class="control-label col-sm-4" for="ddlSupplierName">
                                                                                    City
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlActivityFilterCity" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group row">
                                                                                <label class="control-label col-sm-4" for="txtfltProductName">
                                                                                    Name
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtActivityFilterName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group row pull-right">
                                                                            <div class="col-md-12">
                                                                                <asp:Button ID="btnActivityFilter" runat="server" CssClass="btn btn-primary" OnClick="btnActivityFilter_Click" Text="Filter" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>

                                                    <br />
                                                    <div class="panel panel-default">
                                                        <asp:FormView ID="ActivityFormView" AllowPaging="true" runat="server">
                                                            <HeaderStyle ForeColor="white" BackColor="Blue" />
                                                            <ItemTemplate>
                                                                <div class="panel-body">
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <p><strong>General Information</strong></p>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-12">
                                                                        <table class="table table-condensed">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td><strong>CKIS Type</strong></td>
                                                                                    <td>
                                                                                        <asp:HiddenField ID="hdnActivityId" runat="server" Value='<%# Eval("Activity_Id") %>' />
                                                                                        <asp:Label ID="lblProductSubType" runat="server" Text='<%# Eval("ProductSubType") %>'></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td><strong>Type</strong></td>
                                                                                    <td>
                                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("ProductCategory") %>'></asp:Label></td>
                                                                                </tr>
                                                                                <tr id="trStartingPoint" runat="server">
                                                                                    <%--visible='<%# !string.IsNullOrWhiteSpace((string)Eval("StartingPoint")) %>'--%>
                                                                                    <td><strong>Starting Point</strong></td>
                                                                                    <td>
                                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("StartingPoint") %>'></asp:Label></td>
                                                                                </tr>
                                                                                <tr id="trEndingPoint" runat="server">
                                                                                    <%--visible='<%# !string.IsNullOrWhiteSpace((string)Eval("EndingPoint")) %>'--%>
                                                                                    <td><strong>Ending Point</strong></td>
                                                                                    <td>
                                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("EndingPoint") %>'></asp:Label></td>
                                                                                </tr>
                                                                                <tr id="trDuration" runat="server">
                                                                                    <%--visible='<%# !string.IsNullOrWhiteSpace((string)Eval("Duration")) %>'--%>
                                                                                    <td><strong>Duration</strong></td>
                                                                                    <td>
                                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Duration") %>'></asp:Label></td>
                                                                                </tr>
                                                                                <tr id="trCompanyRecommended" runat="server">
                                                                                    <%--visible='<%# !string.IsNullOrWhiteSpace((string)Eval("CompanyRecommended")) %>'--%>
                                                                                    <td><strong>Company Recommended</strong></td>
                                                                                    <td>
                                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("CompanyRecommended") %>'></asp:Label></td>
                                                                                </tr>
                                                                                <tr id="trMealsYN" runat="server">
                                                                                    <%--visible='<%# Convert.ToString(Eval("MealsYN")) == string.Empty ? false : true %>'--%>
                                                                                    <td><strong>Meals</strong></td>
                                                                                    <td>
                                                                                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("MealsYN")  %>'></asp:Label></td>
                                                                                </tr>
                                                                                <tr id="trGuideYN" runat="server">
                                                                                    <%--visible='<%# Convert.ToString(Eval("GuideYN")) == string.Empty ? false : true %>'--%>
                                                                                    <td><strong>Guide</strong></td>
                                                                                    <td>
                                                                                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("GuideYN") %>'></asp:Label></td>
                                                                                </tr>
                                                                                <tr id="trTransferYN" runat="server">
                                                                                    <%--visible='<%# Convert.ToString(Eval("TransferYN")) == string.Empty ? false : true %>'--%>
                                                                                    <td><strong>Transfer</strong></td>
                                                                                    <td>
                                                                                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("TransferYN") %>'></asp:Label></td>
                                                                                </tr>
                                                                                <tr id="trPhysicalLevel" runat="server">
                                                                                    <%--visible='<%# !string.IsNullOrWhiteSpace((string)Eval("PhysicalLevel")) %>'--%>
                                                                                    <td><strong>Physical Level</strong></td>
                                                                                    <td>
                                                                                        <asp:Label ID="Label10" runat="server" Text='<%# Eval("PhysicalLevel") %>'></asp:Label></td>
                                                                                </tr>
                                                                                <tr id="trAdvisory" runat="server">
                                                                                    <%--visible='<%# !string.IsNullOrWhiteSpace((string)Eval("Advisory")) %>'--%>>
                                                                                    <td><strong>Advisory</strong></td>
                                                                                    <td>
                                                                                        <asp:Label ID="Label11" runat="server" Text='<%# Eval("Advisory") %>'></asp:Label></td>
                                                                                </tr>
                                                                                <tr id="trThingsToCarry" runat="server">
                                                                                    <%--visible='<%# !string.IsNullOrWhiteSpace((string)Eval("ThingsToCarry")) %>'--%>>
                                                                                    <td><strong>Things To Carry</strong></td>
                                                                                    <td>
                                                                                        <asp:Label ID="Label12" runat="server" Text='<%# Eval("ThingsToCarry") %>'></asp:Label></td>
                                                                                </tr>
                                                                                <tr id="trDeparturePoint" runat="server">
                                                                                    <%--visible='<%# !string.IsNullOrWhiteSpace((string)Eval("DeparturePoint")) %>'--%>
                                                                                    <td><strong>Departure Point </strong></td>
                                                                                    <td>
                                                                                        <asp:Label ID="Label13" runat="server" Text='<%# Eval("DeparturePoint") %>'></asp:Label></td>
                                                                                </tr>
                                                                                <tr id="trTourType" runat="server">
                                                                                    <%--visible='<%# !string.IsNullOrWhiteSpace((string)Eval("TourType")) %>'--%>
                                                                                    <td><strong>Tour Type</strong></td>
                                                                                    <td>
                                                                                        <asp:Label ID="Label14" runat="server" Text='<%# Eval("TourType") %>'></asp:Label></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                    <div class="col-lg-12">
                                                                        <strong>Short Description</strong>
                                                                        <hr />
                                                                        <asp:Label ID="lblShortDescription" runat="server" Text='<%# Server.HtmlDecode(Convert.ToString(Eval("ShortDescription"))) %>'></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-12">
                                                                        <ajaxToolkit:CollapsiblePanelExtender ID="cpe" runat="Server"
                                                                            TargetControlID="pnlLongDes"
                                                                            CollapsedSize="0"
                                                                            Collapsed="true"
                                                                            ExpandControlID="pnlClickLongDes"
                                                                            CollapseControlID="pnlClickLongDes"
                                                                            AutoCollapse="False"
                                                                            AutoExpand="False"
                                                                            ImageControlID="Image1"
                                                                            ExpandedImage="~/images/collapse.jpg"
                                                                            CollapsedImage="~/images/expand.jpg"
                                                                            ExpandDirection="Vertical" />
                                                                        <%-- <strong>Long Description</strong>
                                                                                            <hr />--%>
                                                                        <asp:Panel ID="pnlClickLongDes" runat="server">
                                                                            <div style="font-weight: bold;">
                                                                                <strong>Long Description</strong>
                                                                                <asp:Image ImageUrl="~/images/expand.jpg" ID="Image1" runat="server" />
                                                                            </div>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlLongDes" runat="server">
                                                                            <hr />
                                                                            <asp:Literal ID="lblLongDescription" Mode="Transform" runat="server" Text='<%# Server.HtmlDecode(Convert.ToString(Eval("LongDescription"))) %>'></asp:Literal>
                                                                        </asp:Panel>
                                                                    </div>
                                                                    <div class="row col-lg-12">
                                                                        <hr />
                                                                        <div class="col-lg-6">
                                                                            <p><strong>Inclusions</strong></p>
                                                                            <hr />
                                                                            <table class="table table-condensed table-striped">
                                                                                <asp:Repeater ID="rptInclusions" runat="server">
                                                                                    <HeaderTemplate>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lblInclusion" runat="server" Text='<%# Server.HtmlDecode(Convert.ToString(Eval("Content_Text")))  %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                    </FooterTemplate>
                                                                                </asp:Repeater>
                                                                            </table>
                                                                        </div>
                                                                        <div class="col-lg-6">
                                                                            <p><strong>Exclusions</strong></p>
                                                                            <hr />
                                                                            <table class="table table-condensed  table-striped">
                                                                                <asp:Repeater ID="rptExclusion" runat="server">
                                                                                    <HeaderTemplate>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lblExclusion" runat="server" Text='<%# Server.HtmlDecode(Convert.ToString(Eval("Content_Text")))  %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                    </FooterTemplate>
                                                                                </asp:Repeater>
                                                                            </table>
                                                                        </div>
                                                                        <hr />
                                                                        <div class="col-lg-12">
                                                                            <p><strong>Notes</strong></p>
                                                                            <hr />
                                                                            <table class="table table-condensed  table-striped">
                                                                                <asp:Repeater ID="rptNotes" runat="server">
                                                                                    <HeaderTemplate>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lblNotes" runat="server" Text='<%# Server.HtmlDecode(Convert.ToString(Eval("Content_Text")))  %>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                    </FooterTemplate>
                                                                                </asp:Repeater>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>No Data Found</EmptyDataTemplate>
                                                        </asp:FormView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- existing mapping grid -->
                                    <div class="panel panel-default" runat="server">
                                        <%--id="divMapped" style="display: none;"--%>
                                        <div class="panel-heading">Existing Mapping</div>
                                        <div class="panel-body">
                                            <div id="dvMsgUnMapped" runat="server" style="display: none;" enableviewstate="false"></div>
                                            <asp:GridView ID="grdActivityMapped" runat="server" AllowPaging="True" AllowCustomPaging="true" AutoGenerateColumns="False" DataKeyNames="ActivitySupplierProductMapping_Id"
                                                CssClass="table table-responsive table-hover table-striped table-bordered"
                                                OnRowCommand="grdActivityMapped_RowCommand" PagerStyle-CssClass="Page navigation" EmptyDataText="No Mapping Defined.">
                                                <Columns>
                                                    <asp:BoundField DataField="MapID" HeaderText="Map Id" />
                                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier" />
                                                    <asp:BoundField DataField="SuplierProductCode" HeaderText="ID Supplier" />
                                                    <asp:BoundField DataField="SupplierCountryName" HeaderText="Country" />
                                                    <asp:BoundField DataField="SupplierCityName" HeaderText="City" />
                                                    <asp:BoundField DataField="SupplierProductName" HeaderText="Name" />
                                                    <asp:BoundField DataField="ourRef" HeaderText="Our Ref" />
                                                    <asp:BoundField DataField="SystemCountryName" HeaderText="Country">
                                                        <HeaderStyle BackColor="Turquoise" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SystemCityName" HeaderText="City">
                                                        <HeaderStyle BackColor="Turquoise" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CKIS_Master" HeaderText="CKIS Master">
                                                        <HeaderStyle BackColor="Turquoise" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="MappingStatus" HeaderText="Status" />
                                                    <asp:TemplateField ShowHeader="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Unmap" CssClass="btn btn-default"
                                                                Enabled="true" CommandArgument='<%# Bind("ActivitySupplierProductMapping_Id") %>'>
                                                            <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Unmap
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="pagination-ys" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="tab-pane fade in" id="panCKISProductSearch">
        <div class="panel-group" id="accordionCKISProductSearch">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title"><a data-toggle="collapse" data-parent="#accordion" href="#collapseCKISProductSearch">Search Filters</a></h4>
                </div>
                <div id="collapseCKISProductSearch" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="col-lg-6">
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlSystemCountryActivityByProduct">
                                            System Country
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlSystemCountryActivityByProduct" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSystemCountryActivityByProduct_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlSystemCityActivityByProduct">
                                            System City 
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlSystemCityActivityByProduct" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlProdStatusActivityByProduct">
                                            Status
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlProdStatusActivityByProduct" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-6">
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlCKISProductTypeActivityByProduct">
                                            CKIS Product Type
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlCKISProductTypeActivityByProduct" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>



                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="txtCKISProductNameActivityByProduct">
                                            System Product Name
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtCKISProductNameActivityByProduct" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="txtCKISProductNameActivityByProduct">
                                            Search By Keyword
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtKeyWordByProduct" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group row col-sm-12">
                                        <asp:Button ID="btnSearchActivityByProduct" runat="server" OnClick="btnSearchActivityByProduct_Click" CssClass="btn btn-primary btn-sm" Text="Search" />
                                        <asp:Button ID="btnResetActivityByProduct" runat="server" OnClick="btnResetActivityByProduct_Click" CssClass="btn btn-primary btn-sm" Text="Reset" CausesValidation="false" />
                                    </div>

                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="panel-group" id="accordionCKISProductResult">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4 class="panel-title"><a data-toggle="collapse" data-parent="#accordion" href="#collapseCKISProductResult">CKIS Product Masters (Total Count:
                                <asp:Label ID="lblCKISProductMasters" runat="server" Text="0"></asp:Label>)</a></h4>
                        </div>
                        <div id="collapseCKISProductResult" class="panel-collapse collapse in">
                            <div class="panel-body">

                                <div class="col-sm-2 pull-right">
                                    <div class="input-group">
                                        <label class="input-group-addon" for="ddlPageSize"><strong>Page Size</strong></label>
                                        <asp:DropDownList ID="ddlPageSizeActivityByProduct" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPageSizeActivityByProduct_SelectedIndexChanged" AutoPostBack="true" Width="100px">
                                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                            <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <br />
                                <div id="div1" runat="server" style="display: none;"></div>
                                <asp:GridView ID="grdActivitySearchByProduct" runat="server" AllowPaging="True" AllowCustomPaging="true" AutoGenerateColumns="False" DataKeyNames="Activity_ID"
                                    CssClass="table table-responsive table-hover table-striped table-bordered" OnPageIndexChanging="grdActivitySearchByProduct_PageIndexChanging" OnRowCommand="grdActivitySearchByProduct_RowCommand" PagerStyle-CssClass="Page navigation" EmptyDataText="No Mapping Defined.">
                                    <Columns>
                                        <asp:BoundField DataField="CommonProductID" HeaderText="CKIS Id" />
                                        <asp:BoundField DataField="ProductSubType" HeaderText="CKIS Product Type" />
                                        <asp:BoundField DataField="ProductCategory" HeaderText="CKIS Category" />
                                        <asp:BoundField DataField="Country" HeaderText="Country" />
                                        <asp:BoundField DataField="City" HeaderText="City" />
                                        <asp:BoundField DataField="Product_Name" HeaderText="Product Name" />
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                                    Enabled="true" CommandArgument='<%# Bind("Activity_ID") %>' OnClientClick="showmoProductWiseMapping();">
                                                            <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Select
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>

                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <!-- PRODUCT WISE MAPPING SCREEN MODAL -->
        <div class="modal fade" id="moProductWiseMapping" role="dialog">
            <div class="">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Product-wise Activity Mapping</h4>
                    </div>
                    <div class="modal-body fontForModel">
                        <div class="row">

                            <div class="col-lg-8">
                                <div class="panel panel-default">
                                    <div class="panel-heading">TLGX Activity Sub Types</div>
                                    <div class="panel-body">
                                        <!-- panel should scoll -->
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:FormView ID="frmVwMasterActivityDetails" AllowPaging="true" runat="server">
                                                    <HeaderStyle ForeColor="white" BackColor="Blue" />
                                                    <ItemTemplate>
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <p><strong>General Information</strong></p>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-12 span">
                                                                <table class="table table-condensed">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td><strong>CKIS Type</strong></td>
                                                                            <td>
                                                                                <asp:HiddenField ID="hdnActivityId" runat="server" Value='<%# Eval("Activity_Id") %>' />
                                                                                <asp:Label ID="lblProductSubType" runat="server" Text='<%# Eval("ProductSubType") %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><strong>Type</strong></td>
                                                                            <td>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ProductCategory") %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr id="trStartingPoint" runat="server" visible='<%# !string.IsNullOrWhiteSpace((string)Eval("StartingPoint")) %>'>
                                                                            <td><strong>Starting Point</strong></td>
                                                                            <td>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("StartingPoint") %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr id="trEndingPoint" runat="server" visible='<%# !string.IsNullOrWhiteSpace((string)Eval("EndingPoint")) %>'>
                                                                            <td><strong>Ending Point</strong></td>
                                                                            <td>
                                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("EndingPoint") %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr id="trDuration" runat="server" visible='<%# !string.IsNullOrWhiteSpace((string)Eval("Duration")) %>'>
                                                                            <td><strong>Duration</strong></td>
                                                                            <td>
                                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("Duration") %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr id="trCompanyRecommended" runat="server" visible='<%# !string.IsNullOrWhiteSpace((string)Eval("CompanyRecommended")) %>'>
                                                                            <td><strong>Company Recommended</strong></td>
                                                                            <td>
                                                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("CompanyRecommended") %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr id="trMealsYN" runat="server" visible='<%# Convert.ToString(Eval("MealsYN")) == string.Empty ? false : true %>'>
                                                                            <td><strong>MealsYN</strong></td>
                                                                            <td>
                                                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("MealsYN") %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr id="trGuideYN" runat="server" >
                                                                            <td><strong>GuideYN</strong></td>
                                                                            <td>
                                                                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("GuideYN") %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr id="trTransferYN" runat="server" >
                                                                            <td><strong>TransferYN</strong></td>
                                                                            <td>
                                                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("TransferYN") %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr id="trPhysicalLevel" runat="server" >
                                                                            <td><strong>Physical Level</strong></td>
                                                                            <td>
                                                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("PhysicalLevel") %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr id="trAdvisory" runat="server" >
                                                                            <td><strong>Advisory</strong></td>
                                                                            <td>
                                                                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("Advisory") %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr id="trThingsToCarry" runat="server" >
                                                                            <td><strong>Things To Carry</strong></td>
                                                                            <td>
                                                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("ThingsToCarry") %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr id="trDeparturePoint" runat="server" >
                                                                            <td><strong>Departure Point </strong></td>
                                                                            <td>
                                                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("DeparturePoint") %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr id="trTourType" runat="server" >
                                                                            <td><strong>Tour Type</strong></td>
                                                                            <td>
                                                                                <asp:Label ID="Label14" runat="server" Text='<%# Eval("TourType") %>'></asp:Label></td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                            <div class="col-lg-12">
                                                                <strong>Short Description</strong>
                                                                <hr />
                                                                <asp:Label ID="lblShortDescription" CssClass="" runat="server" Text='<%# Server.HtmlDecode(Convert.ToString(Eval("ShortDescription"))) %>'></asp:Label>
                                                            </div>
                                                            <br />
                                                            <div class="col-lg-12">
                                                                <ajaxToolkit:CollapsiblePanelExtender ID="cpe" runat="Server"
                                                                    TargetControlID="pnlLongDes"
                                                                    CollapsedSize="0"
                                                                    Collapsed="true"
                                                                    ExpandControlID="pnlClickLongDes"
                                                                    CollapseControlID="pnlClickLongDes"
                                                                    AutoCollapse="False"
                                                                    AutoExpand="False"
                                                                    ImageControlID="Image1"
                                                                    ExpandedImage="~/images/collapse.jpg"
                                                                    CollapsedImage="~/images/expand.jpg"
                                                                    ExpandDirection="Vertical" />
                                                                <asp:Panel ID="pnlClickLongDes" runat="server">
                                                                    <div style="font-weight: bold;">
                                                                        <br />
                                                                        <strong>Long Description</strong>
                                                                        <asp:Image ImageUrl="~/images/expand.jpg" ID="Image1" runat="server" />
                                                                    </div>
                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlLongDes" runat="server">
                                                                    <hr />
                                                                    <asp:Literal ID="lblLongDescription" Mode="Transform" runat="server" Text='<%# Server.HtmlDecode(Convert.ToString(Eval("LongDescription"))) %>'></asp:Literal>
                                                                </asp:Panel>
                                                            </div>
                                                            <div class="row col-lg-12">
                                                                <hr />
                                                                <div class="col-lg-6">
                                                                    <p><strong>Inclusions</strong></p>
                                                                    <hr />
                                                                    <table class="table table-condensed table-striped">
                                                                        <asp:Repeater ID="rptInclusions" runat="server">
                                                                            <HeaderTemplate>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblInclusion" runat="server" Text='<%# Server.HtmlDecode(Convert.ToString(Eval("Content_Text"))) %>'></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                            </FooterTemplate>
                                                                        </asp:Repeater>
                                                                    </table>
                                                                </div>
                                                                <div class="col-lg-6">
                                                                    <p><strong>Exclusions</strong></p>
                                                                    <hr />
                                                                    <table class="table table-condensed  table-striped">
                                                                        <asp:Repeater ID="rptExclusion" runat="server">
                                                                            <HeaderTemplate>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblExclusion" runat="server" Text='<%# Server.HtmlDecode(Convert.ToString(Eval("Content_Text"))) %>'></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                            </FooterTemplate>
                                                                        </asp:Repeater>
                                                                    </table>
                                                                </div>
                                                                <hr />
                                                                <div class="col-lg-12">
                                                                    <p><strong>Notes</strong></p>
                                                                    <hr />
                                                                    <table class="table table-condensed  table-striped">
                                                                        <asp:Repeater ID="rptNotes" runat="server">
                                                                            <HeaderTemplate>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblNotes" runat="server" Text='<%# Server.HtmlDecode(Convert.ToString(Eval("Content_Text"))) %>'></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                            </FooterTemplate>
                                                                        </asp:Repeater>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                    <EmptyDataTemplate>No Data Found</EmptyDataTemplate>
                                                </asp:FormView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                </div>
                            </div>
                            <div class="col-lg-4">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">Supplier Static Information</div>
                                            <div class="panel-body">
                                                <div id="dvmsgByProductMapped" runat="server" style="display: none;" enableviewstate="false"></div>
                                                <div class="row">

                                                    <div class="col-lg-12">

                                                        <span>
                                                            <asp:Label ID="lblActivityNoByProduct" runat="server" Text=""></asp:Label>
                                                            out of
                                                                <asp:Label ID="lblTotalCountActivityByProduct" runat="server" Text=""></asp:Label>
                                                            Products</span>
                                                        <asp:Button ID="btnPreviousByProduct" runat="server" CssClass="btn btn-default " Text="<" Enabled="false" OnClick="btnPreviousByProduct_Click" />
                                                        <asp:Button ID="btnNextByProduct" runat="server" CssClass="btn btn-default " Text=">" Enabled="false" OnClick="btnNextByProduct_Click" />
                                                        <a href="#" data-toggle="collapse" class="btn btn-default" id="filterlinkForSupplier">Filter</a>
                                                        <asp:Button ID="btnMapActivityByProduct" runat="server" CssClass="btn btn-primary " Text="Map" OnClick="btnMapActivityByProduct_Click" />
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="Server"
                                                                    TargetControlID="pnlFilterSupplierForMapping"
                                                                    CollapsedSize="0"
                                                                    Collapsed="true"
                                                                    ExpandControlID="filterlinkForSupplier"
                                                                    CollapseControlID="filterlinkForSupplier"
                                                                    AutoCollapse="False"
                                                                    AutoExpand="False"
                                                                    TextLabelID="Label1"
                                                                    ExpandDirection="Vertical" />
                                                                <asp:Panel ID="pnlFilterSupplierForMapping" runat="server">
                                                                    <div id="demo1" class="panel panel-default">
                                                                        <div class="panel-body">
                                                                            <div class="form-group row">
                                                                                <label class="control-label col-sm-4" for="ddlSupplierFilterforMappingByProduct">
                                                                                    Suppplier
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlSupplierFilterforMappingByProduct" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true">
                                                                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>

                                                                            <div class="form-group row">
                                                                                <label class="control-label col-sm-4" for="txtCountryFileterByProductSupplier">
                                                                                    Country
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtCountryFileterByProductSupplier" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>

                                                                            <div class="form-group row">
                                                                                <label class="control-label col-sm-4" for="txtCityFileterByProductSupplier">
                                                                                    City
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtCityFileterByProductSupplier" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group row">
                                                                                <label class="control-label col-sm-4" for="txtKeywordFilterByProducntSupplier">
                                                                                    Search By Keyword
                                                                                </label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtKeywordFilterByProducntSupplier" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <asp:Button ID="btnSearchSupplierForMapping" OnClick="btnSearchSupplierForMapping_Click" runat="server" CssClass="btn btn-primary " Text="Filter" />
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="panel panel-default">
                                                    <div class="panel-body">
                                                        <asp:FormView ID="frmvwSupplierActivtiy" AllowPaging="true" runat="server">
                                                            <HeaderStyle ForeColor="white" BackColor="Blue" />
                                                            <ItemTemplate>
                                                                <div class="panel-body">
                                                                    <div class="span">
                                                                        <table class="table table-condensed span">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td><strong>ID</strong></td>
                                                                                    <td>
                                                                                        <asp:HiddenField ID="hdnActivitySupplierProductMapping_Id" runat="server" Value='<%# Eval("ActivitySupplierProductMapping_Id") %>' />
                                                                                        <asp:Label ID="lblProductSubType" runat="server" Text='<%# Eval("SuplierProductCode") %>'></asp:Label></td>
                                                                                </tr>

                                                                                <tr>
                                                                                    <td><strong>Type</strong></td>
                                                                                    <td>
                                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("SupplierProductType") %>'></asp:Label></td>
                                                                                </tr>

                                                                                <tr>
                                                                                    <td><strong>Country</strong></td>
                                                                                    <td>
                                                                                        <asp:Label ID="Label15" runat="server" Text='<%# Eval("SupplierCountryName") %>'></asp:Label></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td><strong>City</strong></td>
                                                                                    <td>
                                                                                        <asp:Label ID="Label16" runat="server" Text='<%# Eval("SupplierCityName") %>'></asp:Label></td>
                                                                                </tr>

                                                                                <tr>
                                                                                    <td><strong>Name</strong></td>
                                                                                    <td>
                                                                                        <asp:Label ID="Label17" runat="server" Text='<%# Eval("SupplierProductName") %>'></asp:Label></td>
                                                                                </tr>

                                                                                <tr>
                                                                                    <td><strong>Address</strong></td>
                                                                                    <td>
                                                                                        <asp:Label ID="Label18" runat="server" Text='<%# Eval("Address") %>'></asp:Label></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <p><strong>Inclusions</strong></p>
                                                                        <hr />
                                                                        <p>
                                                                            <asp:Label ID="lblInclusionSupplierByProduct" runat="server" Text='<%# Eval("Inclusions") %>'></asp:Label>
                                                                        </p>

                                                                        <p><strong>Exclusions</strong></p>
                                                                        <hr />
                                                                        <p>
                                                                            <asp:Label ID="lblExclusionsSupplierByProduct" runat="server" Text='<%# Eval("Exclusions") %>'></asp:Label>
                                                                        </p>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:FormView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <!-- existing mapping grid -->
                        <div class="panel panel-default">
                            <div class="panel-heading">Existing Mapping</div>
                            <div class="panel-body">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <div id="dvmsgByProductUnMapped" runat="server" style="display: none;" enableviewstate="false"></div>

                                        <asp:GridView ID="grdvwMappedActivityByProduct" runat="server" AllowPaging="True" AllowCustomPaging="true" AutoGenerateColumns="False" DataKeyNames="ActivitySupplierProductMapping_Id"
                                            CssClass="table table-responsive table-hover table-striped table-bordered"
                                            OnRowCommand="grdvwMappedActivityByProduct_RowCommand" PagerStyle-CssClass="Page navigation" EmptyDataText="No Mapping Defined.">
                                            <Columns>
                                                <asp:BoundField DataField="MapID" HeaderText="Map Id" />
                                                <asp:BoundField DataField="SupplierName" HeaderText="Supplier" />
                                                <asp:BoundField DataField="SuplierProductCode" HeaderText="ID Supplier" />
                                                <asp:BoundField DataField="SupplierCountryName" HeaderText="Country" />
                                                <asp:BoundField DataField="SupplierCityName" HeaderText="City" />
                                                <asp:BoundField DataField="SupplierProductName" HeaderText="Name" />
                                                <asp:BoundField DataField="ourRef" HeaderText="Our Ref" />
                                                <asp:BoundField DataField="SystemCountryName" HeaderText="Country">
                                                    <HeaderStyle BackColor="Turquoise" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SystemCityName" HeaderText="City">
                                                    <HeaderStyle BackColor="Turquoise" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CKIS_Master" HeaderText="CKIS Master">
                                                    <HeaderStyle BackColor="Turquoise" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MappingStatus" HeaderText="Status" />
                                                <asp:TemplateField ShowHeader="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Unmap" CssClass="btn btn-default"
                                                            Enabled="true" CommandArgument='<%# Bind("ActivitySupplierProductMapping_Id") %>'>
                                                            <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Unmap
                                                        </asp:LinkButton>
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
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
