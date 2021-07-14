define(['_MasterPageController', '_MenuPageController', '_DetailsModalController', 'datatables-bootstrap', 'bootstrap-select'], function (module, menu, modal) {
    var WorkOrder = (function () {
        var app = angular.module('WorkOrderPage', [])
        app.controller('WorkOrderController', function ($scope, $compile) {
            var Main = module.Main
            var Languages = module.Languages
            var Convert = module.Convert
            var InfoDetails = modal
            var Loading = module.Loading
            var Alert = module.Alert
            var MainMenu = menu
            var BuildTreeView = module.BuildTreeView
            var menuID = 'mnuListWorkOrder'
            var currentNamePage = 'WorkOrderWeb'
            var buttonFloat = [
                {
                    "id": "btnMain",
                    "url": "#",
                    "icon": "<i class='fa fa-angle-double-up'></i>"
                },
                {
                    "id": "btnAdd",
                    "url": "#",
                    "icon": "<i class='fa fa-plus-circle'></i>",
                    "lang": "btnThem",
                    "func": "fn.Add"
                },
                {
                    "id": "btnEdit",
                    "url": "#",
                    "icon": "<i class='fa fa-pencil'></i>",
                    "lang": "btnSua",
                    "func": "fn.Edit"
                },
                {
                    "id": "btnRemove",
                    "url": "#",
                    "icon": "<i class='fa fa-trash'></i>",
                    "lang": "btnXoa",
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
                    $tableWO: 'undefined',
                    $tableWork: 'undefined',
                    $tableSparePart: 'undefined',
                    $tbWO: $('#tbWO'),
                    $btnEdit: $('#btnEdit'),
                    $btnSaveWO: $('#btnSaveWO'),
                    $cboOption: $('#cboOption'),
                    $cboDevice: $('#cboDevice'),
                    $cboTypeMaintenance: $('#cboTypeMaintenance'),
                    $cboPriority: $('#cboPriority'),
                    $endDate: $('#endDate'),
                    $startDate: $('#startDate'),
                    $createdDate: $('#createdDate'),
                    $startCorruptDate: $('#startCorruptDate'),
                    $lastDate: $('#lastDate'),
                    $endCorruptDate: $('#endCorruptDate'),
                    $txtWorkOrderNO: $('#txtWorkOrderNO'),
                    $txtWorkOrderID: $('#txtWorkOrderID'),
                    $txtReason: $('#txtReason'),
                    $tbWOBody: $('#tbWO tbody'),
                    $tbWorkBody: $("#tbWorkForWO tbody"),
                    $tbWorkSelectedBody: $('#tbWorkSelected tbody'),
                    $tvComponent: $('#tvComponent'),
                    $tbSparePartSelectedBody: $('#tbSparePartSelected tbody'),
                    $tbSparePartSelected: $('#tbSparePartSelected'),
                    $tbWorkSelecting: $("#tbWorkSelecting"),
                    $tbWorkSelectingBody: $("#tbWorkSelecting tbody"),
                    $tbWorkSelected: $('#tbWorkSelected'),
                    $$tabContents: $('#tabContentModal a[data-toggle="tab"]'),
                    $tabWorkDetail: $('a[href="#tabWorkDetail"]'),
                    $dataSparePartSelected: new Array(),
                    $typeOfMaintennanceTmp: 0
                }
            }
            var fnPrivate = {
                FilterData: function () {
                    $.post(urlFilter, { msnx: $('#cbbDiaDiem').val(), msmay: $('#cbbThietBi').val(), fromDate: $('#fromDate').val(), toDate: $('#toDate').val(), type: $('#cboOption').val() }, function (data) {
                        if (data.length > 0) {
                            if ($.fn.DataTable.isDataTable('#tbWO')) {
                                $('#tbWO').dataTable().fnDestroy();
                            }
                            vars.$tableWO = vars.$tbWO.DataTable({
                                data: data,
                                columns: [
                                    { data: 'Document' },
                                    { data: 'ID' },
                                    { data: 'DeviceID' },
                                    { data: 'DeviceName' },
                                    { data: 'StartDatePlan' },
                                    { data: 'EndDatePlan' },
                                    { data: 'TheStatusOfWorkOrderID' },
                                    { data: 'PriorityName' },
                                    { data: 'TheStatusOfWorkOrderName' },
                                    { data: 'TheStatusDetailsOfWorkOrderName' },
                                    { data: 'Reason' },
                                    { data: 'DateCreated' },
                                    { data: 'TimeCreated' },
                                    { data: 'WorkSiteName' },
                                    { data: 'TypeOfMaintenanceID' },
                                    { data: 'TypeOfMaintenanceName' },
                                    { data: 'CreatedBy' },
                                    { data: 'UserCreated' },
                                    { data: 'StaffName' },
                                    { data: 'MonitoredBy' },
                                    { data: 'BreakdownStartDate' },
                                    { data: 'BreakdownStartTime' },
                                    { data: 'BreakdownEndDate' },
                                    { data: 'BreakdownEndTime' },
                                    { data: 'IssuedID' },
                                    { data: 'LastDateModified' }
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
                                    { 'targets': 0, 'width': '70px' },
                                    { 'width': '150px', 'targets': 1 },
                                    { 'width': '150px', 'targets': 2 },
                                    { 'width': '250px', 'targets': 3 },
                                    { 'width': '100px', 'targets': 4 },
                                    { 'width': '100px', 'targets': 5 },
                                    { 'width': '100px', 'targets': 6 },
                                    { 'width': '60px', 'targets': 7 },
                                    { 'width': '100px', 'targets': [8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25] },
                                ],
                                bLengthChange: false,
                                scrollY: 350,
                                scrollX: true,
                                createdRow: function (row, data, dataIndex) {
                                    if (data.hasOwnProperty("ID")) {
                                        $(row).attr('data-id', data.ID);
                                        $(row).attr('data-priorityid', data.PriorityID);
                                    }
                                    $('td', row).eq(1).addClass('details')
                                    $('td', row).eq(1).attr('ng-click', 'fn.GetWorkDetailByWorkOrder("' + data.ID + '", true)');
                                    $compile($('td', row))($scope)
                                }
                            });
                            $($.fn.dataTable.tables(true)).DataTable().columns.adjust().draw(false);
                        }
                        else {
                            vars.$tableWO.rows().remove().draw();
                        }
                        Loading.fn.Hide();
                    }).fail(function (response) {
                        Loading.fn.Hide()
                        Alert.fn.Show('Please choose datetime approx 1 years')
                    });
                },
                GetMachineParts: function () {
                    method.GetMaintenanceTypeByDevice();
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
                GetMaintenanceTypeByDevice: function () {
                    $.post(urlGetTypeOfMaintenanceByDevice, { deviceID: $('#cboDevice').selectpicker('val') }, function (data) {
                        console.log('123')
                        $('#cboTypeMaintenance').empty()
                        var length = data.length;
                        for (var i = 0; i < length; i++) {
                            $('#cboTypeMaintenance').append('<option value="' + data[i].TypeOfMaintenanceID + '" >' + data[i].TypeOfMaintenanceName + '</option>')
                        }
                        $('#cboTypeMaintenance').selectpicker('refresh');
                        if (vars.$typeOfMaintennanceTmp != 0) {
                            $('#cboTypeMaintenance').selectpicker('val', vars.$typeOfMaintennanceTmp);
                            vars.$typeOfMaintennanceTmp = 0;
                        }
                    });
                },
                ChangeMachinesParts: function () {

                    if (vars.$tvDataChangedFlag == false) {
                        this.GetMachineParts();
                    }
                },
                GetSpareParts: function () {
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
                TabContentModalSelectIndexChanged: function (e) {
                    if ($(e.target).attr('data-index') == 0) {
                        method.ValidateInput(function (result) {
                            if (result == true) {
                                method.ChangeMachinesParts()
                            }
                            else {
                                $('a[href="#tabChooseDevice"').tab("show")
                                console.log('voo')
                                return;
                            }
                        })
                    }
                    else if ($(e.target).attr('data-index') == 1) {
                        method.ValidateInput(function (result) {
                            if (result == true) {
                                method.ChangeMachinesParts()
                            }
                            else {
                                $('a[href="#tabChooseDevice"').tab("show")
                                console.log('voo')
                                return;
                            }
                        })
                    }
                    else {
                        method.ValidateInput(function (result) {
                            if (result != true) {
                                $('a[href="#tabChooseDevice"').tab("show")
                                console.log('voo')
                                return;
                            }
                        })
                        if (!$.fn.DataTable.isDataTable('#tbWorkSelected')) {
                            Alert.fn.Show(Messenger.msgChonCongViec, Alert.Type.warning)
                            $('a[href="#tabChooseWork"').tab("show")
                            return;
                        }
                        if (vars.$tableWorkSelected.rows().count() == 0) {
                            Alert.fn.Show(Messenger.msgChonCongViec, Alert.Type.warning)
                            $('a[href="#tabChooseWork"').tab("show")
                            return;
                        }
                        method.GetSpareParts()
                    }
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
                TableWorkWORowChanged: function () {
                    var workOrderID = vars.$tbWOBody.find('tr[class$="selected"]').attr('data-id')
                    var componentID = $(this).attr('data-componentid');
                    var workID = $(this).attr('data-workid');
                    $scope.fn.GetSparePartByWorkOrder(workOrderID, componentID, workID)
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
                        vars.$tableWorkSelected = $('#tbWorkSelected').DataTable({
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
                        vars.$tableWorkSelected.$('tr[data-componentid="' + $('#tvComponent li[class$=active]').attr('data-id') + '"][data-workid=' + $(this).parent().parent().attr('data-id') + ']').each(function () {
                            var row = $(this).get(0);
                            vars.$tableWorkSelected.row(row).remove().draw(false)
                        });
                    }
                },
                ToggleCheckboxesOnSparePartSelected: function () {
                    if ($(this).prop('checked')) {
                        var i = vars.$dataSparePartSelected.length;
                        vars.$dataSparePartSelected[i] = new Object();
                        vars.$dataSparePartSelected[i].DeviceID = $('#cboDevice').val();
                        vars.$dataSparePartSelected[i].WorkID = vars.$tableWorkSelected.$('tr[class$="selected"]').attr('data-workid');
                        vars.$dataSparePartSelected[i].ComponentID = vars.$tableWorkSelected.$('tr[class$="selected"]').attr('data-componentid');
                        vars.$dataSparePartSelected[i].SparePartID = $(this).closest('tr').attr('data-sparepart');
                        vars.$dataSparePartSelected[i].PlanQuantity = 1
                        vars.$dataSparePartSelected[i].ActualQuantity = 1
                    }
                    else {
                        var length = vars.$dataSparePartSelected.length;
                        for (var j = 0; j < length; j++) {
                            if (vars.$dataSparePartSelected[j].WorkID == vars.$tableWorkSelected.$('tr[class$="selected"]').attr('data-workid') && vars.$dataSparePartSelected[j].ComponentID == vars.$tableWorkSelected.$('tr[class$="selected"]').attr('data-componentid') && vars.$dataSparePartSelected[i].SparePartID == $(this).closest('tr').attr('data-sparepart')) {
                                vars.$dataSparePartSelected.splice(j, 1)
                                break;
                            }
                        }
                    }
                },
                ShowDetailAboutWorkOrder: function () {
                    $(this).each(function () {
                        if (!this.rowIndex) return;
                        InfoDetails.fn.Show($('#tbWO thead tr th'), this.cells);
                        return false;
                    });
                },
                ValidateInput: function (callback) {
                    var tmp = $('#cboDevice').val()
                    if (tmp === '') {
                        Alert(Messenger.msgChooseDevice);
                        callback(false);
                        return false;
                    }
                    tmp = $('#createdDate').val()
                    if (tmp === '') {
                        Alert.fn.Show(Messenger.msgChooseDateCreated, Alert.Type.warning);
                        callback(false);
                        return false;
                    }
                    tmp = $('#startDate').val()
                    if (tmp === '') {
                        Alert.fn.Show(Messenger.msgChooseStartDate, Alert.Type.warning);
                        callback(false);
                        return false;
                    }

                    tmp = $('#endDate').val()
                    if (tmp === '') {
                        Alert.fn.Show(Messenger.msgChooseEndDate, Alert.Type.warning);
                        callback(false);
                        return false;
                    }
                    var startDate = Convert.fn.DateTime($('#startDate').val(), 'MM/dd/yyyy')
                    var endDate = Convert.fn.DateTime($('#endDate').val(), 'MM/dd/yyyy')
                    if (startDate > endDate) {
                        Alert.fn.Show(Messenger.msgStartDateGreaterThanEndDate, Alert.Type.warning);
                        callback(false);
                        return false;
                    }

                    var reason = $('#txtReason').val()
                    if (reason === '') {
                        Alert.fn.Show(Messenger.msgTypeReason, Alert.Type.warning);
                        callback(false);
                        return false;
                    }
                    callback(true);
                    return true;
                },
                WorkOrderSave: function () {
                    method.ValidateInput(function (result) {
                        if (result == false)
                            return false;
                        $('#myModal').appendTo("body").modal('hide')
                        Loading.fn.Show()
                        var lstWO = new Array();
                        lstWO[0] = new Object();
                        lstWO[0].ID = vars.$txtWorkOrderID.val()
                        lstWO[0].WorkOrderNo = vars.$txtWorkOrderNO.val()
                        lstWO[0].DeviceID = vars.$cboDevice.val()
                        lstWO[0].TypeOfMaintenanceID = vars.$cboTypeMaintenance.val();
                        lstWO[0].DateCreated = Convert.fn.DateTime(vars.$createdDate.val().split(' ')[0], 'MM/dd/yyyy') + ' 00:00:00'
                        lstWO[0].TimeCreated = Convert.fn.DateTime('1900/01/01 ' + vars.$createdDate.val().split(' ')[1])
                        lstWO[0].Reason = vars.$txtReason.val()
                        lstWO[0].TheStatusOfWorkOrderID = 1;
                        lstWO[0].StartDatePlan = Convert.fn.DateTime(vars.$startDate.val(), 'MM/dd/yyyy');
                        lstWO[0].EndDatePlan = Convert.fn.DateTime(vars.$endDate.val(), 'MM/dd/yyyy');
                        lstWO[0].BreakdownStartTime = Convert.fn.DateTime('1900/01/01 ' + vars.$startCorruptDate.val().split(' ')[1])
                        lstWO[0].BreakdownStartDate = Convert.fn.DateTime(vars.$startCorruptDate.val().split(' ')[0], 'MM/dd/yyyy') + ' 00:00:00'
                        lstWO[0].BreakdownEndTime = Convert.fn.DateTime('1900/01/01 ' + vars.$endCorruptDate.val().split(' ')[1])
                        lstWO[0].BreakdownEndDate = Convert.fn.DateTime(vars.$endCorruptDate.val().split(' ')[0], 'MM/dd/yyyy') + ' 00:00:00'
                        lstWO[0].LastDateModified = Convert.fn.DateTime(vars.$lastDate.val(), 'MM/dd/yyyy') + ' 00:00:00'
                        lstWO[0].UserCreated = '';
                        lstWO[0].PriorityID = vars.$cboPriority.val();
                        lstWO[0].MonitoredBy = $('#cboCreatedBy').val();
                        lstWO[0].CreatedBy = $('#cboCreatedBy').val();
                        var lstWorkOfWO = new Array();
                        var i = 0;
                        $('#tbWorkSelected tbody tr').each(function (i, row) {
                            lstWorkOfWO[i] = new Object();
                            lstWorkOfWO[i].DeviceID = vars.$cboDevice.val()
                            lstWorkOfWO[i].WorkID = $(this).attr('data-workid')
                            lstWorkOfWO[i].ComponentID = $(this).attr('data-componentid')
                            lstWorkOfWO[i].PriorityID = vars.$cboPriority.val()
                        });
                        if (vars.$action == Action.add) {
                            $.post(urlCreateWO, { data: JSON.stringify(lstWO), data2: JSON.stringify(lstWorkOfWO), data3: JSON.stringify(vars.$dataSparePartSelected) },
                                function (data) {
                                    if (data.Message == "success") {
                                        var startDate = new Date(Convert.fn.DateTime($('#startDate').val(), 'MM/dd/yyyy'))
                                        var fromDate = new Date(Convert.fn.DateTime($('#fromDate').val(), 'MM/dd/yyyy'))
                                        var toDate = new Date(Convert.fn.DateTime($('#toDate').val(), 'MM/dd/yyyy'))
                                        if (startDate <= toDate && startDate >= fromDate) {
                                            var dataRow = {
                                                'Document': '',
                                                'ID': lstWO[0].ID,
                                                'DeviceID': lstWO[0].DeviceID,
                                                'DeviceName': vars.$cboDevice.selectpicker('text'),
                                                'StartDatePlan': $('#startDate').val(),
                                                'EndDatePlan': $('#endDate').val(),
                                                'TheStatusOfWorkOrderID': 1,
                                                'PriorityName': vars.$cboPriority.selectpicker('text'),
                                                'TheStatusOfWorkOrderName': global.TypeLanguage == 0 ? 'Đang soạn' : 'Đang thực hiện',
                                                'TheStatusDetailsOfWorkOrderName': 'Approved for Planning',
                                                'Reason': lstWO[0].Reason,
                                                'DateCreated': lstWO[0].DateCreated,
                                                'TimeCreated': lstWO[0].TimeCreated.split(' ')[1],
                                                'WorkSiteName': '',
                                                'TypeOfMaintenanceID': lstWO[0].TypeOfMaintenanceID,
                                                'TypeOfMaintenanceName': vars.$cboTypeMaintenance.selectpicker('text'),
                                                'CreatedBy': lstWO[0].CreatedBy,
                                                'UserCreated': '',
                                                'StaffName': '',
                                                'MonitoredBy': lstWO[0].MonitoredBy,
                                                'BreakdownStartDate': lstWO[0].BreakdownStartDate,
                                                'BreakdownStartTime': lstWO[0].BreakdownStartTime,
                                                'BreakdownEndDate': lstWO[0].BreakdownEndDate,
                                                'BreakdownEndTime': lstWO[0].BreakdownEndTime,
                                                'IssuedID': '',
                                                'LastDateModified': lstWO[0].LastDateModified,
                                            }

                                            var rowIndex = vars.$tbWO.dataTable().fnAddData(dataRow);
                                            var row = vars.$tbWO.dataTable().fnGetNodes(rowIndex);
                                            $(row).attr('data-id', data.ID);
                                            $(row).attr('data-priorityid', lstWO[0].PriorityID);
                                            vars.$tableWO.order([1, 'desc']).draw();
                                        }
                                        Loading.fn.Hide()
                                        Alert.fn.Show(Messenger.msgSaveSuccess, Alert.Type.success)
                                    }
                                    else {
                                        Loading.fn.Hide()
                                        Alert.fn.Show(Messenger.msgSaveNotSuccess + ' (' + data.Message + ')', Alert.Type.error)
                                    }
                                    $('#modalWorkOrderOfWork').modal('hide');
                                });
                        }
                        else {
                            $.post(urlEdit, { data: JSON.stringify(lstWO), data2: JSON.stringify(lstWorkOfWO), data3: JSON.stringify(vars.$dataSparePartSelected) }, function (data) {
                                if (data == "success") {
                                    var rowIndex = vars.$tableWO.row($('#tbWO tr[class$="selected"]')).index();
                                    var row = vars.$tbWO.dataTable().fnGetNodes(rowIndex);

                                    $('td', row).eq(4).html(vars.$startDate.val())
                                    $('td', row).eq(5).html(vars.$endDate.val())
                                    $('td', row).eq(7).html(vars.$cboPriority.selectpicker('text'))
                                    $('td', row).eq(10).html(lstWO[0].Reason)
                                    $('td', row).eq(11).html(lstWO[0].DateCreated)
                                    $('td', row).eq(12).html(lstWO[0].TimeCreated.split(' ')[1])
                                    $('td', row).eq(16).html(lstWO[0].CreatedBy)
                                    $('td', row).eq(19).html(lstWO[0].CreatedBy)
                                    $('td', row).eq(20).html(lstWO[0].BreakdownStartDate)
                                    $('td', row).eq(21).html(lstWO[0].BreakdownStartTime.split(' ')[1])
                                    $('td', row).eq(22).html(lstWO[0].BreakdownEndDate)
                                    $('td', row).eq(23).html(lstWO[0].BreakdownEndTime.split(' ')[1])
                                    $('td', row).eq(25).html(lstWO[0].LastDateModified)

                                    Loading.fn.Hide()
                                    Alert.fn.Show(Messenger.msgSaveSuccess, Alert.Type.success)

                                }
                                else {
                                    Loading.fn.Hide()
                                    Alert.fn.Show(Messenger.msgSaveNotSuccess + ' (' + data + ')', Alert.Type.error)
                                }
                                $('#modalWorkOrderOfWork').modal('hide');
                            });
                        }
                    })
                },
                ResetText: function () {
                    $('#txtReason').val('')
                    $('#startCorruptDate').val('')
                    $('#endCorruptDate').val('')
                    $('#lastDate').val('')
                    $('#cboPriority option:first').prop('selected', true)
                    $('#endDate').val('')
                    $('#startDate').val('')
                    $('#createdDate').val('')
                    $('#cboCreatedBy option:first').prop('selected', true)
                    $('#cboTypeMaintenance option:first').prop('selected', true)
                    $("#cboDevice option:first").prop('selected', true)
                    $('#txtWorkOrderNO').val('')
                    $('#txtWorkOrderID').val('')
                },
                DisableControl: function (flag) {
                    vars.$cboDevice.prop('disabled', flag);
                    vars.$createdDate.prop('disabled', flag);
                    vars.$cboTypeMaintenance.prop('disabled', flag);
                },
                LoadMay: function (msmay) {
                    $('#cbbThietBi option').remove();
                    $.ajax({
                        type: "POST",
                        url: urlDevices,
                        data: { WorkSiteID: $('#cbbDiaDiem').val() },
                        success: function (data) {
                            var s = '<option value=-1> < ALL > </option>';
                            for (var i = 0; i < data.length; i++) {
                                if (data[i].ID === msmay) {
                                    s += '<option value="' + data[i].ID + '" selected>' + data[i].Name + '</option>';
                                }
                                else {
                                    s += '<option value="' + data[i].ID + '">' + data[i].Name + '</option>';
                                }
                            }
                            $("#cbbThietBi").html(s);
                        }
                    });
                }
            }

            var method
            $scope.fn = {
                Init: function () {
                    global.CurrentNamePage = currentNamePage
                    MainMenu.fn.SetActive(menuID);
                    Languages.fn.AutoChangeLanguage()
                    vars = bindVariables();
                    method = fnPrivate;
                    method.LoadMay();
                    $("#cbbDiaDiem").change(function () {
                        method.LoadMay();
                        method.FilterData();
                    });
                    $("#cbbThietBi").change(function () {
                        method.FilterData();
                    });
                    $scope.cboOption = '1'
                    vars.$tableWO = $('#tbWO').DataTable({
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
                    $("#cboOption").change(function () {
                        Loading.fn.Show();
                        var id = $('#cboOption').val();
                        if (id > 3) {
                            $('#btnRemove').attr("disabled", true);
                        }
                        else {
                            $('#btnRemove').attr("disabled", false);
                        }
                        window.setTimeout(function () {
                            method.FilterData();
                        }, 1000);
                        if (parseInt(vars.$cboOption.val()) > 2) {
                            vars.$btnEdit.prop('disabled', true);
                        }
                        else vars.$btnEdit.prop('disabled', false);
                    });
                    vars.$tbWOBody.on('dblclick', 'tr', method.ShowDetailAboutWorkOrder);
                    vars.$tbWOBody.on('doubletap', 'tr', method.ShowDetailAboutWorkOrder);
                    vars.$tvComponent.on('click', '.tr', method.TreeViewSelectIndexChanged);
                    vars.$tbWorkSelectingBody.on('click', 'tr', method.ToggleCheckboxesOnTableWorkSelecting);
                    vars.$tbWorkSelectedBody.on('click', 'tr', method.TableWorkSelectedRowChanged);
                    vars.$tbWorkBody.on('click', 'tr', method.TableWorkWORowChanged);
                    vars.$tbSparePartSelectedBody.on('click', 'tr', method.ToggleCheckboxesOnSparePartSelected);
                    vars.$cboDevice.change(method.GetMachineParts);
                    vars.$btnSaveWO.click(method.WorkOrderSave);
                    Main.fn.InitButtonFloat(buttonFloat);
                    Main.fn.InitDateTimePickerChanged([$('#fromDate'), $('#toDate')], method.FilterData);
                    if ($('#link').val() === '1') {
                        $('#fromDate').val($('#tngay').val());
                        $('#toDate').val($('#dngay').val());
                        $("#cboOption").val('2');
                        $("#cbbDiaDiem").val($('#nx').val());
                        method.LoadMay('HCSHCM0044');
                    }
                    $('.select2').select2({ theme: "classic" });

                    method.FilterData();
              
                },

                GetWorkDetailByWorkOrder: function (workOrderID, flag) {
                    var tableBody = $('#tbWorkForWO tbody');
                    $.post(urlGetWorkDetailByWorkOrder, { workOrderID: workOrderID }, function (data) {
                        var i;
                        tableBody.empty();
                        $('#tbSparePartWO tbody').empty();
                        if (data.length > 0) {
                            var length = data.length;
                            for (i = 0; i < length; i++) {
                                tableBody.append('<tr data-componentid=' + data[i].ComponentID + '  data-workid=' + data[i].WorkID + ' ><td>' + data[i].ComponentID + '</td><td style="width: 350px;">' + data[i].Description + '</td><td style="width: 250px;">' + data[i].ActualTime + '</td></tr>');
                            }
                            $('#tbWorkForWO tbody tr:first-child').addClass('selected')
                            var componentID = vars.$tbWorkBody.find('tr:first-child').attr('data-componentid');
                            var workID = vars.$tbWorkBody.find('tr:first-child').attr('data-workid');
                            $scope.fn.GetSparePartByWorkOrder(workOrderID, componentID, workID);
                        }
                        if (flag == true) {
                            window.setTimeout(function () { vars.$tabWorkDetail.tab('show') }, 500);
                        }
                    });
                },
                GetSparePartByWorkOrder: function (workOrderID, componentID, workID) {
                    var tableBody = $('#tbSparePartWO tbody');
                    $.post(urlGetSparePartByWorkOrder, { workOrderID: workOrderID, componentID: componentID, workID: workID }, function (data) {
                        var i;
                        tableBody.empty();
                        if (data.length > 0) {
                            var length = data.length;
                            for (i = 0; i < length; i++) {
                                tableBody.append('<tr data-sparepart=' + data[i].SparePartID + ' ><td>' + data[i].SparePartID + '</td><td>' + data[i].Name + '</td><td>' + data[i].PlanQuantity + '</td><td>' + data[i].ActualQuantity + '</td></tr>');
                            }
                        }
                    });
                    return false;
                },
                Add: function () {
                    Loading.fn.Show()
                    method.ResetText()
                    vars.$action = Action.add
                    vars.$tvDataChangedFlag = false
                    vars.$dataSparePartSelected = new Array()
                    method.GetMaintenanceTypeByDevice()
                    $.post(urlGetWorkOrderNumber, function (workOrderNo) {
                        vars.$txtWorkOrderID.val(workOrderNo)
                        vars.$txtWorkOrderNO.val(workOrderNo)
                        Loading.fn.Hide()
                        $('a[href="#tabChooseDevice"').tab("show")
                        $('#modalWorkOrderOfWork').modal('show');
                    })

                },
                Edit: function () {
                    var workOrderID = $('#tbWO tr[class$="selected"]').attr('data-id');
                    if (typeof workOrderID == 'undefined') {
                        Alert.fn.Show("Bạn cần chọn phiếu bảo trì trước khi sửa!", Alert.Type.warning);
                        return false;
                    }
                    $('a[href="#tabChooseDevice"').tab("show")
                    vars.$tvDataChangedFlag = false
                    vars.$action = Action.edit
                    //method.DisableControl(true)
                    if (vars.$tableSparePart != 'undefined') {
                        vars.$tableSparePart.$('tr').find('input[type=checkbox]').prop('checked', false);
                    }
                    $('#tbWO tr[class$="selected"]').each(function (i, row) {
                        if (!this.rowIndex) return;
                        $('#txtWorkOrderNO').val(this.cells[1].textContent)
                        $('#txtWorkOrderID').val(this.cells[1].textContent)
                        vars.$typeOfMaintennanceTmp = this.cells[14].textContent
                        vars.$cboDevice.selectpicker('val', this.cells[2].textContent);
                        method.GetMaintenanceTypeByDevice()
                        $('#startDate').val(this.cells[4].textContent)
                        $('#endDate').val(this.cells[5].textContent)
                        $('#txtReason').val(this.cells[10].textContent)
                        $('#createdDate').val(this.cells[11].textContent + ' ' + this.cells[12].textContent)
                        $('#cboCreatedBy').selectpicker('val', this.cells[16].textContent);
                        $('#startCorruptDate').val(this.cells[20].textContent + ' ' + this.cells[21].textContent)
                        $('#endCorruptDate').val(this.cells[22].textContent + ' ' + this.cells[23].textContent)
                        $('#lastDate').val(this.cells[25].textContent)
                        $('#cboPriority').selectpicker('val', $(this).attr('data-priorityid'));
                        return false;
                    });
                    $.post(urlGetWorkOrderDetail, { workOrderID: workOrderID }, function (data) {
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
                            vars.$tableWorkSelected = $('#tbWorkSelected').DataTable({
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
                                        'width': '300px',
                                        'targets': 0
                                    }
                                ],
                                "searching": false,
                                "lengthChange": false,
                                "lengthMenu": [5],
                                scrollY: 145,
                                scrollX: true,
                            });
                            $($.fn.dataTable.tables(true)).DataTable().columns.adjust().draw(false);
                        }
                    });
                    $('#modalWorkOrderOfWork').modal('show');
                },
                Remove: function () {
                    var workOrderID = $('#tbWO tr[class$="selected"]').attr('data-id');
                    if (typeof workOrderID == 'undefined') {
                        Alert.fn.Show("Bạn cần chọn phiếu bảo trì trước khi sửa!", Alert.Type.warning);
                        return false;
                    }
                    Alert.fn.ShowConfirm("Bạn có muốn xóa phiếu bảo trì này không ? ", Alert.Type.info, "Xóa", function (result) {
                        if (result === true) {
                            $.post(urlRemove, { workOrderID: workOrderID }, function (data) {
                                if (data == "success") {
                                    $('#tbWO tr[class$="selected"]').remove();
                                }
                                else {
                                    Alert.fn.Show(data, Alert.Type.error);
                                }
                            });
                            return false;
                        }
                    });
                },
            }
        })
        app.init = function () {
            angular.bootstrap(document, ['WorkOrderPage'])
        }
        return app;
    })();
    $(function () {
        WorkOrder.init()
    });
})