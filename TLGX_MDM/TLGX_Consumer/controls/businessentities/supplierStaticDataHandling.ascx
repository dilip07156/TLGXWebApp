<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="supplierStaticDataHandling.ascx.cs" Inherits="TLGX_Consumer.controls.businessentities.supplierStaticDataHandling" %>
<link href="../../Scripts/bootStrap-TimePicker/bootstrap-datetimepicker.css" rel="stylesheet" />
<script src="../../Scripts/bootStrap-TimePicker/bootstrap-timepicker.js"></script>
<link href="../../Content/jquery-cron-quartz.css" rel="stylesheet" />
<script src="../../Scripts/jquery-cron-quartz.js"></script>
<style type="text/css">
    .radioButtonList label {
        display: inline;
        text-align: right;
        padding: 5px;
        vertical-align: text-bottom;
    }

    .table-borderless td,
    .table-borderless th {
        border: 0;
    }

    .Divdisplay {
        display: none;
    }

    .checkbox label {
        display: inline;
        text-align: right;
        padding: 5px;
        vertical-align: text-bottom;
    }
    .strongtext{
        font-weight:bold;
    }
</style>
<script type="text/javascript">
    var dataexist = false; 
    $(document).ready(function () {

        cronfunc();
        //LoadFrequencyAndIsAPIXMLChangeData();
        LoadFrequencyAndIsAPIXMLChangeDataForCron();               
    });



    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        //LoadFrequencyAndIsAPIXMLChangeData();

       // cronfunc();
        LoadFrequencyAndIsAPIXMLChangeDataForCron();
    });

    function GetSupplierScheduleDetailOnEdit(strActiveFrequencyType, DateOfMonth, WeekOfMonth, Recur_No, MonthweekList, MonthWeekYear, Monthval, starthour, startminute) {
        
        $('#suppliershcedular').css('display', 'block');
        $('#msgAlert').css('display', 'none');
        //  cronfunc();
        $('#example1-cron').cronBuilder();
        if (strActiveFrequencyType == 'H') {
            $('#ddl_period').val(strActiveFrequencyType);
            var $selector = $('.cron-period-select').parent();
            var $hourlyEl = $selector.siblings('div.cron-hourly');
            $hourlyEl.show()
                .find("input[name=hourlyType][value=every]").prop('checked', true);
            $hourlyEl.find("select.cron-hourly-hour").val('12');
            $selector.siblings('div.cron-start-time').hide();
            $('#ddl_hourly').val(Recur_No);
        }
        else if (strActiveFrequencyType == 'D') {
            $('#ddl_period').val(strActiveFrequencyType);
            var $selector = $('.cron-period-select').parent();
            var $dailyEl = $selector.siblings('div.cron-daily');
            $dailyEl.show()
                .find("input[name=dailyType][value=every]").prop('checked', true);
            $('#ddl_daily').val(Recur_No);
        }
        else if (strActiveFrequencyType == 'W') {
            $('#ddl_period').val(strActiveFrequencyType);
            var $selector = $('.cron-period-select').parent();
            $selector.siblings('div.cron-weekly')
                .show()
                .find("input[type=checkbox]").prop('checked', false);
            var selected = [0, 0, 1, 1, 0, 0, 0];
            selected = Monthval.split(',');
            var i = 0;
            $('div.cron-weekly input[type=checkbox]').each(function () {
                $(this).prop('checked', parseInt(selected[i]));
                i++;
            });

        }
        else if (strActiveFrequencyType == 'M') {
            $('#ddl_period').val(strActiveFrequencyType);
            var $selector = $('.cron-period-select').parent();
            var $monthlyEl = $selector.siblings('div.cron-monthly');
            $monthlyEl.show()
            //.find("input[name=monthlyType][value=byDay]").prop('checked', true);
            if (Monthval == "D") {
                $('input[name=monthlyType][value=byDay]').prop('checked', true);
                $('#ddl_monthly_day').val(DateOfMonth);
                $('#ddl_monthly_monthly').val(Recur_No);
            }
            else {
                $('input[name=monthlyType][value=byWeek]').prop('checked', true);
                $('#ddl_monthly_nthday').val(WeekOfMonth);
                $('#ddl_monthly_day_of_week').val(DateOfMonth);
                $('#ddl_monthly_month_by_week').val(Recur_No);
            }
        }
        else if (strActiveFrequencyType == 'Y') {
            $('#ddl_period').val(strActiveFrequencyType);
            var $selector = $('.cron-period-select').parent();
            var $yearlyEl = $selector.siblings('div.cron-yearly');
            $yearlyEl.show()
                .find("input[name=yearlyType][value=byDay]").prop('checked', true);
            if (Monthval == "M") {
                $('input[name=yearlyType][value=byDay]').prop('checked', true);
                $('#ddl_yearly_Month').val(DateOfMonth);
                $('#ddl_yearly_day').val(WeekOfMonth);
            }
            else if (Monthval == "W") {
                $('input[name=yearlyType][value=byWeek]').prop('checked', true);
                $('#ddl_yearly_nthday').val(WeekOfMonth);
                $('#ddl_yearly_day_od_week').val(MonthweekList);
                $('#ddl_yearly_month_by_week').val(MonthWeekYear);
            }
        }
        $('#ddl_clock_hour').val(parseInt(starthour));
        $('#ddl_clock_minute').val(parseInt(startminute));
    }

    function cronfunc() {
        $('#example1-cron').cronBuilder();
        $('#example1-btn').click(function () {
            var current = $('#example1-cron').data('cronBuilder').getExpression();
            $('#example1-result').text(current);
        })

    }

    function EditSchedular() {

        var $selector = $('.cron-period-select').parent();
        var $monthlyEl = $selector.siblings('div.cron-monthly');
        $monthlyEl.show()
            .find("input[name=monthlyType][value=byDay]").prop('checked', true);
    }

    function alertdivMessage(msg) {
        $('#MainContent_manageSupplier_supplierStaticDataHandling_msgAlert').show();
        $('#MainContent_manageSupplier_supplierStaticDataHandling_msgAlert').addClass('alert alert-danger alert-dismissable');
        $('#MainContent_manageSupplier_supplierStaticDataHandling_msgAlert').css({ 'font-weight': 'Bold' });
        $('#MainContent_manageSupplier_supplierStaticDataHandling_msgAlert').text(msg);

    }

    function CheckExistingData() {

        //
        
        var checkBoxList = document.getElementById("<%=Checkentitysequence.ClientID %>");
        var checkboxes = checkBoxList.getElementsByTagName("input");
        var isValid = false;
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].checked) {
                isValid = true;
                break;
            }
        }
        if (!isValid) {
            alertdivMessage('Please select atleast one entity');
            return false;
        }
        //
        
        var current = $('#example1-cron').data('cronBuilder').getExpression();
        $('#cron_expression').val(current);
        var SupplierScheduleId = $('#SupplierScheduleId').val();
        if (SupplierScheduleId == "") {
            var urlParams = new URLSearchParams(location.search);
            var supplierid = urlParams.get('Supplier_Id');
            var jsonObj = [];
            var checked = [];

            var item = {};
            item.Suppllier_ID = supplierid;

            var checked_checkboxes = $("[id*=Checkentitysequence] input:checked");
            checked_checkboxes.each(function () {
                checked.push($(this).first().next("label").text());
            });

            item.Entities = checked;
            jsonObj.push(item);
            $.ajax({
                async: false,
                type: 'POST',
                url: '../../../Service/CheckExistingSupplierData.ashx',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(jsonObj),
                responseType: "json",
                success: function (result) {
                    if (result == false) {
                        alertdivMessage('Please select other entity');                       
                        dataexist = result;                        
                    }
                    else {
                        dataexist = true;
                        
                    }
                },
                failure: function () {
                    dataexist = false;
                    
                }
            });
        }
        else {
            dataexist = true;
        }
        return dataexist;
    }

   
    function LoadFrequencyAndIsAPIXMLChangeData() {
        var Freq = { 'H': 'Hourly', 'D': 'Daily', 'W': 'Weekly', 'M': 'Monthly', 'Y': 'Yearly' };
        $('#rdbFrequency input').change(function () {

            var FrequencyType = Freq[$(this).val()];
            $("div.activediv").addClass("Divdisplay").removeClass("activediv");
            $("#MainContent_manageSupplier_supplierStaticDataHandling_div" + FrequencyType).removeClass("Divdisplay").addClass("activediv");
            //Validation Enabale Disable on selection
            var list = [];
            var validationlist = [];

            validationlist.push("rfvRecur_Hourly");
            validationlist.push("rfvOccur_Daily");
            validationlist.push("rfvRecur_Weekly");
            validationlist.push("rfvDayOfEvery_Monthly");
            validationlist.push("rfvDayOf_Monthly");
            validationlist.push("rfvOccurEvery_Month");
            validationlist.push("rfvMonthDay_Year");
            validationlist.push("rfvRecurEvery_Year");


            for (var i = 0, size = validationlist.length; i < size; i++) {
                var itemval = validationlist[i];
                var item = document.getElementById(itemval);
                ValidatorEnable(item, false);
            }

            //array.push();
            if (FrequencyType == "Hourly") {
                list = [];
                //var rfvRecur_Hourly = $("#rfvRecur_Hourly");
                list.push("rfvRecur_Hourly");
            }
            else if (FrequencyType == "Daily") {
                list = [];
                //  var rfvOccur_Daily = $("#rfvOccur_Daily");
                list.push("rfvOccur_Daily");


            }
            else if (FrequencyType == "Weekly") {
                list = [];
                //   var rfvRecur_Weekly = $("#rfvRecur_Weekly");
                list.push("rfvRecur_Weekly");

            }
            else if (FrequencyType == "Monthly") {
                list = [];
                //  var rfvOccurEvery_Month = $("#rfvOccurEvery_Month");
                list.push("rfvOccurEvery_Month");
                //  var rfvDayOfEvery_Monthly = $("#rfvDayOfEvery_Monthly");
                list.push("rfvDayOfEvery_Monthly");
                //  var rfvDayOf_Monthly = $("#rfvDayOf_Monthly");
                list.push("rfvDayOf_Monthly");

            }
            else if (FrequencyType == "Yearly") {
                list = [];
                // var rfvMonthDay_Year = $("#rfvMonthDay_Year");
                list.push("rfvMonthDay_Year");
                //  var rfvRecurEvery_Year = $("#rfvRecurEvery_Year");
                list.push("rfvRecurEvery_Year");
            }

            //To handle Yearly --Always disable first
            for (var i = 0, size = list.length; i < size; i++) {
                var itemval = list[i];
                //                if (itemval == "rfvOccurEvery_Month" && $('#rblDays_Monthly')[0].checked) {
                //                    continue;
                //                }
                //                if (itemval == "rfvDayOfEvery_Monthly" && $('#rbThe_Montly')[0].checked) {
                //                    continue;
                //                }
                //                if (itemval == "rfvDayOf_Monthly" && $('#rbThe_Montly')[0].checked) {
                //continue;
                //                }
                //                if (itemval == rfvDayOf_Monthly && !$('#rblDays_Monthly')[0].checked) {
                //                }
                var item = document.getElementById(itemval);
                ValidatorEnable(item, true);
            }

        });

        $('#rdbtnIsAPIXMLSupplier').change(function () {
            var checked_radio = $("[id*=rdbtnIsAPIXMLSupplier] input:checked");
            EnableDisableControls(checked_radio.val());
        });
    }


    function LoadFrequencyAndIsAPIXMLChangeDataForCron() {
        var Freq = { 'H': 'Hourly', 'D': 'Daily', 'W': 'Weekly', 'M': 'Monthly', 'Y': 'Yearly' };
        $('#rdbFrequency input').change(function () {

            var FrequencyType = Freq[$(this).val()];
            $("div.activediv").addClass("Divdisplay").removeClass("activediv");
            $("#MainContent_manageSupplier_supplierStaticDataHandling_div" + FrequencyType).removeClass("Divdisplay").addClass("activediv");
            //Validation Enabale Disable on selection
            var list = [];
            var validationlist = [];

            validationlist.push("rfvRecur_Hourly");
            validationlist.push("rfvOccur_Daily");
            validationlist.push("rfvRecur_Weekly");
            validationlist.push("rfvDayOfEvery_Monthly");
            validationlist.push("rfvDayOf_Monthly");
            validationlist.push("rfvOccurEvery_Month");
            validationlist.push("rfvMonthDay_Year");
            validationlist.push("rfvRecurEvery_Year");


            for (var i = 0, size = validationlist.length; i < size; i++) {
                var itemval = validationlist[i];
                var item = document.getElementById(itemval);
                ValidatorEnable(item, false);
            }

            //array.push();
            if (FrequencyType == "Hourly") {
                list = [];
                //var rfvRecur_Hourly = $("#rfvRecur_Hourly");
                list.push("rfvRecur_Hourly");
            }
            else if (FrequencyType == "Daily") {
                list = [];
                //  var rfvOccur_Daily = $("#rfvOccur_Daily");
                list.push("rfvOccur_Daily");


            }
            else if (FrequencyType == "Weekly") {
                list = [];
                //   var rfvRecur_Weekly = $("#rfvRecur_Weekly");
                list.push("rfvRecur_Weekly");

            }
            else if (FrequencyType == "Monthly") {
                list = [];
                //  var rfvOccurEvery_Month = $("#rfvOccurEvery_Month");
                list.push("rfvOccurEvery_Month");
                //  var rfvDayOfEvery_Monthly = $("#rfvDayOfEvery_Monthly");
                list.push("rfvDayOfEvery_Monthly");
                //  var rfvDayOf_Monthly = $("#rfvDayOf_Monthly");
                list.push("rfvDayOf_Monthly");

            }
            else if (FrequencyType == "Yearly") {
                list = [];
                // var rfvMonthDay_Year = $("#rfvMonthDay_Year");
                list.push("rfvMonthDay_Year");
                //  var rfvRecurEvery_Year = $("#rfvRecurEvery_Year");
                list.push("rfvRecurEvery_Year");
            }

            //To handle Yearly --Always disable first
            for (var i = 0, size = list.length; i < size; i++) {
                var itemval = list[i];
                //                if (itemval == "rfvOccurEvery_Month" && $('#rblDays_Monthly')[0].checked) {
                //                    continue;
                //                }
                //                if (itemval == "rfvDayOfEvery_Monthly" && $('#rbThe_Montly')[0].checked) {
                //                    continue;
                //                }
                //                if (itemval == "rfvDayOf_Monthly" && $('#rbThe_Montly')[0].checked) {
                //continue;
                //                }
                //                if (itemval == rfvDayOf_Monthly && !$('#rblDays_Monthly')[0].checked) {
                //                }
                var item = document.getElementById(itemval);
                ValidatorEnable(item, true);
            }

        });

        $('#rdbtnIsAPIXMLSupplier').change(function () {
            var checked_radio = $("[id*=rdbtnIsAPIXMLSupplier] input:checked");
            EnableDisableControls(checked_radio.val());
        });
    }


    function EnableDisableControls(flag) {
        $('#MainContent_manageSupplier_supplierStaticDataHandling_rdbIsUpdateFrequencyRequired').enabled = flag;
    }
    //For Year Tab
    function OptionYear() {
        var rbDay_Year = $('#rbDay_Year')[0].checked;
        if (rbDay_Year) {
            $("#ddlMonths_Year").prop("disabled", false);
            $('#txtMonthDay_Year').prop("disabled", false);
            $("#ddlWeek_Year").prop("disabled", true);
            $("#ddlMonthWeekList_Year").prop("disabled", true);
            $("#ddlYearMonth_Year").prop("disabled", true);
        }
        var rbOnthe_Year = $('#rbOnthe_Year')[0].checked;
        if (rbOnthe_Year) {
            $("#ddlMonths_Year").prop("disabled", true);
            $('#txtMonthDay_Year').prop("disabled", true);
            $("#ddlWeek_Year").prop("disabled", false);
            $("#ddlMonthWeekList_Year").prop("disabled", false);
            $("#ddlYearMonth_Year").prop("disabled", false);
        }

    }
    function OptionMonth() {
        var rblDays_Monthly = $('#rblDays_Monthly')[0].checked;
        if (rblDays_Monthly) {
            $("#ddlTheSequencyWeek_Month").prop("disabled", true);
            $('#ddlTheDaysOf_Month').prop("disabled", true);
            $('#txtOccurEvery_Month').prop("disabled", true);
            $("#txtDayOf_Monthly").prop("disabled", false);
            $("#txtDayOfEvery_Monthly").prop("disabled", false);
        }
        var rbThe_Montly = $('#rbThe_Montly')[0].checked;
        if (rbThe_Montly) {
            $("#ddlTheSequencyWeek_Month").prop("disabled", false);
            $('#ddlTheDaysOf_Month').prop("disabled", false);
            $('#txtOccurEvery_Month').prop("disabled", false);
            $("#txtDayOf_Monthly").prop("disabled", true);
            $("#txtDayOfEvery_Monthly").prop("disabled", true);
        }

    }
    function NewSupplierSchedular() {
        $('#suppliershcedular').css('display', 'block');
        $('#MainContent_manageSupplier_supplierStaticDataHandling_msgAlert').css('display', 'none');
        cronfunc();
        LoadFrequencyAndIsAPIXMLChangeDataForCron();
    }
   
</script>
<asp:UpdatePanel ID="supplerStaticData" runat="server" UpdateMode="Conditional">
    <ContentTemplate>

        <div class="panel panel-default">
            
                <div class="pull-right">
                    <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnNewCreate_Click" ID="btnNewCreate" Text="Create New Supplier Schedular"></asp:LinkButton>
                </div>
            <div class="panel-heading">
                <h4 class="panel-title"><a data-toggle="collapse" data-parent="#accordion" href="#collapseSearch">Manage Supplier Schedule</a></h4>

            </div>
        <asp:GridView ID="grdSupplierSchedule" runat="server" AllowCustomPaging="true" AllowPaging="true" AutoGenerateColumns="False" DataKeyNames="SupplierScheduleID"
            CssClass="table table-hover table-striped table-bordered table-fixed" EmptyDataText="No Data Found" OnRowCommand="grdSupplierSchedule_RowCommand">
            <Columns>
                <asp:BoundField DataField="Entity" HeaderText="Entity" SortExpression="Entity" />
                <asp:BoundField DataField="FrequencyTypeCode" HeaderText="FrequencyTypeCode" SortExpression="FrequencyTypeCode" />
                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="false" CommandName="Select" CssClass="btn btn-default"
                            CommandArgument='<%# Bind("SupplierScheduleID") %>'>
                                                            <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>&nbsp Edit
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName='<%# "SoftDelete" %>'
                            CssClass="btn btn-default" CommandArgument='<%# Bind("SupplierScheduleID") %>'>
                                                                <span aria-hidden="true" class='<%# "glyphicon glyphicon-remove" %>'</span>
                                                           
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pagination-ys" />
        </asp:GridView>

        </div>
        <div id="suppliershcedular" style="display:none">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Supplier Content Update Frequency
                </div>
                <div id="msgAlert" runat="server" style="display: none;"></div>
                
                <div class="panel-body">
                    <div class="form-group row">
                        <div class="col-sm-12">
                            <asp:ValidationSummary ID="vlsSumm" runat="server" ValidationGroup="Frequency" DisplayMode="BulletList" ShowMessageBox="false" ShowSummary="true" CssClass="alert alert-danger" />
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="col-md-5">
                            <div class="input-group row">
                                <label class="input-group-addon" for="Checkentitysequence">
                                   
                                    <strong>Entity</strong>                                
                                </label>
                                <asp:CheckBoxList ID="Checkentitysequence" CssClass="radioButtonList form-control" RepeatLayout="Flow" RepeatDirection="Horizontal"  runat="server">
                                    <asp:ListItem Text="Country" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="City" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Hotel" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Room Type" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Activity" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Holiday" Value="6"></asp:ListItem>
                                </asp:CheckBoxList>
                                
                            </div>
                            <div class="row clear">&nbsp;</div>
                            <div class="input-group row">
                                <label class="input-group-addon" for="rdbtnIsAPIXMLSupplier">
                                    <strong>Is API / XML Supplier</strong>
                                </label>
                                <asp:RadioButtonList ID="rdbtnIsAPIXMLSupplier" ClientIDMode="Static" CssClass="radioButtonList form-control" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                    <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="row clear">&nbsp;</div>
                            <div class="input-group row">
                                <%--<label class="input-group-addon radio" for="rdbtnIsAPIXMLSupplier">
                                    <strong>Frequency</strong>
                                </label>--%>
                                <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                <%--<asp:RadioButtonList ID="rdbFrequency"  ClientIDMode="Static" CssClass="radioButtonList form-control" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                    <asp:ListItem Value="H" Text="Hourly"></asp:ListItem>
                                    <asp:ListItem Value="D" Text="Daily"></asp:ListItem>
                                    <asp:ListItem Value="W" Text="Weekly"></asp:ListItem>
                                    <asp:ListItem Value="M" Text="Monthly"></asp:ListItem>
                                    <asp:ListItem Selected="True" Value="Y" Text="Yearly"></asp:ListItem>
                                </asp:RadioButtonList>--%>
                                <%--<asp:DropDownList ID="rdbFrequency" ClientIDMode="Static" CssClass="form-control"  runat="server">
                                    <asp:ListItem Value="H" Text="Hourly"></asp:ListItem>
                                    <asp:ListItem Value="D" Text="Daily"></asp:ListItem>
                                    <asp:ListItem Value="W" Text="Weekly"></asp:ListItem>
                                    <asp:ListItem Value="M" Text="Monthly"></asp:ListItem>
                                    <asp:ListItem Selected="True" Value="Y" Text="Yearly"></asp:ListItem>
                                </asp:DropDownList>--%>
                            </div>
                        </div>
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-5">
                            <div class="input-group row">
                                <label class="input-group-addon" for="rdbtnIsAPIXMLSupplier">
                                    <strong>Is Update Frequencry Required
                                    </strong>
                                </label>
                                <asp:RadioButtonList ID="rdbIsUpdateFrequencyRequired" CssClass="radioButtonList form-control" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                    <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="row clear">&nbsp;</div>
                            <div class="input-group row">
                                <label class="input-group-addon" for="ddlStatus"><strong>Status</strong></label>
                                <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                                    <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                                    <asp:ListItem Text="Inactive" Value="Inactives"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-5">
                            <div class="input-group row">
                                <label class="input-group-addon" for="ddlUserRoleId"><strong>UserRoleID</strong></label>
                                <asp:DropDownList ID="ddlUserRoleId" CssClass="form-control" runat="server">
                                    <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div id="example1-cron" class="cron-builder col-md-5">                            
                        </div>
                        <asp:HiddenField ID="cron_expression" ClientIDMode="Static" runat="server" Value="" EnableViewState="false" />
                        <asp:HiddenField ID="SupplierScheduleId" ClientIDMode="Static" runat="server" Value="" EnableViewState="false" />
                    </div>

                    <%--<div id="divYearly" runat="server" class="activediv">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="form-group row col-md-4">
                                    <div class="form-inline input-group">
                                        <label class="input-group-addon" for="txtRecurEveryYear">
                                            <strong>Recur every
                                        <asp:RequiredFieldValidator ID="rfvRecurEvery_Year" ClientIDMode="Static" ValidationGroup="Frequency" runat="server" ControlToValidate="txtRecurEvery_Year"
                                            CssClass="text-danger" ErrorMessage="Year -Recur every field is required." Text="*" />
                                            </strong>
                                        </label>
                                        <asp:TextBox ID="txtRecurEvery_Year" runat="server" CssClass="form-control"></asp:TextBox>
                                        <label class="input-group-addon" for="txtRecurEveryYear">Year(s)</label>
                                    </div>
                                </div>
                                <div class="form-group row col-md-12">
                                    <div class="form-inline">
                                        <div class="col-md-1">
                                            <asp:RadioButton ID="rbDay_Year" Checked="true" onchange="OptionYear();" ClientIDMode="Static" runat="server" Text="Day" GroupName="_year" />
                                            <asp:RequiredFieldValidator ID="rfvMonthDay_Year" ClientIDMode="Static" ValidationGroup="Frequency" runat="server" ControlToValidate="txtMonthDay_Year"
                                                CssClass="text-danger" ErrorMessage="Year - Day of the month is required." Text="*" />
                                        </div>
                                        <div class="col-md-11">
                                            <asp:DropDownList ID="ddlMonths_Year" runat="server" ClientIDMode="Static" CssClass="form-control">
                                                <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtMonthDay_Year" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group row col-md-12">
                                    <div class="form-inline">
                                        <div class="col-md-1">
                                            <asp:RadioButton ID="rbOnthe_Year" onchange="OptionYear();" ClientIDMode="Static" runat="server" Text="On the" GroupName="_year" />
                                        </div>
                                        <div class="col-md-11">
                                            <asp:DropDownList ID="ddlWeek_Year" Enabled="false" ClientIDMode="Static" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="First" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Secord" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Third" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Four" Value="4"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlMonthWeekList_Year" Enabled="false" ClientIDMode="Static" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Sunday" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Monday" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Tuesday" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Wednesday" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="Thursday" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="Friday" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="Saturday" Value="7"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlYearMonth_Year" ClientIDMode="Static" Enabled="false" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divMonthly" runat="server" class="Divdisplay">
                        <div class="form-group row col-md-12">
                            <div class="form-inline">
                                <div class="col-md-1">
                                    <asp:RadioButton ID="rblDays_Monthly" onchange="OptionMonth();" ClientIDMode="Static" runat="server" Text="Day" Checked="true" GroupName="_month" />
                                </div>
                                <div class="col-md-11">
                                    <asp:TextBox ID="txtDayOf_Monthly" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Enabled="false" ID="rfvDayOf_Monthly" ClientIDMode="Static" ValidationGroup="Frequency" runat="server" ControlToValidate="txtDayOf_Monthly"
                                        CssClass="text-danger" ErrorMessage="Monthly - Day of the month is required." Text="*" />
                                    <label>of every </label>
                                    <asp:TextBox ID="txtDayOfEvery_Monthly" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDayOfEvery_Monthly" ClientIDMode="Static" ValidationGroup="Frequency" runat="server" ControlToValidate="txtDayOfEvery_Monthly"
                                        CssClass="text-danger" Enabled="false" ErrorMessage="Monthly-Month frequence is required." Text="*" />
                                    <label>Month(s)</label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row col-md-12">
                            <div class="form-inline">
                                <div class="col-md-1">
                                    <asp:RadioButton ID="rbThe_Montly" ClientIDMode="Static" onchange="OptionMonth();" runat="server" Text="The" GroupName="_month" />
                                </div>
                                <div class="col-md-11">
                                    <asp:DropDownList ID="ddlTheSequencyWeek_Month" Enabled="false" ClientIDMode="Static" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="First" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Secord" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Third" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Four" Value="4"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlTheDaysOf_Month" Enabled="false" runat="server" ClientIDMode="Static" CssClass="form-control">
                                        <asp:ListItem Text="Sunday" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Monday" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Tuesday" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Wednesday" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Thursday" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Friday" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="Saturday" Value="7"></asp:ListItem>
                                    </asp:DropDownList>
                                    <label>of every </label>
                                    <asp:TextBox ID="txtOccurEvery_Month" Enabled="false" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvOccurEvery_Month" ClientIDMode="Static" ValidationGroup="Frequency" runat="server" ControlToValidate="txtOccurEvery_Month"
                                        CssClass="text-danger" Enabled="false" ErrorMessage="Month- Day frequence is required." Text="*" />
                                    <label>month(s) </label>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divWeekly" class="Divdisplay" runat="server">
                        <div class="form-group row col-md-12">
                            <div class="form-inline">
                                <label for="txtRecurEveryYear">
                                    Recur every
                            <asp:RequiredFieldValidator ID="rfvRecur_Weekly" Enabled="false" ClientIDMode="Static" ValidationGroup="Frequency" runat="server" ControlToValidate="txtRecur_Weekly"
                                CssClass="text-danger" ErrorMessage="Weekly - Recur frequence is required." Text="*" />
                                </label>
                                <asp:TextBox ID="txtRecur_Weekly" runat="server" CssClass="form-control "></asp:TextBox>
                                <label>Week(s) on</label>
                            </div>
                        </div>
                        <div class="form-group row col-md-12">
                            <div class="form-inline">
                                <asp:CheckBoxList ID="chckbxWeek_Weekly" CssClass="radioButtonList" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                    <asp:ListItem Text="Sunday" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Monday" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Tuesday" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Wednesday" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Thursday" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Friday" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="Saturday" Value="7"></asp:ListItem>
                                </asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                    <div id="divDaily" class="Divdisplay" runat="server">
                        <div class="form-group row col-md-12">
                            <div class="form-inline">
                                <div class="form-inline col-md-12">
                                    <label for="txtRecurEveryYear">
                                        Recur every
                      <asp:RequiredFieldValidator ID="rfvOccur_Daily" Enabled="false" ClientIDMode="Static" ValidationGroup="Frequency" runat="server" ControlToValidate="txtOccur_Daily"
                          CssClass="text-danger listValidation" ErrorMessage="Daily -Recur frequence is required." Text="*" />
                                    </label>
                                    <asp:TextBox ID="txtOccur_Daily" runat="server" CssClass="form-control "></asp:TextBox>
                                    <label>Day(s) on</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divHourly" class="Divdisplay" runat="server">
                        <div class="form-group row col-md-12">
                            <div class="form-inline">
                                <div class="form-inline col-md-12">
                                    <label for="txtRecurEveryYear">
                                        Recur every
                     <asp:RequiredFieldValidator ID="rfvRecur_Hourly" ClientIDMode="Static" ValidationGroup="Frequency" runat="server" ControlToValidate="txtRecur_Hourly"
                         CssClass="text-danger listValidation" Enabled="false" ErrorMessage="Hourly - Recur frequence is required." Text="*" />
                                    </label>
                                    <asp:TextBox ID="txtRecur_Hourly" ClientIDMode="Static" runat="server" CssClass="form-control "></asp:TextBox>
                                    <label>Hour(s) on</label>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                    <%--<div class="form-group row col-md-12">
                        <hr />
                        <label>Download Time</label>
                    </div>
                    <div class="form-group row col-md-12">
                        <div class="form-inline">
                            <div class="col-md-6">
                                <label for="txtStartTime">Start</label>
                                <div class="input-group bootstrap-timepicker timepicker">
                                    <input id="timepickerStart_Hourly" runat="server" type="text" class="form-control input-small">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label for="txtEndTime">End</label>
                                <div class="input-group bootstrap-timepicker timepicker">
                                    <input id="timepickerEnd_Hourly" runat="server" type="text" class="form-control input-small">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                    <div class="form-group row col-md-12">
                    </div>
                    <div class="form-group row col-md-12">
                        <asp:Button ID="btnSaveSchedule" OnClick="btnSaveSchedule_Click" OnClientClick="return CheckExistingData()"  runat="server" ValidationGroup="Frequency" CssClass="btn btn-primary btn-sm" Text="Save" />
                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnReset_Click" Text="Reset" />
                    </div>
                </div>
            </div>
        </div>
        <%--  <div class="panel panel-default">
            <div class="panel-body">
                <div class="form-group row">
                    <div class="col-sm-12">
                        <asp:Button runat="server" ID="btnNewCreate" OnClick="btnNewCreate_Click" CssClass="btn btn-sm btn-primary pull-right" Text="Create New Supplier Schedular" />
                    </div>
                </div>
            </div>
        </div>--%>
    </ContentTemplate>

</asp:UpdatePanel>
<script type="text/javascript">
    $('#MainContent_manageSupplier_supplierStaticDataHandling_timepickerStart_Hourly').timepicker();
    $('#MainContent_manageSupplier_supplierStaticDataHandling_timepickerEnd_Hourly').timepicker();
</script>
