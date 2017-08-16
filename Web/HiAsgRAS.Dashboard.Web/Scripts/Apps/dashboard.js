type = ['', 'info', 'success', 'warning', 'danger'];


dashboard = {
    initPickColor: function () {
        $('.pick-class-label').click(function () {
            var new_class = $(this).attr('new-class');
            var old_class = $('#display-buttons').attr('data-class');
            var display_div = $('#display-buttons');
            if (display_div.length) {
                var display_buttons = display_div.find('.btn');
                display_buttons.removeClass(old_class);
                display_buttons.addClass(new_class);
                display_div.attr('data-class', new_class);
            }
        });
    },

    initFormExtendedDatetimepickers: function () {
        $('.datetimepicker').datetimepicker({
            icons: {
                time: "fa fa-clock-o",
                date: "fa fa-calendar",
                up: "fa fa-chevron-up",
                down: "fa fa-chevron-down",
                previous: 'fa fa-chevron-left',
                next: 'fa fa-chevron-right',
                today: 'fa fa-screenshot',
                clear: 'fa fa-trash',
                close: 'fa fa-remove'
            }
        });
    },

    showNotification: function (from, align) {
        color = Math.floor((Math.random() * 4) + 1);

        $.notify({
            icon: "notifications",
            message: "Welcome to <b>HI ASG CDAS Dashboard</b>"
        }, {
            type: type[color],
            timer: 4000,
            placement: {
                from: from,
                align: align
            }
        });
    }
    , showMessage: function (messageType, message, closeParent) {
        //Refer: http://bootstrap-notify.remabledesigns.com/
        var setTime;
        if (messageType.toLowerCase() == 'warning' || messageType.toLowerCase() == 'danger') {
            setTime = 10000;
        }
        else {
            setTime = 1000;
        }
        $.notify({
            icon: "notifications",
            message: message,
            title: "Action - Result"
        }, {
            element: 'body',
            type: messageType, //Success, Info, warning, danger
            closeWith: ['button', 'click'],
            delay: 1000,
            timer: setTime,
            modal: true,
            allow_dismiss: true,
            newest_on_top: true
            , z_index: 9999
            , placement: {
                from: 'bottom', //top, bottom
                align: 'center'
            }
            , animate: {
                enter: 'animated bounceInDown',
                exit: 'animated flipOutX'
            }
            , onShow: function () {
                if (closeParent) {
                    $('#dvModal').modal('hide');
                }
            }
            , onClose: function () {
                if (closeParent) {
                    $('#dvModal').modal('hide');
                }
            }
        });
        return false;
    }
}

initJsMethods = {

    invokePortlet: function () {
        //portlet_SPSites
        initJsMethods.initSPSites();

        //Monitor Section        
        initJsMethods.initMonitor();

        //Site Visitors Section
        initJsMethods.initSiteVisitors();

        //RAM Usage
        initJsMethods.initRAMUsage();

        //Daily Status
        initJsMethods.initDailyStatus();

        initJsMethods.initSiteVisitorsCount();

        initJsMethods.initRebootCount();

        initJsMethods.initHDDCount();

        initJsMethods.initServerStatusCount();

        initJsMethods.initRAMCount();
    },

    initSPSites: function () {
        $.ajax({
            //Calling Partial View
            url: $("#uPrtSPSites").val(),
            async: true,
            type: 'GET',
            success: function (data) {
                if (data != null) {
                    $('#portlet_SPSites').html(data);
                }
            },
            error: function (data, status, jqXHR) {
                console.log('Error in Loading SharePoint Portlet: ' + jqXHR);
            }
        });
    },

    initMonitor: function(){
        $.ajax({
            //Calling Partial View
            url: $("#uPrtPMonitor").val(),
            async: true,
            type: 'GET',
            success: function (data) {
                if (data != null) {
                    $('#portlet_Monitor').html(data);
                }
            },
            error: function (data, status, jqXHR) {
                console.log('Error in Loading Monitoring Portlet: ' + jqXHR);
            }
        });
    },

    initSiteVisitors: function () {
        $.ajax({
            //Calling Partial View
            url: $("#uPrtSiteVisitors").val(),
            async: true,
            type: 'GET',
            success: function (data) {
                if (data != null) {
                    $('#portlet_SiteVisitors').html(data);
                }
            },
            error: function (data, status, jqXHR) {
                console.log('Error in Loading Monitoring Portlet: ' + jqXHR);
            }
        });
    },

    initRAMUsage: function () {
        $.ajax({
            //Calling Partial View
            url: $("#uPrtRAMUsage").val(),
            async: true,
            type: 'GET',
            success: function (data) {
                if (data != null) {
                    $('#portlet_RAMUsage').html(data);
                }
            },
            error: function (data, status, jqXHR) {
                console.log('Error in Loading RAM Portlet: ' + jqXHR);
            }
        });
    },

    initDailyStatus: function () {
        $.ajax({
            //Calling Partial View
            url: $("#uPrtDailyStatus").val(),
            async: true,
            type: 'GET',
            success: function (data) {
                if (data != null) {
                    $('#portlet_DailyStatus').html(data);
                }
            },
            error: function (data, status, jqXHR) {
                console.log('Error in Loading Daily Status Portlet: ' + jqXHR);
            }
        });
    },

    initSiteVisitorsCount: function () {
        $.ajax({
            //Calling Partial View
            url: $("#uPrtSiteVistorsCount").val(),
            async: true,
            type: 'GET',
            success: function (data) {
                if (data != null) {
                    $('#portlet_SiteVistorsCount').html(data);
                }
            },
            error: function (data, status, jqXHR) {
                console.log('Error in Loading Monitoring Portlet: ' + jqXHR);
            }
        });
    },

    initRebootCount: function () {
        $.ajax({
            //Calling Partial View
            url: $("#uPrtRebootCount").val(),
            async: true,
            type: 'GET',
            success: function (data) {
                if (data != null) {
                    $('#portlet_RebootCount').html(data);
                }
            },
            error: function (data, status, jqXHR) {
                console.log('Error in Loading Reboot Portlet: ' + jqXHR);
            }
        });
    },

    initHDDCount: function () {
        $.ajax({
            //Calling Partial View
            url: $("#uPrtHDDCount").val(),
            async: true,
            type: 'GET',
            success: function (data) {
                if (data != null) {
                    $('#portlet_HDDCount').html(data);
                }
            },
            error: function (data, status, jqXHR) {
                console.log('Error in Loading HDD Portlet: ' + jqXHR);
            }
        });
    },

    initRAMCount: function () {
        $.ajax({
            //Calling Partial View
            url: $("#uPrtRAMCount").val(),
            async: true,
            type: 'GET',
            success: function (data) {
                if (data != null) {
                    $('#portlet_RAMCount').html(data);
                }
            },
            error: function (data, status, jqXHR) {
                console.log('Error in Loading RAM Count Portlet: ' + jqXHR);
            }
        });
    },

    initServerStatusCount: function () {
        $.ajax({
            //Calling Partial View
            url: $("#uPrtServerStatusCount").val(),
            async: true,
            type: 'GET',
            success: function (data) {
                if (data != null) {
                    $('#portlet_ServerStatusCount').html(data);
                }
            },
            error: function (data, status, jqXHR) {
                console.log('Error in Loading HDD Portlet: ' + jqXHR);
            }
        });
    },

    initLogRowPopup: function () {
        //on Each Row click
        $(".viewServer").on('click', (function () {           

            hiradServerCRUD.viewLogRowPopup(null, $(this).data("serverid"))
        }));

        $(".viewWebLog").on('click', (function () {
            hiradWebLog.viewLogRowPopup($(this).data("webid"))
        }));

        $(".viewDBLog").on('click', (function () {
            hiradDBLog.viewLogRowPopup($(this).data("dbmonitorid"))
        }));
    }
    
}

initChartMethods = {

    initSiteVisitorsChart: function () {
        var dataSiteVisitorsChart = {
            labels: ['Jan', 'Feb', 'Mar', 'Apr', 'Mai', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
            series: [
              [542, 443, 320, 780, 553, 453, 326, 434, 568, 610, 756, 895]

            ]
        };
        var optionsSiteVisitorsChart = {
            axisX: {
                showGrid: false
            },
            low: 0,
            high: 1000,
            chartPadding: { top: 0, right: 5, bottom: 0, left: 0 }
        };
        var responsiveOptions = [
          ['screen and (max-width: 640px)', {
              seriesBarDistance: 5,
              axisX: {
                  labelInterpolationFnc: function (value) {
                      return value[0];
                  }
              }
          }]
        ];
        var siteVisitorsChart = Chartist.Bar('#chartSiteVisitors', dataSiteVisitorsChart, optionsSiteVisitorsChart, responsiveOptions);

        //start animation for the Emails Subscription Chart
        md.startAnimationForBarChart(siteVisitorsChart);
    },

    initRAMUsageChart: function () {
        //dataRamUsageChart = {
        //    labels: ['12am', '3pm', '6pm', '9pm', '12pm', '3am', '6am', '9am'],
        //    series: [
        //        [230, 750, 450, 300, 280, 240, 200, 190]
        //    ]
        //};

        //optionsRamUsageChart = {
        //    lineSmooth: Chartist.Interpolation.cardinal({
        //        tension: 0
        //    }),
        //    low: 0,
        //    high: 1000, //  we recommend you to set the high sa the biggest value + something for a better look
        //    chartPadding: { top: 0, right: 0, bottom: 0, left: 0 }
        //}

        //var ramUsageChart = new Chartist.Line('#chartRAMUsage', dataRamUsageChart, optionsRamUsageChart);

        //// start animation for the Completed Tasks Chart - Line Chart
        //md.startAnimationForLineChart(ramUsageChart);
    },

    initDailyStatusChart: function () {
        ////labels: ['M', 'T', 'W', 'T', 'F', 'S', 'S'],
        ////series: [
        //    //[12, 17, 70, 17, 23, 18, 68],
        //    //[32, 27, 80, 37, 83, 28, 98],
        //dataDailyStatusChart = {
        //    labels: ['M', 'T', 'W', 'T', 'F', 'S', 'S'],
        //    series: [
        //        //Row 1
        //            [
        //            { meta: 'himoafact042', value: 100 },
        //            { meta: 'himoafact042', value: 52 },
        //            { meta: 'himoafact042', value: 34 },
        //            { meta: 'himoafact042', value: 81 },
        //            { meta: 'himoafact042', value: 65 },
        //            { meta: 'himoafact042', value: 93 },
        //            { meta: 'himoafact042', value: 11 }
        //            ],
        //            //Row 2
        //            [
        //            { meta: 'hidscfact057', value: 32 },
        //            { meta: 'hidscfact057', value: 27 },
        //            { meta: 'hidscfact057', value: 80 },
        //            { meta: 'hidscfact057', value: 37 },
        //            { meta: 'hidscfact057', value: 83 },
        //            { meta: 'hidscfact057', value: 28 },
        //            { meta: 'hidscfact057', value: 91 }
        //            ]
        //    ]
        //};

        //optionsDailyStatusChart = {
        //    lineSmooth: Chartist.Interpolation.cardinal({
        //        tension: 0
        //    }),
        //    low: 0,
        //    high: 101,
        //    chartPadding: { top: 0, right: 0, bottom: 0, left: 0 },
        //    plugins: [
        //      Chartist.plugins.tooltip(),
        //      //Chartist.plugins.ctPointLabels({
        //      //    textAnchor: 'middle',
        //      //    labelInterpolationFnc: function (value) { return value + '%' }
        //      //})
        //    ]
        //}

        //var dailyStatusChart = new Chartist.Line('#chartDailyStatus', dataDailyStatusChart, optionsDailyStatusChart);

        //md.startAnimationForLineChart(dailyStatusChart);
    }

}


