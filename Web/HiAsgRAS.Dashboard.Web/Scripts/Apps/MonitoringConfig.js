

MonitoringConfig = {
    //Servers    
    SMServers: function () {
        var sUrl = $("#uServerList").val();
        $.ajax({
            url: sUrl,
            type: 'GET',
            cache: false,
            contentType: 'text/json',
            success: function (data) {
                if (data != null) {
                    MonitoringConfig.LoadServer(data);
                }
            },
            error: function (data, status, jqXHR) {
                dashboard.showMessage('danger', 'Load failed.Please try again', false);
            }
        });
    },

    //WebApps    
    SMWebs: function () {
        var sUrl = $("#uWebList").val();
        $.ajax({
            url: sUrl,
            type: 'GET',
            cache: false,
            contentType: 'text/json',
            success: function (data) {
                if (data != null) {
                    MonitoringConfig.LoadWeb(data);
                }
            },
            error: function (data, status, jqXHR) {
                dashboard.showMessage('danger', 'Load failed.Please try again', false);
            }
        });
    },

    //DBServers    
    SMDBServers: function () {
        var sUrl = $("#uDBList").val();
        $.ajax({
            url: sUrl,
            type: 'GET',
            cache: false,
            contentType: 'text/json',
            success: function (data) {

                if (data != null) {
                    MonitoringConfig.LoadDBServers(data);
                }
            },
            error: function (data, status, jqXHR) {

                dashboard.showMessage('danger', 'Load failed.Please try again', false);
            }
        });
    },

    initOnClickEvents: function () {
        // Handle click on button    
        $("#btnServerUpdate").on("click", function () {
            MonitoringConfig.CallUpdate();
        });
        $("#btnServerUpdate1").on("click", function () {
            MonitoringConfig.CallUpdate();
        });
    },

    LoadServer: function (data) {

        var table = $('#tblSMServers').DataTable({
            "bPaginate": false,
            "bDestroy": true,
            "bResponsive": "true",
            "aaData": data,
            "bStateSave": true,

            "aoColumns": [
                {
                    "sTitle": '<input type="checkbox" id="check_Server" name="check_Server" >',
                    "mData": "IsMonitor",
                    "bSortable": false,
                    "className": "td-actions",
                    'orderable': false,
                    'targets': 0,
                    "mRender": function (full, type, data) {
                        if (data.IsMonitor == true) {
                            return '<input type="checkbox" checked="checked" value="' + data.Id + '">';
                        }
                        else {
                            return '<input type="checkbox" value="' + data.Id + '">';
                        }
                    }
                }
                , {
                    "mData": "SystemName",
                    "width": "25%"
                }
                , {
                    "mData": "Location",
                    "width": "25%"
                }
                , {
                    "mData": "IPAddress",
                    "width": "20%"
                }
                , {
                    "mData": "SupportStaff",
                    "width": "25%"
                }
            ],

            "bAutoWidth": false,
            'select': {
                'style': 'multi'
            },
            'order': [[1, 'asc']]
        });

        table.columns.adjust().draw();
        $('#check_Server').change(function () {
            var checked = $(this).is(":checked");

            if (checked) {
                $('#tblSMServers tbody input[type="checkbox"]').prop('checked', this.checked);
            }
            else {
                $('#tblSMServers tbody input[type="checkbox"]').prop('checked', '');
            }

        });
    },
    LoadWeb: function (data) {

        var table = $('#tblSMWeb').DataTable({
            "bPaginate": false,
            "bDestroy": true,
            "bResponsive": "true",
            "aaData": data,
            "bStateSave": true,
            "aoColumns": [
                          {
                              "sTitle": '<input type="checkbox" id="check_all_Web" name="check_all_Web" >',
                              "mData": "IsMonitor",
                              "bSortable": false,
                              "className": "td-actions",
                              'orderable': false,
                              'targets': 0,
                              "mRender": function (full, type, data) {
                                  if (data.IsMonitor == true) {
                                      return '<input type="checkbox" checked="checked" value="' + data.Id + '">';
                                  }
                                  else {
                                      return '<input type="checkbox" value="' + data.Id + '">';
                                  }
                              }
                          }
                          , {
                              "mData": "WebFolder",
                              "width": "25%"
                          }
                          ,
                          {
                              "mData": "WebStat",
                              "width": "25%"
                          }
                          , {
                              "mData": "RemedyGroupName",
                              "width": "20%"
                          }
                          , {
                              "mData": "PrimayContact",
                              "width": "25%"
                          }
            ],
            "bAutoWidth": false,
            'select': {
                'style': 'multi'
            },
            'order': [[1, 'asc']]
        });
        table.columns.adjust().draw();
        $('#tblSMWeb #check_all_Web').change(function () {

            var checked = $(this).is(":checked");

            if (checked) {
                $('#tblSMWeb tbody input[type="checkbox"]').prop('checked', this.checked);
            }
            else {
                $('#tblSMWeb tbody input[type="checkbox"]').prop('checked', '');
            }

        });
    },
    LoadDBServers: function (data) {

        var table = $('#tblSMDB').DataTable({
            "bPaginate": false,
            "bDestroy": true,
            "bResponsive": "true",
            "aaData": data,
            "bStateSave": true,
            "aoColumns": [
                          {
                              "sTitle": '<input type="checkbox" id="check_all_Db" name="check_all_Db" >',
                              "mData": "IsMonitor",
                              "bSortable": false,
                              "className": "td-actions",
                              'orderable': false,
                              'targets': 0,
                              "mRender": function (full, type, data) {
                                  if (data.IsMonitor == true) {
                                      return '<input type="checkbox" checked="checked" value="' + data.Id + '">';
                                  }
                                  else {
                                      return '<input type="checkbox" value="' + data.Id + '">';
                                  }
                              }
                          }
                          , {
                              "mData": "DBServer",
                              "width": "40%"
                          }
                          , {
                              "mData": "DbName",
                              "width": "25%"
                          }
                          , {
                              "mData": "Application",
                              "width": "30%"
                          }

            ],

            'select': {
                'style': 'multi'
            },
            'order': [[1, 'asc']]

        });
        table.columns.adjust().draw();

        $('#tblSMDB #check_all_Db').change(function () {

            var checked = $(this).is(":checked");

            if (checked) {
                $('#tblSMDB tbody input[type="checkbox"]').prop('checked', this.checked);
            }
            else {
                $('#tblSMDB tbody input[type="checkbox"]').prop('checked', '');
            }

        });

    },

    reset_datatable_state: function () {
        localStorage.removeItem('DataTables_tblSMDB_' + window.location.pathname);
        localStorage.removeItem('DataTables_tblSMWeb_' + window.location.pathname);
        localStorage.removeItem('DataTables_tblSMServers_' + window.location.pathname);
    },
    CallUpdate: function () {
        var index = $('#hdnTabIndex').val();
        if (index == 0)
            MonitoringConfig.updateServerList();
        else if (index == 1)
            MonitoringConfig.updateWebList();
        else if (index == 2)
            MonitoringConfig.updateDBServerList();

    },
    updateServerList: function () {

        var selectedIds = [];
        var id = "";
        var oTable = $("#tblSMServers").dataTable();
        $("input:checked", oTable.fnGetNodes()).each(function () {
            if (id != "") {
                id = id + "," + $(this).val();

            } else {
                id = $(this).val();
            }
            selectedIds.push($(this).val());
        });

        MonitoringConfig.updateMonitorInfo(id, 'server');

    },
    updateDBServerList: function () {

        var selectedIds = [];
        var id = "";
        var oTable = $("#tblSMDB").dataTable();
        $("input:checked", oTable.fnGetNodes()).each(function () {
            if (id != "") {
                id = id + "," + $(this).val();

            } else {
                id = $(this).val();
            }
            selectedIds.push($(this).val());
        });

        MonitoringConfig.updateMonitorInfo(id, 'dbserver');
    },
    updateWebList: function () {
        var selectedIds = [];
        var id = "";
        var oTable = $("#tblSMWeb").dataTable();
        $("input:checked", oTable.fnGetNodes()).each(function () {
            if (id != "") {
                id = id + "," + $(this).val();

            } else {
                id = $(this).val();
            }
            selectedIds.push($(this).val());
        });

        MonitoringConfig.updateMonitorInfo(id, 'web');

    },


    updateMonitorInfo: function (IDs, Source) {

        var sUrl = $('#uUpdateMonitorList').val();
        $.ajax({
            url: sUrl,
            data: { Ids: IDs, source: Source },
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data != null) {
                    if (data.RecStatus == "Saved") {
                        dashboard.showMessage('success', 'Added to monitoring list successfully.', true);
                    }

                    else {
                        dashboard.showMessage('danger', 'Failed to add into monitoring list .', false);
                    }
                }
            },
            error: function (data, status, jqXHR) {
                dashboard.showMessage('danger', 'Save failed. Please try again...', false);
            }
        });
    }

}


