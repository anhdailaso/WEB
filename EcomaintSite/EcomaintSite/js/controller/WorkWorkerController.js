define(['_MasterPageController', '_MenuPageController', '_DetailsModalController', 'datatables-bootstrap', 'bootstrap-select'], function (module, menu, modal) {
    var WorkWorker = (function () {
        var app = angular.module('WorkWorkerPage', [])
        app.controller('WorkWorkerController', function ($scope, $compile) {
            var Main = module.Main
            var Languages = module.Languages
            var Convert = module.Convert
            var InfoDetails = modal
            var Loading = module.Loading
            var Alert = module.Alert
            var MainMenu = menu
            var BuildTreeView = module.BuildTreeView
            var menuID = 'mnuListWorkWorker'
            var currentNamePage = 'WorkWorkerWeb'
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
                    $tbWO: $('#tbWO'),
                    $cboDevice: $('#cboDevice'),
                    $tbWOBody: $('#tbWO tbody')
                }
            }
            var fnPrivate = {FilterData: function () {
                    $.post(urlFilter, { msnx: $('#cbbDiaDiem').val(), msmay: $('#cbbThietBi').val(), fromDate: $('#fromDate').val(), toDate: $('#toDate').val(), type: -1 }, function (data) {
                        if (data.length > 0) {
                            if ($.fn.DataTable.isDataTable('#tbWO')) {
                                $('#tbWO').dataTable().fnDestroy();
                            }
                            vars.$tableWO = $('#tbWO').DataTable({
                                data: data,
                                columns: [
                                    { data: 'ID' },
                                    { data: 'DeviceID' },
                                    { data: 'DeviceName' },
                                    { data: 'TypeOfMaintenanceName' }
                                ],
                                "columnDefs": [
                                    { 'width': '20%', 'targets': 0 },
                                    { 'width': '20%', 'targets': 1 },
                                    { 'width': '25%', 'targets': 2 },
                                    { 'width': '25%', 'targets': 3 }
                                ],
                                "language": {
                                    "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>",
                                    "sSearch": "<span data-lang='lblSearch'></span> ",
                                    "info": "",
                                    "zeroRecords": "<span data-lang='lblFilterInfo'>" + (global.TypeLanguage === 0 ? "Không tìm thấy" : "No matching records found") + "</span>",
                                    "lengthMenu": "<span data-lang='lblShow'></span> _MENU_ <span data-lang='lblEntries'></span>",
                                    "infoEmpty": "",
                                    "infoFiltered": "",
                                    "paginate": {
                                        "first": "<<",
                                        "last": ">>",
                                        "next": ">",
                                        "previous": "<"
                                    },
                                    "emptyTable": "<span data-lang='lblEmpty'></span>",
                                },
                                "scrollY": 350,
                                "bLengthChange": false,
                                "scrollX": true,
                                "processing": true,
                                createdRow: function (row, data, dataIndex) {
                                    if (data.hasOwnProperty("ID")) {
                                        $(row).attr('data-id', data.ID);
                                        $(row).attr('data-priorityid', data.PriorityID);
                                    }
                                    $('td', row).eq(0).addClass('details')
                                    //$('td', row).eq(1).attr('ng-click', 'fn.GetWorkDetailByWorkWorker("' + data.ID + '", true)');
                                    $compile($('td', row))($scope)
                                }
                            });
                            $($.fn.dataTable.tables(true)).DataTable().columns.adjust().draw(false);
                            vars.$tbWOBody.find('tr:first-child').addClass('selected');
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
                        },
                        "lengthChange": false,
                    });
                    Main.fn.InitDateTimePickerChanged([$('#fromDate'), $('#toDate')], method.FilterData);
                    $('.select2').select2({ theme: "classic" });
                    method.FilterData();
                },
            }
        })
        app.init = function () {
            angular.bootstrap(document, ['WorkWorkerPage'])
        }
        return app;
    })();
    $(function () {
        WorkWorker.init()
    });
})