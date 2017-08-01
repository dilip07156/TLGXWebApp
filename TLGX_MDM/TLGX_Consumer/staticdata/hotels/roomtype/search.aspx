<%@ Page Title="Room Type Mapping" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="search.aspx.cs" Inherits="TLGX_Consumer.staticdata.hotels.roomtype.search" %>

<%@ Register Src="~/controls/roomtype/searchRoomTypes.ascx" TagPrefix="uc1" TagName="searchRoomTypes" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
    <script type="text/javascript">

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
                                        listItems += "<option value='" + result[i].Accommodation_RoomInfo_Id + "'>" + result[i].RoomCategory + "</option>";
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
                debugger;
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
    </script>
    <h1 class="page-header">Room Type Mapping</h1>
    <uc1:searchRoomTypes runat="server" ID="searchRoomTypes" />
</asp:Content>
