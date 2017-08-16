hiradServerCRUD = {

    GetAllMonitoringServers: function (sUrl, ctrl) {
        $("#" + ctrl).empty();
        $.ajax({
            url: sUrl,
            async: true,
            type: 'GET',
            success: function (data) {
                if (data != null) {
                    $.each(data, function (index, optiondata) {
                        $("#" + ctrl).append("<option value='" + optiondata.Id + "'>" + optiondata.SystemName + "</option>");
                    });

                    //Be Defaultly selected rows data to be shown in Chart
                    if (ctrl == "cmbDlyServerStatus") {
                        $("select#" + ctrl).prepend("<option value='0'>All</option>");
                        $("select#" + ctrl).val("0");
                        hiradServerCRUD.GetAllServerLogStatusByLastRun();
                    }
                    else if (ctrl == "cmbServerAllStatus") {
                        $("select#" + ctrl).prepend("<option value='0'>All</option>");
                        $("select#" + ctrl).val("0");
                    }
                    else if (ctrl == "cmbRamUsage") {
                        $("select#" + ctrl).prepend("<option value='0'>Critical</option>");
                        $("select#" + ctrl).val("0");

                        //Load Default RAM chart based on Critical Count
                        if (($("#spanRAMCount").text() == "0") ||
                            ($("#spanRAMCount").text() == "")) {
                            var optionVal = $('#cmbRamUsage option:nth-child(2)').val();
                            if (optionVal != undefined) {
                                hiradRAM.GetServerRAMByDate(optionVal);
                                $("select#" + ctrl).val(optionVal);
                            }
                        }
                        else {
                            hiradRAM.GetAllCriticalRAMChart();
                        }
                    }
                }
            },
            error: function (data, status, jqXHR) {
                console.log('Error in Loading GetAllMonitoringServers Dropdown: ' + ctrl + '. Error ' + jqXHR);
            }
        });
    }
    , initLogStatusPopup: function () {
        //Daily Status View All Click 
        $("#aLogStatusViewAll").on('click', (function () {
            $.ajax({
                //Calling Partial View
                url: $("#uViewAllServerLogChart").val(),
                type: 'GET',
                success: function (data) {
                    if (data != null) {
                        $('#dvMdlDlgContent').html(data);
                        $("#dvModal").modal().css({
                            'max-height': '100%'
                        });
                        //ToDo: Need set Start Date, End Date
                        hiradServerCRUD.GetAllServerLogStatus();
                    }
                },
                error: function (data, status, jqXHR) {
                    console.log('Error in Log Status Popup on click: ' + jqXHR);
                }
            });
        }));
    }

    //Home page - First Row Data
    , GetOverAllServerStatus: function () {

        var sUrl = $("#uGetOverAllServerStatus").val() + '?' + new Date().getTime();

        $.getJSON(sUrl, function (data) {
            if (data != null) {                
                //Other Columns
                $("#spanServerStatus").text(data.uptime);
                $("#spanServerMoniteredAt").text(data.LastMonitoredAt);
            }
        });
    }

    //Chart Data
    , GetAllServerLogStatusByLastRun: function () {
        var sUrl = $("#uGetChartData").val();
        var inuptData = {
            chartName: 'LastRunAllServers'
        };

        $.getJSON(sUrl, inuptData, function (data) {
            if (data != null) {
                //Other Columns
                $("#spanDlyUptime").text(data.uptime);
                $("#spanDlyMoniteredAt").text(data.LastMonitoredAt);
                $("#spanRamMoniteredAt").text(data.LastMonitoredAt);
                
                //Chart
                dataDailyStatusChart = {
                    labels: data.labels,
                    series: data.series
                };

                optionsDailyStatusChart = {
                    lineSmooth: Chartist.Interpolation.cardinal({
                        tension: 0
                    }),
                    low: 0,
                    high: 101,
                    chartPadding: { top: 0, right: 0, bottom: 0, left: 0 },
                    plugins: [
                      Chartist.plugins.tooltip(
                          {
                              transformTooltipTextFnc: function (value) {
                                  return value + '%';
                              }
                          }),
                    ],
                    distributeSeries: true,
                    //axisY: {
                    //    offset: 0
                    //},
                    axisX: {
                        offset: 0
                    }
                    , axisY: {
                        labelInterpolationFnc: function (value, index) {
                            return value + "%";
                        }
                    }
                }

                var dailyStatusChart = new Chartist.Bar('#chartDailyStatus', dataDailyStatusChart, optionsDailyStatusChart);

                md.startAnimationForBarChart(dailyStatusChart);
            }
        });
    }

    , GetServerLogStatusByDate: function (serverId) {
        var sUrl = $("#uGetChartData").val();
        var inuptData = {
            serverId: serverId,
            chartName: 'ServerByDate'
        };

        $.getJSON(sUrl, inuptData, function (data) {
            if (data != null) {
                //Other Columns
                $("#spanDlyUptime").text(data.uptime);
                $("#spanDlyMoniteredAt").text(data.LastMonitoredAt);
                $("#spanRamMoniteredAt").text(data.LastMonitoredAt);

                //Chart
                dataDailyStatusChart = {
                    labels: data.labels,
                    series: data.series
                };
                optionsDailyStatusChart = {
                    lineSmooth: Chartist.Interpolation.cardinal({
                        tension: 0
                    }),
                    low: 0,
                    high: 101,
                    chartPadding: { top: 0, right: 0, bottom: 0, left: 0 },
                    plugins: [
                      Chartist.plugins.tooltip(
                          {
                              transformTooltipTextFnc: function (value) {
                                  return value + '%';
                              }
                          })
                    ]
                    , axisX: {
                        //offset: 0,       
                        //Skip the bottom text by 2
                        labelInterpolationFnc: function (value, index) {
                            return index % 2 === 0 ? value : null;
                        }
                    }
                    , axisY: {
                        labelInterpolationFnc: function (value, index) {
                            return value + "%";
                        }
                    }
                }

                var dailyStatusChart = new Chartist.Line('#chartDailyStatus', dataDailyStatusChart, optionsDailyStatusChart);

                md.startAnimationForLineChart(dailyStatusChart);
                //md.startAdditionalAnimationForLineChart(dailyStatusChart);
            }
        });
    }

    , GetServerLogStatusByWeekDayName: function (serverId, fromDate, toDate) {
        var sUrl = $("#uGetChartData").val();
        var inuptData = {
            serverId: serverId,
            fromDate: fromDate,
            toDate: toDate,
            chartName: 'WeekDayName'
        };

        $.getJSON(sUrl, inuptData, function (data) {
            if (data != null) {
                dataDailyStatusChart = {
                    labels: data.labels,
                    series: data.series
                };

                optionsDailyStatusChart = {
                    lineSmooth: Chartist.Interpolation.cardinal({
                        tension: 0
                    }),
                    low: 0,
                    high: 101,
                    chartPadding: { top: 0, right: 0, bottom: 0, left: 0 },
                    plugins: [
                      Chartist.plugins.tooltip({
                          transformTooltipTextFnc: function (value) {
                              return value + '%';
                          }
                      }),
                    ]
                    , axisY: {
                        labelInterpolationFnc: function (value, index) {
                            return value + "%";
                        }
                    }
                }

                var dailyStatusChart = new Chartist.Line('#chartDailyStatus', dataDailyStatusChart, optionsDailyStatusChart);

                md.startAnimationForLineChart(dailyStatusChart);
                //md.startAdditionalAnimationForLineChart(dailyStatusChart);
            }
        });

    }

    , GetAllServerLogStatus: function (serverId, fromDate, toDate) {
        var sUrl = $("#uGetChartData").val();
        //Set Default Value 
        if (serverId == "0" || serverId == undefined) {
            serverId = null;
        }
        if (fromDate == null && toDate == null) {
            fromDate = moment().format("MM/DD/YYYY");
            toDate = moment().format("MM/DD/YYYY");
        }

        //Input data to Chart records
        var inuptData = {
            serverId: serverId,
            fromDate: fromDate,
            toDate: toDate,
            chartName: 'AllServersByDates'
        };

        $.getJSON(sUrl, inuptData, function (data) {
            if (data != null) {
                dataDailyStatusChart = {
                    labels: data.labels,
                    series: data.series
                };

                optionsDailyStatusChart = {
                    lineSmooth: Chartist.Interpolation.cardinal({
                        tension: 0
                    }),
                    low: 0,
                    high: 101,
                    chartPadding: { top: 0, right: 0, bottom: 0, left: 0 },
                    plugins: [
                      Chartist.plugins.tooltip(
                          {
                              transformTooltipTextFnc: function (value) {
                                  return value + '%';
                              }
                          }),
                    ],
                    height: '500px',
                    width: '1100px'
                    , axisY: {
                        labelInterpolationFnc: function (value, index) {
                            return value + "%";
                        }
                    }
                }

                var dailyStatusChart = new Chartist.Bar('#chartAllStatus', dataDailyStatusChart, optionsDailyStatusChart);

                md.startAnimationForBarChart(dailyStatusChart);
            }
        });

    }

    , viewLogRowPopup: function (serverName, serverId) {
        //on Each Row click
        var record = {
            serverId: serverId,
            serverName: serverName
        };
        $.ajax({
            //Calling Partial View
            url: $("#uPrtServerLog").val(),
            type: 'GET',
            data: record,
            success: function (data) {
                if (data != null) {
                    $('#dvMdlDlgContent').html(data);
                    $("#dvModal").modal();
                    //$("#dvModal").removeData("modal").modal({ backdrop: 'static', keyboard: true });
                }
            },
            error: function (data, status, jqXHR) {
                console.log('Error in view Server Log while click chart: ' + jqXHR);
            }
        });
    }


    //Page Related

    , searchResult: function (defaultLoad) {

        var sUrl = $('#uSearchServerList').val();


        var searchData = {
            Id: 0
        };

        $('#dvLoadModal').modal('show');

        $.ajax({
            url: sUrl,
            data: searchData,
            type: 'GET',
            cache: false,
            contentType: 'text/json',
            success: function (data) {
                if (data != null) {
                    hiradServerCRUD.loadServerDetails(data);
                    hiradServerCRUD.disableCtrl('disabled');
                    $('#dvLoadModal').modal('hide');
                }
            },
            error: function (data, status, jqXHR) {
                dashboard.showMessage('Danger', 'Search failed. Please try again', false);
                $('#dvLoadModal').modal('hide');
            }
        });
    }

    , loadServerDetails: function (data) {
        var userType = $('#hdnUserType').data('value');

        $('#hiServers').dataTable().fnDestroy();

        $('#hiServers').dataTable({
            "oLanguage": {
                "sSearch": "Search all columns:"
            },
            /* For Default Search */
            //"oSearch": { "sSearch": "AD RAS HI SERVER" },
            "lengthMenu": [[-1, 10, 25, 50, 100], ["All", 10, 25, 50, 100]],
            "bDestroy": true,
            "bResponsive": "true",
            "aaData": data,
            //"pageLength": 25,
            "order": [[1, "asc"]],
            "bStateSave": true,
            "aoColumns": [
            {
                "sWidth": "3%",
                "mDataProp": null,
                "bSortable": false,
                //"mRender": function (o) {
                //    return '<a href="#" class="viewHiradServer" data-serverid=' + o.Id + '>' + 'View' + '</a> ';
                //}
                "mRender": function (o) {
                    if (userType == "A") {
                        return '<div style="text-align:center;"><table><tr><td> <a href="#" class="viewHiradServer" data-serverid=' + o.Id + '>' + '<i class="fa fa-bars" title="View"></i>' + '</a></td>'
                        + '<td>&nbsp;&nbsp;</td><td><a href="#" class="viewHiradServer" data-serverid=' + o.Id + '>' + '<i class="fa fa-edit" title="Edit"></i>' + '</a></td>'
                        + '<td>&nbsp;&nbsp;</td><td><a href="#" class="viewHiradServer" data-serverid=' + o.Id + '>' + '<i class="fa fa-trash" title="Delete"></i>' + '</a></td></tr></table> </div>';
                    }
                    else {
                        return '<div style="text-align:center;"> <a href="#" class="viewHiradServer" data-serverid=' + o.Id + '>' + '<i class="fa fa-bars" title="View"></i>' + '</a>';
                    }
                }
            },
            //{
            //    "sWidth": "3%",
            //    "mDataProp": null,
            //    "bSortable": false,
            //    "mRender": function (o) {
            //        return '<a href="#" class="viewHiradServer" data-serverid=' + o.Id + '>' + 'Edit' + '</a> ';
            //    }
            //},
            //{
            //    "sWidth": "4%",
            //    "mDataProp": null,
            //    "bSortable": false,
            //    "mRender": function (o) {
            //        return '<a href="#" class="viewHiradServer" data-serverid=' + o.Id + '>' + 'Delete' + '</a>';
            //    }
            //},
            {
                "mDataProp": "SystemName",
                "bAutoWidth": true,
                //"sWidth": "15%",
                "mRender": function (data, type, full) {
                    return type == 'display' && data.length > 50
                        ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>'
                        : data;

                }
            },
            {
                "mDataProp": "Location",
                //"sWidth": "10%",
                "bAutoWidth": true,
                "mRender": function (data, type, full) {
                    return type == 'display' && data.length > 50
                        ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>'
                        : data;

                }
            },
            {
                "mDataProp": "SupportStaff",
                //"sWidth": "10%",
                "bAutoWidth": true,
                "orderable": true,
                "searchable": true
            },
               {
                   "mDataProp": "IPAddress",
                   //"sWidth": "10%",
                   "bAutoWidth": true,
                   "mRender": function (data, type, full) {
                       return type == 'display' && data.length > 50
                           ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>'
                           : data;
                   }

               },

                  {
                      "mDataProp": "Comments",
                      //"sWidth": "20%",
                      "bAutoWidth": true,
                      "mRender": function (data, type, full) {
                          return type == 'display' && data.length > 50
                              ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>'
                              : data;
                      }
                      , "visible": false

                  },
                  { "mDataProp": "ApplicationUse" },
            {
                "mDataProp": "Platform",
                //"sWidth": "15%",
                "bAutoWidth": true,
                "mRender": function (data, type, full) {
                    return type == 'display' && data.length > 50
                        ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>'
                        : data;

                }
                , "visible": false
            },

            {
                "mDataProp": "SerialNumber",
                //"sWidth": "15%",
                "bAutoWidth": true,
                "mRender": function (data, type, full) {
                    return type == 'display' && data.length > 50
                        ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>'
                        : data;

                }
                , "visible": false
            },
            {
                "mDataProp": "Processor",
                //"sWidth": "10%",
                "bAutoWidth": true,
                "mRender": function (data, type, full) {
                    return type == 'display' && data.length > 50
                        ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>'
                        : data;

                }
                , "visible": false
            },
            {
                "mDataProp": "Model",
                //"sWidth": "15%",
                "bAutoWidth": true,
                "mRender": function (data, type, full) {
                    return type == 'display' && data.length > 50
                        ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>'
                        : data;

                }
                , "visible": false
            },
            { "mDataProp": "AssetTag", "visible": false },
            { "mDataProp": "TotalCores", "visible": false },
            { "mDataProp": "ABCId", "visible": true },
            { "mDataProp": "Storage", "visible": false },
            { "mDataProp": "CostCenter", "visible": false },
            { "mDataProp": "HDDConfiguration", "visible": false },

            { "mDataProp": "RAM", "visible": false },
            { "mDataProp": "TSMInstalled", "visible": false }
            ],
            "dom": '<"top"l>Bfrt<"bottom"ip><"clear">'
            , buttons: [
                {
                    extend: 'excel',
                    exportOptions: {
                        columns: "thead th:not(.notExport)"
                    }
                },
                {
                    extend: 'pdfHtml5',
                    orientation: 'landscape',
                    pageSize: 'A4',
                    exportOptions: {
                        columns: "thead th:not(.notExport)"
                    },

                    customize: function (doc) {
                        if (doc.content.length > 1)
                            doc.content[1].layout = "Borders";
                    }
                }
            ]           
           
        });


        //if (userType == "G") {
        //    var table = $('#hiServers').DataTable();
        //    // Hide edit & delete column to General User
        //    table.column(1).visible(false);
        //    table.column(2).visible(false);
        //}

        hiradServerCRUD.initOnClickEvents();

        //When pagination happend, need to initialize the event
        $('#hiServers').on('draw.dt', function () {
            hiradServerCRUD.initOnClickEvents();
        });
    }

     , reset_datatable_state: function () {
         localStorage.removeItem('DataTables_hiServers_' + window.location.pathname);
     }

    , initOnClickEvents: function () {
        //on Each Row click
        $(".viewHiradServer").unbind();
        $(".viewHiradServer").on('click', (function () {
            var clickedIcon = $(this).find('i')[0];
            if (clickedIcon != null) {
                if (clickedIcon.title == "Delete") {

                    var userType = $('#hdnUserType').data('value');
                    if (userType == "A") {
                        hiradServerCRUD.confirm_deleteServer($(this).data("serverid"));
                    }
                }
                else {
                    hiradServerCRUD.viewServerInformation($(this).data("serverid"), clickedIcon.title)
                }
            }
        }));

        $("#btnServersAdd").unbind();
        $("#btnServersAdd").on('click', function () {
            hiradServerCRUD.viewServerInformation(0, "Add")
        });

        $("#btnAllSysInfo").on('click', function () {
            hiradServerCRUD.UpdateAllSystemInformation();
        });

    }

    , viewServerInformation: function (serverId, mode) {
        //on Each Row click
        var record = {
            serverId: serverId,
            actionMode: mode
        };
        $.ajax({
            //Calling Partial View
            url: $("#uViewServerInformation").val(),
            type: 'POST',
            data: record,
            async: true,
            success: function (data) {
                if (data != null) {
                    $('#dvMdlDlgContent').html(data);
                    $("#dvModal").modal();
                    hiradServerCRUD.showHideButtons();
                }
            },
            error: function (data, status, jqXHR) {

                console.log('Error in view Server Information: ' + jqXHR + "," + data.responseText);
            }
        });
    }

    , showHideButtons: function () {
        if ($("#hdnServerAction").val() == "View") {
            $("#btnServerUpdate").hide();
            $("#btnGetSystemInfo").attr("disabled", "disabled");
        }
        else if ($("#hdnServerAction").val() == "Edit") {
            $("#btnServerUpdate").on("click", function () {
                if (hiradServerCRUD.validateServer()) {
                    hiradServerCRUD.updateServer();
                }
            });

            $("#btnGetSystemInfo").on("click", function (event) {
                event.preventDefault();
                hiradServerCRUD.GetSystemInformation($("#txtSysn").val());
            });
        }
        else if ($("#hdnServerAction").val() == "Add") {

            $("#btnServerUpdate").text("Add Server");
            $("#btnServerUpdate").on("click", function () {
                if (hiradServerCRUD.validateServer()) {
                    hiradServerCRUD.updateServer();
                }
            });

            $("#btnGetSystemInfo").on("click", function (event) {
                event.preventDefault();
                hiradServerCRUD.GetSystemInformation($("#txtSysn").val());
            });
        }
        else {
            $("#btnServerUpdate").hide();
            $("#btnGetSystemInfo").attr("disabled", "disabled");
        }

        if ($("#hdnServerAction").val() == "View") {
            $("#dvModal").find('input:text').attr('disabled', 'disabled');
            $("#dvModal").find('input:checkbox').attr('disabled', 'disabled');
            $("#dvModal").find('input:radio').attr('disabled', 'disabled');
            $("#dvModal").find('textarea').attr('disabled', 'disabled');
            $("#dvModal").find('select').attr('disabled', 'disabled');
        }
    }
     , validateServer: function () {

         if ($.trim($('#txtSysn').val()) == '') {
             dashboard.showMessage('warning', 'Please enter System Name', false);
             return false;
         }
         return true;
     }
    , updateServer: function () {        
        var serverId = $.trim($('#hdnSelectedRowId').val());
        var systemname = $.trim($('#txtSysn').val());
        var location = $.trim($('#txtLoc').val());
        var serialNumber = $.trim($('#txtSn').val());
        var assetTag = $.trim($('#txtAt').val());
        var abcId = $.trim($('#txtAbc').val());
        var costCenter = $.trim($('#txtCc').val());
        var supportStaff = $.trim($('#txtSs').val());
        var model = $.trim($('#txtMo').val());
        var platform = $.trim($('#txtPf').val());
        var ipAddress = $.trim($('#txtIa').val());
        var processor = $.trim($('#txtProcessor').val());
        var totalCores = $.trim($('#txtTot').val());
        var buildVersion = $.trim($('#txtBuildVersion').val());
        var hddConfiguration = $.trim($('#txtHdd').val());
        var RAM = $.trim($('#txtRam').val());
        var tsmInstalled = $("input[name='rdTSMI']:checked").val();
        var comments = $.trim($('#txtComment').val());
        var isMonitor = $("#chkIsMonitor").is(':checked') ? true : false;
        var bootTime = $.trim($('#txtBootTime').val());
        // var domain = $.trim($('#txtDomain').val());
        var newServerId = $.trim($("#txtNewServerId").val());
        var statusChangedOn = $.trim($("#txtStatusTypeChangedOn").val());
        var statusTypeId = $("#ddlStatusType option:selected").val();
        var prvStatusTypeId = $.trim($("#hdnPrvStatusTypeId").val());
        var createdBy = $("#hdnCreatedBy").val();
        var createdDate = $("#hdnCreatedDate").val();
        var appUse = $.trim($("#txtAppUse").val());

        var rec = {
            Id: serverId,
            SystemName: systemname,
            Location: location,
            SerialNumber: serialNumber,
            AssetTag: assetTag,
            ABCId: abcId,
            CostCenter: costCenter,
            SupportStaff: supportStaff,
            Model: model,
            Platform: platform,
            IPAddress: ipAddress,
            Processor: processor,
            TotalCores: totalCores,
            HDDConfiguration: hddConfiguration,
            RAM: RAM,
            TSMInstalled: tsmInstalled,
            Comments: comments,
            IsMonitor: isMonitor,
            IsDeleted: false,
            BuildVersion: buildVersion,
            LastBootTime: bootTime,
            //     Domain: domain,
            StatusTypeChangedOn: statusChangedOn,
            NewServerId: newServerId,
            PreviousStatusTypeId: prvStatusTypeId,
            StatusTypeId: statusTypeId,
            CreatedBy: createdBy,
            CreatedDate: createdDate,
            ApplicationUse: appUse
        };

        var sUrl = $('#hdnServerSaveUrl').val();
        $.ajax({
            url: sUrl,
            data: rec,
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data != null) {
                    if (data.RecStatus == "Saved") {
                        hiradServerCRUD.refreshServerCtrl();

                        //Refresh DataTable
                        hiradServerCRUD.searchResult('Y');

                        if (serverId == 0 || serverId == '') {
                            dashboard.showMessage('success', 'Server (' + systemname + ') added successfully.', true);

                        } else {
                            dashboard.showMessage('success', 'Server (' + systemname + ') updated successfully.', true);
                        }
                    }
                    else if (data != null && data.RecStatus == "Duplicate") {
                        var sDupMsg = 'System Name already exists. Please enter any other System Name.';
                        dashboard.showMessage('warning', sDupMsg, false);
                    }
                    else {
                        dashboard.showMessage('danger', 'Validation failed.', false);
                    }
                }
            },
            error: function (data, status, jqXHR) {
                hiradServerCRUD.refreshServerCtrl();
                dashboard.showMessage('danger', 'Save failed. Please try again...', false);
            }
        });
    }

    , refreshServerCtrl: function () {
        hiradServerCRUD.clearData();
        hiradServerCRUD.disableCtrl('disabled');
        hiradServerCRUD.clearServerSearchCtrl();
    }

    , clearServerSearchCtrl: function () {
        $("#txtSearchSname").val('');
        $("#txtSearchLoc").val('');
        $("#txtSearchPro").val('');
        $("#txtSearchIpa").val('');
        $("#txtSearchPF").val('');
        $("#txtSearchCC").val('');
        $("#txtSearchABC").val('');
        $("#txtSearchSN").val('');
        $("#txtAppUse").val('');
    }

    , clearData: function () {
        $("#modalpopup").find('input:text').val('');
        $("#modalpopup").find('textarea').val('');
        $("input:checked").removeAttr('checked');
        $('#hdnSelectedRowId').val('');
    }

    , disableCtrl: function (disable) {
        var userType = $('#hdnUserType').data('value');
        if (disable == 'disabled') {
            if (userType == "G") {
                $("#btnServersAdd").attr('disabled', 'disabled');
            }
        }
    }


    , confirm_deleteServer: function (sId) {
        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this server!",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "No, cancel it!",
            closeOnConfirm: true,
            closeOnCancel: true
        },
        function (isConfirm) {
            if (isConfirm) {
                //swal("Deleted!", "Your server has been deleted.", "success");
                hiradServerCRUD.deleteServer(sId);
            }
        });
    }

    , deleteServer: function (sId) {
        var sUrl = $('#hdnServerDeleteUrl').val();
        var rec = {
            serverId: sId
        };
        $.ajax({
            url: sUrl,
            data: rec,
            dataType: 'json',
            type: 'POST',
            async: true,
            success: function (data) {
                if (data != null) {
                    if (data.RecStatus == "Deleted") {
                        hiradServerCRUD.refreshServerCtrl();
                        hiradServerCRUD.searchResult('Y');
                        dashboard.showMessage('success', 'Server deleted successfully.', true);
                    }
                    else if (data != null && data.RecStatus == "Not Deleted") {
                        var sDupMsg = 'We could not delete this server since it is used in Client Apps and Web Apps. Please complete the decommision process and try again later.';
                        dashboard.showMessage('warning', sDupMsg, false);
                    }
                }
            },
            error: function (data, status, jqXHR) {
                console.log(jqXHR);
                dashboard.showMessage('danger', 'Failed to delete the server details. Please try again.', false);
            }
        });
    }

    , GetSystemInformation: function (systemName) {
        //        
        $("#dvLoadModal").modal({ backdrop: 'static', keyboard: false });
        if (systemName.trim() != "") {
            var sUrl = $('#hdnGetSystemInformationUrl').val();
            $.ajax({
                url: sUrl,
                data: { systemName: systemName },
                dataType: 'json',
                type: 'POST',
                async: true,
                success: function (data) {
                    $("#dvLoadModal").modal("hide");
                    if (data != null) {
                        if (data.RecStatus != undefined && data.RecStatus == "Invalid SystemName") {
                            dashboard.showMessage('warning', 'Enter valid System name!', true);
                        }
                        else if (data.ErrorInfo != null && data.ErrorInfo != "") {
                            dashboard.showMessage('danger', data.ErrorInfo + "", false);
                        }
                        else {
                            console.log(data);
                            //Map the server details
                            $("#txtIa").val(data.IPAddress);
                            $("#txtPf").val(data.OsName);
                            $("#txtBuildVersion").val(data.OsVersion);
                            $("#txtMo").val(data.SystemModel);
                            $("#txtProcessor").val(data.Processor);
                            $("#txtTot").val(data.TotalCores);
                            $("#txtHdd").val(data.TotalHDD);
                            $("#txtRam").val(data.TotalRAM);
                            $("#txtBootTime").val(data.LastBootTimeText);
                            //  $("#txtDomain").val(data.Domain);
                        }
                    }

                },
                error: function (data, status, jqXHR) {
                    $("#dvLoadModal").modal("hide");
                    dashboard.showMessage('Danger', 'Failed to delete the server details. Please try again.', false);
                }
            });
        }
        return false;
    }

    , UpdateAllSystemInformation: function () {
        var sUrl = $('#uUpdateAllSystemInformation').val();
        $("#dvLoadModal").modal({ backdrop: 'static', keyboard: false });
        $.ajax({
            url: sUrl,
            dataType: 'json',
            type: 'POST',
            async: true,
            success: function (data) {
                if (data != null) {
                    $("#dvLoadModal").modal("hide");
                    if (data.RecStatus == "Updated") {
                        dashboard.showMessage('success', 'Server record Updated successfully', true);
                    }
                }
            },
            error: function (data, status, jqXHR) {
                $("#dvLoadModal").modal("hide");
                console.log(jqXHR);
                dashboard.showMessage('danger', 'Failed to UpdateAllSystemInformation server details. Please try again.', false);
            }
        });
    }

    //End of Namespace
}



//Top Row
hiradRAM = {
    GetCriticalRAMCount: function () {
        var sUrl = $("#uGetCriticalRAMCount").val();

        $.getJSON(sUrl, function (data) {
            if (data != null) {
                //Other Columns
                $("#spanRAMCount").text(data.RAMCount);
                $("#spanLastUpdate").text(data.LastMonitoredAt);
                if (data.RAMCount == 0) {
                    $("#ramWaringCount").hide();
                    $("#aViewRamList").hide();
                }
                else {
                    $("#ramLastUpdate").hide();
                    $("#spanLastUpdate").hide();
                    //Initialize the event
                    hiradRAM.GetAllCriticalRAM();
                }
            }
            $("#spanRamThersold").text($("#hdnRamThreshold").val() + "% ");
        });

    }

    , GetAllCriticalRAM: function () {
        $("#aViewRamList, #spanRAMCount").on("click", function () {
            $.ajax({
                //Calling Partial View
                url: $("#uGetAllCriticalRAM").val(),
                type: 'GET',
                success: function (data) {
                    if (data != null) {
                        $('#dvMdlDlgContent').html(data);
                        $("#dvModal").modal();
                    }
                },
                error: function (data, status, jqXHR) {
                    console.log('Error in view GetAllCriticalRAM view: ' + jqXHR);
                }
            });
        });
    }

    //Chart
    , GetAllCriticalRAMChart: function () {

        var sUrl = $("#uGetRAMChartData").val();
        var inputData = {
            chartName: 'AllCriticalRAM'
        };

        //hiradRAM.PrepareRAMChart(sUrl, inputData);       
        hiradRAM.PrepareCriticalRAMChart(sUrl, inputData);
    }

    , PrepareCriticalRAMChart: function (sUrl, inuptData) {

        $.getJSON(sUrl, inuptData, function (data) {
            if (data != null) {                
                dataRamUsageChart = {
                    labels: data.labels,
                    series: data.series
                };                

                optionsRamUsageChart = {
                    lineSmooth: Chartist.Interpolation.cardinal({
                        tension: 0
                    }),
                    low: 0,
                    high: 101,
                    chartPadding: { top: 0, right: 0, bottom: 0, left: 0 },
                    plugins: [
                      Chartist.plugins.tooltip(
                          {
                              transformTooltipTextFnc: function (value) {
                                  return value + '%';
                              }
                          }),
                    ],
                    distributeSeries: true,
                    axisX: {
                        //Skip the bottom text by 3
                        labelInterpolationFnc: function (value, index) {
                            return index % 3 === 0 ? value : null;
                        }
                    }
                    ,
                     axisY: {
                         labelInterpolationFnc: function (value, index) {                            
                            return value + "%";
                        }
                    }
                }

                var ramUsageChart = new Chartist.Bar('#chartRAMUsage', dataRamUsageChart, optionsRamUsageChart);

                md.startAnimationForBarChart(ramUsageChart);
            }
        });
    }

    , GetServerRAMByDate: function (serverId) {
        var sUrl = $("#uGetRAMChartData").val();
        var inputData = {
            serverId: serverId,
            chartName: 'ServerRAMByDate'
        };

          hiradRAM.PrepareRAMChart(sUrl, inputData);        
    }
    , PrepareRAMChart: function (sUrl, inuptData) {

        $.getJSON(sUrl, inuptData, function (data) {
            if (data != null) {
                dataRamUsageChart = {
                    labels: data.labels,
                    series: data.series
                };

                optionsRamUsageChart = {
                    lineSmooth: Chartist.Interpolation.cardinal({
                        tension: 0,
                        fillHoles: true,
                    }),
                    low: 0,
                    high: 101,
                    chartPadding: { top: 0, right: 0, bottom: 0, left: 0 },
                    plugins: [
                      Chartist.plugins.tooltip({
                          transformTooltipTextFnc: function (value) {
                              return value + '%';
                          }
                      }),
                    ]
                    , axisX: {
                        offset: 0,       
                        //Skip the bottom text by 2
                        labelInterpolationFnc: function (value, index) {
                            return index % 5 === 0 ? value : null;
                        }
                    }
                    , axisY: {
                        labelInterpolationFnc: function (value, index) {
                            return value + "%";
                        }
                    }
                }

                var ramUsageChart = new Chartist.Line('#chartRAMUsage', dataRamUsageChart, optionsRamUsageChart);

                // start animation for the Completed Tasks Chart - Line Chart
                md.startAnimationForLineChart(ramUsageChart);
                md.startAdditionalAnimationForLineChart(ramUsageChart);
            }
        });
    }
}

hiradHdd = {
    GetCriticalHddCount: function () {
        var sUrl = $("#uGetCriticalHddCount").val();

        $.getJSON(sUrl, function (data) {
            if (data != null) {
                //Other Columns
                $("#spanHddCount").text(data.RAMCount);
                $("#spanHddLastUpdate").text(data.LastMonitoredAt);
                if (data.RAMCount == 0) {
                    $("#HddWaringCount").hide();
                    $("#aViewHddList").hide();
                }
                else {
                    $("#HddLastUpdate").hide();
                    $("#spanHddLastUpdate").hide();
                    //Initialize the event
                    hiradHdd.GetAllCriticalHdd();
                }
            }
        });
        $("#spanHddThersold").text($("#hdnHddThreshold").val() + "% ");
    }

    , GetAllCriticalHdd: function () {
        $("#aViewHddList, #spanHddCount").on("click", function () {
            $.ajax({
                //Calling Partial View
                url: $("#uGetAllCriticalHdd").val(),
                type: 'GET',
                success: function (data) {
                    if (data != null) {
                        $('#dvMdlDlgContent').html(data);
                        $("#dvModal").modal();
                    }
                },
                error: function (data, status, jqXHR) {
                    console.log('Error in view GetAllCriticalHdd Count: ' + jqXHR);
                }
            });
        });
    }
}

hiradReb = {
    GetCriticalRebCount: function () {
        var sUrl = $("#uGetCriticalRebCount").val();

        $.getJSON(sUrl, function (data) {
            if (data != null) {
                //Other Columns
                $("#spanRebCount").text(data.RebootResetDays);
                if (data.RebootResetDays == 0) {
                    $("#aViewRebList").hide();
                }
                else {
                    //Initialize the event
                    hiradReb.GetAllCriticalReb();
                }
            }
        });
        $("#spanRebootResetDays").text($("#hdnspanRebootResetDays").val() + "");
    }

    , GetAllCriticalReb: function () {
        $("#aViewRebList, #spanRebCount").on("click", function () {
            $.ajax({
                //Calling Partial View
                url: $("#uGetAllCriticalReb").val(),
                type: 'GET',
                success: function (data) {
                    if (data != null) {
                        $('#dvMdlDlgContent').html(data);
                        $("#dvModal").modal();
                    }
                },
                error: function (data, status, jqXHR) {
                    console.log('Error in view GetAllCriticalReb Count: ' + jqXHR);
                }
            });
        });
    }
}

//Portlets
portletMonitoring = {
    //Servers
    SMServers: function () {
        var sUrl = $("#uGetAllServerLogStatusByLastRun").val();
        //Get Data
        $.getJSON(sUrl, function (data) {            
            if (data != null && data.length>0) {               
                portletMonitoring.LoadServerMonitorData(data);
                initJsMethods.initLogRowPopup();
                $("#spanSMServerMoniteredAt").text("Last Monitored At " + data[0].MonitoredAt);
            }
        });
    }

    , LoadServerMonitorData: function (data) {
        $('#tblSMServers').dataTable({
            "bDestroy": true,
            "aaData": data,
            "scrollY": '20vh',
            "scrollCollapse": true,
            "pageLength": -1,
            "lengthMenu": [[-1, 10, 20, 50, 100], ["All", 10, 20, 50, 100]],
            "bAutoWidth": false,
            "aoColumns": [
                {
                    "mData": "View",
                    "width": "15px",
                    "bSortable": false,
                    "className": "tdactions",
                    'orderable': false,
                    'targets': 0,
                    "mRender": function (full, type, data) {
                        return '<button type="button" rel="tooltip" title="View Logs"'
                                    + 'data-ServerId="' + data.ServerId + '"'
                                    + 'class="btn btn-primary btn-simple btn-xs viewServer">'
                                        + '<i class="fa fa-bars" aria-hidden="true"></i>'
                                    + '</button>';
                    }
                }
                , { "mData": "SystemName", "width": "150px" }
                , { "mData": "MonitoredAt", "width": "115px", "visible": false }
                , { "mData": "Status", "width": "50px" }
                , { "mData": "ErrorDescription", "width": "220px" }
            ]
            , "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                $(nRow).children().each(function (index, td) {
                    if (index == 2) {
                        if ($(td).html() === "Failed") {
                            $(td).addClass('text-danger');
                        } else {
                            $(td).addClass('text-success');
                        }
                    }
                });
                return nRow;
            }
            , 'aaSorting': [[3, 'asc']]
            , "fnInitComplete": function () {
                $("#tblSMServers").css("width", "100%");
                $(".dataTables_scrollHead").css("width", "100%");
                $(".dataTables_scrollHeadInner").css("width", "100%");
                $(".dataTables_scrollHeadInner table").css("width", "100%");
            }
        });

        //When pagination happend, need to initialize the event
        $('#tblSMServers').on('draw.dt', function () {
            initJsMethods.initLogRowPopup();
        });
    }

    //RAM
    , SMRam: function () {
        var sUrl = $("#uGetAllRAMStatusByLastRun").val();
        //Get Data
        $.getJSON(sUrl, function (data) {
            if (data != null) {
                portletMonitoring.LoadRAMData(data);
            }
        });
    }
    , LoadRAMData: function (data) {
        $('#tblSMRAM').dataTable({
            "bDestroy": true,
            "aaData": data,
            "scrollY": '20vh',
            "scrollCollapse": true,
            "pageLength": -1,
            "lengthMenu": [[-1, 10, 20, 50, 100], ["All", 10, 20, 50, 100]],
            "bAutoWidth": false,
            "aoColumns": [
                 { "mData": "SystemName", "width": "140px" }
                , { "mData": "MonitoredAt", "width": "150px", "visible": false }
                , { "mData": "RAM", "width": "100px" }
                , { "mData": "AvblRAM", "width": "100px" }
                , { "mData": "RAMPercentage", "width": "100px" }
            ]
            , "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                $(nRow).children().each(function (index, td) {
                    if (index == 3) {
                        if (parseInt($(td).html()) >= parseInt($("#hdnRamThreshold").val())) {
                            $(td).addClass('text-danger');
                        } else {
                            $(td).addClass('text-success');
                        }
                    }
                });
                return nRow;
            }
            , 'aaSorting': [[4, 'desc']]
            , "fnInitComplete": function () {
                $("#tblSMRAM").css("width", "100%");
                $(".dataTables_scrollHead").css("width", "100%");
                $(".dataTables_scrollHeadInner").css("width", "100%");
                $(".dataTables_scrollHeadInner table").css("width", "100%");
            }
        });
    }
    //End of RAM

    //Hdd
    , SMHdd: function () {
        var sUrl = $("#uGetAllHddStatusByLastRun").val();
        //Get Data
        $.getJSON(sUrl, function (data) {
            if (data != null) {
                portletMonitoring.LoadHddData(data);
            }
        });
    }
    , LoadHddData: function (data) {
        $('#tblSMHdd').dataTable({
            "bDestroy": true,
            "aaData": data,
            "scrollY": '20vh',
            "scrollCollapse": true,
            "pageLength": -1,
            "lengthMenu": [[-1, 10, 20, 50, 100], ["All", 10, 20, 50, 100]],
            "bAutoWidth": false,
            "aoColumns": [
                 { "mData": "SystemName", "width": "100px" }
                , { "mData": "MonitoredAt", "width": "100px", "visible": false }
                , { "mData": "HDDConfiguration", "width": "150px" }
                , { "mData": "AvblHDDSpace", "width": "150px" }
                , { "mData": "HddPercentage", "width": "50px" }
            ]
            , "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                $(nRow).children().each(function (index, td) {
                    if (index == 3) {
                        if (parseInt($(td).html()) >= parseInt($("#hdnHddThreshold").val())) {
                            $(td).addClass('text-danger');
                        } else {
                            $(td).addClass('text-success');
                        }
                    }
                });
                return nRow;
            }
            , 'aaSorting': [[4, 'desc']]
            , "fnInitComplete": function () {
                $("#tblSMHdd").css("width", "100%");
                $(".dataTables_scrollHead").css("width", "100%");
                $(".dataTables_scrollHeadInner").css("width", "100%");
                $(".dataTables_scrollHeadInner table").css("width", "100%");
            }
        });
    }
    //End of Hdd

    //Reboot
    , SMReboot: function () {
        var sUrl = $("#uGetAllRebStatusByLastRun").val();
        //Get Data
        $.getJSON(sUrl, function (data) {
            if (data != null) {
                portletMonitoring.LoadRebootData(data);
            }
        });
    }
    , LoadRebootData: function (data) {
        $('#tblSMReboot').dataTable({
            "bDestroy": true,
            "aaData": data,
            "scrollY": '20vh',
            "scrollCollapse": true,
            "pageLength": -1,
            "lengthMenu": [[-1, 10, 20, 50, 100], ["All", 10, 20, 50, 100]],
            "bAutoWidth": false,
            "aoColumns": [
                 { "mData": "SystemName", "width": "100px" }
                , { "mData": "MonitoredAt", "width": "100px", "visible": false }
                , { "mData": "LastBootTime", "width": "150px" }
                , { "mData": "LastBootInDays", "width": "150px" }
            ]
            , "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                $(nRow).children().each(function (index, td) {
                    if (index == 2) {
                        if (parseInt($(td).html()) >= parseInt($("#hdnspanRebootResetDays").val())) {
                            $(td).addClass('text-danger');
                        } else {
                            $(td).addClass('text-success');
                        }
                    }
                });
                return nRow;
            }
            , 'aaSorting': [[3, 'desc']]
            , "fnInitComplete": function () {
                $("#tblSMReboot").css("width", "100%");
                $(".dataTables_scrollHead").css("width", "100%");
                $(".dataTables_scrollHeadInner").css("width", "100%");
                $(".dataTables_scrollHeadInner table").css("width", "100%");
            }
        });
    }
    //End of Reboot

    //SMWebsites
    , SMWebsites: function () {
        var sUrl = $("#uGetAllWebLogStatusByLastRun").val();
        //Get Data
        $.getJSON(sUrl, function (data) {
            if (data != null) {
                portletMonitoring.LoadWebsitesData(data);
                initJsMethods.initLogRowPopup();
            }
        });
    }
    , LoadWebsitesData: function (data) {
        $('#tblSMWeb').dataTable({
            "bDestroy": true,
            "aaData": data,
            "scrollY": '20vh',
            "scrollCollapse": true,
            "pageLength": -1,
            "lengthMenu": [[-1, 10, 20, 50, 100, -1], ["All",10, 20, 50, 100]],
            "bAutoWidth": false,
            "aoColumns": [
                {
                    "mData": "View",
                    "width": "15px",
                    "bSortable": false,
                    "className": "tdactions",
                    'orderable': false,
                    'targets': 0,
                    "mRender": function (full, type, data) {
                        return '<button type="button" rel="tooltip" title="View Logs"'
                                    + 'data-WebId="' + data.WebId + '"'
                                    + 'class="btn btn-primary btn-simple btn-xs viewWebLog">'
                                        + '<i class="fa fa-bars" aria-hidden="true"></i>'
                                    + '</button>';
                    }
                }
                , {
                    "mData": "WebFolder", "width": "150px",
                    "mRender": function (full, type, data) {
                        return '<a href="' + data.WebSite + '" target="_blank" >' +
                                    data.WebFolder + '</a>';
                    }
                }
                , { "mData": "MonitoredAt", "width": "115px", "visible": false }
                , { "mData": "Status", "width": "50px" }
                , { "mData": "ErrorDescription", "width": "220px" }
            ]
            , "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                $(nRow).children().each(function (index, td) {
                    if (index == 2) {
                        if ($(td).html() === "Failed") {
                            $(td).addClass('text-danger');
                        } else {
                            $(td).addClass('text-success');
                        }
                    }
                });
                return nRow;
            }
            , 'aaSorting': [[3, 'asc']]
            , "fnInitComplete": function () {
                $("#tblSMWeb").css("width", "100%");
                $(".dataTables_scrollHead").css("width", "100%");
                $(".dataTables_scrollHeadInner").css("width", "100%");
                $(".dataTables_scrollHeadInner table").css("width", "100%");
            }
        });

        //When pagination happend, need to initialize the event
        $('#tblSMWeb').on('draw.dt', function () {
            initJsMethods.initLogRowPopup();
        });
    }
    //End of SMWebsites
    //SMDatabases
    , SMDatabase: function () {      
        var sUrl = $("#uGetAllDBLogStatusByLastRun").val();
        //Get Data
        $.getJSON(sUrl, function (data) {          
            if (data != null) {
                portletMonitoring.LoadDBData(data);
                initJsMethods.initLogRowPopup();
            }
        });
    }
    , LoadDBData: function (data) {
        $('#tblSMDB').dataTable({
            "bDestroy": true,
            "aaData": data,
            "scrollY": '20vh',
            "scrollCollapse": true,
            "pageLength": -1,
            "lengthMenu": [[-1, 10, 20, 50, 100], ["All", 10, 20, 50, 100]],
            "bAutoWidth": false,
            "aoColumns": [
                {
                    "mData": "View",
                    "width": "15px",
                    "bSortable": false,
                    "className": "tdactions",
                    'orderable': false,
                    'targets': 0,
                    "mRender": function (full, type, data) {
                        return '<button type="button" rel="tooltip" title="View Logs"'
                                    + 'data-dbmonitorid="' + data.DbMonitorId + '"'
                                    + 'class="btn btn-primary btn-simple btn-xs viewDBLog">'
                                        + '<i class="fa fa-bars" aria-hidden="true"></i>'
                                    + '</button>';
                    }
                }
                 , { "mData": "Application", "width": "150px", "visible": false }
                  , { "mData": "DbName", "width": "150px" }
                , { "mData": "DBServerName", "width": "150px", "visible": false }

                , { "mData": "MonitoredAt", "width": "115px", "visible": false }
                , { "mData": "Status", "width": "50px" }
                , { "mData": "ErrorDescription", "width": "220px" }
            ]
            , "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                $(nRow).children().each(function (index, td) {
                    if (index == 2) {
                        if ($(td).html() === "Failed") {
                            $(td).addClass('text-danger');
                        } else {
                            $(td).addClass('text-success');
                        }
                    }
                });
                return nRow;
            }
            , 'aaSorting': [[3, 'asc']]
            , "fnInitComplete": function () {
                $("#tblSMDB").css("width", "100%");
                $(".dataTables_scrollHead").css("width", "100%");
                $(".dataTables_scrollHeadInner").css("width", "100%");
                $(".dataTables_scrollHeadInner table").css("width", "100%");
            }
        });

        //When pagination happend, need to initialize the event
        $('#tblSMDB').on('draw.dt', function () {
            initJsMethods.initLogRowPopup();
        });
    }
    //End of SMDatabases

}


hiradWebLog = {
    viewLogRowPopup: function (webId) {
        //on Each Row click
        var record = {
            webId: webId
        };
        $.ajax({
            //Calling Partial View
            url: $("#uGetAllWebLog").val(),
            type: 'GET',
            data: record,
            success: function (data) {
                if (data != null) {
                    $('#dvMdlDlgContent').html(data);
                    $("#dvModal").modal();
                }
            },
            error: function (data, status, jqXHR) {
                console.log('Error in view website Log viewLogRowPopup: ' + jqXHR);
            }
        });
    }
}

sharepointSites = {
    //Sharepoint Sites
    SPSites: function () {
        var sUrl = $("#uGetAllSPSites").val();
        //Get Data
        $.getJSON(sUrl, function (data) {
            if (data != null) {
                sharepointSites.LoadSPsitesData(data);
            }
        });
    }
    , LoadSPsitesData: function (data) {
        $('#tblSPSites').dataTable({
            "bDestroy": true,
            "aaData": data,
            "scrollY": '20vh',
            "scrollCollapse": true,
            "pageLength": -1,
            "lengthMenu": [[-1, 10, 20, 50, 100], ["All", 10, 20, 50, 100]],
            "bAutoWidth": false,
            "aoColumns": [
                {
                    "mData": "WebFolder", "width": "150px",
                    "mRender": function (full, type, data) {
                        return '<a href="' + data.WebSite + '" target="_blank" >' +
                                    data.WebFolder + '</a>';
                    }
                }
                , { "mData": "PrimayContact", "width": "100px" }
                , { "mData": "SecondaryContact", "width": "100px" }
                , { "mData": "BPDept", "width": "100px" }
                , { "mData": "BPContact", "width": "100px" }
                , { "mData": "BPPhone", "width": "100px" }
            ]
            , 'aaSorting': [[0, 'asc']]
            , "fnInitComplete": function () {
                $("#tblSMWeb").css("width", "100%");
                $(".dataTables_scrollHead").css("width", "100%");
                $(".dataTables_scrollHeadInner").css("width", "100%");
                $(".dataTables_scrollHeadInner table").css("width", "100%");
            }
        });
    }
    //End of Sharepoint
}


hiradDBLog = {
    viewLogRowPopup: function (dbMonitorId) {
        //on Each Row click
        var record = {
            DbMonitorId: dbMonitorId
        };
        $.ajax({
            //Calling Partial View
            url: $("#uGetAllDBLog").val(),
            type: 'GET',
            data: record,
            success: function (data) {
                if (data != null) {
                    $('#dvMdlDlgContent').html(data);
                    $("#dvModal").modal();
                }
            },
            error: function (data, status, jqXHR) {
                console.log('Error in view DB Log viewLogRowPopup: ' + jqXHR);
            }
        });
    }
}
