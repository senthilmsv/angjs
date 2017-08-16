applicationInfoCRUD = {        
   
    //Page Related
    getAppInfoList: function () {      
        $('#dvLoadModal').modal('show');
        $.ajax({
            url: $('#uGetAppInfoList').val(),
            type: 'GET',
            cache: false,
            contentType: 'text/json',
            success: function (data) {
                if (data != null) {
                    applicationInfoCRUD.loadApplicationList(data);
                    $('#dvLoadModal').modal('hide');
                }
            },
            error: function (data, status, jqXHR) {
                $('#dvLoadModal').modal('hide');
                dashboard.showMessage('Danger', 'Search failed. Please try again', false);
            }
        });
    },
 loadApplicationList: function (data) {
  
    $('#hiAppInfo').dataTable().fnDestroy();

    $('#hiAppInfo').dataTable({
        "oLanguage": {
            "sSearch": "Search all columns:"
        },
        "lengthMenu": [[-1, 10, 25, 50, 100], ["All", 10, 25, 50, 100]],
        "bDestroy": true,
        "bResponsive": "true",
        "aaData": data,        
        "order": [[1, "asc"]],
        "bStateSave": true,
        "bAutoWidth":false,
        "aoColumns": [
              {
                  "sWidth": "3%",
                  "mDataProp": null,
                  "bSortable": false,                  
                  "mRender": function (o) {
                      return '<div style="text-align:center;"> <a href="#" class="viewAppInfo" data-appid=' + o.Id + '>' + '<i class="fa fa-bars" title="View"></i>' + '</a>';
                  }
              },              
                {
                    "mDataProp": "ApplicationName",
                    "bAutoWidth": true,
                    "mRender": function (data, type, full) {
                        return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;

                    }
                },
                {
                    "mDataProp": "ApplicationType",
                    "bAutoWidth": true,
                    "mRender": function (data, type, full) {
                        return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;

                    }
                },
                {
                    "mDataProp": "ApplicationInformation",
                    "bAutoWidth": true,
                    "mRender": function (data, type, full) {
                        return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;

                    }
                },
                {
                    "mDataProp": "Comments",
                    "bAutoWidth": true,
                    "mRender": function (data, type, full) {
                        return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;

                    }
                }
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
        "fnDrawCallback": function () {
            applicationInfoCRUD.initOnClickEvents();
        }      
      
    }); 

}
    , reset_datatable_state: function () {
        localStorage.removeItem('DataTables_hiAppInfo_' + window.location.pathname);
       }
    , initOnClickEvents: function () {
        //on Each Row click
        $(".viewAppInfo").unbind();
        $(".viewAppInfo").on('click', (function () {
            var clickedIcon = $(this).find('i')[0];
            if (clickedIcon != null) {
                applicationInfoCRUD.viewAppInformation($(this).data("appid"))
            }
           
        }));
     
    }

    , viewAppInformation: function (appId) {
        //on Each Row click
        var record = {
            appId: appId          
        };
        $.ajax({
            //Calling Partial View
            url: $("#uViewappInformation").val(),
            type: 'POST',
            data: record,
            async: true,
            success: function (data) {             
                if (data != null) {
                    $('#dvMdlDlgContent').html(data);
                    $("#dvModal").modal();
                    
                }
            },
            error: function (data, status, jqXHR) {      
                dashboard.showMessage('Danger', 'Error in view Application Information', false);
            }
        });
        
        return false;
    }

       , validateApplicationInfo: function () {
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
   
    , updateApplicationInfo: function () {        
        var sUrl = $('#hdnUpdateUrl').val();
        var baoId = $('#hdnBAOId').val();
        var appId = $('#hdnAppId').val();
     
        var baop = $.trim($('#txtBAOP').val());
        var bapp = $.trim($('#txtBAPP').val());
        var bape = $.trim($('#txtBAPE').val());
        var badp = $.trim($('#txtBADP').val());
        var baos = $.trim($('#txtBAOS').val());
        var baps = $.trim($('#txtBAPS').val());
        var baes = $.trim($('#txtBAES').val());
        var bads = $.trim($('#txtBADS').val());

        var appType = $.trim($('#hdnAppType').val());
        var appName = $.trim($('#txtAppName').val());
        var appInfo = $("#ddlAppInformation option:selected").val();
        var comments = $.trim($('#txtComment').val());

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

        var app = {
            ApplicationId: appId,
            ApplicationType: appType,
            ApplicationName: appName,
            ApplicationInformation: appInfo,
            Comments: comments
        };

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
            ApplicationInformation: app
        };


        $.ajax({
            url: sUrl,
            data: rec,
            dataType: 'json',
            type: 'POST',
            success: function (data) {

                if (data != null) {
                    if (data.RecStatus == "Saved") {
                        dashboard.showMessage('success', 'Application information updated successfully', true);
                    }
                    else if (data != null && data.RecStatus == "Duplicate") {
                        var sDupMsg = 'BAO already exists. Please enter any other BAO detail.';
                        dashboard.showMessage('warning', sDupMsg, false);
                    }
                    else {
                        dashboard.showMessage('danger', 'Validation failed.', false);
                    }
                }
                
            },
            error: function (data, status, jqXHR) {                
                dashboard.showMessage('danger', 'Save failed.Please try again.', false);
            }
        });

    }



  
    //End of Namespace
}



