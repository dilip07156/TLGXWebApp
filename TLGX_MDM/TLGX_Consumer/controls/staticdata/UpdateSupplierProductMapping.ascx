<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdateSupplierProductMapping.ascx.cs" Inherits="TLGX_Consumer.controls.staticdata.UpdateSupplierProductMapping" %>

<%@ Register Src="~/controls/hotel/AddNew.ascx" TagPrefix="uc2" TagName="AddNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<script src="../../Scripts/JqueryUI/jquery-ui.js"></script>
<link href="../../Scripts/JqueryUI/jquery-ui.css" rel="stylesheet" />--%>
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
</style>
<script type="text/javascript">

    $(document).ready(function () {
        callajax();
    });

    function showLoadingImage() {
        $('#loading').show();
    }
    function hideLoadingImage() {
        $('#loading').hide();
    }

    function checkLen(val) {
        var rfvtxtSearchSystemProduct = document.getElementById("rfvtxtSearchSystemProduct");
        var hdnSelSystemProduct_Id = document.getElementById("hdnSelSystemProduct_Id");
        if (val.length == 0) {
            val.text = null;
            hdnSelSystemProduct_Id.value = null;
            ValidatorEnable(rfvtxtSearchSystemProduct, true);
            document.getElementById("btnAddProduct").style.display = "block";
        }
        else {
            ValidatorEnable(rfvtxtSearchSystemProduct, false);
            document.getElementById("btnAddProduct").style.display = "none";
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
            minLength: 3,
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
        //var moCityMapping = document.getElementById("moCityMapping");
        var hdnSystemProduct_Id = document.getElementById("hdnSystemProduct_Id");
        var hdnSystemProduct = document.getElementById("hdnSystemProduct");
        var hdnSelSystemProduct_Id = document.getElementById("hdnSelSystemProduct_Id");
        $("[id*=txtSearchSystemProduct]").autocomplete({
            source: function (request, response) {
                if (request.term.length > 2) {
                    showLoadingImage();
                    $.ajax({
                        url: '../../../Service/HotelMappingAutoComplete.ashx',
                        dataType: "json",
                        data: {
                            term: request.term,
                            country: $("[id*=ddlSystemCountryName]").children("option:selected").text(),
                            country_id: $("[id*=ddlSystemCountryName]").children("option:selected").val(),
                            state: $("[id*=ddlSystemStateName]").children("option:selected").text(),
                            source: 'autocomplete'
                        },
                        success: function (result) {
                            if (result != null && result.length > 0) {
                                hdnSystemProduct_Id.value = "";
                                hdnSystemProduct.value = "";
                                hdnSelSystemProduct_Id.value = "";
                                var data = [];
                                for (var i = 0; i < result.length; i++) {
                                    if (hdnSystemProduct_Id != null && result[i].Accommodation_Id != null) {
                                        hdnSystemProduct_Id.value = hdnSystemProduct_Id.value + result[i].Accommodation_Id + "`";
                                    }
                                    if (result[i].HotelName != null) {
                                        var hotelname = result[i].HotelName;
                                        if (result[i].City != null) {
                                            hotelname = hotelname + ", " + result[i].City;
                                        }
                                        if (result[i].State != null) {
                                            hotelname = hotelname + ", " + result[i].State;
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
                                hideLoadingImage();
                                response(data);
                            }
                            else {
                                var data = [];
                                var NoDataFound = "No Data Found";
                                data.push(NoDataFound);
                                response(data);
                            }
                        }
                    });
                }
                else {
                    hideLoadingImage();
                }
            },
            min_length: 3,
            delay: 300
        });

        $("[id*=txtSearchSystemProduct]").on('autocompleteselect', function (e, ui) {

            if (ui.item.value != "No Data Found")
                callSystemProductNamechange(ui.item.value);
            else {
                ui.item.value = null;
                document.getElementById("txtSearchSystemProduct").value = "";
            }

        });

    }
    function callSystemProductNamechange(selection) {
        var hdnSystemProduct_Id = document.getElementById("hdnSystemProduct_Id");
        var hdnSystemProduct = document.getElementById("hdnSystemProduct");
        var hdnSelSystemProduct_Id = document.getElementById("hdnSelSystemProduct_Id");
        var vrbtnAddProduct = document.getElementById("btnAddProduct");
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

        var ddlSystemProductName = document.getElementById("ddlSystemProductName");
        var listItems = '';
        if (selId != null) {
            ddlSystemProductName.innerHTML = "";
            listItems += '<option selected="selected" value="' + selId + '">' + brkvrhdnSystemProduct[0].split(';')[0] + '</option>';
            $('#ddlSystemProductName').append(listItems);
            if (vrbtnAddProduct != null)
                vrbtnAddProduct.style.display = "none";

        }


        var ddlSystemCityName = document.getElementById("ddlSystemCityName");
        var ddlSystemStateName = document.getElementById("ddlSystemStateName");
        var txtSystemProductCode = document.getElementById("txtSystemProductCode");
        var lblSystemProductAddress = document.getElementById("lblSystemProductAddress");
        var lblSystemLocation = document.getElementById("lblSystemLocation");
        var lblSystemTelephone = document.getElementById("lblSystemTelephone");
        var lblSystemLatitude = document.getElementById("lblSystemLatitude");
        var lblSystemLongitude = document.getElementById("lblSystemLongitude");
        var lblSystemProductType = document.getElementById("lblSystemProductType");


        var hdnIsJavascriptChagedValueddlSystemStateName = document.getElementById("hdnIsJavascriptChagedValueddlSystemStateName");

        var hdnIsJavascriptChagedValueddlSystemCityName = document.getElementById("hdnIsJavascriptChagedValueddlSystemCityName");


        var txtSearchSystemProduct = document.getElementById("txtSearchSystemProduct")

        if (selId != "") {
            if (vrbtnAddProduct != null)
                vrbtnAddProduct.style.display = "none";
            hdnSelSystemProduct_Id.value = selId;
            $.ajax({
                url: '../../../Service/HotelMappingAutoComplete.ashx',
                dataType: "json",
                data: {
                    accoid: selId,
                    source: 'details'
                },
                responseType: "json",
                success: function (result) {
                    if (result != null) {
                        txtSearchSystemProduct.value = result[0].HotelName;
                        txtSystemProductCode.value = result[0].CompanyHotelID;
                        lblSystemProductAddress.innerHTML = result[0].FullAddress;
                        lblSystemLocation.innerHTML = result[0].Location;
                        lblSystemTelephone.innerHTML = result[0].Telephone_Tx;
                        lblSystemLatitude.innerHTML = result[0].Latitude;
                        lblSystemLongitude.innerHTML = result[0].Longitude;
                        lblSystemProductType.innerHTML = result[0].SystemProductType;
                        if (result[0].City != null) {

                            for (var i = 0; i < ddlSystemCityName.options.length; i++) {
                                if (ddlSystemCityName.options[i].text == result[0].City) {
                                    ddlSystemCityName.options[i].selected = true;
                                    hdnIsJavascriptChagedValueddlSystemCityName.value = "true";
                                    break;
                                }
                            }
                        }
                        if (ddlSystemStateName.options[ddlSystemStateName.selectedIndex].value == "0") {
                            if (result[0].State_Name != null) {
                                for (var i = 0; i < ddlSystemStateName.options.length; i++) {
                                    if (ddlSystemStateName.options[i].text == result[0].State_Name) {
                                        ddlSystemStateName.options[i].selected = true;
                                        hdnIsJavascriptChagedValueddlSystemStateName.value = "true";
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
    
    function MatchedSelect(elem) {
        var element = elem.parentNode.parentNode.nextSibling.childNodes[13];
        if (typeof element !== 'undefined') {
            if (element.lastElementChild != null)
                element.lastElementChild.focus();
        }
    }
    
   
    function ddlStatusChanged(ddl) {
        var ddlStatus = $('#ddlStatus option:selected').html();
        var mySystemCountryName = document.getElementById("vddlSystemCountryName");
        var mySystemCityName = document.getElementById("vddlSystemCityName");
        // var myProductName = document.getElementById("vddlSystemProductName");
        //var myVal = $('#vddlSystemCountryName').val();
        if (ddlStatus == 'DELETE') {
            ValidatorEnable(mySystemCountryName, false);
            if (mySystemCityName != null)
                ValidatorEnable(mySystemCityName, false);
        }
        else {
            ValidatorEnable(mySystemCountryName, true);
            if (mySystemCityName != null)
                ValidatorEnable(mySystemCityName, true);
        }
    }
</script>
<div id="UpdateSupProdmapping">
    <asp:HiddenField ID="hdnIsAnyChanges" runat="server" Value="false"></asp:HiddenField>
     <asp:HiddenField ID="hdnFlag" runat="server" Value="" EnableViewState="false" />
    <asp:HiddenField ID="hdmApmId" runat="server" Value="" />
    <asp:UpdatePanel ID="UpdCityMapModal" runat="server">
        <ContentTemplate>
            <%--<asp:HiddenField ID="hdnIsAnyChanges" runat="server" Value="false"></asp:HiddenField>--%>
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
                                                    <td><strong>ProductType</strong></td>
                                                    <td>
                                                        <asp:Label ID="lblProductType" runat="server" Text=""></asp:Label>
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
                                            <label class="control-label col-sm-3" for="ddlSystemCountryName">Country<asp:RequiredFieldValidator ID="vddlSystemCountryName" runat="server" ErrorMessage="*" ControlToValidate="ddlSystemCountryName" InitialValue="0" CssClass="text-danger" ValidationGroup="CityMappingPop" ClientIDMode="Static"></asp:RequiredFieldValidator></label>
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
                                            <label class="control-label col-sm-3" for="ddlSystemStateName">State<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlSystemStateName" InitialValue="0" CssClass="text-danger" ValidationGroup="CityMappingPop"></asp:RequiredFieldValidator>--%></label><div class="col-sm-9">
                                                <div class="col-sm-10">
                                                    <asp:HiddenField ID="hdnIsJavascriptChagedValueddlSystemStateName" runat="server" Value=""  ClientIDMode="Static"/>

                                                    <asp:DropDownList ID="ddlSystemStateName" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSystemStateName_SelectedIndexChanged" ClientIDMode="Static">
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
                                            <label class="control-label col-sm-3" for="ddlSystemCityName">
                                                City
                                                                <%--<asp:RequiredFieldValidator ID="vddlSystemCityName" runat="server" ErrorMessage="*" ControlToValidate="ddlSystemCityName" InitialValue="0" CssClass="text-danger" ValidationGroup="CityMappingPop" ClientIDMode="Static"></asp:RequiredFieldValidator>--%>
                                            </label>
                                            <div class="col-sm-9">
                                                <div class="col-sm-10">
                                                    <asp:HiddenField ID="hdnIsJavascriptChagedValueddlSystemCityName" runat="server" Value="" ClientIDMode="Static"/>
                                                    <asp:DropDownList ID="ddlSystemCityName" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSystemCityName_SelectedIndexChanged" ClientIDMode="Static">
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
                                                <asp:RequiredFieldValidator ID="rfvtxtSearchSystemProduct" runat="server" ErrorMessage="*" ControlToValidate="txtSearchSystemProduct" CssClass="text-danger" ValidationGroup="CityMappingPop" ClientIDMode="Static"></asp:RequiredFieldValidator>

                                            </label>
                                            <div class="col-sm-9">
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtSearchSystemProduct" onkeyup="checkLen(this.value)" runat="server" CssClass="form-control" onlostfocus="callSystemProductNamechange(this);" ClientIDMode="Static"></asp:TextBox>
                                                    <div id="loading" style="padding: 5px; display: none;">
                                                        <img alt="Loading..." src="../../../images/ajax-loader.gif" />
                                                    </div>
                                                    <asp:DropDownList ID="ddlSystemProductName" Style="display: none" AppendDataBoundItems="true" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hdnSystemProduct_Id" runat="server"  ClientIDMode="Static"/>
                                                    <asp:HiddenField ID="hdnSystemProduct" runat="server"  ClientIDMode="Static"/>
                                                    <asp:HiddenField ID="hdnSelSystemProduct_Id" runat="server"  ClientIDMode="Static"/>
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
                                                    <asp:TextBox ID="txtSystemProductCode" runat="server" Enabled="false" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
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
                                            <label class="control-label col-sm-3" for="lblSystemLocation">Product Type</label>
                                            <div class="col-sm-9">
                                                <div class="col-sm-10">
                                                    <asp:Label ID="lblSystemProductType" runat="server" Text="" ClientIDMode="Static"></asp:Label>
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
                                                    <strong></string><asp:Label ID="lblSystemProductAddress" runat="server" Text="" ClientIDMode="Static"></asp:Label></strong>
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
                                                    <asp:Label ID="lblSystemLocation" runat="server" Text="" ClientIDMode="Static"></asp:Label>
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
                                                    <asp:Label ID="lblSystemTelephone" runat="server" Text="" ClientIDMode="Static"></asp:Label>
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
                                                    <asp:Label ID="lblSystemLatitude" runat="server" Text="" ClientIDMode="Static"></asp:Label>
                                                    &nbsp;/&nbsp;
                                                                    <asp:Label ID="lblSystemLongitude" runat="server" Text="" ClientIDMode="Static"></asp:Label>
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
                                                <asp:Button ID="btnAddProduct" runat="server" CssClass="btn btn-primary btn-sm" Text="Add Hotel" CommandName="OpenAddProduct" CausesValidation="true" ValidationGroup="AddCity" ClientIDMode="Static" />
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
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control col-lg-3" AppendDataBoundItems="true" onchange="ddlStatusChanged(this);" ClientIDMode="Static">
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
                                    <asp:Button ID="btnMatchedMapSelected" Visible="false" runat="server" CssClass="btn btn-primary btn-sm" Text="Map Selected" CommandName="MapSelected" OnClientClick="javascript:return confirm('Are you really sure you want to do this?');" OnClick="btnMatchedMapSelected_Click" CausesValidation="false" />
                                    <asp:Button ID="btnMatchedMapAll" Visible="false" runat="server" CssClass="btn btn-primary btn-sm" Text="Map All" CommandName="MapAll" OnClientClick="javascript:return confirm('Are you really sure you want to do this?');" OnClick="btnMatchedMapAll_Click" CausesValidation="false" />
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:GridView ID="grdMatchingProducts" runat="server" AllowPaging="True" AllowCustomPaging="true" AutoGenerateColumns="False"
                                    EmptyDataText="No Static Updates" CssClass="table table-hover table-striped" DataKeyNames="Accommodation_ProductMapping_Id,Supplier_Id,Accommodation_Id"
                                     OnPageIndexChanging="grdMatchingProducts_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                                        <asp:BoundField DataField="SupplierProductReference" HeaderText="Product Code" />
                                        <asp:BoundField DataField="ProductName" HeaderText="ProductName" />
                                        <asp:BoundField DataField="ProductType" HeaderText="ProductType" />
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

