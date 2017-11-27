<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Flavours.ascx.cs" Inherits="TLGX_Consumer.controls.activity.ManageActivityFlavours.Flavours" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Src="~/controls/activity/ManageActivityFlavours/ActivityContactDetails.ascx" TagPrefix="uc1" TagName="ActivityContactDetails" %>--%>
<%@ Register Src="~/controls/activity/ManageActivityFlavours/ActivityStatus.ascx" TagPrefix="uc1" TagName="ActivityStatus" %>

<style type="text/css">
    .TextWidthMultiline {
        width: 728px;
        resize: none;
    }

    .TextWidthSingleline {
        width: 728px;
        max-height: 32px;
        resize: none;
    }

    .btn-circle {
        width: 30px;
        height: 30px;
        text-align: center;
        padding: 6px 0;
        font-size: 12px;
        line-height: 1.428571429;
        border-radius: 15px;
    }

        .btn-circle.btn-lg {
            width: 50px;
            height: 50px;
            padding: 10px 16px;
            font-size: 18px;
            line-height: 1.33;
            border-radius: 25px;
        }
</style>
<script type="text/javascript">
    $(function () {
        $('[id*=lst]').multiselect({
            includeSelectAllOption: true
        });
    });

    $(document).ready()
    {
        var countForProductType = 0;
        var countForProductSubType = 0;
        var countForDaysOfWeek = 0;
        var countForStartTime = 0;
        var countForDuration = 0;
        var countForddlSession = 0;
    };

    //For Multiple dropdown of Product Type
    function GetDdlProductType(value) {
        debugger;
        countForProductType = countForProductType + 1;
        var div = $("<div />");

        var dropdown = $("<select />").attr("id", "DynamicDdlProductType" + countForProductType).attr("class", "col-sm-8 form-control");
        dropdown.val(value);
        div.append(dropdown);

        var button = $("<input />").attr("type", "button").attr("value", "-").attr("style", "font-weight:bold;").attr("class", "btn btn-default");
        button.attr("onclick", "RemoveDdlProductType(this)");
        div.append(button);

        return div;
    }
    function AddDdlProductType() {
        debugger;
        var div = GetDdlProductType("");
        var div2 = GetDdlProductSubType("");
        $("#DynamicDdlProductTypeControls").append(div);
        //$("#DynamicDdlProdNameSubTypeControls").append(div2);

        $('#DynamicDdlProductType' + countForProductType).html($('#MainContent_Flavours_frmActivityFlavour_ddlProductType').html());
        //$('#DynamicDdlProductSubType' + countForProductSubType).html($('#MainContent_Flavours_frmActivityFlavour_ddlProdNameSubType').html());
    }
    function RemoveDdlProductType(button) {
        $(button).parent().remove();
    }
    $(function () {
        var values = eval('@Html.Raw(ViewBag.Values)');
        if (values != null) {
            $("#DynamicDdlProductTypeControls").html("");
            $(values).each(function () {
                $("#DynamicDdlProductTypeControls").append(GetDdlProductType(this));
            });
        }
    });

    function computeValueForProductType() {
        debugger;
        //var Contain = "";
        //$("#DynamicDdlProductTypeControls select").each(function () {
        //    Contain += $(this).val() + ",";
        //});
        var selects = [];
        $("#DynamicDdlProductTypeControls select").each(function () {
            selects.push($(this).val());
        });
        //$('#hdnValueWithCommaSepratedForProductType').val(Contain);
    }




    //For Multiple dropdown of Product Sub Type
    function GetDdlProductSubType(value) {
        countForProductSubType = countForProductSubType + 1;
        var div = $("<div />");

        var dropdown = $("<select />").attr("id", "DynamicDdlProductSubType" + countForProductSubType).attr("class", "col-sm-8 form-control");
        dropdown.val(value);
        div.append(dropdown);

        var button = $("<input />").attr("type", "button").attr("value", "-").attr("style", "font-weight:bold;").attr("class", "btn btn-default");
        button.attr("onclick", "RemoveDdlProductSubType(this)");
        div.append(button);

        return div;
    }
    function AddDdlProductSubType() {
        var div = GetDdlProductSubType("");
        $("#DynamicDdlProdNameSubTypeControls").append(div);

        $('#DynamicDdlProductSubType' + countForProductSubType).html($('#MainContent_Flavours_frmActivityFlavour_ddlProdNameSubType').html());
    }
    function RemoveDdlProductSubType(button) {
        $(button).parent().remove();
    }
    $(function () {
        var values = eval('@Html.Raw(ViewBag.Values)');
        if (values != null) {
            $("#DynamicDdlProdNameSubTypeControls").html("");
            $(values).each(function () {
                $("#DynamicDdlProdNameSubTypeControls").append(GetDdlProductSubType(this));
            });
        }
    });

    function computeValueForProductSubType() {
        debugger;
        //var Contain = "";
        //$("#DynamicDdlProdNameSubTypeControls select").each(function () {
        //    Contain += $(this).val() + ",";
        //});
        var selects = [];
        $("#DynamicDdlProdNameSubTypeControls select").each(function () {
            selects.push($(this).val());
        });
        //$('#hdnValueWithCommaSepratedForProductSubType').val(Contain);
    }

    function computeAll() {
        debugger;
        computeValueForProductType()
        computeValueForProductSubType();
    }




    //For Adding Multiple DaysOfWeek
    function GetDaysOfWeekCount(value) {
        debugger;
        countForDaysOfWeek = countForDaysOfWeek + 1;
        var div = $("<div />").attr("class", "form-group row");
        var div2 = $("<div />").attr("class", "col-xs-2");
        var div3 = $("<div />").attr("class", "col-xs-3");
        var div4 = $("<div />").attr("class", "col-xs-3");
        var div5 = $("<div />").attr("class", "col-xs-3");
        var div6 = $("<div />").attr("class", "col-xs-1");

        var startTextBox = $("<input />").attr("id", "txtStartTime" + countForStartTime).attr("type", "text").attr("class", "form-control");
        countForStartTime = countForStartTime + 1;

        var durationTextBox = $("<input />").attr("id", "txtDuration" + countForDuration).attr("type", "text").attr("class", "form-control");
        countForDuration = countForDuration + 1;

        var sessionDDL = $("<select />").attr("id", "DemoddlSession" + countForddlSession).attr("class", "form-control");
        sessionDDL.val(value);
        

        //var applicableOnChk = $("<input />").attr("type", "check").attr("class", "form-control");

        var removeButton = $("<input />").attr("type", "button").attr("value", "-").attr("style", "font-weight:bold;").attr("class", "btn btn-default btn-circle");
        removeButton.attr("onclick", "RemovedDaysOfWeek(this)");

        div2.append(startTextBox);
        div3.append(durationTextBox);
        div4.append(sessionDDL);
        //div5.append(sessionDDL);
        //div4.append(applicableOnChk);
        div6.append(removeButton);

        div.append(div2, div3, div4, div6);

        $("#daysOfWeek").append(div);
        $('#DemoddlSession' + countForddlSession).html($('#MainContent_Flavours_frmActivityFlavour_ddlSession').html());
        countForddlSession = countForddlSession + 1;

        return div;
    }
    function AddDaysOfWeek() {
        debugger;
        var div = GetDaysOfWeekCount("");
        //$('#txtStartTime' + countForStartTime).html($('#MainContent_Flavours_frmActivityFlavour_txtStartTime').html());
        //$('#txtDuration' + countForDuration).html($('#MainContent_Flavours_frmActivityFlavour_txtDuration').html());
        //$('#DemoddlSession' + countForddlSession).html($('#MainContent_Flavours_frmActivityFlavour_ddlSession').html());

    }
    function RemovedDaysOfWeek(removeButton) {
        debugger;
        $(removeButton).parent().parent().remove();
    }
    $(function () {
        var values = eval('@Html.Raw(ViewBag.Values)');
        if (values != null) {
            $("#daysOfWeek").html("");
            $(values).each(function () {
                $("#daysOfWeek").append(GetDaysOfWeekCount(this));
            });
        }
    });
</script>


<div class="container">
    <div class="row">
        <div class="col-lg-12">
            <div id="dvMsg" runat="server" style="display: none;"></div>
        </div>
    </div>
</div>

<asp:FormView ID="frmActivityFlavour" runat="server" DataKeyNames="Activity_Flavour_Id" DefaultMode="Edit" OnItemCommand="frmActivityFlavour_ItemCommand">
    <HeaderTemplate>
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="ProductOverView" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                </div>
            </div>
        </div>
    </HeaderTemplate>

    <EditItemTemplate>
        <div class="row">
            <div class="col-lg-12 row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">Product Name</div>
                        <div class="panel-body">
                            <div class="form-group row" style="display: none">
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtActivity_Flavour_Id" runat="server" Text='<%# Bind("Activity_Flavour_Id") %>' class="form-control" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="control-label-mand col-sm-4" for="txtProductName">
                                    Product Name
                                    <asp:RequiredFieldValidator ID="vtxtProductName" runat="server" ErrorMessage="Please enter Hotel Name" Text="*" ControlToValidate="txtProductName" CssClass="text-danger" ValidationGroup="ProductOverView"></asp:RequiredFieldValidator>
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtProductName" runat="server" Text='<%# Bind("ProductName") %>' TextMode="MultiLine" Rows="1" CssClass="TextWidthSingleline form-control" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="control-label col-sm-4" for="txtShortDescription">
                                    Short Description
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtShortDescription" runat="server" TextMode="MultiLine" CssClass="form-control TextWidthMultiline" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="control-label col-sm-4" for="txtLongDescription">
                                    Long Description
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtLongDescription" runat="server" TextMode="MultiLine" CssClass="form-control TextWidthMultiline" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-12 row">
                <div class="col-lg-8">
                    <div class="panel panel-default">
                        <div class="panel-heading">TLGX Classification Mapping</div>
                        <div class="panel-body">
                            <asp:UpdatePanel ID="up2" runat="server">
                                <ContentTemplate>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-2" for="ddlCountry">
                                            Country
                                        </label>
                                        <asp:Label ID="lblSuppCountry" runat="server" class="control-label col-sm-2"></asp:Label>
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
                                        <asp:Label ID="lblSuppCity" runat="server" class="control-label col-sm-2"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-2" for="ddlProdCategory">
                                            Category
                                    <asp:RequiredFieldValidator ID="vddlProdCategory" runat="server" ErrorMessage="Please select Product Category" Text="*" ControlToValidate="ddlProdCategory" InitialValue="0" CssClass="text-danger" ValidationGroup="ProductOverView"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-2"></div>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlProdCategory" runat="server" CssClass="form-control" AppendDataBoundItems="true" Enabled="false">
                                                <asp:ListItem>Activity</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-2" for="ddlProdcategorySubType">
                                            Activity Category 
                                     <asp:RequiredFieldValidator ID="vddlProdcategorySubType" runat="server" ErrorMessage="Please select Activity Category" Text="*" ControlToValidate="ddlProdcategorySubType" InitialValue="0" CssClass="text-danger" ValidationGroup="ProductOverView"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-2"></div>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlProdcategorySubType" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlProdcategorySubType_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-2" for="ddlProductType">
                                            Product Type
                                        </label>
                                        <asp:Label ID="lblSuppProductType" runat="server" class="control-label col-sm-2"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:HiddenField ID="hdnValueWithCommaSepratedForProductType" runat="server" />
                                            <div id="DynamicDdlProductTypeControls">
                                                <asp:DropDownList ID="ddlProductType" runat="server" CssClass="col-sm-8 form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                </asp:DropDownList>

                                                <button class="btn btn-default" id="btnAdd" type="button" onclick="AddDdlProductType()">
                                                    <i class="glyphicon glyphicon-plus"></i>
                                                </button>
                                                <br />
                                                <!--Dropdowns will be added here -->
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <label class="control-label col-sm-2" for="ddlProdNameSubType">
                                            Product SubType
                                    <asp:RequiredFieldValidator ID="vddlProdNameSubType" runat="server" ErrorMessage="Please select Product  Sub Type" Text="*" ControlToValidate="ddlProdNameSubType" InitialValue="0" CssClass="text-danger" ValidationGroup="ProductOverView"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:Label ID="lblSuppProdNameSubType" runat="server" class="control-label col-sm-2"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:HiddenField ID="hdnValueWithCommaSepratedForProductSubType" runat="server" />
                                            <div id="DynamicDdlProdNameSubTypeControls">
                                                <asp:DropDownList ID="ddlProdNameSubType" runat="server" CssClass="col-sm-8 form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                </asp:DropDownList>
                                                <button class="btn btn-default" id="btnAddProdSubType" type="button" onclick="AddDdlProductSubType()">
                                                    <i class="glyphicon glyphicon-plus"></i>
                                                </button>
                                                <br />
                                                <!--Dropdowns will be added here -->
                                            </div>
                                        </div>
                                    </div>

                                    <button id="btndemo" runat="server" onclick="computeAll();" class="btn btn-default">Click</button>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4">
                    <div class="panel panel-default">
                        <div class="panel-heading">Key Facts</div>
                        <div class="panel-body">
                            <div class="form-group row">
                                <div class="col-sm-6 row">
                                    <label class="control-label col-sm-4" for="chklstSuitableFor">Suitable For</label>
                                    <asp:Label ID="lblSuppSuitableFor" runat="server" class="control-label col-sm-2"></asp:Label>
                                </div>
                                <div class="col-sm-6" style="padding: 0px 10px 0px 5px;">
                                    <asp:CheckBoxList ID="chklstSuitableFor" runat="server" RepeatLayout="Table" RepeatColumns="2" CssClass="row"></asp:CheckBoxList>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-sm-6 row">
                                    <label class="control-label col-sm-4" for="chklstPhysicalIntensity">Physical Intensity</label>
                                    <asp:Label ID="lblSuppPhysicalIntensity" runat="server" class="control-label col-sm-2"></asp:Label>
                                </div>
                                <div class="col-sm-6" style="padding: 0px 10px 0px 5px;">
                                    <asp:CheckBoxList ID="chklstPhysicalIntensity" runat="server" RepeatLayout="Table" RepeatColumns="2" CssClass="row"></asp:CheckBoxList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="pull-left">
                        <asp:LinkButton ID="btnSave" runat="server" CausesValidation="True" CommandName="SaveProduct" Text="Update Flavour Info" CssClass="btn btn-primary btn-sm" ValidationGroup="ProductOverView" />
                        <asp:LinkButton ID="btnCancel" runat="server" CausesValidation="False" CommandName="CancelProduct" Text="Cancel" CssClass="btn btn-primary btn-sm" />
                    </div>
                </div>
            </div>

            <div class="col-lg-12 row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">Duration/Session</div>
                        <div class="panel-body">
                            <div class="form-group row">
                                <div class="col-sm-6 row">
                                    <label class="control-label col-sm-12" for="lblSuppStartTime">Supplier Start Time</label>
                                </div>
                                <div class="col-sm-6 row">
                                    <asp:Label ID="lblSuppStartTime" runat="server" class="control-label col-sm-12"></asp:Label>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-sm-6 row">
                                    <label class="control-label col-sm-12" for="lblSuppDuration">Supplier Duration</label>
                                </div>
                                <div class="col-sm-6 row">
                                    <asp:Label ID="lblSuppDuration" runat="server" class="control-label col-sm-12"></asp:Label>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-sm-6 row">
                                    <label class="control-label col-sm-12" for="lblSuppOperatingDays">Operating Days</label>
                                </div>
                                <div class="col-sm-6 row">
                                    <asp:Label ID="lblSuppOperatingDays" runat="server" class="control-label col-sm-12"></asp:Label>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-sm-4 row">
                                    <label class="control-label col-sm-6" for="chkSpecificOperatingDays">Specific Operating Days</label>
                                    <asp:CheckBox ID="chkSpecificOperatingDays" runat="server" CssClass="col-sm-6" />
                                </div>

                                <div class="col-sm-4">
                                    <label class="control-label col-sm-6" for="txtFrom">
                                        From Date
                                        <%--<asp:RequiredFieldValidator ID="vldtxtFrom" runat="server" ControlToValidate="txtFrom" ErrorMessage="Please select from date" Text="*" ValidationGroup="vldgrpDescriptions" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
                                    </label>
                                    <div class="col-sm-6">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" />
                                            <span class="input-group-btn">
                                                <button class="btn btn-default" type="button" id="iCalFrom">
                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                </button>
                                            </span>
                                        </div>
                                        <cc1:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy" PopupButtonID="iCalFrom"></cc1:CalendarExtender>
                                        <cc1:FilteredTextBoxExtender ID="axfte_txtFrom" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtFrom" />
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <label class="control-label col-sm-6" for="txtTo">
                                        To Date
                                        <%--<asp:RequiredFieldValidator ID="vldtxtTo" runat="server" ControlToValidate="txtTo" ErrorMessage="Please select To date" Text="*" ValidationGroup="vldgrpDescriptions" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
                                    </label>
                                    <div class="col-sm-6">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" />
                                            <span class="input-group-btn">
                                                <button class="btn btn-default" type="button" id="iCalTo">
                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                </button>
                                            </span>
                                        </div>
                                        <cc1:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy" PopupButtonID="iCalTo"></cc1:CalendarExtender>
                                        <cc1:FilteredTextBoxExtender ID="axfte_txtTo" runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtTo" />
                                    </div>
                                </div>

                            </div>

                            <div class="form-group row">
                                <div class="col-sm-12 row">
                                    <label class="control-label col-sm-2" for="txtFrequency">Frequency</label>
                                    <div class="col-sm-6 row">
                                        <label class="radio-inline">
                                            <input type="radio" id="rdoDaysOfWeek" runat="server" name="DaysOfWeek">Days Of Week
                                        </label>
                                        <label class="radio-inline">
                                            <input type="radio" id="rdoMonthly" runat="server" name="Monthly">Monthly
                                        </label>
                                        <label class="radio-inline">
                                            <input type="radio" id="rdoSpecificDates" runat="server" name="SpecificDates">Specific Dates
                                        </label>
                                    </div>
                                    <%--<div class="col-sm-8">
                                        <asp:RadioButtonList ID="rdoFrequency" runat="server" CssClass="radio-inline" RepeatDirection="Horizontal">
                                            <asp:ListItem><b>Days Of Week</b></asp:ListItem>
                                            <asp:ListItem><b>Monthly</b></asp:ListItem>
                                            <asp:ListItem><b>Specific Dates</b></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>--%>
                                </div>
                            </div>

                            <%--<div class="form-group">
                                <div class="panel panel-default">
                                    <div class="panel-heading">Days Of Week</div>
                                    <div class="panel-body">
                                        <div class="form-group row">
                                            <div class="col-xs-2 row">
                                                <br />
                                                <label class="control-label col-sm-12" for="lblStartTime"></label>
                                                <div>
                                                    <asp:TextBox ID="txtHrs" runat="server" CssClass="form-control col-sm-6"></asp:TextBox>
                                                    <asp:TextBox ID="txtMin" runat="server" CssClass="form-control col-sm-6"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>

                            <div class="form-group">
                                <div class="panel panel-default">
                                    <div class="panel-heading">Days Of Week</div>
                                    <div class="panel-body">
                                        <div id="daysOfWeek">
                                            <div class="form-group row">

                                                <%--<div class="form-group">
                                                <label class="control-label-mand col-sm-6" for="txtCheckinTime">
                                                    Check In
                                    <asp:RequiredFieldValidator ID="vtxtCheckinTime" runat="server" ErrorMessage="Please enter Check In Time" Text="*" ControlToValidate="txtCheckinTime" CssClass="text-danger" ValidationGroup="HotelOverView"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revtxtCheckinTime" runat="server" ErrorMessage="Invalid Check In Time." Text="*" ControlToValidate="txtCheckinTime" CssClass="text-danger" ValidationGroup="HotelOverView" ValidationExpression="^(?:[01][0-9]|2[0-3]):[0-5][0-9]$"></asp:RegularExpressionValidator>
                                                </label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtCheckinTime" runat="server" Text='<%# Bind("CheckInTime") %>' class="form-control" />
                                                    
                                                </div>
                                            </div>--%>

                                                <div class="col-xs-2">
                                                    <label class="control-label-mand" for="txtStartTime">
                                                        Start Time
                                                     <asp:RequiredFieldValidator ID="vtxtStartTime" runat="server" ErrorMessage="Please enter Start Time" Text="*" ControlToValidate="txtStartTime" CssClass="text-danger" ValidationGroup=""></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revtxtCheckinTime" runat="server" ErrorMessage="Invalid Start Time." Text="*" ControlToValidate="txtStartTime" CssClass="text-danger" ValidationGroup="" ValidationExpression="^(?:[01][0-9]|2[0-3]):[0-5][0-9]$"></asp:RegularExpressionValidator>
                                                    </label>
                                                    <asp:TextBox ID="txtStartTime" runat="server" Text='' CssClass="form-control"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="txtStartTime_MaskEditExtender" runat="server" AcceptAMPM="false"
                                                        Mask="99:99" MaskType="Time" PromptCharacter="_" TargetControlID="txtStartTime"
                                                        UserTimeFormat="TwentyFourHour" InputDirection="LeftToRight"></cc1:MaskedEditExtender>
                                                </div>

                                                <div class="col-xs-3">
                                                    <label class="control-label-mand" for="txtDuration">
                                                        Duration
                                                    <asp:RequiredFieldValidator ID="vtxtDuration" runat="server" ErrorMessage="Please Duaration" Text="*" ControlToValidate="txtDuration" CssClass="text-danger" ValidationGroup=""></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revtxtDuration" runat="server" ErrorMessage="Invalid Duration." Text="*" ControlToValidate="txtDuration" CssClass="text-danger" ValidationGroup="" ValidationExpression="^(?:[01][0-9]|2[0-3]):[0-5][0-9]$"></asp:RegularExpressionValidator>
                                                    </label>
                                                    <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="txtDuration_MaskEditExtender" runat="server" AcceptAMPM="false"
                                                        Mask="99:99:99" MaskType="Time" PromptCharacter="_" TargetControlID="txtDuration"
                                                        UserTimeFormat="TwentyFourHour" InputDirection="LeftToRight"></cc1:MaskedEditExtender>
                                                </div>

                                                <div class="col-xs-3">
                                                    <label class="control-label-mand" for="txtSession">
                                                        Session
                                                    </label>
                                                    <%--<asp:TextBox ID="txtSession" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtSession" />--%>
                                                    <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>

                                                <div class="col-xs-3">
                                                    <label class="control-label-mand" for="txtSession">
                                                        Applicable On
                                                    </label>
                                                    <%--<asp:TextBox ID="txtSession" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtSession" />--%>
                                                    <div>
                                                        <label class="control-label">
                                                            M
                                                            <div>
                                                                <input type="checkbox" id="chkMonday" runat="server" name="Monday">
                                                            </div>
                                                        </label>
                                                        <label class="control-label">
                                                            T
                                                            <div>
                                                                <input type="checkbox" id="chkTues" runat="server" name="Tuesday">
                                                            </div>
                                                        </label>
                                                        <label class="control-label">
                                                            W
                                                            <div>
                                                                <input type="checkbox" id="chkWed" runat="server" name="Wednesday">
                                                            </div>
                                                        </label>
                                                        <label class="control-label">
                                                            TH
                                                            <div>
                                                                <input type="checkbox" id="chkThurs" runat="server" name="Thursday">
                                                            </div>
                                                        </label>
                                                        <label class="control-label">
                                                            F
                                                            <div>
                                                                <input type="checkbox" id="chkFri" runat="server" name="Friday">
                                                            </div>
                                                        </label>
                                                        <label class="control-label">
                                                            S
                                                            <div>
                                                                <input type="checkbox" id="chkSat" runat="server" name="Saturday">
                                                            </div>
                                                        </label>
                                                        <label class="control-label">
                                                            SU
                                                            <div>
                                                                <input type="checkbox" id="chkSun" runat="server" name="Sunday">
                                                            </div>
                                                        </label>
                                                    </div>
                                                    <%--<asp:CheckBoxList ID="chkDaysApplicable" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="M  "></asp:ListItem>
                                                        <asp:ListItem>T</asp:ListItem>
                                                        <asp:ListItem>W</asp:ListItem>
                                                        <asp:ListItem>T</asp:ListItem>
                                                        <asp:ListItem>F</asp:ListItem>
                                                        <asp:ListItem>S</asp:ListItem>
                                                        <asp:ListItem>S</asp:ListItem>
                                                    </asp:CheckBoxList>--%>
                                                </div>

                                                <div class="col-xs-1">
                                                    <%--<asp:TextBox ID="txtSession" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtSession" />--%>
                                                    <%--<asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                    <%--<asp:Button ID="btnAddSession" runat="server" CssClass="btn btn-default form-control button5" Text="+" />--%>
                                                    </br>
                                                    <button type="button" class="btn btn-default btn-circle" onclick="AddDaysOfWeek();"><i class="glyphicon glyphicon-plus"></i></button>
                                                    <%--<div></br></div>--%>
                                                    <%--<button type="button" class="btn btn-default btn-circle"><i class="glyphicon glyphicon-minus"></i></button>--%>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </div>
    </EditItemTemplate>
</asp:FormView>

<%--<asp:ListBox ID="lstboxProductType" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
    <asp:Button ID="btnAddToListBox" runat="server" CssClass="btn btn-primary btn-sm" CommandName="FillListBox2" Text="v" />
    <asp:Button ID="btnRemoveFromListBox" runat="server" CssClass="btn btn-primary btn-sm" CommandName="EmptyListBox2" Text="^" />
    <asp:Literal ID="lblmsg2" runat="server" />
   <asp:ListBox ID="lstboxSelectedProductType" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>--%>


