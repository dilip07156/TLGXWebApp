//function to give auto side of textarea for Suggested Room Info
function pageLoad(sender, args) {
    var ta = document.querySelectorAll('textarea');
    autosize(ta);
    if ($('#tblTLGXRoomInfo').length > 0)
        $('#tblTLGXRoomInfo').DataTable({
            "bPaginate": true,
            "bProcessing": true
        });
}
$(document).ready(function () {
    if ($('#tblTLGXRoomInfo').length > 0)
        $('#tblTLGXRoomInfo').DataTable({
            "bPaginate": true,
            "bProcessing": true
        });
});

function showOnlineSuggestionModal(controlval) {
    $('#lblForSupplierRoomTypeName').text($(controlval).parent().closest('td').prev().find('#lblSupplierRoomTypeName').text());
    $("#modalOnlineSuggestion").modal('show');
    CheckSuggestionOnlineSyntactic(controlval);
    CheckSuggestionOnlineSemanticUnSup(controlval);
    CheckSuggestionOnlineSemanticSup(controlval);
}
function closeOnlineSuggestionModal() {
    $("#modalOnlineSuggestion").modal('hide');
}
//function to show  RoomDescription as tooltip 
function DisplayToolTip(controls) {
    var div = controls.nextElementSibling;
    if (div != null) {
        if (div.textContent.trim().length == 0) {
            div.innerHTML = "No Room Description Provided";
        }
        div.classList.remove("TooltipDescriptionHide");
        div.classList.add("TooltipDescriptionShow");
    }
}
//function to hide RoomDescription as tooltip
function HideToolTip(controls) {
    var div = controls.nextElementSibling;
    if (div != null) {
        // div.innerHTML = "";
        div.classList.remove("TooltipDescriptionShow");
        div.classList.add("TooltipDescriptionHide");
    }
}


function mySelectedID(selectedcheckboxval) {

    // var roomName = selectedcheckboxval.parentElement.parentElement.firstChild.textContent;
    var tr = selectedcheckboxval.parentElement.parentElement.parentElement.getElementsByTagName("tr");
    for (var i = 0; i < tr.length; i++) {
        //tr[i].childNodes[0].firstChild.checked = false;
        tr[i].className = "row";
    }
    //selectedcheckboxval.parentElement.parentElement.className += " alert alert-success";
    //selectedcheckboxval.parentElement.parentElement.className += " backgroundRowColor";
    //selectedcheckboxval.parentElement.getElementsByClassName("checkboxClass")[0].checked = true;
    var hdnAccommodation_SupplierRoomInfo_IdPopUp = $("#hdnAccommodation_SupplierRoomInfo_IdPopUp");

    var parentTr = (document.getElementById("MainContent_searchRoomTypes_grdRoomTypeMappingSearchResultsBySupplier")).getElementsByTagName("tr");
    for (var j = 0; j < parentTr.length; j++) {
        if (parentTr[j].lastElementChild.getElementsByClassName("hdnAccommodation_RoomInfo_Id")[0] != undefined && parentTr[j].lastElementChild.getElementsByClassName("hdnAccommodation_SupplierRoomTypeMapping_Id")[0].value == hdnAccommodation_SupplierRoomInfo_IdPopUp.val()) {
            //get Row
            var RoomInfoName = selectedcheckboxval.parentElement.parentElement.firstChild.nextSibling.nextSibling.textContent;
            var RoomCategory = selectedcheckboxval.parentElement.parentElement.firstChild.nextSibling.nextSibling.nextSibling.textContent;
            if (RoomCategory != undefined && RoomCategory != "") {

                RoomInfoName = RoomInfoName + "\n" + "(" + RoomCategory + ")";
            }
            parentTr[j].getElementsByClassName("roomtype")[0].textContent = RoomInfoName;
            var hdnAccommodation_RoomInfo_Id = parentTr[j].lastElementChild.getElementsByClassName("hdnAccommodation_RoomInfo_Id")[0];
            hdnAccommodation_RoomInfo_Id.value = selectedcheckboxval.parentElement.parentElement.lastElementChild.firstChild.textContent;




            //Setting check box and Dropdown to Mapped
            var checkBoxForSelectedRow = parentTr[j].lastElementChild.firstElementChild;
            if (checkBoxForSelectedRow != null)
                checkBoxForSelectedRow.checked = true;

            var MappingStatusDdl = parentTr[j].getElementsByClassName("MappingStatus")[0];
            if (MappingStatusDdl != null) {
                for (var i = 0; i < MappingStatusDdl.options.length; i++) {
                    if (MappingStatusDdl.options[i].text == "MAPPED") {
                        MappingStatusDdl.options[i].selected = true;
                        break;
                    }
                }
            }

        }
    }



    //var tillUL = selectedcheckboxval.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement;
    //var Button = tillUL.firstElementChild.firstChild;

    //Button.textContent = selectedcheckboxval.parentElement.parentElement.firstChild.nextSibling.nextSibling.textContent;
    //selectedcheckboxval.parentElement.getElementsByClassName("checkboxClass")[0].checked = true;
    //var hdnAccommodation_RoomInfo_Id = tillUL.parentElement.parentElement.lastElementChild.getElementsByClassName("hdnAccommodation_RoomInfo_Id")[0];
    //hdnAccommodation_RoomInfo_Id.value = selectedcheckboxval.parentElement.parentElement.lastElementChild.firstChild.textContent;

    ////Setting check box and Dropdown to Mapped
    //var checkBoxForSelectedRow = tillUL.parentElement.parentElement.lastElementChild.firstElementChild;
    //if (checkBoxForSelectedRow != null)
    //    checkBoxForSelectedRow.checked = true;

    //var MappingStatusDdl = tillUL.parentElement.parentElement.getElementsByClassName("MappingStatus")[0];
    //if (MappingStatusDdl != null) {
    //    for (var i = 0; i < MappingStatusDdl.options.length; i++) {
    //        if (MappingStatusDdl.options[i].text == "MAPPED") {
    //            MappingStatusDdl.options[i].selected = true;
    //            break;
    //        }
    //    }
    //}

}
function mySelectedOnlineOptionID(selectedcheckboxval) {
    var roomName = selectedcheckboxval.parentElement.parentElement.firstChild.textContent;
    var tillUL = selectedcheckboxval.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement;
    var Button = tillUL.firstElementChild.firstChild;
    var tr = selectedcheckboxval.parentElement.parentElement.parentElement.getElementsByTagName("tr");
    for (var i = 1; i < tr.length; i++) {
        tr[i].childNodes[0].firstChild.checked = false;
        tr[i].className = "row";
    }
    selectedcheckboxval.parentElement.parentElement.className += " alert alert-success";
    // Button.textContent = selectedcheckboxval.parentElement.parentElement.firstChild.nextSibling.nextSibling.textContent;
    selectedcheckboxval.parentElement.getElementsByClassName("checkboxClass")[0].checked = true;
    var hdnAccommodation_RoomInfo_Id = tillUL.parentElement.parentElement.lastElementChild.getElementsByClassName("hdnAccommodation_RoomInfo_Id")[0];
    hdnAccommodation_RoomInfo_Id.value = selectedcheckboxval.parentElement.parentElement.lastElementChild.firstChild.textContent;

    //Setting check box and Dropdown to Mapped
    var checkBoxForSelectedRow = tillUL.parentElement.parentElement.lastElementChild.firstElementChild;
    if (checkBoxForSelectedRow != null)
        checkBoxForSelectedRow.checked = true;

    var MappingStatusDdl = tillUL.parentElement.parentElement.getElementsByClassName("MappingStatus")[0];
    if (MappingStatusDdl != null) {
        for (var i = 0; i < MappingStatusDdl.options.length; i++) {
            if (MappingStatusDdl.options[i].text == "MAPPED") {
                MappingStatusDdl.options[i].selected = true;
                break;
            }
        }
    }

}

function showLoadingImage() {
    $('#loading').show();
}
function hideLoadingImage() {
    $('#loading').hide();
}
function showLoadingImageOnline() {
    $('#loadingOnline').show();
}
function hideLoadingImageOnline() {
    $('#loadingOnline').hide();
}

function SelectedRow(element) {
    var row = $(element).parent().parent().closest('tr').next();
    if (row != null)
        if (row.find('.dropdownforBind') != null)
            row.find('.dropdownforBind').focus();
}

function BindRTDetailsInTable(result, ulRoomInfo, acco_roomType_id, acco_id, acco_SupplierRoomTypeMapping_Id ) {
    var value = JSON.stringify(result);
    //var hdnAccommodation_RoomInfo_IdPopUp = $("#hdnAccommodation_RoomInfo_IdPopUp");
    //hdnAccommodation_RoomInfo_IdPopUp.val(acco_roomType_id);
    $("#hdnAcco_SuppRoomTypeMapping_Id").val(acco_SupplierRoomTypeMapping_Id);
    if (result != null) {
        var def = '<table id="tblTLGXRoomInfo" runat="server" class="table table-responsive table-hover table-striped table-bordered" style="border-collapse:collapse;" cellspacing="0" border="1" rules="all"> <thead> <tr class="row"><th class="col-md-1  CheckboxColumn" style="display: none;"></th><th class="col-md-1">ROOMID</th><th class="col-md-2">MDM Room Name</th> <th class="col-md-2">MDM Room Category</th>';
        def = def + '  <th class="col-md-1">MDM Bed Type</th> <th class="col-md-2">MDM Room View </th> <th class="col-md-1">MDM Room Size</th>  <th class="col-md-1">MDM Smk Flag</th><th class="col-md-1">Matching Score</th><th class="col-md-1">Mapping Status</th><th style="display: none;"></th><th style="display: none;"></th><th style="display: none;"></th><th style="display: none;"></th><th style="display: none;"></th></tr></thead>';
        var li = def;
        var licheckbox = '<input type="checkbox" class="checkboxClass" id="myCheck" onclick="mySelectedID(this)">';
        var licheckboxWithChecked = '<input type="checkbox" checked="true" class="checkboxClass" id="myCheck" onclick="mySelectedID(this)">';

        var td2 = '<td class="col-md-2" style="word-wrap:  break-all;">';
        var td3 = '<td class="col-md-3" style="word-wrap:  break-all;">';
        var td1 = '<td class="col-md-1">';
        var tdCheckbox = '<td class="col-md-1 CheckboxColumn" style="display: none;">';
        //var td21 = '<td class="col-md-2">';


        var ddlStatusValues = null;

        $.ajax({
            url: '../../../Service/fillAttributeDDL.ashx',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            data: {
                'OptionFor': 'ProductSupplierMapping',
                'Attribute_Name': 'MappingStatus'
            },
            responseType: "json",
            success: function (resultStatusValues) {

                ddlStatusValues = resultStatusValues;
            },
            failure: function () {
            }
        });


        var lic = ' <td style="display: none;" id="tdRoomInfoId">';
        var lic1 = ' <td style="display: none;" id="tdacco_SupplierRoomTypeMapping_Id">';
        var lic2 = '<td style="display: none;" id="acco_id">';
        var lic3 = ' <td style="display: none;" id="tdacco_SupplierRoomTypeMapping_Value_Id">';
        var lic4 = ' <td style="display: none;" id="tdCurrentMaapingStatus">';

        var licClose = '</table>';
        var tdc = '</td>';
        var selected = '';
        var listItems = '';
        var hdnAccommodation_RoomInfo_Id = '<input type="hidden" id="custId" name="custId" value="">';
        var hiddentd = '<td style="visibility:hidden" class="divOne">id2</td>';
        var currentMappingStatus = '';
        //Find index
        for (var i = 0; i < result.length; i++) {
            if (acco_roomType_id != null && acco_roomType_id == result[i].Accommodation_RoomInfo_Id) {
                li = li + '<tr class="row" >';
                li = li + tdCheckbox + licheckbox + tdc;
                li = li + td1 + result[i].TLGXAccoRoomId + tdc;
                li = li + td2 + result[i].RoomName + tdc;
                li = li + td2 + result[i].RoomCategory + tdc;
                li = li + td2 + result[i].BedType + tdc;
                li = li + td2 + result[i].RoomView + tdc;
                li = li + td2 + result[i].RoomSize + tdc;
                li = li + td1 + result[i].IsSomking + tdc;
                li = li + td1 + (result[i].MatchingScore == null ? '' : result[i].MatchingScore) + tdc;
                listItems = '';
                
                if (Date.parse(result[i].SystemEditDate) > Date.parse(result[i].UserEditDate)) {
                    currentMappingStatus = result[i].SystemMappingStatus;
                }
                else {
                    currentMappingStatus = result[i].UserMappingStatus;
                }
                for (var j = 0; j < ddlStatusValues.length; j++) {
                    var ddlStatusDetalis = '<select id= ddlAttributeValues_' + result[i].Accommodation_RoomInfo_Id + '>';
                    if (currentMappingStatus == null && ddlStatusValues[j].AttributeValue == "UNMAPPED") {
                        listItems += "<option selected='selected' value='" + ddlStatusValues[j].MasterAttributeValue_Id + "'>" + ddlStatusValues[j].AttributeValue + "</option>";
                    }
                    else if (currentMappingStatus == ddlStatusValues[j].AttributeValue) {
                        listItems += "<option selected='selected' value='" + ddlStatusValues[j].MasterAttributeValue_Id + "'>" + ddlStatusValues[j].AttributeValue + "</option>";
                    }
                    else {
                        listItems += "<option value='" + ddlStatusValues[j].MasterAttributeValue_Id + "'>" + ddlStatusValues[j].AttributeValue + "</option>";
                    }
                }
                li = li + td1 + ddlStatusDetalis + listItems + '</select>' + tdc;
                li = li + lic1 + acco_SupplierRoomTypeMapping_Id + tdc;
                li = li + lic2 + acco_id + tdc;
                li = li + lic3 + result[i].Accommodation_SupplierRoomTypeMapping_Value_Id + tdc;
                li = li + lic + result[i].Accommodation_RoomInfo_Id + tdc;

                if (currentMappingStatus == null) {
                    currentMappingStatus = "UNMAPPED";
                }
                li = li + lic4 + result[i].MappingStatus + tdc + "</tr>";
            }
        }
        listItems = '';
        currentMappingStatus = '';
        for (var i = 0; i < result.length; i++) {
            if (acco_roomType_id != null && acco_roomType_id == result[i].Accommodation_RoomInfo_Id) {
                continue;
                //li = li + '<tr class="row alert alert-success">';
                //li = li + td1 + licheckboxWithChecked + tdc;
            }
            else {
                li = li + '<tr class="row">';
                li = li + tdCheckbox + licheckbox + tdc;
            }

            li = li + td1 + result[i].TLGXAccoRoomId + tdc;
            li = li + td2 + result[i].RoomName + tdc;
            li = li + td2 + result[i].RoomCategory + tdc;
            li = li + td2 + result[i].BedType + tdc;
            li = li + td2 + result[i].RoomView + tdc;
            li = li + td2 + result[i].RoomSize + tdc;
            li = li + td1 + result[i].IsSomking + tdc;
            li = li + td1 + (result[i].MatchingScore == null ? '' : result[i].MatchingScore) + tdc;
            listItems = '';
            
            if (Date.parse(result[i].SystemEditDate) > Date.parse(result[i].UserEditDate)) {
                currentMappingStatus = result[i].SystemMappingStatus; 
            }
            else {
                currentMappingStatus = result[i].UserMappingStatus;
            }

            for (var j = 0; j < ddlStatusValues.length; j++) {
                var ddlStatusDetalis = '<select id= ddlAttributeValues_' + result[i].Accommodation_RoomInfo_Id + '>';
                if (currentMappingStatus == null && ddlStatusValues[j].AttributeValue == "UNMAPPED") {
                    listItems += "<option selected='selected' value='" + ddlStatusValues[j].MasterAttributeValue_Id + "'>" + ddlStatusValues[j].AttributeValue + "</option>";
                }
                else if (currentMappingStatus == ddlStatusValues[j].AttributeValue) {
                    listItems += "<option selected='selected' value='" + ddlStatusValues[j].MasterAttributeValue_Id + "'>" + ddlStatusValues[j].AttributeValue + "</option>";
                }
                else {
                    listItems += "<option value='" + ddlStatusValues[j].MasterAttributeValue_Id + "'>" + ddlStatusValues[j].AttributeValue + "</option>";
                }
            }
            
            li = li + td1 + ddlStatusDetalis + listItems + '</select>' + tdc;
            li = li + lic1 + acco_SupplierRoomTypeMapping_Id + tdc;
            li = li + lic2 + acco_id + tdc;
            li = li + lic3 + result[i].Accommodation_SupplierRoomTypeMapping_Value_Id + tdc;
            li = li + lic + result[i].Accommodation_RoomInfo_Id + tdc;
            if (currentMappingStatus == null) {
                currentMappingStatus = "UNMAPPED";
            }
            
            li = li + lic4 + currentMappingStatus + tdc + "</tr>";
        }
        li = li + licClose;
        hideLoadingImage();
        ulRoomInfo.html(li);
        if ($('#tblTLGXRoomInfo').length > 0)
            $('#tblTLGXRoomInfo').DataTable({
                "bPaginate": true,
                "bProcessing": true
            });
    }
}
function BindRTDetails(controlval) {
    var lblSupplierRoomTypeName = $(controlval).parent().parent().parent().find('#lblSupplierRoomTypeName').text();
    //Getting Extracted Attribute
    var ExtractedAttribute = "";
    var tableExtractedAttribute = "<table style='border-collapse: collapse;'><tbody><tr><td>Attribute Flags:  </td><table class='table table-responsive table-hover table-striped table-bordered'><tbody><tr>";
    var flag = 0;
    if ($(controlval).parent().parent().parent().find('#lstAlias') != undefined) {
        if ($(controlval).parent().parent().parent().find('#lstAlias tr').length > 0) {
            for (var i = 0; i < $(controlval).parent().parent().parent().find('#lstAlias tr td').length; i++) {
                var CurrentTd = $(controlval).parent().parent().parent().find('#lstAlias tr td')[i];
                if (CurrentTd != undefined && CurrentTd.firstElementChild != null && CurrentTd.firstElementChild.firstElementChild != null) {
                    if (flag > 3) {
                        tableExtractedAttribute = tableExtractedAttribute + "</tr><tr>"
                        flag = 0;
                    }
                    tableExtractedAttribute = tableExtractedAttribute + "<td>" + CurrentTd.firstElementChild.firstElementChild.title.trim() + "</td>";
                    flag += 1;
                }
            }
        }
    }
    tableExtractedAttribute = tableExtractedAttribute + "</tr></tbody></table></tr></tbody></table>";
    tableExtractedAttribute = tableExtractedAttribute + "<table cellspacing='10px' style='border - collapse: collapse;'><tbody><tr><td><input type='button' class='btn btn-primary btn-sm' value='Save'  onclick='submitSave();'/> </td> <td> &nbsp; <input class='btn btn-primary btn-sm' type='button' value='Perform Mapping'  onclick='submitTTFU()' /> </td><td><input type='hidden' id='hdnAcco_SuppRoomTypeMapping_Id'  value='' /></td></tr></table>";
    tableExtractedAttribute = tableExtractedAttribute + "<table cellspacing='10px' style='border - collapse: collapse;'><tbody><tr><td><div id='responseMessage' style='display: none;' class=''></div></td></tr></table>";
    $('#lblForTLGXRoomInfoName').text(lblSupplierRoomTypeName);
    $('#divAttribute').html(tableExtractedAttribute);


    $("#modalTLGXRoomInfo").modal('show');
    showLoadingImage();
    var hdnControlID = $("#hdnControlID");
    hdnControlID.val = controlval;
    var acco_id = $(controlval).parent().parent().parent().find('.hidnAcoo_Id').val();
    var ulRoomInfo = $('#ulRoomInfo');
    var acco_roomType_id = $(controlval).parent().parent().parent().find('.hdnAccommodation_RoomInfo_Id').val();
    var acco_SupplierRoomTypeMapping_Id = $(controlval).parent().parent().parent().find('.hdnAccommodation_SupplierRoomTypeMapping_Id').val();
    //alert(acco_id + "====" + acco_roomType_id + "====" + acco_SupplierRoomTypeMapping_Id);

    //Getting Row identity id and set into the model hidden variable
    var hdnAccommodation_SupplierRoomInfo_IdPopUp = $("#hdnAccommodation_SupplierRoomInfo_IdPopUp");
    if (hdnAccommodation_SupplierRoomInfo_IdPopUp != undefined && hdnAccommodation_SupplierRoomInfo_IdPopUp != null)
        hdnAccommodation_SupplierRoomInfo_IdPopUp.val(acco_SupplierRoomTypeMapping_Id);


    //if (ulRoomInfo != null && ulRoomInfo[0].innerHTML.trim() == "") {
    // if (ulRoomInfo != null && ulRoomInfo[0].getElementsByTagName("table")[0] === undefined) {
    if (acco_id != null && ulRoomInfo != null) {
        $.ajax({
            url: '../../../Service/RoomCategoryAutoComplete.ashx',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: {
                'acco_id': acco_id,
                'acco_SupplierRoomTypeMapping_Id': acco_SupplierRoomTypeMapping_Id,
                'type': 'fillcategorywithdetails'
            },
            responseType: "json",
            success: function (result) {
                BindRTDetailsInTable(result, ulRoomInfo, acco_roomType_id, acco_id, acco_SupplierRoomTypeMapping_Id );

            },
            failure: function () {
            }
        });
    }
    //}
}

function BindResponse(result) {
    var value = JSON.stringify(result);
    var table = "<table class='table table-responsive table-hover table-striped table-bordered'>";
    var tableparent = "<table class='table table-responsive table-hover table-striped table-bordered' style='width:23%'>";
    var listItems = tableparent + "<tr><th style='font-size: 14px;'>Syntactic</th><th style='font-size: 14px;'>Semantic (Unsupervised)</th><th style='font-size: 14px;'>Semantic (Supervised)</th></tr><tr><td>";
    var tr = "<tr>"; var trc = "</tr>";
    var td = "<td>"; var tdc = "</td>";

    listItems = listItems + table;
    if (result != null) {
        if (result._objMLSyn != null) {
            if (result._objMLSyn.matches.length > 0) {
                listItems = listItems + "<tr><th>Matched String</th><th>Score</th></tr>";
                for (var i = 0; i < result._objMLSyn.matches.length; i++) {
                    listItems = listItems + tr + td + result._objMLSyn.matches[i].matched_string + tdc;
                    listItems = listItems + td + result._objMLSyn.matches[i].score + tdc + trc;
                }
            }
            //listItems = listItems + "<tr><td colspan='2'>" + "<b>Accommodation Room Details :</b>" + "</td></tr>";<th>AccommodationRoomInfo_Id</th>

            if (result._objMLSyn.AccommodationRoomInfo_Id.length > 0) {
                listItems = listItems + "<tr><td colspan='2'><div class='ulRoomInfoOnlineAccoDetails'>" + table + "<tr><th> System Room Name</th></tr>";
                for (var j = 0; j < result._objMLSyn.AccommodationRoomInfo_Id.length; j++) {
                    //listItems = listItems + "<tr> <td> " + result[0].AccommodationRoomInfo_Id[j].AccommodationRoomInfo_Id + "</td>";
                    listItems = listItems + "<tr><td> " + result._objMLSyn.AccommodationRoomInfo_Id[j].system_room_name + "</td></tr>";
                }
                listItems = listItems + "</table></div></td></tr></table><td>";
            }
        }

        listItems = listItems + table;
        if (result._objMLSem != null) {
            if (result._objMLSem.matches.length > 0) {
                listItems = listItems + "<tr><th>Matched String</th><th>Score</th></tr>";
                for (var i = 0; i < result._objMLSem.matches.length; i++) {
                    listItems = listItems + tr + td + result._objMLSem.matches[i].matched_string + tdc;
                    listItems = listItems + td + result._objMLSem.matches[i].score + tdc + trc;
                }
            }
            if (result._objMLSem.AccommodationRoomInfo_Id.length > 0) {
                listItems = listItems + "<tr><td colspan='2'><div class='ulRoomInfoOnlineAccoDetails'>" + table + "<tr><th> System Room Name</th></tr>";
                for (var j = 0; j < result._objMLSem.AccommodationRoomInfo_Id.length; j++) {
                    listItems = listItems + "<tr> <td> " + result._objMLSem.AccommodationRoomInfo_Id[j].system_room_name + "</td></tr>";
                }
                listItems = listItems + "</table></div></td></tr></table><td>";
            }
        }

        listItems = listItems + table;
        if (result._objMLSupSem != null) {
            if (result._objMLSupSem.matches.length > 0) {
                listItems = listItems + "<tr><th>Matched String</th><th>Score</th></tr>";
                for (var i = 0; i < result._objMLSupSem.matches.length; i++) {
                    listItems = listItems + tr + td + result._objMLSupSem.matches[i].matched_string + tdc;
                    listItems = listItems + td + result._objMLSupSem.matches[i].score + tdc + trc;
                }
            }
            if (result._objMLSupSem.AccommodationRoomInfo_Id.length > 0) {
                listItems = listItems + "<tr><td colspan='2'><div class='ulRoomInfoOnlineAccoDetails'>" + table + "<tr><th> System Room Name</th></tr>";
                for (var j = 0; j < result._objMLSupSem.AccommodationRoomInfo_Id.length; j++) {
                    listItems = listItems + "<tr> <td> " + result._objMLSupSem.AccommodationRoomInfo_Id[j].system_room_name + "</td></tr>";
                }
                listItems = listItems + "</table></div></td></tr></table>";
            }
        }
        hideLoadingImageOnline();
        // listItems = listItems.replace("<td></td>", "");
        listItems = listItems + "</table>";
        $('.ulRoomInfoOnline').html(listItems);
    }

}
function CheckSuggestionOnline(controlval) {
    showLoadingImageOnline();
    var acco_SupplierRoomTypeMapping_Id = $(controlval).parent().parent().find('.hdnAccommodation_SupplierRoomTypeMapping_Id').val();
    var ulRoomInfo = $('.ulRoomInfoOnline');// $(controlval).parent().find('#ulRoomInfoOnline');
    var acco_roomType_id = $(controlval).parent().parent().parent().find('.hdnAccommodation_RoomInfo_Id').val();
    //if (ulRoomInfo != null && ulRoomInfo[0].innerHTML.trim() == "") {
    // if (ulRoomInfo != null) {
    if (acco_SupplierRoomTypeMapping_Id != null) {
        $.ajax({
            url: '../../../Service/GetSRT_ML_suggestion.ashx',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: {
                'acco_SupplierRoomTypeMapping_Id': acco_SupplierRoomTypeMapping_Id
            },
            responseType: "json",
            success: function (result) {
                if (result != null) {
                    BindResponse(result);
                }
            },
            failure: function () {
            }
        });
    }
    //}
}

//ML Lazy loding
//ML Syntactic
function showLoadingImageOnlineSyntactic() {
    $('#loadingOnlineSyntactic').show();
}
function hideLoadingImageOnlineSyntactic() {
    $('#loadingOnlineSyntactic').hide();
}
function BindResponseSyntactic(resultarray) {
    var value = JSON.stringify(result);
    var table = "<table class='table table-responsive table-hover table-striped table-bordered'>";

    var tableparent = "<table class='table table-responsive table-hover table-striped table-bordered' style='width: 23%;'>";
    var tr = "<tr>"; var trc = "</tr>";
    var td = "<td>"; var tdc = "</td>";
    var listItems = tableparent;
    if (resultarray != null) {
        if (resultarray.length > 0) {
            var result = resultarray[0];
            if (result.matches.length > 0) {
                listItems = listItems + "<tr><th>Matched String</th><th>Score</th></tr>";
                for (var i = 0; i < result.matches.length; i++) {
                    listItems = listItems + tr + td + result.matches[i].matched_string + tdc;
                    listItems = listItems + td + result.matches[i].score + tdc + trc;
                }



                if (result.AccommodationRoomInfo_Id.length > 0) {
                    listItems = listItems + "<tr><td colspan='2'><div class='ulRoomInfoOnlineAccoDetails'>" + table + "<tr><th> System Room Name</th></tr>";
                    for (var j = 0; j < result.AccommodationRoomInfo_Id.length; j++) {
                        //listItems = listItems + "<tr> <td> " + result[0].AccommodationRoomInfo_Id[j].AccommodationRoomInfo_Id + "</td>";
                        listItems = listItems + "<tr><td> " + result.AccommodationRoomInfo_Id[j].system_room_name + "</td></tr>";
                    }
                    listItems = listItems + "</table></div></td></tr></table>";
                }
                hideLoadingImageOnlineSyntactic();
                $('#ulRoomInfoOnlineSyntactic').html(listItems);

            }
        }
    }
}
function CheckSuggestionOnlineSyntactic(controlval) {
    showLoadingImageOnlineSyntactic();
    var acco_SupplierRoomTypeMapping_Id = $(controlval).parent().parent().find('.hdnAccommodation_SupplierRoomTypeMapping_Id').val();
    var ulRoomInfo = $('.ulRoomInfoOnline');// $(controlval).parent().find('#ulRoomInfoOnline');
    var acco_roomType_id = $(controlval).parent().parent().parent().find('.hdnAccommodation_RoomInfo_Id').val();
    if (acco_SupplierRoomTypeMapping_Id != null) {
        $.ajax({
            url: '../../../Service/GetSRT_ML_suggestion_Syntactic.ashx',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: {
                'acco_SupplierRoomTypeMapping_Id': acco_SupplierRoomTypeMapping_Id
            },
            responseType: "json",
            success: function (result) {
                if (result != null) {
                    BindResponseSyntactic(result);
                }
            },
            failure: function () {
            }
        });
    }

}

//END ML Syntactic

//ML UnS Semantic
function showLoadingImageOnlineSemanticUnSup() {
    $('#loadingOnlineSemanticUnSup').show();
}
function hideLoadingImageOnlineSemanticUnSup() {
    $('#loadingOnlineSemanticUnSup').hide();
}
function BindResponseSemanticUnSup(resultarray) {
    var value = JSON.stringify(result);
    var table = "<table class='table table-responsive table-hover table-striped table-bordered'>";

    var tableparent = "<table class='table table-responsive table-hover table-striped table-bordered' style='width: 23%;'>";
    var tr = "<tr>"; var trc = "</tr>";
    var td = "<td>"; var tdc = "</td>";
    var listItems = tableparent;
    if (resultarray != null) {
        if (resultarray.length > 0) {
            var result = resultarray[0];
            if (result.matches.length > 0) {
                listItems = listItems + "<tr><th>Matched String</th><th>Score</th></tr>";
                for (var i = 0; i < result.matches.length; i++) {
                    listItems = listItems + tr + td + result.matches[i].matched_string + tdc;
                    listItems = listItems + td + result.matches[i].score + tdc + trc;
                }



                if (result.AccommodationRoomInfo_Id.length > 0) {
                    listItems = listItems + "<tr><td colspan='2'><div class='ulRoomInfoOnlineAccoDetails'>" + table + "<tr><th> System Room Name</th></tr>";
                    for (var j = 0; j < result.AccommodationRoomInfo_Id.length; j++) {
                        //listItems = listItems + "<tr> <td> " + result[0].AccommodationRoomInfo_Id[j].AccommodationRoomInfo_Id + "</td>";
                        listItems = listItems + "<tr><td> " + result.AccommodationRoomInfo_Id[j].system_room_name + "</td></tr>";
                    }
                    listItems = listItems + "</table></div></td></tr></table>";
                }
                hideLoadingImageOnlineSemanticUnSup();
                $('#ulRoomInfoOnlineSemanticUnSup').html(listItems);

            }
        }
    }
}
function CheckSuggestionOnlineSemanticUnSup(controlval) {
    showLoadingImageOnlineSemanticUnSup();
    var acco_SupplierRoomTypeMapping_Id = $(controlval).parent().parent().find('.hdnAccommodation_SupplierRoomTypeMapping_Id').val();
    var ulRoomInfo = $('.ulRoomInfoOnline');// $(controlval).parent().find('#ulRoomInfoOnline');
    var acco_roomType_id = $(controlval).parent().parent().parent().find('.hdnAccommodation_RoomInfo_Id').val();
    if (acco_SupplierRoomTypeMapping_Id != null) {
        $.ajax({
            url: '../../../Service/GetSRT_ML_suggestion_Semantic.ashx',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: {
                'acco_SupplierRoomTypeMapping_Id': acco_SupplierRoomTypeMapping_Id
            },
            responseType: "json",
            success: function (result) {
                if (result != null) {
                    BindResponseSemanticUnSup(result);
                }
            },
            failure: function () {
            }
        });
    }

}
//End ML UsS Semantic

//ML Sup Semantic
function showLoadingImageOnlineSemanticSup() {
    $('#loadingOnlineSemanticSup').show();
}
function hideLoadingImageOnlineSemanticSup() {
    $('#loadingOnlineSemanticSup').hide();
}
function BindResponseSemanticSup(resultarray) {
    var value = JSON.stringify(result);
    var table = "<table class='table table-responsive table-hover table-striped table-bordered'>";

    var tableparent = "<table class='table table-responsive table-hover table-striped table-bordered' style='width: 23%;'>";
    var tr = "<tr>"; var trc = "</tr>";
    var td = "<td>"; var tdc = "</td>";
    var listItems = tableparent;
    if (resultarray != null) {
        if (resultarray.length > 0) {
            var result = resultarray[0];
            if (result.matches.length > 0) {
                listItems = listItems + "<tr><th>Matched String</th><th>Score</th></tr>";
                for (var i = 0; i < result.matches.length; i++) {
                    listItems = listItems + tr + td + result.matches[i].matched_string + tdc;
                    listItems = listItems + td + result.matches[i].score + tdc + trc;
                }



                if (result.AccommodationRoomInfo_Id.length > 0) {
                    listItems = listItems + "<tr><td colspan='2'><div class='ulRoomInfoOnlineAccoDetails'>" + table + "<tr><th> System Room Name</th></tr>";
                    for (var j = 0; j < result.AccommodationRoomInfo_Id.length; j++) {
                        //listItems = listItems + "<tr> <td> " + result[0].AccommodationRoomInfo_Id[j].AccommodationRoomInfo_Id + "</td>";
                        listItems = listItems + "<tr><td> " + result.AccommodationRoomInfo_Id[j].system_room_name + "</td></tr>";
                    }
                    listItems = listItems + "</table></div></td></tr></table>";
                }
                hideLoadingImageOnlineSemanticSup();
                $('#ulRoomInfoOnlineSemanticSup').html(listItems);

            }
        }
    }
}
function CheckSuggestionOnlineSemanticSup(controlval) {
    showLoadingImageOnlineSemanticSup();
    var acco_SupplierRoomTypeMapping_Id = $(controlval).parent().parent().find('.hdnAccommodation_SupplierRoomTypeMapping_Id').val();
    var ulRoomInfo = $('.ulRoomInfoOnline');// $(controlval).parent().find('#ulRoomInfoOnline');
    var acco_roomType_id = $(controlval).parent().parent().parent().find('.hdnAccommodation_RoomInfo_Id').val();
    if (acco_SupplierRoomTypeMapping_Id != null) {
        $.ajax({
            url: '../../../Service/GetSRT_ML_suggestion_Supervised_Semantic.ashx',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: {
                'acco_SupplierRoomTypeMapping_Id': acco_SupplierRoomTypeMapping_Id
            },
            responseType: "json",
            success: function (result) {
                if (result != null) {
                    BindResponseSemanticSup(result);
                }
            },
            failure: function () {
            }
        });
    }

}
//End ML UsS Semantic
//


function SelectedRow(element) {
    var row = $(element).parent().parent().closest('tr').next();
    if (row != null)
        if (row.find('.dropdownforBind') != null)
            row.find('.dropdownforBind').focus();
}
function fillDropDown(record, onClick) {

    if (onClick) {
        var currentRow = $(record).parent().parent();
        var acco_id = currentRow.find('.hidnAcoo_Id').val();
        var hdnAccommodation_RoomInfo_Id = currentRow.find('.hdnAccommodation_RoomInfo_Id');
        var hdnAccommodation_RoomInfo_Name = currentRow.find('.hdnAccommodation_RoomInfo_Name');


        var ddlRoomInfo = currentRow.find('.dropdownforBind');
        if (acco_id != null && ddlRoomInfo != null) {
            var selectedText = null;
            var selectedOption = null;
            var selectedVal = null;
            //if (ddlRoomInfo.find("option").length > 1) {
            //    selectedText = ddlRoomInfo.find("option:selected").text();
            //    selectedOption = ddlRoomInfo.find("option");
            //    selectedVal = ddlRoomInfo.val();
            //}
            if (ddlRoomInfo != null && ddlRoomInfo.is("select")) {
                $.ajax({
                    url: '../../../Service/RoomCategoryAutoComplete.ashx',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: {
                        'acco_id': acco_id,
                        'type': 'fillcategory'
                    },
                    responseType: "json",
                    success: function (result) {
                        //if (ddlRoomInfo.find("option:not(:first)").length > 0) {
                        ddlRoomInfo.find("option").remove();
                        //}

                        var value = JSON.stringify(result);
                        var listItems = '';
                        if (result != null) {

                            listItems += "<option value='0'>Select</option>";

                            for (var i = 0; i < result.length; i++) {
                                listItems += "<option value='" + result[i].Accommodation_RoomInfo_Id + "'>" + result[i].RoomName + "| " + result[i].RoomCategory + "| " + result[i].BedType + "| " + result[i].IsSomking + "</option>";
                            }
                            ddlRoomInfo.append(listItems);
                        }
                        $('option:selected', ddlRoomInfo).removeAttr('selected');
                        //  $('#mySelect option:selected').removeAttr('selected');
                        if (hdnAccommodation_RoomInfo_Id != null && hdnAccommodation_RoomInfo_Id.val() != '') {
                            ddlRoomInfo.find("option").prop('selected', false).filter(function () {
                                return $(this).val() == hdnAccommodation_RoomInfo_Id.val();
                            }).attr("selected", "selected");
                        }
                        else if (selectedText != null && selectedText != "Select") {
                            ddlRoomInfo.find("option").prop('selected', false).filter(function () {
                                return $(this).text() == selectedText;
                            }).attr("selected", "selected");
                        }
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
        var ddlRoomInfo = currentRow.find('.dropdownforBind');
        var selectedText = ddlRoomInfo.find("option:selected").text();
        var selectedVal = ddlRoomInfo.val();
        ddlRoomInfo.find("option").remove();
        var listItems = "<option value='0'>Select</option>";
        listItems = listItems + "<option selected = 'selected' value='" + selectedVal + "'>" + selectedText + "</option>";
        ddlRoomInfo.append(listItems);
        var hdnAccommodation_RoomInfo_Id = currentRow.find('.hdnAccommodation_RoomInfo_Id');
        hdnAccommodation_RoomInfo_Id.val(selectedVal);
    }
}

function submitSave() {
    var r = confirm("Are you really sure you want to do this ?");
    if (r == true) {
        showLoadingImage();
        var values = new Array();
        //var checkcount = $('#tblTLGXRoomInfo #myCheck:checked').length;
        var table = $("#tblTLGXRoomInfo");
        jsonObj = [];
        var emptyGuid = '00000000-0000-0000-0000-000000000000';
       
        table.find('tr').each(function (i) {
            var row = $(this);
           // if (row.find('input[type="checkbox"]').is(':checked')) {
           
                var $tds = $(this).find('td');
            item = {};
            item.Accommodation_RoomInfo_Id = $tds.eq(13).text();
            var ddlMapingStatusData = $('#ddlAttributeValues_' + item.Accommodation_RoomInfo_Id + ' :selected').text();
            if (ddlMapingStatusData != $tds.eq(14).text()) {
                item.id = $tds.eq(1).text();
                item.RoomCategory = $tds.eq(3).text();
                item.MatchingScore = $tds.eq(8).text();
                item.Accommodation_RoomInfo_Id = $tds.eq(13).text();
                item.UserMappingStatus = $('#ddlAttributeValues_' + item.Accommodation_RoomInfo_Id + ' :selected').text();
                item.Accommodation_SupplierRoomTypeMapping_Id = $tds.eq(10).text();
                item.acco_id = $tds.eq(11).text();
                item.Accommodation_SupplierRoomTypeMapping_Value_Id = $tds.eq(12).text();
                if (item.Accommodation_SupplierRoomTypeMapping_Value_Id == 'null')
                    item.Accommodation_SupplierRoomTypeMapping_Value_Id = emptyGuid;


                jsonObj.push(item);
            }
          //  }
                //option: selected").val();

            //}
        });

        console.log(jsonObj);
        $.ajax({
            type: 'POST',
            url: '../../../Service/AddUpdateMappingDetails.ashx',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(jsonObj),
            responseType: "json",
            success: function (result) {
                $("#btnSuggestion").click();
                hideLoadingImage();
                $("#responseMessage").css("display", "block").addClass("alert alert-success").html(result.StatusMessage).delay(2000).fadeOut();
            },
            failure: function () {
                hideLoadingImage();
                $("#responseMessage").css("display", "block").addClass("alert alert-warning").html(result.StatusMessage).delay(2000).fadeOut();
            }
        });
    }
    else {
        return false;
    }

}

function submitTTFU() {
    var r = confirm("Are you really sure you want to do this ?");
    if (r == true) {
        showLoadingImage();
        var values = new Array();

        jsonObj = [];
        item = {};
        item.Acco_RoomTypeMap_Id = $("#hdnAcco_SuppRoomTypeMapping_Id").val();
        jsonObj.push(item);

        console.log(jsonObj);
        $.ajax({
            type: 'POST',
            url: '../../../Service/TTFUSelectedBySupplier.ashx',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(jsonObj),
            responseType: "json",
            success: function (result) {
                //$("#btnSuggestion").click();
                hideLoadingImage();
                $("#responseMessage").css("display", "block").addClass("alert alert-success").html("Mapping Updated Successfully").delay(2000).fadeOut();
            },
            failure: function () {
                hideLoadingImage();
                $("#responseMessage").css("display", "block").addClass("alert alert-warning").html("Error while updating mapping").delay(2000).fadeOut();
            }
        });

    }
        else {
            return false;
        }
    
}
$("#modalOnlineSuggestion").on('shown.bs.modal', function (e) {
    alert("I want this to appear after the modal has opened!");
});