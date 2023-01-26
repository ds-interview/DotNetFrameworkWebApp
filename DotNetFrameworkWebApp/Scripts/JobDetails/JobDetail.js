(function ($) {

    function JobDetailsList() {
        var $this = this;

        function initializeModalWithForm() {

            $("#modal-add-edit-jobdetails").on('loaded.bs.modal', function (e) {
                formAddEditJobDetails = new Global.FormHelper($(this).find("form"),
                    { updateTargetId: "validation-summary" }, function onSuccess(result) {
                        if (result.errorMessage) {
                            alertify.error(result.errorMessage)
                        }
                        else {
                            window.location.href = result.redirectUrl;
                        }
                    });
                $('.form-checkbox').bootstrapSwitch();


            }).on('hidden.bs.modal', function (e) {
                $(this).removeData('bs.modal');
            });


        }


        function initializeGrid() {
            if ($.fn.DataTable.isDataTable($this.gridJobDetails)) {
                $($this.gridJobDetails).DataTable().destroy();
            }
            $this.gridJobDetails = new Global.GridHelper('#grid-jobdetails-management', {
                "columnDefs": [
                    {
                        "targets": [0],
                        "visible": false,
                        "sortable": false,
                        "orderable": false,
                    },
                    {
                        "targets": [1],
                        "visible": true,
                        "sortable": false,
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
                        "sortable": true,
                        "searchable": true
                    },
                    {
                        "targets": [4],
                        "visible": true,
                        "sortable": true,
                        "searchable": true
                    },

                    {
                        "targets": [5],
                        "visible": true,
                        "sortable": true,
                        "searchable": true
                    },
                    {
                        "targets": 6,
                        "data": "0",
                        "visible": true,
                        "searchable": false,
                        "sortable": false,
                        "render": function (data, type, row, meta) {
                            var actionLink = ''
                            actionLink = $("<a/>", {
                                href: 'http://localhost:50175/' + "/JobDetails/AddEditJobDetails/" + row[0],

                                id: "addeditPresenterModal",
                                class: "btn btn-primary btn-sm",
                                'title': "Edit",
                                'data-toggle': "modal",
                                'data-backdrop': "static",
                                'data-target': "#modal-add-edit-jobdetails",
                                html: $("<i/>", {
                                    class: "fa fa-pencil"
                                }),
                            }).append(" Edit").get(0).outerHTML + "&nbsp";

                            actionLink += $("<a/>", {
                                href: 'http://localhost:50175/' + "/JobDetails/DeleteJobDetails/" + row[0],
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
                    },

                ],
                "direction": "rtl",
                "bPaginate": true,
                "sPaginationType": "full_numbers",
                "bProcessing": true,
                "bServerSide": true,
                "bAutoWidth": false,
                "stateSave": false,
                "sAjaxSource": 'http://localhost:50175/' + "JobDetails/Index",
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
                        window.scrollTo(0, 0);
                    }
                },

                "stateSaveCallback": function (settings, data) {
                    localStorage.setItem('DataTables_' + settings.sInstance, JSON.stringify(data))
                },
                "stateLoadCallback": function (settings) {
                    return JSON.parse(localStorage.getItem('DataTables_' + settings.sInstance))
                }
            });
            table = $this.gridJobDetails.DataTable();
            $('.dataTable').on('draw.dt', function () {
                bindEnterEventInDataTableJSGrid(true);
            });
        }

        function initGridControlsWithEvents() {
            if ($('.switchBox').data('bootstrapSwitch')) {
                $('.switchBox').off('switchChange.bootstrapSwitch');
                $('.switchBox').bootstrapSwitch('destroy');
            }

        }


        $this.init = function () {
            initializeModalWithForm();
            initializeGrid();
            

        };
    }
    $(function () {
        var self = new JobDetailsList();
        self.init();
    });
}(jQuery));

