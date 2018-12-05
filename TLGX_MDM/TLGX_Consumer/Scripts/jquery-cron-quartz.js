


; (function ($) {
    "use strict";

    var cronInputs = {
        period: '<div class="cron-select-period col-md-5 input-group row"><label class="input-group-addon strongtext"></label><select id="ddl_period" name="ddl_period" class="cron-period-select form-control" runat="server" style="margin-left:0px;"></select></div>',
        startTime: '<div class="cron-input cron-start-time col-md-8 input-group row"><label class="input-group-addon strongtext" style="width:20%;">Start time </label><select id="ddl_clock_hour" name="ddl_clock_hour" class="cron-clock-hour form-control" runat="server" style="width:35%;margin-left:0px;"></select><select id="ddl_clock_minute" name="ddl_clock_minute" class="cron-clock-minute form-control" runat="server" style="width:35%;"></select></div>',
        container: '<div class="cron-input input-group row"></div>',
        minutes: {
            tag: 'cron-minutes',
            inputs: ['']
        },
        hourly: {
            tag: 'cron-hourly',
            inputs: ['<p><div><div class="col-md-1"><input type="radio" name="hourlyType" value="every"></div><div class="col-md-10 input-group"><div class="col-md-3"><label style="width:20%;">Every</label></div><div class="col-md-4" style="padding-left:0px; margin-left:9px;"><select id="ddl_hourly" name="ddl_hourly" class="cron-hourly-select form-control" runat="server" style="padding-left:0px;margin-left:-5px;width:10 %"></select></div><div class="col-md-4" style="padding-left:0px;margin-left:-9px;"><label  style="width:8%;"> hour(s)</label></div></div></p>']

                //'<p><div><div class="col-md-1"><input type="radio" name="hourlyType" value="clock"></div><div class="col-md-10 input-group"><div class="col-md-4"><label style="width:100%;"> Every day at </label></div> <div class="col-md-4"><select class="cron-hourly-hour form-control"></select></div><div class="col-md-4"><select class="cron-hourly-minute form-control"></select></div></div></p>']
        },
        daily: {
            tag: 'cron-daily',
            inputs: ['<p><div><div class="col-md-1"><input type="radio" name="dailyType" value="every"></div><div class="col-md-10 input-group"><div class="col-md-3"><label style="width:20%;">Every</label></div><div class="col-md-5" style="padding-left:0px; margin-left:-5px;"><select id="ddl_daily" name="ddl_daily" class="cron-daily-select form-control" runat="server"></select></div><div class="col-md-4" style="padding-left:0px;margin-left:-2px;"><label  style="width:8%;"> day</label></div></div></p>']
                //'<p><input type="radio" name="dailyType" value="clock"> Every week day</p>']
        },
        //weekly: {
        //    tag: 'cron-weekly',
        //    inputs: [ '<p><input type="checkbox" name="dayOfWeekMon"> Monday  <input type="checkbox" name="dayOfWeekTue"> Tuesday  '+ 
        //        '<input type="checkbox" name="dayOfWeekWed"> Wednesday  <input type="checkbox" name="dayOfWeekThu"> Thursday</p>', 
        //        '<p><input type="checkbox" name="dayOfWeekFri"> Friday  <input type="checkbox" name="dayOfWeekSat"> Saturday  '+ 
        //        '<input type="checkbox" name="dayOfWeekSun"> Sunday</p>']
        weekly: {
            tag: 'cron-weekly',
            inputs: ['<div id="weeklychk"><p><input type="checkbox" name="dayOfWeek" value="2"> Monday  <input type="checkbox" name="dayOfWeek" value="3"> Tuesday  ' +
                '<input type="checkbox" name="dayOfWeek" value="4"> Wednesday  <input type="checkbox" name="dayOfWeek" value="5"> Thursday</p>',
            '<p><input type="checkbox" name="dayOfWeek" value="6"> Friday  <input type="checkbox" name="dayOfWeek" value="7"> Saturday  ' +
            '<input type="checkbox" name="dayOfWeek" value="1"> Sunday</p></div>']
        },
        monthly: {
            tag: 'cron-monthly',
            inputs: ['<p><div><div class="col-md-1"><input type="radio" id="monthlyday" name="monthlyType" value="byDay"></div><div class="col-md-11 input-group"><div class="col-md-2"><label style="width:20%;">Day</label></div><div class="col-md-3" style="padding-left:0px; margin-left:-5px;"> <select id="ddl_monthly_day" name="ddl_monthly_day" class="cron-monthly-day form-control" runat="server"></select></div><div class="col-md-2" style="padding-left:0px;margin-left:5px;"><label  style="width:8%;"> of every</label></div><div class="col-md-3" style="padding-left:0px; margin-left:1px;"> <select id  ="ddl_monthly_monthly" name="ddl_monthly_monthly" class="cron-monthly-month form-control" runat="server"></select></div><div class="col-md-2" style="padding-left:0px;margin-left:-1px;"><label  style="width:8%;">  month(s)</label></div></div></p>',

                '<p><div><div class="col-md-1"><input type="radio" id="monthlyweek" name="monthlyType" value="byWeek" ></div><div class="col-md-11 input-group"><div class="col-md-1"><label style="width:20%;">The</label></div><div class="col-md-3" style="padding-left:0px; margin-left:-5px;"> <select id="ddl_monthly_nthday" name="ddl_monthly_nthday" class="cron-monthly-nth-day form-control" runat="server"></select></div> ' +
                '<div class="col-md-3" style="padding-left:0px; margin-left:-5px;"><select id="ddl_monthly_day_of_week" name="ddl_monthly_day_of_week" class="cron-monthly-day-of-week form-control" runat="server"></select></div><div class="col-md-1"><label style="width:20%;"> of every</label></div><div class="col-md-3" style="padding-left:0px; margin-left:3px;"><select id="ddl_monthly_month_by_week" name="ddl_monthly_month_by_week" class="cron-monthly-month-by-week form-control" runat="server"></select></div><div class="col-md-1"><label style="width:20%;">month(s)</label></div></div></p>']
        },
        yearly: {
            tag: 'cron-yearly',
            inputs: ['<p><div><div class="col-md-1"><input type="radio" name="yearlyType" value="byDay"></div><div class="col-md-11 input-group"><div class="col-md-2"><label style="width:20%;">Every</label></div><div class="col-md-3" style="padding-left:0px; margin-left:-5px;"><select id="ddl_yearly_Month" name="ddl_yearly_Month" class="cron-yearly-month form-control" runat="server"></select></div> <div class="col-md-3" style="padding-left:0px; margin-left:-5px;"> <select id="ddl_yearly_day" name="ddl_yearly_day" class="cron-yearly-day form-control" runat="server"></select></div></div></p>',

                '<p><div><div class="col-md-1"><input type="radio" name="yearlyType" value="byWeek"></div><div class="col-md-11 input-group"><div class="col-md-1"><label style="width:20%;">The</label></div>  <div class="col-md-3" style="padding-left:0px; margin-left:-5px;"><select id="ddl_yearly_nthday" name="ddl_yearly_nthday" class="cron-yearly-nth-day form-control" runat="server"></select></div> ' +
                '<div class="col-md-3" style="padding-left:0px; margin-left:-5px;"><select id="ddl_yearly_day_od_week" name="ddl_yearly_day_od_week" class="cron-yearly-day-of-week form-control" runat="server"></select></div><div class="col-md-1"><label style="width:20%;">of</label></div><div class="col-md-3" style="padding-left:0px; margin-left:-5px;"><select id="ddl_yearly_month_by_week" name="ddl_yearly_month_by_week" class="cron-yearly-month-by-week form-control"></select></div></div></p>']
        }
    };

    var periodOpts = arrayToOptions([ "Hourly", "Daily", "Weekly", "Monthly", "Yearly"], ["H", "D", "W", "M", "Y"]);
    var minuteOpts = rangeToOptions(1, 60);
    var hourOpts = rangeToOptions(1, 24);
    var dayOpts = rangeToOptions(1, 100);
    var minuteClockOpts = rangeToOptions(0, 59, true);
    var hourClockOpts = rangeToOptions(0, 23, true);
    var dayInMonthOpts = rangeToOptions(1, 31);
    var monthOpts = arrayToOptions(["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
        [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]);
    var monthNumOpts = rangeToOptions(1, 12);
    var nthWeekOpts = arrayToOptions(["First", "Second", "Third", "Forth"], [1, 2, 3, 4]);
    var dayOfWeekOpts = arrayToOptions(["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"], [1, 2, 3, 4, 5, 6, 7]);


    // Convert an array of values to options to append to select input
    function arrayToOptions(opts, values) {
        var inputOpts = '';
        for (var i = 0; i < opts.length; i++) {
            var value = opts[i];
            if (values != null) value = values[i];
            inputOpts += "<option value='" + value + "'>" + opts[i] + "</option>\n";

        }
        return inputOpts;
    }

    // Convert an integer range to options to append to select input
    function rangeToOptions(start, end, isClock) {
        var inputOpts = '', label;
        for (var i = start; i <= end; i++) {
            if (isClock && i < 10) label = "0" + i;
            else label = i;
            inputOpts += "<option value='" + i + "'>" + label + "</option>\n";
        }
        return inputOpts;
    }

    // Add input elements to UI as defined in cronInputs
    function addInputElements($baseEl, inputObj, onFinish) {
        $(cronInputs.container).addClass(inputObj.tag).appendTo($baseEl);
        $baseEl.children("." + inputObj.tag).append(inputObj.inputs);
        if (typeof onFinish === "function") onFinish();
    }

    var eventHandlers = {
        periodSelect: function () {
            var period = ($(this).val());
            var $selector = $(this).parent();
            $selector.siblings('div.cron-input').hide();
            $selector.siblings().find("select option").removeAttr("selected");
            $selector.siblings().find("select option:first").attr("selected", "selected");
            $selector.siblings('div.cron-start-time').show();
            $selector.siblings('div.cron-start-time').children("select.cron-clock-hour").val('12');
            switch (period) {
                case 'Minutes':
                    //$selector.siblings('div.cron-minutes')
                    //    .show()
                    //    .find("select.cron-minutes-select").val('1');
                    //$selector.siblings('div.cron-start-time').hide();
                    break;
                case 'H':
                    var $hourlyEl = $selector.siblings('div.cron-hourly');
                    $hourlyEl.show()
                        .find("input[name=hourlyType][value=every]").prop('checked', true);
                    $hourlyEl.find("select.cron-hourly-hour").val('12');
                    $selector.siblings('div.cron-start-time').hide();
                    break;
                case 'D':
                    var $dailyEl = $selector.siblings('div.cron-daily');
                    $dailyEl.show()
                        .find("input[name=dailyType][value=every]").prop('checked', true);
                    break;
                case 'W':
                    $selector.siblings('div.cron-weekly')
                        .show()
                        .find("input[type=checkbox]").prop('checked', false);
                    break;
                case 'M':
                    var $monthlyEl = $selector.siblings('div.cron-monthly');
                    $monthlyEl.show()
                        .find("input[name=monthlyType][value=byDay]").prop('checked', true);
                    break;
                case 'Y':
                    var $yearlyEl = $selector.siblings('div.cron-yearly');
                    $yearlyEl.show()
                        .find("input[name=yearlyType][value=byDay]").prop('checked', true);
                    break;
            }
        }
    };

    // Public functions
    $.cronBuilder = function (el, options) {
        var base = this;

        // Access to jQuery and DOM versions of element
        base.$el = $(el);
        base.el = el;

        // Reverse reference to the DOM object
        base.$el.data('cronBuilder', base);

        // Initialization
        base.init = function () {
            base.options = $.extend({}, $.cronBuilder.defaultOptions, options);


            base.$el.append(cronInputs.period);
            base.$el.find("div.cron-select-period label").text(base.options.selectorLabel);
            base.$el.find("select.cron-period-select")
                .append(periodOpts)
                .bind("change", eventHandlers.periodSelect);

            addInputElements(base.$el, cronInputs.minutes, function () {
                base.$el.find("select.cron-minutes-select").append(minuteOpts);
            });

            addInputElements(base.$el, cronInputs.hourly, function () {
                base.$el.find("select.cron-hourly-select").append(hourOpts);
                base.$el.find("select.cron-hourly-hour").append(hourClockOpts);
                base.$el.find("select.cron-hourly-minute").append(minuteClockOpts);
            });

            addInputElements(base.$el, cronInputs.daily, function () {
                base.$el.find("select.cron-daily-select").append(dayOpts);
            });

            addInputElements(base.$el, cronInputs.weekly);

            addInputElements(base.$el, cronInputs.monthly, function () {
                base.$el.find("select.cron-monthly-day").append(dayInMonthOpts);
                base.$el.find("select.cron-monthly-month").append(monthNumOpts);
                base.$el.find("select.cron-monthly-nth-day").append(nthWeekOpts);
                base.$el.find("select.cron-monthly-day-of-week").append(dayOfWeekOpts);
                base.$el.find("select.cron-monthly-month-by-week").append(monthNumOpts);
            });

            addInputElements(base.$el, cronInputs.yearly, function () {
                base.$el.find("select.cron-yearly-month").append(monthOpts);
                base.$el.find("select.cron-yearly-day").append(dayInMonthOpts);
                base.$el.find("select.cron-yearly-nth-day").append(nthWeekOpts);
                base.$el.find("select.cron-yearly-day-of-week").append(dayOfWeekOpts);
                base.$el.find("select.cron-yearly-month-by-week").append(monthOpts);
            });


            base.$el.append(cronInputs.startTime);
            base.$el.find("select.cron-clock-hour").append(hourClockOpts);
            base.$el.find("select.cron-clock-minute").append(minuteClockOpts);

            if (typeof base.options.onChange === "function") {
                base.$el.find("select, input").change(function () {
                    base.options.onChange(base.getExpression());
                });
            }

            base.$el.find("select.cron-period-select")
                .triggerHandler("change");

        }

        base.getExpression = function () {
            //var b = c.data("block");
            var sec = 0; // ignoring seconds by default
            var year = "*"; // every year by default
            var dow = "?";
            var month = "*", dom = "*";
            var min = base.$el.find("select.cron-clock-minute").val();
            var hour = base.$el.find("select.cron-clock-hour").val();
            var period = base.$el.find("select.cron-period-select").val();
            switch (period) {
                case 'Minutes':
                    //   var $selector=base.$el.find("div.cron-minutes");
                    //   var nmin=$selector.find("select.cron-minutes-select").val();
                    //if(nmin > 1) min ="0/"+nmin;
                    //else min="*";
                    //hour="*";
                    break;

                case 'H':
                    var $selector = base.$el.find("div.cron-hourly");
                    if ($selector.find("input[name=hourlyType][value=every]").is(":checked")) {
                        min = 0;
                        hour = "*";
                        var nhour = $selector.find("select.cron-hourly-select").val();
                        if (nhour > 1) hour = "0/" + nhour;
                    } else {
                        min = $selector.find("select.cron-hourly-minute").val();
                        hour = $selector.find("select.cron-hourly-hour").val();
                    }
                    break;

                case 'D':
                    var $selector = base.$el.find("div.cron-daily");
                    if ($selector.find("input[name=dailyType][value=every]").is(":checked")) {
                        var ndom = $selector.find("select.cron-daily-select").val();
                        if (ndom > 1) dom = "1/" + ndom;
                    } else {
                        dom = "?";
                        dow = "MON-FRI";
                    }
                    break;

                case 'W':
                    var $selector = base.$el.find("div.cron-weekly");
                    var ndow = [];
                    if ($selector.find("input[value=2]").is(":checked"))
                        ndow.push("MON");
                    if ($selector.find("input[value=3]").is(":checked"))
                        ndow.push("TUE");
                    if ($selector.find("input[value=4]").is(":checked"))
                        ndow.push("WED");
                    if ($selector.find("input[value=5]").is(":checked"))
                        ndow.push("THU");
                    if ($selector.find("input[value=6]").is(":checked"))
                        ndow.push("FRI");
                    if ($selector.find("input[value=7]").is(":checked"))
                        ndow.push("SAT");
                    if ($selector.find("input[value=1]").is(":checked"))
                        ndow.push("SUN");
                    dow = "*";
                    dom = "?";
                    if (ndow.length < 7 && ndow.length > 0) dow = ndow.join(",");
                    break;

                case 'M':
                    var $selector = base.$el.find("div.cron-monthly");
                    var nmonth;
                    if ($selector.find("input[name=monthlyType][value=byDay]").is(":checked")) {
                        month = "*";
                        nmonth = $selector.find("select.cron-monthly-month").val();
                        dom = $selector.find("select.cron-monthly-day").val();
                        dow = "?";
                    } else {
                        dow = $selector.find("select.cron-monthly-day-of-week").val()
                            + "#" + $selector.find("select.cron-monthly-nth-day").val();
                        nmonth = $selector.find("select.cron-monthly-month-by-week").val();
                        dom = "?";
                    }
                    if (nmonth > 1) month = "1/" + nmonth;
                    break;

                case 'Y':
                    var $selector = base.$el.find("div.cron-yearly");
                    if ($selector.find("input[name=yearlyType][value=byDay]").is(":checked")) {
                        dom = $selector.find("select.cron-yearly-day").val();
                        month = $selector.find("select.cron-yearly-month").val();
                        dow = "?";
                    } else {
                        dow = $selector.find("select.cron-yearly-day-of-week").val()
                            + "#" + $selector.find("select.cron-yearly-nth-day").val();
                        month = $selector.find("select.cron-yearly-month-by-week").val();
                        dom = "?";
                    }
                    break;

                default:
                    break;
            }
            return [sec, min, hour, dom, month, dow, year].join(" ");
        };

        base.init();
    };

    // Plugin default options
    $.cronBuilder.defaultOptions = {
        selectorLabel: "Select period:"

    };

    // Plugin definition 
    $.fn.cronBuilder = function (options) {
        return this.each(function () {
            (new $.cronBuilder(this, options));
        });
    };

}(jQuery));