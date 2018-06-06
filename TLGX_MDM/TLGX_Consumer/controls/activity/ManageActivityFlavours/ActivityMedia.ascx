<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityMedia.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.ActivityMedia" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        $('#carousel-example-generic').bind('slid.bs.carousel', function (e) {
            //after slide changed
            getMeta();
        });
    });

    function renderImageDeatils(result) {
        if (result != null && result.length > 0) {
            for (var i = 0; i < result.length; i++) {
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
                imgColDiv.append(" <img class='img-responsive' src= '" + result[i].Media_URL + "' alt='Not Found'/>");

                imgRowDiv.append(imgColDiv);

                var capColDiv = $('<div class="col-sm-3"/>');
                var d = $("<div class='carousel-caption'/>");
                
                d.append('<h4><b>Details by Supplier  - </b></h4>');
                d.append("<p><b>Caption</b> : " + (result[i].Media_Caption == null ? "Not given by supplier" : result[i].Media_Caption) + "</p>");
                d.append("<p><b>Name</b> : " + (result[i].MediaName == null ? "Not given by supplier" : result[i].MediaName) + "</p>");
                d.append("<p><b>Description</b> : " + (result[i].Description == null ? "Not given by supplier" : result[i].Description) + "</p>");
                d.append("<p><b>Width</b>  : " + (result[i].Media_Width == null ? "Not given by supplier" : result[i].Media_Width) + "</p>");
                d.append("<p><b>Height</b>  : " + (result[i].Media_Height == null ? "Not given by supplier" : result[i].Media_Height) + "</p>");
                d.append("<p><b>Media Type</b>  : " + (result[i].MediaType == null ? "Not given by supplier" : result[i].MediaType) + "</p>");
                d.append("<br/>");
                d.append("<div class='carousel-caption-imagedetails'/>");

                capColDiv.append(d);

                imgRowDiv.append(capColDiv);

                div.append(imgRowDiv);

                $("#photolost").append(div);
            }
            getMeta();
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
                renderImageDeatils(result);
            }
        });
        showImageModal();
    }
    // GET IMAGE DETAILS FROM SRC

    function getMeta() {
        var Naturalheight = $('.item.active').find('img').prop('naturalHeight');
        var Naturalwidth = $('.item.active').find('img').prop('naturalWidth');
        var captActive = $('.item.active').find('.carousel-caption').find('.carousel-caption-imagedetails');
        captActive.empty();
        captActive.append('<h4><b>Image Details -</b></h4>');
        captActive.append("<p><b>Natural width</b> : " + Naturalwidth + " pixels </p>");
        captActive.append("<p><b>Natural height</b> : " + Naturalheight + " pixels </p>");
        captActive.append("<p><b>Dimensions</b> : " + Naturalheight + " x " + Naturalwidth + " </p>");
    }
    // end
    function showImageModal() {
        $("#moImgGallery").modal('show');
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


<!--Start Gallery-->
<div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-hidden="true" id="moImgGallery">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">Image Details</h4>
            </div>
            <%--   <div class="modal-body" >--%>

            <div id="carousel-example-generic" class="carousel slide" data-ride="carousel" data-interval="false" style="width: auto; height: 500px;">

                <!-- Wrapper for slides -->
                <div class="carousel-inner" id="photolost">
                    <!-- Dynamic Image Content from Ajax call -->
                </div>

                <!-- Controls -->
                <a class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">
                    <span class="glyphicon glyphicon-chevron-left"></span>
                </a>
                <a class="right carousel-control" href="#carousel-example-generic" role="button" data-slide="next">
                    <span class="glyphicon glyphicon-chevron-right"></span>
                </a>
            </div>
            <%-- </div>--%>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
<!--end-->



