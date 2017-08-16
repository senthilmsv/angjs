UserDetailCRUD = {        
   
    //Page Related

    searchResult: function () {      
        $('#dvLoadModal').modal('show');
        $.ajax({
            url: $('#uSearchUserList').val(),
            type: 'GET',
            cache: false,
            contentType: 'text/json',
            success: function (data) {
                if (data != null) {
                    UserDetailCRUD.loadUserList(data);
                    $('#dvLoadModal').modal('hide');
                }
            },
            error: function (data, status, jqXHR) {
                $('#dvLoadModal').modal('hide');
                dashboard.showMessage('Danger', 'Search failed. Please try again', false);
            }
        });
    }

, loadUserList: function (data) {
    var userType = $('#hdnUserType').data('value');

    $('#hiUserDetail').dataTable().fnDestroy();

    $('#hiUserDetail').dataTable({
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
                  "sWidth": "5%",
                  "mDataProp": null,
                  "bSortable": false,
                  //"mRender": function (o) { return '<a href="#" class="viewUserInfo" data-userid=' + o.Id + '>' + 'View' + '</a> '; }
                  "mRender": function (o) {
                      if (userType == "A") {
                          return '<div style="text-align:center;"><table><tr><td> <a href="#" class="viewUserInfo" data-userid=' + o.Id + '>' + '<i class="fa fa-bars" title="View"></i>' + '</a></td>'
                          + '<td>&nbsp;&nbsp;</td><td><a href="#" class="viewUserInfo" data-userid=' + o.Id + '>' + '<i class="fa fa-edit" title="Edit"></i>' + '</a></td>'
                          + '<td>&nbsp;&nbsp;</td><td><a href="#" class="viewUserInfo" data-userid=' + o.Id + '>' + '<i class="fa fa-trash" title="Delete"></i>' + '</a></td></tr></table> </div>';
                      }
                      else {
                          return '<div style="text-align:center;"> <a href="#" class="viewUserInfo" data-userid=' + o.Id + '>' + '<i class="fa fa-bars" title="View"></i>' + '</a>';
                      }
                  }
              },
               //{
               //    "sWidth": "5%",
               //    "mDataProp": null,
               //    "bSortable": false,
               //    "mRender": function (o) { return '<a href="#" class="viewUserInfo" data-userid=' + o.Id + '>' + 'Edit' + '</a> '; }
               //},
               //{
               //    "sWidth": "4%",
               //    "mDataProp": null,
               //    "bSortable": false,
               //    "mRender": function (o) { return '<a href="#" class="viewUserInfo" data-userid=' + o.Id + '>' + 'Delete' + '</a>'; }
               //},
                 {
                     "mDataProp": "UserName",
                     //"sWidth": "20%",
                     "bAutoWidth": true,
                     "mRender": function (data, type, full) {
                         return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;

                     }
                 },
                {
                    "mDataProp": "NUID",
                    //"sWidth": "10%"
                    "bAutoWidth": true,
                },
                {
                    "mDataProp": "EmailID" ,
                    //"sWidth": "30%",
                    "bAutoWidth": true,
                    "mRender": function (data, type, full) {
                        return type == 'display' && data.length > 50 ? '<span title=\"' + data + '\">' + data.substr(0, 50) + '...</span>' : data;

                    }
                },
                {
                    "mDataProp": "UserType" ,
                    //"sWidth": "10%",
                    "bAutoWidth": true,
                    "mRender": function (data, type, full) {
                        return data == 'A' ? 'Admin' : 'General';

                    }
                },
                {
                    "mDataProp": "IsActive",
                    //"sWidth": "10%",
                    "bAutoWidth": true,
                    "mRender": function (data, type, full) {
                        return data.toLowerCase() == 'yes' ? 'Yes' : 'No';
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
            UserDetailCRUD.initOnClickEvents();
        }       
      
    });
    //if (userType == "G") {
    //    var table = $('#hiUserDetail').DataTable();
    //    // Hide edit & delete column to General User
    //    table.column(1).visible(false);
    //    table.column(2).visible(false);
    //}     

}
    
    ,reset_datatable_state: function () {
        localStorage.removeItem('DataTables_hiUserDetail_' + window.location.pathname);   
    }
    , initOnClickEvents: function () {
        //on Each Row click
        $(".viewUserInfo").unbind();
        $(".viewUserInfo").on('click', (function () {
            var clickedIcon = $(this).find('i')[0];
            if (clickedIcon != null) {
                if (clickedIcon.title == "Delete") {
                    var userType = $('#hdnUserType').data('value');
                    if (userType == "A") {
                        UserDetailCRUD.confirm_deleteUser($(this).data("userid"));
                    }
                }
                else {
                    UserDetailCRUD.viewUserDeatil($(this).data("userid"), clickedIcon.title)
                }
            }
           
        }));

        $("#btnUserDetailAdd").unbind();
        $("#btnUserDetailAdd").on('click', function () {
            UserDetailCRUD.viewUserDeatil(0, "Add");
        });

     
    }

    , viewUserDeatil: function (userId, mode) {
        //on Each Row click
        var record = {
            userId: userId,
            actionMode: mode
        };
        $.ajax({
            //Calling Partial View
            url: $("#uViewUserDeatil").val(),
            type: 'POST',
            data: record,
            async: true,
            success: function (data) {                
                if (data != null) {
                    $('#dvMdlDlgContent').html(data);
                    $("#dvModal").modal();
                    UserDetailCRUD.showHideButtons();
                }
            },
            error: function (data, status, jqXHR) {      
                dashboard.showMessage('Danger', 'Error in view User Detail', false);
            }
        });
        
        return false;
    }

    , showHideButtons: function () {
        if ($("#hdnUserDetailAction").val() == "View") {
            $("#btnUserDetailUpdate").hide();
        }
        else if ($("#hdnUserDetailAction").val() == "Edit") {
            $("#btnUserDetailUpdate").on("click", function () {
                if (UserDetailCRUD.validateUser()) {
                    UserDetailCRUD.updateUserDetail();
                }
               
            });          
        }
        else if ($("#hdnUserDetailAction").val() == "Add") {
            $("#btnUserDetailUpdate").text("Add User");
            $("#btnUserDetailUpdate").on("click", function () {
                if (UserDetailCRUD.validateUser()) {
                    UserDetailCRUD.updateUserDetail();
                }                
            });   
        }
        else {
            $("#btnUserDetailUpdate").hide();
        }

        if ($("#hdnUserDetailAction").val() == "View") {
            $("#dvModal").find('input:text').attr('disabled', 'disabled');
            $("#dvModal").find('input:checkbox').attr('disabled', 'disabled');
            $("#dvModal").find('input:radio').attr('disabled', 'disabled');
            $("#dvModal").find('textarea').attr('disabled', 'disabled');
            $("#dvModal").find('select').attr('disabled', 'disabled');
        }
    }

    , validateUser: function () {
        if ($.trim($('#txtUserNUID').val()) == '') {
            dashboard.showMessage('warning', 'Please enter User NUID',false);
            return false;
        }
        if (!UserDetailCRUD.isValidNuid($.trim($('#txtUserNUID').val()))) {
            dashboard.showMessage('warning', 'Please enter valid User NUID', false);
            return false;
        }
        if ($.trim($('#txtUserName').val()) == '') {
            dashboard.showMessage('warning', 'Please enter User Name',false);
            return false;
        }
        if ($.trim($('#txtUserEmail').val()) == '') {
            dashboard.showMessage('warning', 'Please enter User Email',false);
            return false;
        }       
       
        if (!UserDetailCRUD.validateEmail($.trim($('#txtUserEmail').val()))) {
            dashboard.showMessage('warning', 'Please enter valid User Email', false);
            return false;
        }
        
        return true;
    }
    , updateUserDetail: function () {
        
        var sUrl = $('#hdnSaveUserUrl').val();
        var userId = $('#hdnSelectedRowId').val();
     
        var userNUID = $.trim($('#txtUserNUID').val());
        var userName = $.trim($('#txtUserName').val());
        var userEmail = $.trim($('#txtUserEmail').val());
        var userType = $("input[name='rdUserType']:checked").val();
        var isActive = $("input[name='rdActive']:checked").val();
        var createdBy = $("#hdnCreatedBy").val();
        var createdDate = $("#hdnCreatedDate").val();

        //var validDomain = "kp.org";
        //var pattern = new RegExp("^[A-Za-z0-9_\\-\\.]+@@" + validDomain + "$");
        //var pattern = new RegExp("^[A-Za-z0-9_\\-\\.]+@@" + validDomain + "$", 'i');
        //var msg = "";

      
        var rec = {
            Id: userId,
            NUID: userNUID,
            UserName: userName,
            EmailID: userEmail,
            UserType: userType,
            IsActive: isActive,
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
                        UserDetailCRUD.searchResult();
                        UserDetailCRUD.refreshUserDetailCtrl();
                        if (userId == 0 || userId == '') {
                            dashboard.showMessage('success', 'User Detail added successfully', true);

                        } else {
                            dashboard.showMessage('success', 'User Detail updated successfully', true);
                        }

                    }
                    else if (data != null && data.RecStatus == "DuplicateNUID") {
                        var sDupMsg = 'User NUID already exists. Please enter any other User NUID.';
                        dashboard.showMessage('warning', sDupMsg, false);
                    }
                    else if (data != null && data.RecStatus == "DuplicateEmail") {
                        var sDupMsg = 'User Email already exists. Please enter any other User Email.';
                        dashboard.showMessage('warning', sDupMsg, false);
                    }
                    else {
                        dashboard.showMessage('danger', 'Validation failed.', false);
                    }
                }
                
            },
            error: function (data, status, jqXHR) {

                UserDetailCRUD.refreshUserDetailCtrl();
                dashboard.showMessage('danger', 'Save failed.Please try again.', false);
            }
        });

    }

    , refreshUserDetailCtrl: function () {
        UserDetailCRUD.clearUserDetail();
    }   

    , clearUserDetail: function () {
        $('#hdnSelectedRowId').val('');  
        $('#txtLayerName').val('');
        $('#txtLayerLoc').val('');
        $('#rdNo').removeAttr('checked');
        $("#rdYes").attr('checked', 'checked');
    }

  

    , confirm_deleteUser: function (userId) {
        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this User!",
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
                UserDetailCRUD.deleteUser(userId);
            }
        });
    }

    , deleteUser: function (userId) {
        var sUrl = $('#hdnUserDeleteUrl').val();
        var rec = {
            uId: userId
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
                        UserDetailCRUD.searchResult();
                        dashboard.showMessage('success', 'User deleted successfully', true);
                    }
                }
            },
            error: function (data, status, jqXHR) {
                console.log(jqXHR);
                dashboard.showMessage('danger', 'Failed to delete User detail. Please try again.', false);
            }
        });
    }

      , validateEmail: function (email) {
          var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
          return emailReg.test(email);
      }
    ,isValidNuid: function(nuid) {
        var pattern;
        if (nuid.substr(-1).toLowerCase() == 'a') {
            pattern = new RegExp(/^[a-zA-Z][0-9]{6}[a-zA-Z]$/);
        }
        else {
            pattern = new RegExp(/^[a-zA-Z][0-9]{6}$/);
        }
  return pattern.test(nuid);
  }

    //End of Namespace
}



