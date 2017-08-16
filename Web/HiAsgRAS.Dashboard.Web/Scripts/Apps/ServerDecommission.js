

ServerDecommission = {
    //Servers  

    SDClient: function (appServerId) {

        var sUrl = $("#uClientList").val();
        $.ajax({
            url: sUrl,
            type: 'GET',
            cache: false,
            async: false,
            data: { Id: appServerId },
            contentType: 'text/json',
            success: function (data) {
                if (data != null) {
                    ServerDecommission.LoadClientApps(data);                  
                }
            },
            error: function (data, status, jqXHR) {
                dashboard.showMessage('danger', 'Load failed. Please try again', false);
            }
        });
    },

    //WebApps    
    SDWebs: function (appServerId) {

        var sUrl = $("#uWebList").val();
        $.ajax({
            url: sUrl,
            type: 'GET',
            data: { Id: appServerId },
            cache: false,
            async: false,
            contentType: 'text/json',
            success: function (data) {
                if (data != null) {                    
                    ServerDecommission.LoadWeb(data);                  
                }
            },
            error: function (data, status, jqXHR) {
                dashboard.showMessage('danger', 'Load failed. Please try again', false);
            }
        });
    },

    //DBServers    
    SDDBServers: function (dbServerId) {

        var sUrl = $("#uDBList").val();
        $.ajax({
            url: sUrl,
            type: 'GET',
            data: { Id: dbServerId },
            cache: false,
            async: false,
            contentType: 'text/json',
            success: function (data) {

                if (data != null) {
                    ServerDecommission.LoadDBServers(data);                   
                }
            },
            error: function (data, status, jqXHR) {
                dashboard.showMessage('danger', 'Load failed. Please try again', false);
            }
        });
    },

    initOnClickEvents: function () {
        // Handle click on button    
        $("#btnInfoSave").on("click", function () {
            ServerDecommission.CallUpdate();
        });
        $("#btnInfoSave1").on("click", function () {
            ServerDecommission.CallUpdate();
        });
        $("#btnUpdate").on("click", function () {
            var serverId = $("#ddlOldServers").val();
            var statusTypeId = $("#ddlStatusType").val();
            ServerDecommission.updateServerStatus(serverId, statusTypeId);
        });

        $("#ddlOldServers").on("change", function () {
            var appServerId = $("#ddlOldServers").val();
            var dbServerId = $("#ddlOldServers").val();
            if (appServerId > 0) {                
                ServerDecommission.getAppsByServer(appServerId);
            }
            else {
                $('#showRecords').hide();
                $('#showStatus').hide();
                $('#showStatusUpdate').hide();
                $('#showMessage').hide();
                $('#showNewServer').show();
               
            }
        });
        
        $("#ddlNewServers").on("change", function () {            
            var appServerId = $("#ddlOldServers").val();
            if (appServerId == 0) {
                dashboard.showMessage('danger', 'Please select old server name');
            }
        });
        
    },

    showOrHideAppsList: function (recCount, sId) {
        $('#showMessage').hide();
        if (recCount == 0) {
            $('#showStatus').show();
            $('#showStatusUpdate').show();
            $('#showMessage').show("slow");
            $('#showNewServer').hide();
            $('#showRecords').hide();    
        }
        else {
            $('#showStatus').hide();
            $('#showStatusUpdate').hide();
            $('#showNewServer').show();
            $('#showRecords').show();            
        }
    },

    setServerStatus: function (id) {        
        var sUrl = $("#uGetStatus").val();
        $.ajax({
            cache: false,
            url: sUrl,
            data: { serverId: id },
            type: 'GET',
            async: false,
            contentType: "text/json",
            success: function (data) {
                if (data != null) {
                    $("#ddlStatusType").val(data.StatusTypeId);
                }
            }
        });
    },

    getAppsByServer: function (id) {
          var sUrl = $("#uGetAppsCount").val();
            $('#dvLoadModal').modal('show');
            $.ajax({
                cache: false,
                url: sUrl,
                data: { serverId: id },
                type: 'GET',
                async: false,
                contentType: "text/json",
                success: function (data) {    
                    if (data == 0) {
                        ServerDecommission.setServerStatus(id);
                    }
                    else {
                        ServerDecommission.SDClient(id);
                        ServerDecommission.SDWebs(id);
                        ServerDecommission.SDDBServers(id);
                    }
                    ServerDecommission.showOrHideAppsList(data, id);
                    $('#dvLoadModal').modal('hide');
                }
                ,
                error: function (data, status, jqXHR) {                   
                    $('#dvLoadModal').modal('hide');
                }                  
            });           
       
    },

   

    LoadClientApps: function (data) {

        var table = $('#tblSDClient').DataTable({

            "bPaginate": false,
            "bDestroy": true,
            responsive: true,

            //"bResponsive": "true",
            "aaData": data,
            "bStateSave": true,

            "aoColumns": [
                {
                    "sTitle": '<input type="checkbox" id="check_all_Client" name="check_all_Client" >&nbsp;All',
                    // "mData": "IsDeleted",
                    "bSortable": false,
                    "bAutoWidth": true,
                    //"sWidth": "5%",
                    "targets": 0,
                    "mRender": function (full, type, data) {
                        return '<input type="checkbox" value="' + data.Id + '">';
                    }
                }
                , {
                    "mData": "Application",
                    "bAutoWidth": true,
                    //"width": "25%"
                },
                {
                    "mData": "Version",
                    "bAutoWidth": true,
                    //"width": "10%"
                },
                {
                    "mData": "Server",
                    "bAutoWidth": true,
                    //"width": "10%"
                },
                {
                    "mData": "ApplicationLayer",
                    "bAutoWidth": true,
                    //"width": "10%"
                },
                {
                    "mData": "Layer5Location",
                    "bAutoWidth": true,
                    //"width": "10%"
                },
                {
                    "mData": "RemedyGroupName",
                    "bAutoWidth": true,
                    //"width": "10%"
                },
                {
                    "mData": "RADPOC",
                    "bAutoWidth": true,
                    //"width": "10%"
                },
                {
                    "mData": "BPInfo",
                    "bAutoWidth": true,
                    //"width": "10%"
                }


            ],
            'select': {
                'style': 'multi'
            },
            'order': [[1, 'asc']],
            "fnDrawCallback": function () {
                $("div.dataTables_wrapper div.dataTables_filter").css("text-align", "right");
            }
        });

        table.columns.adjust().draw();
        $('#check_all_Client').change(function () {
            var checked = $(this).is(":checked");

            if (checked) {
                $('#tblSDClient tbody input[type="checkbox"]').prop('checked', this.checked);
            }
            else {
                $('#tblSDClient tbody input[type="checkbox"]').prop('checked', '');
            }

        });
    },
    LoadWeb: function (data) {

        var table = $('#tblSDWeb').DataTable({

            "bPaginate": false,
            "bDestroy": true,
            responsive: true,
            //"bResponsive": "true",
            "aaData": data,
            "bStateSave": true,
            "aoColumns": [
                          {
                              "sTitle": '<input type="checkbox" id="check_all_Web" name="check_all_Web" >&nbsp;All',
                              // "mData": "IsDeleted",
                              "bSortable": false,
                              "bAutoWidth": true,
                              //"sWidth": "10%",
                              "targets": 0,
                              "mRender": function (full, type, data) {
                                  return '<input type="checkbox" value="' + data.Id + '">';
                              }
                          }
                          , {
                              "mData": "WebFolder",
                              "bAutoWidth": true
                              //"width": "25%"

                          }
                           , {
                               "mData": "Active",
                               "bAutoWidth": true,
                               //"width": "5%"                         
                           }
                            , {
                                "mData": "Status",
                                "bAutoWidth": true,
                                //"width": "10%"
                            }
                             , {
                                 "mData": "RemedyGroupName",
                                 "bAutoWidth": true,
                                 //"width": "15%"
                             }
                              , {
                                  "mData": "PrimayContact",
                                  "bAutoWidth": true,
                                  //"width": "10%"
                              }
                               , {
                                   "mData": "SecondaryContact",
                                   "bAutoWidth": true,
                                   //"width": "10%"
                               }
                                , {
                                    "mData": "BAOwnerPrimary",
                                    "bAutoWidth": true,
                                    //"width": "10%"
                                }
                                 , {
                                     "mData": "BAODeptPrimary",
                                     "bAutoWidth": true,
                                     //"width": "10%"
                                 }
                            ],
            'select': {
                'style': 'multi'
            },
            'order': [[1, 'asc']],
            "fnDrawCallback": function () {
                $("div.dataTables_wrapper div.dataTables_filter").css("text-align", "right");
            }
        });
        table.columns.adjust().draw();
        $('#tblSDWeb #check_all_Web').change(function () {

            var checked = $(this).is(":checked");

            if (checked) {
                $('#tblSDWeb tbody input[type="checkbox"]').prop('checked', this.checked);
            }
            else {
                $('#tblSDWeb tbody input[type="checkbox"]').prop('checked', '');
            }

        });
    },
    LoadDBServers: function (data) {

        var table = $('#tblSDDB').DataTable({

            "bPaginate": false,
            "bDestroy": true,
            responsive: true,
            //   "bResponsive": "true",
            "aaData": data,
            "bStateSave": true,
            "aoColumns": [
                          {
                              "sTitle": '<input type="checkbox" id="check_all_Db" name="check_all_Db" >&nbsp;All',
                              //"mData": "IsDeleted",
                              "bSortable": false,
                              "bAutoWidth": true,
                              //"sWidth": "10%",
                              "targets": 0,
                              "mRender": function (full, type, data) {
                                  return '<input type="checkbox" value="' + data.Id + '">';
                              }
                          }
                          , {
                              "mData": "Application",
                              "bAutoWidth": true,
                              //"width": "25%"
                          }

                        , {
                            "mData": "DbName",
                            "bAutoWidth": true,
                            //"width": "25%"
                        }
                        , {
                            "mData": "StatusText",
                            "bAutoWidth": true,
                            //"width": "10%"
                        }
                        , {
                            "mData": "DBServer",
                            "bAutoWidth": true,
                            //"width": "15%"
                        }
                        , {
                            "mData": "SupportStaff",
                            "bAutoWidth": true,
                            //"width": "15%"
                        }

                    ],
            'select': {
                'style': 'multi'
            },
            'order': [[1, 'asc']],
            "fnDrawCallback": function () {
                $("div.dataTables_wrapper div.dataTables_filter").css("text-align", "right");
            }

        });
        table.columns.adjust().draw();

        $('#tblSDDB #check_all_Db').change(function () {

            var checked = $(this).is(":checked");

            if (checked) {
                $('#tblSDDB tbody input[type="checkbox"]').prop('checked', this.checked);
            }
            else {
                $('#tblSDDB tbody input[type="checkbox"]').prop('checked', '');
            }

        });

    },

    reset_datatable_state: function () {
        localStorage.removeItem('DataTables_tblSDDB_' + window.location.pathname);
        localStorage.removeItem('DataTables_tblSDWeb_' + window.location.pathname);
        localStorage.removeItem('DataTables_tblSDClient_' + window.location.pathname);
    },
    CallUpdate: function () {
        var index = $('#hdnTabIndex').val();
        if (index == 0)
            ServerDecommission.updateClientAppsList();
        else if (index == 1)
            ServerDecommission.updateWebList();
        else if (index == 2)
            ServerDecommission.updateDBServerList();

    },
    updateClientAppsList: function () {

        var oldServerId = $("#ddlOldServers").val();
        var newServerId = $("#ddlNewServers").val();
        if (newServerId != oldServerId) {
            if (oldServerId != 0 && newServerId != 0) {
                var selectedIds = [];
                var id = "";
                var oTable = $("#tblSDClient").dataTable();
                $("input:checked", oTable.fnGetNodes()).each(function () {
                    if (id != "") {
                        id = id + "," + $(this).val();

                    } else {
                        id = $(this).val();
                    }
                    selectedIds.push($(this).val());
                });
                if (id != '') {
                    ServerDecommission.updateServerInfo(id, 'clientApps', newServerId, oldServerId);                   
                }
                else {
                    dashboard.showMessage('danger', 'Please select application to update');
                }
            }
            else {
                dashboard.showMessage('danger', 'Please select new server name');
            }
        }
        else {
            dashboard.showMessage('danger', 'Both server names are same. Please select different server names');
        }
    },
    updateDBServerList: function () {

        var oldServerId = $("#ddlOldServers").val();
        var newServerId = $("#ddlNewServers").val();
        if (newServerId != oldServerId) {
            if (oldServerId != 0 && newServerId != 0) {
                var selectedIds = [];
                var id = "";
                var oTable = $("#tblSDDB").dataTable();
                $("input:checked", oTable.fnGetNodes()).each(function () {
                    if (id != "") {
                        id = id + "," + $(this).val();

                    } else {
                        id = $(this).val();
                    }

                    selectedIds.push($(this).val());
                });
                if (id != '') {
                    ServerDecommission.updateServerInfo(id, 'dbserver', newServerId, oldServerId);                   
                }
                else {
                    dashboard.showMessage('danger', 'Please select application to update');
                }
            }
            else {
                dashboard.showMessage('danger', 'Please select new server name');
            }
        }
        else {
            dashboard.showMessage('danger', 'Both server names are same. Please select different server names');
        }

    },
    updateWebList: function () {

        var oldServerId = $("#ddlOldServers").val();
        var newServerId = $("#ddlNewServers").val();
        if (newServerId != oldServerId) {
            if (oldServerId != 0 && newServerId != 0) {
                var selectedIds = [];
                var id = "";
                var oTable = $("#tblSDWeb").dataTable();
                $("input:checked", oTable.fnGetNodes()).each(function () {
                    if (id != "") {
                        id = id + "," + $(this).val();

                    } else {
                        id = $(this).val();
                    }
                    selectedIds.push($(this).val());
                });
                if (id != '') {
                    ServerDecommission.updateServerInfo(id, 'web', newServerId, oldServerId);     
                }
                else {
                    dashboard.showMessage('danger', 'Please select application to update');
                }
            }
            else {
                dashboard.showMessage('danger', 'Please select new server name');
            }
        }
        else {
            dashboard.showMessage('danger', 'Both server names are same. Please select different server names');
        }
    },


    updateServerInfo: function (IDs, Source, NewServerId, OldServerId) {

        var sUrl = $('#uUpdateServerList').val();
        $.ajax({
            url: sUrl,
            data: { Ids: IDs, source: Source, oldServerId: OldServerId, newServerId: NewServerId },
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data != null) {
                    if (data.RecStatus == "Saved") {
                        //setTimeout(function () {
                        //    location.reload();
                        //}, 1000);
                        dashboard.showMessage('success', 'Server list updated successfully.', true);
                        if (Source == 'web') {
                            ServerDecommission.SDWebs(OldServerId);
                        }
                        else if (Source == 'clientApps')
                        {
                            ServerDecommission.SDClient(OldServerId);
                        }
                        else if (Source == 'dbserver') {
                            ServerDecommission.SDDBServers(OldServerId);
                        }
                    }

                    else {
                        dashboard.showMessage('danger', 'Failed to update the server.', false);
                    }
                }
            },
            error: function (data, status, jqXHR) {
                dashboard.showMessage('danger', 'Save failed. Please try again...', false);
            }
        });
    },


    updateServerStatus: function (ServerId, StatusTypeId) {

        var sUrl = $('#uUpdateServerStatus').val();
        $.ajax({
            url: sUrl,
            data: { serverId: ServerId, statusTypeId: StatusTypeId },
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data != null) {
                    if (data.RecStatus == "Updated") {
                        dashboard.showMessage('success', 'Server status updated successfully.', true);
                    }

                    else {
                        dashboard.showMessage('danger', 'Failed to update the server status.', false);
                    }
                }
            },
            error: function (data, status, jqXHR) {
                dashboard.showMessage('danger', 'Save failed. Please try again...', false);
            }
        });
    }

}


