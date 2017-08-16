hiradWebCRUD = {

    //Page Related

    searchResult: function (defaultLoad) {

        var sUrl = $('#uSearchWebList').val();

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
                    hiradWebCRUD.loadWebAppDetails(data);
                    hiradWebCRUD.disableCtrl('disabled');
                    $('#dvLoadModal').modal('hide');
                }
            },
            error: function (data, status, jqXHR) {
                $('#dvLoadModal').modal('hide');
            }
        });
    }

, loadWebAppDetails: function (data) {
    var userType = $('#hdnUserType').data('value');
    $('#hiWebApps').dataTable().fnDestroy();

    $('#hiWebApps').dataTable({
        "oLanguage": {
            "sSearch": "Search all columns:"
        },
        /* For Default Search */
        //"oSearch": { "sSearch": "AD RAS HI WEB" },
        "lengthMenu": [[-1, 10, 25, 50, 100], ["All", 10, 25, 50, 100]],
        "bDestroy": true,
        "bResponsive": "true",
        "aaData": data,
        //"pageLength": "All",
        "order": [[1, "asc"]],
        "bStateSave": true,
        "aoColumns": [
             {
                 "sWidth": "3%",
                 "mDataProp": null,
                 "bSortable": false,
                 //"mRender": function (o) { return '<a href="#" class="viewHiradWeb" data-appid=' + o.Id + '>' + 'View' + '</a> '; }
                 "mRender": function (o) {
                     if (userType == "A") {
                         return '<div style="text-align:center;"><table><tr><td> <a href="#" class="viewHiradWeb" data-appid=' + o.Id + '>' + '<i class="fa fa-bars" title="View"></i>' + '</a></td>'
                         + '<td>&nbsp;&nbsp;</td><td><a href="#" class="viewHiradWeb" data-appid=' + o.Id + '>' + '<i class="fa fa-edit" title="Edit"></i>' + '</a></td>'
                         + '<td>&nbsp;&nbsp;</td><td><a href="#" class="viewHiradWeb" data-appid=' + o.Id + '>' + '<i class="fa fa-trash" title="Delete"></i>' + '</a></td></tr></table> </div>';
                     }
                     else {
                         return '<div style="text-align:center;"> <a href="#" class="viewHiradWeb" data-appid=' + o.Id + '>' + '<i class="fa fa-bars" title="View"></i>' + '</a>';
                     }
                 }
             },
            //{
            //    "sWidth": "3%",
            //    "mDataProp": null,
            //    "bSortable": false,
            //    "mRender": function (o) { return '<a href="#" class="viewHiradWeb" data-appid=' + o.Id + '>' + 'Edit' + '</a> '; }
            //},
            //{
            //    "sWidth": "4%",
            //    "mDataProp": null,
            //    "bSortable": false,
            //    "mRender": function (o) { return '<a href="#" class="viewHiradWeb" data-appid=' + o.Id + '>' + 'Delete' + '</a>'; }
            //},
            {
                "mDataProp": "WebFolder",
                //"sWidth": "20%",
                "bAutoWidth": true,
                "mRender": function (data, type, full) {
                    //return type == 'display' && data.length > 20 ? '<span title=\"' + data + '\">' + data.substr(0, 20) + '...</span>' : data;
                    return '<a href="' + full.WebSite + '" target="_blank" >' + data + '</a>';
                }
            },
            {
                "mDataProp": "WebSite",
                //"sWidth": "10%",
                "bAutoWidth": true,
                visible: false
                //"mRender": function (data, type, full) {
                //    return type == 'display' && data.length > 20 ? '<span title=\"' + data + '\">' + data.substr(0, 20) + '...</span>' : data;

                //}
            },
            {
                "mDataProp": "WebStat",
                //"sWidth": "8%",
                "bAutoWidth": true,
                "mRender": function (data, type, full) {
                    return '<a href="' + full.WebStat + '" target="_blank" >' + data + '</a>';
                }
            },
            {
                "mDataProp": "Active",
                //"sWidth": "5%",
                "mRender": function (data, type, full) {
                    return data.toLowerCase() == 'yes' || data.toLowerCase() == 'y' ? 'Yes' : 'No';

                }
            },
            {
                "mDataProp": "Status",
                //"sWidth": "5%",
                "bAutoWidth": true,
                "mRender": function (data, type, full) {
                    return type == 'display' && data.length > 20 ? '<span title=\"' + data + '\">' + data.substr(0, 20) + '...</span>' : data;

                }
            },
            {
                "mDataProp": "RemedyGroupName",
                //"sWidth": "8%",
                "bAutoWidth": true,
                "mRender": function (data, type, full) {
                    return type == 'display' && data.length > 30 ? '<span title=\"' + data + '\">' + data.substr(0, 20) + '...</span>' : data;

                }
            },
            {
                "mDataProp": "PrimayContact",
                //"sWidth": "8%",
                "bAutoWidth": true,
                "mRender": function (data, type, full) {
                    return type == 'display' && data.length > 30 ? '<span title=\"' + data + '\">' + data.substr(0, 20) + '...</span>' : data;

                }
            },
            {
                "mDataProp": "SecondaryContact",
                //"sWidth": "8%",
                "bAutoWidth": true,
                visible: false,
                "mRender": function (data, type, full) {
                    return type == 'display' && data.length > 40 ? '<span title=\"' + data + '\">' + data.substr(0, 20) + '...</span>' : data;

                }
            },
            {
                "mDataProp": "BAOwnerPrimary",
                //"sWidth": "8%",
                "bAutoWidth": true,
                "mRender": function (data, type, full) {
                    return type == 'display' && data.length > 30 ? '<span title=\"' + data + '\">' + data.substr(0, 20) + '...</span>' : data;

                }
            },
            {
                "mDataProp": "BAODeptPrimary",
                //"sWidth": "10%",
                "bAutoWidth": true,
                "mRender": function (data, type, full) {
                    return type == 'display' && data.length > 30 ? '<span title=\"' + data + '\">' + data.substr(0, 20) + '...</span>' : data;

                }

            },

            { "mDataProp": "ABCID" },
            { "mDataProp": "APPServerText" },
            { "mDataProp": "DBServerText" },
            { "mDataProp": "ProdSupportAgreement" },
            //{ "mDataProp": "Description" }
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
        columnDefs: [{
            targets: [-1, -2, -3],
            visible: false
        }],
        "fnDrawCallback": function () {
            hiradWebCRUD.initOnClickEvents();
        }

    });

    //if (userType == "G") {
    //    var table = $('#hiWebApps').DataTable();
    //    // Hide edit & delete column to General User
    //    table.column(1).visible(false);
    //    table.column(2).visible(false);
    //}

}

     , reset_datatable_state: function () {
         localStorage.removeItem('DataTables_hiWebApps_' + window.location.pathname);
     }

    , initOnClickEvents: function () {
        //on Each Row click
        $(".viewHiradWeb").unbind();
        $(".viewHiradWeb").on('click', (function () {
            var clickedIcon = $(this).find('i')[0];
            if (clickedIcon != null) {
                if (clickedIcon.title == "Delete") {

                    var userType = $('#hdnUserType').data('value');
                    if (userType == "A") {
                        hiradWebCRUD.confirm_deleteWebApp($(this).data("appid"));
                    }
                }
                else {
                    hiradWebCRUD.viewWebAppInformation($(this).data("appid"), clickedIcon.title)
                }
            }
        }));

        $("#btnWebAppAdd").unbind();
        $("#btnWebAppAdd").on('click', function () {
            hiradWebCRUD.viewWebAppInformation(0, "Add");
        });


    }

    , viewWebAppInformation: function (webappId, mode) {

        //on Each Row click
        var record = {
            webAppId: webappId,
            actionMode: mode
        };
        $.ajax({
            //Calling Partial View
            url: $("#uViewWebAppInformation").val(),
            type: 'POST',
            data: record,
            async: true,
            success: function (data) {

                if (data != null) {
                    $('#dvMdlDlgContent').html(data);
                    $("#dvModal").modal('show');
                    hiradWebCRUD.showHideButtons();
                }
            },
            error: function (data, status, jqXHR) {
                dashboard.showMessage('Danger', 'Error in view web app Information', false);
            }
        });


    }

    , showHideButtons: function () {
        $('input[name="txtArd"]').daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            autoUpdateInput: false,
            locale: {
                format: 'MM-DD-YYYY'
            }
        }, function (chosen_date) {
            $('#txtArd').val(chosen_date.format('MM-DD-YYYY'));
        });

        $("#txtWebPSA").keyup(function () {
            $("#lnkWebPSA").attr('href', $(this).val());
            $("#lnkWebPSA").html($(this).val());
        });

        if ($("#hdnWebAppAction").val() == "View") {
            $("#btnWebAppUpdate").hide();
            $("#btnServerSearch, #btnBAOSearch, #btnDBServerSearch").on('click', function () {
                event.preventDefault();
            });

            $("#txtWebPSA").hide();
            $("#lnkWebPSA").show();
        }
        else if ($("#hdnWebAppAction").val() == "Edit") {

            $("#btnWebAppUpdate").on("click", function () {
                if (hiradWebCRUD.validatedWebApp()) {
                    hiradWebCRUD.updateWebApp();
                }
            });

            $("#btnServerSearch").on('click', function () {
                $("#hdnSeverSearchFor").val('APP');
                event.preventDefault();
                hiradWebCRUD.showServerPopup();
            });
            $("#btnDBServerSearch").on('click', function () {
                $("#hdnSeverSearchFor").val('DB');
                event.preventDefault();
                hiradWebCRUD.showServerPopup();
            });
            $("#btnBAOSearch").on('click', function () {
                event.preventDefault();
                hiradWebCRUD.showBAOPopup();
            });
            //$("input[name='txtWebPSA']").click(function () {
            //    $(this).parent(".form-group")
            //    .find('input[type=file]')
            //    .trigger('click');

            //});
            $("#txtWebPSA").show();
            $("#lnkWebPSA").show();
        }
        else if ($("#hdnWebAppAction").val() == "Add") {
            var webSiteType = $("#hdnwebSiteType").val();
            if (webSiteType.toLowerCase() == 'web') {
                $("#btnWebAppUpdate").text("Add Web App");
            }
            else {
                $("#btnWebAppUpdate").text("Add Sharepoint App");
            }
            $("#btnWebAppUpdate").on("click", function () {
                if (hiradWebCRUD.validatedWebApp()) {
                    hiradWebCRUD.updateWebApp();
                }

            });
            $("#btnServerSearch").on('click', function () {
                event.preventDefault();
                $("#hdnSeverSearchFor").val('APP');
                hiradWebCRUD.showServerPopup();
            });
            $("#btnDBServerSearch").on('click', function () {
                $("#hdnSeverSearchFor").val('DB');
                event.preventDefault();
                hiradWebCRUD.showServerPopup();
            });
            $("#btnBAOSearch").on('click', function () {
                event.preventDefault();
                hiradWebCRUD.showBAOPopup();
            });
            //$("input[name='txtWebPSA']").click(function () {
            //    $(this).parent(".form-group")
            //    .find('input[type=file]')
            //    .trigger('click');

            //});
            $("#txtWebPSA").show();
            $("#lnkWebPSA").hide();
        }
        else {
            $("#btnWebAppUpdate").hide();

        }

        $("input[name='rdAIsRenewal']").change(function () {
            var isRenewal = $("input[name='rdAIsRenewal']:checked").val();
            if (isRenewal == "true")
                $('#txtArd').removeAttr('disabled');
            else {
                $('#txtArd').val('');
                $('#txtArd').attr('disabled', 'disabled');
            }
        });


        $("input[type=file]").change(function () {
            $(this).parent(".form-group")
            .find("input[name='txtWebPSA']")
            .val($(this).val());

        });

        if ($("#hdnWebAppAction").val() == "View") {
            $("#dvModal").find('input:text').attr('disabled', 'disabled');
            $("#dvModal").find('input:checkbox').attr('disabled', 'disabled');
            $("#dvModal").find('input:radio').attr('disabled', 'disabled');
            $("#dvModal").find('textarea').attr('disabled', 'disabled');
            $("#dvModal").find('select').attr('disabled', 'disabled');
        }

    }

    , validatedWebApp: function () {

        if ($.trim($('#txtWebWF').val()) == '') {
            dashboard.showMessage('warning', 'Please enter Web Folder', false);
            return false;
        }
        return true;
    }
    , updateWebApp: function () {
        var appId = $('#hdnSelectedRowId').val();
        var webFolder = $.trim($('#txtWebWF').val());
        var webSite = $.trim($('#txtWebWS').val());
        var webStat = $.trim($('#txtWebWST').val());
        //   var active = $('#txtWebActive').val();
        var app_Server = $.trim($("#txtWebAppSvr").val());//$('#optWebDBServer option:selected').text();
        var dB_Server = $.trim($("#txtWebDbSvr").val()); //$('#optWebAppServer option:selected').text();
        var status = $.trim($('#txtWebStatus').val());
        var remedyGroupName = $.trim($('#txtWebRgn').val());
        var primayContact = $.trim($('#txtWebPS').val());
        var secondaryContact = $.trim($('#txtWebSS').val());
        var bPContact = $.trim($('#txtWebBPC').val());
        var description = $.trim($('#txtWebComments').val());
        var prodSupportAgreement = $.trim($('#txtWebPSA').val());
        var abcID = $.trim($('#txtWebABC').val());
        var baoId = $.trim($('#txtWebBPCId').val());
        var dbName = $.trim($('#txtDBName').val());
        var appServerId = $('#txtServerId').val();
        var dbServerId = $('#txtDBServerId').val();
        var hiradNew = $('#hdnHiradNew').val();
        var statusTypeId = $("#ddlStatusType option:selected").val();
        var statusChangedOn = $.trim($("#txtStatusTypeChangedOn").val());
        var prvStatusTypeId = $.trim($("#hdnPrvStatusTypeId").val());
        var createdBy = $("#hdnCreatedBy").val();
        var createdDate = $("#hdnCreatedDate").val();
        var webSiteType = $("#hdnwebSiteType").val();
        var active = $("input[name='rdActive']:checked").val();
        var applicationRenewalDate = $('#txtArd').val();
        var isRenewal = $("input[name='rdAIsRenewal']:checked").val();
        var isMonitor = $("#chkIsMonitor").is(':checked') ? true : false;

        var rec = {
            Id: appId,
            ABCID: abcID,
            WebFolder: webFolder,
            WebSite: webSite,
            WebStat: webStat,
            Active: active,
            Status: status,
            AppServer: app_Server,
            DBServer: dB_Server,
            RemedyGroupName: remedyGroupName,
            PrimayContact: primayContact,
            SecondaryContact: secondaryContact,
            BPContact: bPContact,
            Description: description,
            ProdSupportAgreement: prodSupportAgreement,
            BAOId: baoId,
            DBName: dbName,
            AppServerId: appServerId,
            DbServerId: dbServerId,
            StatusTypeChangedOn: statusChangedOn,
            StatusTypeId: statusTypeId,
            PreviousStatusTypeId: prvStatusTypeId,
            IsRenewal: isRenewal,
            ApplicationRenewalDate: applicationRenewalDate,
            CreatedBy: createdBy,
            CreatedDate: createdDate,
            WebSiteType: webSiteType,
            IsMonitor: isMonitor
        };

        var sUrl = $('#hdnWebAppSaveUrl').val();
        $.ajax({
            url: sUrl,
            data: rec,
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data != null) {
                    if (data.RecStatus == "Saved") {
                        hiradWebCRUD.refreshWepAppCtrl();

                        //Refresh DataTable
                        hiradWebCRUD.searchResult('Y');

                        if (appId == 0 || appId == '') {
                            dashboard.showMessage('success', 'Application added successfully', true);

                        } else {
                            dashboard.showMessage('success', 'Application updated successfully', true);
                        }

                    }
                    else if (data != null && data.RecStatus == "Duplicate") {
                        var sDupMsg = 'Web Folder already exists. Please enter any other Web Folder.';
                        dashboard.showMessage('warning', sDupMsg, false);
                    }
                    else {
                        dashboard.showMessage('danger', 'Validation failed.', false);
                    }
                }
            },
            error: function (data, status, jqXHR) {
                hiradWebCRUD.refreshWepAppCtrl();
                dashboard.showMessage('danger', 'Save failed. Please try again...', false);
            }
        });
    }

    , refreshWepAppCtrl: function () {
        // hiradWebCRUD.clearData();
        hiradWebCRUD.disableCtrl('disabled');
        // hiradWebCRUD.clearWebSearchCtrl();
    }

    //, clearWebSearchCtrl: function () {
    //    $("#txtSearchWebFolder").val('');
    //    $("#txtSearchActive").val('');
    //    $("#txtSearchStatus").val('');
    //    $("#txtSearchRgn").val('');
    //    $("#txtSearchWebSite").val('');
    //    $("#txtSearchWebStat").val('');
    //    $("#txtSearchWebABC").val('');
    //    $("#txtSearchWebServer").val('');
    //}

    //, clearData: function () {
    //    $("#modalpopup").find('input:text').val('');
    //    $("#modalpopup").find('textarea').val('');
    //    $("input:checked").removeAttr('checked');
    //    $('#hdnSelectedRowId').val('');
    //}

    , disableCtrl: function (disable) {
        var userType = $('#hdnUserType').data('value');
        if (disable == 'disabled') {
            if (userType == "G") {
                $("#btnWebAppAdd").attr('disabled', 'disabled');
            }
        }

    }


    , confirm_deleteWebApp: function (aId) {
        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this application!",
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
                hiradWebCRUD.deleteWebApp(aId);
            }
        });
    }

    , deleteWebApp: function (aId) {

        var sUrl = $('#hdnWebAppDeleteUrl').val();
        var rec = {
            appId: aId
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
                        hiradWebCRUD.refreshWepAppCtrl();
                        hiradWebCRUD.searchResult('Y');
                        dashboard.showMessage('success', 'Web application deleted successfully', true);
                    }
                }
            },
            error: function (data, status, jqXHR) {
                console.log(jqXHR);
                dashboard.showMessage('danger', 'Failed to delete the application details. Please try again.', false);
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
                    hiradWebCRUD.serverSearch('Y');
                    $('#dvSubMdlDlgContent').html(data);
                    $("#dvSubModal").modal('show');
                }
            },
            error: function (data, status, jqXHR) {

                dashboard.showMessage('Danger', 'Error in Server Search', false);
            }
        });

    }

    , serverSearch: function (defaultLoad) {

        $.ajax({
            url: $('#uSearchServerList').val(),
            type: 'GET',
            cache: false,
            contentType: 'text/json',
            success: function (data) {
                if (data != null) {
                    hiradWebCRUD.loadServerSearchResult(data);
                }
            },
            error: function (data, status, jqXHR) {
                dashboard.showMessage('Danger', 'Search failed. Please try again', false);
            }
        });
    }
    
     , loadServerSearchResult: function (data) {
         $('#dtServerDetail').dataTable().fnDestroy();

         $('#dtServerDetail').dataTable({
             "oLanguage": {
                 "sSearch": "Search all columns:"
             },
             "lengthMenu": [[-1, 10, 25, 50, 100], ["All", 10, 25, 50, 100]],
             "bDestroy": true,
             "bResponsive": "true",
             "aaData": data,
             "pageLength": -1,
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
                              var a = '<a onclick="hiradWebCRUD.loadServerData(' + "'" + row.SystemName + "'," + row.Id + ')" ><i class="fa fa-edit"></i>  ' + row.SystemName + '</a>'; // row object contains the row data                        
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
         var searchfor = $("#hdnSeverSearchFor").val();
         if (searchfor == 'DB') {
             $('#dtServerDetail').DataTable().search($("#txtWebDbSvr").val()).draw();
         }
         else if (searchfor == 'APP') {
             $('#dtServerDetail').DataTable().search($("#txtWebAppSvr").val()).draw();
         }

     }

     , loadServerData: function (servername, sid) {
         var searchfor = $("#hdnSeverSearchFor").val();

         if (searchfor == 'DB') {
             $('#txtWebDbSvr').val(servername);
             $('#txtDBServerId').val(sid);
         }
         else if (searchfor == 'APP') {
             $('#txtWebAppSvr').val(servername);
             $('#txtServerId').val(sid);
         }
         $("#dvSubModal").modal('hide');
     }

     , showBAOPopup: function () {

         $.ajax({
             //Calling Partial View
             url: $("#uSearchBAOUrl").val(),
             type: 'GET',
             async: true,
             success: function (data) {
                 if (data != null) {
                     hiradWebCRUD.BAOSearch('Y');
                     $('#dvSubMdlDlgContent').html(data);
                     $("#dvSubModal").modal('show');
                 }
             },
             error: function (data, status, jqXHR) {

                 dashboard.showMessage('danger', 'Error in BAO Search', false);
             }
         });

     }

    , BAOSearch: function (defaultLoad) {

        $.ajax({
            url: $('#uSearchBAOList').val(),
            type: 'GET',
            cache: false,
            contentType: 'text/json',
            success: function (data) {
                if (data != null) {
                    hiradWebCRUD.loadBAOSearchResult(data);
                }
            },
            error: function (data, status, jqXHR) {
                dashboard.showMessage('Danger', 'Search failed. Please try again', false);
            }
        });
    }
    , loadBAOSearchResult: function (data) {
        $('#dtBAODetail').dataTable().fnDestroy();

        $('#dtBAODetail').dataTable({
            "oLanguage": {
                "sSearch": "Search all columns:"
            },
            "lengthMenu": [[-1, 10, 25, 50, 100], ["All", 10, 25, 50, 100]],
            "bDestroy": true,
            "bResponsive": "true",
            "aaData": data,
            "pageLength": -1,
            "aoColumns": [
                     {
                         "mDataProp": "Id",
                         "sWidth": "10%",
                         "orderable": false,
                         "searchable": false,
                         "visible": false
                     },
                    {
                        "mDataProp": "BAOwnerPrimary",
                        "orderable": true,
                        "searchable": true,
                        "render": function (data, type, row, meta) { // render event defines the markup of the cell text 
                            var a = '<a onclick="hiradWebCRUD.loadBAODetail(' + "'" + row.BAOwnerPrimary + "', '" + +row.BAODeptPrimary + "'," + row.Id + ')" ><i class="fa fa-edit"></i>  ' + row.BAOwnerPrimary + '</a>'; // row object contains the row data
                            return a;
                        },
                        "sWidth": "20%"
                    },
                    {
                        "mDataProp": "BAEmailPrimary",
                        "sWidth": "20%",
                        "orderable": true,
                        "searchable": true
                    },
                    {
                        "mDataProp": "BAODeptPrimary",
                        "sWidth": "10%",
                        "orderable": true,
                        "searchable": true
                    },
                    {
                        "mDataProp": "BAOwnerSecondary",
                        "sWidth": "20%",
                        "orderable": true,
                        "searchable": true
                    },
                    {
                        "mDataProp": "BAEmailSecondary",
                        "sWidth": "10%",
                        "orderable": true,
                        "searchable": true
                    },

            ],
            "scrollY": '40vh',
            "scrollCollapse": true,

            "fnInitComplete": function () {

                $("#dtBAODetail").css("width", "100%");
                $(".dataTables_scrollHead").css("width", "100%");
                $(".dataTables_scrollHeadInner").css("width", "100%");
                $(".dataTables_scrollHeadInner table").css("width", "100%");

                $("div.dataTables_wrapper div.dataTables_filter label").css("width", "100%");
            }
             , "fnDrawCallback": function () {
                 $("#dtBAODetail").css("width", "100%");
                 $(".dataTables_scrollHead").css("width", "100%");
                 $(".dataTables_scrollHeadInner").css("width", "100%");
                 $(".dataTables_scrollHeadInner table").css("width", "100%");

                 $("div.dataTables_wrapper div.dataTables_filter label").css("width", "100%");
             }
        });

        //For Default Selection    
        var bpInfo = $('#txtWebBPC').val().replace('-', ' ');
        $('#dtBAODetail').DataTable().search(bpInfo).draw();

    }
    , loadBAODetail: function (BAOName, BAODept, BAOId) {
        var baoInfo = '';
        if ((BAOName != null && $.trim(BAOName) != '') && (BAODept != null && $.trim(BAODept) != '')) {

            baoInfo = BAOName + ' - ' + BAODept;
        }
        else if ((BAOName == null || $.trim(BAOName) == '') && (BAODept != null && $.trim(BAODept) != '')) {

            baoInfo = BAODept;
        }
        else if ((BAOName != null && $.trim(BAOName) != '') && (BAODept == null || $.trim(BAODept) == '')) {
            baoInfo = BAOName;
        }
        else {
            baoInfo = '';
        }
        $('#txtWebBPC').val(BAOName);
        $('#txtWebBPCId').val(BAOId);
        $("#dvSubModal").modal('hide');
    }

    , loadAllWebStateSites: function () {
        var sUrl = $('#uGetWebList').val();
        var searchData = {
            Id: 0,
            WebSiteType: 'web'
        };
        $('#dvLoadModal').modal('show');

        $.ajax({
            url: sUrl,
            data: searchData,
            type: 'GET',
            cache: false,
            contentType: 'text/json',
            success: function (data) {
                $('#dvLoadModal').modal('hide');
                if (data != null) {
                    var mySelect = $('#cmbWebBlog');
                    var valOfWebState = "";
                    var webState = "";
                    
                    $.each(data, function (index, element) {
                        if (element.WebStat != null && element.WebStat != "") {
                            webState = element.WebStat;

                            var webStateaddress = webState.slice(0, webState.lastIndexOf('/'));

                            valOfWebState = webStateaddress + "/Daily_Activity_for__" + element.WebFolder + "_.png";
                            
                            mySelect.append($('<option></option>').val(valOfWebState).html(element.WebFolder));
                        }
                    });                    
                }

                $("#spnWebStateGen").text(moment().format("MM/DD/YYYY"));
            },
            error: function (data, status, jqXHR) {
                $('#dvLoadModal').modal('hide');
            }
        });
    }   

    //End of Namespace
}



