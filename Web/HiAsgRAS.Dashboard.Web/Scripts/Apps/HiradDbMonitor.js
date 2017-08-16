hiradDbCRUD = {

    //Page Related

    searchResult: function () {

        var sUrl = $('#hdnDbListUrl').val();

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
                    hiradDbCRUD.loadDbDetails(data);
                    hiradDbCRUD.disableCtrl('disabled');
                    $('#dvLoadModal').modal('hide');
                }
            },
            error: function (data, status, jqXHR) {
                $('#dvLoadModal').modal('hide');
            }
        });
    }

, loadDbDetails: function (data) {
    var userType = $('#hdnUserType').data('value');
    $('#hiDbList').dataTable().fnDestroy();

    $('#hiDbList').dataTable({
        "oLanguage": {
            "sSearch": "Search all columns:"
        },
        /* For Default Search */
        "oSearch": { "sSearch": "AD RAS HI SERVER" },
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
                 //"mRender": function (o) { return '<a href="#" class="viewHiradDb" data-dbid=' + o.Id + '>' + 'View' + '</a> '; }
                 "mRender": function (o) {
                     if (userType == "A") {
                         return '<div style="text-align:center;"><table><tr><td> <a href="#" class="viewHiradDb" data-dbid=' + o.Id + '>' + '<i class="fa fa-bars" title="View"></i>' + '</a></td>'
                         + '<td>&nbsp;&nbsp;</td><td><a href="#" class="viewHiradDb" data-dbid=' + o.Id + '>' + '<i class="fa fa-edit" title="Edit"></i>' + '</a></td>'
                         + '<td>&nbsp;&nbsp;</td><td><a href="#" class="viewHiradDb" data-dbid=' + o.Id + '>' + '<i class="fa fa-trash" title="Delete"></i>' + '</a></td></tr></table> </div>';
                     }
                     else {
                         return '<div style="text-align:center;"> <a href="#" class="viewHiradDb" data-dbid=' + o.Id + '>' + '<i class="fa fa-bars" title="View"></i>' + '</a>';
                     }
                 }
             },
            //{
            //    "sWidth": "3%",
            //    "mDataProp": null,
            //    "bSortable": false,
            //    "mRender": function (o) { return '<a href="#" class="viewHiradDb" data-dbid=' + o.Id + '>' + 'Edit' + '</a> '; }
            //},
            //{
            //    "sWidth": "4%",
            //    "mDataProp": null,
            //    "bSortable": false,
            //    "mRender": function (o) { return '<a href="#" class="viewHiradDb" data-dbid=' + o.Id + '>' + 'Delete' + '</a>'; }
            //},
            {
                "mDataProp": "Application",               
                "bAutoWidth": true,
                "mRender": function (data, type, full) {
                    return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;

                }
            }, {
                "mDataProp": "DbName",              
                "bAutoWidth": true,
                "mRender": function (data, type, full) {
                    return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;
                }
            },
            {
                "mDataProp": "StatusText",              
                "bAutoWidth": true,
                "mRender": function (data, type, full) {
                    return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;
                }
            },
            {
                "mDataProp": "DBServer",              
                "bAutoWidth": true,
                "mRender": function (data, type, full) {
                    return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;

                }
            },

            {
                "mDataProp": "SupportStaff",               
                "bAutoWidth": true,
                "orderable": true,
                "searchable": true
            },

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
            ],

        "fnDrawCallback": function () {
            hiradDbCRUD.initOnClickEvents();
        }        
      
    });

    //if (userType == "G") {
    //    var table = $('#hiDbList').DataTable();
    //    // Hide edit & delete column to General User
    //    table.column(1).visible(false);
    //    table.column(2).visible(false);
    //}

}
    , reset_datatable_state: function () {
        localStorage.removeItem('DataTables_hiDbList_' + window.location.pathname);
    }
    , initOnClickEvents: function () {
        //on Each Row click
        $(".viewHiradDb").unbind();
        $(".viewHiradDb").on('click', (function () {
            var clickedIcon = $(this).find('i')[0];
            if (clickedIcon != null) {
                if (clickedIcon.title == "Delete") {

                    var userType = $('#hdnUserType').data('value');
                    if (userType == "A") {
                        hiradDbCRUD.confirm_deleteDb($(this).data("dbid"));
                    }
                }
                else {
                    hiradDbCRUD.viewDbInformation($(this).data("dbid"), clickedIcon.title);
                }
            }
            }));

        $("#btnDbAdd").unbind();
        $("#btnDbAdd").on('click', function () {
            hiradDbCRUD.viewDbInformation(0, "Add");
        });


    }

    , viewDbInformation: function (dbId, mode) {

        //on Each Row click
        var record = {
            dbId: dbId,
            actionMode: mode
        };
        $.ajax({
            //Calling Partial View
            url: $("#uviewDbInformation").val(),
            type: 'POST',
            data: record,
            async: true,
            success: function (data) {

                if (data != null) {
                    $('#dvMdlDlgContent').html(data);
                    $("#dvModal").modal('show');
                    hiradDbCRUD.showHideButtons();
                }
            },
            error: function (data, status, jqXHR) {
                dashboard.showMessage('Danger', 'Error in view DB Information', false);
            }
        });


    }

    , showHideButtons: function () {

        if ($("#hdnDbAction").val() == "View") {
            $("#btnDbUpdate").hide();
            $("#btnDbServerSearch").on('click', function () {
                event.preventDefault();
            });
        }
        else if ($("#hdnDbAction").val() == "Edit") {

            $("#btnDbUpdate").on("click", function () {
                if (hiradDbCRUD.validatedDb()) {
                    hiradDbCRUD.updateDb();
                }
            });

            $("#btnDbServerSearch").on('click', function () {
                event.preventDefault();
                hiradDbCRUD.showServerPopup();
            });

        }
        else if ($("#hdnDbAction").val() == "Add") {

            $("#btnDbUpdate").text("Add Database");
            $("#btnDbUpdate").on("click", function () {
                if (hiradDbCRUD.validatedDb()) {
                    hiradDbCRUD.updateDb();
                }

            });
            $("#btnDbServerSearch").on('click', function () {
                event.preventDefault();
                hiradDbCRUD.showServerPopup();
            });

        }
        else {
            $("#btnDbUpdate").hide();

        }

        if ($("#hdnDbAction").val() == "View") {
            $("#dvModal").find('input:text').attr('disabled', 'disabled');
            $("#dvModal").find('input:checkbox').attr('disabled', 'disabled');
            $("#dvModal").find('input:radio').attr('disabled', 'disabled');
            $("#dvModal").find('textarea').attr('disabled', 'disabled');
            $("#dvModal").find('select').attr('disabled', 'disabled');
        }
    }

    , validatedDb: function () {

        if ($.trim($('#txtApp').val()) == '') {
            dashboard.showMessage('warning', 'Please enter Application', false);
            return false;
        }
        if ($.trim($('#txtDbServer').val()) == '') {
            dashboard.showMessage('warning', 'Please select DB Server', false);
            return false;
        }
        if ($.trim($('#txtDbName').val()) == '') {
            dashboard.showMessage('warning', 'Please enter DB Name', false);
            return false;
        }
        return true;
    }
    , updateDb: function () {
        var dbId = $('#hdnSelectedRowId').val();
        var application = $.trim($('#txtApp').val());
        var dbName = $.trim($('#txtDbName').val());
        var dB_Server = $.trim($("#txtDbServer").val());
        var dbServerId = $('#txtDbServerId').val();
        var statusTypeId = $("#ddlStatusType option:selected").val();
        var statusChangedOn = $.trim($("#txtStatusTypeChangedOn").val());
        var prvStatusTypeId = $.trim($("#hdnPrvStatusTypeId").val());
        var createdBy = $("#hdnCreatedBy").val();
        var createdDate = $("#hdnCreatedDate").val();
        var isMonitor = $("#chkIsMonitor").is(':checked') ? true : false;

        var rec = {
            Id: dbId,
            Application: application,
            DBServer: dB_Server,
            DbName: dbName,
            DbServerId: dbServerId,
            StatusTypeChangedOn: statusChangedOn,
            StatusTypeId: statusTypeId,
            PreviousStatusTypeId: prvStatusTypeId,
            CreatedBy: createdBy,
            CreatedDate: createdDate,
            IsMonitor: isMonitor
        };

        var sUrl = $('#hdnDbSaveUrl').val();
        $.ajax({
            url: sUrl,
            data: rec,
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data != null) {
                    if (data.RecStatus == "Saved") {
                        hiradDbCRUD.refreshDbCtrl();

                        //Refresh DataTable
                        hiradDbCRUD.searchResult();

                        if (dbId == 0 || dbId == '') {
                            dashboard.showMessage('success', 'Database added successfully', true);

                        } else {
                            dashboard.showMessage('success', 'Database updated successfully', true);
                        }

                    }
                    else if (data != null && data.RecStatus == "Duplicate") {
                        var sDupMsg = 'Database already exists. Please enter any other valid Database.';
                        dashboard.showMessage('warning', sDupMsg, false);
                    }
                    else {
                        dashboard.showMessage('danger', 'Validation failed.', false);
                    }
                }
            },
            error: function (data, status, jqXHR) {
                hiradDbCRUD.refreshDbtrl();
                dashboard.showMessage('danger', 'Save failed. Please try again...', false);
            }
        });
    }

    , refreshDbCtrl: function () {
        hiradDbCRUD.clearData();
        hiradDbCRUD.disableCtrl('disabled');
    }



    , clearData: function () {
        $("#dvModal").find('input:text').val('');
        $("#dvModal").find('textarea').val('');
        $("input:checked").removeAttr('checked');
        $('#hdnSelectedRowId').val('');
    }

    , disableCtrl: function (disable) {
        var userType = $('#hdnUserType').data('value');
        if (disable == 'disabled') {
            if (userType == "G") {
                $("#btnDbAdd").attr('disabled', 'disabled');
            }
        }
    }


    , confirm_deleteDb: function (dbId) {
        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this Database!",
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
                hiradDbCRUD.deleteDb(dbId);
            }
        });
    }

    , deleteDb: function (dbId) {

        var sUrl = $('#hdnDbDeleteUrl').val();
        var rec = {
            dbId: dbId
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
                        hiradDbCRUD.refreshDbCtrl();
                        hiradDbCRUD.searchResult();
                        dashboard.showMessage('success', 'Database deleted successfully', true);
                    }
                }
            },
            error: function (data, status, jqXHR) {
                console.log(jqXHR);
                dashboard.showMessage('danger', 'Failed to delete the database details. Please try again.', false);
            }
        });
    }


    , showServerPopup: function () {
        $.ajax({
            //Calling Partial View
            url: $("#uSearchServerUrl").val(),
            type: 'GET',
            async: true,
            success: function (data) {
                if (data != null) {
                    $('#dvSubMdlDlgContent').html(data);
                    $("#dvSubModal").modal('show');
                }
            },
            error: function (data, status, jqXHR) {

                dashboard.showMessage('Danger', 'Error in Server Search', false);
            }
        });

    }

    , serverSearch: function () {

        $.ajax({
            url: $('#uSearchServerList').val(),
            type: 'GET',
            cache: false,
            contentType: 'text/json',
            success: function (data) {
                if (data != null) {
                    hiradDbCRUD.loadServerSearchResult(data);
                }
            },
            error: function (data, status, jqXHR) {
                dashboard.showMessage('Danger', 'Search failed. Please try again', false);
            }
        });
    }



     , loadServerSearchResult: function (data) {
         $('#dthiradDbServer').dataTable().fnDestroy();

         $('#dthiradDbServer').dataTable({
             "oLanguage": {
                 "sSearch": "Search all columns:"
             },
             "lengthMenu": [[-1, 10, 25, 50, 100], ["All", 10, 25, 50, 100]],
             "bDestroy": true,
             "bResponsive": "true",
             "aaData": data,
             "pageLength": 25,
             "aoColumns": [
                   {
                       "mDataProp": "Id",
                       "sWidth": "10%",
                       "orderable": false,
                       "searchable": false,
                       "visible": false
                   },
                      {
                          "mDataProp": "SystemName",
                          "sWidth": "20%",
                          "orderable": false,
                          "searchable": false,
                          "render": function (data, type, row, meta) { // render event defines the markup of the cell text 
                              var a = '<a onclick="hiradDbCRUD.loadServerData(' + "'" + row.SystemName + "'," + row.Id + ')" ><i class="fa fa-edit"></i>  ' + row.SystemName + '</a>'; // row object contains the row data                        
                              return a;
                          },
                          "orderable": true,
                          "searchable": true
                      },
                     {
                         "mDataProp": "IPAddress",
                         "sWidth": "20%",
                         "orderable": true,
                         "searchable": true,
                     },
                    {
                        "mDataProp": "StatusText",
                        "sWidth": "20%",
                        "orderable": true,
                        "searchable": true
                    },
                    {
                        "mDataProp": "SupportStaff",
                        "sWidth": "20%",
                        "orderable": true,
                        "searchable": true
                    }
             ],
             "scrollY": '40vh',
             "scrollCollapse": true,

             "fnInitComplete": function () {

                 $("#dtServerDetail").css("width", "100%");
                 $(".dataTables_scrollHead").css("width", "100%");
                 $(".dataTables_scrollHeadInner").css("width", "100%");
                 $(".dataTables_scrollHeadInner table").css("width", "100%");

                 $("div.dataTables_wrapper div.dataTables_filter label").css("width", "100%");
             }
             , "fnDrawCallback": function () {
                 $("#dtServerDetail").css("width", "100%");
                 $(".dataTables_scrollHead").css("width", "100%");
                 $(".dataTables_scrollHeadInner").css("width", "100%");
                 $(".dataTables_scrollHeadInner table").css("width", "100%");
                 $("div.dataTables_wrapper div.dataTables_filter label").css("width", "100%");
             }
         });


         //For Default Selection   
         $('#dthiradDbServer').DataTable().search($("#txtDbServer").val()).draw();

     }
     , loadServerData: function (servername, sid) {
         $('#txtDbServer').val(servername);
         $('#txtDbServerId').val(sid);
         $("#dvSubModal").modal('hide');
     }


    //End of Namespace
}



