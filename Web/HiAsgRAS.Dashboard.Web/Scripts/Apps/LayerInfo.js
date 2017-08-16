layerInfoCRUD = {        
   
    //Page Related

    searchResult: function () {      
        $('#dvLoadModal').modal('show');
        $.ajax({
            url: $('#uSearchLayerList').val(),
            type: 'GET',
            cache: false,
            contentType: 'text/json',
            success: function (data) {
                if (data != null) {
                    layerInfoCRUD.loadLayerList(data);
                    $('#dvLoadModal').modal('hide');
                }
            },
            error: function (data, status, jqXHR) {
                $('#dvLoadModal').modal('hide');
                dashboard.showMessage('Danger', 'Search failed. Please try again', false);
            }
        });
    }

, loadLayerList: function (data) {
    var userType = $('#hdnUserType').data('value');

    $('#hiLayerInfo').dataTable().fnDestroy();

    $('#hiLayerInfo').dataTable({
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
        "bAutoWidth":false,
        "aoColumns": [
              {
                  "sWidth": "3%",
                  "mDataProp": null,
                  "bSortable": false,
                  //"mRender": function (o) { return '<a href="#" class="viewlayerInfo" data-layerid=' + o.Id + '>' + 'View' + '</a> '; }
                  "mRender": function (o) {
                      if (userType == "A") {
                          return '<div style="text-align:center;"><table><tr><td> <a href="#" class="viewlayerInfo" data-layerid=' + o.Id + '>' + '<i class="fa fa-bars" title="View"></i>' + '</a></td>'
                          + '<td>&nbsp;&nbsp;</td><td><a href="#" class="viewlayerInfo" data-layerid=' + o.Id + '>' + '<i class="fa fa-edit" title="Edit"></i>' + '</a></td>'
                          + '<td>&nbsp;&nbsp;</td><td><a href="#" class="viewlayerInfo" data-layerid=' + o.Id + '>' + '<i class="fa fa-trash" title="Delete"></i>' + '</a></td></tr></table> </div>';
                      }
                      else {
                          return '<div style="text-align:center;"> <a href="#" class="viewlayerInfo" data-layerid=' + o.Id + '>' + '<i class="fa fa-bars" title="View"></i>' + '</a>';
                      }
                  }
              },
               //{
               //    "sWidth": "5%",
               //    "mDataProp": null,
               //    "bSortable": false,
               //    "mRender": function (o) { return '<a href="#" class="viewlayerInfo" data-layerid=' + o.Id + '>' + 'Edit' + '</a> '; }
               //},
               //{
               //    "sWidth": "5%",
               //    "mDataProp": null,
               //    "bSortable": false,
               //    "mRender": function (o) {return '<a href="#" class="viewlayerInfo" data-layerid=' + o.Id + '>' + 'Delete' + '</a>'; }
               //},
                {
                    "mDataProp": "AppLayerName",
                    "sWidth": "15%",
                    "mRender": function (data, type, full) {
                        return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;

                    }
                },
                {
                    "mDataProp": "LayerLocation",
                    "sWidth": "70%",
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
            layerInfoCRUD.initOnClickEvents();
        }      
      
    });
    //if (userType == "G") {
    //    var table = $('#hiLayerInfo').DataTable();
    //    // Hide edit & delete column to General User
    //    table.column(1).visible(false);
    //    table.column(2).visible(false);
    //}   
  

}
    , reset_datatable_state: function () {
           localStorage.removeItem('DataTables_hiLayerInfo_' + window.location.pathname);
       }
    , initOnClickEvents: function () {
        //on Each Row click
        $(".viewlayerInfo").unbind();
        $(".viewlayerInfo").on('click', (function () {
            var clickedIcon = $(this).find('i')[0];
            if (clickedIcon != null) {
                if (clickedIcon.title == "Delete") {

                    var userType = $('#hdnUserType').data('value');
                    if (userType == "A") {
                        layerInfoCRUD.confirm_deleteLayer($(this).data("layerid"));
                    }
                }
                else {
                    layerInfoCRUD.viewLayerInformation($(this).data("layerid"), clickedIcon.title)
                }
            }
           
        }));

        $("#btnLayerInfoAdd").unbind();
        $("#btnLayerInfoAdd").on('click', function () {
            layerInfoCRUD.viewLayerInformation(0, "Add");
        });

     
    }

    , viewLayerInformation: function (layerId, mode) {
        //on Each Row click
        var record = {
            layerId: layerId,
            actionMode: mode
        };
        $.ajax({
            //Calling Partial View
            url: $("#uViewLayerInformation").val(),
            type: 'POST',
            data: record,
            async: true,
            success: function (data) {             
                if (data != null) {
                    $('#dvMdlDlgContent').html(data);
                    $("#dvModal").modal();
                    layerInfoCRUD.showHideButtons();
                }
            },
            error: function (data, status, jqXHR) {      
                dashboard.showMessage('Danger', 'Error in view BAO Information', false);
            }
        });
        
        return false;
    }

    , showHideButtons: function () {
        if ($("#hdnLayerInfoAction").val() == "View") {
            $("#btnLayerInfoUpdate").hide();            
        }
        else if ($("#hdnLayerInfoAction").val() == "Edit") {
            $("#btnLayerInfoUpdate").on("click", function () {
                if (layerInfoCRUD.validateLayerInfo()) {
                    layerInfoCRUD.updateLayerInfo();
                }
               
            });          
        }
        else if ($("#hdnLayerInfoAction").val() == "Add") {
            $("#btnLayerInfoUpdate").text("Add Layer");
            $("#btnLayerInfoUpdate").on("click", function () {
                if (layerInfoCRUD.validateLayerInfo()) {
                    layerInfoCRUD.updateLayerInfo();
                }                
            });   
        }
        else {
            $("#btnLayerInfoUpdate").hide();            
        }

        if ($("#hdnLayerInfoAction").val() == "View") {
            $("#dvModal").find('input:text').attr('disabled', 'disabled');
            $("#dvModal").find('input:checkbox').attr('disabled', 'disabled');
            $("#dvModal").find('input:radio').attr('disabled', 'disabled');
            $("#dvModal").find('textarea').attr('disabled', 'disabled');
            $("#dvModal").find('select').attr('disabled', 'disabled');
        }
    }

    , validateLayerInfo: function () {
        if ($.trim($('#txtLayerName').val()) == '') {
            dashboard.showMessage('warning', 'Please enter Layer Name', false);
            return false;
        }
        if ($.trim($('#txtLayerLoc').val()) == '') {
            dashboard.showMessage('warning', 'Please enter Layer Location', false);
            return false;
        }
        return true;
    }
    , updateLayerInfo: function () {
        
        var sUrl = $('#hdnSaveLayerUrl').val();
        var lId = $('#hdnSelectedRowId').val();
     
        var layerName = $.trim($('#txtLayerName').val());
        var layerLoc = $.trim($('#txtLayerLoc').val());
      //  var isActive = $("input[name='rdActive']:checked").val();
        var createdBy = $("#hdnCreatedBy").val();
        var createdDate = $("#hdnCreatedDate").val();


        var rec = {
            Id: lId,
            AppLayerName: layerName,
            LayerLocation: layerLoc,
        //    IsActive: isActive,
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
                        layerInfoCRUD.searchResult();
                        layerInfoCRUD.refreshLayerInfoCtrl();
                        if (lId == 0 || lId == '') {
                            dashboard.showMessage('success', 'Layer information added successfully', true);

                        } else {                            
                            dashboard.showMessage('success', 'Layer information updated successfully', true);
                        }

                    }
                    else if (data != null && data.RecStatus == "Duplicate") {
                        var sDupMsg = 'Layer Name and Location already exists. Please enter any other layer detail.';
                        dashboard.showMessage('warning', sDupMsg, false);
                    }
                    else {
                        dashboard.showMessage('danger', 'Validation failed.', false);
                    }
                }
                
            },
            error: function (data, status, jqXHR) {

                layerInfoCRUD.clearBAOInfo();
                dashboard.showMessage('danger', 'Save failed.Please try again.', false);
            }
        });

    }

    , refreshLayerInfoCtrl: function () {
        layerInfoCRUD.clearLayerInfo();        
    }   

    , clearLayerInfo: function () {
        $('#hdnSelectedRowId').val('');  
        $('#txtLayerName').val('');
        $('#txtLayerLoc').val('');
        $('#rdNo').removeAttr('checked');
        $("#rdYes").attr('checked', 'checked');
    }

  

    , confirm_deleteLayer: function (layerId) {
        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this Layer!",
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
                layerInfoCRUD.deleteLayer(layerId);
            }
        });
    }

    , deleteLayer: function (layerId) {
        var sUrl = $('#hdnLayerDeleteUrl').val();
        var rec = {
            lId: layerId
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
                        layerInfoCRUD.searchResult();
                        dashboard.showMessage('success', 'Layer deleted successfully', true);
                    }
                    else if (data != null && data.RecStatus == "Not Deleted") {
                        var sDupMsg = 'We could not delete this layer since it is used in Client Apps. Please delete them and try again later.';
                        dashboard.showMessage('warning', sDupMsg, false);
                    }
                }
            },
            error: function (data, status, jqXHR) {
                console.log(jqXHR);
                dashboard.showMessage('danger', 'Failed to delete Layer details. Please try again.', false);
            }
        });
    }

     
    //End of Namespace
}



