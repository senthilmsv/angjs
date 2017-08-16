ClientAppCRUD = {
    appSearchResult: function (defaultLoad) {
        var sUrl = $('#hdnAppLoadUrl').val();

        var searchData = {
            Id: 0
        };

        $('#dvLoadModal').modal('show');

        $.ajax({
            url: sUrl,
            data: searchData,
            type: 'POST',
            cache: false,
            contentType: 'text/json',
            success: function (data) {
                if (data != null) {
                    ClientAppCRUD.loadAppDetails(data);
                    ClientAppCRUD.disableAppCtrl('disabled');
                    $('#dvLoadModal').modal('hide');
                }
            },
            error: function (data, status, jqXHR) {
                $('#dvLoadModal').modal('hide');
            }
        });
    }

    , loadAppDetails: function (data) {
        var userType = $('#hdnUserType').data('value');

        $('#dtHiradApp').dataTable().fnDestroy();
      
        $('#dtHiradApp').dataTable({
            "oLanguage": {
                "sSearch": "Search all columns:"
            },
            /* For Default Search */
            //"oSearch": { "sSearch": "AD RAS HIAPLCTN INT" },
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
                        //    return '<a href="#" class="viewClientApp" data-appid=' + o.Id + '>' + 'View' + '</a> ';
                        //}
                        "mRender": function (o) {
                            if (userType == "A") {
                                return '<div style="text-align:center;"><table><tr><td> <a href="#" class="viewClientApp" data-appid=' + o.Id + '>' + '<i class="fa fa-bars" title="View"></i>' + '</a></td>'
                                + '<td>&nbsp;&nbsp;</td><td><a href="#" class="viewClientApp" data-appid=' + o.Id + '>' + '<i class="fa fa-edit" title="Edit"></i>' + '</a></td>'
                                + '<td>&nbsp;&nbsp;</td><td><a href="#" class="viewClientApp" data-appid=' + o.Id + '>' + '<i class="fa fa-trash" title="Delete"></i>' + '</a></td></tr></table> </div>';
                            }
                            else {
                                return '<div style="text-align:center;"> <a href="#" class="viewClientApp" data-appid=' + o.Id + '>' + '<i class="fa fa-bars" title="View"></i>' + '</a>';
                            }
                        }
                    },
                    //{
                    //    "sWidth": "3%",                       
                    //    "mDataProp": null,
                    //    "bSortable": false,
                    //    "mRender": function (o) {
                    //        return '<a href="#" class="viewClientApp" data-appid=' + o.Id + '>' + 'Edit' + '</a> ';
                    //    }
                    //},
                    //{
                    //    "sWidth": "4%",                       
                    //    "mDataProp": null,
                    //    "bSortable": false,
                    //    "mRender": function (o) {
                    //        return '<a href="#" class="viewClientApp" data-appid=' + o.Id + '>' + 'Delete' + '</a>';
                    //    }
                    //},
                    {
                        "mDataProp": "Application",
                        //"sWidth": "13%",
                        "bAutoWidth": true,
                        "mRender": function (data, type, full) {
                            return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;

                        }
                    },
                    {
                        "mDataProp": "Version"  ,
                        //"sWidth": "5%",
                        "bAutoWidth": true,
                        "mRender": function (data, type, full) {
                            return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;

                        }
                    },
                    
                    {
                        "mDataProp": "APPServerText",
                        //"sWidth": "14%",
                        "bAutoWidth": true,
                        "mRender": function (data, type, full) {
                            return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;

                        }
                    },
                    {
                        "mDataProp": "ApplicationLayer",
                        //"sWidth": "10%",
                        "bAutoWidth": true,
                        "mRender": function (data, type, full) {
                            return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;

                        }
                    },
                    {
                        "mDataProp": "Layer5Location",
                        //"sWidth": "10%",
                        "bAutoWidth": true,
                        "mRender": function (data, type, full) {
                            return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;

                        }
                    },
                    {
                        "mDataProp": "RemedyGroupName",
                        //"sWidth": "13%",
                        "bAutoWidth": true,
                        "mRender": function (data, type, full) {
                            return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;

                        }
                    },
                     {
                         "mDataProp": "RADPOC" ,
                         //"sWidth": "13%",
                         "bAutoWidth": true,
                         "mRender": function (data, type, full) {
                             return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;

                         }
                     },
                      {
                          "mDataProp": "BAOwnerPrimary" ,
                          //"sWidth": "12%",
                          "bAutoWidth": true,
                          "mRender": function (data, type, full) {
                              return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;

                          }
                      },
                      { "mDataProp": "ABCID", "bAutoWidth": true },
                    //{ "mDataProp": "StatusTypeId" },
                    { "mDataProp": "Vendor" },
                    { "mDataProp": "WebsiteURL" },
                    { "mDataProp": "SATName" },
                    { "mDataProp": "LicenseType" },
                    { "mDataProp": "Windows1032Tested" },
                    { "mDataProp": "Windows1064Tested" },                  
                    { "mDataProp": "ApplicationLiveDate" },
                    { "mDataProp": "ApplicationRenewalDate" }

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
                targets: [-1, -2, -3, -4, -5, -6, -7, -8],
                visible: false
            }],
            "fnDrawCallback": function () {
                ClientAppCRUD.initOnClickEvents();
            }            
           
        });

        //if (userType == "G") {
        //    var table = $('#dtHiradApp').DataTable();
        //    // Hide edit & delete column to General User
        //    table.column(1).visible(false);
        //    table.column(2).visible(false);
        //}

     
    }

    , reset_datatable_state: function () {       
        localStorage.removeItem('DataTables_dtHiradApp_' + window.location.pathname);        
    }

    //End of Function

    , initOnClickEvents: function () {

        //on Each Row click
        $(".viewClientApp").unbind();
        $(".viewClientApp").on('click', (function () {
            var clickedIcon = $(this).find('i')[0];
            if (clickedIcon != null) {
                if (clickedIcon.title == "Delete"){
                    var userType = $('#hdnUserType').data('value');
                    if (userType == "A") {
                        ClientAppCRUD.confirm_delete($(this).data("appid"));
                    }
                }
                else {
                    ClientAppCRUD.viewClientInformation($(this).data("appid"), clickedIcon.title)
                }
            }
        }));

        $("#btnClientAppsAdd").unbind();
        $("#btnClientAppsAdd").on('click', function () {
            ClientAppCRUD.viewClientInformation(0, "Add")
        });


    }
    //End Of Function

    , viewClientInformation: function (appId, mode) {
        //on Each Row click
        var record = {
            appId: appId,
            actionMode: mode
        };
        $.ajax({
            //Calling Partial View
            url: $("#uviewClientInformation").val(),
            type: 'POST',
            data: record,
            async: true,
            success: function (data) {
                if (data != null) {
                    $('#dvMdlDlgContent').html(data);
                    $("#dvModal").modal();
                    ClientAppCRUD.showHideButtons();
                    return false;
                }
            },
            error: function (data, status, jqXHR) {

                console.log('Error in view Client Apps Information: ' + jqXHR + "," + data.responseText);
            }
        });
    }

    , showHideButtons: function () {

        //   $("#txtAld").mask("99/99/9999");        
        //var date = new Date();
        //$("#txtAld").val(moment(date).format('MM/DD/YYYY'));

        $('input[name="txtAld"]').daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            autoUpdateInput: false,
            locale: {
                format: 'MM-DD-YYYY'
            }
        }, function (chosen_date) {
            $('#txtAld').val(chosen_date.format('MM-DD-YYYY'));
        });

        //var date1 = new Date();
        //$("#txtArd").val(moment(date1).format('MM/DD/YYYY'));

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


        //$("#ddlLayerTypes").on('change', function () {
        //    ClientAppCRUD.getLayerLocation();
        //});

        if ($("#hdnAppAction").val() == "View") {
            $("#btnAppUpdate").hide();
            $("#btnServerSearch, #btnBAOSearch").on('click', function () {
                event.preventDefault();
            });
        }
        else if ($("#hdnAppAction").val() == "Edit") {

            $("#btnAppUpdate").on("click", function () {
                if (ClientAppCRUD.validateClientApp()) {
                    ClientAppCRUD.updateClientApp();
                }
            });

            $("#btnServerSearch").on('click', function () {
                $("#hdnSeverSearchFor").val('APP');
                event.preventDefault();
                ClientAppCRUD.showServerPopup();
            });

            $("#btnBAOSearch").on('click', function () {
                event.preventDefault();
                ClientAppCRUD.showBAOPopup();
            });
        }
        else if ($("#hdnAppAction").val() == "Add") {
            $("#btnAppUpdate").text("Add Client App");
            $("#btnAppUpdate").on("click", function () {
                if (ClientAppCRUD.validateClientApp()) {
                    ClientAppCRUD.updateClientApp();
                }
            });
            $("#btnServerSearch").on('click', function () {
                event.preventDefault();
                $("#hdnSeverSearchFor").val('APP');
                ClientAppCRUD.showServerPopup();
            });

            $("#btnBAOSearch").on('click', function () {
                event.preventDefault();
                ClientAppCRUD.showBAOPopup();
            });

        }
        else {
            $("#btnAppUpdate").hide();

        }
        if ($("#hdnAppAction").val() == "View") {
            $("#dvModal").find('input:text').attr('disabled', 'disabled');
            $("#dvModal").find('input:checkbox').attr('disabled', 'disabled');
            $("#dvModal").find('input:radio').attr('disabled', 'disabled');
            $("#dvModal").find('textarea').attr('disabled', 'disabled');
            $("#dvModal").find('select').attr('disabled', 'disabled');
        }

    }
      , validateClientApp: function () {

          if ($.trim($('#txtApp').val()) == '') {
              dashboard.showMessage('warning', 'Please enter Application', false);
              return false;
          }
          return true;
      }

    , updateClientApp: function () {
        var sUrl = $('#hdnClientAppSaveUrl').val();
        var appId = $('#hdnSelectedRowId').val();

        var application = $.trim($('#txtApp').val());
        var version = $.trim($('#txtVer').val());
        var vendor = $.trim($('#txtVen').val());
        var webURL = $.trim($('#txtWebURL').val());
        var vendorPOC = $.trim($('#txtVenPOC').val());
        var vendorPhone = $.trim($('#txtVenPhone').val());
        var abcID = $.trim($('#txtABC').val());
        var desc = $.trim($('#txtDesc').val());
        var appServerName = $.trim($('#txtAppServer').val());
        //  var dbServerName = $('#txtdbSvr').val();
        var applicationLayer = $('#ddlLayerTypes option:selected').text();
        var layerId = $('#ddlLayerTypes option:selected').val();
        var sATName = $.trim($('#txtSat').val());
        var layer5Location = $.trim($('#txtllo').val());
        var licenseType = $("input[name='rdLR']:checked").val();
        var licenseInformation = $.trim($('#txtLci').val());

        var comments = $.trim($('#txtComment').val());
        var rgn = $.trim($('#txtRgn').val());
        var windows1032Tested = $("input[name='rdW10T']:checked").val();
        var windows1064Tested = $("input[name='rdW1064T']:checked").val();
        var ps = $.trim($('#txtPS').val());
        var ss = $.trim($('#txtSS').val());
        var applicationLiveDate = $.trim($('#txtAld').val());
        var applicationRenewalDate = $.trim($('#txtArd').val());
        var hospitalApplication = $('#ddlHospitalApp option:selected').text();
        var knownIssues = $.trim($('#txtKni').val());
        var appDomain = $.trim($('#txtad').val());
        var bpInfo = $.trim($('#txtBpc').val());
        var baoId = $.trim($('#txtAppBPCId').val());
        var appServerId = $('#txtServerId').val();
        var statusTypeId = $("#ddlStatusType option:selected").val();
        var createdBy = $("#hdnCreatedBy").val();
        var createdDate = $("#hdnCreatedDate").val();

        var statusChangedOn = $.trim($("#txtStatusTypeChangedOn").val());
        var prvStatusTypeId = $.trim($("#hdnPrvStatusTypeId").val());

        var rec = {
            Id: appId,
            ABCID: abcID,
            Application: application,
            ApplicationLayer: applicationLayer,
            ApplicationLiveDate: applicationLiveDate,
            RADPOC: ps,
            ApplicationRenewalDate: applicationRenewalDate,
            Comments: comments,
            AppServerName: appServerName,
            //  DbServerName: dbServerName,
            RemedyGroupName: rgn,
            HospitalApplication: hospitalApplication,
            KnownIssues: knownIssues,
            Layer5Location: layer5Location,
            LicenseInformation: licenseInformation,
            LicenseType: licenseType,
            SecondarySupport: ss,
            SATName: sATName,
            //Server: server,
            //ServerName: serverName,
            Vendor: vendor,
            Version: version,
            VendorPOC: vendorPOC,
            VendorPhone: vendorPhone,
            Windows1032Tested: windows1032Tested,
            Windows1064Tested: windows1064Tested,
            WebsiteURL: webURL,
            Description: desc,
            ApplicationDomain: appDomain,
            BPInfo: bpInfo,
            ApplicationLayerId: layerId,
            BAOId: baoId,
            StatusTypeId: statusTypeId,
            StatusTypeChangedOn: statusChangedOn,
            PreviousStatusTypeId: prvStatusTypeId,
            AppServerId: appServerId,
            CreatedBy: createdBy,
            CreatedDate: createdDate
        };

        $.ajax({
            url: sUrl,
            data: rec,
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data != null) {
                    if (data.RecStatus == "Saved") {
                        ClientAppCRUD.refreshAppCtrl();
                        ClientAppCRUD.appSearchResult('Y');
                        if (appId == 0 || appId == '') {
                            dashboard.showMessage('success', 'Application added successfully', true);

                        } else {
                            dashboard.showMessage('success', 'Application updated successfully', true);
                        }

                    }
                    else if (data != null && data.RecStatus == "Duplicate") {
                        var sDupMsg = 'Application already exists. Please enter any other Application.';
                        dashboard.showMessage('warning', sDupMsg, false);
                    }
                    else if (data != null && data.RecStatus == "Duplicate Layer") {
                        var sDupMsg = 'The combination of Application Layer and Layer Location already exists. Please enter any other Layer Location.';
                        dashboard.showMessage('warning', sDupMsg, false);
                    }
                    else {
                        dashboard.showMessage('danger', 'Validation failed.', false);
                    }

                }

            },
            error: function (data, status, jqXHR) {
                ClientAppCRUD.refreshAppCtrl();

                dashboard.showMessage('danger', 'ave failed.Please try again', false);
            }
        });

    }




    , confirm_delete: function (aId) {
        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this client app!",
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
                ClientAppCRUD.deleteClientApp(aId);
            }
        });
    }

    , deleteClientApp: function (aId) {
        var sUrl = $('#hdnAppDeleteUrl').val();

        $.ajax({
            url: sUrl,
            data: { appId: aId },
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data != null) {
                    if (data.RecStatus == "Deleted") {
                        ClientAppCRUD.refreshAppCtrl();
                        ClientAppCRUD.appSearchResult('Y');
                        dashboard.showMessage('success', 'Application deleted successfully', true);
                    }
                }

            },
            error: function (data, status, jqXHR) {
                dashboard.showMessage('danger', 'Failed to delete the application details. Please try again.', false);
            }
        });
    }

, refreshAppCtrl: function () {   
    ClientAppCRUD.clearAppData();
    ClientAppCRUD.disableAppCtrl('disabled');
}


, clearAppData: function () {   
    $("#optHAE").prop('selectedIndex', 0);
    $("#ddlLayerTypes").prop('selectedIndex', 0);
    $('#hdnSelectedRowId').val('');
}
, disableAppCtrl: function (disable) {
    var userType = $('#hdnUserType').data('value');
    if (disable == 'disabled') {
        if (userType == "G") {
            $("#btnClientAppsAdd").attr('disabled', 'disabled');
        }
    }

}
    //, getLayerLocation: function () {
    //    var lId = $("#ddlLayerTypes option:selected").val();
    //    var sUrl = $('#hdnGetLayerLocUrl').val();

    //    $.ajax({
    //        url: sUrl,
    //        data: { lId: lId },
    //        dataType: 'json',
    //        type: 'POST',
    //        success: function (data) {
    //            if (data != null) {
    //                var loc = data['LayerLocation'];
    //                $('#txtllo').val(loc);
    //            }
    //        },
    //        error: function (data, status, jqXHR) {
    //            $('#txtllo').val('');
    //        }
    //    });


    //}

     , showServerPopup: function () {

         $.ajax({
             //Calling Partial View
             url: $("#uSearchServerUrl").val(),
             type: 'GET',
             async: true,
             success: function (data) {
                 if (data != null) {
                     //ClientAppCRUD.serverSearch('Y');
                     $('#dvSubMdlDlgContent').html(data);
                     $("#dvSubModal").modal();
                     return false;
                 }
             },
             error: function (data, status, jqXHR) {
                 dashboard.showMessage('danger', 'Error in Server Search', false);
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
                    ClientAppCRUD.loadServerSearchResult(data);
                }
            },
            error: function (data, status, jqXHR) {
                dashboard.showMessage('danger', 'App Server Search failed. Please try again', false);
            }
        });
    }



     , loadServerSearchResult: function (data) {         
         $('#dtServerDetail').dataTable().fnDestroy();

         var dtServerTable = $('#dtServerDetail').dataTable({
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
                    "render": function (data, type, row, meta) { // render event defines the markup of the cell text 
                        var a = '<a onclick="ClientAppCRUD.loadServerData(' + "'" + row.SystemName + "'," + row.Id + ')" ' +
                                    'style="cursor:pointer;" >' + row.SystemName + '</a>'; // row object contains the row data                        
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
         }); //End of Table
         
         //For Default Selection         
         $('#dtServerDetail').DataTable().search($("#txtAppServer").val()).draw();
     }

     , loadServerData: function (servername, sid) {
         $('#txtAppServer').val(servername);
         $('#txtServerId').val(sid);
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
                     ClientAppCRUD.BAOSearch('Y');
                     $('#dvSubMdlDlgContent').html(data);
                     $("#dvSubModal").modal();
                 }
             },
             error: function (data, status, jqXHR) {

                 dashboard.showMessage('Danger', 'Error in Server Search', false);
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
                    ClientAppCRUD.loadBAOSearchResult(data);
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
                            var a = '<a onclick="ClientAppCRUD.loadBAODetail(' + "'" + row.BAOwnerPrimary + "', '" + row.BAODeptPrimary + "'," + row.Id + ')" ><i class="fa fa-edit"></i>  ' + row.BAOwnerPrimary + '</a>'; // row object contains the row data                                        
                            return a;
                        }
                         ,
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
        var bpInfo = $('#txtBpc').val().replace('-', ' ');
        $('#dtBAODetail').DataTable().search(bpInfo).draw();
    }
    , loadBAODetail: function (BAOName, BAODept, BAOId) {

        var baoInfo='';
        if ((BAOName != null && $.trim(BAOName) != '') && (BAODept != null && $.trim(BAODept) != '')) {

            baoInfo = BAOName + ' - ' +BAODept;
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
        $('#txtBpc').val(BAOName);
        $('#txtAppBPCId').val(BAOId);
        $("#dvSubModal").modal('hide');
    }

    //End of Namespace
}