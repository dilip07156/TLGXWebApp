//function to give auto side of textarea for Suggested Room Info
function pageLoad(sender, args) {
    var ta = document.querySelectorAll('textarea');
    autosize(ta);
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
        div.innerHTML = "";
        div.classList.remove("TooltipDescriptionShow");
        div.classList.add("TooltipDescriptionHide");
    }
}


function mySelectedID(selectedcheckboxval) {
    var roomName = selectedcheckboxval.parentElement.parentElement.firstChild.textContent;
    var tillUL = selectedcheckboxval.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement;
    var Button = tillUL.firstElementChild.firstChild;
    var tr = selectedcheckboxval.parentElement.parentElement.parentElement.getElementsByTagName("tr");
    for (var i = 1 ; i < tr.length ; i++) {
        tr[i].childNodes[0].firstChild.checked = false;
        tr[i].className = "row";
    }
    selectedcheckboxval.parentElement.parentElement.className += " alert alert-success";
    Button.textContent = selectedcheckboxval.parentElement.parentElement.firstChild.nextSibling.nextSibling.textContent;
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
function SelectedRow(element) {
    var row = $(element).parent().parent().closest('tr').next();
    if (row != null)
        if (row.find('.dropdownforBind') != null)
            row.find('.dropdownforBind').focus();
}

function BindRTDetails(controlval) {
    showLoadingImage();
    var acco_id = $(controlval).parent().parent().parent().find('.hidnAcoo_Id').val();
    var ulRoomInfo = $(controlval).parent().find('#ulRoomInfo');
    var acco_roomType_id = $(controlval).parent().parent().parent().find('.hdnAccommodation_RoomInfo_Id').val();
    //if (ulRoomInfo != null && ulRoomInfo[0].innerHTML.trim() == "") {
    if (ulRoomInfo != null && ulRoomInfo[0].getElementsByTagName("table")[0] === undefined) {
        if (acco_id != null && ulRoomInfo != null) {
            $.ajax({
                url: '../../../Service/RoomCategoryAutoComplete.ashx',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: {
                    'acco_id': acco_id,
                    'type': 'fillcategorywithdetails'
                },
                responseType: "json",
                success: function (result) {
                    var value = JSON.stringify(result);
                    var listItems = '';
                    if (result != null) {
                        var def = '<table class="table-bordered">  <tr class="row"><th class="col-md-1"></th><th class="col-md-5">Room Name</th> <th class="col-md-4">Room Category</th>';
                        def = def + ' <th class="col-md-1">Bed Type</th> <th class="col-md-1">Is Smoking</th></tr>';
                        var li = def;
                        var licheckbox = '<input type="checkbox" class="checkboxClass" id="myCheck" onclick="mySelectedID(this)">';
                        var licheckboxWithChecked = '<input type="checkbox" checked="true" class="checkboxClass" id="myCheck" onclick="mySelectedID(this)">';

                        var td = '<td class="col-md-3">';
                        var td4 = '<td class="col-md-4" style="word-wrap:  break-all;">';
                        var td1 = '<td class="col-md-1">';
                        var td2 = '<td class="col-md-2">';


                        var lic = ' <td style="display: none;" id="tdRoomInfoId">';
                        var licClose = '</table>';
                        var tdc = '</td>';
                        for (var i = 0; i < result.length; i++) {
                            if (acco_roomType_id != null && acco_roomType_id == result[i].Accommodation_RoomInfo_Id) {
                                li = li + '<tr class="row alert alert-success">';
                                li = li + td1 + licheckboxWithChecked + tdc;
                            }
                            else {
                                li = li + '<tr class="row">';
                                li = li + td1 + licheckbox + tdc;
                            }

                            li = li + td4 + result[i].RoomName + tdc;
                            li = li + td4 + result[i].RoomCategory + tdc;
                            li = li + td2 + result[i].BedType + tdc;
                            li = li + td1 + result[i].IsSomking + tdc;
                            li = li + lic + result[i].Accommodation_RoomInfo_Id + tdc + "</tr>";
                        }
                        li = li + licClose;

                        hideLoadingImage();
                        ulRoomInfo[0].innerHTML = li;
                    }
                },
                failure: function () {
                }
            });
        }
    }
}


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