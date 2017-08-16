var SharedPath = {

    reset_datatable_state: function () {
        localStorage.removeItem('DataTables_hiSharedPath_' + window.location.pathname);
    }


    , searchResult: function (defaultLoad) {

        var sUrl = $('#uSharedPathList').val();


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
                    SharedPath.loadSharedPathDetails(data);
                    SharedPath.disableSharedPathCtrl('disabled');
                    $('#dvLoadModal').modal('hide');
                }
            },
            error: function (data, status, jqXHR) {
                dashboard.showMessage('Danger', 'Search failed. Please try again', false);
                $('#dvLoadModal').modal('hide');
            }
        });
    }

    , loadSharedPathDetails: function (data) {
        var userType = $('#hdnUserType').data('value');

        $('#hiSharedPath').dataTable().fnDestroy();

        $('#hiSharedPath').dataTable({
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
                "mRender": function (o) {
                    if (userType == "A") {
                        return '<div style="text-align:center;"><table><tr><td><a href="#" class="viewSharedPath" data-serverid=' + o.Id + '>' + '<i class="fa fa-bars" title="View"></i>' + '</a></td>'
                        + '<td>&nbsp;&nbsp;</td><td><a href="#" class="viewSharedPath" data-serverid=' + o.Id + '>' + '<i class="fa fa-edit" title="Edit"></i>' + '</a></td>'
                        + '<td>&nbsp;&nbsp;</td><td><a href="#" class="viewSharedPath" data-serverid=' + o.Id + '>' + '<i class="fa fa-trash" title="Delete"></i>' + '</a></td></tr></table> </div>';
                    }
                    else {
                        return '<div style="text-align:center;"> <a href="#" class="viewSharedPath" data-serverid=' + o.Id + '>' + '<i class="fa fa-bars" title="View"></i>' + '</a>';
                    }
                }
            },
            {
                "mDataProp": "Name",
                "bAutoWidth": true,
                "mRender": function (data, type, full) {
                    return type == 'display' && data.length > 50
                        ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>'
                        : data;
                }
            },
            {
                "mDataProp": "ServerName",
                "bAutoWidth": true,
                "mRender": function (data, type, full) {
                    return type == 'display' && data.length > 50
                        ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>'
                        : data;

                }
            },
            {
                "mDataProp": "Path",
                "bAutoWidth": true,
                "orderable": true,
                "searchable": true
            },
            {
                "mDataProp": "BAOwnerPrimary",
                "bAutoWidth": true
            },

            {
                "mDataProp": "BAPhonePrimary",
                "bAutoWidth": true          

            },
            {
                "mDataProp": "BAOwnerSecondary",
                "bAutoWidth": true  
            },

            {
                "mDataProp": "BAPhoneSecondary",
                "bAutoWidth": true
            }      
            
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



        SharedPath.initOnClickEvents();

        //When pagination happend, need to initialize the event
        $('#hiSharedPath').on('draw.dt', function () {
            SharedPath.initOnClickEvents();
        });
    }

    , initOnClickEvents: function () {
        //on Each Row click
        $(".viewSharedPath").unbind();
        $(".viewSharedPath").on('click', (function () {
            var clickedIcon = $(this).find('i')[0];
            if (clickedIcon != null) {
                if (clickedIcon.title == "Delete") {
                    var userType = $('#hdnUserType').data('value');
                    if (userType == "A") {
                        SharedPath.confirm_deleteSharedPath($(this).data("serverid"));
                    }
                }
                else {
                    SharedPath.viewSharedPath($(this).data("serverid"), clickedIcon.title)
                }
            }
        }));

        $("#btnSharePathAdd").unbind();
        $("#btnSharePathAdd").on('click', function () {
            SharedPath.viewSharedPath(0, "Add")
        });

    }
     , showHideButtons: function () {


         if ($("#hdnSharedPathAction").val() == "View") {
             $("#btnSharedPathUpdate").hide();
             $("#btnServerSearch, #btnBAOSearch").on('click', function () {
                 event.preventDefault();
             });
         }
         else if ($("#hdnSharedPathAction").val() == "Edit") {

             $("#btnSharedPathUpdate").on("click", function () {
                 if (SharedPath.validateSharedPath()) {
                     SharedPath.updateSharedPath();
                 }
             });
                       

             $("#btnBAOSearch").on('click', function () {
                 event.preventDefault();
                 ClientAppCRUD.showBAOPopup();
             });
         }
         else if ($("#hdnSharedPathAction").val() == "Add") {
             $("#btnSharedPathUpdate").text("Add Shared NW Path");
             $("#btnSharedPathUpdate").on("click", function () {
                 if (SharedPath.validateSharedPath()) {
                     SharedPath.updateSharedPath();
                 }
             });
            
             $("#btnBAOSearch").on('click', function () {                 
                 event.preventDefault();
                 ClientAppCRUD.showBAOPopup();
             });

         }
         else {
             $("#btnSharedPathUpdate").hide();

         }
         if ($("#hdnSharedPathAction").val() == "View") {
             $("#dvModal").find('input:text').attr('disabled', 'disabled');
             $("#dvModal").find('input:checkbox').attr('disabled', 'disabled');
             $("#dvModal").find('input:radio').attr('disabled', 'disabled');
             $("#dvModal").find('textarea').attr('disabled', 'disabled');
             $("#dvModal").find('select').attr('disabled', 'disabled');
         }

     }
    , viewSharedPath: function (sharedPathId, mode) {
        //on Each Row click
        var record = {
            sharedPathId: sharedPathId,
            actionMode: mode
        };
        $.ajax({
            //Calling Partial View
            url: $("#uViewSharedPathInformation").val(),
            type: 'POST',
            data: record,
            async: true,
            success: function (data) {                
                if (data != null) {
                    $('#dvMdlDlgContent').html(data);
                    $("#dvModal").modal();
                    SharedPath.showHideButtons();
                }
            },
            error: function (data, status, jqXHR) {

                console.log('Error in view Server Information: ' + jqXHR + "," + data.responseText);
            }
        });

        
    }

         , validateSharedPath: function () {

             if ($.trim($('#txtName').val()) == '') {
                 dashboard.showMessage('warning', 'Please enter Shared Network Path Name', false);
                 return false;
             }
             if ($.trim($('#txtPath').val()) == '') {
                 dashboard.showMessage('warning', 'Please enter Shared Network Path', false);
                 return false;
             }
             return true;
         }

    
    , updateSharedPath: function () {
        var sUrl = $('#hdnSaveSharedPathUrl').val();
        var sharedPathId = $('#hdnSelectedRowId').val();

        var name = $.trim($('#txtName').val());
        var path = $.trim($('#txtPath').val());
        var BPCId = $('#txtAppBPCId').val();
        var appServerId = $("#ddlServers option:selected").val();
        var comments = $.trim($('#txtComment').val());
        var createdBy = $("#hdnCreatedBy").val();
        var createdDate = $("#hdnCreatedDate").val();


        var rec = {
            Id: sharedPathId,
            Name: name,
            Path: path,
            BAOId: BPCId,
            AppServerId: appServerId,
            Comments: comments,
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
                        SharedPath.refreshSharedPathCtrl();
                        SharedPath.searchResult('Y');
                        if (sharedPathId == 0 || sharedPathId == '') {
                            dashboard.showMessage('success', 'Shared Network Path added successfully', true);

                        } else {
                            dashboard.showMessage('success', 'Shared Network Path updated successfully', true);
                        }

                    }
                   
                    else {
                        dashboard.showMessage('danger', 'Validation failed.', false);
                    }

                }

            },
            error: function (data, status, jqXHR) {
                SharedPath.refreshSharedPathCtrl();
                dashboard.showMessage('danger', 'Save failed.Please try again', false);
            }
        });
    }
    , refreshSharedPathCtrl: function () {     
        SharedPath.disableSharedPathCtrl('disabled');
    }


, disableSharedPathCtrl: function (disable) {
    var userType = $('#hdnUserType').data('value');
    if (disable == 'disabled') {
        if (userType == "G") {
            $("#btnSharePathAdd").attr('disabled', 'disabled');
        }
    }

}

    , confirm_deleteSharedPath: function (sId) {
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
                SharedPath.deleteSharedPath(sId);
            }
        });
    }

    , deleteSharedPath: function (sId) {
        var sUrl = $('#hdnDeleteSharedPathUrl').val();
        var rec = {
            sharedPathId: sId
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
                        SharedPath.refreshSharedPathCtrl();
                        SharedPath.searchResult('Y');
                        dashboard.showMessage('success', 'Shared Network Path deleted successfully.', true);
                    }
                    else {
                        dashboard.showMessage('danger', data.RecStatus, true);
                    }
                }
            },
            error: function (data, status, jqXHR) {
                console.log(jqXHR);
                dashboard.showMessage('danger', 'Failed to delete the shared network path details. Please try again.', false);
            }
        });        
    }


}