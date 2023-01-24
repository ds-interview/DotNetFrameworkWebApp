(function ($) {


    function JobList() {
        var $this = this;

        function initializeGrid() {

            var gridPresenter = new Global.GridHelper('#grid-job-management', {
                "columnDefs": [
                    {
                        "targets": [0],
                        "visible": false,
                        "searchable": false
                    },
                    {
                        "targets": [1],
                        "visible": true,
                        "sortable": true,
                        "searchable": true
                    },
                    {
                        "targets": [2],
                        "visible": true,
                        "sortable": true,
                        "searchable": true
                    },
                    {
                        "targets": [3],
                        "visible": true,
                        "sortable": false,
                        "searchable": false
                    },
                    {
                        "targets": [4],
                        "visible": true,
                        "sortable": false,
                        "searchable": false
                    },
                    {
                        "targets": [5],
                        "visible": true,
                        "sortable": false,
                        "searchable": false
                    },
                    
                    {

                        "targets": 6,
                        "searchable": false,
                        "sortable": false,
                        "data": "0",
                        "render": function (data, type, row, meta) {

                            var actionLink = $("<a/>", {
                                href: domain + "/JobDetails/AddEditJobDetails/" + row[0],
                                id: "editPresenterModal",
                                class: "btn btn-primary btn-sm",
                                'data-toggle': "modal",
                                'data-target': "#modal-add-edit-jobdetails",
                                html: $("<i/>", {
                                    class: "fa fa-pencil"
                                }),
                            }).append(" Edit").get(0).outerHTML + "&nbsp;"


                            actionLink += $("<a/>", {
                                href: domain + "/JobDetails/DeleteJobDetails/" + row[0],
                                id: "deletePresenter",
                                class: "btn btn-danger btn-sm",
                                'data-toggle': "modal",
                                'data-target': "#modal-delete-jobdetails",
                                html: $("<i/>", {
                                    class: "fa fa-trash-o"
                                }),
                            }).append(" Delete").get(0).outerHTML + "&nbsp;"


                            return actionLink;
                        }
                    }
                ],
                "direction": "rtl",
                "bPaginate": true,
                "sPaginationType": "simple_numbers",
                "bProcessing": true,
                "bServerSide": true,
                "bAutoWidth": false,
                "stateSave": false,
                "sAjaxSource": domain + "JobDetails/Index",   //some changes here //

                "fnServerData": function (url, data, callback) {

                    $.ajax({
                        "url": url,
                        "data": data,
                        "success": callback,
                        "contentType": "application/x-www-form-urlencoded; charset=utf-8",
                        "dataType": "json",
                        "type": "POST",
                        "cache": false,
                        "error": function () {

                        }
                    });


                },


                "fnDrawCallback": function (oSettings) {
                    initGridControlsWithEvents();


                    if (oSettings._iDisplayLength > oSettings.fnRecordsDisplay()) {
                        $(oSettings.nTableWrapper).find('.dataTables_paginate').hide();
                    }
                    else {
                        $(oSettings.nTableWrapper).find('.dataTables_paginate').show();
                    }
                },
                "stateSave": true,
                "stateSaveCallback": function (settings, data) {
                    localStorage.setItem('DataTables_' + settings.sInstance, JSON.stringify(data))
                },
                "stateLoadCallback": function (settings) {
                    return JSON.parse(localStorage.getItem('DataTables_' + settings.sInstance))
                }
            });
            table = gridPresenter.DataTable();

            $('.dataTables_filter input').attr("placeholder", "Press and type Enter");  ///code for filter serch placeholder

        }




        function initGridControlsWithEvents() {
            if ($('.switchBox').data('bootstrapSwitch')) {
                $('.switchBox').off('switchChange.bootstrapSwitch');
                $('.switchBox').bootstrapSwitch('destroy');
            }

            $('.switchBox').bootstrapSwitch()
                .on('switchChange.bootstrapSwitch', function () {
                    var switchElement = this;

                    $.post(domain + 'Admin/Brand/UpdateStatus', { brandId: this.value },
                        function (result) {
                            alertify.dismissAll();
                            alertify.success(result.message)
                        })
                });
        }

        ///chh

        function initializeModalWithForm() {


            //Add And Edit Circular


            $("#modal-add-edit-jobdetails").on('loaded.bs.modal', function (e) {

                $attendanceValue = [];
                $('.form-checkbox').bootstrapSwitch();
                formAddEditDepartment = new Global.FormHelper($("#modal-add-edit-jobdetails"), { updateTargetId: "validation-summary" }, function (data) {

                    console.log(data.isSuccess); //Here
                    if (data.isSuccess == true) {
                        $("#validation-summary").html("");
                        $("#validation-summary").hide();
                        window.location.href = data.redirectUrl;
                        window.location.reload();
                    }
                    else {
                        //$("#validation-summary").show();
                        $("#validation-summary").text(data.data).show().delay(5000).fadeOut(2000);
                    }
                });

                $('.datefield').datepicker({
                    dateFormat: 'dd-mm-yy',
                    autoclose: true,
                    minDate: new Date()
                }).on('change', function (e) {

                });

            }).on('hidden.bs.modal', function (e) {
                $("#modal-add-edit-jobdetails").find(".modal-content").html("");
                $(this).removeData('bs.modal');
            });





            $("#modal-delete-jobdetails").on('loaded.bs.modal', function (e) {

                formDeleteJobTitle = new Global.FormHelper($('#frm-add-edit-jobdetails'), { updateTargetId: "validation-summary" }, function (data) {
                    if (data.isSuccess) {
                        // alert(data.data);
                        window.location.reload();
                    }
                    else {
                        alert(data.data);
                    }
                });
            }).on('hidden.bs.modal', function (e) {
                $("#modal-delete-jobdetails").find(".modal-content").html("");
                $(this).removeData('bs.modal');
            });


        }


        $this.init = function () {
            initializeGrid();
            initializeModalWithForm();
        };
    }


    $(function () {
        var self = new JobList();
        self.init();
    });



}(jQuery));