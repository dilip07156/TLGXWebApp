<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="media.ascx.cs" Inherits="TLGX_Consumer.controls.hotel.media" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<script src="../../Scripts/jquery.ae.image.resize.min.js"></script>--%>
<style type="text/css">
    .is-breakable {
        word-break: break-all;
    }

    @media (min-width: 700px) {
        .modal-xl {
            width: 70%;
            max-width: 1200px;
        }
    }
</style>

<script lang="javascript" type="text/javascript">

    $(document).ready(ajustamodal);
    $(window).resize(ajustamodal);
    function ajustamodal() {
        var altura = $(window).height() - 170; //value corresponding to the modal heading + footer
        $(".modal-scroll").css({ "height": altura, "overflow-y": "auto" });
    }

    function axuploadComplete(sender, args) {
        $('#<%=txtMediaPath.ClientID%>').val('<%= MediaAbsPath %>' + args.get_fileName());
        var _localMediaAbsUrl = '<%= MediaAbsUrl %>' + args.get_fileName();
        $('#<%=txtMediaURL.ClientID%>').val(_localMediaAbsUrl);
        $('#<%=hdnMediaName.ClientID%>').val(args.get_fileName());
        var imgDisplay = $('#<%=imgMedia.ClientID%>');
        imgDisplay.attr("src", _localMediaAbsUrl);

       <%-- var imgDisplayThumbnail = $('#<%=imgMediaThumbnail.ClientID%>');
        imgDisplayThumbnail.attr("src", _localMediaAbsUrl);
        $(".resizeme").aeImageResize({ height: 60, width: 90 });--%>

    }

    function showMediaModal() {
        //alert("Hi");
        $("#moMedia").modal('show');
    }
    function closeMediaModal() {
        $("#moMedia").modal('hide');
    }

    function ddlFileMasterChanged(ddl) {
        var FileMaster = ddl.options[ddl.selectedIndex].text;
        var myVal = document.getElementById("<%=vldddlRoomCategory.ClientID%>");
        if (FileMaster == 'Room Type') {
            ValidatorEnable(myVal, true);
        }
        else {
            ValidatorEnable(myVal, false);
        }
    }
    function InIEvent() {
        ClosePopup();
    }
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
    function ClosePopup() {
        var hv = $('#MainContent_media_hdnFlag').val();
        if (hv == "true") {
            closeMediaModal();
            $('#MainContent_media_hdnFlag').val("false");
        }
    }
    // Read a page's GET URL variables and return them as an associative array.
    function getUrlVars() {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    }
    function Custvalidate(sender, args) {
        var hdnCustomValidate = $('#hdnCustomValidate').val();
        if (hdnCustomValidate == "true") {
            args.IsValid = false;
        }
        else {
            args.IsValid = true;
        }
    }
    //Ajax Call to get media position
    function CheckMediaPositionDuplicate() {
        var MediaPostion = $('#MainContent_media_txtMediaPosition').val();
        var MediaID = $('#hdnEditMediaId').val();
        var UrlVars = getUrlVars();
        var Accommodation_Id = getUrlVars()["Hotel_Id"];
        if (MediaID == null)
            MediaID = "0";
        if (MediaPostion != null && Accommodation_Id != null) {
            var url_t = '<%=ConfigurationManager.AppSettings["MDMSVCUrl"] %>'
            url_t = url_t + '<%=ConfigurationManager.AppSettings["CheckMediaPositionDuplicateforAccommodation_ajax"] %>';
            var act_url = url_t + Accommodation_Id + '/' + MediaPostion + '/' + MediaID;
            var mediapositioinError = $('#MainContent_media_mediapositioinError');
            mediapositioinError.css("display", "none");
            //Ajax Call
            $.ajax({
                url: act_url,
                type: "GET",
                Accept: "application/json",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                async: false,
                data: {},
                success: function (result) {
                    if (result) {
                        mediapositioinError.text('Media Position is already exist.Please give different media positioin.');
                        mediapositioinError.css("display", "block");
                        $('#hdnCustomValidate').val(true);
                    }
                    else {
                        mediapositioinError.css("display", "none");
                        $('#hdnCustomValidate').val(false);
                    }

                },
                failure: function () {
                    $('#hdnCustomValidate').val(true);
                }

            });
        }
    }
</script>

<asp:UpdatePanel ID="upAccoMedia" runat="server">
    <ContentTemplate>

        <div class="panel panel-default">
            <div class="panel-heading">
                Media Details&nbsp;
            <asp:Button ID="btnAddMedia" runat="server" Text="Add New Media" CssClass="btn btn-primary btn-sm" OnClick="btnAddMedia_Click" OnClientClick="showMediaModal();" />
            </div>
            <div class="panel-body">
                <div id="dvMsg" runat="server" style="display: none;"></div>
                <asp:GridView ID="gvMedia" runat="server" AutoGenerateColumns="False" DataKeyNames="Accommodation_Media_Id" CssClass="table table-hover table-striped" EmptyDataText="No Media defined." OnRowCommand="grdMedia_RowCommand" OnRowDataBound="grdMedia_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="ValidFrom" HeaderText="Valid From" SortExpression="ValidFrom" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="ValidTo" HeaderText="Valid To" SortExpression="ValidTo" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
                        <asp:BoundField DataField="SubCategory" HeaderText="SubCategory" SortExpression="SubCategory" />
                        <asp:BoundField DataField="MediaType" HeaderText="Media Type" SortExpression="MediaType" />
                        <%--<asp:BoundField DataField="FileFormat" HeaderText="File Format" SortExpression="FileFormat" />--%>
                        <asp:BoundField DataField="MediaFileMaster" HeaderText="File Master" SortExpression="MediaFileMaster" />
                        <asp:BoundField DataField="RoomCategory" HeaderText="Room Category" SortExpression="RoomCategory" />
                        <%-- <asp:BoundField DataField="Media_Path" HeaderText="Media Path" SortExpression="Media_Path" />
                        <asp:BoundField DataField="Media_URL" HeaderText="Media URL" SortExpression="Media_URL" />--%>
                        <asp:ImageField DataImageUrlField="Media_URL" HeaderText="Preview Image" ControlStyle-Width="70" ControlStyle-Height="70"></asp:ImageField>
                        <asp:BoundField DataField="Media_Position" HeaderText="Media Position" SortExpression="Media_Position" />
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                        <asp:BoundField DataField="MediaID" HeaderText="Media ID" SortExpression="MediaID" />
                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                    Enabled='<%# Eval("IsActive") %>' CommandArgument='<%# Bind("Accommodation_Media_Id") %>' OnClientClick="showMediaModal();">
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                                    CssClass="btn btn-default" CommandArgument='<%# Bind("Accommodation_Media_Id") %>'>
                                         <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'</span>
                                        <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete"   %>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>

<div class="modal fade" id="moMedia" role="dialog" data-backdrop="static" data-keyboard="false">
    <div class="modal-dialog modal-xl">
        <div class="modal-content">
            <div class="modal-header">
                <form class="form-inline">
                    <label class="modal-title"><strong>Media Details</strong></label>
                    <asp:UpdateProgress ID="updProgress" AssociatedUpdatePanelID="UpMediaModal" runat="server">
                        <ProgressTemplate>
                            <img src="../../images/ajax-loader-blue.gif" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </form>
                <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
            </div>

            <div class="modal-body modal-scroll">

                <asp:UpdatePanel ID="UpMediaModal" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnFlag" runat="server" Value="" EnableViewState="false" />
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="vldgrpMedia" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6">

                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        Fields
                                    </div>
                                    <div class="panel-body">

                                        <div class="form-group row" id="dvBrowseMedia" runat="server">
                                            <label class="control-label col-sm-4" for="axFileUpload">
                                                Browse Media
                                                <img id="imgLoader" runat="server" src="../../images/ajax-loader-blue.gif" />
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:HiddenField ID="hdnMediaName" runat="server" Value="" />
                                                <cc1:AsyncFileUpload ID="axAsyncFileUpload" runat="server" OnUploadedComplete="axAsyncFileUpload_UploadedComplete"
                                                    OnClientUploadComplete="axuploadComplete" UploaderStyle="Traditional" ThrobberID="imgLoader" Width="300px" Enabled="true" />

                                                <%-- <asp:FileUpload ID="fuMedia" runat="server" onchange="ShowImagePreview(this);" ClientIDMode="Static" EnableViewState="true" />--%>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="control-label-mand col-sm-4" for="txtValidFrom">
                                                Valid From
                                                <asp:RequiredFieldValidator ID="vldtxtValidFrom" runat="server" ControlToValidate="txtValidFrom" ErrorMessage="Please select valid from date" Text="*" ValidationGroup="vldgrpMedia" CssClass="text-danger"></asp:RequiredFieldValidator>
                                            </label>
                                            <div class="col-sm-8">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtValidFrom" runat="server" class="form-control" />
                                                    <span class="input-group-btn">
                                                        <button class="btn btn-default" type="button" id="iMediaFrom">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </button>
                                                    </span>
                                                </div>
                                                <cc1:CalendarExtender ID="txtValidFrom_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtValidFrom" PopupButtonID="iMediaFrom"></cc1:CalendarExtender>
                                                <cc1:FilteredTextBoxExtender ID="axfte_txtValidFrom" runat="server" FilterType="Numbers, Custom" ValidChars="/"
                                                    TargetControlID="txtValidFrom" />

                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="control-label-mand col-sm-4" for="txtValidTo">
                                                Valid To
                                                <asp:RequiredFieldValidator ID="vldtxtValidTo" runat="server" ControlToValidate="txtValidTo" ErrorMessage="Please select valid to date" Text="*" ValidationGroup="vldgrpMedia" CssClass="text-danger"></asp:RequiredFieldValidator>
                                            </label>
                                            <div class="col-sm-8">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtValidTo" runat="server" class="form-control" />
                                                    <span class="input-group-btn">
                                                        <button class="btn btn-default" type="button" id="iMediaTo">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </button>
                                                    </span>
                                                </div>
                                                <cc1:CalendarExtender ID="txtValidTo_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtValidTo" PopupButtonID="iMediaTo"></cc1:CalendarExtender>
                                                <cc1:FilteredTextBoxExtender ID="axfte_txtValidTo" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtValidTo" />
                                                <asp:CompareValidator ID="vldCmpDateFromTo" runat="server" ErrorMessage="To date can't be less than from date." ControlToCompare="txtValidFrom" ControlToValidate="txtValidTo" ValidationGroup="vldgrpMedia" Text="*" CssClass="text-danger" Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="control-label-mand col-sm-4" for="ddlFileCategory">
                                                File Category
                                                <asp:RequiredFieldValidator ID="vldddlFileCategory" runat="server" ControlToValidate="ddlFileCategory" InitialValue="0" ErrorMessage="Please select file category" Text="*" ValidationGroup="vldgrpMedia" CssClass="text-danger"></asp:RequiredFieldValidator>
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlFileCategory" runat="server" class="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="control-label-mand col-sm-4" for="ddlSubCategory">
                                                Sub Category
                                                <asp:RequiredFieldValidator ID="vldddlSubCategory" runat="server" ControlToValidate="ddlSubCategory" InitialValue="0" ErrorMessage="Please select file sub category" Text="*" ValidationGroup="vldgrpMedia" CssClass="text-danger"></asp:RequiredFieldValidator>
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlSubCategory" runat="server" class="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="control-label-mand col-sm-4" for="ddlMediaType">
                                                Media Type
                                                <asp:RequiredFieldValidator ID="vldddlMediaType" runat="server" ControlToValidate="ddlMediaType" InitialValue="0" ErrorMessage="Please select media type" Text="*" ValidationGroup="vldgrpMedia" CssClass="text-danger"></asp:RequiredFieldValidator>
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlMediaType" runat="server" class="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                </asp:DropDownList>


                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="control-label-mand col-sm-4" for="ddlFileMaster">
                                                File Master
                                                <asp:RequiredFieldValidator ID="vldddlFileMaster" runat="server" ControlToValidate="ddlFileMaster" InitialValue="0" EErrorMessage="Please select file master" Text="*" ValidationGroup="vldgrpMedia" CssClass="text-danger"></asp:RequiredFieldValidator>
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlFileMaster" runat="server" class="form-control" AppendDataBoundItems="true" onchange="ddlFileMasterChanged(this);">
                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="ddlRoomCategory">
                                                Room Category
                                                <asp:RequiredFieldValidator ID="vldddlRoomCategory" runat="server" ControlToValidate="ddlRoomCategory"
                                                    InitialValue="0" ErrorMessage="Please select room category" Text="*" ValidationGroup="vldgrpMedia"
                                                    CssClass="text-danger" Enabled="false"></asp:RequiredFieldValidator>
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlRoomCategory" runat="server" class="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="txtMediaPosition">
                                                Media Position
                                                <asp:RequiredFieldValidator ID="rfvtxtMediaPosition" runat="server" ControlToValidate="txtMediaPosition"
                                                    ErrorMessage="Please entry a valid media position" Text="*" ValidationGroup="vldgrpMedia"
                                                    CssClass="text-danger"></asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="cvMediaPosition" runat="server" ClientIDMode="Static"
                                                    ErrorMessage="Media Position is already exist.Please give different media positioin."
                                                    Text="*"
                                                    ControlToValidate="txtMediaPosition"
                                                    ClientValidationFunction="Custvalidate" ValidationGroup="vldgrpMedia"
                                                    CssClass="text-danger" />
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtMediaPosition" runat="server" class="form-control" MaxLength="3" />
                                                <cc1:FilteredTextBoxExtender ID="axfte_txtMediaPosition" runat="server" FilterType="Numbers" TargetControlID="txtMediaPosition" />
                                                <label runat="server" id="mediapositioinError" style="display: none;" class="text-danger"></label>
                                                <asp:HiddenField ID="hdnCustomValidate" runat="server" ClientIDMode="Static" />
                                                <asp:HiddenField ID="hdnEditMediaId" runat="server" ClientIDMode="Static" />
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="txtMediaPath">Media Path</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtMediaPath" runat="server" class="form-control" ReadOnly="true" />
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="txtMediaURL">Media URL</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtMediaURL" runat="server" class="form-control" ReadOnly="true" />
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="txtDescription">Description</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtDescription" runat="server" class="form-control" />
                                            </div>
                                        </div>

                                        <div class="form-group pull-right row">
                                            <div class="col-sm-4"></div>
                                            <div class="col-sm-4">
                                                <asp:LinkButton ID="btnSaveMedia" runat="server" CssClass="btn btn-primary btn-sm pull-right" OnClick="btnSaveMedia_Click" OnClientClick="return CheckMediaPositionDuplicate();" CommandName="AddMedia" ValidationGroup="vldgrpMedia" CausesValidation="true">Add New Media</asp:LinkButton>
                                            </div>

                                            <div class="col-sm-4">
                                                <asp:LinkButton ID="btnMediaReset" runat="server" CssClass="btn btn-primary btn-sm pull-right" OnClick="btnMediaReset_Click" CommandName="ResetMedia">Reset</asp:LinkButton>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                            </div>

                            <div class="col-lg-6">

                                <div class="panel panel-default">
                                    <div class="panel-heading">Media Preview</div>
                                    <div class="panel-body">
                                        <asp:Image ID="imgMedia" runat="server" CssClass="img-responsive" />
                                    </div>
                                </div>

                                <div id="divMediaAttributes" runat="server">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">Media Attributes (File System Attributes will not be editable)</div>
                                        <div class="panel-body">

                                            <div class="form-group row">
                                                <div class="col-sm-12">
                                                    <div id="dvMsgMediaAttribute" runat="server" style="display: none;"></div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-12">
                                                    <asp:ValidationSummary ID="vlsSummAttr" runat="server" ValidationGroup="vldgrpMediaAttribute" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-12">
                                                    <div id="dvMsgAttribute" runat="server" style="display: none;"></div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="control-label-mand col-sm-6" for="txtAttributeType">Type</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlAttributeType" runat="server" class="form-control" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="vldddlAttributeType" runat="server" ControlToValidate="ddlAttributeType" InitialValue="0"
                                                        ErrorMessage="Please select Attribute Type" Text="*" ValidationGroup="vldgrpMediaAttribute" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="control-label-mand col-sm-6" for="txtAttributeValue">Value</label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtAttributeValue" runat="server" class="form-control" />
                                                    <asp:RequiredFieldValidator ID="vldtxtAttributeValue" runat="server" ControlToValidate="txtAttributeValue"
                                                        ErrorMessage="Please enter attribute value" Text="*" ValidationGroup="vldgrpMediaAttribute" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="form-group pull-right row">
                                                <div class="col-sm-6"></div>
                                                <div class="col-sm-3">
                                                    <asp:LinkButton ID="btnSaveMediaAttributes" runat="server" CssClass="btn btn-primary btn-sm pull-right" OnClick="btnSaveMediaAttributes_Click" CommandName="AddAttributes" ValidationGroup="vldgrpMediaAttribute" CausesValidation="true">Add New Media Attribute</asp:LinkButton>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:LinkButton ID="btnResetMediaAttributes" runat="server" CssClass="btn btn-primary btn-sm pull-right" OnClick="btnResetMediaAttributes_Click" CommandName="ResetAttributes">Reset</asp:LinkButton>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <asp:GridView ID="gvMediaAttributes" runat="server" AllowCustomPaging="true" AllowPaging="true" AutoGenerateColumns="False"
                                                    DataKeyNames="Accomodation_Media_Attributes_Id" CssClass="table table-hover table-bordered" EmptyDataText="No Media Attributes defined for this media"
                                                    OnRowCommand="gvMediaAttributes_RowCommand" OnRowDataBound="gvMediaAttributes_RowDataBound" OnPageIndexChanging="gvMediaAttributes_PageIndexChanging">
                                                    <Columns>
                                                        <asp:BoundField DataField="AttributeType" HeaderText="MediaType" SortExpression="MediaType">
                                                            <ItemStyle CssClass="is-breakable"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="AttributeValue" HeaderText="MediaName" SortExpression="MediaName">
                                                            <ItemStyle CssClass="is-breakable"></ItemStyle>
                                                        </asp:BoundField>

                                                        <asp:TemplateField ShowHeader="false">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                                                                    Enabled='<%# Convert.ToBoolean(Eval("IsActive")) && !Convert.ToBoolean(Eval("IsSystemAttribute")) %>' CommandArgument='<%# Bind("Accomodation_Media_Attributes_Id") %>'>
                                                            <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp; Edit
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ShowHeader="false">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("IsActive").ToString() == "True" ? "SoftDelete" : "UnDelete"   %>'
                                                                    CssClass="btn btn-default" CommandArgument='<%# Bind("Accomodation_Media_Attributes_Id") %>'>
                                                             <span aria-hidden="true" class='<%# Eval("IsActive").ToString() == "True" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat"   %>'></span>
                                                             <%# Eval("IsActive").ToString() == "True" ? "Delete" : "UnDelete" %>
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

