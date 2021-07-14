define(['_MasterPageController', '_MenuPageController', '_DetailsModalController', 'datatables-bootstrap', 'bootstrap-select'], function (module, menu, modal) {
    var PeriodicMaintenance = (function () {
        var app = angular.module('PeriodicMaintenancePage', [])
        app.controller('PeriodicMaintenanceController', function ($scope) {
            var Main = module.Main
            var Languages = module.Languages
            var InfoDetails = modal
            var Loading = module.Loading
            var Alert = module.Alert
            var MainMenu = menu
            var table
            var menuID = 'mnuPeriodicMaintenance'
            var currentNamePage = 'MasterPlanWeb'
            var buttonFloat = [
                {
                    "id": "btnMain",
                    "url": "#",
                    "icon": "<i class='fa fa-angle-double-up'></i>"
                },
                {
                    "id": "btnCreateWO",
                    "url": "#",
                    "icon": "<i class='fa fa-recycle'></i>",
                    "lang": "btnCreateWO",
                    "func": "fn.CreateWO"
                },
                {
                    "id": "btnKHTT",
                    "url": "#",
                    "icon": "<i class='fa fa-pencil-square-o'></i>",
                    "lang": "btnKHTT",
                    "func": "fn.CreatePlans"
                }
            ]
            var vars = {}
            var bindVariables = function () {
                return {
                    $table: 'undefined',
                    $tbPeriodicMaintenance: $('#tbPeriodicMaintenance'),
                    $tbPeriodicMaintenanceBody: $('#tbPeriodicMaintenance tbody')
                };
            }
            var fnPrivate = {
                FilterData: function () {
                    $.post(urlFilter, { fromDate: $('#fromDate').val(), toDate: $('#toDate').val(), timeOut: $('#ckTimeOut').is(":checked") ? true : false }, function (data) {
                        if (data.length > 0) {
                            if ($.fn.DataTable.isDataTable('#tbPeriodicMaintenance')) {
                                vars.$tbPeriodicMaintenance.dataTable().fnClearTable();
                                vars.$tbPeriodicMaintenance.dataTable().fnDestroy();
                            }
                            vars.$table = vars.$tbPeriodicMaintenance.DataTable({
                                data: data,
                                columns: [
                                    { data: 'Choose' },
                                    { data: 'DeviceID' },
                                    { data: 'DeviceName' },
                                    { data: 'TypeOfMaintenanceName' },
                                    { data: 'LastDate' },
                                    { data: 'NextDate' },
                                    { data: 'RuntimeCycle' },
                                    { data: 'CurrentRunTime' },
                                    { data: 'DeviceGroupName' },
                                    { data: 'PriorityID' }
                                ],
                                "language":
                                {
                                    "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>",
                                    "sSearch": "<span data-lang='lblSearch'>" + (global.TypeLanguage == 0 ? "Tìm " : "Search") + "</span> ",
                                    "info": "",
                                    "zeroRecords": "<span data-lang='lblFilterInfo'>" + (global.TypeLanguage == 0 ? "Không tìm thấy" : "No matching records found") + "</span>",
                                    "lengthMenu": "<span data-lang='lblShow'></span> _MENU_ <span data-lang='lblEntries'></span>",
                                    "infoEmpty": "",
                                    "infoFiltered": "",
                                    "paginate": {
                                        "first": "<<",
                                        "last": ">>",
                                        "next": ">",
                                        "previous": "<"
                                    },
                                    "emptyTable": "<span data-lang='lblEmpty'>" + (global.TypeLanguage == 0 ? "Không có dữ liệu" : "No data available in table") + "</span>",
                                },
                                columnDefs: [
                                    {
                                        'targets': 0,
                                        'width': '90px',
                                        'searchable': false,
                                        'orderable': false,
                                        'className': 'dt-body-center',
                                        'data-toggle': "buttons",
                                        'render': function (data, type, full, meta) {
                                            return '<input type="checkbox" />';
                                        }
                                    },
                                    { 'width': '180px', 'targets': 1 },
                                    { 'width': '180px', 'targets': 2 },
                                    { 'width': '200px', 'targets': 3 },
                                    { 'width': '100px', 'targets': 4 },
                                    { 'width': '100px', 'targets': 5 },
                                    { 'width': '100px', 'targets': 6 },
                                    { 'width': '100px', 'targets': 7 },
                                    { 'width': '100px', 'targets': 8 },
                                    { 'width': '100px', 'targets': 9 },
                                ],
                                "scrollY": 350,
                                "scrollX": true,
                                "createdRow": function (row, data, dataIndex) {
                                    if (data.hasOwnProperty("DeviceID")) {
                                        $(row).attr('data-id', data.DeviceID);
                                        $(row).attr('data-typeofmaintenance', data.TypeOfMaintenanceID);
                                    }
                                },
                            });
                            $($.fn.dataTable.tables(true)).DataTable().columns.adjust().draw(false);
                        }
                        else {
                            vars.$table.rows().remove().draw();
                        }
                        Loading.fn.Hide();
                    });
                },
                ShowDetailAboutPeriodicMaintenance: function () {
                    $(this).each(function () {
                        if (!this.rowIndex) return;
                        InfoDetails.fn.Show($('#tbPeriodicMaintenance thead tr th'), this.cells);
                        return false;
                    });
                }
            }

            var method
            $scope.fn = {
                Init: function () {
                    global.CurrentNamePage = currentNamePage
                    MainMenu.fn.SetActive(menuID);
                    Languages.fn.AutoChangeLanguage()
                    method = fnPrivate
                    //init variables
                    vars = bindVariables();
                    vars.$tbPeriodicMaintenanceBody.on('dblclick doubletap', 'tr', method.ShowDetailAboutPeriodicMaintenance)
                    Main.fn.InitButtonFloat(buttonFloat)
                    Main.fn.InitDateTimePickerChanged([$('#fromDate'), $('#toDate')], method.FilterData)
                },
                CreatePlans: function () {
                    var keys = vars.$table.$('input[type="checkbox"]:checked')
                    if (keys.length == 0) {
                        Alert.fn.Show(Messenger.msgChonDevice, Alert.Type.info)
                        return;
                    }
                    var lstPeriodicMaintenance = new Array();
                    var j = 0;
                    vars.$table.$('input[type="checkbox"]:checked').each(function () {
                        var row = $(this).closest('tr').get(0);
                        $(row).each(function () {
                            lstPeriodicMaintenance[j] = new Object();
                            lstPeriodicMaintenance[j].DeviceID = row.cells[1].textContent;
                            lstPeriodicMaintenance[j].TypeOfMaintenanceID = $(row).attr('data-typeofmaintenance');
                            lstPeriodicMaintenance[j].DueDate = row.cells[5].textContent.split('/')[2] + '/' + row.cells[5].textContent.split('/')[1] + '/' + row.cells[5].textContent.split('/')[0];
                            lstPeriodicMaintenance[j].DateCreated = row.cells[5].textContent.split('/')[2] + '/' + row.cells[5].textContent.split('/')[1] + '/' + row.cells[5].textContent.split('/')[0];
                            lstPeriodicMaintenance[j].Name = global.TypeLanguage == 0 ? "Chuyển từ BTPN" : "From PM";
                            lstPeriodicMaintenance[j].ReasonForRepairID = 2;
                            lstPeriodicMaintenance[j].DateCompleted = row.cells[5].textContent.split('/')[2] + '/' + row.cells[5].textContent.split('/')[1] + '/' + row.cells[5].textContent.split('/')[0];;
                            lstPeriodicMaintenance[j].OriginalStartDate = row.cells[5].textContent.split('/')[2] + '/' + row.cells[5].textContent.split('/')[1] + '/' + row.cells[5].textContent.split('/')[0];
                            lstPeriodicMaintenance[j].Username = '';
                            lstPeriodicMaintenance[j].PriorityID = row.cells[9].textContent;
                            lstPeriodicMaintenance[j].MonitoredBy = $('#cboMonitoredBy').val();
                            lstPeriodicMaintenance[j].ExecutedBy = $('#cboCreatedBy').val();
                        });
                        j++
                    });
                    $.post(urlSaveMasterPlan, { data: JSON.stringify(lstPeriodicMaintenance) }, function (data) {
                        if (data == "success") {
                            vars.$table.$('input[type="checkbox"]:checked').each(function () {
                                var row = $(this).closest('tr').get(0);
                                vars.$table.row(row).remove().draw(false)
                            });
                            Alert.fn.Show(Messenger.msgGhiThanhCong, Alert.Type.success)
                        }
                        else {
                            Alert.fn.Show(Messenger.msgKhongThanhCong + ' (' + data + ')', Alert.Type.error)
                        }
                    });
                },
                CreateWO: function () {
                    var keys = vars.$table.$('input[type="checkbox"]:checked')
                    if (keys.length == 0) {
                        Alert.fn.Show(Messenger.msgChonDevice, Alert.Type.info)
                    }
                    var lstPeriodicMaintenance = new Array();
                    var j = 0;
                    vars.$table.$('input[type="checkbox"]:checked').each(function () {
                        var row = $(this).closest('tr').get(0);
                        $(row).each(function () {
                            lstPeriodicMaintenance[j] = new Object();
                            lstPeriodicMaintenance[j].WorkOrderID = '';
                            lstPeriodicMaintenance[j].DeviceID = row.cells[1].textContent;
                            lstPeriodicMaintenance[j].TypeOfMaintenanceID = $(row).attr('data-typeofmaintenance');
                            lstPeriodicMaintenance[j].DateCreated = '1900/01/01 00:00'
                            lstPeriodicMaintenance[j].TimeCreated = '1900/01/01 00:00'
                            lstPeriodicMaintenance[j].DueDate = row.cells[5].textContent.split('/')[2] + '/' + row.cells[5].textContent.split('/')[1] + '/' + row.cells[5].textContent.split('/')[0];
                            lstPeriodicMaintenance[j].Reason = row.cells[3].textContent;
                            lstPeriodicMaintenance[j].TheStatusOfWorkOrderID = 1;
                            lstPeriodicMaintenance[j].StartDatePlan = row.cells[5].textContent.split('/')[2] + '/' + row.cells[5].textContent.split('/')[1] + '/' + row.cells[5].textContent.split('/')[0];;
                            lstPeriodicMaintenance[j].EndDatePlan = row.cells[5].textContent.split('/')[2] + '/' + row.cells[5].textContent.split('/')[1] + '/' + row.cells[5].textContent.split('/')[0];
                            lstPeriodicMaintenance[j].UserCreated = '';
                            lstPeriodicMaintenance[j].PriorityID = row.cells[9].textContent;
                            lstPeriodicMaintenance[j].MonitoredBy = $('#cboMonitoredBy').val();
                            lstPeriodicMaintenance[j].CreatedBy = $('#cboCreatedBy').val();
                        });
                        j++
                    });
                    $.post(urlSaveWO, { data: JSON.stringify(lstPeriodicMaintenance) }, function (data) {
                        if (data == "success") {
                            vars.$table.$('input[type="checkbox"]:checked').each(function () {
                                var row = $(this).closest('tr').get(0);
                                vars.$table.row(row).remove().draw(false)
                            });
                            Alert.fn.Show(Messenger.msgGhiThanhCong, Alert.Type.success)
                        }
                        else {
                            Alert.fn.Show(Messenger.msgKhongThanhCong + ' (' + data + ')', Alert.Type.error)
                        }
                    });
                }
            }
        })
        app.init = function () {
            angular.bootstrap(document, ['PeriodicMaintenancePage'])
        }
        return app;
    })();
    $(function () {
        PeriodicMaintenance.init()
    });
})