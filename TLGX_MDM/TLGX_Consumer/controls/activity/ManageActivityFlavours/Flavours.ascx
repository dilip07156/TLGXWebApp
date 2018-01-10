﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Flavours.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.Flavours" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Src="~/controls/activity/ManageActivityFlavours/ActivityContactDetails.ascx" TagPrefix="uc1" TagName="ActivityContactDetails" %>--%>
<%@ Register Src="~/controls/activity/ManageActivityFlavours/ActivityStatus.ascx" TagPrefix="uc1" TagName="ActivityStatus" %>

<script type="text/javascript">

    function MutExChkList(chk) {
        var chkList = chk.parentNode.parentNode.parentNode;
        var chks = chkList.getElementsByTagName("input");
        for (var i = 0; i < chks.length; i++) {
            if (chks[i] != chk) {
                chks[i].checked = chk.checked;
            }
        }
    }

    function SetSession(StartTime) {

        var value = StartTime.value;
        var Footer = StartTime.parentNode.parentNode;
        var sessionddl = Footer.getElementsByClassName("sessionSet");

        var hour = parseInt(value);

        var textToFind = "";

        if (!isNaN(hour)) {
            if (hour >= 3 && hour <= 8) {
                textToFind = "Early Morning";
            } else if (hour >= 9 && hour <= 11) {
                textToFind = "Morning";
            } else if (hour >= 12 && hour <= 16) {
                textToFind = "Afternoon";
            } else if (hour >= 17 && hour <= 18) {
                textToFind = "Evening";
            } else if (hour >= 19 && hour <= 20) {
                textToFind = "Late Evening";
            } else if ((hour >= 21 && hour <= 23) || (hour >= 0 && hour <= 2)) {
                textToFind = "Night";
            }
        }


        var isOptionSet = false;
        for (var i = 0; i < sessionddl[0].options.length; i++) {

            sessionddl[0].options[i].removeAttribute('selected');

            if (sessionddl[0].options[i].text === textToFind && textToFind.length != 0) {
                sessionddl[0].options[i].setAttribute('selected', 'selected');
                sessionddl[0].options[i].selected = true;
                isOptionSet = true;
            }

        }
        if (!isOptionSet) {
            sessionddl[0].options[0].setAttribute('selected', 'selected');
        }


    }

    function SetddlValue(selected) {
        for (var i = 0; i < selected.options.length; i++) {
            selected.options[i].removeAttribute('selected');
        }
        selected.options[selected.selectedIndex].setAttribute('selected', 'selected');
    }

    function setDurationType(dropdown, type) {

        var value = dropdown.options[dropdown.selectedIndex].text;

        var row = dropdown.parentNode.parentNode;

        var ddlDay = row.getElementsByClassName("classDurationDay")[0];
        var ddlHour = row.getElementsByClassName("classDurationHour")[0];
        var ddlMinute = row.getElementsByClassName("classDurationMinute")[0];
        var ddlType = row.parentNode.getElementsByClassName("classDurationType")[0];

        var day = parseInt(ddlDay.value);
        var hour = parseInt(ddlHour.value);
        var minute = parseInt(ddlMinute.value);

        var textToFind = "";

        if (!isNaN(hour) && !isNaN(day) && !isNaN(minute)) {
            if (day === 0 && hour === 0 && minute === 0) {
                textToFind = "-Select-";
            }
            else if ((day === 0 && hour === 4 && minute > 0) || (day === 0 && hour > 4) || (day === 1 && hour === 0 && minute === 0)) {
                textToFind = "Full Day";
            }
            else if (day > 1 || (day === 1 && (hour > 0 || minute > 0))) {
                textToFind = "Overnight";
            }
            else if ((day === 0 && hour < 4) || (day === 0 && hour === 4 && minute === 0)) {
                textToFind = "Half Day";
            }
            else {
                textToFind = "-Select-";
            }
        }

        var isOptionSet = false;
        for (var i = 0; i < ddlType.options.length; i++) {

            ddlType.options[i].removeAttribute('selected');

            if (ddlType.options[i].text === textToFind && textToFind.length != 0) {
                ddlType.options[i].selected = true;
                ddlType.options[i].setAttribute('selected', 'selected');
                isOptionSet = true;
            }

        }
        if (!isOptionSet) {
            ddlType.options[0].setAttribute('selected', 'selected');
        }


    }

    function SelectAllToDelete(flag) {
        var chckboxtoDetleteOperation = document.getElementsByClassName("chkToDeleteOperation");
        for (var i = 0; i < chckboxtoDetleteOperation.length; i++) {
            chckboxtoDetleteOperation[i].children[0].checked = flag;
        }
        var chckboxtoDetleteDays = document.getElementsByClassName("chkToDeleteDays");
        for (var i = 0; i < chckboxtoDetleteDays.length; i++) {
            chckboxtoDetleteDays[i].children[0].checked = flag;
        }

    }
</script>

<style>
    .selectRemoveArrow {
        -webkit-appearance: none;
        -moz-appearance: none;
        text-indent: 1px;
        text-overflow: '';
        padding: 0px !important;
        width: 30px !important;
    }

    .floatingButton {
        text-align: right;
        width: 80%;
        position: fixed;
        bottom: 15px;
        z-index: 1000;
    }
</style>
<asp:UpdatePanel ID="upActivityFlavour" runat="server">
    <ContentTemplate>

        <div class="row">
            <div class="col-lg-12">
                <div id="dvMsg" runat="server" style="display: none;"></div>
                <div id="dvMsgStatusUpdate" runat="server" style="display: none;"></div>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-12">
                <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="ProductOverView" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
            </div>
        </div>

        <div class="floatingButton">
            <asp:LinkButton ID="btnSave" runat="server" CausesValidation="false" Text="Update Flavour Info" CssClass="btn btn-primary btn-sm" ValidationGroup="ProductOverView" OnClick="btnSave_Click" />
            <br />

        </div>


        <div class="row">
            <div class="col-lg-7">

                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4>Geography</h4>
                    </div>
                    <div class="panel-body">
                        <div class="form-group row">
                            <label class="control-label col-sm-2" for="ddlCountry">
                                Country
                            </label>
                            <em>
                                <asp:Label ID="lblSuppCountry" runat="server" class="control-label col-sm-2"></asp:Label></em>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group row">
                            <label class="control-label col-sm-2" for="ddlCity">
                                City
                            </label>
                            <em>
                                <asp:Label ID="lblSuppCity" runat="server" class="control-label col-sm-2"></asp:Label></em>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4>Classification Mapping</h4>
                    </div>
                    <div class="panel-body">

                        <div class="form-group row">
                            <label class="control-label col-sm-2" for="txtProdCategory">
                                Product Category
                            </label>
                            <em>
                                <asp:Label ID="lblSuppProductCategory" runat="server" Text=" " class="control-label col-sm-2"></asp:Label></em>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtProdCategory" runat="server" Text="ACTIVITIES" CssClass="form-control" Enabled="false">
                                </asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group row">
                            <label class="control-label col-sm-2" for="ddlProdcategorySubType">
                                Product Sub Category 
                            </label>
                            <em>
                                <asp:Label ID="lblSuppProductSubCategory" runat="server" Text="" class="control-label col-sm-2"></asp:Label></em>
                            <div class="col-sm-8">
                                <div class="row">
                                    <div class="col-md-10">
                                        <asp:DropDownList ID="ddlProdcategorySubType" runat="server" CssClass="col-sm-8 form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <asp:LinkButton CssClass="btn btn-default" runat="server" ID="btnAddSubCategory" OnClick="btnAddSubCategory_Click">
                                                    <i class="glyphicon glyphicon-plus"></i>
                                    </asp:LinkButton>
                                </div>

                                <asp:Repeater ID="repSubCategory" runat="server" OnItemCommand="repSubCategory_ItemCommand">
                                    <HeaderTemplate>
                                        <table class="table table-stripped table-hover">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="row">
                                            <td class="col-md-10">
                                                <asp:Label ID="lblSubCategory" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SubCategory") %>'></asp:Label>
                                            </td>
                                            <td class="col-md-2">
                                                <asp:LinkButton CssClass="btn btn-default" runat="server" ID="btnRemoveSubCategory" CommandName="RemoveSubCategory" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SubCategory_Id") %>'>
                                                        <i class="glyphicon glyphicon-minus"></i>
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table> 
                                    </FooterTemplate>
                                </asp:Repeater>

                            </div>
                        </div>

                        <div class="form-group row">
                            <label class="control-label col-md-2" for="ddlProductType">
                                Product Name Type
                            </label>
                            <em>
                                <asp:Label ID="lblSuppProductType" runat="server" class="control-label col-md-2"></asp:Label></em>
                            <div class="col-md-8">

                                <div class="row">
                                    <div class="col-md-10">
                                        <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <asp:LinkButton CssClass="btn btn-default " runat="server" ID="btnAddProductType" OnClick="btnAddProductType_Click">
                                                        <i class="glyphicon glyphicon-plus"></i>
                                    </asp:LinkButton>

                                </div>

                                <asp:Repeater ID="repProductType" runat="server" OnItemCommand="repProductType_ItemCommand">
                                    <HeaderTemplate>
                                        <table class="table table-stripped table-hover">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="row">
                                            <td class="col-md-10">
                                                <asp:Label ID="lblProductType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProductType") %>'></asp:Label>
                                            </td>
                                            <td class="col-md-2">
                                                <asp:LinkButton CssClass="btn btn-default" runat="server" ID="btnRemoveProductType" CommandName="RemoveProductType" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductType_Id") %>'>
                                                        <i class="glyphicon glyphicon-minus"></i>
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table> 
                                    </FooterTemplate>
                                </asp:Repeater>

                            </div>
                        </div>

                        <div class="form-group row">
                            <label class="control-label col-sm-2" for="ddlProdNameSubType">
                                Product Name SubType
                            </label>
                            <em>
                                <asp:Label ID="lblSuppProdNameSubType" runat="server" class="control-label col-sm-2"></asp:Label></em>
                            <div class="col-sm-8">

                                <div class="row">
                                    <div class="col-md-10">
                                        <asp:DropDownList ID="ddlProdNameSubType" runat="server" CssClass="col-sm-8 form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <asp:LinkButton CssClass="btn btn-default" runat="server" ID="btnAddProductSubType" OnClick="btnAddProductSubType_Click">
                                         <i class="glyphicon glyphicon-plus"></i>
                                    </asp:LinkButton>
                                </div>
                                <asp:Repeater ID="repProductSubType" runat="server" OnItemCommand="repProductSubType_ItemCommand">
                                    <HeaderTemplate>
                                        <table class="table table-stripped table-hover">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="row">
                                            <td class="col-md-10">
                                                <asp:Label ID="lblProductSubType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProductSubType") %>'></asp:Label>
                                            </td>
                                            <td class="col-md-2">
                                                <asp:LinkButton CssClass="btn btn-default" runat="server" ID="btnRemoveProductSubType" CommandName="RemoveProductSubType" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductSubType_Id") %>'>
                                                        <i class="glyphicon glyphicon-minus"></i>
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table> 
                                    </FooterTemplate>
                                </asp:Repeater>

                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="col-lg-5">

                <div class="panel panel-default">
                    <div class="panel-heading">Key Facts</div>
                    <div class="panel-body">
                        <div class="form-group row">
                            <div class="col-md-6 row">
                                <label class="control-label col-sm-4" for="chklstSuitableFor">Suitable For</label>
                                <em>
                                    <asp:Label ID="lblSuppSuitableFor" runat="server" class="control-label col-sm-2"></asp:Label></em>
                            </div>
                            <div class="col-sm-6">
                                <asp:CheckBoxList ID="chklstSuitableFor" runat="server"></asp:CheckBoxList>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-md-6 row">
                                <label class="control-label col-sm-4" for="chklstSpecials">Specials</label>
                                <em>
                                    <asp:Label ID="lblSuppSpecials" runat="server" class="control-label col-sm-2"></asp:Label></em>
                            </div>
                            <div class="col-sm-6">
                                <asp:CheckBoxList ID="chklstSpecials" runat="server"></asp:CheckBoxList>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-sm-6 row">
                                <label class="control-label col-sm-4" for="chklstPhysicalIntensity">Physical Intensity</label>
                                <em>
                                    <asp:Label ID="lblSuppPhysicalIntensity" runat="server" class="control-label col-sm-2"></asp:Label></em>
                            </div>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlPhysicalIntensity" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>

        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-md-6">
                                <h4>Operating Dates & Days of Week</h4>
                            </div>
                            <div class="col-md-3 ">
                                <table class="table">
                                    <tr>
                                        <td>
                                            <label class="control-label">Select</label></td>
                                        <td>
                                            <button class="btn btn-link" style="padding: 0px !important;" onclick="SelectAllToDelete(true);">All</button></td>
                                        <td>
                                            <button class="btn btn-link" style="padding: 0px !important;" onclick="SelectAllToDelete(false);">None</button></td>
                                        <td>
                                            <asp:Button runat="server" Style="padding: 0px !important;" CssClass="btn btn-link" Text="Remove Selected" OnClick="btnRemoveSelectedOperationDays_Click" ID="btnRemoveSelectedOperationDays" /></td>
                                    </tr>
                                </table>

                            </div>
                            <div class="col-md-3 ">
                                &nbsp;
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-9">
                            <asp:Repeater ID="repOperatingDays" runat="server" OnItemCommand="repOperatingDays_ItemCommand" OnItemDataBound="repOperatingDays_ItemDataBound">

                                <HeaderTemplate>

                                    <div class="form-group">
                                        <div class="panel panel-primary">
                                            <div class="panel-body">
                                </HeaderTemplate>

                                <ItemTemplate>

                                    <div class="form-group row well">

                                        <div class="col-sm-3">
                                            <label class="control-label col-sm-8" for="chkSpecificOperatingDays">Operating Dates</label>
                                            <asp:CheckBox ID="chkSpecificOperatingDays" runat="server" CssClass="col-sm-4" Checked='<%# DataBinder.Eval(Container.DataItem, "IsOperatingDays") %>' />
                                        </div>

                                        <div class="col-sm-4">
                                            <label class="control-label col-sm-4" for="txtFrom">
                                                From Date
                                            </label>
                                            <div class="col-sm-8">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "FromDate", "{0:dd/MM/yyyy}") %>' />
                                                    <span class="input-group-btn">
                                                        <button class="btn btn-default" type="button" id="iCalFrom" runat="server">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </button>
                                                    </span>
                                                </div>
                                                <cc1:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy" PopupButtonID="iCalFrom"></cc1:CalendarExtender>
                                                <cc1:FilteredTextBoxExtender ID="axfte_txtFrom" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtFrom" />

                                            </div>

                                        </div>

                                        <div class="col-sm-4">
                                            <label class="control-label col-sm-4" for="txtTo">
                                                To Date
                                            </label>
                                            <div class="col-sm-8">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "EndDate", "{0:dd/MM/yyyy}") %>' />
                                                    <span class="input-group-btn">
                                                        <button class="btn btn-default" type="button" id="iCalTo" runat="server">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </button>
                                                    </span>
                                                </div>
                                                <cc1:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy" PopupButtonID="iCalTo"></cc1:CalendarExtender>
                                                <cc1:FilteredTextBoxExtender ID="axfte_txtTo" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtTo" />
                                            </div>
                                        </div>

                                        <div class="col-sm-1" style="padding-left: 0px;">
                                            <div class="input-group">
                                                <asp:CheckBox ID="chkToDeleteOperation" runat="server" CssClass="chkToDeleteOperation input-group-addon" />
                                                <asp:LinkButton CssClass="btn btn-default" runat="server" ID="btnRemoveOperatingDays" CommandName="RemoveOperatingDays" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Activity_DaysOfOperation_Id") %>'>
                                                    <i class="glyphicon glyphicon-trash"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="form-group row">
                                        <div class="form-group col-sm-12">
                                            <asp:Repeater ID="repDaysOfWeek" runat="server" DataSource='<%# DataBinder.Eval(Container.DataItem, "DaysOfWeek") %>' OnItemCommand="repDaysOfWeek_ItemCommand" OnItemDataBound="repDaysOfWeek_ItemDataBound">
                                                <HeaderTemplate>

                                                    <div class="panel panel-primary">

                                                        <div class="panel-body">
                                                </HeaderTemplate>
                                                <ItemTemplate>

                                                    <div class="row">
                                                        <div class="col-xs-2">
                                                            <label class="control-label-mand" for="txtStartTime">
                                                                Start Time
                                                            </label>
                                                            <em>&nbsp;(<asp:Label ID="lblSupplierStartTime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierStartTime") %>'></asp:Label>)</em>
                                                        </div>
                                                        <div class="col-xs-2">
                                                            <label class="control-label-mand" for="txtSession">
                                                                Session
                                                            </label>
                                                            <em>&nbsp;(<asp:Label ID="lblSupplierSession" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierSession") %>'></asp:Label>)</em>
                                                            <asp:HiddenField ID="hdnSession" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Session") %>' />
                                                        </div>
                                                        <div class="col-xs-2">
                                                            <label class="control-label-mand" for="txtDuration">
                                                                Duration (dd.HH:mm)
                                                            </label>
                                                            <em>&nbsp;(<asp:Label ID="lblSupplierDuration" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierDuration") %>'></asp:Label>)</em>
                                                            <asp:HiddenField ID="hdnDuration" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Duration") %>' />
                                                        </div>
                                                        <div class="col-xs-2">
                                                            <label class="control-label-mand" for="ddlDurationType">
                                                                Duration Type
                                                            </label>
                                                            <asp:HiddenField ID="hdnDurationType" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "DurationType") %>' />
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label class="control-label-mand" for="txtSession">
                                                                Applicable On
                                                            </label>
                                                            <em>&nbsp;(<asp:Label ID="lblSupplierFrequency" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierFrequency") %>'></asp:Label>)</em>
                                                        </div>
                                                        <div class="col-xs-1">
                                                        </div>
                                                    </div>

                                                    <div class="row well">

                                                        <div class="col-xs-2">
                                                            <asp:TextBox ID="txtStartTime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StartTime") %>' CssClass="form-control" onchange="SetSession(this)"></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="txtStartTime_MaskEditExtender" runat="server" AcceptAMPM="false"
                                                                Mask="99:99" MaskType="Time" PromptCharacter="_" TargetControlID="txtStartTime"
                                                                UserTimeFormat="TwentyFourHour" InputDirection="LeftToRight"></cc1:MaskedEditExtender>
                                                        </div>

                                                        <div class="col-xs-2">
                                                            <asp:DropDownList ID="ddlSession" runat="server" onchange="SetddlValue(this)" CssClass="form-control sessionSet" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="col-xs-2">

                                                            <div class="form-inline">
                                                                <asp:DropDownList ID="ddlDurationDay" runat="server" class="form-control selectRemoveArrow classDurationDay" onchange="setDurationType(this, 'Day')">
                                                                    <asp:ListItem Text="00"></asp:ListItem>
                                                                    <asp:ListItem Text="01"></asp:ListItem>
                                                                    <asp:ListItem Text="02"></asp:ListItem>
                                                                    <asp:ListItem Text="03"></asp:ListItem>
                                                                    <asp:ListItem Text="04"></asp:ListItem>
                                                                    <asp:ListItem Text="05"></asp:ListItem>
                                                                    <asp:ListItem Text="06"></asp:ListItem>
                                                                    <asp:ListItem Text="07"></asp:ListItem>
                                                                    <asp:ListItem Text="08"></asp:ListItem>
                                                                    <asp:ListItem Text="09"></asp:ListItem>
                                                                    <asp:ListItem Text="10"></asp:ListItem>
                                                                    <asp:ListItem Text="11"></asp:ListItem>
                                                                    <asp:ListItem Text="12"></asp:ListItem>
                                                                    <asp:ListItem Text="13"></asp:ListItem>
                                                                    <asp:ListItem Text="14"></asp:ListItem>
                                                                    <asp:ListItem Text="15"></asp:ListItem>
                                                                    <asp:ListItem Text="16"></asp:ListItem>
                                                                    <asp:ListItem Text="17"></asp:ListItem>
                                                                    <asp:ListItem Text="18"></asp:ListItem>
                                                                    <asp:ListItem Text="19"></asp:ListItem>
                                                                    <asp:ListItem Text="20"></asp:ListItem>
                                                                    <asp:ListItem Text="21"></asp:ListItem>
                                                                    <asp:ListItem Text="22"></asp:ListItem>
                                                                    <asp:ListItem Text="23"></asp:ListItem>
                                                                    <asp:ListItem Text="24"></asp:ListItem>
                                                                    <asp:ListItem Text="25"></asp:ListItem>
                                                                    <asp:ListItem Text="26"></asp:ListItem>
                                                                    <asp:ListItem Text="27"></asp:ListItem>
                                                                    <asp:ListItem Text="28"></asp:ListItem>
                                                                    <asp:ListItem Text="29"></asp:ListItem>
                                                                    <asp:ListItem Text="30"></asp:ListItem>
                                                                    <asp:ListItem Text="31"></asp:ListItem>
                                                                    <asp:ListItem Text="32"></asp:ListItem>
                                                                    <asp:ListItem Text="33"></asp:ListItem>
                                                                    <asp:ListItem Text="34"></asp:ListItem>
                                                                    <asp:ListItem Text="35"></asp:ListItem>
                                                                    <asp:ListItem Text="36"></asp:ListItem>
                                                                    <asp:ListItem Text="37"></asp:ListItem>
                                                                    <asp:ListItem Text="38"></asp:ListItem>
                                                                    <asp:ListItem Text="39"></asp:ListItem>
                                                                    <asp:ListItem Text="40"></asp:ListItem>
                                                                    <asp:ListItem Text="41"></asp:ListItem>
                                                                    <asp:ListItem Text="42"></asp:ListItem>
                                                                    <asp:ListItem Text="43"></asp:ListItem>
                                                                    <asp:ListItem Text="44"></asp:ListItem>
                                                                    <asp:ListItem Text="45"></asp:ListItem>
                                                                    <asp:ListItem Text="46"></asp:ListItem>
                                                                    <asp:ListItem Text="47"></asp:ListItem>
                                                                    <asp:ListItem Text="48"></asp:ListItem>
                                                                    <asp:ListItem Text="49"></asp:ListItem>
                                                                    <asp:ListItem Text="50"></asp:ListItem>
                                                                    <asp:ListItem Text="51"></asp:ListItem>
                                                                    <asp:ListItem Text="52"></asp:ListItem>
                                                                    <asp:ListItem Text="53"></asp:ListItem>
                                                                    <asp:ListItem Text="54"></asp:ListItem>
                                                                    <asp:ListItem Text="55"></asp:ListItem>
                                                                    <asp:ListItem Text="56"></asp:ListItem>
                                                                    <asp:ListItem Text="57"></asp:ListItem>
                                                                    <asp:ListItem Text="58"></asp:ListItem>
                                                                    <asp:ListItem Text="59"></asp:ListItem>
                                                                    <asp:ListItem Text="60"></asp:ListItem>
                                                                    <asp:ListItem Text="61"></asp:ListItem>
                                                                    <asp:ListItem Text="62"></asp:ListItem>
                                                                    <asp:ListItem Text="63"></asp:ListItem>
                                                                    <asp:ListItem Text="64"></asp:ListItem>
                                                                    <asp:ListItem Text="65"></asp:ListItem>
                                                                    <asp:ListItem Text="66"></asp:ListItem>
                                                                    <asp:ListItem Text="67"></asp:ListItem>
                                                                    <asp:ListItem Text="68"></asp:ListItem>
                                                                    <asp:ListItem Text="69"></asp:ListItem>
                                                                    <asp:ListItem Text="70"></asp:ListItem>
                                                                    <asp:ListItem Text="71"></asp:ListItem>
                                                                    <asp:ListItem Text="72"></asp:ListItem>
                                                                    <asp:ListItem Text="73"></asp:ListItem>
                                                                    <asp:ListItem Text="74"></asp:ListItem>
                                                                    <asp:ListItem Text="75"></asp:ListItem>
                                                                    <asp:ListItem Text="76"></asp:ListItem>
                                                                    <asp:ListItem Text="77"></asp:ListItem>
                                                                    <asp:ListItem Text="78"></asp:ListItem>
                                                                    <asp:ListItem Text="79"></asp:ListItem>
                                                                    <asp:ListItem Text="80"></asp:ListItem>
                                                                    <asp:ListItem Text="81"></asp:ListItem>
                                                                    <asp:ListItem Text="82"></asp:ListItem>
                                                                    <asp:ListItem Text="83"></asp:ListItem>
                                                                    <asp:ListItem Text="84"></asp:ListItem>
                                                                    <asp:ListItem Text="85"></asp:ListItem>
                                                                    <asp:ListItem Text="86"></asp:ListItem>
                                                                    <asp:ListItem Text="87"></asp:ListItem>
                                                                    <asp:ListItem Text="88"></asp:ListItem>
                                                                    <asp:ListItem Text="89"></asp:ListItem>
                                                                    <asp:ListItem Text="90"></asp:ListItem>
                                                                    <asp:ListItem Text="91"></asp:ListItem>
                                                                    <asp:ListItem Text="92"></asp:ListItem>
                                                                    <asp:ListItem Text="93"></asp:ListItem>
                                                                    <asp:ListItem Text="94"></asp:ListItem>
                                                                    <asp:ListItem Text="95"></asp:ListItem>
                                                                    <asp:ListItem Text="96"></asp:ListItem>
                                                                    <asp:ListItem Text="97"></asp:ListItem>
                                                                    <asp:ListItem Text="98"></asp:ListItem>
                                                                    <asp:ListItem Text="99"></asp:ListItem>

                                                                </asp:DropDownList>

                                                                <asp:DropDownList ID="ddlDurationHour" runat="server" class="form-control selectRemoveArrow classDurationHour" onchange="setDurationType(this, 'Hour')">
                                                                    <asp:ListItem Text="00"></asp:ListItem>
                                                                    <asp:ListItem Text="01"></asp:ListItem>
                                                                    <asp:ListItem Text="02"></asp:ListItem>
                                                                    <asp:ListItem Text="03"></asp:ListItem>
                                                                    <asp:ListItem Text="04"></asp:ListItem>
                                                                    <asp:ListItem Text="05"></asp:ListItem>
                                                                    <asp:ListItem Text="06"></asp:ListItem>
                                                                    <asp:ListItem Text="07"></asp:ListItem>
                                                                    <asp:ListItem Text="08"></asp:ListItem>
                                                                    <asp:ListItem Text="09"></asp:ListItem>
                                                                    <asp:ListItem Text="10"></asp:ListItem>
                                                                    <asp:ListItem Text="11"></asp:ListItem>
                                                                    <asp:ListItem Text="12"></asp:ListItem>
                                                                    <asp:ListItem Text="13"></asp:ListItem>
                                                                    <asp:ListItem Text="14"></asp:ListItem>
                                                                    <asp:ListItem Text="15"></asp:ListItem>
                                                                    <asp:ListItem Text="16"></asp:ListItem>
                                                                    <asp:ListItem Text="17"></asp:ListItem>
                                                                    <asp:ListItem Text="18"></asp:ListItem>
                                                                    <asp:ListItem Text="19"></asp:ListItem>
                                                                    <asp:ListItem Text="20"></asp:ListItem>
                                                                    <asp:ListItem Text="21"></asp:ListItem>
                                                                    <asp:ListItem Text="22"></asp:ListItem>
                                                                    <asp:ListItem Text="23"></asp:ListItem>
                                                                </asp:DropDownList>

                                                                <asp:DropDownList ID="ddlDurationMinute" runat="server" class="form-control selectRemoveArrow classDurationMinute" onchange="setDurationType(this, 'Minute')">
                                                                    <asp:ListItem Text="00"></asp:ListItem>
                                                                    <asp:ListItem Text="01"></asp:ListItem>
                                                                    <asp:ListItem Text="02"></asp:ListItem>
                                                                    <asp:ListItem Text="03"></asp:ListItem>
                                                                    <asp:ListItem Text="04"></asp:ListItem>
                                                                    <asp:ListItem Text="05"></asp:ListItem>
                                                                    <asp:ListItem Text="06"></asp:ListItem>
                                                                    <asp:ListItem Text="07"></asp:ListItem>
                                                                    <asp:ListItem Text="08"></asp:ListItem>
                                                                    <asp:ListItem Text="09"></asp:ListItem>
                                                                    <asp:ListItem Text="10"></asp:ListItem>
                                                                    <asp:ListItem Text="11"></asp:ListItem>
                                                                    <asp:ListItem Text="12"></asp:ListItem>
                                                                    <asp:ListItem Text="13"></asp:ListItem>
                                                                    <asp:ListItem Text="14"></asp:ListItem>
                                                                    <asp:ListItem Text="15"></asp:ListItem>
                                                                    <asp:ListItem Text="16"></asp:ListItem>
                                                                    <asp:ListItem Text="17"></asp:ListItem>
                                                                    <asp:ListItem Text="18"></asp:ListItem>
                                                                    <asp:ListItem Text="19"></asp:ListItem>
                                                                    <asp:ListItem Text="20"></asp:ListItem>
                                                                    <asp:ListItem Text="21"></asp:ListItem>
                                                                    <asp:ListItem Text="22"></asp:ListItem>
                                                                    <asp:ListItem Text="23"></asp:ListItem>
                                                                    <asp:ListItem Text="24"></asp:ListItem>
                                                                    <asp:ListItem Text="25"></asp:ListItem>
                                                                    <asp:ListItem Text="26"></asp:ListItem>
                                                                    <asp:ListItem Text="27"></asp:ListItem>
                                                                    <asp:ListItem Text="28"></asp:ListItem>
                                                                    <asp:ListItem Text="29"></asp:ListItem>
                                                                    <asp:ListItem Text="30"></asp:ListItem>
                                                                    <asp:ListItem Text="31"></asp:ListItem>
                                                                    <asp:ListItem Text="32"></asp:ListItem>
                                                                    <asp:ListItem Text="33"></asp:ListItem>
                                                                    <asp:ListItem Text="34"></asp:ListItem>
                                                                    <asp:ListItem Text="35"></asp:ListItem>
                                                                    <asp:ListItem Text="36"></asp:ListItem>
                                                                    <asp:ListItem Text="37"></asp:ListItem>
                                                                    <asp:ListItem Text="38"></asp:ListItem>
                                                                    <asp:ListItem Text="39"></asp:ListItem>
                                                                    <asp:ListItem Text="40"></asp:ListItem>
                                                                    <asp:ListItem Text="41"></asp:ListItem>
                                                                    <asp:ListItem Text="42"></asp:ListItem>
                                                                    <asp:ListItem Text="43"></asp:ListItem>
                                                                    <asp:ListItem Text="44"></asp:ListItem>
                                                                    <asp:ListItem Text="45"></asp:ListItem>
                                                                    <asp:ListItem Text="46"></asp:ListItem>
                                                                    <asp:ListItem Text="47"></asp:ListItem>
                                                                    <asp:ListItem Text="48"></asp:ListItem>
                                                                    <asp:ListItem Text="49"></asp:ListItem>
                                                                    <asp:ListItem Text="50"></asp:ListItem>
                                                                    <asp:ListItem Text="51"></asp:ListItem>
                                                                    <asp:ListItem Text="52"></asp:ListItem>
                                                                    <asp:ListItem Text="53"></asp:ListItem>
                                                                    <asp:ListItem Text="54"></asp:ListItem>
                                                                    <asp:ListItem Text="55"></asp:ListItem>
                                                                    <asp:ListItem Text="56"></asp:ListItem>
                                                                    <asp:ListItem Text="57"></asp:ListItem>
                                                                    <asp:ListItem Text="58"></asp:ListItem>
                                                                    <asp:ListItem Text="59"></asp:ListItem>

                                                                </asp:DropDownList>
                                                            </div>

                                                        </div>

                                                        <div class="col-xs-2">
                                                            <asp:DropDownList ID="ddlDurationType" runat="server" CssClass="form-control classDurationType" AppendDataBoundItems="true" onchange="SetddlValue(this)">
                                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="col-xs-3">
                                                            <div class="input-group input-group-sm">
                                                                <label class="control-label">
                                                                    ALL
                                                                                    <div>
                                                                                        <input type="checkbox" id="chkAll" name="All" aria-label="Checkbox for daily" onchange="MutExChkList(this)">
                                                                                    </div>
                                                                </label>
                                                                <label class="control-label">
                                                                    M
                                                            <div>
                                                                <input type="checkbox" id="chkMon" runat="server" name="Monday" checked='<%# DataBinder.Eval(Container.DataItem, "Mon") %>'>
                                                            </div>
                                                                </label>
                                                                <label class="control-label">
                                                                    T
                                                            <div>
                                                                <input type="checkbox" id="chkTues" runat="server" name="Tuesday" checked='<%# DataBinder.Eval(Container.DataItem, "Tues") %>'>
                                                            </div>
                                                                </label>
                                                                <label class="control-label">
                                                                    W
                                                            <div>
                                                                <input type="checkbox" id="chkWed" runat="server" name="Wednesday" checked='<%# DataBinder.Eval(Container.DataItem, "Wed") %>'>
                                                            </div>
                                                                </label>
                                                                <label class="control-label">
                                                                    TH
                                                            <div>
                                                                <input type="checkbox" id="chkThurs" runat="server" name="Thursday" checked='<%# DataBinder.Eval(Container.DataItem, "Thur") %>'>
                                                            </div>
                                                                </label>
                                                                <label class="control-label">
                                                                    F
                                                            <div>
                                                                <input type="checkbox" id="chkFri" runat="server" name="Friday" checked='<%# DataBinder.Eval(Container.DataItem, "Fri") %>'>
                                                            </div>
                                                                </label>
                                                                <label class="control-label">
                                                                    S
                                                            <div>
                                                                <input type="checkbox" id="chkSat" runat="server" name="Saturday" checked='<%# DataBinder.Eval(Container.DataItem, "Sat") %>'>
                                                            </div>
                                                                </label>
                                                                <label class="control-label">
                                                                    SU
                                                            <div>
                                                                <input type="checkbox" id="chkSun" runat="server" name="Sunday" checked='<%# DataBinder.Eval(Container.DataItem, "Sun") %>'>
                                                            </div>
                                                                </label>
                                                            </div>
                                                        </div>

                                                        <div class="col-xs-1" style="padding-left: 0px;">
                                                            <div class="input-group">
                                                                <asp:CheckBox ID="chkToDeleteDays" runat="server" CssClass="chkToDeleteDays input-group-addon" />
                                                                <asp:LinkButton CssClass="btn btn-default" runat="server" ID="btnRemoveDaysOfWeek" CommandName="RemoveDaysOfWeek" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Activity_DaysOfWeek_ID") %>'>
                                                            <i class="glyphicon glyphicon-trash"></i>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div class="form-group row">
                                                        <br />
                                                    </div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </div>
                                                            <div class="panel-footer">

                                                                <div class="form-group row col-md-12">
                                                                    <strong>Add Week Days</strong>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-xs-2">
                                                                        <label class="control-label-mand" for="txtStartTime">
                                                                            Start Time (HH:mm)
                                                                        </label>
                                                                    </div>

                                                                    <div class="col-xs-2">
                                                                        <label class="control-label-mand" for="ddlSession">
                                                                            Session
                                                                        </label>
                                                                    </div>

                                                                    <div class="col-xs-2">
                                                                        <label class="control-label-mand" for="txtDuration">
                                                                            Duration (dd.HH:mm)
                                                                        </label>
                                                                    </div>

                                                                    <div class="col-xs-2">
                                                                        <label class="control-label-mand" for="ddlDurationType">
                                                                            Duration Type
                                                                        </label>
                                                                    </div>

                                                                    <div class="col-xs-3">
                                                                        <label class="control-label-mand">
                                                                            Applicable On
                                                                        </label>
                                                                    </div>

                                                                    <div class="col-xs-1">
                                                                    </div>

                                                                </div>

                                                                <div class="row well">

                                                                    <div class="col-xs-2">
                                                                        <asp:TextBox ID="txtStartTime" runat="server" CssClass="form-control" onchange="SetSession(this)"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="txtStartTime_MaskEditExtender" runat="server" AcceptAMPM="false"
                                                                            Mask="99:99" MaskType="Time" PromptCharacter="_" TargetControlID="txtStartTime"
                                                                            UserTimeFormat="TwentyFourHour" InputDirection="LeftToRight"></cc1:MaskedEditExtender>
                                                                    </div>

                                                                    <div class="col-xs-2">
                                                                        <asp:DropDownList ID="ddlSession" runat="server" onchange="SetddlValue(this)" CssClass="form-control sessionSet"></asp:DropDownList>
                                                                    </div>

                                                                    <div class="col-xs-2">

                                                                        <div class="form-inline">

                                                                            <asp:DropDownList ID="ddlDurationDay" runat="server" class="form-control selectRemoveArrow classDurationDay" onchange="setDurationType(this, 'Day')">
                                                                                <asp:ListItem Text="00"></asp:ListItem>
                                                                                <asp:ListItem Text="01"></asp:ListItem>
                                                                                <asp:ListItem Text="02"></asp:ListItem>
                                                                                <asp:ListItem Text="03"></asp:ListItem>
                                                                                <asp:ListItem Text="04"></asp:ListItem>
                                                                                <asp:ListItem Text="05"></asp:ListItem>
                                                                                <asp:ListItem Text="06"></asp:ListItem>
                                                                                <asp:ListItem Text="07"></asp:ListItem>
                                                                                <asp:ListItem Text="08"></asp:ListItem>
                                                                                <asp:ListItem Text="09"></asp:ListItem>
                                                                                <asp:ListItem Text="10"></asp:ListItem>
                                                                                <asp:ListItem Text="11"></asp:ListItem>
                                                                                <asp:ListItem Text="12"></asp:ListItem>
                                                                                <asp:ListItem Text="13"></asp:ListItem>
                                                                                <asp:ListItem Text="14"></asp:ListItem>
                                                                                <asp:ListItem Text="15"></asp:ListItem>
                                                                                <asp:ListItem Text="16"></asp:ListItem>
                                                                                <asp:ListItem Text="17"></asp:ListItem>
                                                                                <asp:ListItem Text="18"></asp:ListItem>
                                                                                <asp:ListItem Text="19"></asp:ListItem>
                                                                                <asp:ListItem Text="20"></asp:ListItem>
                                                                                <asp:ListItem Text="21"></asp:ListItem>
                                                                                <asp:ListItem Text="22"></asp:ListItem>
                                                                                <asp:ListItem Text="23"></asp:ListItem>
                                                                                <asp:ListItem Text="24"></asp:ListItem>
                                                                                <asp:ListItem Text="25"></asp:ListItem>
                                                                                <asp:ListItem Text="26"></asp:ListItem>
                                                                                <asp:ListItem Text="27"></asp:ListItem>
                                                                                <asp:ListItem Text="28"></asp:ListItem>
                                                                                <asp:ListItem Text="29"></asp:ListItem>
                                                                                <asp:ListItem Text="30"></asp:ListItem>
                                                                                <asp:ListItem Text="31"></asp:ListItem>
                                                                                <asp:ListItem Text="32"></asp:ListItem>
                                                                                <asp:ListItem Text="33"></asp:ListItem>
                                                                                <asp:ListItem Text="34"></asp:ListItem>
                                                                                <asp:ListItem Text="35"></asp:ListItem>
                                                                                <asp:ListItem Text="36"></asp:ListItem>
                                                                                <asp:ListItem Text="37"></asp:ListItem>
                                                                                <asp:ListItem Text="38"></asp:ListItem>
                                                                                <asp:ListItem Text="39"></asp:ListItem>
                                                                                <asp:ListItem Text="40"></asp:ListItem>
                                                                                <asp:ListItem Text="41"></asp:ListItem>
                                                                                <asp:ListItem Text="42"></asp:ListItem>
                                                                                <asp:ListItem Text="43"></asp:ListItem>
                                                                                <asp:ListItem Text="44"></asp:ListItem>
                                                                                <asp:ListItem Text="45"></asp:ListItem>
                                                                                <asp:ListItem Text="46"></asp:ListItem>
                                                                                <asp:ListItem Text="47"></asp:ListItem>
                                                                                <asp:ListItem Text="48"></asp:ListItem>
                                                                                <asp:ListItem Text="49"></asp:ListItem>
                                                                                <asp:ListItem Text="50"></asp:ListItem>
                                                                                <asp:ListItem Text="51"></asp:ListItem>
                                                                                <asp:ListItem Text="52"></asp:ListItem>
                                                                                <asp:ListItem Text="53"></asp:ListItem>
                                                                                <asp:ListItem Text="54"></asp:ListItem>
                                                                                <asp:ListItem Text="55"></asp:ListItem>
                                                                                <asp:ListItem Text="56"></asp:ListItem>
                                                                                <asp:ListItem Text="57"></asp:ListItem>
                                                                                <asp:ListItem Text="58"></asp:ListItem>
                                                                                <asp:ListItem Text="59"></asp:ListItem>
                                                                                <asp:ListItem Text="60"></asp:ListItem>
                                                                                <asp:ListItem Text="61"></asp:ListItem>
                                                                                <asp:ListItem Text="62"></asp:ListItem>
                                                                                <asp:ListItem Text="63"></asp:ListItem>
                                                                                <asp:ListItem Text="64"></asp:ListItem>
                                                                                <asp:ListItem Text="65"></asp:ListItem>
                                                                                <asp:ListItem Text="66"></asp:ListItem>
                                                                                <asp:ListItem Text="67"></asp:ListItem>
                                                                                <asp:ListItem Text="68"></asp:ListItem>
                                                                                <asp:ListItem Text="69"></asp:ListItem>
                                                                                <asp:ListItem Text="70"></asp:ListItem>
                                                                                <asp:ListItem Text="71"></asp:ListItem>
                                                                                <asp:ListItem Text="72"></asp:ListItem>
                                                                                <asp:ListItem Text="73"></asp:ListItem>
                                                                                <asp:ListItem Text="74"></asp:ListItem>
                                                                                <asp:ListItem Text="75"></asp:ListItem>
                                                                                <asp:ListItem Text="76"></asp:ListItem>
                                                                                <asp:ListItem Text="77"></asp:ListItem>
                                                                                <asp:ListItem Text="78"></asp:ListItem>
                                                                                <asp:ListItem Text="79"></asp:ListItem>
                                                                                <asp:ListItem Text="80"></asp:ListItem>
                                                                                <asp:ListItem Text="81"></asp:ListItem>
                                                                                <asp:ListItem Text="82"></asp:ListItem>
                                                                                <asp:ListItem Text="83"></asp:ListItem>
                                                                                <asp:ListItem Text="84"></asp:ListItem>
                                                                                <asp:ListItem Text="85"></asp:ListItem>
                                                                                <asp:ListItem Text="86"></asp:ListItem>
                                                                                <asp:ListItem Text="87"></asp:ListItem>
                                                                                <asp:ListItem Text="88"></asp:ListItem>
                                                                                <asp:ListItem Text="89"></asp:ListItem>
                                                                                <asp:ListItem Text="90"></asp:ListItem>
                                                                                <asp:ListItem Text="91"></asp:ListItem>
                                                                                <asp:ListItem Text="92"></asp:ListItem>
                                                                                <asp:ListItem Text="93"></asp:ListItem>
                                                                                <asp:ListItem Text="94"></asp:ListItem>
                                                                                <asp:ListItem Text="95"></asp:ListItem>
                                                                                <asp:ListItem Text="96"></asp:ListItem>
                                                                                <asp:ListItem Text="97"></asp:ListItem>
                                                                                <asp:ListItem Text="98"></asp:ListItem>
                                                                                <asp:ListItem Text="99"></asp:ListItem>

                                                                            </asp:DropDownList>

                                                                            <asp:DropDownList ID="ddlDurationHour" runat="server" class="form-control selectRemoveArrow classDurationHour" onchange="setDurationType(this, 'Hour')">
                                                                                <asp:ListItem Text="00"></asp:ListItem>
                                                                                <asp:ListItem Text="01"></asp:ListItem>
                                                                                <asp:ListItem Text="02"></asp:ListItem>
                                                                                <asp:ListItem Text="03"></asp:ListItem>
                                                                                <asp:ListItem Text="04"></asp:ListItem>
                                                                                <asp:ListItem Text="05"></asp:ListItem>
                                                                                <asp:ListItem Text="06"></asp:ListItem>
                                                                                <asp:ListItem Text="07"></asp:ListItem>
                                                                                <asp:ListItem Text="08"></asp:ListItem>
                                                                                <asp:ListItem Text="09"></asp:ListItem>
                                                                                <asp:ListItem Text="10"></asp:ListItem>
                                                                                <asp:ListItem Text="11"></asp:ListItem>
                                                                                <asp:ListItem Text="12"></asp:ListItem>
                                                                                <asp:ListItem Text="13"></asp:ListItem>
                                                                                <asp:ListItem Text="14"></asp:ListItem>
                                                                                <asp:ListItem Text="15"></asp:ListItem>
                                                                                <asp:ListItem Text="16"></asp:ListItem>
                                                                                <asp:ListItem Text="17"></asp:ListItem>
                                                                                <asp:ListItem Text="18"></asp:ListItem>
                                                                                <asp:ListItem Text="19"></asp:ListItem>
                                                                                <asp:ListItem Text="20"></asp:ListItem>
                                                                                <asp:ListItem Text="21"></asp:ListItem>
                                                                                <asp:ListItem Text="22"></asp:ListItem>
                                                                                <asp:ListItem Text="23"></asp:ListItem>
                                                                            </asp:DropDownList>

                                                                            <asp:DropDownList ID="ddlDurationMinute" runat="server" class="form-control selectRemoveArrow classDurationMinute" onchange="setDurationType(this, 'Minute')">
                                                                                <asp:ListItem Text="00"></asp:ListItem>
                                                                                <asp:ListItem Text="01"></asp:ListItem>
                                                                                <asp:ListItem Text="02"></asp:ListItem>
                                                                                <asp:ListItem Text="03"></asp:ListItem>
                                                                                <asp:ListItem Text="04"></asp:ListItem>
                                                                                <asp:ListItem Text="05"></asp:ListItem>
                                                                                <asp:ListItem Text="06"></asp:ListItem>
                                                                                <asp:ListItem Text="07"></asp:ListItem>
                                                                                <asp:ListItem Text="08"></asp:ListItem>
                                                                                <asp:ListItem Text="09"></asp:ListItem>
                                                                                <asp:ListItem Text="10"></asp:ListItem>
                                                                                <asp:ListItem Text="11"></asp:ListItem>
                                                                                <asp:ListItem Text="12"></asp:ListItem>
                                                                                <asp:ListItem Text="13"></asp:ListItem>
                                                                                <asp:ListItem Text="14"></asp:ListItem>
                                                                                <asp:ListItem Text="15"></asp:ListItem>
                                                                                <asp:ListItem Text="16"></asp:ListItem>
                                                                                <asp:ListItem Text="17"></asp:ListItem>
                                                                                <asp:ListItem Text="18"></asp:ListItem>
                                                                                <asp:ListItem Text="19"></asp:ListItem>
                                                                                <asp:ListItem Text="20"></asp:ListItem>
                                                                                <asp:ListItem Text="21"></asp:ListItem>
                                                                                <asp:ListItem Text="22"></asp:ListItem>
                                                                                <asp:ListItem Text="23"></asp:ListItem>
                                                                                <asp:ListItem Text="24"></asp:ListItem>
                                                                                <asp:ListItem Text="25"></asp:ListItem>
                                                                                <asp:ListItem Text="26"></asp:ListItem>
                                                                                <asp:ListItem Text="27"></asp:ListItem>
                                                                                <asp:ListItem Text="28"></asp:ListItem>
                                                                                <asp:ListItem Text="29"></asp:ListItem>
                                                                                <asp:ListItem Text="30"></asp:ListItem>
                                                                                <asp:ListItem Text="31"></asp:ListItem>
                                                                                <asp:ListItem Text="32"></asp:ListItem>
                                                                                <asp:ListItem Text="33"></asp:ListItem>
                                                                                <asp:ListItem Text="34"></asp:ListItem>
                                                                                <asp:ListItem Text="35"></asp:ListItem>
                                                                                <asp:ListItem Text="36"></asp:ListItem>
                                                                                <asp:ListItem Text="37"></asp:ListItem>
                                                                                <asp:ListItem Text="38"></asp:ListItem>
                                                                                <asp:ListItem Text="39"></asp:ListItem>
                                                                                <asp:ListItem Text="40"></asp:ListItem>
                                                                                <asp:ListItem Text="41"></asp:ListItem>
                                                                                <asp:ListItem Text="42"></asp:ListItem>
                                                                                <asp:ListItem Text="43"></asp:ListItem>
                                                                                <asp:ListItem Text="44"></asp:ListItem>
                                                                                <asp:ListItem Text="45"></asp:ListItem>
                                                                                <asp:ListItem Text="46"></asp:ListItem>
                                                                                <asp:ListItem Text="47"></asp:ListItem>
                                                                                <asp:ListItem Text="48"></asp:ListItem>
                                                                                <asp:ListItem Text="49"></asp:ListItem>
                                                                                <asp:ListItem Text="50"></asp:ListItem>
                                                                                <asp:ListItem Text="51"></asp:ListItem>
                                                                                <asp:ListItem Text="52"></asp:ListItem>
                                                                                <asp:ListItem Text="53"></asp:ListItem>
                                                                                <asp:ListItem Text="54"></asp:ListItem>
                                                                                <asp:ListItem Text="55"></asp:ListItem>
                                                                                <asp:ListItem Text="56"></asp:ListItem>
                                                                                <asp:ListItem Text="57"></asp:ListItem>
                                                                                <asp:ListItem Text="58"></asp:ListItem>
                                                                                <asp:ListItem Text="59"></asp:ListItem>

                                                                            </asp:DropDownList>

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-xs-2">
                                                                        <asp:DropDownList ID="ddlDurationType" runat="server" CssClass="form-control classDurationType" onchange="SetddlValue(this)">
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="col-xs-3">

                                                                        <div class="input-group input-group-sm">
                                                                            <label class="control-label">
                                                                                ALL
                                                                                    <div>
                                                                                        <input type="checkbox" id="chkAll" name="All" aria-label="Checkbox for daily" onchange="MutExChkList(this)" />
                                                                                    </div>
                                                                            </label>
                                                                            <label class="control-label">
                                                                                M
                                                                    <div>
                                                                        <input type="checkbox" id="chkMon" runat="server" name="Monday" />
                                                                    </div>
                                                                            </label>
                                                                            <label class="control-label">
                                                                                T
                                                                    <div>
                                                                        <input type="checkbox" id="chkTues" runat="server" name="Tuesday" />
                                                                    </div>
                                                                            </label>
                                                                            <label class="control-label">
                                                                                W
                                                                    <div>
                                                                        <input type="checkbox" id="chkWed" runat="server" name="Wednesday" />
                                                                    </div>
                                                                            </label>
                                                                            <label class="control-label">
                                                                                TH
                                                                    <div>
                                                                        <input type="checkbox" id="chkThurs" runat="server" name="Thursday" />
                                                                    </div>
                                                                            </label>
                                                                            <label class="control-label">
                                                                                F
                                                                    <div>
                                                                        <input type="checkbox" id="chkFri" runat="server" name="Friday" />
                                                                    </div>
                                                                            </label>
                                                                            <label class="control-label">
                                                                                S
                                                                    <div>
                                                                        <input type="checkbox" id="chkSat" runat="server" name="Saturday" />
                                                                    </div>
                                                                            </label>
                                                                            <label class="control-label">
                                                                                SU
                                                                    <div>
                                                                        <input type="checkbox" id="chkSun" runat="server" name="Sunday" />
                                                                    </div>
                                                                            </label>
                                                                        </div>

                                                                    </div>

                                                                    <div class="col-xs-1">
                                                                        <asp:LinkButton CssClass="btn btn-default" runat="server" ID="btnAddDaysOfWeek" CommandName="AddDaysOfWeek">
                                                                        <i class="glyphicon glyphicon-plus"></i>
                                                                        </asp:LinkButton>
                                                                    </div>

                                                                </div>

                                                            </div>
                                                    </div>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>

                                    <hr>
                                </ItemTemplate>

                                <FooterTemplate>
                                    </div>
                                                <div class="panel-footer">
                                                    <div class="form-group row col-md-12">
                                                        <strong>Add Operating Dates</strong>
                                                    </div>

                                                    <div class="form-group row well">

                                                        <div class="col-sm-3">
                                                            <label class="control-label col-sm-8" for="chkSpecificOperatingDays">Operating Dates</label>
                                                            <asp:CheckBox ID="chkSpecificOperatingDays" runat="server" CssClass="col-sm-4" />
                                                        </div>

                                                        <div class="col-sm-4">
                                                            <label class="control-label col-sm-4" for="txtFrom">
                                                                From Date
                                                            </label>
                                                            <div class="col-sm-8">
                                                                <div class="input-group">
                                                                    <asp:TextBox ID="txtFromAdd" runat="server" CssClass="form-control" />
                                                                    <span class="input-group-btn">
                                                                        <button class="btn btn-default" type="button" id="iCalFromAdd">
                                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                                <cc1:CalendarExtender ID="calFromDateAdd" runat="server" TargetControlID="txtFromAdd" Format="dd/MM/yyyy" PopupButtonID="iCalFromAdd"></cc1:CalendarExtender>
                                                                <cc1:FilteredTextBoxExtender ID="axfte_txtFromAdd" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtFromAdd" />

                                                            </div>

                                                        </div>

                                                        <div class="col-sm-4">
                                                            <label class="control-label col-sm-4" for="txtTo">
                                                                To Date
                                                            </label>
                                                            <div class="col-sm-8">
                                                                <div class="input-group">
                                                                    <asp:TextBox ID="txtToAdd" runat="server" CssClass="form-control" />
                                                                    <span class="input-group-btn">
                                                                        <button class="btn btn-default" type="button" id="iCalToAdd">
                                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                                <cc1:CalendarExtender ID="calToDateAdd" runat="server" TargetControlID="txtToAdd" Format="dd/MM/yyyy" PopupButtonID="iCalToAdd"></cc1:CalendarExtender>
                                                                <cc1:FilteredTextBoxExtender ID="axfte_txtToAdd" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtToAdd" />
                                                            </div>
                                                        </div>

                                                        <div class="col-sm-1">
                                                            <asp:LinkButton CssClass="btn btn-default" runat="server" ID="btnAddOperatingDays" CommandName="AddOperatingDays">
                                                                <i class="glyphicon glyphicon-plus"></i>
                                                            </asp:LinkButton>
                                                        </div>

                                                    </div>

                                                </div>
                                    </div>
                                        </div>
                                </FooterTemplate>

                            </asp:Repeater>
                        </div>

                        <div class="col-md-3">
                            <asp:Repeater ID="repSupplierInformation" runat="server">
                                <HeaderTemplate>
                                    <h4><strong>Supplier Level Info</strong></h4>
                                    <table class="table table-hover table-bordered">
                                        <tr>
                                            <th>Type</th>
                                            <th>SubType</th>
                                            <th>Value</th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="word-wrap: break-word;">
                                            <em><strong><%# DataBinder.Eval(Container.DataItem, "AttributeType") %></strong></em>
                                        </td>
                                        <td style="word-wrap: break-word;">
                                            <em><%# DataBinder.Eval(Container.DataItem, "AttributeSubType") %></em>
                                        </td>
                                        <td style="word-break: break-all;">
                                            <em><%# DataBinder.Eval(Container.DataItem, "AttributeValue") %></em>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>

                    </div>
                </div>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>


