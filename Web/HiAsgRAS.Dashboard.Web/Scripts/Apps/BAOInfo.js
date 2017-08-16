baoInfoCRUD = {        
   
    //Page Related

    searchResult: function () {      
        $('#dvLoadModal').modal('show');
        $.ajax({
            url: $('#uSearchBAOList').val(),
            type: 'GET',
            cache: false,
            contentType: 'text/json',
            success: function (data) {
                if (data != null) {
                    baoInfoCRUD.loadBAOList(data);
                    $('#dvLoadModal').modal('hide');
                }
            },
            error: function (data, status, jqXHR) {
                $('#dvLoadModal').modal('hide');
                dashboard.showMessage('Danger', 'Search failed. Please try again', false);
            }
        });
    }

, loadBAOList: function (data) {
    var userType = $('#hdnUserType').data('value');

    $('#hiBAOInfo').dataTable().fnDestroy();

    $('#hiBAOInfo').dataTable({
        "oLanguage": {
            "sSearch": "Search all columns:"
        },
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
                 //"mRender": function (o) { return '<a href="#" class="viewBAOInfo" data-baoid=' + o.Id + '>' + 'View' + '</a> '; }
                 //"mRender": function (o) { return '<a href="#" class="viewBAOInfo" data-baoid=' + o.Id + '>' + '<i class="fa fa-bars" title="View"></i>' + '</a> '; }
                 "mRender": function (o) {
                     if (userType == "A") {
                         return '<div style="text-align:center;"><table><tr><td> <a href="#" class="viewBAOInfo" data-baoid=' + o.Id + '>' + '<i class="fa fa-bars" title="View"></i>' + '</a></td>'
                         + '<td>&nbsp;&nbsp;</td><td><a href="#" class="viewBAOInfo" data-baoid=' + o.Id + '>' + '<i class="fa fa-edit" title="Edit"></i>' + '</a></td>'
                         + '<td>&nbsp;&nbsp;</td><td><a href="#" class="viewBAOInfo" data-baoid=' + o.Id + '>' + '<i class="fa fa-trash" title="Delete"></i>' + '</a></td></tr></table> </div>';
                     }
                     else {
                         return '<div style="text-align:center;"> <a href="#" class="viewBAOInfo" data-baoid=' + o.Id + '>' + '<i class="fa fa-bars" title="View"></i>' + '</a>';
                     }
                 }
             },
              //{
              //    "mDataProp": null,
              //    "sWidth": "3%",
              //    "bSortable": false,
              //    //"mRender": function (o) { return '<a href="#" class="viewBAOInfo" data-baoid=' + o.Id + '>' + 'Edit' + '</a> '; }
              //    "mRender": function (o) { return '<a href="#" class="viewBAOInfo" data-baoid=' + o.Id + '>' + '<i class="fa fa-edit" title="Edit"></i>' + '</a> '; }
              //},
              //   {
              //       "sWidth": "4%",
              //       "mDataProp": null,
              //       "bSortable": false,
              //       "mRender": function (o) {
              //           //return '<a href="#" class="viewBAOInfo" data-baoid=' + o.Id + '>' + 'Delete' + '</a>';
              //           return '<a href="#" class="viewBAOInfo" data-baoid=' + o.Id + '>' + '<i class="fa fa-trash" title="Delete"></i>' + '</a>';
              //       }
              //   },
                {
                    "mDataProp": "BAOwnerPrimary",
                    //"sWidth": "20%",
                    "bAutoWidth": true,
                    "mRender": function (data, type, full) {
                        return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;

                    }
                },

                {
                    "mDataProp": "BAEmailPrimary",
                    //"sWidth": "15%",
                    "bAutoWidth": true,
                    "mRender": function (data, type, full) {
                        return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;

                    }
                },
                {
                    "mDataProp": "BAPhonePrimary",
                    "bAutoWidth": true
                },
                {
                    "mDataProp": "BAODeptPrimary",
                    "bAutoWidth": true
                },
                {
                    "mDataProp": "BAOwnerSecondary",
                    //"sWidth": "15%",
                    "bAutoWidth": true,
                    "mRender": function (data, type, full) {
                        return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;

                    }
                },

                {
                    "mDataProp": "BAEmailSecondary",
                    //"sWidth": "15%",
                    "bAutoWidth": true,
                    "mRender": function (data, type, full) {
                        return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;
                    }
                },
                     {
                         "mDataProp": "BAPhoneSecondary",
                         "bAutoWidth": true,
                     },
                {
                    "mDataProp": "BAODeptSecondary",
                    //"sWidth": "15%",
                    "bAutoWidth": true,
                    "mRender": function (data, type, full) {
                        return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;

                    }
                }
                //,
                //{
                //    "mDataProp": "IsActive",
                //    //"sWidth": "10%",
                //    "bAutoWidth": true,
                //    "mRender": function (data, type, full) {
                //        return data.toLowerCase() == 'yes' ? 'Yes' : 'No';
                //    }
                //}
                //{ "mDataProp": "ID" },


        ]
        ,
        dom: 'B<"clear">lfrtip',
        buttons: [
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
            //targets: [5, 6, 9, 10],
            targets: [3,4,7,8],
            visible: false
        }],
        "fnDrawCallback": function () {
            baoInfoCRUD.initOnClickEvents();
        }

    });

   // if (userType == "G") {
       // var table = $('#hiBAOInfo').DataTable();
       // // Hide edit & delete column to General User
       // table.column(1).visible(false);
      //  table.column(2).visible(false);
   // }


}
      , reset_datatable_state: function () {
          localStorage.removeItem('DataTables_hiBAOInfo_' + window.location.pathname);
      }
    , initOnClickEvents: function () {
        //on Each Row click
        $(".viewBAOInfo").unbind();
        $(".viewBAOInfo").on('click', (function () {            
            var clickedIcon = $(this).find('i')[0];
            if (clickedIcon != null) {
                if (clickedIcon.title == "Delete") { // if ($(this).text() == "Delete") {

                    var userType = $('#hdnUserType').data('value');
                    if (userType == "A") {
                        baoInfoCRUD.confirm_deleteBAO($(this).data("baoid"));
                    }
                }
                else {
                    baoInfoCRUD.viewBAOInformation($(this).data("baoid"), clickedIcon.title)
                }
            }
        }));

        $("#btnBAOInfoAdd").unbind();
        $("#btnBAOInfoAdd").on('click', function () {
            baoInfoCRUD.viewBAOInformation(0, "Add");
        });

     
    }

    , viewBAOInformation: function (baoId, mode) {
        event.preventDefault();
        event.stopImmediatePropagation();
        //on Each Row click
        var record = {
            baoId: baoId,
            actionMode: mode
        };
        $.ajax({
            //Calling Partial View
            url: $("#uViewBAOInformation").val(),
            type: 'POST',
            data: record,
            async: true,
            success: function (data) {             
                if (data != null) {
                    $('#dvMdlDlgContent').html(data);
                    $("#dvModal").modal();
                    baoInfoCRUD.showHideButtons();
                }
            },
            error: function (data, status, jqXHR) {      
                dashboard.showMessage('Danger', 'Error in view BAO Information', false);
            }
        });
        
        return false;
    }

    , showHideButtons: function () {
        if ($("#hdnBAOInfoAction").val() == "View") {
            $("#btnBAOInfoUpdate").hide();            
        }
        else if ($("#hdnBAOInfoAction").val() == "Edit") {            
            $("#btnBAOInfoUpdate").on("click", function () {                           
                if (baoInfoCRUD.validateBAOInfo()) {
                    baoInfoCRUD.updateBAOInfo();
                }
            });          
        }
        else if ($("#hdnBAOInfoAction").val() == "Add") {
            $("#btnBAOInfoUpdate").text("Add BAO");
            $("#btnBAOInfoUpdate").on("click", function () {
                if (baoInfoCRUD.validateBAOInfo()) {
                    baoInfoCRUD.updateBAOInfo();
                }
            });   
        }
        else {
            $("#btnBAOInfoUpdate").hide();
            
        }

        if ($("#hdnBAOInfoAction").val() == "View") {
            $("#dvModal").find('input:text').attr('disabled', 'disabled');
            $("#dvModal").find('input:checkbox').attr('disabled', 'disabled');
            $("#dvModal").find('input:radio').attr('disabled', 'disabled');
            $("#dvModal").find('textarea').attr('disabled', 'disabled');
            $("#dvModal").find('select').attr('disabled', 'disabled');
        }
    }

    , validateBAOInfo: function () {
        if ($.trim($('#txtBAOP').val()) == '') {
            dashboard.showMessage('warning', 'Please enter Primary APP Owner ', false);
            return false;
        }
        if ($.trim($('#txtBAPE').val()) == '') {
            dashboard.showMessage('warning', 'Please enter Primary Email', false);
            return false;
        }
        return true;
    }
    , updateBAOInfo: function () {
        
        var sUrl = $('#hdnSaveBAOUrl').val();
        var baoId = $('#hdnSelectedRowId').val();
     
        var baop = $.trim($('#txtBAOP').val());
        var bapp = $.trim($('#txtBAPP').val());
        var bape = $.trim($('#txtBAPE').val());
        var badp = $.trim($('#txtBADP').val());
        var baos = $.trim($('#txtBAOS').val());
        var baps = $.trim($('#txtBAPS').val());
        var baes = $.trim($('#txtBAES').val());
        var bads = $.trim($('#txtBADS').val());
       // var isActive = $("input[name='rdActive']:checked").val();
        var createdBy = $("#hdnCreatedBy").val();
        var createdDate = $("#hdnCreatedDate").val();

        if (bape != "") {
            if (!baoInfoCRUD.validateEmail(bape)) {
                dashboard.showMessage('warning', 'Please enter valid Primary Email', false);
                return false;
            }
        }

        if (baes != "") {
            if (!baoInfoCRUD.validateEmail(baes)) {
                dashboard.showMessage('warning', 'Please enter valid Secondary Email', false);
                return false;
            }
        }

        var rec = {
            Id: baoId,
            BAOwnerPrimary: baop,
            BAPhonePrimary: bapp,
            BAEmailPrimary: bape,
            BAODeptPrimary: badp,
            BAOwnerSecondary: baos,
            BAPhoneSecondary: baps,
            BAEmailSecondary: baes,
            BAODeptSecondary: bads,
           // IsActive: isActive,
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
                        baoInfoCRUD.searchResult();
                        baoInfoCRUD.refresBAOInfoCtrl();
                        if (baoId == 0 || baoId == '') {                            
                            dashboard.showMessage('success', 'BAO information added successfully', true);

                        } else {                            
                            dashboard.showMessage('success', 'BAO information updated successfully', true);
                        }

                    }
                    else if (data != null && data.RecStatus == "Duplicate") {
                        var sDupMsg = 'Primary Email already exists. Please enter any other email address.';
                        dashboard.showMessage('warning', sDupMsg, false);
                    }
                    else {
                        dashboard.showMessage('danger', 'Validation failed.', false);
                    }
                }
                
            },
            error: function (data, status, jqXHR) {

                baoInfoCRUD.clearBAOInfo();
                dashboard.showMessage('danger', 'Save failed.Please try again.', false);
            }
        });

    }

    , refresBAOInfoCtrl: function () {
        baoInfoCRUD.clearBAOInfo();
        baoInfoCRUD.disableCtrl('disabled');
    }   

    , clearBAOInfo: function () {
        $('#hdnSelectedRowId').val('');        
        $('#txtBAOP').val('');
        $('#txtBAPP').val('');
        $('#txtBAPE').val('');
        $('#txtBADP').val('');
        $('#txtBAOS').val('');
        $('#txtBAPS').val('');
        $('#txtBAES').val('');
        $('#txtBADS').val('');
        $('#rdNo').removeAttr('checked');
        $("#rdYes").attr('checked', 'checked');
    }

    , disableCtrl: function (disable) {
        var userType = $('#hdnUserType').data('value');
        if (disable == 'disabled') {
            if (userType == "G") {
                $("#btnBAOInfoAdd").attr('disabled', 'disabled');
            }
        }
    }


    , confirm_deleteBAO: function (baoId) {
        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this BAO!",
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
                baoInfoCRUD.deleteBAO(baoId);
            }
        });
    }

    , deleteBAO: function (baoId) {        
        var sUrl = $('#hdnBAODeleteUrl').val();
        var rec = {
            baoId: baoId
        };
        $.ajax({
            url: sUrl,
            data: rec,
            dataType: 'json',
            type: 'POST',
            async:true,
            success: function (data) {
                if (data != null) {
                    if (data.RecStatus == "Deleted") {
                        baoInfoCRUD.refresBAOInfoCtrl();
                        baoInfoCRUD.searchResult();
                        dashboard.showMessage('success', 'BAO deleted successfully', true);
                    }
                    else if (data != null && data.RecStatus == "Not Deleted") {
                        var sDupMsg = 'We could not delete this BAO since it is used in applications. Please delete them and try again later.';
                        dashboard.showMessage('warning', sDupMsg, false);
                    }
                }
            },
            error: function (data, status, jqXHR) {
                console.log(jqXHR);
                dashboard.showMessage('danger', 'Failed to delete BAO details. Please try again later.', false);
            }
        });
    }

    , validateEmail: function (email) {
        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        return emailReg.test(email);
    }


  
    //End of Namespace
}



