﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="keywordManager.ascx.cs" Inherits="TLGX_Consumer.controls.keywords.keywordManager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="../../Content/bootstrap-select.min.css" rel="stylesheet" />
<style>
    @media (min-width: 768px) {
        .modal-xl {
            width: 80%;
            max-width: 1200px;
        }
    }
</style>
<script type="text/javascript">

    $(document).ready(ajustamodal);
    $(window).resize(ajustamodal);
    function ajustamodal() {
        var altura = $(window).height() - 120; //value corresponding to the modal heading + footer
        $(".modal-scroll").css({ "height": altura, "overflow-y": "auto" });
    }

    var count;
    function showModal() {
        $("#moKeywordMapping").modal('show');
        count = 1;
        $('.AttributeLevel_Hide').collapse('hide');
        $('.AttributeLevel_RoomInfo_Hide').collapse('hide');
    }
    function closeModal() {
        $("#moKeywordMapping").modal('hide');
    }
    function SetGlyphicon(control) {
        var selectedtext = control.options[control.selectedIndex].innerHTML;
        $('#MainContent_keywordManager_spanglyphicon').removeClass().addClass('glyphicon').addClass("glyphicon-" + selectedtext); //.addClass('input-group-addon')
    }

    function EnableDisableValidationForIcons(chk) {
        var valName = document.getElementById("<%=rfvicondropdownmenu.ClientID%>");
        ValidatorEnable(valName, chk.checked);

        chk.checked == true ? $('#MainContent_keywordManager_dvAttrDetails').css("display", "block") : $('#MainContent_keywordManager_dvAttrDetails').css("display", "none");
    }

    function ValidateCheckBoxList(sender, args) {
        var checkBoxList = document.getElementById("<%=chklistEntityFor.ClientID %>");
        var checkboxes = checkBoxList.getElementsByTagName("input");
        var isValid = false;
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].checked) {
                isValid = true;
                break;
            }
        }
        args.IsValid = isValid;
    }

    function hideshowAttrLvl(ctrl) {
        //debugger;
        //Saves in a variable the wanted div
        var selector = '.AttributeLevel_' + $('#' + ctrl).find("option:selected").text().replace(' ', '');

        //hide all elements
        $('.AttributeLevel_Hide').collapse('hide');

        //show only element connected to selected option
        $(selector).collapse('show');
    };

    function hideshowAttrLvlRoomSchema(ctrl) {
        //Saves in a variable the wanted div
        var selector = '.AttributeLevel_RoomInfo_' + $('#' + ctrl).find("option:selected").text().replace(' ', '');

        //hide all elements
        $('.AttributeLevel_RoomInfo_Hide').collapse('hide');

        //show only element connected to selected option
        $(selector).collapse('show');
    };

</script>

<asp:UpdatePanel ID="updatepnl" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hdnFieldTotalTextboxes" Value="1" runat="server" />
        <asp:HiddenField ID="hdnAliasId" runat="server" Value="" />
        <div class="container">
            <div class="panel-group" id="accordion">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Search Filters</a>
                        </h4>
                    </div>
                    <div id="collapseSearch" class="panel-collapse collapse in">
                        <div class="panel-body">
                            <form class="row form-horizontal">
                                <div class="col-lg-3">

                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="txtKeyword">Keyword</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtKeyword" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="txtAlias">Alias</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtAlias" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="rdoBtnAttribute">Attribute</label>
                                        <div class="col-sm-8">
                                            <label class="radio-inline">
                                                <input type="radio" id="rdoIsAttributeAll" runat="server" name="IsAttribute"><b>All</b></label>
                                            <label class="radio-inline">
                                                <input type="radio" id="rdoIsAttributeYes" runat="server" name="IsAttribute"><b>Yes</b></label>
                                            <label class="radio-inline">
                                                <input type="radio" id="rdoIsAttributeNo" runat="server" name="IsAttribute"><b>No</b></label>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="ddlStatus">Status</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack="true">
                                                <asp:ListItem Text="---ALL---" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-lg-3">
                                    <div class="form-group row">
                                        <label class="control-label col-sm-4" for="chkListEntityForSearch">
                                            Entity For
                                        </label>
                                        <div class="col-sm-8">
                                            <div class="form-horizontal">
                                                <fieldset class="form-group">
                                                    <div class="col-sm-12">
                                                        <asp:CheckBoxList ID="chkListEntityForSearch" runat="server"></asp:CheckBoxList>
                                                    </div>
                                                </fieldset>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-3">

                                    <div class="form-group row">
                                        <div class="col-sm-3">
                                            <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary btn-sm pull-right" Text="Search" OnClick="btnSearch_Click" />
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Button runat="server" ID="btnReset" CssClass="btn btn-primary btn-sm pull-right" Text="Reset" OnClick="btnReset_Click" />
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Button runat="server" ID="btnAddNew" CssClass="btn btn-primary btn-sm pull-right" Text="Add New" OnClick="btnAddNew_Click" OnClientClick="showModal();" />
                                            &nbsp;&nbsp;<br />
                                        </div>                                         
                                        <div class="col-sm-4">
                                            <asp:Button runat="server" ID="btnReRun_Master" CssClass="btn btn-primary btn-sm pull-right" Text="ReRunMaster" OnClick="btnReRun_Master_Click"  />
                                        </div>
                                        <div class="col-sm-4">
                                             <asp:Button runat="server" ID="btnReRun_Supplier" CssClass="btn btn-primary btn-sm pull-right" Text="ReRunSupplier" OnClick="btnReRun_Supplier_Click" />
                                         </div>
                                    </div>
                                    
                                   

                                    <div class="form-group row">
                                        <div class="panel-group">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <h4 class="panel-title">
                                                        <strong>Wildcard keywords special operation</strong>
                                                        <p>Below keywords can be added with values for respective action.</p>                                                       
                                                    </h4>
                                                </div>
                                                <ul>
                                                    <li>##_REMOVE_WORD_FROM_START</li>
                                                    <li>##_REMOVE_WORD_FROM_END</li>
                                                    <li>##_REMOVE_WORD_FROM_STRING</li>
                                                    <li>##_REMOVE_ANYWHERE_IN_STRING</li>
                                                    <li>##_REMOVE_CONTENTS_IN_BRACKETS (YES/NO)</li>
                                                    <li>##_REPLACE_NUMBERS_WITH_WORDS (YES/NO)</li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </form>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div id="dvMsg" runat="server" style="display: none;"></div>
                </div>
            </div>
        </div>

        <div class="container">
            <div class="panel-group" id="accordion1">
                <div class="panel panel-default">
                    <div class="panel-heading clearfix">
                        <div class="row">
                            <div class="col-md-6">
                                <h4 class="panel-title pull-left">
                                    <a data-toggle="collapse" data-parent="#accordion1" href="#collapseSearchResult">Search Results (Total Count:
                                <asp:Label ID="lblTotalCount" runat="server" Text="0"></asp:Label>)</a>
                                </h4>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group pull-right">
                                    <div class="input-group">
                                        <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                                        <asp:DropDownList ID="ddlShowEntries" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlShowEntries_SelectedIndexChanged">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="25">25</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="35">35</asp:ListItem>
                                            <asp:ListItem Value="40">40</asp:ListItem>
                                            <asp:ListItem Value="45">45</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div id="collapseSearchResult" class="panel-collapse collapse in">
                        <div class="panel-body">
                            <div class="col-lg-12">

                                <asp:GridView ID="gvSearchResult" runat="server"
                                    EmptyDataText="No data for search conditions" CssClass="table table-bordered table-hover" AllowPaging="true" AllowCustomPaging="True" AutoGenerateColumns="false"
                                    DataKeyNames="Keyword_Id" OnRowCommand="gvSearchResult_RowCommand" OnPageIndexChanging="gvSearchResult_PageIndexChanging"
                                    HeaderStyle-CssClass="info" OnRowDataBound="gvSearchResult_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="Keyword" HeaderText="Keyword" />
                                        <asp:BoundField DataField="Attribute" HeaderText="Attribute" />
                                        <asp:BoundField DataField="Sequence" HeaderText="Sequence" />
                                        <asp:TemplateField ShowHeader="true">
                                            <HeaderTemplate>
                                                Icon
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("Icon") %>' ID="lblIconText"></asp:Label>
                                                <span aria-hidden="true" class="glyphicon glyphicon-<%# Eval("Icon") %>"></span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="EntityFor" HeaderText="EntityFor" />

                                        <asp:BoundField DataField="Status" HeaderText="Status" />

                                        <asp:TemplateField ShowHeader="true">
                                            <HeaderTemplate>
                                                Alias
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <asp:DataList ID="lstAlias" runat="server" DataSource='<%# Bind("Alias") %>'
                                                    RepeatLayout="Flow" RepeatDirection="Horizontal" CssClass="table table-bordered table-striped">
                                                    <ItemTemplate>
                                                        <span class='<%# Eval("Status").ToString() == "ACTIVE" ? "label label-primary form-control" : "label label-default form-control" %>' style="font-size: smaller">
                                                            <%# Eval("Value") %>
                                                        </span>
                                                    </ItemTemplate>
                                                </asp:DataList>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false" HeaderStyle-CssClass="Info">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="EditKeyWordMgmr" CommandArgument='<%# Bind("Keyword_Id")%>' OnClientClick="showModal();" CssClass="btn btn-default">
                                                    <span aria-hidden="true" class="glyphicon glyphicon-pencil"></span>
                                                    Edit
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false" HeaderStyle-CssClass="Info">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("Status").ToString() == "ACTIVE" ? "SoftDelete" : "UnDelete"   %>'
                                                    CssClass="btn btn-default" CommandArgument='<%# Bind("Keyword_Id") %>'>
                                                    <span aria-hidden="true" class='<%# Eval("Status").ToString() == "ACTIVE" ? "glyphicon glyphicon-remove" : "glyphicon glyphicon-repeat" %>'></span>
                                                    <%# Eval("Status").ToString() == "ACTIVE" ? "Delete" : "UnDelete"   %>
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

<!-- Add MODAL -->
<div class="modal fade" id="moKeywordMapping" role="dialog">
    <div class="modal-dialog modal-xl">
        <div class="modal-content">

            <div class="modal-header">
                <h4 class="panel-title">Add Update Keyword and Alias
                </h4>
            </div>

            <div class="modal-body modal-scroll">
                <asp:UpdatePanel ID="pnlupdate" runat="server">
                    <ContentTemplate>

                        <div class="row">
                            <div class="col-md-12">
                                <div id="dvMsgAlias" runat="server" style="display: none;"></div>
                            </div>
                        </div>

                        <div class="row ">
                            <div class="col-md-12">
                                <asp:ValidationSummary ID="vlsSummKey" runat="server" ValidationGroup="vldgrpKeyword" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                            </div>
                        </div>

                        <div class="row ">
                            <div class="col-md-12">
                                <asp:ValidationSummary ID="vlsSummAliasNew" runat="server" ValidationGroup="vldgrpAliasNew" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                            </div>
                        </div>

                        <div class="row ">
                            <div class="col-md-12">
                                <asp:ValidationSummary ID="vlsSummAliasEdit" runat="server" ValidationGroup="vldgrpAliasEdit" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                            </div>
                        </div>

                        <div class="row">
                            <%--<div class="col-md-6">
                                <label class="control-label">Total Alias Records :</label>
                                <asp:Label runat="server" ID="lblTotalAlias" Text="0" CssClass="control-label"></asp:Label>
                            </div>--%>

                            <div class="col-md-12">
                                <div class="form-group pull-right">
                                    <div class="input-group">
                                        <label class="input-group-addon" for="ddlShowEntries">Page Size</label>
                                        <asp:DropDownList ID="ddlShowEntriesAlias" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlShowEntriesAlias_SelectedIndexChanged">
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="25">25</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <div class="panel panel-default">
                                    <div class="panel-heading"><strong>Keyword</strong></div>
                                    <div class="panel-body">

                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="chklistEntityFor">
                                                Entity For
                                                 <asp:CustomValidator ID="cusChkListEntity" ClientValidationFunction="ValidateCheckBoxList" runat="server"
                                                     ErrorMessage="Please select atleast one Entity" Text="*" ValidationGroup="vldgrpKeyword" CssClass="text-danger" />
                                            </label>
                                            <div class="col-sm-8">
                                                <div class="form-horizontal">
                                                    <fieldset class="form-group">
                                                        <div class="col-sm-12">
                                                            <asp:CheckBoxList ID="chklistEntityFor" runat="server"></asp:CheckBoxList>
                                                        </div>
                                                    </fieldset>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="txtAddNewKeyword">
                                                Keyword
                                                <asp:RequiredFieldValidator ID="vldtxtKeyword" runat="server" ControlToValidate="txtAddNewKeyword"
                                                    ErrorMessage="Keyword cannot be empty." Text="*" ValidationGroup="vldgrpKeyword" CssClass="text-danger">
                                                </asp:RequiredFieldValidator>
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtAddNewKeyword" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <asp:HiddenField ID="hdnKeywordId" runat="server" />
                                        </div>

                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="txtKeywordSequence">
                                                Sequence
                                                <asp:RequiredFieldValidator ID="vldtxtKeywordSequence" runat="server" ControlToValidate="txtKeywordSequence"
                                                    ErrorMessage="Keyword Sequence cannot be empty." Text="*" ValidationGroup="vldgrpKeyword" CssClass="text-danger">
                                                </asp:RequiredFieldValidator>
                                                <cc1:FilteredTextBoxExtender ID="axfte_txtKeywordSequence" runat="server" FilterType="Numbers" TargetControlID="txtKeywordSequence" />
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtKeywordSequence" runat="server" CssClass="form-control" MaxLength="3"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="chkNewKeywordAttribute">Attribute</label>
                                            <div class="col-sm-8">
                                                <asp:CheckBox ID="chkNewKeywordAttribute" onclick="EnableDisableValidationForIcons(this);" runat="server" />
                                                <%--data-toggle="collapse" data-target="#MainContent_keywordManager_dvAttrDetails" --%>
                                            </div>
                                        </div>

                                        <div class="well">

                                            <div id="dvAttrDetails" runat="server">

                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4" for="icondropdownmenu">
                                                        Icon&nbsp;
                                                <asp:RequiredFieldValidator ID="rfvicondropdownmenu" runat="server" ControlToValidate="ddlglyphiconForAttributes"
                                                    ErrorMessage="Please select icon." InitialValue="0" Text="*" Enabled="false" ValidationGroup="vldgrpKeyword" CssClass="text-danger">
                                                </asp:RequiredFieldValidator>
                                                        <span id="spanglyphicon" runat="server" class=""></span>
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList runat="server" ID="ddlglyphiconForAttributes" onchange="SetGlyphicon(this)" data-show-icon="true" CssClass="form-control">
                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4" for="ddlAttrType">
                                                        Attribute Type
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList runat="server" ID="ddlAttrType" CssClass="form-control">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1">Extract & Strip</asp:ListItem>
                                                            <asp:ListItem Value="2">Extract & Replace</asp:ListItem>
                                                            <asp:ListItem Value="3">Extract Only</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="control-label col-sm-4" for="ddlAttrLvl">
                                                        Attribute Level
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList runat="server" ID="ddlAttrLvl" CssClass="form-control ddlAttrLvl" onchange="hideshowAttrLvl('MainContent_keywordManager_ddlAttrLvl');">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1">Room Info</asp:ListItem>
                                                            <asp:ListItem Value="2">Room Amenity</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="form-group row AttributeLevel_Hide AttributeLevel_RoomAmenity collapse">
                                                    <label class="control-label col-sm-4" for="ddlAmentityType">
                                                        Amentity Type
                                                    </label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList runat="server" ID="ddlAmentityType" CssClass="form-control" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="AttributeLevel_Hide AttributeLevel_RoomInfo collapse">

                                                    <div class="form-group row">
                                                        <label class="control-label col-sm-4" for="ddlAmentityType">
                                                            Location
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList runat="server" ID="ddlRoomSchemaLoc" CssClass="form-control ddlRoomSchemaLoc" onchange="hideshowAttrLvlRoomSchema('MainContent_keywordManager_ddlRoomSchemaLoc');">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Number Of Rooms</asp:ListItem>
                                                                <asp:ListItem Value="2">Room Category</asp:ListItem>
                                                                <asp:ListItem Value="3">Floor Name</asp:ListItem>
                                                                <asp:ListItem Value="4">Floor Number</asp:ListItem>
                                                                <asp:ListItem Value="5">Room View</asp:ListItem>
                                                                <asp:ListItem Value="6">Room Decor</asp:ListItem>
                                                                <asp:ListItem Value="7">Bed Type</asp:ListItem>
                                                                <asp:ListItem Value="8">Bathroom Type</asp:ListItem>
                                                                <asp:ListItem Value="9">Smoking</asp:ListItem>
                                                                <asp:ListItem Value="10">Room Size</asp:ListItem>
                                                                <asp:ListItem Value="11">Inter Rooms</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row AttributeLevel_RoomInfo_Hide AttributeLevel_RoomInfo_RoomCategory collapse">
                                                        <label class="control-label col-sm-4" for="ddlRoomInfo_Category">
                                                            Room Category
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList runat="server" ID="ddlRoomInfo_Category" CssClass="form-control" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row AttributeLevel_RoomInfo_Hide AttributeLevel_RoomInfo_BedType collapse">
                                                        <label class="control-label col-sm-4" for="ddlRoomInfo_BedType">
                                                            Bed Type
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList runat="server" ID="ddlRoomInfo_BedType" CssClass="form-control" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row AttributeLevel_RoomInfo_Hide AttributeLevel_RoomInfo_BathroomType collapse">
                                                        <label class="control-label col-sm-4" for="ddlRoomInfo_BathroomType">
                                                            Bathroom Type
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList runat="server" ID="ddlRoomInfo_BathroomType" CssClass="form-control" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group row AttributeLevel_RoomInfo_Hide AttributeLevel_RoomInfo_Smoking collapse">
                                                        <label class="control-label col-sm-4" for="ddlRoomInfo_Smoking">
                                                            Smoking?
                                                        </label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList runat="server" ID="ddlRoomInfo_Smoking" CssClass="form-control">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">YES</asp:ListItem>
                                                                <asp:ListItem Value="2">NO</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                </div>

                                            </div>

                                        </div>





                                        <div class="form-group row">
                                            <label class="control-label col-sm-4" for="btnSave"></label>
                                            <div class="col-sm-8">
                                                <asp:Button runat="server" ID="btnSave" CausesValidation="true" ValidationGroup="vldgrpKeyword"
                                                    CssClass="btn btn-sm btn-primary pull-right" Text="Save" OnClick="btnSave_Click" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="col-md-8">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <strong>Alias</strong>
                                    </div>
                                    <div class="panel-body">

                                        <asp:GridView ID="grdAlias" runat="server"
                                            CssClass="table table-bordered table-hover" AllowPaging="true" AllowCustomPaging="True" AutoGenerateColumns="false"
                                            DataKeyNames="KeywordAlias_Id" OnRowCommand="grdAlias_RowCommand" OnPageIndexChanging="grdAlias_PageIndexChanging"
                                            ShowHeader="true" EmptyDataText="No Alias defined yet."
                                            OnRowDeleting="grdAlias_RowDeleting" OnRowCancelingEdit="grdAlias_RowCancelingEdit" OnRowUpdating="grdAlias_RowUpdating"
                                            OnRowEditing="grdAlias_RowEditing" ShowHeaderWhenEmpty="false" OnRowDataBound="grdAlias_RowDataBound">

                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Alias Value
                                                        <asp:TextBox ID="txtAlias" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="vldtxtAliasNew" runat="server" ControlToValidate="txtAlias"
                                                            ErrorMessage="Alias cannot be empty." Text="*" ValidationGroup="vldgrpAliasNew" CssClass="text-danger">
                                                        </asp:RequiredFieldValidator>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAlias" runat="server" CssClass="control-label" Text='<%# Bind("Value") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtAlias" runat="server" CssClass="form-control" Text='<%# Bind("Value") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="vldtxtAliasEdit" runat="server" ControlToValidate="txtAlias"
                                                            ErrorMessage="Alias cannot be empty." Text="*" ValidationGroup="vldgrpAliasEdit" CssClass="text-danger">
                                                        </asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Alias Sequence
                                                        <asp:TextBox ID="txtAliasSequence" runat="server" CssClass="form-control" MaxLength="3"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="vldtxtAliasNewSequence" runat="server" ControlToValidate="txtAliasSequence"
                                                            ErrorMessage="Alias Sequence cannot be empty." Text="*" ValidationGroup="vldgrpAliasNew" CssClass="text-danger">
                                                        </asp:RequiredFieldValidator>
                                                        <cc1:FilteredTextBoxExtender ID="axfte_txtAliasSequence" runat="server" FilterType="Numbers" TargetControlID="txtAliasSequence" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAliasSequence" runat="server" CssClass="control-label" Text='<%# Bind("Sequence") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtAliasSequence" runat="server" CssClass="form-control" Text='<%# Bind("Sequence") %>' MaxLength="3"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="vldtxtAliasEditSequence" runat="server" ControlToValidate="txtAliasSequence"
                                                            ErrorMessage="Alias Sequence cannot be empty." Text="*" ValidationGroup="vldgrpAliasEditSequence" CssClass="text-danger">
                                                        </asp:RequiredFieldValidator>
                                                        <cc1:FilteredTextBoxExtender ID="axfte_txtAliasSequence" runat="server" FilterType="Numbers" TargetControlID="txtAliasSequence" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        No Of Hits
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAliasNoOfHits" runat="server" CssClass="control-label" Text='<%# Bind("NoOfHits") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblAliasNoOfHits" runat="server" CssClass="control-label" Text='<%# Bind("NoOfHits") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ShowHeader="false">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="btnAdd" runat="server" CausesValidation="true" CommandName="AddNew" ToolTip="Add Alias"
                                                            ValidationGroup="vldgrpAliasNew" CssClass="btn btn-default" CommandArgument='<%# Guid.NewGuid() %>'>
                                                                        <span aria-hidden="true" class="glyphicon glyphicon-plus"></span>
                                                        </asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>

                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CssClass="btn btn-default"
                                                            CommandArgument='<%# Bind("KeywordAlias_Id") %>' ToolTip="Edit Alias">
                                                            <span aria-hidden="true" class="glyphicon glyphicon-pencil"></span>
                                                        </asp:LinkButton>

                                                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# Eval("Status").ToString() == "ACTIVE" ? "Delete" : "UnDelete" %>'
                                                            CssClass="btn btn-default" CommandArgument='<%# Bind("KeywordAlias_Id") %>' ToolTip='<%# Eval("Status").ToString() == "ACTIVE" ? "Delete" : "UnDelete"   %>'>
                                                            <span aria-hidden="true" class='<%# Eval("Status").ToString() == "ACTIVE" ? "glyphicon glyphicon-trash" : "glyphicon glyphicon-repeat" %>'></span>
                                                        </asp:LinkButton>

                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="btnUpdate" runat="server" CausesValidation="true" ValidationGroup="vldgrpAliasEdit"
                                                            CommandName="Update" CssClass="btn btn-default" CommandArgument='<%# Bind("KeywordAlias_Id") %>' ToolTip="Update Alias">
                                                                        <span aria-hidden="true" class="glyphicon glyphicon-ok"></span>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CssClass="btn btn-default"
                                                            CommandArgument='<%# Bind("KeywordAlias_Id") %>' ToolTip="Cancel Update">
                                                                        <span aria-hidden="true" class="glyphicon glyphicon-remove"></span>
                                                        </asp:LinkButton>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pagination-ys" />
                                        </asp:GridView>

                                    </div>
                                </div>

                                <div class=" pull-right">
                                    <asp:Button runat="server" ID="btnClose" CssClass="btn btn-sm btn-primary" Text="Close" data-dismiss="modal" />
                                </div>

                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</div>
