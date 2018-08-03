<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="searchAccoMapping.ascx.cs" Inherits="TLGX_Consumer.controls.staticdata.AccoMap" %>
<%@ Register Src="~/controls/staticdata/bulkHotelMapping.ascx" TagPrefix="uc1" TagName="bulkHotelMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/controls/staticdata/UpdateSupplierProductMapping.ascx" TagPrefix="uc1" TagName="UpdateSupplierProductMapping" %>

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
        max-height: 250px;
        overflow-y: scroll;
    }

    .MouseOverCell {
        background-color: rgb(22, 53,114);
        color: rgb(255, 255, 255);
        font-weight: bold;
        cursor: pointer;
    }

    .MouseOverRow {
        background-color: #d1e7fa !important;
    }
</style>
<script type="text/javascript">


    // Highlight the cell when mouse over on it  
    function onMouseOver(rowIndex) {
        var gv = document.getElementById("MainContent_searchAccoMapping_grdAccoMaps");
        var rowElement = gv.rows[rowIndex];
        $(rowElement).addClass("MouseOverRow");
        //var isChecked = $(rowElement.cells[15]).find('input');
        //isChecked.change(function () {
        //    if (isChecked.is(':checked')) {
        //        $(rowElement).addClass("alert alert-success");
        //    }
        //    else {
        //        $(rowElement).removeClass("alert alert-success");
        //    }
        //});


        $(rowElement.cells[2]).on('mouseover', function () {
            rowElement.cells[2].classList.add("MouseOverCell");
            rowElement.cells[8].classList.add("MouseOverCell");
            //click event
            $(this).unbind('click').click(function (e) {
                //e.preventDefault();
                var txtStatus = $(rowElement.cells[13]).text().trim();
                if (txtStatus.includes("REVIEW")) {
                    debugger;
                    var isChkChecked = $(rowElement.cells[15]).find('input');
                    if (isChkChecked.is(':checked')) {
                        $(rowElement.cells[15]).find('input').attr("checked", false);
                        //$(rowElement).removeClass("alert alert-success");
                    }
                    else {
                        $(rowElement.cells[15]).find('input').attr("checked", true);
                        //$(rowElement).addClass("alert alert-success");
                    }
                }
            });

        });
        $(rowElement.cells[8]).on('mouseover', function () {
            rowElement.cells[2].classList.add("MouseOverCell");
            rowElement.cells[8].classList.add("MouseOverCell");
        });
        $(rowElement.cells[4]).on('mouseover', function () {
            rowElement.cells[4].classList.add("MouseOverCell");
            rowElement.cells[11].classList.add("MouseOverCell");
        });
        $(rowElement.cells[11]).on('mouseover', function () {
            rowElement.cells[4].classList.add("MouseOverCell");
            rowElement.cells[11].classList.add("MouseOverCell");
        });
        $(rowElement.cells[15]).on('mouseover', function () {
            rowElement.cells[2].classList.add("MouseOverCell");
            rowElement.cells[8].classList.add("MouseOverCell");
            rowElement.cells[4].classList.add("MouseOverCell");
            rowElement.cells[11].classList.add("MouseOverCell");
        });
    }

    function onMouseOut(rowIndex) {
        var gv = document.getElementById("MainContent_searchAccoMapping_grdAccoMaps");
        var rowElement = gv.rows[rowIndex];
        $(rowElement).removeClass("MouseOverRow");
        rowElement.cells[2].classList.remove("MouseOverCell");
        rowElement.cells[8].classList.remove("MouseOverCell");
        rowElement.cells[4].classList.remove("MouseOverCell");
        rowElement.cells[11].classList.remove("MouseOverCell");
        //Remove click event
        $(rowElement.cells[2]).removeAttr("onclick");
    }
    //End
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


    //function checkLen(val) {
    //    var rfvtxtSearchSystemProduct = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_rfvtxtSearchSystemProduct");
    //    var hdnSelSystemProduct_Id = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_hdnSelSystemProduct_Id");
    //    if (val.length == 0) {
    //        val.text = null;
    //        hdnSelSystemProduct_Id.value = null;
    //        ValidatorEnable(rfvtxtSearchSystemProduct, true);
    //        document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_btnAddProduct").style.display = "block";
    //    }
    //    else {
    //        ValidatorEnable(rfvtxtSearchSystemProduct, false);
    //        document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_btnAddProduct").style.display = "none";
    //    }
    //}

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        callajax();
    });
    function showLoadingImage() {
        $('#loading').show();
    }
    function hideLoadingImage() {
        $('#loading').hide();
    }
    //function callajax() {
    //    $("[id*=txtSearch]").autocomplete({
    //        source: function (request, response) {
    //            $.ajax({
    //                url: '../../Service/HotelNameAutoComplete.ashx',
    //                dataType: "json",
    //                data: {
    //                    term: request.term,
    //                    country: $("[id*=ddlCountryName]").children("option:selected").text(),
    //                    city: $("[id*=ddlCity]").children("option:selected").text(),
    //                    chain: $("[id*=ddlChain]").children("option:selected").text(),
    //                    brand: $("[id*=ddlBrand]").children("option:selected").text()
    //                },
    //                success: function (data) {
    //                    response(data);
    //                }
    //            });
    //        },
    //        min_length: 3,
    //        delay: 300
    //    });
    //    $("[id*=txtSystemProductName]").autocomplete({
    //        source: function (request, response) {
    //            $.ajax({
    //                url: '../../Service/HotelNameAutoComplete.ashx',
    //                dataType: "json",
    //                data: {
    //                    term: request.term,
    //                    country: $("[id*=ddlSystemCountryName]").children("option:selected").text(),
    //                    city: $("[id*=ddlSystemCityName]").children("option:selected").text()
    //                },
    //                success: function (data) {
    //                    response(data);
    //                }
    //            });
    //        },
    //        minLength: 3,
    //        delay: 300
    //    });

    //    $("[id*=txtSupCountry]").autocomplete({
    //        source: function (request, response) {
    //            $.ajax({
    //                url: '../../Service/CountryAutoComplete.ashx',
    //                dataType: "json",
    //                data: {
    //                    term: request.term
    //                },
    //                success: function (data) {
    //                    response(data);
    //                }
    //            });
    //        },
    //        min_length: 3,
    //        delay: 300
    //    });

    //    $("[id*=txtSupCity]").autocomplete({
    //        source: function (request, response) {
    //            $.ajax({
    //                url: '../../Service/CityAutoComplete.ashx',
    //                dataType: "json",
    //                data: {
    //                    term: request.term,
    //                    country: $("[id*=txtSupCountry]").val()
    //                },
    //                success: function (data) {
    //                    response(data);
    //                }
    //            });
    //        },
    //        min_length: 3,
    //        delay: 300
    //    });
    //    var moCityMapping = document.getElementById("moCityMapping");
    //    var hdnSystemProduct_Id = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_hdnSystemProduct_Id");
    //    var hdnSystemProduct = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_hdnSystemProduct");
    //    var hdnSelSystemProduct_Id = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_hdnSelSystemProduct_Id");

    //    $("[id*=txtSearchSystemProduct]").autocomplete({
    //        source: function (request, response) {
    //            if (request.term.length > 2) {
    //                showLoadingImage();
    //                $.ajax({
    //                    url: '../../Service/HotelMappingAutoComplete.ashx',
    //                    dataType: "json",
    //                    data: {
    //                        term: request.term,
    //                        country: $("[id*=ddlSystemCountryName]").children("option:selected").text(),
    //                        country_id: $("[id*=ddlSystemCountryName]").children("option:selected").val(),
    //                        state: $("[id*=ddlSystemStateName]").children("option:selected").text(),
    //                        source: 'autocomplete'
    //                    },
    //                    success: function (result) {
    //                        if (result != null && result.length > 0) {
    //                            hdnSystemProduct_Id.value = "";
    //                            hdnSystemProduct.value = "";
    //                            hdnSelSystemProduct_Id.value = "";
    //                            var data = [];
    //                            for (var i = 0; i < result.length; i++) {
    //                                if (hdnSystemProduct_Id != null && result[i].Accommodation_Id != null) {
    //                                    hdnSystemProduct_Id.value = hdnSystemProduct_Id.value + result[i].Accommodation_Id + "`";
    //                                }
    //                                if (result[i].HotelName != null) {
    //                                    var hotelname = result[i].HotelName;
    //                                    if (result[i].City != null) {
    //                                        hotelname = hotelname + ", " + result[i].City;
    //                                    }
    //                                    if (result[i].State != null) {
    //                                        hotelname = hotelname + ", " + result[i].State;
    //                                    }
    //                                    if (result[i].StateCode != null) {
    //                                        hotelname = hotelname + " (" + result[i].StateCode.substring(3, result[i].StateCode.length) + ")";
    //                                    }
    //                                    if (result[i].Country != null) {
    //                                        hotelname = hotelname + ", " + result[i].Country;
    //                                    }
    //                                    hdnSystemProduct.value = hdnSystemProduct.value + hotelname + "`";
    //                                    data.push(hotelname);
    //                                }
    //                            }
    //                            hideLoadingImage();
    //                            response(data);
    //                        }
    //                        else {
    //                            var data = [];
    //                            var NoDataFound = "No Data Found";
    //                            data.push(NoDataFound);
    //                            response(data);
    //                        }
    //                    }
    //                });
    //            }
    //            else {
    //                hideLoadingImage();
    //            }
    //        },
    //        min_length: 3,
    //        delay: 300
    //    });

    //    $("[id*=txtSearchSystemProduct]").on('autocompleteselect', function (e, ui) {

    //        if (ui.item.value != "No Data Found")
    //            callSystemProductNamechange(ui.item.value);
    //        else {
    //            ui.item.value = null;
    //            document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_txtSearchSystemProduct").value = "";
    //        }

    //    });

    //}
    //function callSystemProductNamechange(selection) {

    //    var vrtxtSearch = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_txtSearch");
    //    var hdnSystemProduct_Id = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_hdnSystemProduct_Id");
    //    var hdnSystemProduct = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_hdnSystemProduct");
    //    var hdnSelSystemProduct_Id = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_hdnSelSystemProduct_Id");
    //    var vrbtnAddProduct = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_btnAddProduct");
    //    var selId = "";
    //    if (selection != null) {
    //        vrbtnAddProduct.style.display = "none";
    //        if (selection.trim() != "") {
    //            var brkvrhdnSystemProduct = hdnSystemProduct.value.split('`');
    //            var brkvrhdnSystemProduct_Id = hdnSystemProduct_Id.value.split('`');
    //            var idx = brkvrhdnSystemProduct.indexOf(selection);
    //            if (idx != null && idx != (-1)) {
    //                selId = brkvrhdnSystemProduct_Id[idx];
    //            }
    //        }
    //    }

    //    var ddlSystemProductName = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_ddlSystemProductName");
    //    var listItems = '';
    //    if (selId != null) {
    //        ddlSystemProductName.innerHTML = "";
    //        listItems += '<option selected="selected" value="' + selId + '">' + brkvrhdnSystemProduct[0].split(';')[0] + '</option>';
    //        $('#MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_ddlSystemProductName').append(listItems);
    //        if (vrbtnAddProduct != null)
    //            vrbtnAddProduct.style.display = "none";

    //    }


    //    var ddlSystemCityName = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_ddlSystemCityName");
    //    var ddlSystemStateName = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_ddlSystemStateName");
    //    var txtSystemProductCode = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_txtSystemProductCode");
    //    var lblSystemProductAddress = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_lblSystemProductAddress");
    //    var lblSystemLocation = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_lblSystemLocation");
    //    var lblSystemTelephone = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_lblSystemTelephone");
    //    var lblSystemLatitude = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_lblSystemLatitude");
    //    var lblSystemLongitude = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_lblSystemLongitude");
    //    var lblSystemProductType = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_lblSystemProductType");


    //    var hdnIsJavascriptChagedValueddlSystemStateName = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_hdnIsJavascriptChagedValueddlSystemStateName");

    //    var hdnIsJavascriptChagedValueddlSystemCityName = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_hdnIsJavascriptChagedValueddlSystemCityName");


    //    var txtSearchSystemProduct = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_txtSearchSystemProduct")

    //    if (selId != "") {
    //        if (vrbtnAddProduct != null)
    //            vrbtnAddProduct.style.display = "none";
    //        hdnSelSystemProduct_Id.value = selId;
    //        $.ajax({
    //            url: '../../Service/HotelMappingAutoComplete.ashx',
    //            dataType: "json",
    //            data: {
    //                accoid: selId,
    //                source: 'details'
    //            },
    //            responseType: "json",
    //            success: function (result) {
    //                if (result != null) {
    //                    txtSearchSystemProduct.value = result[0].HotelName;
    //                    txtSystemProductCode.value = result[0].CompanyHotelID;
    //                    lblSystemProductAddress.innerHTML = result[0].FullAddress;
    //                    lblSystemLocation.innerHTML = result[0].Location;
    //                    lblSystemTelephone.innerHTML = result[0].Telephone_Tx;
    //                    lblSystemLatitude.innerHTML = result[0].Latitude;
    //                    lblSystemLongitude.innerHTML = result[0].Longitude;
    //                    lblSystemProductType.innerHTML = result[0].SystemProductType;
    //                    if (result[0].City != null) {

    //                        for (var i = 0; i < ddlSystemCityName.options.length; i++) {
    //                            if (ddlSystemCityName.options[i].text == result[0].City) {
    //                                ddlSystemCityName.options[i].selected = true;
    //                                hdnIsJavascriptChagedValueddlSystemCityName.value = "true";
    //                                break;
    //                            }
    //                        }
    //                    }
    //                    if (ddlSystemStateName.options[ddlSystemStateName.selectedIndex].value == "0") {
    //                        if (result[0].State_Name != null) {
    //                            for (var i = 0; i < ddlSystemStateName.options.length; i++) {
    //                                if (ddlSystemStateName.options[i].text == result[0].State_Name) {
    //                                    ddlSystemStateName.options[i].selected = true;
    //                                    hdnIsJavascriptChagedValueddlSystemStateName.value = "true";
    //                                    break;
    //                                }
    //                            }
    //                        }
    //                    }


    //                }
    //            },
    //            failure: function () {
    //            }
    //        });
    //    }
    //    else {
    //        if (vrbtnAddProduct != null)
    //            vrbtnAddProduct.style.display = "none";
    //    }

    //}
    function SelectedRow(element) {
        var ddlStatus = $('#MainContent_searchAccoMapping_ddlMappingStatus option:selected').html();
        if (ddlStatus == "REVIEW") {
            element.parentNode.parentNode.nextSibling.childNodes[16].lastElementChild.focus();
        }
        else if (ddlStatus == "UNMAPPED") {
            element.parentNode.parentNode.nextSibling.childNodes[12].lastElementChild.focus();
        }
    }
    //function MatchedSelect(elem) {
    //    var element = elem.parentNode.parentNode.nextSibling.childNodes[13];
    //    if (typeof element !== 'undefined') {
    //        if (element.lastElementChild != null)
    //            element.lastElementChild.focus();
    //    }
    //}
    function fillDropDown(record, onClick) {
        if (onClick) {
            //Getting Dropdown
            var currentRow = $(record).parent().parent();
            //var countryname = currentRow.find("td:eq(8)").text().trim();
            //var cityname = currentRow.find("td:eq(10)").text().trim();
            var countryname = null;
            var cityname = null;
            // var hdnAccoVal = currentRow.find("td:eq(16)");

            var countrynameddl = document.getElementById("MainContent_searchAccoMapping_ddlCountry");
            var citynameddl = document.getElementById("MainContent_searchAccoMapping_ddlSupplierCity");
            if (countrynameddl != null)
                countryname = countrynameddl.options[countrynameddl.selectedIndex].text;
            if (citynameddl != null)
                cityname = citynameddl.options[citynameddl.selectedIndex].text;


            var AccoDDL = currentRow.find("td:eq(11)").find('select');
            var selectedText = AccoDDL.find("option:selected").text();
            var selectedOption = AccoDDL.find("option");
            var selectedVal = AccoDDL.val();
            //if (hdnAccoVal != null)
            //    hdnAccoVal.val(selectedVal);
            if ((countryname != null && countryname != "") || (cityname != null && cityname != "")) {
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
                            // AccoDDL.find("option:not(:first)").remove();
                            AccoDDL.find('option').remove();
                            AccoDDL.append("<option value='0'>Select</option>");
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
        if (!onClick) {
            var currentRow = $(record).parent().parent();
            var AccoDDL = currentRow.find("td:eq(11)").find('select');
            var selectedText = AccoDDL.find("option:selected").text();
            var selectedVal = AccoDDL.val();
            // AccoDDL.find("option:not(:first)").remove();
            AccoDDL.find('option').remove();
            AccoDDL.append("<option value='0'>Select</option>");
            var listItems = "<option selected = 'selected' value='" + selectedVal + "'>" + selectedText + "</option>";
            AccoDDL.append(listItems);
            var acco_id = record.parentNode.parentNode.childNodes[17].firstElementChild;
            acco_id.value = selectedVal;
        }
    }
    //function ddlStatusChanged(ddl) {
    //    var ddlStatus = $('#MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_ddlStatus option:selected').html();
    //    var mySystemCountryName = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_vddlSystemCountryName");
    //    var mySystemCityName = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_vddlSystemCityName");
    //    // var myProductName = document.getElementById("MainContent_searchAccoMapping_UpdateSupplierProductMapping_frmEditProductMap_vddlSystemProductName");
    //    //var myVal = $('#vddlSystemCountryName').val();
    //    if (ddlStatus == 'DELETE') {
    //        ValidatorEnable(mySystemCountryName, false);
    //        if (mySystemCityName != null)
    //            ValidatorEnable(mySystemCityName, false);
    //        //  ValidatorEnable(myProductName, false);
    //    }
    //    else {
    //        ValidatorEnable(mySystemCountryName, true);
    //        if (mySystemCityName != null)
    //            ValidatorEnable(mySystemCityName, true);
    //        // ValidatorEnable(myProductName, true);
    //    }
    //}
</script>

<div id="myWizard">

    <div class="navbar">
        <div class="navbar-inner">
            <ul class="nav nav-tabs">
                <li class="active"><a href="#panSupplierSearch" data-toggle="tab"><strong>Search by Supplier</strong></a></li>
                <li><a href="#panProductSearch" data-toggle="tab"><strong>Search by Product</strong></a></li>
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
                                                <label class="control-label col-sm-4" for="txtSuppName">Product Type</label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
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
                                                <label class="control-label col-sm-4" for="txtSuppName">Supplier Product Name</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtSuppProduct" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="control-label col-sm-4" for="txtSuppProdCode">Supplier Product Code</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtSuppProdCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                             <div class="form-group row">
                                                <label class="control-label col-sm-4" for="ddlAccoPriority">
                                                    Accomodation Priority
                                                </label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlAccoPriority" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    </asp:DropDownList>
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
                                                    <asp:HiddenField ID="hdnIsAnyChanges" runat="server" Value="false"></asp:HiddenField>
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
                                    <asp:Button ID="btnMapSelected" runat="server" CssClass="btn btn-primary btn-sm" Text="Map Selected" OnClientClick="javascript:return confirm('Are you really sure you want to do this?');" OnClick="btnMapSelected_Click" />
                                    <asp:Button ID="btnMapAll" runat="server" CssClass="btn btn-primary btn-sm" Text="Map All" OnClientClick="javascript:return confirm('Are you really sure you want to do this?');" OnClick="btnMapAll_Click" />
                                </div>
                            </div>
                            <div id="collapseSearchResult" class="panel-collapse collapse in">
                                <div class="panel-body">
                                    <div id="divMsgForMapping" runat="server" style="display: none;"></div>
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
                                            <asp:BoundField DataField="ProductType" HeaderText="ProductType" />
                                            <asp:BoundField DataField="FullAddress" HeaderText="Address" />
                                            <asp:BoundField DataField="TelephoneNUmber" HeaderText="Tel" />
                                            <asp:TemplateField HeaderText="Country">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSupCoutry" runat="server" Text='<%# string.IsNullOrWhiteSpace(Convert.ToString(Eval("CountryCode"))) ? Eval("CountryName") : Eval("CountryName") + " (" + Eval("CountryCode") + " )" %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--   <asp:BoundField DataField="CountryCode" HeaderText="Country Code" />--%>
                                            <%--<asp:BoundField DataField="CountryName" HeaderText="Country Name" />--%>
                                            <%--   <asp:BoundField DataField="CityCode" HeaderText="City Code" />--%>
                                            <asp:TemplateField HeaderText="City">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSupCity" runat="server" Text='<%# string.IsNullOrWhiteSpace(Convert.ToString(Eval("CityCode"))) ? Eval("CityName") : Eval("CityName") + " (" + Eval("CityCode") + " )" %>'></asp:Label>
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
                                                <label class="control-label col-sm-4" for="ddlSearchByProdAccoPriority">
                                                    Accomodation Priority
                                                </label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlAccoPrioritySearchByProd" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                   </asp:DropDownList>
                                                </div>
                                            </div>


                                        </div>

                                        <div class="col-lg-4">
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
                                                <label class="control-label col-sm-4" for="ddlProductMappingStatus">
                                                    Mapping Status
                                                </label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlProductMappingStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true">
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
                                                <div class="col-sm-12">
                                                    <asp:Button ID="Button3" runat="server" CssClass="btn btn-primary btn-sm" Text="Add Mapping by Product" CausesValidation="false" Visible="false" />
                                                    <!-- wire me up to go to /addProductMapping add straight to Product Search -->
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
                                                <label class="control-label col-sm-6" for="ddlProductBasedPageSize">Page Size</label>
                                                <div class="col-sm-6">
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
                            
                            <uc1:UpdateSupplierProductMapping runat="server" id="UpdateSupplierProductMapping" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true" runat="server" onserverclick="GridRefersh">Close</button>
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

        $('#myWizard a:first').tab('show');

    })
</script>
