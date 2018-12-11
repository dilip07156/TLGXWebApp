<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityMedia.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.ActivityMedia" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script src="../../../Scripts/JqueryUI/jquery-ui.js"></script>
<link href="../../../Scripts/JqueryUI/jquery-ui.css" rel="stylesheet" /
<link href="../../../Scripts/dataTables.bootstrap.min.css" rel="stylesheet" />
<link href="../../../Scripts/jquery.dataTables.min.css" rel="stylesheet" />
<script type="text/javascript" src="../../../Scripts/jquery.dataTables.min.js"></script>
<style type="text/css">
    .carousel-control {
        width: 5%;
    }

    .carousel {
        position: relative;
    }

    .controllers {
        position: absolute;
        top: 50px;
    }

    .carousel-caption {
        /*left: 20%;
right: 20%;
padding-bottom: 30px;*/
        right: auto;
        left: 10%;
    }

    .carousel-control.left,
    .carousel-control.right {
        background-image: none;
    }

    .carousel-caption {
        position: relative;
        left: 0;
        right: 0;
        bottom: 0;
        z-index: 10;
        padding-top: 0;
        padding-bottom: 0;
        color: #000;
        text-align: left;
        text-shadow: none;
        /; /*/text-shadow: 0 1px 2px rgba(0,0,0,.6);*/
    }

    .x-lg {
        width: 90%;
        overflow-y: hidden !important;
    }
    .dataTables_paginate_css {
  float: left !important;
}
    .CustomStyle
    {
        top: 1% !important;
        color: black;
    }
    .pagination a {
  border: 1px solid #ddd; /* Gray */
}
</style>

<script>
    function showMediaModal() {
        $("#moMedia").modal('show');
    }
    function closeMediaModal() {
        $("#moMedia").modal('hide');
    }
    function pageLoad(sender, args) {
        var hv = $('#MainContent_ActivityMedia_hdnFlag').val();
        if (hv == "true") {
            closeMediaModal();
            $('#MainContent_ActivityMedia_hdnFlag').val("false");
        }
    }

</script>
<script src="../../../Scripts/bootbox.min.js"></script>

<script>

    $(function () {
        //$('#carousel-example-generic').bind('slide.bs.carousel', function (e) {
        ////before slide change
        //    getMeta();
        //});
        var ImageAttribute = [];
        $('#carousel-example-generic').bind('slid.bs.carousel', function (e) {

            //SaveImageReview();
            //after slide changed
            getMeta();
        });
        $('#carousel-example-generic').bind('slide.bs.carousel', function (e) {

            SaveImageReview();

        });
    });

    function renderImageDeatils(result, imagereviewresult) {

        if (result != null && result.length > 0) {
            for (var i = 0; i < result.length; i++) {
                if (result[i].Media_URL != null) {

                    $('#<%=Product_Id.ClientID %>').text(result[i].CommonProductNameSubType_Id);
                    // $('#MainContent_ActivityMedia_hdn_activitymedia_id').val(result[i].Activity_Media_Id);                    
                    var divcontrol = '<div class="item"/>';
                    var divcontrolactive = '<div class="item active"/>';
                    var div = null;
                    if (i == 0) {
                        div = $(divcontrolactive);
                    }
                    else {
                        div = $(divcontrol);
                    }
                    var imgRowDiv = $('<div class="row"/>');

                    var imgColDiv = $('<div class="col-sm-9" style="padding: 0px!important;"/>');
                    imgColDiv.append(" <img class='img-responsive' src= '" + result[i].Media_URL + "' alt=' Image Not Found'/>");

                    imgRowDiv.append(imgColDiv);

                    var capColDiv = $('<div class="col-sm-3 valuediv"/>');
                    var d = $("<div class='carousel-caption'/>");
                    d.append("<p><h5><b style='font-size:initial;'>Media Id:</b><strong style='font-size:initial;'>" + (result[i].MediaID == null ? "Not given by supplier" : result[i].MediaID) + "</strong></h5></p>");
                    d.append('<h4><b>Details by Supplier  - </b></h4>');
                    var td = "<td>";
                    var tr = "<tr>";
                    var td_close = "</td>";
                    var tr_close = "</tr>";
                    var tbltable = "<table class='table table-striped table-bordered' style='width:80%'><tbody>"
                    tbltable = tbltable + '<th>Attribute Type</th><th>Attribute Value</th>'
                    if (result[i].Media_Caption != null) {
                        tbltable = tbltable + tr + td + '<b>Caption</b>' + td_close;
                        tbltable = tbltable + td + (result[i].Media_Caption) + td_close + tr_close;
                    }
                    if (result[i].MediaName != null) {
                        tbltable = tbltable + tr + td + '<b>Name</b>' + td_close;
                        tbltable = tbltable + td + (result[i].MediaName) + td_close + tr_close;
                    }
                    if (result[i].Description != null) {
                        tbltable = tbltable + tr + td + '<b>Description</b>' + td_close;
                        tbltable = tbltable + td + (result[i].Description) + td_close + tr_close;
                    }
                    if (result[i].Media_Width != null) {
                        tbltable = tbltable + tr + td + '<b>Media Width</b>' + td_close;
                        tbltable = tbltable + td + (result[i].Media_Width) + td_close + tr_close;
                    }
                    if (result[i].Media_Height != null) {
                        tbltable = tbltable + tr + td + '<b>Media Height</b>' + td_close;
                        tbltable = tbltable + td + (result[i].Media_Height) + td_close + tr_close;
                    }
                    if (result[i].MediaType != null) {
                        tbltable = tbltable + tr + td + '<b>Media Type</b>' + td_close;
                        tbltable = tbltable + td + (result[i].MediaType) + td_close + tr_close;
                    }
                    tbltable = tbltable + "</table>";
                    d.append(tbltable);
                    var ImagePreview = $("<div class='carousel-caption-ImagePreview' id='ImagePreview'/>");
                   

                    d.append(ImagePreview);
                    var tblcheckbox = "";// = "<table class='table table-striped table-bordered' style='width:80%'><tbody><tr><td><input type=" + 'checkbox runat=server' + " id=" + 'ckh_Watermarkx' + " checked  /></td><td><b>Has Watermark</b>";

                    if (result[i].IsWaterMark) {
                        tblcheckbox = tblcheckbox + "<table class='table table-striped table-bordered' style='width:80%'><tbody><tr><td><input type=" + 'checkbox runat=server' +
                            " id=" + 'ckh_Watermark' + " checked  /></td><td><b>Has Watermark</b>";
                    }
                    else {
                        tblcheckbox = tblcheckbox + "<table class='table table-striped table-bordered' style='width:80%'><tbody><tr><td><input type=" + 'checkbox runat=server' +
                            " id=" + 'ckh_Watermark' + " /></td><td><b>Has Watermark</b>";
                    }
                    if (result[i].IsRelevent) {
                        tblcheckbox = tblcheckbox + "<tr><td><input type=" + 'checkbox runat=server' +
                            " id=" + 'cbk_relevent' + " checked  /></td><td><b>Is relevant</b>";
                    }
                    else {
                        tblcheckbox = tblcheckbox + "<tr><td><input type=" + 'checkbox runat=server' +
                            " id=" + 'cbk_relevent' + " /></td><td><b>Is relevant</b>";
                    }

                    if (result[i].IsDuplicate) {
                        tblcheckbox = tblcheckbox + "<tr><td><input type=" + 'checkbox runat=server' +
                            " id=" + 'chk_duplicate' + " checked  /></td><td><b>Is Duplicate</b>";

                    }
                    else {
                        tblcheckbox = tblcheckbox + "<tr><td><input type=" + 'checkbox runat=server' +
                            " id=" + 'chk_duplicate' + " /></td><td><b>Is Duplicate</b>";

                    }
                    d.append("<br/>");
                    if (result[i].Media_Feedback != null) {
                        tblcheckbox = tblcheckbox + "<tr> <td colspan='2'><b>Feedback</b>  : <textarea rows=" + '4' + " cols=" + '40' + "  id=" + 'txt_feedback' + " style='resize:none;border-radius:4px;' >" + result[i].Media_Feedback + "</textarea></td></tr>";

                    }
                    else {
                        tblcheckbox = tblcheckbox + "<tr> <td colspan='2'><b>Feedback</b>  : <textarea rows=" + '4' + " cols=" + '40' + "  id=" + 'txt_feedback' + " style='resize:none;border-radius:4px;' >" + '' + "</textarea></td></tr>";
                    }

                    tblcheckbox = tblcheckbox + "</td></tr></tbody></table>";

                    d.append(tblcheckbox);
                    //$('#ulRoomInfo').html(tblcheckbox);
                    d.append("<input type=" + 'hidden' + " id=" + 'hdn_activitymedia_id' + " value='" + result[i].Activity_Media_Id + "' />");
                    d.append("<br/>");
                    d.append("<div class='carousel-caption-imagedetails'/>");
                    capColDiv.append(d);
                    imgRowDiv.append(capColDiv);
                    div.append(imgRowDiv);
                    var imgRowCountDiv = $('<div class="row"/>');
                    var imgCountDiv = $('<div class="col-sm-12" style="padding-right:30px;margin-top:10px; font-size: larger!important;"/>');
                    imgCountDiv.append("<p class='pull-right orange'><b>" + (i + 1) + " Of " + result.length + "</b></p>");
                    imgRowDiv.append(imgCountDiv);
                    div.append(imgRowCountDiv);


                    $("#photolost").append(div);


                }
            }
            if (imagereviewresult.length > 0) {
                var captActive = $('.item.active').find('.carousel-caption').find('.carousel-caption-ImagePreview');
                var ImageMediaId = $("#photolost >.active >.row >.valuediv").find('#hdn_activitymedia_id').val();
                var td = "<td>";
                var tr = "<tr>";
                var td_close = "</td>";
                var tr_close = "</tr>";

                var tbltable = "<table id='ImagePreviewTable' class='table  table-striped table-bordered' style='width:90%;margin:0px;'><thead><th class='col-sm-4'>Attribute Type</th><th class='col-sm-8'>Attribute Value</th></thead><tbody>"
                captActive.empty();
                captActive.append('<h4><b>Image Details -</b></h4>');

                for (var i = 0; i < imagereviewresult.length; i++) {
                    if (imagereviewresult[i].Activity_Media_Id == ImageMediaId) {
                        tbltable = tbltable + tr + td + "<b>" + imagereviewresult[i].AttributeType + "</b>" + td_close;
                        tbltable = tbltable + td + imagereviewresult[i].AttributeValue + td_close + tr_close;
                    }
                }
                tbltable = tbltable + "</body></table>";
                captActive.append(tbltable);
                //$('#ImagePreview').html(tbltable);
                if ($('#ImagePreviewTable').length > 0)
                    $('#ImagePreviewTable').DataTable({
                        "bPaginate": true,
                        "bProcessing": true,
                        "pageLength": 3,
                        "searching": false,
                        "bLengthChange": false,
                        "bInfo": false,
                        "pagingType": "numbers"

                    });
                
                $(".dataTables_paginate").addClass("dataTables_paginate_css "); 
               // $(".dataTables_paginate").addClass("pagination"); 
                

            } else {
                getMeta();
            }


            //getMeta();
        }
    }

    function GetMediaFor() {
        $("#photolost").empty();
        var ActFlavID = '<%= Request.QueryString["Activity_Flavour_Id"] %>';
        $.ajax({
            type: 'GET',
            url: '/../../../../Service/ActivityMediaInfo.ashx?ActFlavID=' + ActFlavID,
            dataType: "json",
            success: function (result) {

                ImageAttribute = result.ImageResultReview;
                renderImageDeatils(result.ImageResult, result.ImageResultReview);

            }
        });
        showImageModal();
    }
    // GET IMAGE DETAILS FROM SRC
    function getMeta() {

        var captActive = $('.item.active').find('.carousel-caption').find('.carousel-caption-ImagePreview');
        var ImageMediaId = $("#photolost >.active >.row >.valuediv").find('#hdn_activitymedia_id').val();
        var found_Image = $.grep(ImageAttribute, function (v) {
            return v.Activity_Media_Id === ImageMediaId;
        });

        if (found_Image.length > 0) {
            var td = "<td>";
            var tr = "<tr>";
            var td_close = "</td>";
            var tr_close = "</tr>";
            var table_id = 'ImagePreviewTable_' + ImageMediaId;
            var tbltable = "<table id='"+table_id+"' class='table  table-striped table-bordered' style='width:90%;margin:0px;'><thead><th class='col-sm-4'>Attribute Type</th><th class='col-sm-8'>Attribute Value</th></thead><tbody>"
            captActive.empty();
            captActive.append('<h4><b>Image Details -</b></h4>');
            for (var i = 0; i < found_Image.length; i++) {
                //if (ImageAttribute[i].Activity_Media_Id == ImageMediaId) {
                tbltable = tbltable + tr + td + "<b>" + found_Image[i].AttributeType + "</b>" + td_close;
                tbltable = tbltable + td + found_Image[i].AttributeValue + td_close + tr_close;
                //}
            }
            tbltable = tbltable + "</tbody></table>";
           
            captActive.append(tbltable);
            //$('#ImagePreview').html(tbltable);
            if ($('#'+table_id).length > 0) {
               
                $('#'+table_id).DataTable({                    
                        "bPaginate": true,
                        "bProcessing": true,
                        "pageLength": 3,
                        "searching": false,
                        "bLengthChange": false,
                         "bInfo": false,
                         "pagingType": "numbers"
                });
            }
            
            $(".dataTables_paginate").addClass("dataTables_paginate_css"); 


        }
        else {
            var td = "<td>";
            var tr = "<tr>";
            var td_close = "</td>";
            var tr_close = "</tr>";
            var tbltable = "<table class='table  table-striped table-bordered' style='width:80%'><tbody>"
            var Naturalheight = $('.item.active').find('img').prop('naturalHeight');
            var Naturalwidth = $('.item.active').find('img').prop('naturalWidth');
            var captActive = $('.item.active').find('.carousel-caption').find('.carousel-caption-ImagePreview');
            captActive.empty();

            captActive.append('<h4><b>Image Details -</b></h4>');
            tbltable = tbltable + tr + td + "<p><b>Natural width</b> :" + td_close;
            tbltable = tbltable + td + Naturalwidth + " pixels " + td_close + tr_close;

            tbltable = tbltable + tr + td + "<p><b>Natural height</b> :" + td_close;
            tbltable = tbltable + td + Naturalheight + " pixels " + td_close + tr_close;

            tbltable = tbltable + tr + td + "<p><b>Dimensions</b> :" + td_close;
            tbltable = tbltable + td + Naturalwidth + " x " + Naturalheight + " pixels " + td_close + tr_close;

            //tbltable = tbltable + tr + td + "<p><b>Natural width</b> : " + Naturalwidth + " pixels </p>" + td_close;
            //tbltable = tbltable + tr + td + "<p><b>Natural height</b> : " + Naturalheight + " pixels </p>" + td_close;
            //tbltable = tbltable + tr + td + "<p><b>Dimensions</b> : " + Naturalwidth + " x " + Naturalheight + " pixels </p>" + td_close;

            tbltable = tbltable + "</table>";
            captActive.append(tbltable);
            //captActive.append('<h4><b>Image Details -</b></h4>');
            //captActive.append("<p><b>Natural width</b> : " + Naturalwidth + " pixels </p>");
            //captActive.append("<p><b>Natural height</b> : " + Naturalheight + " pixels </p>");
            //captActive.append("<p><b>Dimensions</b> : " + Naturalwidth + " x " + Naturalheight + " </p>");
            captActive.append("<br/>");
        }



    }
    // end
    function showImageModal() {
        $("#moImgGallery").modal('show');
    }

    function SaveImageReview() {
        var jsonObj = [];
        var item = {};
        item.Activity_Media_Id = $("#photolost >.active >.row >.valuediv").find('#hdn_activitymedia_id').val();
        item.IsWaterMark = $("#photolost >.active >.row >.valuediv").find('#ckh_Watermark').prop("checked");
        item.IsRelevent = $("#photolost >.active >.row >.valuediv").find('#cbk_relevent').prop("checked");
        item.IsDuplicate = $("#photolost >.active >.row >.valuediv").find('#chk_duplicate').prop("checked")
        item.Media_ReviewFeedback = $("#photolost >.active >.row >.valuediv").find("#txt_feedback").val();
        jsonObj.push(item);

        $.ajax({
            type: 'POST',
            url: '../../../Service/AddUpdateMediaInfoReview.ashx',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(jsonObj),
            responseType: "json",
            success: function (result) {

            },
            failure: function () {

            }
        });
    }

</script>


<asp:UpdatePanel ID="updMedia" runat="server">
    <ContentTemplate>
        <div class="col-lg-8">
            <h4 class="panel-title pull-left">
                <a data-toggle="collapse" data-parent="#searchResult" href="#collapseSearchResult">Activity Media Details (Total Count:
                                <asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label>)
                </a>
            </h4>
        </div>
        <div class="col-lg-4 pull-right">
            <div class="col-lg-3">
                <button class="btn btn-primary" onclick="GetMediaFor()">View Media</button>
            </div>
            <div class="col-lg-9">
                <div class="form-group pull-right ">
                    <div class="input-group" runat="server" id="divDropdownForEntries">
                        <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                        <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <div id="collapseSearchResult" class="panel-collapse collapse in">
            <div class="panel-body">
                <div id="dvMsg" runat="server" style="display: none;"></div>
                <asp:GridView ID="gvActMediaSearch" runat="server" AllowPaging="True" AllowCustomPaging="true"
                    EmptyDataText="No Media Found for search conditions" CssClass="table table-hover table-striped"
                    AutoGenerateColumns="false" OnPageIndexChanging="gvActMediaSearch_PageIndexChanging"
                    OnRowCommand="gvActMediaSearch_RowCommand" DataKeyNames="Activity_Media_Id" OnRowDataBound="gvActMediaSearch_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="Category" DataField="Category" />
                        <asp:BoundField HeaderText="Sub Category" DataField="SubCategory" />
                        <asp:BoundField HeaderText="Media Type" DataField="MediaType" />
                        <asp:BoundField HeaderText="Media Name" DataField="MediaName" />
                        <asp:BoundField HeaderText="Media_URL" DataField="Media_URL" />
                        <asp:ImageField DataImageUrlField="Media_URL" HeaderText="Thumbnail" ControlStyle-Width="70" ControlStyle-Height="70"></asp:ImageField>
                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                    Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Activity_Media_Id") %>' OnClientClick="showMediaModal()">
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--OnClientClick='<%# "showDetailsModal('\''"+ Convert.ToString(Eval("SupplierImportFile_Id")) + "'\'');" %>'--%>
                        <%--OnClientClicking='<%#string.Format("showDetailsModal('{0}');",Eval("SupplierImportFile_Id ")) %>'                                            
                                           <%-- showDetailsModal('<%# Eval("SupplierImportFile_Id")%>');--%>
                        <asp:TemplateField ShowHeader="false" HeaderStyle-CssClass="Info">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                                    CssClass="btn btn-default" CommandArgument='<%# Bind("Activity_Media_Id") %>'>
                                                    <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat" %>'></span>
                                                    <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
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

<div class="modal fade" id="moMedia" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">

            <div class="modal-header">
                <h4 class="modal-title">Attribute Details</h4>
            </div>

            <div class="modal-body">
                <asp:UpdatePanel ID="upMediaAddEdit" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnFlag" runat="server" Value="" EnableViewState="false" />
                        <asp:FormView ID="frmMedia" runat="server" DataKeyNames="Activity_Media_Id" DefaultMode="Insert" OnItemCommand="frmMedia_ItemCommand">
                            <InsertItemTemplate>
                            </InsertItemTemplate>
                        </asp:FormView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>


<!--Start Image Gallery-->
<div class="modal fade bs-example-modal-lg x-lg" tabindex="-1" role="dialog" aria-hidden="true" id="moImgGallery">
    <div class="modal-dialog modal-lg x-lg">
        <div class="modal-content" style="width: auto; min-height: 600px; overflow: hidden">

            <div class="modal-header">
                <h4 class="modal-title">Image Details: <span>
                    <strong>Product ID:</strong></span>
                    <strong>
                        <asp:Label ID="Product_Id" runat="server"></asp:Label></strong></h4>
            </div>

            <div id="carousel-example-generic" class="carousel slide" data-ride="carousel" data-interval="false" style="width: auto; min-height: 400px; overflow: hidden">

                <!-- Wrapper for slides -->
                <div class="carousel-inner flex-container" id="photolost">
                    <!-- Dynamic Image Content from Ajax call -->


                </div>

                <!-- Controls left and right buttons-->
                <a class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">
                    <span class="CustomStyle glyphicon glyphicon-chevron-left"></span>
                </a>
                <a class="right carousel-control" href="#carousel-example-generic" role="button" data-slide="next">
                    <span class="CustomStyle glyphicon glyphicon-chevron-right"></span>
                </a>
            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>

        </div>
    </div>
</div>
<!--end-->



