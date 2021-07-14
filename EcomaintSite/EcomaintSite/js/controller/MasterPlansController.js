define(['_MasterPageController', '_MenuPageController', '_DetailsModalController', 'datatables-bootstrap', 'bootstrap-select'], function (module, menu, modal) {
    var MasterPlans = (function () {
        var app = angular.module('MasterPlansPage', [])
        app.controller('MasterPlansController', function ($scope, $compile) {
            var Main = module.Main
            var Languages = module.Languages
            var InfoDetails = modal
            var Loading = module.Loading
            var Alert = module.Alert
            var MainMenu = menu
            var Convert = module.Convert
            var BuildTreeView = module.BuildTreeView
            var menuID = 'mnuListMasterPlan'
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
                    "id": "btnAdd",
                    "url": "#",
                    "icon": "<i class='fa fa-plus-circle'></i>",
                    "lang": "btnAdd",
                    "func": "fn.Add"
                },
                {
                    "id": "btnEdit",
                    "url": "#",
                    "icon": "<i class='fa fa-pencil'></i>",
                    "lang": "btnEdit",
                    "func": "fn.Edit"
                },
                {
                    "id": "btnRemove",
                    "url": "#",
                    "icon": "<i class='fa fa-trash'></i>",
                    "lang": "btnRemove",
                    "func": "fn.Remove"
                }
            ]
            var Action = {
                add: 0, edit: 1
            }
            var vars = {}
            var bindVariables = function () {
                return {
                    $action: Action.add,
                    $table: 'undefined',
                    $tableWork: 'undefined',
                    $tableWorkDaChon: 'undefined',
                    $tableSparePart: 'undefined',
                    $tvDataChangedFlag: false,
                    $dataSparePartSelected: new Array(),
                    $tbPlan: $('#tbPlan'),
                    $tbPlanBody: $('#tbPlan tbody'),
                    $cboDevice: $("#cboDevice"),
                    $cboPriority: $('#cboPriority'),
                    $btnSavePlan: $('#btnSavePlan'),
                    $btnSaveWO: $('#btnSaveWO'),
                    $tbWorkSelectedBody: $('#tbWorkSelected tbody'),
                    $tvComponent: $('#tvComponent'),
                    $tbSparePartSelectedBody: $('#tbSparePartSelected tbody'),
                    $tbSparePartSelected: $('#tbSparePartSelected'),
                    $tbWorkSelecting: $("#tbWorkSelecting"),
                    $tbWorkSelected: $('#tbWorkSelected'),
                    //$tbPlan: $("#tbPlan"),
                    $tbWorkBody: $('#tbWork tbody'),
                    $tbWorkSelectingBody: $("#tbWorkSelecting tbody"),
                    $tabContents: $('#tabContentModal a[data-toggle="tab"]'),
                    $tabWorkDetail: $('a[href="#tabWorkDetail"]'),
                    $tabContentsPlan: $('#tabContentsPlan a[data-toggle="tab"]')
                };
            }
            var method = 'undefined'
            var fnPrivate = {
                FilterData: function () {
                    $.post(urlFilter, { fromDate: $('#fromDate').val(), toDate: $('#toDate').val(), option: $('#cboOption').val() }, function (data) {
                        if (data.length > 0) {
                            if ($.fn.DataTable.isDataTable('#tbPlan')) {
                                vars.$tbPlan.dataTable().fnDestroy();
                            }
                            vars.$table = vars.$tbPlan.DataTable({
                                data: data,
                                columns: [
                                    { data: 'Choose' },
                                    { data: 'PlanName' },
                                    { data: 'DeviceID' },
                                    { data: 'DeviceName' },
                                    { data: 'DateCreated' },
                                    { data: 'DateCompleted' },
                                    { data: 'TotalHours' },
                                    { data: 'ReasonForRepairID' },
                                    { data: 'PriorityName' },
                                    { data: 'ConfirmedBy' },
                                    { data: 'Note' },
                                    { data: 'TypeOfMaintenanceID' },
                                    { data: 'StaffID' },
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
                                    { 'width': '150px', 'targets': 1 },
                                    { 'width': '150px', 'targets': 2 },
                                    { 'width': '150px', 'targets': 3 },
                                    { 'width': '100px', 'targets': 4 },
                                    { 'width': '100px', 'targets': 5 },
                                    { 'width': '100px', 'targets': 6 },
                                    { 'width': '60px', 'targets': 7 },
                                    { 'width': '100px', 'targets': 8 },
                                    { 'width': '100px', 'targets': 9 },
                                    { 'width': '100px', 'targets': 10 },
                                    { 'visible': false, 'targets': 11 },
                                    { 'visible': false, "targets": 12 },
                                    { 'visible': false, "targets": 13 }
                                ],
                                scrollY: 350,
                                scrollX: true,
                                createdRow: function (row, data, dataIndex) {
                                    if (data.hasOwnProperty("ID")) {
                                        $(row).attr('data-id', data.DeviceID);
                                        $(row).attr('data-typeofmaintenanceid', data.TypeOfMaintenanceID);
                                        $(row).attr('data-planid', data.ID);
                                    }
                                    $('td', row).eq(1).addClass('details')
                                    $('td', row).eq(1).attr('ng-click', 'fn.GetWorkPlan(' + data.ID + ', true)');
                                    $('td', row).eq(7).attr('data-reasonid', data.ReasonForRepairID);
                                    $('td', row).eq(8).attr('data-priorityid', data.PriorityID);
                                    $compile($('td', row))($scope);
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
                ShowDetailAboutMasterPlan: function () {
                    $(this).each(function () {
                        if (!this.rowIndex) return;
                        InfoDetails.fn.Show($('#tbPlan thead tr th'), this.cells);
                        return false;
                    });
                },
                GetMachineParts: function () {
                    var treeview = $('#tvComponent ul');
                    treeview.empty();
                    $.post(urlGetComponentByDevice, { deviceID: vars.$cboDevice.val() }, function (data) {
                        if (data.length > 0) {
                            BuildTreeView.fn.BuildDataSourceForTreeView(treeview, data)
                            if (vars.$action == Action.edit) return;
                            if ($.fn.DataTable.isDataTable('#tbWorkSelecting')) {
                                vars.$tbWorkSelecting.dataTable().fnClearTable();
                                vars.$tbWorkSelecting.dataTable().fnDestroy();
                            }
                            if ($.fn.DataTable.isDataTable('#tbWorkSelected')) {
                                vars.$tbWorkSelected.dataTable().fnClearTable();
                                vars.$tbWorkSelected.dataTable().fnDestroy();
                            }
                            vars.$tbWorkSelectingBody.empty()
                            vars.$tbWorkSelectedBody.empty()
                        }
                    });
                    vars.$tvDataChangedFlag = true
                },
                ChangeSpareParts: function () {
                    if (vars.$tableSparePart == 'undefined') {
                        $.post(urlGetSparePart, function (data) {
                            if (data.length > 0) {
                                if ($.fn.DataTable.isDataTable('#tbSparePartSelected')) {
                                    vars.$tbSparePartSelected.dataTable().fnClearTable();
                                    vars.$tbSparePartSelected.dataTable().fnDestroy();
                                }
                                vars.$tableSparePart = $('#tbSparePartSelected').DataTable({
                                    data: data,
                                    columns: [
                                        { data: 'Choose' },
                                        { data: 'SparePartID' },
                                        { data: 'SparePartName' },
                                        { data: 'Specification' },
                                        { data: 'UnitOfCalculationID' }
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
                                            'width': '100px',
                                            'searchable': false,
                                            'orderable': false,
                                            'className': 'dt-body-center',
                                            'render': function (data, type, full, meta) {
                                                return '<input type="checkbox" />';
                                            }
                                        },
                                        { 'width': '150px', 'targets': 1 },
                                        { 'width': '250px', 'targets': 2 },
                                        { 'width': '150px', 'targets': 3 },
                                        { 'width': '100px', 'targets': 4 }
                                    ],
                                    "lengthChange": false,
                                    "lengthMenu": [5],
                                    scrollY: 145,
                                    scrollX: true,
                                    createdRow: function (row, data, dataIndex) {
                                        if (data.hasOwnProperty("SparePartID")) {
                                            $(row).attr('data-sparepart', data.SparePartID);
                                        }
                                    },
                                });
                                $($.fn.dataTable.tables(true)).DataTable().columns.adjust().draw(false);
                            }
                            else {
                                if ($.fn.DataTable.isDataTable('#tbSparePartSelected')) {
                                    vars.$tbSparePartSelected.dataTable().fnClearTable();
                                    vars.$tbSparePartSelected.dataTable().fnDestroy();
                                }
                                vars.$tableSparePart = 'undefined'
                            }
                        });
                    }
                },
                ChangeMachinesParts: function () {
                    if (vars.$tvDataChangedFlag == false) {
                        method.GetMachineParts();
                    }
                },

                ToggleCheckboxesOnSparePartSelected: function () {
                    if ($(this).prop('checked')) {
                        var i = vars.$dataSparePartSelected.length;
                        vars.$dataSparePartSelected[i] = new Object();
                        vars.$dataSparePartSelected[i].DeviceID = $('#cboDevice').val();
                        vars.$dataSparePartSelected[i].WorkID = vars.$tableWorkDaChon.$('tr[class$="selected"]').attr('data-workid');
                        vars.$dataSparePartSelected[i].ComponentID = vars.$tableWorkDaChon.$('tr[class$="selected"]').attr('data-componentid');
                        vars.$dataSparePartSelected[i].SparePartID = $(this).closest('tr').attr('data-sparepart');
                    }
                    else {
                        console.log(vars.$dataSparePartSelected)
                        var length = vars.$dataSparePartSelected.length;
                        for (var i = 0; i < length; i++) {
                            if (vars.$dataSparePartSelected[i].DeviceID == $('#cboDevice').val() && vars.$dataSparePartSelected[i].WorkID == vars.$tableWorkDaChon.$('tr[class$="selected"]').attr('data-workid') && vars.$dataSparePartSelected[i].ComponentID == vars.$tableWorkDaChon.$('tr[class$="selected"]').attr('data-componentid') && vars.$dataSparePartSelected[i].SparePartID == $(this).closest('tr').attr('data-sparepart')) {
                                vars.$dataSparePartSelected.splice(i, 1)
                                break;
                            }
                        }

                    }
                },
                MasterPlanSave: function () {
                    method.ValidateInput(function (result) {
                        if (result == false) {
                            return false;
                        }
                        $('#myModal').appendTo("body").modal('hide')
                        Loading.fn.Show()
                        var lstPlans = new Array();
                        lstPlans[0] = new Object();
                        lstPlans[0].DeviceID = $('#cboDevice').val()
                        lstPlans[0].PriorityID = vars.$cboPriority.val()
                        lstPlans[0].DateCreated = Convert.fn.DateTime($('#startDatePlanModal').val(), 'yyyy/MM/dd')
                        lstPlans[0].DateCompleted = Convert.fn.DateTime($('#endDatePlanModal').val(), 'yyyy/MM/dd')
                        lstPlans[0].DueDate = Convert.fn.DateTime($('#endDatePlanModal').val(), 'yyyy/MM/dd')
                        lstPlans[0].ReasonForRepairID = $('#cboReason').val()
                        lstPlans[0].Name = $('#txtArticle').val()
                        var planID = $('#tbPlan tr[class$="selected"]').attr('data-planid');
                        if (vars.$action == Action.edit) {
                            lstPlans[0].ID = planID
                        }
                        var lstMasterPlansOfWork = new Array();
                        var i = 0;
                        $('#tbWorkSelected tbody tr').each(function (i, row) {
                            lstMasterPlansOfWork[i] = new Object();
                            lstMasterPlansOfWork[i].DeviceID = $('#cboDevice').val()
                            lstMasterPlansOfWork[i].WorkID = $(this).attr('data-workid')
                            lstMasterPlansOfWork[i].ComponentID = $(this).attr('data-componentid')
                            lstMasterPlansOfWork[i].PriorityID = vars.$cboPriority.val()
                            lstMasterPlansOfWork[i].IsIgnored = false
                            lstMasterPlansOfWork[i].IsOutsource = false
                            if (vars.$action == Action.edit) {
                                lstMasterPlansOfWork[i].PlanID = planID
                            }
                        });
                        if (vars.$action == Action.add) {
                            $.post(urlCreatePlans, { data: JSON.stringify(lstPlans), data2: JSON.stringify(lstMasterPlansOfWork), data3: JSON.stringify(vars.$dataSparePartSelected) }, function (data) {
                                if (data.Message == "success") {

                                    var startDate = Convert.fn.DateTime($('#startDatePlanModal').val(), 'yyyy/MM/dd')
                                    var fromDate = Convert.fn.DateTime($('#fromDate').val(), 'yyyy/MM/dd')
                                    var toDate = Convert.fn.DateTime($('#toDate').val(), 'yyyy/MM/dd')
                                    if (!vars.$table.data().count()) {
                                        method.FilterData()
                                    } else {

                                        if (startDate <= toDate && startDate >= fromDate) {
                                            var dataRow = {
                                                'Choose': '',
                                                'PlanName': lstPlans[0].Name,
                                                'DeviceID': lstPlans[0].DeviceID,
                                                'DeviceName': vars.$cboDevice.selectpicker('text'),
                                                'DateCreated': $('#startDatePlanModal').val(),
                                                'DateCompleted': $('#endDatePlanModal').val(),
                                                'ReasonForRepairID': lstPlans[0].ReasonForRepairID,
                                                'TotalHours': '0',
                                                'PriorityName': vars.$cboPriority.selectpicker('text'),
                                                'ConfirmedBy': '',
                                                'Note': '',
                                                'TypeOfMaintenanceID': '',
                                                'StaffID': '',
                                                'PriorityID': lstPlans[0].PriorityID
                                            }

                                            var rowIndex = vars.$tbPlan.dataTable().fnAddData(dataRow);
                                            var row = vars.$tbPlan.dataTable().fnGetNodes(rowIndex);
                                            $(row).attr('data-planid', data.planID);
                                            $(row).attr('data-id', data.DeviceID);
                                            //$(row).attr('data-typeofmaintenanceid', data.TypeOfMaintenanceID);  
                                            vars.$table.order([4, 'desc']).draw();
                                        }
                                    }
                                    Loading.fn.Hide()
                                    Alert.fn.Show(Messenger.msgSaveSuccess, Alert.Type.success)
                                }
                                else {

                                    Loading.fn.Hide()
                                    Alert.fn.Show(Messenger.msgSaveNotSuccess + ' (' + data + ')', Alert.Type.error)
                                }
                                $('#modalMasterPlansOfWork').modal('hide');
                            });
                        }
                        else {
                            $.post(urlEdit, { data: JSON.stringify(lstPlans), data2: JSON.stringify(lstMasterPlansOfWork), data3: JSON.stringify(vars.$dataSparePartSelected) }, function (data) {
                                if (data == "success") {
                                    var rowIndex = vars.$table.row($('#tbPlan tr[class$="selected"]')).index();
                                    var row = vars.$tbPlan.dataTable().fnGetNodes(rowIndex);
                                    $('td', row).eq(1).html(lstPlans[0].Name)
                                    $('td', row).eq(4).html($('#startDatePlanModal').val())
                                    $('td', row).eq(5).html($('#endDatePlanModal').val())
                                    $('td', row).eq(7).html($('#cboReason').selectpicker('text'))
                                    $('td', row).eq(8).html(vars.$cboPriority.selectpicker('text'))
                                    $(row).attr('data-priorityid', lstPlans[0].PriorityID)
                                    Loading.fn.Hide()
                                    Alert.fn.Show(Messenger.msgSaveSuccess, Alert.Type.success)
                                }
                                else {
                                    Loading.fn.Hide()
                                    Alert.fn.Show(Messenger.msgSaveNotSuccess + ' (' + data + ')', Alert.Type.error)
                                }
                                $('#modalMasterPlansOfWork').modal('hide');
                            });
                        }
                    });
                },
                WorkOrderSave: function () {
                    $('#myModal').appendTo("body").modal('hide')
                    Loading.fn.Show()
                    var lstWO = new Array();
                    var lsPlanID = new Array();
                    var j = 0;
                    var rowCount = vars.$table.$('input[type="checkbox"]:checked').length;
                    vars.$table.$('input[type="checkbox"]:checked').each(function () {
                        var row = $(this).closest('tr').get(0);
                        $(row).each(function () {
                            lstWO[j] = new Object();
                            lstWO[j].ID = '';
                            lstWO[j].DeviceID = row.cells[2].textContent;
                            lstWO[j].TypeOfMaintenanceID = rowCount > 1 ? $(row).attr('data-typeofmaintenanceid') : $('#cboTypeMaintenance').val();
                            lstWO[j].PlanID = $(row).attr('data-planid');
                            lstWO[j].DateCreated = '1900/01/01 00:00'
                            lstWO[j].TimeCreated = '1900/01/01 00:00'
                            lstWO[j].DueDate = Convert.fn.DateTime(row.cells[5].textContent, 'yyyy/MM/dd')
                            lstWO[j].Reason = row.cells[1].textContent;
                            lstWO[j].TheStatusOfWorkOrderID = 1;
                            lstWO[j].StartDatePlan = Convert.fn.DateTime($('#startDateKH').val(), 'yyyy/MM/dd')
                            lstWO[j].EndDatePlan = Convert.fn.DateTime($('#endDateKH').val(), 'yyyy/MM/dd')
                            lstWO[j].UserCreated = '';
                            lstWO[j].PriorityID = $('td', this).eq(8).attr('data-priorityid')
                            lstWO[j].MonitoredBy = $('#cboMonitoredBy').val();
                            lstWO[j].CreatedBy = $('#cboCreatedBy').val();
                            lsPlanID[j] = new Object();
                            lsPlanID[j].PlanID = $(row).attr('data-planid');
                        });
                        j++
                    });
                    $.post(urlSaveWO, { data: JSON.stringify(lstWO), data2: JSON.stringify(lsPlanID) }, function (data) {
                        if (data == "success") {
                            vars.$table.$('input[type="checkbox"]:checked').each(function () {
                                var row = $(this).parent().parent().get(0);
                                vars.$table.row(row).remove().draw(false)
                            });
                            Loading.fn.Hide()
                            Alert.fn.Show(Messenger.msgSaveSuccess, Alert.Type.success)
                        }
                        else {
                            Loading.fn.Hide()
                            Alert.fn.Show(Messenger.msgSaveNotSuccess + ' (' + data + ')', Alert.Type.error)
                        }
                    });
                },
                TabContentModalSelectIndexChanged: function (e) {
                    if ($(e.target).attr('data-index') == 0) {
                    }
                    else if ($(e.target).attr('data-index') == 1) {
                        method.ValidateInput(function (result) {
                            if (result == true) {
                                method.ChangeMachinesParts()
                            }
                            else {
                                $('a[href="#tabChooseDevice"').tab("show")
                                return;
                            }
                        })
                    }
                    else {
                        method.ValidateInput(function (result) {
                            if (result != true) {
                                $('a[href="#tabChooseDevice"').tab("show")
                                return;
                            }
                        })
                        if (!$.fn.DataTable.isDataTable('#tbWorkSelected')) {
                            Alert.fn.Show(Messenger.msgChooseWork, Alert.Type.warning)
                            $('a[href="#tabChooseWork"').tab("show")
                            return;
                        }
                        if (vars.$tableWorkDaChon.rows().count() == 0) {
                            Alert.fn.Show(Messenger.msgChooseWork, Alert.Type.warning)
                            $('a[href="#tabChooseWork"').tab("show")
                            return;
                        }
                        method.ChangeSpareParts()
                    }
                },
                TabContentsPlanSelectIndexChanged: function (e) {
                    if ($(e.target).attr('data-index') == 0) {

                    }
                    else {
                        var planID = $('#tbPlan tr[class$="selected"]').attr('data-planid');
                        $scope.fn.GetWorkPlan(planID, false)
                    }
                },
                TableWorkSelectedRowChanged: function () {
                    vars.$tableSparePart.$('tr').find('input[type=checkbox]').prop('checked', false);
                    var length = vars.$dataSparePartSelected.length;
                    for (var i = 0; i < length; i++) {
                        if (vars.$dataSparePartSelected[i].WorkID == $(this).attr('data-workid') && vars.$dataSparePartSelected[i].ComponentID == $(this).attr('data-componentid')) {
                            vars.$tableSparePart.$('tr[data-sparepart="' + vars.$dataSparePartSelected[i].SparePartID + '"]').find('input[type=checkbox]').prop('checked', true);
                        }
                    }
                },
                ToggleCheckboxesOnTableWorkSelecting: function () {
                    if (!$.fn.DataTable.isDataTable('#tbWorkSelected')) {
                        vars.$tableWorkDaChon = $('#tbWorkSelected').DataTable({
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
                                { 'width': '300px', 'targets': 0 }
                            ],
                            "searching": false,
                            "lengthChange": false,
                            "lengthMenu": [5],
                            scrollY: 145,
                            scrollX: true,
                        });
                        $($.fn.dataTable.tables(true)).DataTable().columns.adjust().draw(false);
                    }

                    if ($(this).prop('checked')) {
                        var data = [
                            $('#tvComponent li[class$=active]').attr('data-id') + ' - ' + $(this).parent().next().text()
                        ];
                        var rowIndex = vars.$tbWorkSelected.dataTable().fnAddData(data);
                        var row = vars.$tbWorkSelected.dataTable().fnGetNodes(rowIndex);
                        $(row).attr('data-componentid', $('#tvComponent li[class$=active]').attr('data-id'));
                        $(row).attr('data-workid', $(this).parent().parent().attr('data-id'));
                    }
                    else {
                        vars.$tableWorkDaChon.$('tr[data-componentid="' + $('#tvComponent li[class$=active]').attr('data-id') + '"][data-workid=' + $(this).parent().parent().attr('data-id') + ']').each(function () {
                            var row = $(this).get(0);
                            vars.$tableWorkDaChon.row(row).remove().draw(false)
                        });
                    }
                },
                TableWorkRowChanged: function () {
                    var planID = vars.$tbPlanBody.find('tr[class$="selected"]').attr('data-planid')
                    var componentID = $(this).attr('data-componentid');
                    var workID = $(this).attr('data-workid');
                    $scope.fn.GetSparePartForPlan(planID, componentID, workID)
                },
                TreeViewSelectIndexChanged: function () {
                    var componentID = $(this).parent().attr('data-id')
                    $.post(urlGetWorkByComponent, { deviceID: $('#cboDevice').val(), componentID: componentID }, function (data) {
                        if ($.fn.DataTable.isDataTable('#tbWorkSelecting')) {
                            vars.$tbWorkSelecting.dataTable().fnClearTable();
                            vars.$tbWorkSelecting.dataTable().fnDestroy();
                        }
                        if (data.length > 0) {
                            vars.$tableWork = vars.$tbWorkSelecting.DataTable({
                                data: data,
                                columns: [
                                    { data: 'Choose' },
                                    { data: 'Description' },
                                    { data: 'TimePlan' }
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
                                "searching": false,
                                "paging": false,
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
                                    { 'width': 'auto', 'targets': 1 },
                                    { 'width': '80px', 'targets': 2 }
                                ],
                                scrollY: 145,
                                createdRow: function (row, data, dataIndex) {
                                    if (data.hasOwnProperty("WorkID")) {
                                        $(row).attr('data-id', data.WorkID);
                                    }
                                },
                            });
                            $($.fn.dataTable.tables(true)).DataTable().columns.adjust().draw(false);
                            $('#tbWorkSelected tr[data-componentid="' + componentID + '"]').each(function (i, row) {
                                vars.$tableWork.$('tr[data-id=' + $(row).attr('data-workid') + ']').find('input[type="checkbox"]').prop('checked', true);
                            });
                        }
                    })
                },

                DisableControl: function (flag) {
                    vars.$cboDevice.prop('disabled', flag);
                },
                ResetText: function () {
                    $('#txtArticle').val('')
                    $('#startDatePlanModal').val('')
                    $('#endDatePlanModal').val('')
                    vars.$tvDataChangedFlag = false
                },
                ValidateInput: function (callback) {
                    var tmp = $('#cboDevice').val()
                    if (tmp === '') {
                        Alert(Messenger.msgChooseDevice);
                        callback(false);
                        return false;
                    }
                    tmp = $('#startDatePlanModal').val()
                    if (tmp === '') {
                        Alert.fn.Show(Messenger.msgChooseStartDate, Alert.Type.warning);
                        callback(false);
                        return false;
                    }
                    tmp = $('#endDatePlanModal').val()
                    if (tmp === '') {
                        Alert.fn.Show(Messenger.msgChooseEndDate, Alert.Type.warning);
                        callback(false);
                        return false;
                    }



                    msgStartDateGreaterThanEndDate


                    var startDate = new Date(Convert.fn.DateTime($('#startDatePlanModal').val(), 'MM/dd/yyyy'))
                    var endDate = new Date(Convert.fn.DateTime($('#endDatePlanModal').val(), 'MM/dd/yyyy'))
                    if (startDate > endDate) {
                        Alert.fn.Show(Messenger.msgStartDateGreaterThanEndDate, Alert.Type.warning);
                        callback(false);
                        return false;
                    }
                    var article = $('#txtArticle').val()
                    if (article === '') {
                        Alert.fn.Show(Messenger.msgTypeArticle, Alert.Type.warning);
                        callback(false);
                        return false;
                    }
                    callback(true);
                    return true;
                }
            }
            $scope.fn = {
                Init: function () {
                    global.CurrentNamePage = currentNamePage
                    MainMenu.fn.SetActive(menuID);
                    Languages.fn.AutoChangeLanguage()
                    vars = bindVariables();
                    method = fnPrivate
                    $scope.cboOption = '0'
                    vars.$table = $('#tbPlan').DataTable({
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
                        }
                    });
                    vars.$tbPlanBody.on('dblclick doubletap', 'tr', method.ShowDetailAboutMasterPlan)
                    vars.$cboDevice.change(method.GetMachineParts)
                    vars.$tbSparePartSelectedBody.on('click', 'input', method.ToggleCheckboxesOnSparePartSelected)
                    vars.$btnSavePlan.click(method.MasterPlanSave)
                    vars.$btnSaveWO.click(method.WorkOrderSave)
                    vars.$tbWorkBody.on('click', 'tr', method.TableWorkRowChanged);
                    vars.$tbWorkSelected.on('click', 'tr', method.TableWorkSelectedRowChanged);
                    vars.$tbWorkSelectingBody.on('click', 'input', method.ToggleCheckboxesOnTableWorkSelecting);
                    vars.$tvComponent.on('click', '.leaf', method.TreeViewSelectIndexChanged);
                    vars.$tabContents.on('shown.bs.tab', method.TabContentModalSelectIndexChanged)
                    vars.$tabContentsPlan.on('shown.bs.tab', method.TabContentsPlanSelectIndexChanged)
                    Main.fn.InitButtonFloat(buttonFloat)
                    Main.fn.InitDateTimePickerChanged([$('#fromDate'), $('#toDate')], method.FilterData)
                    method.FilterData()
                },
                DropDownValueChanged: function () {
                    Loading.fn.Show()
                    window.setTimeout(function () { method.FilterData(); }, 1500);
                },
                Add: function () {
                    vars.$action = Action.add
                    method.ResetText()
                    method.DisableControl(false)
                    vars.$dataSparePartSelected = new Array()
                    $('a[href="#tabChooseDevice"').tab("show")
                    $('#modalMasterPlansOfWork').modal('show');
                },
                Edit: function () {
                    var planID = $('#tbPlan tr[class$="selected"]').attr('data-planid');
                    if (typeof planID == 'undefined') {
                        Alert.fn.Show(Messenger.msgChooseArticle, Alert.Type.warning);
                        return false;
                    }
                    $('a[href="#tabChooseDevice"').tab("show")
                    vars.$tvDataChangedFlag = false
                    vars.$action = Action.edit
                    method.DisableControl(true)
                    if (vars.$tableSparePart != 'undefined') {
                        vars.$tableSparePart.$('tr').find('input[type=checkbox]').prop('checked', false);
                    }
                    vars.$cboDevice.selectpicker('val', $('#tbPlan tr[class$="selected"]').attr('data-id'));
                    $('#tbPlan tr[class$="selected"]').each(function (i, row) {
                        if (!this.rowIndex) return;
                        $('#txtArticle').val(this.cells[1].textContent)
                        $('#startDatePlanModal').val(this.cells[4].textContent)
                        $('#endDatePlanModal').val(this.cells[5].textContent)
                        $('#cboReason').selectpicker('val', $('td', row).eq(7).attr('data-reasonid'))
                        vars.$cboPriority.selectpicker('val', $('td', row).eq(8).attr('data-priorityid'))
                        return false;
                    });
                    $.post(urlGetPlanDetails, { planID: planID }, function (data) {
                        if (data.times == 1) {
                            vars.$dataSparePartSelected = data.model
                        }

                        if (data.model.length > 0) {
                            if ($.fn.DataTable.isDataTable('#tbWorkSelecting')) {
                                vars.$tbWorkSelecting.dataTable().fnDestroy();
                            }
                            if ($.fn.DataTable.isDataTable('#tbWorkSelected')) {
                                vars.$tbWorkSelected.dataTable().fnDestroy();
                            }
                            vars.$tbWorkSelectedBody.empty();
                            var dataGrouped = Enumerable.From(data.model).GroupBy(
                                function (item) {
                                    return item.WorkID;
                                },
                                function (item) {
                                    return item;
                                },
                                function (WorkID, grouping) {
                                    return {
                                        WorkID: WorkID, item: grouping.source
                                    };
                                }
                            ).ToArray();
                            var length = dataGrouped.length;
                            for (var i = 0; i < length; i++) {
                                vars.$tbWorkSelectedBody.append('<tr data-componentid="' + dataGrouped[i].item[0].ComponentID + '" data-workid="' + dataGrouped[i].WorkID + '"><td>' + dataGrouped[i].item[0].ComponentID + ' - ' + dataGrouped[i].item[0].Description + '</td></tr>')
                            }
                            vars.$tableWorkDaChon = $('#tbWorkSelected').DataTable({
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
                                columnDefs: [{ 'width': '300px', 'targets': 0 }],
                                "searching": false,
                                "lengthChange": false,
                                "lengthMenu": [5],
                                scrollY: 145,
                                scrollX: true,
                            });
                            $($.fn.dataTable.tables(true)).DataTable().columns.adjust().draw(false);
                        }
                    });
                    $('#modalMasterPlansOfWork').modal('show');
                },
                CreateWO: function () {
                    var keys = vars.$table.$('input[type="checkbox"]:checked')
                    if (keys.length == 0) {
                        Alert.fn.Show(Messenger.msgChooseArticle, Alert.Type.info)
                        return;
                    }
                    if (keys.length == 1) {
                        var input = vars.$table.$('input[type="checkbox"]:checked')[0]
                        $.post(urlGetTypeOfMaintenanceByDevice, { deviceID: $(input).parent().parent().attr('data-id') }, function (data) {
                            $('#cboTypeMaintenance').empty()
                            var length = data.length;
                            for (var i = 0; i < length; i++) {
                                $('#cboTypeMaintenance').append('<option value="' + data[i].TypeOfMaintenanceID + '" >' + data[i].Name + '</option>')
                            }
                            $('#cboTypeMaintenance').selectpicker('refresh');
                        });
                    }
                    else {
                        $('#cboTypeMaintenance').empty()
                        $('#cboTypeMaintenance').selectpicker('refresh');
                        $('#cboTypeMaintenance').prop('disabled', true);
                    }
                    $('#myModal').modal('show');
                },
                GetWorkPlan: function (planID, flag) {
                    var tableBody = $('#tbWork tbody');
                    $.post(urlGetGetWorkPlanOfWork, { planID: planID }, function (data) {
                        var i;
                        tableBody.empty();
                        $('#tbSparePart tbody').empty();
                        if (data.length > 0) {
                            var length = data.length;
                            for (i = 0; i < length; i++) {
                                tableBody.append('<tr data-componentid=' + data[i].ComponentID + ' data-workid=' + data[i].WorkID + '><td>' + data[i].ComponentID + ' - ' + data[i].ComponentName + '</td><td style="width: 350px;">' + data[i].Description + '</td><td style="width: 250px;">' + data[i].TimePlan + '</td></tr>');
                            }
                            $('#tbWork tbody tr:first-child').addClass('selected')
                            var componentID = vars.$tbWorkBody.find('tr:first-child').attr('data-componentid');
                            var workID = vars.$tbWorkBody.find('tr:first-child').attr('data-workid');
                            $scope.fn.GetSparePartForPlan(planID, componentID, workID)
                        }
                        if (flag == true) {
                            window.setTimeout(function () { vars.$tabWorkDetail.tab('show') }, 500);
                        }
                    });
                },
                GetSparePartForPlan: function (planID, componentID, workID) {
                    var tableBody = $('#tbSparePart tbody');
                    $.post(urlGetSparePartForPlan, { planID: planID, workID: workID, componentID: componentID }, function (data) {
                        var i;
                        tableBody.empty();
                        if (data.length > 0) {
                            var length = data.length;
                            for (i = 0; i < length; i++) {
                                tableBody.append('<tr data-sparepart=' + data[i].SparePartID + '  ><td>' + data[i].SparePartID + '</td><td>' + data[i].Name + '</td><td>' + data[i].Amount + '</td></tr>');
                            }
                        }
                    });
                    return false;
                }
            }
        })
        app.init = function () {
            angular.bootstrap(document, ['MasterPlansPage'])
        }
        return app;
    })();
    $(function () {
        MasterPlans.init()
    });
})