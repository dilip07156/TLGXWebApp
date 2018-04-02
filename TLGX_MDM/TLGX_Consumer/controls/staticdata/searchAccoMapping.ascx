<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchAccoMapping.ascx.cs" Inherits="TLGX_Consumer.controls.staticdata.AccoMap" %>
<%@ Register Src="~/controls/staticdata/bulkHotelMapping.ascx" TagPrefix="uc1" TagName="bulkHotelMapping" %>
<%@ Register Src="~/controls/hotel/AddNew.ascx" TagPrefix="uc2" TagName="AddNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script src="../../Scripts/JqueryUI/jquery-ui.js"></script>
<link href="../../Scripts/JqueryUI/jquery-ui.css" rel="stylesheet" />
<style type="text/css">
    .hideColumn {
        display: none;
    }

    .x-lg {
        width: 1200px;
    }

    .ui-autocomplete {
        z-index: 99999999 !important;
    }
</style>
<script type="text/javascript">
    $(function () {
        $("#accordion").accordion();
    });
    function showCityMappingModal() {
        //alert("Hi");
        $("#moCityMapping").modal('show');
    }
    function closeCityMappingModal() {
        //alert("Hi");
        $("#moCityMapping").modal('hide');
    }
    function pageLoad(sender, args) {
        var hv = $('#MainContent_searchAccoMapping_hdnFlag').val();
        if (hv == "true") {
            closeCityMappingModal();
            $('#MainContent_searchAccoMapping_hdnFlag').val("false");
        }
    }
    function removeRsrtNmSpace(controlID, e) {
        var str = controlID.id;
        var contex = document.getElementById(str.replace("txtProductName", "hdnContext"));
        var country = document.getElementById(str.replace("txtProductName", "ddlCountryName"));
        var city = document.getElementById(str.replace("txtProductName", "ddlCity"));
        var chain = document.getElementById(str.replace("txtProductName", "ddlChain"));
        var brand = document.getElementById(str.replace("txtProductName", "ddlBrand"));
        var exten = document.getElementById(str.replace("txtProductName", "txtProductName_autoCompleteExtender"));
        contex.value = "<country>~<city>~<brand>~<chain>~<name>";
        var cont = contex.value;
        cont = cont.replace("<country>~", country.options[country.selectedIndex].text + "~");
        cont = cont.replace("<city>~", city.options[city.selectedIndex].text + "~");
        cont = cont.replace("<chain>~", chain.options[chain.selectedIndex].text + "~");
        cont = cont.replace("<brand>~", brand.options[brand.selectedIndex].text + "~");
        cont = cont.replace("<name>", controlID.value);
        $find(contex).set_contextKey(cont);
        $find(controlID).set_contextKey(cont);
        //controlID.value = cont;
    }

    $(document).ready(function () {
        callajax();
    });
    function checkLen(val) {
        var rfvtxtSearchSystemProduct = document.getElementById("MainContent_searchAccoMapping_frmEditProductMap_rfvtxtSearchSystemProduct");
        var hdnSelSystemProduct_Id = document.getElementById("MainContent_searchAccoMapping_frmEditProductMap_hdnSelSystemProduct_Id");
        if (val.length == 0) {
            val.text = null;
            hdnSelSystemProduct_Id.value = null;
            ValidatorEnable(rfvtxtSearchSystemProduct, true);
            document.getElementById("MainContent_searchAccoMapping_frmEditProductMap_btnAddProduct").style.display = "block";
        }
        else {
            ValidatorEnable(rfvtxtSearchSystemProduct, false);
            document.getElementById("MainContent_searchAccoMapping_frmEditProductMap_btnAddProduct").style.display = "none";
        }
    }

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        callajax();
    });
    function callajax() {
        $("[id*=txtSearch]").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '../../Service/HotelNameAutoComplete.ashx',
                    dataType: "json",
                    data: {
                        term: request.term,
                        country: $("[id*=ddlCountryName]").children("option:selected").text(),
                        city: $("[id*=ddlCity]").children("option:selected").text(),
                        chain: $("[id*=ddlChain]").children("option:selected").text(),
                        brand: $("[id*=ddlBrand]").children("option:selected").text()
                    },
                    success: function (data) {
                        response(data);
                    }
                });
            },
            min_length: 3,
            delay: 300
        });
        $("[id*=txtSystemProductName]").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '../../Service/HotelNameAutoComplete.ashx',
                    dataType: "json",
                    data: {
                        term: request.term,
                        country: $("[id*=ddlSystemCountryName]").children("option:selected").text(),
                        city: $("[id*=ddlSystemCityName]").children("option:selected").text()
                    },
                    success: function (data) {
                        response(data);
                    }
                });
            },
            min_length: 3,
            delay: 300
        });

        $("[id*=txtSupCountry]").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '../../Service/CountryAutoComplete.ashx',
                    dataType: "json",
                    data: {
                        term: request.term
                    },
                    success: function (data) {
                        response(data);
                    }
                });
            },
            min_length: 3,
            delay: 300
        });

        $("[id*=txtSupCity]").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '../../Service/CityAutoComplete.ashx',
                    dataType: "json",
                    data: {
                        term: request.term,
                        country: $("[id*=txtSupCountry]").val()
                    },
                    success: function (data) {
                        response(data);
                    }
                });
            },
            min_length: 3,
            delay: 300
        });

        $("[id*=txtSupProductName]").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '../../Service/StaticDataHotelsAutoComplete.ashx',
                    dataType: "json",
                    data: {
                        term: request.term,
                        country: $("[id*=txtSupCountry]").val(),
                        city: $("[id*=txtSupCity]").val()
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
                                    if (result[i].StateCode != null) {
                                        hotelname = hotelname + " (" + result[i].StateCode.substring(3, result[i].StateCode.length) + ")";
                                    }
                                    if (result[i].Country != null) {
                                        hotelname = hotelname + ", " + result[i].Country;
                                    }
                                    hdnSystemProduct.value = hdnSystemProduct.value + hotelname + "`";
                                    data.push(hotelname);
                                }
                            }
                            response(data);
                        }
                        else
                        {
                            var data = [];
                            var NoDataFound = "No Data Found";
                            data.push(NoDataFound);
                            response(data);
                        }
                    }
                });
            },
            min_length: 3,
            delay: 300
        });

        $("[id*=txtSearchSystemProduct]").on('autocompleteselect', function (e, ui) {

            if (ui.item.value != "No Data Found")
                callSystemProductNamechange(ui.item.value);
            else {
                ui.item.value = null;
                document.getElementById("MainContent_searchAccoMapping_frmEditProductMap_txtSearchSystemProduct").value = "";
            }
            
        });

    }
    function callSystemProductNamechange(selection) {

        var vrtxtSearch = document.getElementById("MainContent_searchAccoMapping_frmEditProductMap_txtSearch");
        var hdnSystemProduct_Id = document.getElementById("MainContent_searchAccoMapping_frmEditProductMap_hdnSystemProduct_Id");
        var hdnSystemProduct = document.getElementById("MainContent_searchAccoMapping_frmEditProductMap_hdnSystemProduct");
        var hdnSelSystemProduct_Id = document.getElementById("MainContent_searchAccoMapping_frmEditProductMap_hdnSelSystemProduct_Id");
        var vrbtnAddProduct = document.getElementById("MainContent_searchAccoMapping_frmEditProductMap_btnAddProduct");
        var selId = "";
        if (selection != null) {
            vrbtnAddProduct.style.display = "none";
            if (selection.trim() != "") {
                var brkvrhdnSystemProduct = hdnSystemProduct.value.split('`');
                var brkvrhdnSystemProduct_Id = hdnSystemProduct_Id.value.split('`');
                var idx = brkvrhdnSystemProduct.indexOf(selection);
                if (idx != null && idx != (-1)) {
                    selId = brkvrhdnSystemProduct_Id[idx];
                }
            }
        }

        var ddlSystemProductName = document.getElementById("MainContent_searchAccoMapping_frmEditProductMap_ddlSystemProductName");
        var listItems = '';
        if (selId != null) {
            ddlSystemProductName.innerHTML = "";
            listItems += '<option selected="selected" value="' + selId + '">' + brkvrhdnSystemProduct[0].split(';')[0] + '</option>';
            ddlSystemProductName.append(listItems);
            if (vrbtnAddProduct != null)
                vrbtnAddProduct.style.display = "none";

        }


        var ddlSystemCityName = document.getElementById("MainContent_searchAccoMapping_frmEditProductMap_ddlSystemCityName");
        var ddlSystemStateName = document.getElementById("MainContent_searchAccoMapping_frmEditProductMap_ddlSystemStateName");
        var txtSystemProductCode = document.getElementById("MainContent_searchAccoMapping_frmEditProductMap_txtSystemProductCode");
        var lblSystemProductAddress = document.getElementById("MainContent_searchAccoMapping_frmEditProductMap_lblSystemProductAddress");
        var lblSystemLocation = document.getElementById("MainContent_searchAccoMapping_frmEditProductMap_lblSystemLocation");
        var lblSystemTelephone = document.getElementById("MainContent_searchAccoMapping_frmEditProductMap_lblSystemTelephone");
        var lblSystemLatitude = document.getElementById("MainContent_searchAccoMapping_frmEditProductMap_lblSystemLatitude");
        var lblSystemLongitude = document.getElementById("MainContent_searchAccoMapping_frmEditProductMap_lblSystemLongitude");

        if (selId != "") {
            if (vrbtnAddProduct != null)
                vrbtnAddProduct.style.display = "none";
            hdnSelSystemProduct_Id.value = selId;
            $.ajax({
                url: '../../Service/HotelMappingAutoComplete.ashx',
                dataType: "json",
                data: {
                    accoid: selId,
                    source: 'details'
                },
                responseType: "json",
                success: function (result) {
                    if (result != null) {
                        txtSystemProductCode.value = result[0].CompanyHotelID;
                        lblSystemProductAddress.innerHTML = result[0].FullAddress;
                        lblSystemLocation.innerHTML = result[0].Location;
                        lblSystemTelephone.innerHTML = result[0].Telephone_Tx;
                        lblSystemLatitude.innerHTML = result[0].Latitude;
                        lblSystemLongitude.innerHTML = result[0].Longitude;
                        if (result[0].City != null) {

                            for (var i = 0; i < ddlSystemCityName.options.length; i++)
                            {
                                if (ddlSystemCityName.options[i].text == result[0].City)
                                {
                                    ddlSystemCityName.options[i].selected = true;
                                    break;
                                }
                            }
                        }
                        if (ddlSystemStateName.options[ddlSystemStateName.selectedIndex].value == "0") {
                            if (result[0].State_Name != null) {
                                for (var i = 0; i < ddlSystemStateName.options.length; i++) {
                                    if (ddlSystemStateName.options[i].text == result[0].State_Name) {
                                        ddlSystemStateName.options[i].selected = true;
                                        break;
                                    }
                                }
                            }
                        }


                    }
                },
                failure: function () {
                }
            });
        }
        else {
            if (vrbtnAddProduct != null)
                vrbtnAddProduct.style.display = "none";
        }

    }

    function SelectedRow(element) {
        var ddlStatus = $('#MainContent_searchAccoMapping_ddlMappingStatus option:selected').html();
        if (ddlStatus == "REVIEW") {
            element.parentNode.parentNode.nextSibling.childNodes[15].lastElementChild.focus();
        }
        else if (ddlStatus == "UNMAPPED") {
            element.parentNode.parentNode.nextSibling.childNodes[6].lastElementChild.focus();
        }
    }
    function MatchedSelect(elem) {
        elem.parentNode.parentNode.nextSibling.childNodes[11].lastElementChild.focus();
    }
    function fillDropDown(record, onClick) {
        if (onClick) {
            //Getting Dropdown
            var currentRow = $(record).parent().parent();
            var countryname = currentRow.find("td:eq(7)").text();
            var cityname = currentRow.find("td:eq(9)").text();

            var AccoDDL = currentRow.find("td:eq(10)").find('select');
            var selectedText = AccoDDL.find("option:selected").text();
            var selectedOption = AccoDDL.find("option");
            var selectedVal = AccoDDL.val();
            if (countryname != null || cityname != null) {
                if (AccoDDL != null && AccoDDL.is("select")) {
                    $.ajax({
                        url: '../../../Service/ToFillDDL.ashx',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: {
                            'EntityType': 'acco',
                            'countryname': countryname,
                            'cityname': cityname
                        },
                        responseType: "json",
                        success: function (result) {
                            AccoDDL.find("option:not(:first)").remove();
                            var value = JSON.stringify(result);
                            var listItems = '';
                            if (result != null) {
                                for (var i = 0; i < result.length; i++) {
                                    listItems += "<option value='" + result[i].AccomodationId + "'>" + result[i].HotelName + "</option>";
                                }
                                AccoDDL.append(listItems);
                            }

                            AccoDDL.find("option").prop('selected', false).filter(function () {
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
        // debugger;
        if (!onClick) {
            var currentRow = $(record).parent().parent();
            var AccoDDL = currentRow.find("td:eq(10)").find('select');
            var selectedText = AccoDDL.find("option:selected").text();
            var selectedVal = AccoDDL.val();
            AccoDDL.find("option:not(:first)").remove();
            var listItems = "<option selected = 'selected' value='" + selectedVal + "'>" + selectedText + "</option>";
            AccoDDL.append(listItems);
            var acco_id = record.parentNode.parentNode.childNodes[16].firstElementChild;
            acco_id.value = selectedVal;
        }
    }

    function ddlStatusChanged(ddl) {
        //debugger;
        var ddlStatus = $('#MainContent_searchAccoMapping_frmEditProductMap_ddlStatus option:selected').html();
        var mySystemCountryName = document.getElementById("MainContent_searchAccoMapping_frmEditProductMap_vddlSystemCountryName");
        var mySystemCityName = document.getElementById("MainContent_searchAccoMapping_frmEditProductMap_vddlSystemCityName");
        var myProductName = document.getElementById("MainContent_searchAccoMapping_frmEditProductMap_vddlSystemProductName");
        //var myVal = $('#vddlSystemCountryName').val();
        if (ddlStatus == 'DELETE') {
            ValidatorEnable(mySystemCountryName, false);
            ValidatorEnable(mySystemCityName, false);
            ValidatorEnable(myProductName, false);
        }
        else {
            ValidatorEnable(mySystemCountryName, true);
            ValidatorEnable(mySystemCityName, true);
            ValidatorEnable(myProductName, true);
        }
    }
</script>
<div id="myWizard">

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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="panel-group" id="accordionSupplierSearch">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title"><a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Filters</a>
                            </div>
                            <div id="collapseSearch" class="panel-collapse collapse in">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlSupplierName">
                                                Supplier Name
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                            <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlCountry">
                                                System Country
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                            <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlCity">
                                                System City
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlSupplierCity" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSupplierCity_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                            <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlProduct">
                                                Product Name
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control" AppendDataBoundItems="true">
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
                                        <div class="col-lg-4">
                                            <div class="form-group row">
                                            <label class="control-label col-sm-4" for="txtSuppName">Supplier Country</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtSuppCountry" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                            <div class="form-group row">
                                            <label class="control-label col-sm-4" for="txtSuppName">Supplier City</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtSuppCity" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                            <div class="form-group row">
                                            <label class="control-label col-sm-4" for="txtSuppName">Supplier Product</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtSuppProduct" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                        <div class="col-lg-4">
                                            <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlPageSize">Page Size</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control col-lg-3">
                                                    <asp:ListItem Text="5" Value="5" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                            <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlMatchedBy">
                                                Matched By
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlMatchedBy" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="99">-Select-</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                            <div class="form-group row">
                                            <div class="col-sm-12">
                                                <asp:HiddenField ID="hdnPageNumber" runat="server" Value="0" />
                                            </div>
                                        </div>

                                            <div class="form-group row">
                                                <div class="col-sm-12">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" />
                                            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" CausesValidation="false" OnClick="btnReset_Click" />
                                        </div>
                                        </div>

                                            <div class="form-group row">
                                                <div class="col-sm-12">
                                            <asp:Button ID="Button4" runat="server" CssClass="btn btn-primary btn-sm" Text="Add Mapping by Supplier" CausesValidation="false" Visible="false" />
                                            <!-- wire me up to go to /addProductMapping add straight to Supplier Search -->
                                        </div>
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
                                <asp:Label ID="lblAccoMaps" runat="server" Text="0"></asp:Label>)</a>
                                </h4>
                                <div class="form-group pull-right">
                                    <asp:Button ID="btnMapSelected" runat="server" CssClass="btn btn-primary btn-sm" Text="Map Selected" OnClick="btnMapSelected_Click" />
                                    <asp:Button ID="btnMapAll" runat="server" CssClass="btn btn-primary btn-sm" Text="Map All" OnClick="btnMapAll_Click" />
                                </div>
                            </div>
                            <div id="collapseSearchResult" class="panel-collapse collapse in">
                                <div class="panel-body">
                                    <div id="divMsgForMapping" runat="server" style="display: none;"></div>
                                    <div class="col-lg-12">
                                        <!-- if you adjust the grid you will need to adjust the codebehin that is generating the super header -->
                                        <asp:GridView ID="grdAccoMaps" runat="server" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="No Static Updates"
                                            CssClass="table table-hover table-striped" OnDataBound="grdAccoMaps_DataBound" OnRowCommand="grdAccoMaps_RowCommand"
                                            AllowCustomPaging="true" OnPageIndexChanging="grdAccoMaps_PageIndexChanging" DataKeyNames="Accommodation_ProductMapping_Id,Accommodation_Id,Supplier_Id,mstAcco_Id,mstHotelName"
                                            OnRowDataBound="grdAccoMaps_RowDataBound">
                                            <Columns>
                                                <%--<asp:BoundField HeaderText="Map Id" DataField="MapId" />--%>
                                                <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                                                <asp:BoundField DataField="ProductId" HeaderText="ProductId" />
                                                <asp:BoundField DataField="ProductName" HeaderText="ProductName" />
                                                <asp:BoundField DataField="FullAddress" HeaderText="Address" />
                                                <asp:BoundField DataField="TelephoneNUmber" HeaderText="Tel" />
                                                <%--   <asp:BoundField DataField="CountryCode" HeaderText="Country Code" />--%>
                                                <asp:BoundField DataField="CountryName" HeaderText="Country Name" />
                                                <%--   <asp:BoundField DataField="CityCode" HeaderText="City Code" />--%>
                                                <asp:TemplateField HeaderText="City">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# string.IsNullOrWhiteSpace(Convert.ToString(Eval("CityCode"))) ? Eval("CityName") : Eval("CityName") + " (" + Eval("CityCode") + " )" %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="CityName" HeaderText="City Name" />--%>
                                                <asp:BoundField DataField="SystemProductName" HeaderText="System Product">
                                                    <HeaderStyle BackColor="Turquoise" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SystemCountryName" HeaderText="Country Name">
                                                    <HeaderStyle BackColor="Turquoise" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SystemCityName" HeaderText="City Name">
                                                    <HeaderStyle BackColor="Turquoise" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SystemFullAddress" HeaderText="Address">
                                                    <HeaderStyle BackColor="Turquoise" />
                                                </asp:BoundField>
                                                <asp:TemplateField ShowHeader="true" HeaderText="Master Product Name">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlGridProduct" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="false" onfocus="fillDropDown(this,true);" onchange="RemoveExtra(this,false);" onclick="fillDropDown(this,true);">
                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderStyle BackColor="Turquoise" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Location" HeaderText="Location">
                                                    <HeaderStyle BackColor="Turquoise" />
                                                </asp:BoundField>                                                
                                                <asp:TemplateField ShowHeader="true" HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status")%>'></asp:Label>
                                                        <asp:Label ID="lblMatchedBy" runat="server" Text='<%# (Convert.ToString(Eval("Status")) == "REVIEW" || Convert.ToString(Eval("Status")) == "AUTOMAPPED") ? (string.IsNullOrWhiteSpace(Convert.ToString(Eval("MatchedBy"))) ? "" :  " (" + Eval("MatchedBy") + ")") : "" %>' ToolTip='<%# Bind("MatchedByString")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                                            Enabled="true" CommandArgument='<%# Bind("Accommodation_ProductMapping_Id") %>' OnClientClick="showCityMappingModal();">
                                                            <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Select
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="false">
                                                    <ItemTemplate>
                                                        <input type="checkbox" runat="server" id="chkSelect" onclick="SelectedRow(this);" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="hideColumn" HeaderStyle-CssClass="hideColumn">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnAcco_Id" Value="" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pagination-ys" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="tab-pane fade in" id="panProductSearch">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="panel-group" id="accordionProductSearch">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseProductSearch">Search Filters</a>
                                </h4>
                            </div>
                            <div id="collapseProductSearch" class="panel-collapse collapse in">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group row">
                                                <asp:HiddenField ID="hdnContext" runat="server" Value="" />
                                                <label class="control-label col-sm-4" for="ddlCountryName">
                                                    System Country
                                                </label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlCountryName" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCountryName_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="control-label col-sm-4" for="ddlCity">
                                                    System City
                                                </label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="control-label col-sm-4" for="ddlChain">
                                                    Chain
                                                </label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlChain" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="control-label col-sm-4" for="ddlBrand">
                                                    Brand
                                                </label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlBrand" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="control-label col-sm-4" for="ddlProductName">
                                                    System Product 
                                                </label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="control-label col-sm-4" for="ddlProductMappingStatus">
                                                    Mapping Status
                                                </label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlProductMappingStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-lg-4">

                                            <div class="form-group row">
                                                <label class="control-label col-sm-4" for="ddlProductBasedPageSize">Page Size</label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlProductBasedPageSize" runat="server" CssClass="form-control col-lg-3">
                                                        <asp:ListItem Text="5" Value="5" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-12">
                                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="Button1_Click" />
                                                <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary btn-sm" Text="Reset" CausesValidation="false" OnClick="Button2_Click" />
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-12">
                                                <asp:Button ID="Button3" runat="server" CssClass="btn btn-primary btn-sm" Text="Add Mapping by Product" CausesValidation="false" Visible="false" />
                                                <!-- wire me up to go to /addProductMapping add straight to Product Search -->
                                            </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-group" id="accordionProductSearchResult">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseProductSearchResult">System Data search Results (Total Count:
                                <asp:Label ID="lblTLGXProdData" runat="server" Text="0"></asp:Label>)</a>
                                </h4>
                            </div>
                            <div id="collapseProductSearchResult" class="panel-collapse collapse in">
                                <div class="panel-body">
                                    <div class="col-lg-12 row">
                                        <br />
                                        <!-- if you adjust the grid you will need to adjust the codebehin that is generating the super header -->
                                        <asp:GridView ID="grdTLGXProdData" runat="server" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="No Static Updates"
                                            CssClass="table table-hover table-striped" OnDataBound="grdTLGXProdData_DataBound" OnRowCommand="grdTLGXProdData_RowCommand"
                                            AllowCustomPaging="true" OnPageIndexChanging="grdTLGXProdData_PageIndexChanging" OnRowDataBound="grdTLGXProdData_RowDataBound" DataKeyNames="AccomodationId"
                                            OnSelectedIndexChanged="grdTLGXProdData_SelectedIndexChanged">

                                            <Columns>
                                                <asp:BoundField DataField="CompanyHotelId" HeaderText="Hotel Id" InsertVisible="False" ReadOnly="True" SortExpression="CompanyHotelId" />
                                                <asp:BoundField DataField="Country" HeaderText="Country Name" SortExpression="Country" />
                                                <asp:BoundField DataField="City" HeaderText="City Name" SortExpression="City" />
                                                <asp:BoundField DataField="HotelName" HeaderText="Hotel Name" SortExpression="HotelName" />
                                                <asp:BoundField DataField="HotelChain" HeaderText="Hotel Chain" SortExpression="HotelChain" />
                                                <asp:BoundField DataField="HotelBrand" HeaderText="Hotel Brand" SortExpression="HotelBrand" />
                                                <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
                                                <asp:BoundField DataField="PostalCode" HeaderText="Postal Code" SortExpression="PostalCode" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                                <asp:TemplateField ShowHeader="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                                            Enabled="true" CommandArgument='<%# Bind("AccomodationId") %>'>
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Select
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pagination-ys" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12">
                            <asp:PlaceHolder ID="pnlLoadControl" runat="server">
                                <uc1:bulkHotelMapping runat="server" ID="bulkHotelMapping" />
                            </asp:PlaceHolder>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <!-- OPEN IN MODAL -->
    <div class="modal fade" id="moCityMapping" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg x-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <div class="panel-heading">
                        <h4 class="modal-title">Update Supplier Product Mapping</h4>
                    </div>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdCityMapModal" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdnFlag" runat="server" Value="" EnableViewState="false" />
                            <asp:FormView ID="frmEditProductMap" runat="server" DefaultMode="Insert" DataKeyNames="Accommodation_ProductMapping_Id" OnItemCommand="frmEditProductMap_ItemCommand">
                                <EditItemTemplate>
                                    <!-- should be edit item template, but using insert just to show UI -->
                                    <div class="container">
                                        <div class="row">
                                            <div class="col-lg-3">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">Supplier</div>
                                                    <div class="panel-body">
                                                        <table class="table table-condensed">
                                                            <tbody>
                                                                <tr>
                                                                    <td><strong>Supplier</strong></td>
                                                                    <td>
                                                                        <asp:Label ID="lblSupplierName" runat="server" Text=""></asp:Label>&nbsp;
                                                                        <asp:Label ID="lblSupplierCode" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td><strong>Country</strong></td>
                                                                    <td>
                                                                        <asp:Label ID="lblSupCountryName" runat="server" Text=""></asp:Label>&nbsp;
                                                                        <asp:Label ID="lblSupCountryCode" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td><strong>State</strong></td>
                                                                    <td>
                                                                        <asp:Label ID="lblSupStateName" runat="server" Text=""></asp:Label>&nbsp;
                                                                        <asp:Label ID="lblSupStateCode" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td><strong>City</strong></td>
                                                                    <td>
                                                                        <asp:Label ID="lblCityName" runat="server" Text=""></asp:Label>&nbsp;
                                                                        <asp:Label ID="lblCityCode" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td><strong>Product</strong></td>
                                                                    <td>
                                                                        <asp:Label ID="lblProductName" runat="server" Text=""></asp:Label>
                                                                        <asp:Label ID="lblHotelName_TX" runat="server" Text="" Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td><strong>Code</strong></td>
                                                                    <td>
                                                                        <asp:Label ID="lblProductCode" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td><strong>Address</strong></td>
                                                                    <td>
                                                                        <strong>
                                                                            <asp:Label ID="lblProductAddress" runat="server" Text="" Font-Bold="true"></asp:Label></strong>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td><strong>Telephone</strong></td>
                                                                    <td>
                                                                        <asp:Label ID="lblProductTelephone" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td><strong>Lat-Long</strong></td>
                                                                    <td>
                                                                        <asp:Label ID="lblProductLatitude" runat="server" Text=""></asp:Label>
                                                                        &nbsp;/&nbsp;
                                                                        <asp:Label ID="lblProductLongitude" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">System</div>
                                                    <div class="panel-body">
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-3" for="ddlSystemCountryName">Country<asp:RequiredFieldValidator ID="vddlSystemCountryName" runat="server" ErrorMessage="*" ControlToValidate="ddlSystemCountryName" InitialValue="0" CssClass="text-danger" ValidationGroup="CityMappingPop"></asp:RequiredFieldValidator></label>
                                                            <div class="col-sm-9">
                                                                <div class="col-sm-10">
                                                                    <asp:DropDownList ID="ddlSystemCountryName" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSystemCountryName_SelectedIndexChanged">
                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-sm-2" style="text-align: left; vertical-align: middle">
                                                                    <asp:Label ID="lblSystemCountryCode" runat="server" Text=""></asp:Label>
                                                                </div>
                                                                <asp:TextBox ID="txtSystemCountryCode" runat="server" Enabled="false" CssClass="form-control" Visible="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <%--For State--%>
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-3" for="ddlSystemStateName">State<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlSystemStateName" InitialValue="0" CssClass="text-danger" ValidationGroup="CityMappingPop"></asp:RequiredFieldValidator>--%></label>
                                                            <div class="col-sm-9">
                                                                <div class="col-sm-10">
                                                                    <asp:DropDownList ID="ddlSystemStateName" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSystemStateName_SelectedIndexChanged">
                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <asp:Label ID="lblSystemStateCode" runat="server" Text=""></asp:Label>
                                                                </div>
                                                                <asp:TextBox ID="txtSystemStateCode" runat="server" Enabled="false" CssClass="form-control" Visible="false"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <label class="control-label col-sm-3" for="ddlSystemCityName">City<asp:RequiredFieldValidator ID="vddlSystemCityName" runat="server" ErrorMessage="*" ControlToValidate="ddlSystemCityName" InitialValue="0" CssClass="text-danger" ValidationGroup="CityMappingPop"></asp:RequiredFieldValidator></label>
                                                            <div class="col-sm-9">
                                                                <div class="col-sm-10">
                                                                    <asp:DropDownList ID="ddlSystemCityName" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSystemCityName_SelectedIndexChanged">
                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <asp:Label ID="lblSystemCityCode" runat="server" Text=""></asp:Label>
                                                                </div>
                                                                <asp:TextBox ID="txtSystemCityCode" runat="server" Enabled="false" CssClass="form-control" Visible="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="control-label col-sm-3" for="ddlSystemProductName">
                                                                Product
                                                            <asp:RequiredFieldValidator ID="vddlSystemProductName" runat="server" ErrorMessage="*" ControlToValidate="ddlSystemProductName" InitialValue="0" CssClass="text-danger" ValidationGroup="CityMappingPop"></asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator ID="rfvtxtSearchSystemProduct" runat="server" ErrorMessage="*" ControlToValidate="txtSearchSystemProduct" CssClass="text-danger" ValidationGroup="CityMappingPop"></asp:RequiredFieldValidator>

                                                            </label>
                                                            <div class="col-sm-9">
                                                                <div class="col-sm-10">
                                                                    <asp:TextBox ID="txtSearchSystemProduct" onkeyup="checkLen(this.value)" runat="server" CssClass="form-control" onlostfocus="callSystemProductNamechange(this);"></asp:TextBox>

                                                                    <%--AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSystemProductName_SelectedIndexChanged"--%>
                                                                    <asp:DropDownList ID="ddlSystemProductName" Style="display: none" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                    <asp:HiddenField ID="hdnSystemProduct_Id" runat="server" />
                                                                    <asp:HiddenField ID="hdnSystemProduct" runat="server" />
                                                                    <asp:HiddenField ID="hdnSelSystemProduct_Id" runat="server" />
                                                                </div>
                                                                <div class="col-sm-2">&nbsp;</div>
                                                            </div>
                                                        </div>
                                                        <%-- <div class="form-group">
                                                            <label class="control-label col-sm-3" for="ddlSystemProductName">
                                                                Product
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlSystemProductName" InitialValue="0" CssClass="text-danger" ValidationGroup="CityMappingPop"></asp:RequiredFieldValidator></label>
                                                            <div class="col-sm-9">
                                                                <div class="col-sm-10">
                                                                    <asp:TextBox ID="txtSystemProductName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2">&nbsp;</div>
                                                            </div>
                                                        </div>--%>
                                                        <div class="form-group form-inline">
                                                            <label class="control-label col-sm-3" for="txtSystemProductCode">Code</label>
                                                            <div class="col-sm-9">
                                                                <div class="col-sm-10">
                                                                    <asp:TextBox ID="txtSystemProductCode" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2">&nbsp;</div>
                                                            </div>
                                                        </div>    
                                                        <div class="form-group form-inline">
                                                            <div class="col-sm-12">
                                                                    &nbsp;
                                                            </div>
                                                        </div>    
                                                        <div class="form-group form-inline">
                                                            <label class="control-label col-sm-3" for="txtSystemProductCode">Address</label>
                                                            <div class="col-sm-9">
                                                                <div class="col-sm-10">
                                                                    <strong></string><asp:Label ID="lblSystemProductAddress" runat="server" Text=""></asp:Label></strong>
                                                                </div>
                                                                <div class="col-sm-2">&nbsp;</div>
                                                            </div>
                                                        </div>      
                                                        <div class="form-group form-inline">
                                                            <div class="col-sm-12">
                                                                    &nbsp;
                                                            </div>
                                                        </div>                                                               
                                                        <div class="form-group form-inline">
                                                            <label class="control-label col-sm-3" for="lblSystemLocation">Location</label>
                                                            <div class="col-sm-9">
                                                                <div class="col-sm-10">
                                                                    <asp:Label ID="lblSystemLocation" runat="server" Text=""></asp:Label>
                                                                </div>
                                                                <div class="col-sm-2">&nbsp;</div>
                                                            </div>
                                                        </div>      
                                                        <div class="form-group form-inline">
                                                            <div class="col-sm-12">
                                                                    &nbsp;
                                                            </div>
                                                        </div>                                                      
                                                        <div class="form-group form-inline">
                                                            <label class="control-label col-sm-3" for="lblSystemTelephone">Telephone</label>
                                                            <div class="col-sm-9">
                                                                <div class="col-sm-10">
                                                                    <asp:Label ID="lblSystemTelephone" runat="server" Text=""></asp:Label>
                                                                </div>
                                                                <div class="col-sm-2">&nbsp;</div>
                                                            </div>
                                                        </div>    
                                                        <div class="form-group form-inline">
                                                            <div class="col-sm-12">
                                                                    &nbsp;
                                                            </div>
                                                        </div>                                                   
                                                        <div class="form-group form-inline">
                                                            <label class="control-label col-sm-3" for="lblSystemLatitude">Lat-Long</label>
                                                            <div class="col-sm-9">
                                                                <div class="col-sm-10">
                                                                    <asp:Label ID="lblSystemLatitude" runat="server" Text=""></asp:Label>
                                                                    &nbsp;/&nbsp;
                                                                    <asp:Label ID="lblSystemLongitude" runat="server" Text=""></asp:Label>
                                                                </div>
                                                                <div class="col-sm-2">&nbsp;</div>
                                                            </div>
                                                        </div>       
                                                        <div class="form-group form-inline">
                                                            <div class="col-sm-12">
                                                                    &nbsp;
                                                            </div>
                                                        </div> 
                                                        <div class="form-group" style="text-align: right">
                                                            <div class="col-sm-12">
                                                                <asp:Button ID="btnAddProduct" runat="server" CssClass="btn btn-primary btn-sm" Text="Add Hotel" CommandName="OpenAddProduct" CausesValidation="true" ValidationGroup="AddCity" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">Status</div>
                                                    <div class="panel-body">
                                                        <div class="form-group">
                                                            <label for="MatchedBy">
                                                                Matched By&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </label>
                                                            <asp:Label ID="lblpMatchedBy" runat="server" Text=""></asp:Label>&nbsp;-&nbsp;
                                                                <asp:Label ID="lblpMatchedByString" runat="server" Text=""></asp:Label>
                                                            <div class="form-group">&nbsp;</div>
                                                            <label for="ddlStatus">
                                                                Status
                                                                        <asp:RequiredFieldValidator ID="vddlStatus" runat="server" ErrorMessage="*" ControlToValidate="ddlStatus" InitialValue="0" CssClass="text-danger" ValidationGroup="CityMappingPop"></asp:RequiredFieldValidator>
                                                            </label>
                                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control col-lg-3" AppendDataBoundItems="true" onchange="ddlStatusChanged(this);">
                                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <div class="form-group">&nbsp;</div>
                                                            <div class="form-group">
                                                                <label for="txtSystemRemark">Remark</label>
                                                                <asp:TextBox ID="txtSystemRemark" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group">
                                                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm" Text="Save" CommandName="Add" ValidationGroup="CityMappingPop" CausesValidation="true" />
                                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-sm" Text="Cancel" CommandName="Cancel" data-dismiss="modal" CausesValidation="false" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </EditItemTemplate>
                            </asp:FormView>
                            <div class="row" runat="server" id="dvAddProduct">
                                <div class="col-lg-4">
                                    <uc2:AddNew runat="server" ID="ucAddNew" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <div id="dvMsg" runat="server" style="display: none;"></div>
                                    </div>
                                    <div runat="server" id="dvMatchingRecords" class="panel panel-default">
                                        <div class="panel-heading">
                                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group form-inline">
                                                <div class="input-group">
                                                    <label class="input-group-addon" for="ddlProductBasedPageSize">Page Size</label>
                                                    <asp:DropDownList ID="ddlMatchingPageSize" runat="server" CssClass="form-control col-lg-3" AutoPostBack="true" OnSelectedIndexChanged="ddlMatchingPageSize_SelectedIndexChanged">
                                                        <asp:ListItem Text="5" Value="5" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="input-group">&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                                <div class="input-group">
                                                    <label class="input-group-addon" for="ddlMatchingStatus">Status</label>
                                                    <asp:DropDownList ID="ddlMatchingStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlMatchingStatus_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="input-group pull-right">
                                                    <asp:Button ID="btnMatchedMapSelected" Visible="false" runat="server" CssClass="btn btn-primary btn-sm" Text="Map Selected" CommandName="MapSelected" OnClick="btnMatchedMapSelected_Click" CausesValidation="false" />
                                                    <asp:Button ID="btnMatchedMapAll" Visible="false" runat="server" CssClass="btn btn-primary btn-sm" Text="Map All" CommandName="MapAll" OnClick="btnMatchedMapAll_Click" CausesValidation="false" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:GridView ID="grdMatchingProducts" runat="server" AllowPaging="True" AllowCustomPaging="true" AutoGenerateColumns="False"
                                                    EmptyDataText="No Static Updates" CssClass="table table-hover table-striped" DataKeyNames="Accommodation_ProductMapping_Id,Supplier_Id,Accommodation_Id"
                                                    OnSelectedIndexChanged="grdMatchingProducts_SelectedIndexChanged" OnPageIndexChanging="grdMatchingProducts_PageIndexChanging" OnRowCommand="grdMatchingProducts_RowCommand"
                                                    OnRowDataBound="grdMatchingProducts_RowDataBound" OnDataBound="grdMatchingProducts_DataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                                                        <asp:BoundField DataField="SupplierProductReference" HeaderText="Product Code" />
                                                        <asp:BoundField DataField="ProductName" HeaderText="ProductName" />
                                                        <asp:BoundField DataField="FullAddress" HeaderText="Address" />
                                                        <asp:BoundField DataField="PostCode" HeaderText="Post Code" />
                                                        <asp:BoundField DataField="CityName" HeaderText="City Name" />
                                                        <asp:BoundField DataField="CountryName" HeaderText="Country Name" />
                                                        <asp:BoundField DataField="TelephoneNumber" HeaderText="Tel" />
                                                        <asp:BoundField DataField="Latitude" HeaderText="Latitude" />
                                                        <asp:BoundField DataField="Longitude" HeaderText="Longitude" />
                                                        <asp:TemplateField ShowHeader="true" HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status")%>'></asp:Label>
                                                                <asp:Label ID="lblMatchedBy" runat="server" Text='<%# Convert.ToString(Eval("Status")) == "REVIEW" ? (string.IsNullOrWhiteSpace(Convert.ToString(Eval("MatchedBy"))) ? "" :  " (" + Eval("MatchedBy") + ")") : "" %>' ToolTip='<%# Bind("MatchedByString")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="false">
                                                            <ItemTemplate>
                                                                <%--  <asp:CheckBox ID="chkSelect" runat="server" CausesValidation="false" CommandName="Select" AutoPostBack="false"
                                                                    Enabled="true" HeaderText="Select" />--%>
                                                                <input type="checkbox" runat="server" id="chkSelect" onclick="MatchedSelect(this);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pagination-ys" HorizontalAlign="Left" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" runat="server" onserverclick="btnSearch_Click">Close</button>
                </div>
            </div>
        </div>
    </div>
</div>
<script type='text/javascript'>
    $('.next').click(function () {

        var nextId = $(this).parents('.tab-pane').next().attr("id");
        $('[href=#' + nextId + ']').tab('show');

    })

    $('.first').click(function () {

        $('#myWizard a:first').tab('show')

    })
</script>
